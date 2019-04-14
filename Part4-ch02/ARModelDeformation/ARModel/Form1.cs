using matrix;
using matrix;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace ARModel
{
    public delegate void Transmit(int m);//通过委托实现窗体传值
    public partial class AR : Form
    {

        int m_Order;//最终模型的时间延迟阶数
        int m_DeformationModelDays;//参与建模的期数       
        double[] m_TP;//模型T检验求得的概率数组
        double m_Fp;//模型F检验求得的概率值
        List<string> m_SheetNameList;     //点名数组 这里只考虑单点，实际上m_SheetNameList集合只有一个元素
        List<double[,]> m_Observes;       //观测值序列数组
        //将绘图控件对象实例化在成员变量位置的原因是为了更改参数后能更新图片
        ZedGraphControl zedGraphControl1 = new ZedGraphControl(), zedGraphControl2 = new ZedGraphControl(), zedGraphControl3 = new ZedGraphControl(), zedGraphControl4 = new ZedGraphControl();//绘图所需要的控件
        ZedGraphControl zedGraphControl5 = new ZedGraphControl();//该绘图控件为报表所用，不可见
        ZedGraphControl zedGraphControl6 = new ZedGraphControl();//该绘图控件绘制原始观测值
        Matrix m_PeriodicTerm = null;//序列的周期项
        Matrix m_Perement;//自回归系数
        Matrix m_FitY, m_SourFitY;//拟合序列和参与拟合的原始序列
        Matrix[] m_AR_PreDataSource, m_AR_Pre, m_AR_PreEoror;//多步预测结果矩阵
        Matrix m_PCF, m_PACF, m_BIC, m_AIC;//PCF为自相关系数矩阵，PACF：为骗自相关矩阵，BIC和AIC分别为BIC和AIC信息准则矩阵
        int m_ExportPreStep = 1;//报表的预测步数
        int m_PreStep;//预测步数
        bool bFail = false;//判断是否已经进行AR建模
        CheckBox checkBox1, checkBox2, checkBox3, checkBox4;//曲线可视化选取按钮
        public AR()
        {
            InitializeComponent();
        }

        private void AR_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = null;
            this.WindowState = FormWindowState.Maximized;
            //PCF和PACF绘图板初始化
            tabControl1.TabPages[1].Controls.Add(zedGraphControl1);
            zedGraphControl1.Visible = false;
            panel4.Controls.Add(zedGraphControl1);
            //BIC和AIC绘图板初始化          
            zedGraphControl3.Visible = false;
            tabControl1.TabPages[1].Controls.Add(zedGraphControl3);
            panel4.Controls.Add(zedGraphControl3);
            //模型拟合绘图板初始化
            zedGraphControl4.Location = new Point(5, 0);
            zedGraphControl4.Visible = false;
            tabControl1.TabPages[2].Controls.Add(zedGraphControl4);
            panel2.Controls.Add(zedGraphControl4);
            checkBox3 = new CheckBox();
            zedGraphControl4.Controls.Add(checkBox3);
            checkBox3.Location = new Point(15, 5);
            checkBox3.Checked = true;
            checkBox3.CheckedChanged += Checkbox3_CheckedChanged; ;
            checkBox4 = new CheckBox();
            zedGraphControl4.Controls.Add(checkBox4);
            checkBox4.Checked = true;
            checkBox4.CheckedChanged += Checkbox4_CheckedChanged;
            //模型预测绘图板初始化
            zedGraphControl2.Location = new Point(5, 0);
            zedGraphControl2.Visible = false;
            tabControl1.TabPages[3].Controls.Add(zedGraphControl2);
            panel3.Controls.Add(zedGraphControl2);
            checkBox1 = new CheckBox();
            zedGraphControl2.Controls.Add(checkBox1);
            checkBox1.Checked = true;
            checkBox1.CheckedChanged += Checkbox1_CheckedChanged;

            checkBox2 = new CheckBox();
            zedGraphControl2.Controls.Add(checkBox2);
            checkBox2.Checked = true;
            checkBox2.CheckedChanged += Checkbox2_CheckedChanged;

            //报表绘图板初始化
            zedGraphControl5.Location = new Point(390, 0);
            zedGraphControl5.Visible = false;
            tabControl1.TabPages[0].Controls.Add(zedGraphControl5);
            //原始序列绘制
            zedGraphControl6.Location = new Point(5, 0);
            zedGraphControl6.Visible = false;
            tabControl1.TabPages[0].Controls.Add(zedGraphControl6);
            panel1.Controls.Add(zedGraphControl6);

        }

        private void Checkbox4_CheckedChanged(object sender, EventArgs e)
        {
            if (zedGraphControl4.MasterPane.PaneList[0].CurveList.Count == 2)
            {
                if (checkBox4.Checked)
                {
                    zedGraphControl4.MasterPane.PaneList[0].CurveList[1].IsVisible = true;
                    zedGraphControl4.Refresh();
                }
                else
                {
                    zedGraphControl4.MasterPane.PaneList[0].CurveList[1].IsVisible = false;
                    zedGraphControl4.Refresh();
                }
            }
        }

        private void Checkbox3_CheckedChanged(object sender, EventArgs e)
        {
            if (zedGraphControl4.MasterPane.PaneList[0].CurveList.Count == 2)
            {
                if (checkBox3.Checked)
                {
                    zedGraphControl4.MasterPane.PaneList[0].CurveList[0].IsVisible = true;
                    zedGraphControl4.Refresh();
                }
                else
                {
                    zedGraphControl4.MasterPane.PaneList[0].CurveList[0].IsVisible = false;
                    zedGraphControl4.Refresh();
                }
            }
        }

        private void Checkbox2_CheckedChanged(object sender, EventArgs e)
        {
            if (zedGraphControl2.MasterPane.PaneList[0].CurveList.Count == 2)
            {
                if (checkBox2.Checked)
                {
                    zedGraphControl2.MasterPane.PaneList[0].CurveList[1].IsVisible = true;
                    zedGraphControl2.Refresh();
                }
                else
                {
                    zedGraphControl2.MasterPane.PaneList[0].CurveList[1].IsVisible = false;
                    zedGraphControl2.Refresh();
                }
            }
        }

        private void Checkbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (zedGraphControl2.MasterPane.PaneList[0].CurveList.Count == 2)
            {
                if (checkBox1.Checked)
                {
                    zedGraphControl2.MasterPane.PaneList[0].CurveList[0].IsVisible = true;
                    zedGraphControl2.Refresh();
                }
                else
                {
                    zedGraphControl2.MasterPane.PaneList[0].CurveList[0].IsVisible = false;
                    zedGraphControl2.Refresh();
                }
            }
        }


        #region
        //private string MyPointValueHandler(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        //{
        //    PointPair pt = curve[iPt];
        //    return "X=" + pt.X.ToString() + " \n Y=" + pt.Y.ToString();

        //}
        #endregion


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ImportToolStripMenuItem.PerformClick();

        }





        private void ARModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "数据导入成功！";
            int i_MDays, i_PreDays;
            progressBar1.Value = Convert.ToInt32(0);
            if (m_Observes == null)
            {
                MessageBox.Show("没有数据，请单击导入数据!");
                return;
            }
            if (textBox2.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("请选择建模期数和预测期数！");
                return;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("请选择预测期数！");
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("请选择建模期数！");
                return;
            }
            else if (!(Int32.TryParse(textBox2.Text, out i_MDays) && (i_MDays > 0) && Int32.TryParse(textBox3.Text, out i_PreDays) & i_PreDays > 0))
            {
                MessageBox.Show("建模期数和预测期数只能为正整数!");
                return;
            }

            if (Convert.ToInt32(textBox2.Text) >= m_Observes[0].GetLength(0))
            {
                MessageBox.Show("选择建模期数大于等于导入数据期数,请重选建模期数！");
                return;
            }
            if (Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox2.Text) > m_Observes[0].GetLength(0))
            {
                MessageBox.Show("选择预测期数大于导入数据期数，建模结果无法对比,请重选预测期数！");
                return;
            }
            double d_Leve;
            if (Double.TryParse(textBox4.Text, out d_Leve))
            {
                if (d_Leve <= 0 || d_Leve >= 1)
                {
                    MessageBox.Show("显著水平必须大于0且小于1！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("显著水平必须为double类型数据!");
                return;
            }
            //对多步预测步数设置异常对话框
            if (textBox1.Text == "")
            {
                m_PreStep = 1;
            }
            else if (!(Int32.TryParse(textBox1.Text, out i_MDays) && (i_MDays > 0)))
            {
                MessageBox.Show("预测步数必须为正整数!");
                return;
            }
            else if (Convert.ToInt32(textBox3.Text) < Convert.ToInt32(textBox1.Text))
            {
                MessageBox.Show("预测步数必须小于等于预测期数！");
                return;
            }
            else
            {
                m_PreStep = Convert.ToInt32(textBox1.Text);//获取多步预测的步数
            }
            try
            {
                zedGraphControl1.Visible = false;
                zedGraphControl2.Visible = false;
                zedGraphControl3.Visible = false;
                zedGraphControl4.Visible = false;
                /////////////////////////////////////
                dataGridView1.Rows.Clear();        //
                dataGridView1.Columns.Clear();     //   
                dataGridView2.Rows.Clear();        //
                dataGridView2.Columns.Clear();     // 每一次计算时先清空所有的DataGridView表格  
                dataGridView3.Rows.Clear();        //   
                dataGridView3.Columns.Clear();     //
                /////////////////////////////////////
                Matrix Deal_Y = CARFuction.RemovePeridTerm(m_Observes[0], out m_PeriodicTerm);         //去趋势后的序列
                Matrix Deal_Y_Model = Deal_Y.GetR(0, Convert.ToInt32(textBox2.Text) - 1);           //参与建模的去趋势后的序列
                Matrix Deal_Y_PreModel = Deal_Y.GetR(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) - 1);//预测期数的去趋势后的序列

                int T_lag = 40;   //计算时间延迟从1到40阶，目的是为了搜寻模型的合适阶数
                if (Convert.ToInt32(textBox2.Text) < 40)
                {
                    MessageBox.Show("为了搜寻模型阶数，建模数据必须大于等于40期！");
                    return;
                }
                progressBar1.Value = Convert.ToInt32(20);
                m_PCF = CARFuction.GetACF(Deal_Y_Model, T_lag);//计算自相关系数PCF    
                progressBar1.Value = Convert.ToInt32(40);
                m_PACF = CARFuction.GetPACF(T_lag, m_PCF);       //计算偏自相关系数PACF
                progressBar1.Value = Convert.ToInt32(60);
                //绘制PCF,PACF条形统计图
                Graph.GraphPCF(panel4.Width, panel4.Height, zedGraphControl1, m_PCF, m_PACF, false);
                //模型定阶，通过AIC和BIC准则进行对比

                m_BIC = CARFuction.GetBICAndAIC(Deal_Y_Model, T_lag, out m_AIC);//返回BIC数组
                progressBar1.Value = Convert.ToInt32(80);
                //绘制BIC和AIC图
                Graph.GraphBIC(panel4.Width, panel4.Height, zedGraphControl3, m_BIC, m_AIC, false);
                int[] BICMinIndex = CARFuction.FindMin(m_BIC);//求最小值的索引
                m_Order = BICMinIndex[0] + 1;//确定模型的阶数
                Matrix _A;//最小二乘的系数矩阵

                m_Perement = CARFuction.LSE(m_Order, Deal_Y_Model, out _A); //最小二乘法估计
                m_FitY = _A * m_Perement;
                //模型的假设检验
                m_SourFitY = Deal_Y_Model.GetR(m_Order, Deal_Y_Model.Rows - 1);
                double dFP;//F检验的概率值
                m_TP = CARFuction.ModingTest(m_SourFitY, m_Order, m_FitY, m_Perement, _A, out dFP);//返回T检验的概率值
                if (m_TP == null)

                {
                    MessageBox.Show("出现自由度为负数，建模数据太少！");
                    return;
                }
                if (dFP >= Convert.ToDouble(textBox4.Text))
                {
                    MessageBox.Show("模型未通过F检验,序列不适合用AR模型！");
                    return;
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                dataGridView3.RowHeadersVisible = false;//隐藏行头                                                                     //
                dataGridView3.Columns.Add("col0", "");                                                                                 //
                dataGridView3.Columns.Add("col1", "整体F检验");                                                                        //
                for (int i = 0; i < m_Order; i++)                                                                                      //
                {                                                                                                                      //
                    dataGridView3.Columns.Add("col" + (i + 2), (i + 1) + "阶延迟");                                                    //
                }                                                                                                                      //
                foreach (DataGridViewColumn item in dataGridView3.Columns)//单元格居中                                                 //
                {                                                                                                                      //
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;                                                            //
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                                       //
                    item.DefaultCellStyle.BackColor = Color.Gainsboro;                                                                 //
                }                                                                                                                      //
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;                                              //模型检验的界面可视化
                dataGridView3.EnableHeadersVisualStyles = false;//在启动了可视样式的时候，BackColor和ForeColor的值会被忽略。           //
                dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;                                                    //
                dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                     //    
                dataGridView3.Rows.Add();                                                                                              //
                dataGridView3.Rows.Add();                                                                                              //
                dataGridView3[0, 0].Value = "系数";                                                                                    //
                dataGridView3[0, 1].Value = "P";                                                                                       //
                dataGridView3[1, 0].Value = "—";                                                                                      //
                dataGridView3[1, 1].Value = Math.Round(dFP, 2);                                                                        //
                for (int i = 0; i < m_Order; i++)                                                                                      //
                {                                                                                                                      //
                    dataGridView3[i + 2, 1].Value = Math.Round(m_TP[i], 2);                                                            //
                    dataGridView3[i + 2, 0].Value = Math.Round(m_Perement[i, 0], 4);                                                   //
                }                                                                                                                      //
                                                                                                                                       //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                m_FitY = m_FitY + m_PeriodicTerm.GetR(m_Order, Deal_Y_Model.Rows - 1);//加上去掉的趋势项
                m_SourFitY = m_SourFitY + m_PeriodicTerm.GetR(m_Order, Deal_Y_Model.Rows - 1);//加上去掉的趋势项

                int PreDays = Convert.ToInt32(textBox3.Text);//获取预测期数
                m_DeformationModelDays = Convert.ToInt32(textBox2.Text);//获取建模期数
                #region
                //Matrix Mid_Deal_Pre_Data = Deal_Y.GetR(Convert.ToInt32(textBox2.Text) - m_Order, Convert.ToInt32(textBox2.Text) 
                //    + Convert.ToInt32(textBox3.Text) - 1);
                //Matrix[] B1 = new Matrix[m_Order];
                //for (int i = 0; i <m_Order; i++)
                //{
                //    B1[i] = Mid_Deal_Pre_Data.GetR(m_Order - i - 1, Mid_Deal_Pre_Data.Rows - 1 - i-1);
                //}
                //Matrix[] AR_Pre = new Matrix[i_PreStep];
                //for (int i = 0; i < i_PreStep; i++)
                //{
                //    Matrix ARPre = null;
                //    for (int j = 0; j < m_Order; j++)
                //    {
                //        if (j == 0)
                //            ARPre = new Matrix(B1[j].Rows, 1);
                //        ARPre = ARPre + B1[j] * m_Perement[j];
                //    }
                //    for (int p = 0; p < B1.Length - 1; p++)
                //    {
                //        B1[p + 1] = B1[p];
                //    }
                //    B1[0] = ARPre;
                //    for (int k = 0; k < B1.Length; k++)
                //    {
                //        B1[k] = B1[k].GetR(0, B1[k].Rows - 1 - 1);
                //    }

                //    AR_Pre[i] = ARPre;
                //}

                #endregion
                //多步预测
                CARFuction.MulPre(m_Order, PreDays, m_DeformationModelDays, Deal_Y, m_Perement, m_PeriodicTerm, m_PreStep, out m_AR_PreDataSource, out m_AR_Pre, out m_AR_PreEoror);
                comboBox1.Items.Clear();
                for (int i = 0; i < m_PreStep; i++)//为下拉框添加选项
                {
                    comboBox1.Items.Add(i + 1);
                }
                bFail = true;
                toolStripStatusLabel2.Text = "AR建模计算完成！";
                progressBar1.Value = Convert.ToInt32(100);


                tabControl1.SelectedTab = tabPage1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Value = Convert.ToInt32(0);
            try
            {
                OpenFileDialog OpenExcelFile = new OpenFileDialog();
                OpenExcelFile.Filter = "(Excel文件)|*.xlsx|(Excel文件)|*.xls";
                OpenExcelFile.Title = "导入数据";
                OpenExcelFile.InitialDirectory = Directory.GetCurrentDirectory();
                string FilePath = null;
                if (OpenExcelFile.ShowDialog() == DialogResult.OK)
                {
                    FilePath = OpenExcelFile.FileName;
                }
                else
                {
                    return;
                }
                m_SheetNameList = new List<string>();           //获取点名数组
                m_Observes = new List<double[,]>();             //获取观测值序列数组

                IWorkbook st = null;
                List<DataTable> tb = new List<DataTable>();
                string directory = Path.GetDirectoryName(FilePath);//获取路径
                using (FileStream s = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    string sd = Path.GetExtension(FilePath);
                    if (Path.GetExtension(FilePath) == ".xls")
                    {
                        st = new HSSFWorkbook(s);                          //Exvel2003及以下的版本
                    }
                    else if (Path.GetExtension(FilePath) == ".xlsx")
                    {
                        st = new XSSFWorkbook(s);                           //Excel2007及以上的版本
                    }
                    for (int i = 0; i < st.NumberOfSheets; i++)
                    {
                        m_SheetNameList.Add(st.GetSheetName(i));
                        ISheet sheet = st.GetSheetAt(i);
                        m_Observes.Add(new double[sheet.LastRowNum + 1, 1]);
                        if (sheet.LastRowNum < 0)
                        {
                            MessageBox.Show("警告：表" + sheet.SheetName + "导入的数据不存在");
                            continue;
                        }
                        for (int j = 0; j <= sheet.LastRowNum; j++)         //第一行不设置表头
                        {
                            IRow row = sheet.GetRow(j);

                            m_Observes[i][j, 0] = Convert.ToDouble(row.GetCell(0).ToString());

                        }
                    }
                    dataGridView4.Rows.Clear();
                    dataGridView4.Columns.Clear();
                    Graph.GraphDataSource(panel1.Width, panel1.Height, zedGraphControl6, m_Observes[0], m_SheetNameList[0], dataGridView4);
                    int i_DefultModelDays = (int)(m_Observes[0].GetLength(0) * 4.0 / 5);
                    int i_DefultPreDays = m_Observes[0].GetLength(0) - i_DefultModelDays;
                    textBox2.Text = Convert.ToString(i_DefultModelDays);
                    textBox3.Text = Convert.ToString(i_DefultPreDays);
                    textBox1.Text = Convert.ToString(1);
                    textBox4.Text = Convert.ToString(0.05);
                    tabControl1.SelectedTab = tabPage4;



                }
                toolStripStatusLabel2.Text = "数据导入成功！";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Tran(int m)//该函数通过委托在Export窗体实现传值，（将Export中的报表预测步数传递给成员变量：m_ExportPreStep ）
        {
            this.m_ExportPreStep = m;
        }
        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int Width = panel4.Width;
            int Height = panel4.Height;
            toolStripStatusLabel2.Text = "AR建模计算完成！";
            if (m_Observes == null)
            {
                MessageBox.Show("没有导入数据!");
                return;
            }
            if (!bFail)
            {
                MessageBox.Show("请先单击AR建模进行计算!");
                return;
            }

            try
            {
                if (m_PreStep != 1)//当预测步数等于1时，不需要弹出对话框
                {
                    Transmit transmit = Tran;
                    Export exportform = new Export(transmit, m_PreStep);
                    exportform.ShowDialog();
                }
                if (m_ExportPreStep == 0)
                    return;
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "(Excel文件)|*.xlsx|(Excel文件)|*.xls";
                saveFile.Title = "成果导出";
                saveFile.InitialDirectory = Directory.GetCurrentDirectory();
                saveFile.FileName = "点" + m_SheetNameList[0] + "的AR建模结果";
                string FilePath = null;
                if (saveFile.ShowDialog() == DialogResult.OK)
                {

                    FilePath = saveFile.FileName;
                }
                else
                {
                    return;
                }

                FileStream filestream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (filestream == null)
                {
                    return;
                }
                IWorkbook workbook;//创建Excel工作簿
                if (Path.GetExtension(FilePath) == ".xls")
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    workbook = new XSSFWorkbook();
                }







                FileStream filestream1 = null;
                FileStream fs = null;
                for (int k = 0; k < this.m_ExportPreStep; k++)
                {
                    if (k != 0)
                    {
                        fs = new FileStream(FilePath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                        filestream1 = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                        if (Path.GetExtension(FilePath) == ".xls")
                        {
                            workbook = new HSSFWorkbook(filestream1);
                        }
                        else
                        {
                            workbook = new XSSFWorkbook(filestream1);
                        }
                    }

                    ISheet sheet1 = workbook.CreateSheet((k + 1) + "步预测");
                    sheet1.Header.Center = "AR变形建模";//页眉
                    sheet1.Footer.Left = "单位：中南大学";//左页脚
                    sheet1.Footer.Right = "作者：杨志佳";//右页脚
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));//起始行，终止行，起始列，终止列

                    IRow row1 = sheet1.CreateRow(0);//创建行
                    ICell cell = row1.CreateCell(0);
                    cell.SetCellValue(m_SheetNameList[0] + "AR建模" + (k + 1) + "步预测");
                    ICellStyle cellStylerow1 = workbook.CreateCellStyle();
                    IFont font = workbook.CreateFont();
                    font.FontHeight = 20 * 20;
                    font.Color = NPOI.HSSF.Util.HSSFColor.Red.Index; ;
                    cellStylerow1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cell.CellStyle = cellStylerow1;
                    cell.CellStyle.SetFont(font);



                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                    cellStyle.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                    cellStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                    cellStyle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;


                    cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Green.Index;
                    cellStyle.FillPattern = FillPattern.SolidForeground;



                    ICell cel111 = sheet1.CreateRow(1).CreateCell(0);
                    cel111.SetCellValue("期数");

                    ICell cel112 = sheet1.GetRow(1).CreateCell(1);
                    cel112.SetCellValue("观测值(mm)");
                    sheet1.SetColumnWidth(1, 100 * 28);
                    ICell cell13 = sheet1.GetRow(1).CreateCell(2);
                    cell13.SetCellValue("预测值(mm)");
                    sheet1.SetColumnWidth(2, 100 * 28);
                    ICell cell14 = sheet1.GetRow(1).CreateCell(3);
                    cell14.SetCellValue("残差(mm)");
                    sheet1.SetColumnWidth(3, 100 * 28);//设置列宽

                    cel111.CellStyle = cellStyle;
                    cel112.CellStyle = cellStyle;
                    cell13.CellStyle = cellStyle;
                    cell14.CellStyle = cellStyle;

                    cel111 = null;
                    cel112 = null;
                    cell13 = null;
                    cell14 = null;
                    ICellStyle CellStyle1 = workbook.CreateCellStyle();
                    CellStyle1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    CellStyle1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    CellStyle1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    CellStyle1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                    CellStyle1.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                    CellStyle1.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                    CellStyle1.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                    CellStyle1.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;

                    CellStyle1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                    CellStyle1.FillPattern = FillPattern.SolidForeground;

                    for (int i = 0; i < m_AR_PreEoror[k].Rows; i++)
                    {

                        ICell cell1 = sheet1.CreateRow(i + 2).CreateCell(0);
                        ICell cell2 = sheet1.GetRow(i + 2).CreateCell(1);
                        ICell cell3 = sheet1.GetRow(i + 2).CreateCell(2);
                        ICell cell4 = sheet1.GetRow(i + 2).CreateCell(3);

                        cell1.SetCellValue(m_DeformationModelDays + i + 1 + k);
                        cell2.SetCellValue(m_AR_PreDataSource[k][i, 0]);
                        cell3.SetCellValue(Math.Round(m_AR_Pre[k][i, 0], 2));
                        cell4.SetCellValue(Math.Round(m_AR_PreEoror[k][i, 0], 2));
                        //以EXcel内置函数设置保留位数
                        IDataFormat format = workbook.CreateDataFormat();
                        cell2.CellStyle.DataFormat = format.GetFormat("0.00");
                        //设置单元格居中
                        CellStyle1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        cell1.CellStyle = CellStyle1;
                        cell2.CellStyle = CellStyle1;
                        cell3.CellStyle = CellStyle1;
                        cell4.CellStyle = CellStyle1;

                        cell1 = null;
                        cell2 = null;
                        cell3 = null;
                        cell4 = null;
                    }

                    if (k == 0)
                    {
                        for (int i = 0; i < m_Order; i++)
                        {
                            ICell TestCel0 = sheet1.GetRow(1).CreateCell(i + 2 + 7);
                            ICell TestCel1 = sheet1.GetRow(2).CreateCell(i + 2 + 7);
                            ICell TestCel2 = sheet1.GetRow(3).CreateCell(i + 2 + 7);
                            TestCel0.SetCellValue((i + 1) + "阶延迟");
                            TestCel1.SetCellValue(Math.Round(m_Perement[i, 0], 4));
                            TestCel2.SetCellValue(Math.Round(m_TP[i], 2));
                            TestCel0.CellStyle = cellStyle;
                            TestCel1.CellStyle = CellStyle1;
                            TestCel2.CellStyle = CellStyle1;

                        }

                        ICell TestCelT1 = sheet1.GetRow(1).CreateCell(7);
                        ICell TestCelT2 = sheet1.GetRow(2).CreateCell(7);
                        ICell TestCelT3 = sheet1.GetRow(3).CreateCell(7);
                        ICell TestCelT4 = sheet1.GetRow(1).CreateCell(8);
                        ICell TestCelT5 = sheet1.GetRow(2).CreateCell(8);
                        ICell TestCelT6 = sheet1.GetRow(3).CreateCell(8);

                        TestCelT2.SetCellValue("系数"); TestCelT4.SetCellValue("整体F检验");
                        TestCelT3.SetCellValue("P"); TestCelT5.SetCellValue("—");
                        TestCelT6.SetCellValue(Math.Round(m_Fp, 2));
                        TestCelT1.CellStyle = cellStyle; TestCelT2.CellStyle = cellStyle;
                        TestCelT3.CellStyle = cellStyle; TestCelT4.CellStyle = cellStyle;
                        TestCelT5.CellStyle = CellStyle1; TestCelT6.CellStyle = CellStyle1;


                        string ACFDirectory = Directory.GetCurrentDirectory() + "/ACF.bmp";
                        string BICDirectory = Directory.GetCurrentDirectory() + "/BIC.bmp";
                        string FitYDirectory = Directory.GetCurrentDirectory() + "/FitY.bmp";

                        Graph.GraphBIC(0, 0, zedGraphControl3, m_BIC, m_AIC);
                        Graph.GraphPCF(0, 0, zedGraphControl1, m_PCF, m_PACF);
                        zedGraphControl1.GetImage().Save(ACFDirectory);
                        zedGraphControl3.GetImage().Save(BICDirectory);
                        Graph.GraphFitY(0, 0, zedGraphControl5, m_SourFitY, m_FitY, m_Order, m_SheetNameList[0], Fag: false);
                        zedGraphControl5.GetImage().Save(FitYDirectory);
                        UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                        byte[] ACFDirectorybyte = File.ReadAllBytes(ACFDirectory);
                        byte[] BICDirectorybyte = File.ReadAllBytes(BICDirectory);
                        byte[] FitYDirectorybyte = File.ReadAllBytes(FitYDirectory);
                        string PreYDirectory = Directory.GetCurrentDirectory() + "/PreY" + (k + 1) + ".bmp";
                        Graph.GraphPreY(0, 0, zedGraphControl5, m_AR_Pre[k], m_AR_PreEoror[k], m_AR_PreDataSource[k], m_DeformationModelDays, m_SheetNameList[0], MulStep: k + 1, Fag: false);
                        zedGraphControl5.GetImage().Save(PreYDirectory);
                        byte[] PreYDirectorybyte = File.ReadAllBytes(PreYDirectory);


                        InsertPicture(workbook, sheet1, ACFDirectorybyte, 0, 0, 0, 0, 5, 9, 10, 28, FilePath);
                        InsertPicture(workbook, sheet1, BICDirectorybyte, 0, 0, 0, 0, 10, 9, 16, 28, FilePath);
                        InsertPicture(workbook, sheet1, FitYDirectorybyte, 0, 0, 767, 63, 5, 30, 16, 55, FilePath);
                        InsertPicture(workbook, sheet1, PreYDirectorybyte, 0, 0, 767, 63, 5, 57, 16, 82, FilePath);

                        File.Delete(ACFDirectory);
                        File.Delete(BICDirectory);
                        File.Delete(FitYDirectory);
                        File.Delete(PreYDirectory);
                    }
                    else
                    {
                        string PreYDirectory = Directory.GetCurrentDirectory() + "/PreY" + (k + 1) + ".bmp";
                        Graph.GraphPreY(0, 0, zedGraphControl5, m_AR_Pre[k], m_AR_PreEoror[k], m_AR_PreDataSource[k], m_DeformationModelDays, m_SheetNameList[0], MulStep: k + 1, Fag: false);
                        zedGraphControl5.GetImage().Save(PreYDirectory);
                        byte[] PreYDirectorybyte = File.ReadAllBytes(PreYDirectory);
                        InsertPicture(workbook, sheet1, PreYDirectorybyte, 0, 0, 767, 63, 5, 9, 16, 34, FilePath);
                        File.Delete(PreYDirectory);
                    }


                    if (k == 0)
                    {
                        filestream.Flush();
                        workbook.Write(filestream);   //将工作簿写入文件流
                        workbook = null;
                        filestream.Close();

                    }
                    else
                    {
                        fs.Flush();
                        workbook.Write(fs);
                        workbook = null;
                        fs.Close();
                        filestream1.Close();


                    }


                }
                toolStripStatusLabel2.Text = "成果导出成功！";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ExitingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PreToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (m_Observes == null)
            {
                MessageBox.Show("没有导入数据！");
                return;
            }
            if (!bFail)
            {
                MessageBox.Show("请先单击AR建模进行计算!");
                return;
            }
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            tabControl1.SelectedTab = tabPage2;

            Graph.GraphPreY(panel3.Width, panel3.Height, zedGraphControl2, m_AR_Pre[0], m_AR_PreEoror[0], m_AR_PreDataSource[0], m_DeformationModelDays, m_SheetNameList[0], dataGridView2);
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            ExportToolStripMenuItem.PerformClick();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ARModelToolStripMenuItem.PerformClick();
        }



        private void HelpWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CARFuction.HelpWord(), "帮助");
        }

        private void RegardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CARFuction.Regarding(), "关于");
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            FitGraphToolStripMenuItem.PerformClick();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            PreToolStripMenuItem.PerformClick();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            HelpWordToolStripMenuItem.PerformClick();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            RegardToolStripMenuItem.PerformClick();
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            zedGraphControl6.Size = new Size((int)(0.96740 * panel1.Width), (int)(0.59834 * panel1.Height));
            zedGraphControl6.MasterPane.PaneList[0].Rect = new RectangleF((int)(0.015778 * panel1.Width), (int)(0.04126 * panel1.Height), (int)(0.94637 * panel1.Width), (int)(0.55020 * panel1.Height));//设置分图的大小和位置900, 300
            zedGraphControl6.MasterPane.PaneList[0].Chart.Rect = new RectangleF((int)(0.015778 * panel1.Width) + 55, (int)(0.11004 * panel1.Height), (int)(0.78864 * panel1.Width), (int)(0.55020 * panel1.Height) - 40 - (int)(0.11004 * panel1.Height));






        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            int Width = panel2.Width;
            int Height = panel2.Height;
            zedGraphControl4.Size = new Size((int)(0.94117 * Width), (int)(0.65753 * Height));
            zedGraphControl4.MasterPane.PaneList[0].Rect = new RectangleF((int)(0.01764 * Width), (int)(0.04109 * Height), (int)(0.91764 * Width), (int)(0.38356 * Height));//设置分图的大小和位置900, 300
            zedGraphControl4.MasterPane.PaneList[0].Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.04109 * Height) + 40, (int)(0.74117 * Width), (int)(0.27397 * Height));
            checkBox3.Location = new Point((int)(zedGraphControl4.MasterPane.PaneList[0].Rect.X + zedGraphControl4.MasterPane.PaneList[0].Rect.Width / 2 - 70)
                , (int)((zedGraphControl4.MasterPane.PaneList[0].Chart.Rect.Y + zedGraphControl4.MasterPane.PaneList[0].Rect.Y) / 2 - 20));
            checkBox4.Location = new Point((int)(zedGraphControl4.MasterPane.PaneList[0].Rect.X + zedGraphControl4.MasterPane.PaneList[0].Rect.Width / 2 + 40)
                , (int)((zedGraphControl4.MasterPane.PaneList[0].Chart.Rect.Y + zedGraphControl4.MasterPane.PaneList[0].Rect.Y) / 2 - 20));
            if (zedGraphControl4.MasterPane.PaneList.Count == 2)
            {

                zedGraphControl4.MasterPane.PaneList[1].Rect = new RectangleF((int)(0.01764 * Width), (int)(0.43835 * Height), (int)(0.91764 * Width), (int)(0.20547 * Height));//设置分图的大小和位置
                zedGraphControl4.MasterPane.PaneList[1].Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.45205 * Height), (int)(0.74117 * Width), (int)(0.27397 * Height) - 100 - (int)(0.01369 * Height));
            }
            else
            {
                zedGraphControl4.MasterPane.PaneList.Add(new GraphPane());
                zedGraphControl4.MasterPane.PaneList[1].Rect = new RectangleF((int)(0.01764 * Width), (int)(0.43835 * Height), (int)(0.91764 * Width), (int)(0.20547 * Height));//设置分图的大小和位置
                zedGraphControl4.MasterPane.PaneList[1].Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.45205 * Height), (int)(0.74117 * Width), (int)(0.27397 * Height) - 100 - (int)(0.01369 * Height));
            }

        }










        private void panel3_SizeChanged_1(object sender, EventArgs e)
        {

            int Width = panel3.Width;
            int Height = panel3.Height;
            zedGraphControl2.Size = new Size((int)(0.94117 * Width), (int)(0.65753 * Height));
            zedGraphControl2.MasterPane.PaneList[0].Rect = new RectangleF((int)(0.01764 * Width), (int)(0.04109 * Height), (int)(0.91764 * Width), (int)(0.38356 * Height));//设置分图的大小和位置900, 300
            zedGraphControl2.MasterPane.PaneList[0].Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.04109 * Height) + 40, (int)(0.74117 * Width), (int)(0.27397 * Height));

            checkBox1.Location = new Point((int)(zedGraphControl2.MasterPane.PaneList[0].Rect.X + zedGraphControl2.MasterPane.PaneList[0].Rect.Width / 2 - 70)
               , (int)((zedGraphControl2.MasterPane.PaneList[0].Chart.Rect.Y + zedGraphControl2.MasterPane.PaneList[0].Rect.Y) / 2 - 20));
            checkBox2.Location = new Point((int)(zedGraphControl2.MasterPane.PaneList[0].Rect.X + zedGraphControl2.MasterPane.PaneList[0].Rect.Width / 2 + 40)
                , (int)((zedGraphControl2.MasterPane.PaneList[0].Chart.Rect.Y + zedGraphControl2.MasterPane.PaneList[0].Rect.Y) / 2 - 20));

            if (zedGraphControl2.MasterPane.PaneList.Count == 2)
            {

                zedGraphControl2.MasterPane.PaneList[1].Rect = new RectangleF((int)(0.01764 * Width), (int)(0.43835 * Height), (int)(0.91764 * Width), (int)(0.20547 * Height));//设置分图的大小和位置
                zedGraphControl2.MasterPane.PaneList[1].Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.45205 * Height), (int)(0.74117 * Width), (int)(0.27397 * Height) - 100 - (int)(0.01369 * Height));
            }
            else
            {
                zedGraphControl2.MasterPane.PaneList.Add(new GraphPane());
                zedGraphControl2.MasterPane.PaneList[1].Rect = new RectangleF((int)(0.01764 * Width), (int)(0.43835 * Height), (int)(0.91764 * Width), (int)(0.20547 * Height));//设置分图的大小和位置
                zedGraphControl2.MasterPane.PaneList[1].Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.45205 * Height), (int)(0.74117 * Width), (int)(0.27397 * Height) - 100 - (int)(0.01369 * Height));
            }





        }

        private void panel4_SizeChanged(object sender, EventArgs e)
        {
            int Width = panel4.Width;
            int Height = panel4.Height;
            zedGraphControl1.Location = new Point((int)(0.008116 * Width), 0);
            zedGraphControl1.Size = new Size((int)(0.39772 * Width), (int)(0.84782 * Height));
            zedGraphControl1.MasterPane.PaneList[0].Rect = new RectangleF((int)(0.01217 * Width), (int)(0.01672 * Height), (int)(0.34090 * Width), (int)(0.41806 * Height));//设置分图的大小和位置900, 300
            zedGraphControl1.MasterPane.PaneList[0].Chart.Rect = new RectangleF((int)(0.01217 * Width) + 60, (int)(0.10033 * Height), (int)(0.25974 * Width), (int)(0.41806 * Height) - 45 - (int)(0.10033 * Height));
            if (zedGraphControl1.MasterPane.PaneList.Count == 2)
            {
                zedGraphControl1.MasterPane.PaneList[1].Rect = new RectangleF((int)(0.01217 * Width), (int)(0.41806 * Height), (int)(0.34090 * Width), (int)(0.41806 * Height));//设置分图的大小和位置
                zedGraphControl1.MasterPane.PaneList[1].Chart.Rect = new RectangleF((int)(0.01217 * Width) + 60, (int)(0.43478 * Height), (int)(0.25974 * Width), (int)(0.23411 * Height));

            }
            else
            {
                zedGraphControl1.MasterPane.PaneList.Add(new GraphPane());
                zedGraphControl1.MasterPane.PaneList[1].Rect = new RectangleF((int)(0.01217 * Width), (int)(0.41806 * Height), (int)(0.34090 * Width), (int)(0.41806 * Height));//设置分图的大小和位置
                zedGraphControl1.MasterPane.PaneList[1].Chart.Rect = new RectangleF((int)(0.01217 * Width) + 60, (int)(0.43478 * Height), (int)(0.25974 * Width), (int)(0.23411 * Height));
            }

            zedGraphControl3.Location = new Point((int)(0.40584 * Width), 0);
            zedGraphControl3.Size = new Size((int)(0.47889 * Width), (int)(0.84782 * Height));
            zedGraphControl3.MasterPane.PaneList[0].Rect = new RectangleF((int)(0.01217 * Width), (int)(0.01672 * Height), (int)(0.43019 * Width), (int)(0.80267 * Height));//设置分图的大小和位置900, 300
            zedGraphControl3.MasterPane.PaneList[0].Chart.Rect = new RectangleF((int)(0.01217 * Width) + 65, (int)(0.01672 * Height) + 50, (int)(0.35714 * Width), (int)(0.80267 * Height) - 60 - 50);
        }

        private void ExittoolStripButton8_Click(object sender, EventArgs e)
        {
            ExitToolStripMenuItem.PerformClick();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Observes == null)
            {
                MessageBox.Show("没有导入数据！");
                return;
            }

            if (!bFail)
            {
                MessageBox.Show("请先单击AR建模进行计算!");
                return;
            }

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            tabControl1.SelectedTab = tabPage2;
            int i_SeletctIndex = comboBox1.SelectedIndex;
            Graph.GraphPreY(panel3.Width, panel3.Height, zedGraphControl2, m_AR_Pre[i_SeletctIndex], m_AR_PreEoror[i_SeletctIndex], m_AR_PreDataSource[i_SeletctIndex], m_DeformationModelDays, m_SheetNameList[0], dataGridView2, i_SeletctIndex + 1);

        }



        private void FitGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (m_Observes == null)
            {
                MessageBox.Show("没有导入数据！");
                return;
            }
            if (!bFail)
            {
                MessageBox.Show("请先单击AR建模进行计算!");
                return;
            }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            tabControl1.SelectedTab = tabPage3;

            Graph.GraphFitY(panel2.Width, panel2.Height, zedGraphControl4, m_SourFitY, m_FitY, m_Order, m_SheetNameList[0], dataGridView1);
        }



        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage3)
            {
                FitGraphToolStripMenuItem.PerformClick();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {

                PreToolStripMenuItem.PerformClick();
                if (comboBox1.Items.Count != 0)
                    comboBox1.SelectedIndex = 0;
            }


        }










        private void InsertPicture(IWorkbook workbook, ISheet sheet1, byte[] path, int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2, string FilePath, bool Fag = false)
        {
            int pictureIdx = workbook.AddPicture(path, PictureType.PNG);
            IDrawing patriarch = sheet1.CreateDrawingPatriarch();
            IClientAnchor anchor = null;

            if (Path.GetExtension(FilePath) == ".xls")
            {
                anchor = new HSSFClientAnchor(dx1, dy1, dx2, dy2, col1, row1, col2, row2);//office2007版本及以前版本
            }
            else
            {
                anchor = new XSSFClientAnchor(dx1, dy1, dx2, dy2, col1, row1, col2, row2);//office2010版本及以后版本
            }
            IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
            if (Fag == true)
                pict.Resize();
        }
    }
}

