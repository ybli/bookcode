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
using System.Windows.Forms.DataVisualization.Charting;
using CoorLib;
namespace CoorTrans
{
    public partial class Form1 : Form
    {

        
        private ObsData Obs;
        StringBuilder myreport;

        bool BLH = false;
        bool XYZ = false;
        bool xy = false;
        bool BL = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Init()
        {

        }

        private void MenuFile_Click(object sender, EventArgs e)
        {

        }

        #region 文件
        private void toolOpenData_Click(object sender, EventArgs e)
        {
            Init();
            openFileDialog1.Filter = "(txt文件)|*txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Obs = FileHelper.ReadFile(openFileDialog1.FileName);
                    UpdateViews();
                }
                catch (Exception)
                {

                    throw new Exception("数据导入失败！");
                }

            }
        }
        private void MenuFileOpen_Click(object sender, EventArgs e)
        {
            toolOpenData_Click(sender, e);
        }


        private void toolSaveReport_Click(object sender, EventArgs e)
        {
            MenuFileSaveReport_Click(sender, e);
        }

        private void MenuFileSaveXls_Click(object sender, EventArgs e)//保存表格
        {
            saveFileDialog1.Filter = "(xls文件)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Savexls(saveFileDialog1.FileName, Obs.ToDataTable());
                // MessageBox.Show("保存表格成功", "信息提示");
            }
        }

        private void MenuFileSaveChart_Click(object sender, EventArgs e)//保存jpg
        {
            saveFileDialog1.Filter = "(Jpeg文件)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Jpeg);
            }
        }

        private void MenuFileSaveDXF_Click(object sender, EventArgs e)//保存dxf文件
        {
            saveFileDialog1.Filter = "(DXF文件)|*.dxf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Savedxf(Obs.Data, saveFileDialog1.FileName);
            }
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuFileSaveReport_Click(object sender, EventArgs e)//保存txt文件
        {
            saveFileDialog1.Filter = "(TXT文件)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                writer.Write(Report());
                writer.Close();
              

            }
        }



        #endregion


        #region 计算

       
        void UpdateViews()
        {
            richTextBox1.Text = Report();
            dataGridView1.DataSource = Obs.ToDataTable();

        }

        string Report()
        {
            string res = Obs.ToString();

            //如果XYZ坐标已经计算出来
            if (XYZ)
            {
                res += string.Format("\r\n大地坐标（BLH）转换为空间坐标（XYZ）\r\n");
                res += "--------------------------------------\r\n";
                res += $"{"点名",-5}{"B",10}{"L",15}{"  H",8:f4}";
                res += $"{"X",15:f4} {"Y",15:f4}{"Z",15:f4}\r\n";

                foreach (var d in Obs.Data)
                {
                    res += $"{d.Name,-5}{GeoPro.Rad2Str(d.B),15}{GeoPro.Rad2Str(d.L),15}{d.H,10:f4}";
                    res += $"{d.X,15:f4}{d.Y,15:f4}{d.Z,15:f4}\r\n";
                }

            }
            if (BLH)
            {
                res += string.Format("\r\n空间坐标（XYZ）转换为大地坐标（BLH）\r\n");
                res += "--------------------------------------\r\n";
                res += $"{"点名",-5}{"X",15:f4}{"Y",15:f4}{"Z",15:f4}";
                res += $"{"B",15}{"L",15}{"   H",8:f4}\r\n";
                Position pos = new Position(Obs.Datum);

                for (int i = 0; i < Obs.Data.Count; i++)
                {
                    double B, L, H;
                    double X = Obs.Data[i].X + 1000.0;//+1000
                    double Y = Obs.Data[i].Y + 1000.0;//+1000
                    double Z = Obs.Data[i].Z + 1000.0;//+1000
                    pos.CartesianToGeodetic(X, Y, Z, out B, out L, out H);


                    res += $"{Obs.Data[i].Name,-5}{X,15:f4}{Y,15:f4}{Z,15:f4}";
                    res += $"{GeoPro.Rad2Str(B),15}{GeoPro.Rad2Str(L),15}{H,10:f4}\r\n";
                }

            }
            if (xy)
            {
                res += string.Format("\r\n高斯正算（BL-->xy）\r\n");
                res += "--------------------------------------\r\n";
                res += $"{"点名",-5} {"B",12} {"L",12}";
                res += $" {"x",10:f4} {"y",10:f4} \r\n";
                foreach (var d in Obs.Data)
                {
                    res += $"{d.Name,-5} {GeoPro.Rad2Str(d.B),12} {GeoPro.Rad2Str(d.L),12}";
                    res += $" {d.x,10:f4}  {d.y,10:f4}\r\n";
                }

            }
            if (BL)
            {
                res += string.Format("\n高斯反算（xy-->BL）\r\n");
                res += "--------------------------------------\r\n";
                res += $"{"点名",-5} {"x",10:f4} {"y",10:f4}";
                res += $" {"B",12} {"L",12}\r\n";
                Gauss pos = new Gauss(Obs.Datum, Obs.L0);
                double B, L;
                for (int i = 0; i < Obs.Data.Count; i++)
                {
                    // Obs.Data[i].B = 0; Obs.Data[i].L = 0;
                    double x = Obs.Data[i].x + 1000.0;
                    double y = Obs.Data[i].y + 1000.0;
                    pos.xy2BL(x, y, out B, out L);

                    res += $"{Obs.Data[i].Name,-5} {x,10:f4}  {y,10:f4}";
                    res += $" {GeoPro.Rad2Str(B),15}  {GeoPro.Rad2Str(L),15}\r\n ";
                }
            }
            return res;
        }

        private void toolBLH2XYZ_Click(object sender, EventArgs e)
        {

            Position pos = new Position(Obs.Datum);

            for (int i = 0; i < Obs.Data.Count; i++)
            {
                double X, Y, Z;
                double B = Obs.Data[i].B;
                double L = Obs.Data[i].L;
                double H = Obs.Data[i].H;
                pos.GeodeticToCartesian(B, L, H, out X, out Y, out Z);
                Obs.Data[i].X = X;
                Obs.Data[i].Y = Y;
                Obs.Data[i].Z = Z;
            }
            XYZ = true;
            UpdateViews();
        }
        private void MenuBLH2XYZ_Click(object sender, EventArgs e)
        {
            toolBLH2XYZ_Click(sender, e);
        }
        private void toolBL2xy_Click(object sender, EventArgs e)
        {

            Gauss pos = new Gauss(Obs.Datum, Obs.L0);
            for (int i = 0; i < Obs.Data.Count; i++)
            {
                double x, y;
                double B = Obs.Data[i].B;
                double L = Obs.Data[i].L;
                pos.BL2xy(B, L, out x, out y);
                Obs.Data[i].x = x;
                Obs.Data[i].y = y;

            }
            xy = true;
            UpdateViews();
            //return res;
        }
        private void MenuBL2xy_Click(object sender, EventArgs e)
        {
            toolBL2xy_Click(sender, e);
        }
        private void toolXYZ2BLH_Click(object sender, EventArgs e)
        {

            if (XYZ)
            {
                BLH = true;
            }
            UpdateViews();
        }
        private void MenuXYZ2BLH_Click(object sender, EventArgs e)
        {

            toolXYZ2BLH_Click(sender, e);
        }
        private void toolxy2BL_Click(object sender, EventArgs e)
        {
            if (xy)
            {
                BL = true;
            }
            UpdateViews();
        }

        private void Menuxy2BL_Click(object sender, EventArgs e)
        {
            toolxy2BL_Click(sender, e);
        }

        private void MenuDoALL_Click(object sender, EventArgs e)
        {
            toolBLH2XYZ_Click(sender, e);
            toolXYZ2BLH_Click(sender, e);
            toolBL2xy_Click(sender, e);
            toolxy2BL_Click(sender, e);
        }

        #endregion
        

        #region 查看

        private void toolChart_Click(object sender, EventArgs e)
        {
            Draw(this.Width, this.Height);
            tabControl1.SelectedIndex = 1;
        }
        private void MenuChart_Click(object sender, EventArgs e)
        {
            toolChart_Click(sender, e);
        }
        void Draw(int width, int height)
        {
            chart1.Legends.Clear();
            chart1.ChartAreas.Clear();
            // chart1.Width = width;
            // chart1.Height = height;
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "Default";
            DrawChart.ChartAreaPara(ref chartArea1, Obs.Data);
            chart1.ChartAreas.Add(chartArea1);


            //绘制点
            Series series1 = DrawChart.PointSeries(Obs.Data);
            series1.ChartArea = "Default";
            DrawChart.Series1Para(ref series1);


            chart1.Series.Clear();
            chart1.Series.Add(series1);

            chart1.ChartAreas["Default"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Default"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Default"].AxisX.ScaleView.Zoomable = true;

            chart1.ChartAreas["Default"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["Default"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Default"].AxisY.ScaleView.Zoomable = true;

            chart1.DataBind();

        }
        private void toolReport_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
        private void MenuReport_Click(object sender, EventArgs e)
        {
            toolReport_Click(sender, e);

        }

        private void toolZoomIn_Click(object sender, EventArgs e)
        {
            // chart1.ChartAreas["Default"].AxisX.ScaleView.Size = 1.2;
            chart1.Width = Convert.ToInt32(chart1.Width * 1.2);
            chart1.Height = Convert.ToInt32(chart1.Height * 1.2);

            tabControl1.SelectedIndex = 1;
        }

        private void MenuZoomIn_Click(object sender, EventArgs e)
        {
            toolZoomIn_Click(sender, e);

        }

        private void toolZoomout_Click(object sender, EventArgs e)
        {
            chart1.Width = Convert.ToInt32(chart1.Width / 1.2);
            chart1.Height = Convert.ToInt32(chart1.Height / 1.2);
        }

        private void MenuZoomOut_Click(object sender, EventArgs e)
        {
            toolZoomout_Click(sender, e);
        }
        #endregion

        #region 帮助

        private void toolHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("《测绘程序设计试题集（竞赛篇 试题01 坐标转换）》配套程序\n作者：李英冰，辛绍铭，李萌，赵望宇\n武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.6.24");
        }

        private void MenuHelp_Click(object sender, EventArgs e)
        {
            toolHelp_Click(sender, e);
        }

        #endregion

    }
}
