using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using MyMatrix;//引用自定义的矩阵计算类。

namespace MiniClosedLoopSearch
{
    public partial class MainFram : Form
    {
        public MainFram()
        {
            InitializeComponent();
        }
        DataTable dtPoint;
        DataTable dtLine;
        string result = "";//生成结果报告

        /// <summary>
        /// 环闭合差和附和路线闭合差计算方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 闭合差计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CLevelingAdjust.levelingPoints.Count == 0 || CLevelingAdjust.levelingLines.Count == 0)
            {
                MessageBox.Show("请先导入或输入数据再进行闭合差计算！");
                return;
            }
            else
            {
                CLevelingAdjust.m_Pnumber = CLevelingAdjust.levelingPoints.Count;
                CLevelingAdjust.m_Lnumber = CLevelingAdjust.levelingLines.Count;
                //统计该网中已知点的数量。
                for (int i = 0; i < CLevelingAdjust.levelingPoints.Count; i++)
                {
                    if (CLevelingAdjust.levelingPoints[i].PointNature == PointNature.known)
                    {
                        CLevelingAdjust.m_kownPnumber++;
                    }
                }
                CLevelingAdjust.strLoopClosure.Clear();//调用环闭合差计算方法和附和路线闭合差计算方法之前应清空该值。
                CLevelingAdjust.LoopClosure();//调用环闭合差计算方法。
                CLevelingAdjust.LineClosure();//调用附和路线闭合差计算方法。
            }

            string[] jieguo = CLevelingAdjust.strLoopClosure.ToArray();
            result = string.Join("", jieguo);
            // textBox1.Text = result;
            rtxtReport.Text = result;
            近似高程计算ToolStripMenuItem.Enabled = true;
            tabControl1.SelectedIndex = 2;
        }

        /// <summary>
        /// 思路：1、在导入水准路线之前，必须先导入控制点数据。
        /// 2、每行代表一条水准路线，在导入水准路线详细信息时，首先判断其起点和终点是否是控制点中的点，否则不给导入。
        /// 3、判断起点和终点是否是控制点中的点的思路是：取出起点和终点依次和控制点中的点的名称对比，能找到名称一样的即可导入其信息，否则不给导入。
        /// 4、在能导入起点和终点信息的前提下，在导入该路线的其他如路线长度和高差等信息。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    
        private void 导入测站数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 导入水准点信息
            //导入点数据到 CLevelingAdjust.levelingPoints点集合。
          
            OpenFileDialog fl = new OpenFileDialog();
            fl.Filter = "文本文件|*.txt";
            if (fl.ShowDialog() == DialogResult.OK)//判断打开文件对话框后是否选择了文件。
            {
                string pathFile = fl.FileName;//打开的文件路径。

                StreamReader rs = new StreamReader(pathFile);
                string row = rs.ReadLine();//读取一行数据。
                int i = 0;//给水准点编号。
                int j = 0;//给水准的未知点编号，其中-1代表已知点，0，1，2...代表未知点。
                if (CLevelingAdjust.levelingPoints.Count != 0)//如果已经导入数据了，就直接返回。
                {
                    return;
                }
                while (row != null)
                {
                    string[] levelPointArray = row.Split(',');//提取所有的信息。

                    LevelingPoint levePoint = new LevelingPoint();//实例化水准点。

                    levePoint.LevelingPointNum = i;//给水准点编号，从0开始编号，为了后面的闭合环计算。
                    i++;
                    levePoint.StrLevelingPointName = levelPointArray[0];//给实例化的水准点赋名称
                    levePoint.LevelingHeight = Convert.ToDouble(levelPointArray[2]);//给实例化的水准点赋高程。
                    if (levelPointArray[1] == "00")//判断水准点的性质，若是00代表未知点。
                    {
                        levePoint.PointNature = PointNature.unknow;
                        levePoint.UnknowPointNum = j;//未知点的未知点编号属性都等于0，1，2...。
                        j++;
                    }
                    if (levelPointArray[1] == "01")//判断水准点的性质，若是01代表已知点。
                    {
                        levePoint.PointNature = PointNature.known;
                        levePoint.UnknowPointNum = -1;//已知点的未知点编号属性都等于-1。
                    }
                    CLevelingAdjust.levelingPoints.Add(levePoint);
                    row = rs.ReadLine();
                }
                rs.Close();
            }
            else
                return;
            dtPoint = InitTable();//初始化表格。

