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

    public partial class picture : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c点云数据"></param>
        /// <param name="g格网点数据"></param>
        public picture(Tpoint[] c,List<PointF> contourLine)
        {
            InitializeComponent();
            p_cloud = c;
            Lines = contourLine;
        }
        public double z = 3.00;
        public PointF[] p;//保存double型点集
        public Point[] q;//保存int型点集
        public Tpoint[] p_cloud;//点云数据

        List<PointF> Lines;
        PointF[] p1;
        Point[] q1;


        private void button1_Click(object sender, EventArgs e)
        {
            z /= 1.3;
            getpic(z);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            z /= 0.7;
            getpic(z);
        }

        public void getpic(double zoom)
        {
            //变量定义
            int n = p_cloud.Length;//图像点集大小
            int n1 = Lines.Count;
            //点集赋值
            p = new PointF[n];
            p1 = new PointF[n1];
            for (int i = 0; i < n; i++)
            {
                p[i].x = p_cloud[i].x;
                p[i].y = p_cloud[i].y;
            }
            for (int i = 0; i < n1; i++)
            {
                p1[i].x = Lines[i].x;
                p1[i].y = Lines[i].y;
            }
            //点集放缩平移
            double x_average = 0;
            double y_average = 0;
            double x_max = 0;
            double y_max = 0;
            q = new Point[n];
            q1 = new Point[n1];
            //平移
            for (int i = 0; i < n; i++)
            {
                x_average += p[i].x;
                y_average += p[i].y;
            }
            for (int i = 0; i < n1; i++)
            {
                x_average += p1[i].x;
                y_average += p1[i].y;
            }
            x_average /= (n+n1);
            y_average /= (n+n1);
            for (int i = 0; i < n; i++)
            {
                p[i].x -= x_average;
                p[i].y -= y_average;
                if (Math.Abs(p[i].x) > x_max) x_max = Math.Abs(p[i].x);
                if (Math.Abs(p[i].y) > y_max) y_max = Math.Abs(p[i].y);
            }
            for (int i = 0; i < n1; i++)
            {
                p1[i].x -= x_average;
                p1[i].y -= y_average;
                if (Math.Abs(p1[i].x) > x_max) x_max = Math.Abs(p1[i].x);
                if (Math.Abs(p1[i].y) > y_max) y_max = Math.Abs(p1[i].y);
            }

            //放缩
            double  pic_size = 500.00;//图像画布大小
            for (int i = 0; i < n; i++)
            {
                q[i].X = (int)(pic_size / 2 + p[i].x * pic_size / zoom / x_max);
                q[i].Y = (int)(pic_size / 2 - p[i].y * pic_size / zoom / y_max);
            }

            for (int i = 0; i < n1; i++)
            {
                q1[i].X = (int)(pic_size / 2 + p1[i].x * pic_size / zoom / x_max);
                q1[i].Y = (int)(pic_size / 2 - p1[i].y * pic_size / zoom / y_max);
            }
            //画图
            //自定义点
            Bitmap poi = new Bitmap(3,3);
            Image po = poi;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    poi.SetPixel(j,i,Color.Red);
            //略图本体
            Bitmap grrr = new Bitmap(500,500);
            Image grr = grrr;
            Graphics gr = Graphics.FromImage(grr);
            for (int i = 0; i < n;i++ )
            {
                if (i % 3 == 0)
                {
                    gr.DrawLine(new Pen(Color.Black),q[i],q[i+1]); 
                }
                else if (i % 3 == 1)
                {
                    gr.DrawLine(new Pen(Color.Black), q[i], q[i + 1]); 
                }
                else
                {
                    gr.DrawLine(new Pen(Color.Black), q[i], q[i - 2]); 
                }
            }

            for (int i = 0; i < n1-1; i+=2)
            {
               gr.DrawLine(new Pen(Color.Red), q1[i], q1[i + 1]);           
            }
            //显示
            pictureBox1.Image = grr;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "*.bmp|*.bmp";
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            if (sa.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(sa.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picture_Load(object sender, EventArgs e)
        {
            getpic(z);
        }
    }
}
