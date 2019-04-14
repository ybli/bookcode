using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace MeasuringPointsForDesignedPoints
{
    public partial class Form1 : Form
    {
        private List<InfoData> listinfodata;

        private InfoData infodata;

        private List<CzxData> listczxdata;

        private CzxData czxdata;

        private TextAnnotation txtAn;

        private SaveFileDialog savefile;

        private FileHelper fileper;

        private CalDev caldev;

        private List<CalDevData> listcaldevdata;

        private Mathematics_lb math_lb;

        private CharacteristicTrack_lb chatra_lb;

        private LateralDeviationSet_lb latdev_lb;

        private StringBuilder ContextReport;

        private TabControl tabcontrol;

        private ToolStripButton OpenFilesBut;

        private ToolStripButton SaveResBut;

        private ToolStripButton CalBut;

        private ToolStripButton DrawingBut;

        private ToolStripButton HelpBut;

        public Form1()
        {
            InitializeComponent();

            infodata = new InfoData();

            czxdata = new CzxData();

            listinfodata = new List<InfoData>();

            listczxdata = new List<CzxData>();

            fileper = new FileHelper();

            caldev = new CalDev();

            listcaldevdata = new List<CalDevData>();

            math_lb =  new Mathematics_lb();

            chatra_lb =  new CharacteristicTrack_lb();

            latdev_lb = new LateralDeviationSet_lb();

            ContextReport = new StringBuilder();

            tabcontrol = new TabControl();
            
            OpenFilesBut = new ToolStripButton();
            
            SaveResBut = new ToolStripButton();
            
            CalBut = new ToolStripButton();
            
            DrawingBut = new ToolStripButton();
            
            HelpBut = new ToolStripButton();
        }

        // 绘制曲线中桩交点设计位置及实测点
        private void DrawingFunF()
        {
            try
            {
                chart1.ChartAreas[0].AxisX.Title = "Y(m)";
                chart1.ChartAreas[0].AxisY.Title = "X(m)";

                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                chart1.Series[0].LegendText = "线路设计交点(JD)";

                this.chart1.Series[0].ChartType = SeriesChartType.Point;

                // 设置点状符号大小
                this.chart1.Series[0].MarkerColor = Color.Blue;
                this.chart1.Series[0].MarkerSize = 8;
                this.chart1.Series[0].MarkerStyle = MarkerStyle.Circle;

                for (int i = 0; i < listinfodata.Count; i++)
                {
                    this.chart1.Series[0].Points.AddXY(listinfodata[i].Y, listinfodata[i].X);
                }

                this.chart1.Series[0].ToolTip = "(#VAL, #VALX)"; // 纵坐标值#VAL，横坐标值#VALX

                // 对散点进行文字注释                
                for (int i = 0; i < listinfodata.Count; i++)
                {
                    txtAn = new TextAnnotation();

                    txtAn.AnchorDataPoint = chart1.Series[0].Points[i];
                    txtAn.Text = listinfodata[i].Name;
                    txtAn.ForeColor = System.Drawing.Color.Black;
                    txtAn.AnchorX = listinfodata[i].Y + 0; // 在相对的横轴上偏一点的位置
                    txtAn.Y = listinfodata[i].X - 50;
                    txtAn.AllowMoving = true; // 允许用户鼠标移动
                    this.chart1.Annotations.Add(txtAn);
                }

                // 线条名称
                chart1.Series[1].LegendText = "线路实测点";

                this.chart1.Series[1].ChartType = SeriesChartType.Point;
                for (int i = 0; i < listczxdata.Count; i++)
                {
                    this.chart1.Series[1].Points.AddXY(listczxdata[i].Y, listczxdata[i].X);
                }

                this.chart1.Series[1].ToolTip = "(#VAL, #VALX)"; // 纵坐标值#VAL，横坐标值#VALX

                // 计算主点信息
                double detax_jd21 = listinfodata[0].X - listinfodata[1].X;
                double detay_jd21 = listinfodata[0].Y - listinfodata[1].Y;
                double azimuth_jd21 = math_lb.Math_AZIMUTH(detax_jd21, detay_jd21); // 计算交点到前一个交点的方位角,JD(i)到JD(i-1)的方位角
                double azimuth_jd12 = math_lb.Math_AZIMUTH(-detax_jd21, -detay_jd21); // JD(i-1)到JD(i)的方位角
                double detax_jd23 = listinfodata[2].X - listinfodata[1].X;
                double detay_jd23 = listinfodata[2].Y - listinfodata[1].Y;
                double azimuth_jd23 = math_lb.Math_AZIMUTH(detax_jd23, detay_jd23); // 计算交点到后一个交点的方位角,JD(i)到JD(i+1)的方位角
                double azimuth_jd32 = math_lb.Math_AZIMUTH(-detax_jd23, -detay_jd23); // 计算JD(i+1)到JD(i)的方位角

                chatra_lb.l0 = listinfodata[1].l0; // 缓和曲线
                chatra_lb.Rad = listinfodata[1].Rad; // 半径
                chatra_lb.erfa = chatra_lb.ERFAFun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);
                chatra_lb.beta0 = chatra_lb.BRTA0Fun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);
                chatra_lb.m = chatra_lb.MFun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);
                chatra_lb.p = chatra_lb.PFun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);
                chatra_lb.T = chatra_lb.TFun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);
                chatra_lb.L = chatra_lb.LFun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);
                chatra_lb.E0 = chatra_lb.E0Fun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);
                chatra_lb.q = chatra_lb.QFun(listinfodata[1].erfa, chatra_lb.l0, chatra_lb.Rad);

                double azimuth_HY = math_lb.Math_ANGLEtoAZIMUTH(azimuth_jd12 + listinfodata[1].K * chatra_lb.beta0);
                double azimuth_YHJ = math_lb.Math_ANGLEtoAZIMUTH(azimuth_jd23 + listinfodata[1].K * (-1.0 * chatra_lb.beta0));
                double azimuth_YH = azimuth_YHJ;
                double jd_mileage = listinfodata[1].Mil + chatra_lb.T; // 交点的里程

                chatra_lb.ZH_XY = chatra_lb.ZH_XYFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);
                chatra_lb.HY_xy = chatra_lb.HY_xyFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);
                chatra_lb.HY_XY = chatra_lb.HY_XYFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);
                chatra_lb.QZ_xy = chatra_lb.QZ_xyFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);
                chatra_lb.QZ_XY = chatra_lb.QZ_XYFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);
                chatra_lb.YH_xy = chatra_lb.YH_xyFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);
                chatra_lb.YH_XY = chatra_lb.YH_XYFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);
                chatra_lb.HZ_XY = chatra_lb.HZ_XYFun(listinfodata[1].X, listinfodata[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, chatra_lb.l0, chatra_lb.Rad, listinfodata[1].K, listinfodata[1].erfa);

                // **线条名称
                chart1.Series[2].LegendText = "曲线主点";

                this.chart1.Series[2].ChartType = SeriesChartType.Point;

                // 设置点状符号大小
                this.chart1.Series[2].MarkerColor = Color.Green;
                this.chart1.Series[2].MarkerSize = 8;
                this.chart1.Series[2].MarkerStyle = MarkerStyle.Circle;

                this.chart1.Series[2].Points.AddXY(chatra_lb.ZH_XY[0, 1], chatra_lb.ZH_XY[0, 0]);
                this.chart1.Series[2].Points.AddXY(chatra_lb.HY_XY[0, 1], chatra_lb.HY_XY[0, 0]);
                this.chart1.Series[2].Points.AddXY(chatra_lb.QZ_XY[0, 1], chatra_lb.QZ_XY[0, 0]);
                this.chart1.Series[2].Points.AddXY(chatra_lb.YH_XY[0, 1], chatra_lb.YH_XY[0, 0]);
                this.chart1.Series[2].Points.AddXY(chatra_lb.HZ_XY[0, 1], chatra_lb.HZ_XY[0, 0]);

                this.chart1.Series[2].ToolTip = "(#VAL, #VALX)"; // 纵坐标值#VAL，横坐标值#VALX

                // 对散点进行文字注释
                string[] MaPoNa = new string[]{"ZH", "HY", "QZ", "YH", "HZ"};
                double[,] MaPoXY = new double[,] { { chatra_lb.ZH_XY[0, 0], chatra_lb.ZH_XY[0, 1] }, { chatra_lb.HY_XY[0, 0], chatra_lb.HY_XY[0, 1] }, { chatra_lb.QZ_XY[0, 0], chatra_lb.QZ_XY[0, 1] }, { chatra_lb.YH_XY[0, 0], chatra_lb.YH_XY[0, 1] }, { chatra_lb.HZ_XY[0, 0], chatra_lb.HZ_XY[0, 1] } };
                for (int i = 0; i < 5; i++)
                {
                    txtAn = new TextAnnotation();

                    txtAn.AnchorDataPoint = chart1.Series[2].Points[i];

                    txtAn.Text = MaPoNa[i];
                    txtAn.ForeColor = System.Drawing.Color.Black;
                    txtAn.AnchorX = MaPoXY[i, 1] + 5; // 在相对的横轴上偏一点的位置
                    txtAn.Y = MaPoXY[i, 0] - 50;
                    txtAn.AllowMoving = true;
                    this.chart1.Annotations.Add(txtAn); // 放到Chart1中
                }

                MessageBox.Show("数据散点图绘制完毕！", "信息提示");

            }
            catch (Exception)
            {
                MessageBox.Show("绘制数据散点图错误！");
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult infodlg; // 曲线信息
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Filter = "文本文件(*.txt)|*.txt|文本文件(*.dat)|*.dat";
            infodlg = openFileDialog1.ShowDialog();
            try
            {
                if (infodlg == System.Windows.Forms.DialogResult.OK && openFileDialog1.FileName.Length > 0)
                {
                    string infoPath = this.openFileDialog1.FileName; // 存储读取文件的路径
                    listinfodata = fileper.ReadInfoData(infoPath); // 获取曲线信息文件
                }
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("选择的曲线信息文件无效！", "错误", MessageBoxButtons.OK);
            }

            InitInfoTable(); // 设置读取曲线文件的表格中各栏信息

            InitInfoTable_AddData(); // 曲线信息文件表

            System.Windows.Forms.DialogResult czxdlg; // 线路中线
            openFileDialog2.CheckFileExists = true;
            openFileDialog2.Filter = "文本文件(*.txt)|*.txt|文本文件(*.dat)|*.dat";
            czxdlg = openFileDialog2.ShowDialog();
            try
            {
                if (czxdlg == System.Windows.Forms.DialogResult.OK && openFileDialog2.FileName.Length > 0)
                {
                    string czxPath = this.openFileDialog2.FileName;
                    listczxdata = fileper.ReadCzxData(czxPath);
                }
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("选择线路中线数据无效！", "错误", MessageBoxButtons.OK);
            }

            InitCzxTable(); // 设置读取曲线文件的表格中各栏信息

            InitCzxTable_AddData(); // 线路中线实测数据表
        }
        
        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((listinfodata.Count < 1) || (listczxdata.Count < 1))
            {
                MessageBox.Show("请先导入线路数据！", "信息提示");
            }
            else
            {
                if ((listczxdata.Count < 1) || (listinfodata.Count != 3))
                {
                    MessageBox.Show("数据不充足！", "信息提示");
                }
                else
                {
                    listcaldevdata = caldev.CalDevRes(listinfodata, listczxdata);
                    ContextReport = fileper.OutReport(listinfodata, listcaldevdata);
                    richTextBox1.Text = ContextReport.ToString();

                    MessageBox.Show("计算已完成，详情见报告页！", "提示", MessageBoxButtons.OK);
                }

            }

        }

        private void menuStrip1_Resize(object sender, EventArgs e)
        {
            this.TabPageRefresh();
        }

        private void TabPageRefresh() 
        {
            foreach (TabPage p in tabControl1.TabPages)
            {
                p.Refresh();
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("《测绘程序设计试题集(竞赛篇 试题8 任意线路实测点偏差及其设计位置的确定)》配套程序\n作者：李阳腾龙\n成都理工大学地球科学学院\r\nEMAIL: liyangtenglong17@cdut.edu.cn\r\n2018.11.15");
        }

        private void 绘图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawingFunF();
        }

        private void 导出Dxf文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listczxdata.Count >= 1)
            {
                savefile = new SaveFileDialog();
                savefile.Filter = "Dxf文件(*.dxf)|*.dxf|所有文件(*.*)|*.*";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    fileper.LiPointsDxf(savefile.FileName, listczxdata);
                    MessageBox.Show("Dxf文件保存成功!", "提示");
                    savefile.Dispose();
                }
            }
        }

        private void InitInfoTable()
        {
            // 将曲线文件数据显示在DataGridView控件
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = 8; // 8列：交点号，X，Y，里程，偏角(°.′″)，偏转(左偏-1;右偏+1)，圆曲线半径，缓和曲线长
            dataGridView1.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Times New Roman", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            dataGridView1.Columns[0].Name = "JD_Name";
            dataGridView1.Columns[0].HeaderText = "交点号JD";

            dataGridView1.Columns[1].Name = "JD_X";
            dataGridView1.Columns[1].HeaderText = "JD_X(m)";

            dataGridView1.Columns[2].Name = "JD_Y";
            dataGridView1.Columns[2].HeaderText = "JD_Y(m)";

            dataGridView1.Columns[3].Name = "ZH_mileage";
            dataGridView1.Columns[3].HeaderText = "ZH点里程";

            dataGridView1.Columns[4].Name = "erfa";
            dataGridView1.Columns[4].HeaderText = "偏角(°.′″)";

            dataGridView1.Columns[5].Name = "K";
            dataGridView1.Columns[5].HeaderText = "偏角(左偏-1;右偏+1)";

            dataGridView1.Columns[6].Name = "Rad";
            dataGridView1.Columns[6].HeaderText = "圆曲线半径(m)";

            dataGridView1.Columns[7].Name = "l0";
            dataGridView1.Columns[7].HeaderText = "缓和曲线长(m)";
        }

        private void InitInfoTable_AddData()
        {
            dataGridView1.Rows.Add(); // 增加行

            for (int i = 0; i < listinfodata.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(listinfodata[i].Name);
                dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(listinfodata[i].X);
                dataGridView1.Rows[i].Cells[2].Value = Convert.ToString(listinfodata[i].Y);
                dataGridView1.Rows[i].Cells[3].Value = Convert.ToString(listinfodata[i].Mil);
                dataGridView1.Rows[i].Cells[4].Value = Convert.ToString(listinfodata[i].erfa); // 偏角(°.′″)
                dataGridView1.Rows[i].Cells[5].Value = Convert.ToString(listinfodata[i].K); // 偏转(左偏-1;右偏+1)
                dataGridView1.Rows[i].Cells[6].Value = Convert.ToString(listinfodata[i].Rad);
                dataGridView1.Rows[i].Cells[7].Value = Convert.ToString(listinfodata[i].l0);

                dataGridView1.Rows.Add();
            }
        }

        private void InitCzxTable()
        {
            dataGridView2.Rows.Clear();

            dataGridView2.ColumnCount = 3; // 3列：中线坐标：点号，X，Y
            dataGridView2.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Times New Roman", 8, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            dataGridView2.Columns[0].Name = "ZxPoint_Name";
            dataGridView2.Columns[0].HeaderText = "中线点号"; // 显示文字

            dataGridView2.Columns[1].Name = "X";
            dataGridView2.Columns[1].HeaderText = "X(m)";

            dataGridView2.Columns[2].Name = "Y";
            dataGridView2.Columns[2].HeaderText = "Y(m)";
        }

        private void InitCzxTable_AddData()
        {
            dataGridView2.Rows.Add(); // 增加行

            for (int i = 0; i < listczxdata.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = Convert.ToString(listczxdata[i].Name);
                dataGridView2.Rows[i].Cells[1].Value = Convert.ToString(listczxdata[i].X);
                dataGridView2.Rows[i].Cells[2].Value = Convert.ToString(listczxdata[i].Y);

                dataGridView2.Rows.Add();
            }
        }

        private void 保存计算报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savefile = new SaveFileDialog();
            savefile.Filter = "txt文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                fileper.SaveResultReport(savefile.FileName, richTextBox1.Text);
                MessageBox.Show("保存成功!", "提示");
                savefile.Dispose();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.toolStripButton2.Click += new EventHandler(保存计算报告ToolStripMenuItem_Click);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.toolStripButton3.Click += new EventHandler(计算ToolStripMenuItem_Click);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.toolStripButton4.Click += new EventHandler(绘图ToolStripMenuItem_Click);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.toolStripButton5.Click += new EventHandler(帮助ToolStripMenuItem_Click);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.toolStripButton1)
            {
                打开ToolStripMenuItem_Click(sender, e);
            }
            if (e.ClickedItem == this.toolStripButton2)
            {
                保存计算报告ToolStripMenuItem_Click(sender, e);
            }
            if (e.ClickedItem == this.toolStripButton3)
            {
                计算ToolStripMenuItem_Click(sender, e);
            }
            if (e.ClickedItem == this.toolStripButton4)
            {
                绘图ToolStripMenuItem_Click(sender, e);
            }
            if (e.ClickedItem == this.toolStripButton5)
            {
                帮助ToolStripMenuItem_Click(sender, e);
            }
        }
    }

}
