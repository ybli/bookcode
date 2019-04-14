using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PPP
{
    public partial class Form1 : Form
    {
  
     public class fileindex
        {
         public   int index;
         public   string path;
        }
   
       public  List<fileindex> file = new List<fileindex>();
        string[] fname = new string[] { "观测值文件" ,"导航文件","精密星历文件sp3","精密钟差文件clk","天线文件",
                                        "地球自转文件erp","码偏差文件P1_C1","码偏差文件P1_P2"};
        int flag = 0;
        List<result> res = new List<result>();
        obs_t obss = new obs_t(); nav_t nav = new nav_t(); dcb_t dcb = new dcb_t();
        erp_t erp = new erp_t(); station sta = new station(); pcv_t pcv = new pcv_t();
        sp3_t sp3 = new sp3_t(); clk_t clk = new clk_t(); sat_t sat = new sat_t();
        public Form1()
        {
            InitializeComponent();
            for(int i=0;i<fname.Count();i++)
            {
                fileindex f = new fileindex();
                f.index = dataGridView1.Rows.Add();
                dataGridView1.Rows[f.index].Cells[0].Value = fname[i];
                file.Add(f);
            }
            createchartarea();
        }

        private void 读取数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Title = "选择文件";
            int i = 0;
            if(open.ShowDialog()==DialogResult.OK)
            {
                foreach(var v in open.FileNames)
                {
                    int index = 0;
                    string str = (v.Split('.').Last()).ToLower();
                    if(str.Contains("o"))
                    {
                        index = 0;                  
                    }
                    else if(str.Contains("n"))
                    { index = 1; }
                    else if(str.Contains("sp3"))
                    { index = 2; }
                    else if(str.Contains("clk"))
                    { index = 3; }
                    else if(str.Contains("atx"))
                    { index = 4; }
                    else if(str.Contains("erp"))
                    { index = 5; }
                    else if(str.Contains("dcb"))
                    {
                        if (v.Contains("C1")) { index = 6; }
                        else { index = 7; }
                    }
                    else { break; }                    
                    file[index].path = v;
                    dataGridView1.Rows[index].Cells[1].Value = v;
                }
                
            }
            foreach (var v in file)
                {
                if (v.path == null)
                {
                    flag = 0;
                    MessageBox.Show("数据未全部读入");
                    break;
                }
                else i++;
                }
            if (i >= 8) flag = 1;
        }

        private void x坐标曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawXYZline("X", res);

        }
        public void createchartarea()
        {
            chart1.Series.Clear();
            ChartArea area = new ChartArea();
            area.Name = "FirstArea";
            area.BackColor = Color.AliceBlue;
            area.BackSecondaryColor = Color.White;
            area.BackGradientStyle = GradientStyle.TopBottom;
            area.BackHatchStyle = ChartHatchStyle.None;
            area.BorderDashStyle = ChartDashStyle.NotSet;
           
            area.Position.X = 5;
            area.Position.Y = 7;
            area.Position.Height = 85;
            area.Position.Width = 85;

            area.BorderWidth = 1;
            area.BorderColor = Color.Black;

            area.AxisX.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.Enabled = true;

            area.AxisX.IntervalAutoMode= IntervalAutoMode.VariableCount;
            area.AxisY.IsStartedFromZero = false;
            area.AxisX.IsStartedFromZero = true;
            area.AxisX.LabelStyle.IsEndLabelVisible = true;
            

            area.CursorX.IsUserEnabled = true;
            area.CursorX.AutoScroll = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.CursorY.IsUserEnabled = true;
            area.CursorY.AutoScroll = true;
            area.CursorY.IsUserSelectionEnabled = true;

            chart1.ChartAreas.Add(area);
        }

        private void 开始计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] dcbpath = new string[2] { file[6].path, file[7].path };
            tabControl1.SelectedIndex = 2;
            richTextBox1.Clear();
            res.Clear();
            richTextBox1.AppendText(string.Format("{0,-20}", "时间") + string.Format("{0,-20}", "X坐标") + string.Format("{0,-20}", "Y坐标") + string.Format("{0,-20}", "Z坐标")+"\r\n");
            if (flag == 1)
            {
                Read.readobs(file[0].path, obss, sta);
                Read.readnav(file[1].path, nav);
                Read.readsp3(file[2].path, sp3);
                Read.readclk(file[3].path, clk);
                Read.readatx(file[4].path, pcv);
                Read.readerp(file[5].path, erp);
                Read.readdcb(dcbpath, dcb);
                pppcmn.combsp3(sp3, sat);

                ppp.clk = clk; ppp.pcv = pcv; ppp.sat = sat;               

                ppp.propos(obss, dcb, pcv, sta, nav, erp, res, richTextBox1);               
            }
        }

        private void y坐标曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawXYZline("Y", res);
        }

        private void z坐标曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawXYZline("Z", res);
        }

        private void 输出计算坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "输出文件";
            save.Filter = "文本文件(*.txt)|*.txt";
            if(richTextBox1.Text!=null&&richTextBox1.Lines.Length>1)
            {
                if(save.ShowDialog()==DialogResult.OK)
                {
                    richTextBox1.SaveFile(save.FileName,RichTextBoxStreamType.PlainText);
                }
            }
            else
            {
                MessageBox.Show("尚无计算结果");
            }
        }

        public void drawXYZline(string type,List<result> res)
        {
            if(res.Count<=0)
            {
                MessageBox.Show("无计算结果");
                return;
            }
            else
            {
                tabControl1.SelectedIndex = 1;
                chart1.Series.Clear();
                Series series1 = new Series();
                series1.ChartArea = "FirstArea";
                chart1.Series.Add(series1);
                series1.ToolTip = "#VALX,#VALY";
                series1.Name = type+"坐标";
                series1.ChartType = SeriesChartType.Spline;

                for (int i = 0; i < res.Count; i++)
                {
                    if(type=="X") series1.Points.AddXY(i+1, res[i].X);
                    else if(type=="Y") series1.Points.AddXY(i+1, res[i].Y);
                    else { series1.Points.AddXY(i+1, res[i].Z);}
                    
                }

            }

        }

        public void drawENUline(string type,List<result>res)
        {
            if (res.Count <= 0)
            {
                MessageBox.Show("无计算结果");
                return;
            }
            else
            {
                tabControl1.SelectedIndex = 1;
                chart1.Series.Clear();
                error_enu(res);
                Series series1 = new Series();
                series1.ChartArea = "FirstArea";
                chart1.Series.Add(series1);
                series1.ToolTip = "#VALX,#VALY";
                series1.Name = type + "方向偏差";
                series1.ChartType = SeriesChartType.Spline;

                for (int i = 0; i < res.Count; i++)
                {
                    if (type == "E") series1.Points.AddXY(i + 1, res[i].error_E);
                    else if (type == "N") series1.Points.AddXY(i + 1, res[i].error_N);
                    else { series1.Points.AddXY(i + 1, res[i].error_U); }

                }

            }

        }

        public void error_enu(List<result> res)
        {
            matrix xyz = new matrix(3, 1);
            matrix r = new matrix(3, 1);
            matrix enu = new matrix(3, 1);
            if(realX!=null&&realY!=null&&realZ!=null)
            {
                xyz[1, 1] = double.Parse(realX.Text.Trim());
                xyz[2, 1] = double.Parse(realY.Text.Trim());
                xyz[3, 1] = double.Parse(realZ.Text.Trim());
                 foreach(var v in res)
                 {
                    r[1, 1] = v.X - xyz[1, 1];
                    r[2, 1] = v.Y - xyz[2, 1];
                    r[3, 1] = v.Z - xyz[3, 1];
                    transcoor.xyz2enu(xyz, r, enu);
                    v.error_E = enu[2, 1];
                    v.error_N = enu[1, 1];
                    v.error_U = enu[3, 1];
                 }
            }
           
        }

        private void e方向偏差ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawENUline("E", res);
        }

        private void n方向偏差ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawENUline("N", res);
        }

        private void u方向偏差ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawENUline("U", res);
        }
     
        /*工具栏 输出坐标*/
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            输出计算坐标ToolStripMenuItem_Click( sender,  e);
        }
        /*工具栏 读取数据*/
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            读取数据ToolStripMenuItem_Click( sender,  e);
        }

        /*工具栏 开始计算*/
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            开始计算ToolStripMenuItem_Click(sender, e);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
