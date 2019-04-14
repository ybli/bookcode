using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FileHelper;
using PointPattern;
using System.Diagnostics;
using ParallelCalculate;
using BaiDuMap;
using System.Security.Permissions;
using POI.Inquiry;

namespace CrimeDataAnalysis
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 w = new CrimeDataAnalysis.AboutBox1();
            w.ShowDialog();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #region 定义全局变量
        //定义全局变量
        public DataCenter m_dataCenter = new DataCenter();//原始数据生成的dataCenter
        public DataCenter m_displayDataCenter = new DataCenter();//根据筛选后的数据表构造dataCenter
        double m_D;//组距
        int m_t;//数据点的个数
        MysqlLoginForm m_mysqlForm;//数据库的登录窗口
        bool m_isMysql = false;//指示是否是用数据库导入的
        bool m_ifCooorTranFlag = false;//是否进行坐标转换
        bool m_ifCalculate = false;
        //作分析的三个类型，对应横坐标
        List<string> m_incident_type_primaryList = new List<string>();
        List<string> m_hour_of_dayList = new List<string>();
        List<string> m_day_of_weekList = new List<string>();
        //三个类型的画图数据，对应纵坐标
        List<int> m_incident_type_primary_numList = new List<int>();
        List<int> m_hour_of_day_numList = new List<int>();
        List<int> m_day_of_week_numList = new List<int>();
        //画G、F、K函数图时的数据源
        List<double> dList = new List<double>();//横坐标d
        List<double> GList = new List<double>();//G函数值的列表
        List<double> FList = new List<double>();//F函数值的列表
        List<double> KList = new List<double>();//K函数值的列表

        #endregion


        #region 三个点模式分析的函数图像绘制
        private void k函数图KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawChart.ClearDataPoints_PointPatternAnalysis(ref chart1, this);

            chart1.ChartAreas[0].AxisX.Title = "d(m)";
            chart1.ChartAreas[0].AxisY.Title = "K(d)";
            chart1.Series[0].Points.DataBindXY(dList, KList);
        }
        private void g函数图GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawChart.ClearDataPoints_PointPatternAnalysis(ref chart1, this);

            chart1.ChartAreas[0].AxisX.Title = "d(m)";
            chart1.ChartAreas[0].AxisY.Title = "G(d)";
            chart1.Series[1].Points.DataBindXY(dList, GList);
        }

        private void f函数图FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawChart.ClearDataPoints_PointPatternAnalysis(ref chart1, this);

            chart1.ChartAreas[0].AxisX.Title = "d(m)";
            chart1.ChartAreas[0].AxisY.Title = "F(d)";
            chart1.Series[2].Points.DataBindXY(dList, FList);
        }
        private void 全部显示AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawChart.ClearDataPoints_PointPatternAnalysis(ref chart1, this);

            chart1.ChartAreas[0].AxisX.Title = "d(m)";
            chart1.ChartAreas[0].AxisY.Title = "X(d)";
            chart1.Series[0].Points.DataBindXY(dList, KList);
            chart1.Series[1].Points.DataBindXY(dList, GList);
            chart1.Series[2].Points.DataBindXY(dList, FList);
        }
        #endregion


        #region 文件菜单
        private void 导入数据IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "导入数据";
            openFileDialog1.FileName = "Data.txt";
            openFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_dataCenter = FileIO.ReadFile(openFileDialog1.FileName);
                    m_dataCenter = new DataCenter(m_dataCenter.crimeDataPointList,
                        m_dataCenter.pointInfoList);//构造出dataCenter的其他成员
                    dataGridView1.DataSource = Report.InitTable(m_dataCenter.crimeDataPointList);

                    m_isMysql = false;
                    m_ifCooorTranFlag = false;
                    m_ifCalculate = false;
                    toolStripStatusLabel1.Text = "数据源：" + openFileDialog1.FileName;
                    GenerateComboboxItem();
                }
                catch (Exception)
                {
                    MessageBox.Show("导入数据失败！");
                }
            }
        }

        private void 连接数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_mysqlForm = new MysqlLoginForm();
            m_mysqlForm.ShowDialog();
            dataGridView1.DataSource = m_mysqlForm.m_dataSource;
            m_isMysql = true;
            m_ifCooorTranFlag = false;
            m_ifCalculate = false;
            toolStripStatusLabel1.Text = "数据源：主机名或IP地址:" + m_mysqlForm.serverName + "； "
                + "database:" + m_mysqlForm.databaseName + "； "
                + "datatable:" + m_mysqlForm.datatableName;
            m_dataCenter = DataCenter.GetDisplayDataCenter(m_mysqlForm.m_dataSource);
            GenerateComboboxItem();
        }

        private void 导出数据OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "导出数据";
            saveFileDialog1.FileName = "Data";
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt|表格文件(*.xlsx)|*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        FileIO.SaveTxt(saveFileDialog1.FileName,(DataTable)dataGridView1.DataSource);
                        MessageBox.Show("导出txt文件成功！");
                    }
                    else
                    {
                        FileIO.SaveXlsx(saveFileDialog1.FileName, (DataTable)dataGridView1.DataSource);
                        MessageBox.Show("导出Excel文件成功！");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("导出数据失败！");
                }
            }
        }

        
        private void 导出图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "保存图形";
            saveFileDialog1.Filter = "(Jpeg文件)|*.jpg";
            saveFileDialog1.FileName = "图形.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Jpeg);
            }
        }

        private void 导出报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "导出报告";
            saveFileDialog1.FileName = "Report.txt";
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    sw.Write(richTextBox1.Text);
                    sw.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("导出报告失败！");
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion


        #region 两个按钮的功能
        private void button1_Click(object sender, EventArgs e)
        {
            //坐标转换按钮
            m_displayDataCenter = DataCenter.GetDisplayDataCenter((DataTable)dataGridView1.DataSource);
            dataGridView1.DataSource = Report.InitTable(m_displayDataCenter.crimeDataPointList,
                m_displayDataCenter.pointInfoList);
            m_ifCooorTranFlag = true;
            m_ifCalculate = false;
            //设置组距，便于画图
            //int groupD = (int)(1 + 3.322 * Math.Log10(m_displayDataCenter.pointInfoList.Count * 1.0));
            //List<double> dminList = new List<double>(m_displayDataCenter.dminArray);
            //m_D = ((int)(dminList.Max() / (double)groupD * 100)) / 100.0;
            //m_t = (int)(1 / m_D) + 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //刷新数据按钮
            if (!m_isMysql)
            {
                try
                {
                    m_dataCenter = FileIO.ReadFile(openFileDialog1.FileName);
                    m_dataCenter = new DataCenter(m_dataCenter.crimeDataPointList,
                        m_dataCenter.pointInfoList);//构造出dataCenter类
                    dataGridView1.DataSource = Report.InitTable(m_dataCenter.crimeDataPointList);

                    ////设置组距，便于画图
                    //int groupD = (int)(1 + 3.322 * Math.Log10(m_dataCenter.dminArray.Length * 1.0));
                    //List<double> dminList = new List<double>(m_dataCenter.dminArray);
                    //m_D = ((int)(dminList.Max() / (double)groupD * 100)) / 100.0;
                    //m_t = (int)(1 / m_D) + 1;
                    m_isMysql = false;
                    toolStripStatusLabel1.Text = "数据源：" + openFileDialog1.FileName;
                    GenerateComboboxItem();
                }
                catch (Exception)
                {
                    MessageBox.Show("重新导入数据失败！");
                }
            }
            else
            {
                dataGridView1.DataSource = MysqlDataIO.ReadMysqlData(m_mysqlForm.serverName, m_mysqlForm.uid,
                    m_mysqlForm.pwd, m_mysqlForm.databaseName, m_mysqlForm.datatableName);
                m_isMysql = true;
            }
            GenerateComboboxItem();
            m_ifCooorTranFlag = false;
            m_ifCalculate = false;
        }
        #endregion


        #region 统计分析的图形绘制
        private void incidenttypeprimaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawChart.ClearDataPoints_StatisticalAnalysis(ref chart1, this);
            chart1.Titles[0].Text = "各种犯罪类型的犯罪数量统计";

            //画犯罪类型分析图表
            chart1.Series[0].Enabled = true;
            chart1.Series[1].Enabled = false;
            chart1.Series[2].Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "incident_type_primary";
            chart1.ChartAreas[0].AxisY.Title = "record_number";

            m_incident_type_primaryList.Sort();
            chart1.Series[0].Points.DataBindXY(m_incident_type_primaryList, 
                m_incident_type_primary_numList);
            chart1.Series[0].Label = "#VAL";                //设置显示X Y的值
            chart1.Series[0].ToolTip = "type:#VALX\r\nnum:#VAL";     //鼠标移动到对应点显示数值
        }

        private void hourofdayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawChart.ClearDataPoints_StatisticalAnalysis(ref chart1, this);
            chart1.Titles[0].Text = "一天内不同时间犯罪数量统计";

            chart1.Series[0].Enabled = false;
            chart1.Series[2].Enabled = false;
            chart1.Series[1].Enabled = true;
            chart1.ChartAreas[0].AxisX.Title = "hour_of_day";
            chart1.ChartAreas[0].AxisY.Title = "record_number";

            m_hour_of_dayList.Clear();
            for (int i = 1; i < comboBox2.Items.Count; i++)
            {
                m_hour_of_dayList.Add(comboBox2.Items[i].ToString());
            }
            chart1.Series[1].Points.DataBindXY(m_hour_of_dayList,
                m_hour_of_day_numList);
            chart1.Series[1].Label = "#VAL";                //设置显示X Y的值
            chart1.Series[1].ToolTip = "hour:#VALX\r\nnum:#VAL";     //鼠标移动到对应点显示数值
        }

        private void dayofweekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawChart.ClearDataPoints_StatisticalAnalysis(ref chart1, this);
            chart1.Titles[0].Text = "一周内不同日期犯罪数量统计";

            chart1.Series[0].Enabled = false;
            chart1.Series[1].Enabled = false;
            chart1.Series[2].Enabled = true;
            chart1.ChartAreas[0].AxisX.Title = "day_of_week";
            chart1.ChartAreas[0].AxisY.Title = "record_number";

            m_day_of_weekList.Clear();
            for (int i = 1; i < comboBox3.Items.Count; i++)
            {
                m_day_of_weekList.Add(comboBox3.Items[i].ToString());
            }
            chart1.Series[2].Points.DataBindXY(m_day_of_weekList,
                m_day_of_week_numList);
            chart1.Series[2].Label = "#VAL";                //设置显示X Y的值
            chart1.Series[2].ToolTip = "day:#VALX\r\nnum:#VAL";     //鼠标移动到对应点显示数值
        }

        string filepath1 = "犯罪数据.txt";
        string filepath2 = "兴趣点信息.txt";
        private void 导入POI数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdBD = new FolderBrowserDialog();
            fdBD.Description = "请选择POI数据所在的文件夹";
            if (fdBD.ShowDialog() == DialogResult.OK)
            {
                filepath1 = fdBD.SelectedPath + "\\" + filepath1;
                filepath2 = fdBD.SelectedPath + "\\" + filepath2;
                MessageBox.Show("导入POI数据成功！");
            }
        }

        private void 计算并统计POIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadFile T = new ReadFile();
            ReadFile.Crime[] crimedata = T.ReadCrimedata(filepath1);
            ReadFile.POI[] pointinfor = T.ReadPOI(filepath2);

            toolStripComboBox1.Items.Clear();
            foreach (var item in pointinfor)
            {
                if(!toolStripComboBox1.Items.Contains(item.type))
                    toolStripComboBox1.Items.Add(item.type);
            }

            Search N = new Search();
            double threshold = 49;//阈值取到50左右

            pointinfor = N.SearchQuiry(crimedata, pointinfor, threshold);
            pointinfor = N.Countpoicrime(pointinfor);
            Console.WriteLine("计算完成，正在写入文件...");

            //将结果写入文件并在控制台输出
            StreamWriter sw = new StreamWriter(@"统计结果.txt");
            sw.WriteLine("--------------------------------------------------");
            sw.WriteLine("--------------------统计结果----------------------");
            sw.WriteLine("--------------------------------------------------");

            for (int i = 0; i < pointinfor.Length; i++)
            {
                sw.WriteLine("{0},{1}", pointinfor[i].type, pointinfor[i].number);
                for (int j = 0; j < pointinfor[i].crimeinfor.Count; j++)
                {
                    //各项犯罪事件的数量
                    int crimeNumber = (pointinfor[i].crimeinfor[j]).number;
                    //该POI上犯罪总数
                    int POI_number = pointinfor[i].number;
                    //各种犯罪类型在该POI上的占比

                    sw.WriteLine("{0},{1},{2:f3}%",
                        (pointinfor[i].crimeinfor[j]).type,
                        crimeNumber,
                        (double)crimeNumber / POI_number * 100);
                }
                sw.WriteLine();
            }

            sw.Close();
            Console.WriteLine("已将结果保存至当前目录下！");
            pointinfor_2 = pointinfor;
            MessageBox.Show("计算完成！");
        }

        //计算结果的副本，用于绘制图形
        ReadFile.POI[] pointinfor_2;

        private void 绘图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //直接从文件读取各兴趣点信息
            List<ReadFile.POI> POIfromFile = new List<ReadFile.POI>();
            StreamReader sr = new StreamReader("统计结果.txt");
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();

            string lineStr;
            while ((lineStr=sr.ReadLine())!=null)
            {
                string[] strArr = lineStr.Split(',');
                ReadFile.POI _poi = new ReadFile.POI();
                _poi.type = strArr[0];
                _poi.number = int.Parse(strArr[1]);
                _poi.crimeinfor = new List<Search.CrimeCount>();

                while ((lineStr = sr.ReadLine()) != "")
                {
                    strArr = lineStr.Split(',');
                    Search.CrimeCount scc = new Search.CrimeCount();
                    scc.type = strArr[0];
                    scc.number = int.Parse(strArr[1]);
                    _poi.crimeinfor.Add(scc);
                }
                POIfromFile.Add(_poi);
            }
            pointinfor_2 = POIfromFile.ToArray();
            toolStripComboBox1.Items.Clear();
            foreach (var item in pointinfor_2)
            {
                //加入选项
                if (!toolStripComboBox1.Items.Contains(item.type))
                    toolStripComboBox1.Items.Add(item.type);
            }
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            //选择的兴趣点类型变化时，重新绘制图形
            List<string> typeList = new List<string>();
            List<int> numberList = new List<int>();
            List<double> ratioList = new List<double>();

            var curPOI = pointinfor_2[toolStripComboBox1.SelectedIndex];
            foreach (var item in curPOI.crimeinfor)
            {
                typeList.Add(item.type);
                numberList.Add(item.number);
                ratioList.Add(Math.Round(
                    (double)item.number / curPOI.number * 100, 3
                    )
                    );
            }

            DrawChart.DrawPOI(ref chart1,
                pointinfor_2[toolStripComboBox1.SelectedIndex].type);

            chart1.Series[0].Points.DataBindXY(typeList, numberList);
            chart1.Series[1].Points.DataBindXY(typeList, ratioList);
            //chart1.Series[0].Label = "#VAL";                //设置显示X Y的值
            chart1.Series[0].ToolTip = "type:#VALX\r\nnum:#VAL";     //鼠标移动到对应点显示数值
            chart1.Series[1].Label = "#VAL" ;                //设置显示X Y的值
            chart1.Series[1].ToolTip = "ratio:#VALX\r\nnum:#VAL";     //鼠标移动到对应点显示数值
        }
        #endregion


        #region 3个combobox的筛选功能
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData();
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData();
        }
        #endregion


        #region 查看菜单
        private void 地图分析PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void 统计分析报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = StatisticalReport(this);
            tabControl1.SelectedIndex = 3;
        }

        private void 点模式分析报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.countanalysis[] Gcutana, Fcutana, Kcutana;
            Report.CountAnalysis(m_displayDataCenter.pointInfoList.Count, dList, GList, FList, KList, out Gcutana, out Fcutana, out Kcutana);
            richTextBox1.Text = "     G函数                 F函数                 K函数\r\n";
            richTextBox1.Text = richTextBox1.Text + "距离 事件数 累积频率" + "  " + "距离 事件数 累积频率" + "  " + "距离 事件数 累积频率" + "\r\n";
            for (int i = 0; i < Gcutana.Length; i++)
            {
                string xy = Gcutana[i].distance.ToString("F2") + "  " + Gcutana[i].number.ToString("D3") + "   " + Gcutana[i].frequency.ToString("F5") + "   " + Fcutana[i].distance.ToString("F2") + "  " + Fcutana[i].number.ToString("D3") + "   " + Fcutana[i].frequency.ToString("F5") + "   " + Kcutana[i].distance.ToString("F2") + "  " + Kcutana[i].number.ToString("D3") + "   " + Kcutana[i].frequency.ToString("F5") + "\r\n";
                richTextBox1.Text += xy + "\r\n";
            }
            tabControl1.SelectedIndex = 3;
        }

        #endregion


        #region 计算菜单

        private void g函数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //计算G函数先要计算每个点的最邻近距离

            //注：此处计算都是根据表格内显示的数据灵活计算，若要计算原始数据，可以先清除筛选再计算
            m_displayDataCenter = DataCenter.GetDisplayDataCenter((DataTable)dataGridView1.DataSource);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (普通计算ToolStripMenuItem.Checked)
            {
                m_displayDataCenter.GetMinDisArr();
            }
            else
            {
                GFunc gFunc = new GFunc(m_displayDataCenter.pointInfoList);
                m_displayDataCenter.minDisArr = gFunc.GetMinDisArr(m_displayDataCenter.pointInfoList);
            }
            stopWatch.Stop();
            if (普通计算ToolStripMenuItem.Checked)
            {
                toolStripStatusLabel2.Text = "G函数，普通计算，耗时：" + stopWatch.ElapsedMilliseconds + "ms";
            }
            else
            {
                toolStripStatusLabel2.Text = "G函数，KD树+并行计算，耗时：" + stopWatch.ElapsedMilliseconds + "ms";
            }
            //设置组距，便于画图
            int groupD = (int)(1 + 3.322 * Math.Log10(m_displayDataCenter.pointInfoList.Count * 1.0));
            List<double> dminList = new List<double>(m_displayDataCenter.minDisArr);
            m_D = ((int)(dminList.Max() / (double)groupD * 100)) / 100.0;
            m_t = (int)(1 / m_D) + 1;

            //计算用于画图的数据点
            dList.Clear(); GList.Clear();
            for (int i = 0; i < m_t; i++)
            {
                dList.Add(m_D * i);
                double Gvalue = Functions.Gfunction(m_displayDataCenter.pointInfoList, m_D * i);
                GList.Add(Gvalue);
            }
        }

        private void f函数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //计算F函数先要计算每个点的到随机点集的最邻近距离

            m_displayDataCenter = DataCenter.GetDisplayDataCenter((DataTable)dataGridView1.DataSource);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (普通计算ToolStripMenuItem.Checked)
            {
                m_displayDataCenter.GetMinDisToRandPtArr();
            }
            else
            {
                FFunc fFunc = new FFunc(m_displayDataCenter.pointInfoList);
                m_displayDataCenter.minDisToRandPtArr = fFunc.GetMinDisArrToRandPt(m_displayDataCenter.randPtlist,
                    ref m_displayDataCenter.pointInfoList);
            }
            stopWatch.Stop();
            if (普通计算ToolStripMenuItem.Checked)
            {
                toolStripStatusLabel2.Text = "F函数，普通计算，耗时：" + stopWatch.ElapsedMilliseconds + "ms";
            }
            else
            {
                toolStripStatusLabel2.Text = "F函数，KD树+并行计算，耗时：" + stopWatch.ElapsedMilliseconds + "ms";
            }
            //计算用于画图的数据点
            FList.Clear();
            for (int i = 0; i < m_t; i++)
            {
                double Fvalue = Functions.Ffunction(m_displayDataCenter.pointInfoList, m_D * i);
                FList.Add(Fvalue);
            }
        }

        private void k函数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //计算K函数先要计算每个点的R范围内的点的个数

            m_displayDataCenter = DataCenter.GetDisplayDataCenter((DataTable)dataGridView1.DataSource);
            m_displayDataCenter.GetMinDisArr();

            //计算用于画图的数据点
            KList.Clear();
            for (int i = 0; i < m_t; i++)
            {
                //归一化一面积应该是1
                double Kvalue = Functions.Kfunction(m_displayDataCenter.pointInfoList, 1, m_D * i);
                KList.Add(Kvalue);
            }
        }

        private void 一键处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g函数ToolStripMenuItem_Click(sender, e);
            f函数ToolStripMenuItem_Click(sender, e);
            k函数ToolStripMenuItem_Click(sender, e);
            MessageBox.Show("一键处理完成！");
        }
        #endregion


        #region 其他方法与函数
        /// <summary>
        /// 根据原始导入的数据给第一个combobox添加项目
        /// </summary>
        private void GenerateComboboxItem()
        {
            //清空以前的数据
            this.m_incident_type_primaryList = new List<string>();
            this.comboBox1.Items.Clear();

            this.comboBox1.Items.Add("");

            int rowNum = this.dataGridView1.Rows.Count - 1;
            for (int i = 0; i < rowNum; i++)
            {
                string name1 = this.dataGridView1.Rows[i].Cells["incident_type_primary"].Value.ToString();
                if (!this.m_incident_type_primaryList.Contains(name1))
                {
                    this.m_incident_type_primaryList.Add(name1);
                    this.comboBox1.Items.Add(name1);
                }
            }

            this.label4.Text = "筛选结果：";
            m_displayDataCenter = m_dataCenter;
            ClassSum();//分类统计
        }

        /// <summary>
        /// 原始数据的分类统计
        /// </summary>
        private void ClassSum()
        {
            m_incident_type_primary_numList.Clear();
            m_hour_of_day_numList.Clear();
            m_day_of_week_numList.Clear();
            DataTable dtable = Report.InitTable(m_dataCenter.crimeDataPointList);
            for (int i = 1; i < comboBox1.Items.Count; i++)
            {
                string sqlcmd1 = "incident_type_primary='" + comboBox1.Items[i].ToString().Replace("'", "''") + "'";
                int n = (dtable.Select(sqlcmd1)).Count();
                m_incident_type_primary_numList.Add(n);
            }
            for (int i = 1; i < comboBox2.Items.Count; i++)
            {
                string sqlcmd2 = "hour_of_day='" + comboBox2.Items[i].ToString() + "'";
                int n = (dtable.Select(sqlcmd2)).Count();
                m_hour_of_day_numList.Add(n);
            }
            for (int i = 1; i < comboBox3.Items.Count; i++)
            {
                string sqlcmd3 = "day_of_week='" + comboBox3.Items[i].ToString() + "'";
                int n = (dtable.Select(sqlcmd3)).Count();
                m_day_of_week_numList.Add(n);
            }
            richTextBox1.Text = StatisticalReport(this);
        }

        /// <summary>
        /// 写统计报告
        /// </summary>
        public string StatisticalReport(MainForm mainForm)
        {
            List<int> _incident_type_primary_numList = mainForm.m_incident_type_primary_numList;
            List<int> _hour_of_day_numList = mainForm.m_hour_of_day_numList;
            List<int> _day_of_week_numList = mainForm.m_day_of_week_numList;

            string sReport = "";
            sReport += "统计报告\r\n";
            sReport += "------------------------------\r\n";
            sReport += "incident_type_primary".PadRightWhileDouble(50) + "recordNumber\r\n";
            for (int i = 0; i < _incident_type_primary_numList.Count; i++)
            {
                sReport += mainForm.comboBox1.Items[i + 1].ToString().PadRightWhileDouble(50) +
                    _incident_type_primary_numList[i] + "\r\n";
            }
            sReport += "------------------------------\r\n";
            sReport += "hour_of_day".PadRightWhileDouble(15) + "recordNumber\r\n";
            for (int i = 0; i < _hour_of_day_numList.Count; i++)
            {
                sReport += mainForm.comboBox2.Items[i + 1].ToString().PadRightWhileDouble(15) +
                    _hour_of_day_numList[i] + "\r\n";
            }
            sReport += "------------------------------\r\n";
            sReport += "day_of_week".PadRightWhileDouble(15) + "recordNumber\r\n";
            for (int i = 0; i < _day_of_week_numList.Count; i++)
            {
                sReport += mainForm.comboBox3.Items[i + 1].ToString().PadRightWhileDouble(15) +
                    _day_of_week_numList[i] + "\r\n";
            }
            return sReport;
        }

        /// <summary>
        /// 根据combobox筛选数据
        /// </summary>
        private void FilterData()
        {
            string sqlcmd;

            DataTable oldTable = Report.InitTable(m_dataCenter.crimeDataPointList);
            if (m_ifCooorTranFlag)
            {
                oldTable = Report.InitTable(m_dataCenter.crimeDataPointList, m_dataCenter.pointInfoList);
            }
            DataTable newTable = new DataTable();
            newTable = oldTable.Clone();//将之前的DataTable结构复制到新的DataTable!!!这步很重要
            string sqlcmd1;
            string sqlcmd2;
            string sqlcmd3;

            if (comboBox1.SelectedItem == null ||
                comboBox1.SelectedItem.ToString() == "")
            {
                sqlcmd1 = "incident_type_primary like " + "'%'";
            }
            else
            {
                sqlcmd1 = "incident_type_primary='" + comboBox1.SelectedItem.ToString().Replace("'", "''") + "'";
            }
            if (comboBox2.SelectedItem == null ||
                comboBox2.SelectedItem.ToString() == "")
            {
                sqlcmd2 = "hour_of_day like " + "'%'";
            }
            else
            {
                sqlcmd2 = "hour_of_day='" + comboBox2.SelectedItem.ToString() + "'";
            }
            if (comboBox3.SelectedItem == null ||
                comboBox3.SelectedItem.ToString() == "")
            {
                sqlcmd3 = "day_of_week like " + "'%'";

            }
            else
            {
                sqlcmd3 = "day_of_week='" + comboBox3.SelectedItem.ToString() + "'";
            }
            sqlcmd = sqlcmd1 + " and " + sqlcmd2 + " and " + sqlcmd3;

            DataRow[] selectRowArr = (oldTable).Select(sqlcmd);
            foreach (var rowItem in selectRowArr)
            {
                newTable.ImportRow(rowItem);
            }
            dataGridView1.DataSource = newTable;
            label4.Text = "筛选结果：\n    " + "共查询到" + newTable.Rows.Count + "条结果！";
            m_displayDataCenter = DataCenter.GetDisplayDataCenter((DataTable)dataGridView1.DataSource);
        }

        private void SetWebBrowser(string htmlPath)
        {
            tabControl1.SelectedIndex = 1;
            Mapini.WebBrowserVersionEmulation();
            try
            {
                webBrowser1.Url = new Uri(Path.Combine(Application.StartupPath, htmlPath));
                webBrowser1.ObjectForScripting = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region 地图分析功能
        private void 统计查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWebBrowser("htmlANDjs/统计查询.html");
        }

        private void 点聚合分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWebBrowser("htmlANDjs/4点聚合.html");
        }

        private void 时间分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWebBrowser("htmlANDjs/4闪烁点.html");

        }

        private void 热力图分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWebBrowser("htmlANDjs/4热力图.html");
        }
        #endregion


        #region C#窗体与JS代码交互
        public double GetLat(int index)
        {
            double lat = -1;
            lat = m_displayDataCenter.crimeDataPointList[index].latitude;
            return lat;
        }
        public double GetLng(int index)
        {
            double lat = -1;
            lat = m_displayDataCenter.crimeDataPointList[index].longitude;
            return lat;
        }
        public int GetTotalNum()
        {
            return m_displayDataCenter.crimeDataPointList.Count;
        }
        #endregion


        #region 是否使用并行
        private void 普通计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            普通计算ToolStripMenuItem.Checked = true;
            kD树并行计算ToolStripMenuItem.Checked = false;
        }

        private void kD树并行计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            普通计算ToolStripMenuItem.Checked = false;
            kD树并行计算ToolStripMenuItem.Checked = true;
        }
        #endregion

        #region 预测菜单
        private void 热力图分析ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetWebBrowser("htmlANDjs/加州范围热力图/CA08-14热力图.html");
        }

        private void lSTM预测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("test.exe");
        }
        #endregion
    }
}
