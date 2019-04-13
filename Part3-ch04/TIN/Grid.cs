using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TIN
{
    public partial class Grid : Form
    {
        DataGridView data;
        RichTextBox textBox;
        internal TIN Tin;             //不规则三角网
        internal Tpoint[] tpoints;    //输入的点集
        internal List<PointF> contourLine; //等高线
        bool rdbcheck = false; 
        /// <summary>
        /// 画图相关
        /// </summary>
        public double zoom = 3.00;
        public PointF[] p;
        double x_average = 0;
        double y_average = 0;
        double x_max = 0;
        double y_max = 0;
        public PointF[] ph;
        public Point[] q;
        public Point[] qh;
        public Point[] go = { new Point(0, 0), new Point(0, 0) };
        public bool Clicked = false;

        public Grid()
        {
            InitializeComponent();
            point_h.Checked = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Clicked)
            {
                Point p = new Point(Cursor.Position.X - go[1].X + go[0].X, go[1].Y - Cursor.Position.Y + go[0].Y);
                GetPic_Line(zoom, p);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                go[1].X = Cursor.Position.X;
                go[1].Y = Cursor.Position.Y;
                Clicked = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            go[0].X = Cursor.Position.X - go[1].X + go[0].X;
            go[0].Y = go[1].Y - Cursor.Position.Y + go[0].Y;
            Clicked = false;
        }

        private void GetPic_Point(double zoom,Point go)
        {
            int n = tpoints.Length;
            p = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                p[i].x = tpoints[i].x;
                p[i].y = tpoints[i].y;
            }

            for (int i = 0; i < n; i++)
            {
                x_average += p[i].x;
                y_average += p[i].y;
            }
            x_average /= n;
            y_average /= n;
            for (int i = 0; i < n; i++)
            {
                p[i].x -= x_average;
                p[i].y -= y_average;
                if (Math.Abs(p[i].x) > x_max) x_max = Math.Abs(p[i].x);
                if (Math.Abs(p[i].y) > y_max) y_max = Math.Abs(p[i].y);
            }
            double pic_height = pictureBox1.Size.Height;
            //投影到图像坐标系
            q = new Point[n];
            for (int i = 0; i < n; i++)
            {
                q[i].X = (int)(pic_height / 2 + go.X + p[i].x * pic_height / x_max / zoom);
                q[i].Y = (int)(pic_height / 2 - go.Y - p[i].y * pic_height / y_max / zoom);
            }
            //画点
            Bitmap map = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            Image ge = map;
            Graphics gra = Graphics.FromImage(ge);

            Bitmap oo = new Bitmap(3, 3);
            Image o = oo;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    oo.SetPixel(i, j, Color.Red);

            for (int i = 0; i <n; i ++)
            {
                gra.DrawImage(o, q[i]);
               // gra.DrawString(tpoints[i].Name, new Font("宋体", 8), new SolidBrush(Color.Black), q[i]);
            }
            pictureBox1.Image = ge;
        }

        private void GetPic_Line(double zoom, Point go)
        {
            int n1 = contourLine.Count;
            int n = Tin.TinNetP.Count;
            p = new PointF[n];
            ph = new PointF[n1];
            //
            for(int i=0;i<n;i++)
            {
                p[i].x = Tin.TinNetP[i].x;
                p[i].y = Tin.TinNetP[i].y;
                p[i].z = Tin.TinNetP[i].h;
            }
            for (int i = 0; i < n1; i++)
            {
                ph[i].x = contourLine[i].x;
                ph[i].y = contourLine[i].y;
                ph[i].z = contourLine[i].z;
            }
            //
            q = new Point[n];
            qh = new Point[n1];
            //
            for (int i = 0; i < n; i++)
            {
                p[i].x -= x_average;
                p[i].y -= y_average;
            }

            for (int i = 0; i < n1; i++)
            {
                ph[i].x -= x_average;
                ph[i].y -= y_average;
            }

            double pic_width = pictureBox1.Size.Width;
            double pic_height = pictureBox1.Size.Height;
            //
            for (int i = 0; i < n; i++)
            {
                q[i].X = (int)(pic_height / 2 + go.X + p[i].x * pic_height / x_max / zoom);
                q[i].Y = (int)(pic_height / 2 - go.Y - p[i].y * pic_height / y_max / zoom);
            }

            for (int i = 0; i < n1; i++)
            {
                qh[i].X = (int)(pic_height / 2 + go.X + ph[i].x * pic_height / zoom / x_max);
                qh[i].Y = (int)(pic_height / 2 - go.Y - ph[i].y * pic_height / zoom / y_max);
            }
            //
            Bitmap bitmap = new Bitmap(3, 3);
            Image image = bitmap;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    bitmap.SetPixel(i, j, Color.Red);

            Bitmap map = new Bitmap(pictureBox1.Size.Width,pictureBox1.Size.Height);
            Image ge = map;
            Graphics gra = Graphics.FromImage(ge);

            for (int i = 0; i < n; i++)
            {
                gra.DrawImage(image, q[i]);
            }

            if(point_h.Checked==true)
            {             
                Point[] q2 = new Point[q.Length];  //三角形点的偏移坐标
                q.CopyTo(q2, 0);
                for(int i=0;i<n;i++)
                {
                    q2[i].X += (int)(10 / zoom);
                    q2[i].Y += (int)(10 / zoom);
                }
                for (int i = 0; i < n; i++)
                {
                    gra.DrawString(p[i].z.ToString("f1"),new Font("Verdana", (int)(pictureBox1.Size.Height/x_max/zoom)),new SolidBrush(Color.Red),q[i]);
                }
            }
            else
            {
                
            }



            for (int i = 0; i < n; i++)
            {
                if (i % 3 == 0)
                {                
                    gra.DrawLine(new Pen(Color.Black), q[i], q[i + 1]);
                }
                else if (i % 3 == 1)
                {
                    gra.DrawLine(new Pen(Color.Black), q[i], q[i + 1]);
                }
                else
                {
                    gra.DrawLine(new Pen(Color.Black), q[i], q[i - 2]);
                }
            }

            //设置虚线
            Pen pen = new Pen(Color.Brown);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 5, 5 };
            for (int i = 0; i < n1 - 1; i += 2)
            {
                gra.DrawLine(pen, qh[i], qh[i + 1]);
            }

            pictureBox1.Image = ge;         
        }





        private void Grid_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 新建数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newgrid(out data, 11);
        }

        private void newgrid(out DataGridView data, int n)
        {
            tabPage1.Controls.Clear();
            data = new DataGridView();
            tabPage1.Controls.Add(data);
            data.Dock = DockStyle.Fill;
            data.ColumnCount = 4;
            data.RowCount = n;
            for (int i = 0; i < data.ColumnCount; i++) data.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < data.ColumnCount; i++) data.Columns[i].Width = (data.Width - 20) / data.ColumnCount;
            data.RowHeadersVisible = false;
            data.Columns[0].HeaderText = "点名";
            data.Columns[1].HeaderText = "X分量";
            data.Columns[2].HeaderText = "Y分量";
            data.Columns[3].HeaderText = "H分量";
            //data.BorderStyle = BorderStyle.FixedSingle;
            //data.Font = new Font("宋体",15,FontStyle.Bold);
            tabControl1.SelectedTab = tabPage1;
            data.Show();
        }

        private void InputGrid()
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Filter = "*.txt|*.txt"
            };
            if (op.ShowDialog() == DialogResult.OK)
            {
                string[] alllines = File.ReadAllLines(op.FileName);
                string[] line;
                int n = alllines.Length - 2;
                newgrid(out data, n);
                for (int i = 0; i < alllines.Length; i++)
                {
                    line = alllines[i].Split(',');
                    if (i == 0) toolStripTextBox1.Text = line[1];
                    else if (i == 1)
                    {

                    }
                    else
                    {
                        data[0, i-2].Value = line[0];
                        data[1, i-2].Value = line[1];
                        data[2, i-2].Value = line[2];
                        data[3, i-2].Value = line[3];
                    }
                }
            }                                              
        }

        private void Open_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {

                InputGrid();
                tpoints = new Tpoint[data.RowCount];
                for (int i = 0; i < data.RowCount; i++)
                {
                    tpoints[i] = new Tpoint(i + 1, data[0, i].Value.ToString(), double.Parse(data[1, i].Value.ToString()),
                        double.Parse(data[2, i].Value.ToString()), double.Parse(data[3, i].Value.ToString()));
                }
                GetPic_Point(zoom, go[1]);
            }
            catch
            {
                MessageBox.Show("打开数据失败");
            }
        }

        /// <summary>
        /// Tin计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            double hr = double.Parse(toolStripTextBox1.Text);        
            Tin = new TIN(hr, tpoints);//实例化三角网类
            Tin.CalTin(); //构建三角网并计算体积完成
            contourLine = Tin.GetContourLine(); //获取等高线
            GetPic_Line(zoom, go[1]);

            toolStripTextBox3.Text = Tin.H0.ToString("f2"); //显示平衡高程
            tabControl1.SelectedTab = tabPage2;
            ReportShow();
        }


        private void ReportShow()
        {
            tabPage3.Controls.Clear();
            string text = "";
            text += "                   结果报告";
            text += "                                                 \n";
            text += "-------------------基本信息----------------------\n";
            text += "基准高程" + Tin.H_start.ToString("f1") + "m" + "\n";
            text += "三角形个数:" + Tin.Net.Count.ToString() + "\n";
            text += "平衡高程:" + Tin.H0.ToString() + "\n";
            text += "总挖方体积:" + Tin.V_cut.ToString() + "\n";
            text += "总填方体积:" + Tin.V_fill.ToString() + "\n";
            text += "总体积:" + Tin.V_sum.ToString() + "\n";
            text += "                                                 \n";
            text += "------------------三角形说明------------------\n";
            text += "序号    三个顶点" + "\n";
            for (int i = 0; i < Tin.Net.Count; i++)
            {
                text += (i + 1).ToString().PadRight(8);
                text += Tin.Net[i].p1.Name.PadRight(8) + Tin.Net[i].p2.Name.PadRight(8) + Tin.Net[i].p3.Name.PadRight(8) + "\n";
            }
            text += "                                                 \n";
            text += "------------------具体体积说明--------------------\n";
            text += "序号    挖方体积  填方体积         " + "\n";
            for (int i = 0; i < Tin.Net.Count; i++)
            {
                text += (i + 1).ToString().PadRight(8);
                text += Tin.Net[i].V_cut.ToString("f3").PadRight(10) + Tin.Net[i].V_fill.ToString("f3").PadRight(10) + "\n";
            }
            textBox = new RichTextBox();
            tabPage3.Controls.Add(textBox);
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Text = text;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            zoom = 3.00;
            go[0] = new Point(0, 0);
            GetPic_Line(zoom, go[0]);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            zoom *= 0.9;
            GetPic_Line(zoom, go[0]);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            zoom *= 1.1;
            GetPic_Line(zoom, go[0]);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                SaveFileDialog sa = new SaveFileDialog();
                sa.Filter = "*.dxf|*.dxf";
                if (sa.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sr = new StreamWriter(sa.FileName);
                    sr.WriteLine("0");
                    sr.WriteLine("SECTION");
                    sr.WriteLine("2");
                    sr.WriteLine("ENTITIES");

                    PointF[] tps = new PointF[tpoints.Length];
                    for(int i=0;i<tps.Length;i++)
                    {
                        tps[i].x = tpoints[i].x;
                        tps[i].y = tpoints[i].y;
                    }
                    //画点|写文字
                    string[] pen = new string[14];

                    for (int i=0;i<tpoints.Length;i++)
                    {
                        pen = CircleDXF(tps[i], 5);
                        for (int j = 0; j < 12; j++) sr.WriteLine(pen[j]);
                        pen = TextDXF(tps[i], 8, tpoints[i].Name);
                        for (int j = 0; j < 14; j++) sr.WriteLine(pen[j]);
                    }

                    //画线
                    PointF[] net = new PointF[Tin.TinNetP.Count];
                    for(int j=0;j<net.Length;j++)
                    {
                        net[j].x = Tin.TinNetP[j].x;
                        net[j].y = Tin.TinNetP[j].y;
                    }

                    for (int i = 0; i < net.Length; i++)
                    {
                        if (i % 3 == 0)
                        {
                            pen=LineDXF(net[i], net[i + 1], "1");
                            for (int j = 0; j < 14; j++) sr.WriteLine(pen[j]);
                        }
                        else if (i % 3 == 1)
                        {
                            pen=LineDXF(net[i], net[i + 1], "1");
                            for (int j = 0; j < 14; j++) sr.WriteLine(pen[j]);
                        }
                        else
                        {
                            pen=LineDXF(net[i], net[i - 2], "1");
                            for (int j = 0; j < 14; j++) sr.WriteLine(pen[j]);
                        }
                    }

                    PointF[] cline = new PointF[contourLine.Count];
                    for (int j = 0; j < cline.Length; j++)
                    {
                        cline[j].x = contourLine[j].x;
                        cline[j].y = contourLine[j].y;
                    }

                    for (int i = 0; i < cline.Length - 1; i += 2)
                    {
                        pen=LineDXF(cline[i], cline[i + 1], "2");
                        for (int j = 0; j < 14; j++) sr.WriteLine(pen[j]);
                    }

                    sr.WriteLine("0");
                    sr.WriteLine("ENDSEC");
                    sr.WriteLine("0");
                    sr.WriteLine("EOF");
                    sr.Close();
                }
            }

            else if(tabControl1.SelectedTab == tabPage3)
            {
                SaveFileDialog sa = new SaveFileDialog();
                sa.Filter = "*.txt|*.txt";
                if (sa.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text= textBox.Text.Replace("\n", "\r\n");
                    File.WriteAllText(sa.FileName,textBox.Text);
                }
            }
        }

        private void 数据表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void 示意图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void 计算报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private string[] LineDXF(PointF start, PointF end, string color)
        {
            string[] data = new string[14];
            data[0] = "0";
            data[1] = "LINE";
            data[2] = "8";
            data[3] = "0";
            data[4] = "62";
            data[5] = color;
            data[6] = "10";
            data[7] = start.x.ToString();
            data[8] = "20";
            data[9] = start.y.ToString();
            data[10] = "11";
            data[11] = end.x.ToString();
            data[12] = "21";
            data[13] = end.y.ToString();
            return data;
        }
        private string[] TextDXF(PointF center, int size, string words)
        {
            string[] data = new string[14];
            data[0] = "0";
            data[1] = "TEXT";
            data[2] = "8";
            data[3] = "0";
            data[4] = "62";
            data[5] = "1";
            data[6] = "10";
            data[7] = center.x.ToString();
            data[8] = "20";
            data[9] = center.y.ToString();
            data[10] = "40";
            data[11] = size.ToString();
            data[12] = "1";
            data[13] = words;
            return data;
        }
        private string[] CircleDXF(PointF center, double radius)
        {
            string[] data = new string[12];
            data[0] = "0";
            data[1] = "CIRCLE";
            data[2] = "8";
            data[3] = "0";
            data[4] = "62";
            data[5] = "1";
            data[6] = "10";
            data[7] = center.x.ToString();
            data[8] = "20";
            data[9] = center.y.ToString();
            data[10] = "40";
            data[11] = radius.ToString();
            return data;
        }

        private void Document_Click(object sender, EventArgs e)
        {

        }

        private void 计算三角网及体积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void 查看帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("该程序适用于不规则三角网体积计算\n  1.新建或打开" +
                "\n   新建用于手动输入数据，打开则通过文件输入数据\n 2.计算\n   输入数据后即可计算\n " +
                "3.保存\n   对应示意图界面可保存为.dxf,对应计算报告界面可保存为.txt\n\n\n" +
                "                                                                                 2018.12.1",
                "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            查看帮助ToolStripMenuItem_Click(sender, e);
        }

        private void point_h_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void point_h_Click(object sender, EventArgs e)
        {
            if(rdbcheck)
            {
                point_h.Checked = false;
                rdbcheck = false;
                GetPic_Line(zoom, go[0]);
            }
            else
            {
                point_h.Checked = true;
                rdbcheck = true;
                GetPic_Line(zoom, go[0]);
            }
        }
    }
}