            //给表格添加数据
            for (int k = 0; k < CLevelingAdjust.levelingPoints.Count; k++)
            {
                DataRow row2 = dtPoint.NewRow();
                row2["点名"] = CLevelingAdjust.levelingPoints[k].StrLevelingPointName;

                if (CLevelingAdjust.levelingPoints[k].PointNature == PointNature.known)
                {
                    row2["属性"] = "01";
                }
                if (CLevelingAdjust.levelingPoints[k].PointNature == PointNature.unknow)
                {
                    row2["属性"] = "00";
                }
                row2["高程(m)"] = CLevelingAdjust.levelingPoints[k].LevelingHeight;
                dtPoint.Rows.Add(row2);
            }
            dgvCtrolPointsInfo.DataSource = dtPoint;
            tabControl1.SelectedIndex = 0;

           
            #endregion
        }

        private DataTable InitTable()
        {
            //2.初始化表格
            dtPoint = new DataTable();
            dtPoint.Columns.Add("点名");
            dtPoint.Columns.Add("属性");
            dtPoint.Columns.Add("高程(m)");
            return dtPoint;
        }


        private void 导入水准路线数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 导入水准路线信息
            //1.导入数据到CLevelingAdjust.levelingLines边集合中去。
            if (CLevelingAdjust.levelingPoints.Count == 0)
            {
                MessageBox.Show("请先导入控制点数据，再导入水准路线信息！");
                return;
            }
            else
            {
                OpenFileDialog fl = new OpenFileDialog();
                fl.Filter = "文本文件|*.txt";

                if (fl.ShowDialog() == DialogResult.OK)//判断打开文件对话框后是否选择了文件。
                {
                    string pathFile = fl.FileName;//打开的文件路径。

                    StreamReader rs = new StreamReader(pathFile);
                    string row = rs.ReadLine();//读取一行数据。
                    int i = 0;
                    if (CLevelingAdjust.levelingLines.Count != 0)//如果已经导入数据了，就直接返回。
                    {
                        return;
                    }
                    while (row != null)
                    {
                        string[] levelLineArray = row.Split(',');//提取所有的信息。

                        LevelingLine levelLine = new LevelingLine();//实例化测段。

                        levelLine.LeveingLineNum = i;//给水准路线编号，目的是为了后面的闭合环计算。
                        i++;
                        levelLine.StarPoint = new LevelingPoint();//实例化测段的起点。
                        levelLine.EndPoint = new LevelingPoint();//实例化测段的终点。
                        for (int j = 0; j < CLevelingAdjust.levelingPoints.Count; j++)
                        {
                            if (levelLineArray[1] == CLevelingAdjust.levelingPoints[j].StrLevelingPointName)
                            {
                                levelLine.StarPoint = CLevelingAdjust.levelingPoints[j];//这点尤为重要，否则会产生同样的点名重复存储的情况。                              
                            }
                        }
                        for (int k = 0; k < CLevelingAdjust.levelingPoints.Count; k++)
                        {
                            if (levelLineArray[2] == CLevelingAdjust.levelingPoints[k].StrLevelingPointName)
                            {
                                levelLine.EndPoint = CLevelingAdjust.levelingPoints[k];//这点尤为重要，否则会产生同样的点名重复存储的情况。                              
                            }
                        }
                        if (levelLine.StarPoint.StrLevelingPointName == null)
                        {
                            MessageBox.Show("测站点不是控制点！");
                            return;
                        }
                        if (levelLine.EndPoint.StrLevelingPointName == null)
                        {
                            MessageBox.Show("照准点不是控制点！");
                            return;
                        }
                        levelLine.LeveingHeightDifferent = Convert.ToDouble(levelLineArray[4]);//给实例化的测段赋高差
                        levelLine.LeveingRoadLength = Convert.ToDouble(levelLineArray[3]);//给实例化的测段的水准路线长度。
                        CLevelingAdjust.levelingLines.Add(levelLine);//添加该测段到CLevelingAdjust.levelingLines的泛型集合。
                        row = rs.ReadLine();
                    }
                    rs.Close();
                }
            }

            //2.初始化表格
            dtLine = new DataTable();
            dtLine.Columns.Add("起始点");
            dtLine.Columns.Add("终点");
            dtLine.Columns.Add("路线长度（m)");
            dtLine.Columns.Add("高差（m)");
            dgvStationInfo.DataSource = dtLine;
            //3.给该表格添加数据
            for (int m = 0; m < CLevelingAdjust.levelingLines.Count; m++)
            {
                DataRow row = dtLine.NewRow();
                row["起始点"] = CLevelingAdjust.levelingLines[m].StarPoint.StrLevelingPointName;
                row["终点"] = CLevelingAdjust.levelingLines[m].EndPoint.StrLevelingPointName;
                row["路线长度（m)"] = CLevelingAdjust.levelingLines[m].LeveingRoadLength;
                row["高差（m)"] = CLevelingAdjust.levelingLines[m].LeveingHeightDifferent;
                dtLine.Rows.Add(row);
            }
            tabControl1.SelectedIndex = 1;
            #endregion
        }
        private void MainFram_Load(object sender, EventArgs e)
        {
            近似高程计算ToolStripMenuItem.Enabled = false;

        }

        private void 近似高程计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CLevelingAdjust.levelingPoints.Count == 0 || CLevelingAdjust.levelingLines.Count == 0)
            {
                MessageBox.Show("请先导入或输入数据再进行近似高程计算！");
                return;
            }
            CLevelingAdjust.Cal_H0();//调用高程近似值计算方法。
            //初始化表格
            dtPoint = new DataTable();
            dtPoint.Columns.Add("点名");
            dtPoint.Columns.Add("属性");
            dtPoint.Columns.Add("高程(m)");
            dgvCtrolPointsInfo.DataSource = dtPoint;
            //.更新表格数据
            for (int i = 0; i < CLevelingAdjust.levelingPoints.Count; i++)
            {
                DataRow row = dtPoint.NewRow();
                row["点名"] = CLevelingAdjust.levelingPoints[i].StrLevelingPointName;

                if (CLevelingAdjust.levelingPoints[i].PointNature == PointNature.known)
                {
                    row["属性"] = "01";
                }
                if (CLevelingAdjust.levelingPoints[i].PointNature == PointNature.unknow)
                {
                    row["属性"] = "00";
                }
                row["高程(m)"] = Math.Round(CLevelingAdjust.levelingPoints[i].LevelingHeight, 3);
                dtPoint.Rows.Add(row);
            }
            tabControl1.SelectedIndex = 0;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            闭合差计算ToolStripMenuItem_Click(sender, e);
             
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            近似高程计算ToolStripMenuItem_Click(sender, e);

        }

        private void 输出报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtxtReport.Text == "")
            {
                MessageBox.Show("请先生成报告，在输出！！！");
            }
            else
            {
                SaveFileDialog sfg = new SaveFileDialog();
                sfg.Filter = "*.txt|文本文件|*.*|所有文件";

                if (DialogResult.OK == sfg.ShowDialog())
                {
                    string filePath = sfg.FileName;
                    using (StreamWriter ws = new StreamWriter(filePath, false, Encoding.Default))
                    {
                        ws.Write(result);
                    }
                    MessageBox.Show("文件导出成功！");
                }

            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("《测绘程序设计试题集（试题16 水准网最小闭合环搜索算法）》配套程序\n作者：钱如友\n武汉大学测绘学院\r\nEMAIL: 562244421@qq.com\r\n2018.12.10");
        }

    }
}
