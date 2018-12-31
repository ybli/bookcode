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

namespace 开采沉陷计算
{
    public partial class Draw : Form
    {
        public Draw()
        {
            InitializeComponent();
        }
        DXF dxf = new DXF();
        public Draw(List<POINT> point,string xlab,string ylab,string picname)
        {
            InitializeComponent();
            GDI gdi = new GDI(pictureBox1);

            gdi.xlable = xlab;
            gdi.ylable = ylab;
            gdi.picname = picname;
            gdi.Draw(point);

            dxf.xname = xlab ;
            dxf.yname = ylab;
            dxf.dxfname = picname;
            dxf.Draw(point);

            pictureBox1.Image = gdi.export();

        }
        
        private void 保存DXFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            dxf.savefile();
        }

        private void 保存BMPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Focus();

        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            pictureBox1.Width += e.Delta;
            pictureBox1.Height += e.Delta;
        }
        private void Draw_Load(object sender, EventArgs e)
        {
            pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);

        }

        private void 图像复位ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
        }

        private void 保存BMPToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image.Save(saveFileDialog1.FileName);
        }

        private void Draw_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
    class GDI
    {
        private Bitmap bt;
        private Graphics g;
        public Font song1 = new Font("宋体", 10);
        public Font song2 = new Font("宋体", 18);
        public SolidBrush bru = new SolidBrush(Color.Red);

        public string xlable = "";
        public string ylable = "";
        public string picname = "";

        public GDI(PictureBox pic)
        {
            bt = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bt);
            g.Clear(Color.SkyBlue);
            //g.TranslateTransform(50, bt.Height - 50);
        }
        public Bitmap export()
        {
            return bt;
        }

        public void Draw(List<POINT> p)
        {
            Pen blackpen = new Pen(Color.Black);
            Pen redpen = new Pen(Color.Red);
            Pen grennpen = new Pen(Color.Green, 3);

            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (int i = 0; i < p.Count; i++)
            {
                x.Add(p[i].x);
                y.Add(p[i].y);
            }

            double xmax = x.Max();
            double xmin = x.Min();
            double ymax = y.Max();
            double ymin = y.Min();

            double xc = xmax - xmin;
            double yc = ymax - ymin;

            double scale=0;

            //根据y坐标的正负情况确定坐标原点的位置
            if (ymin > 0)//说明y值都为正
            {
                g.TranslateTransform(50, bt.Height - 50);  //此时坐标轴原点在窗体左下角
            }
            else
            { 
                scale=ymax /(ymax -ymin);
                g.TranslateTransform(50, (int)((bt.Height -100)*scale+80) );
            }

           // 坐标比例
            double xscale = (bt.Width - 100) / (xc);
            double yscale =0;
            double ysclz=0;
            double ysclf=0;
            if (ymin >=0)
            {
                 yscale = (bt.Height - 100) / (yc);
            }
            else 
            {
                ysclz =((bt.Height - 100)*scale)/ymax ;
                ysclf =((bt.Height - 100)*(1-scale))/(0-ymin );
            }                           
            //绘制点
            List<POINT> pxy = new List<POINT>();
            List <float >yy1=new List<float> ();    //存储点位在GDI中的坐标
            List<float> xx1 = new List<float>();
            for (int i = 0; i < p.Count ; i++)
            {
                float x1 = (float)((p[i].x - xmin) * xscale);
                float y1 = 0;

                xx1.Add(x1);

                if (ymin > 0)
                {
                    y1 = -(float)((p[i].y - ymin) * yscale);
                    yy1.Add (y1);
                }
                else
                {
                    if (p[i].y > 0)
                    {
                        y1 = -(float)((p[i].y) * ysclz);   //y值大于0的情况
                        yy1 .Add (y1);
                    }
                    else
                    {
                        y1 = -(float)((p[i].y) * ysclf);   //y值小于0的情况
                        yy1.Add (y1);
                    }
                }
                
                pxy.Add(new POINT (x1,y1));
                g.DrawEllipse(grennpen, x1, y1, 1, 1);               
            }

            //连线
            for (int i = 0; i < pxy .Count -1; i++)
            {
                g.DrawLine(redpen,(float ) (pxy[i].x) ,(float ) pxy [i].y , (float )pxy [i+1].x ,(float ) pxy [i+1].y );
            }

            //绘制坐标轴
            //X轴
            g.DrawLine(blackpen, 0, 0, (bt.Width - 90), 0);
            g.DrawString(xlable , song1, bru, (bt.Width - 85), 0);
            //Y轴
            if (ymin >= 0)
            {
                g.DrawLine(blackpen, 0, 0, 0, -(bt.Height - 80));
                g.DrawString(ylable, song1, bru, -40, -(bt.Height - 70));
            }
            else
            {
                g.DrawLine(blackpen, 0, 0, 0, yy1.Min ()-50);
                g.DrawLine(blackpen, 0, 0, 0, yy1 .Max ()+10);
                g.DrawString(ylable, song1, bru,-50,yy1.Min  ()-70 );
            }
            //绘制箭头
            g.DrawLine(blackpen, (bt.Width - 90), 0, (bt.Width - 93), -3);
            g.DrawLine(blackpen, (bt.Width - 90), 0, (bt.Width - 93), 3);
            g.DrawLine(blackpen, 0, yy1.Min() - 50, -3, yy1.Min() - 47);
            g.DrawLine(blackpen, 0, yy1.Min() - 50, 3, yy1.Min() - 47);

            //绘制图名
            g.DrawString(picname, song2, bru, (float)(xx1 .Max ()* 0.50-60), yy1.Min ()-80);
            //坐标轴标注
            //坐标标注距离
            float  ydy = 0;   //真实间距
            float  xdx = 0;

            float  dy = 0;
            float  dx = 0;   //GDI坐标间距
            if (ymin > 0)
            {
                ydy =(float )(ymax / 5.0);
                xdx = (float)(xmax / 5.0);
                dy = (float)(yy1.Min() / 5.0);
                dx = (float)(xx1.Max() / 5.0);

                for (int i = 0; i < 6; i++)
                {
                    //y轴
                    float x1 = 0;
                    float y1 = dy * i;
                    float x2 = -10;
                    float y2 = dy * i;
                    g.DrawLine(blackpen, x1, y1, x2, y2);
                    g.DrawString((-ydy*i).ToString(), song1, bru, x2 - 5, y2);

                    //x轴
                    x1 = dx * i;
                    y1 = 0;
                    x2 = dx * i;
                    y2 = -10;
                    g.DrawLine(blackpen ,x1,y1,x2,y2);
                    g.DrawString((xdx *i).ToString (),song1 ,bru,x2,y2+15);
                }

            }
            else
            {
                ydy = (float)((ymax - ymin) / 8.0);
                float ydz = (float)(ymax / ydy) + 1;      //y轴正半轴标注个数
                float ydf = (float)(-ymin / ydy) + 1;     //y轴负半轴标注的个数

                float dyz = (float)(yy1.Min() / ydz);
                float dyf = (float)(yy1.Max() / ydf);

                //y轴正半轴
                for (int i = 0; i < ydz+1; i++)
                {
                    float x1 = 0;
                    float y1 = dyz * i;
                    float x2 = -10;
                    float y2 = dyz * i;
                    g.DrawLine(blackpen ,x1,y1,x2,y2);
                    g.DrawString((Math.Round(ydy * i, 2)).ToString(), song1, bru, x2 - 25, y2 - 5);
                }
                //y轴负半轴
                for (int i = 1; i < ydf+1 ; i++)
                {
                    float x1 = 0;
                    float y1 = dyf * i;
                    float x2 = -10;
                    float y2 = dyf * i;
                    g.DrawLine(blackpen, x1, y1, x2, y2);
                    g.DrawString((Math .Round ( -ydy * i,2)).ToString(), song1, bru, x2 -35, y2-5);
                }              
            }

            //绘制X轴标注
            for (int i = 1; i < xx1 .Count ; i++)
            {
                g.DrawLine(blackpen ,xx1 [i],0,xx1 [i],-5);
                if (i % 2 == 1)
                {
                    g.DrawString("Z"+(i+1).ToString (),song1 ,bru ,xx1 [i]-9,-15);
                }
            }
        }

    
    }

    public class POINT
    {
        public double x;
        public double y;
        public POINT(double x,double y)
        {
            this.x=x;
            this.y=y;       
        }
    
     }
    class DXF
    {
        public string xname = "";
        public string yname = "";
        public string dxfname = "";
        private string begin = 0 + "\r\n" + "SECTION" + "\r\n" + 2 + "\r\n" + "ENTITIES" + "\r\n";
        private string end = 0 + "\r\n" + "ENDSEC" + "\r\n" + 0 + "\r\n" + "EOF";

        private string style = 0 + "\r\n" + "SECTION" + "\r\n" + 2 + "\r\n" + "TABLES" + "\r\n" + 0 + "\r\n" + "TABLE" + "\r\n" + 2 + "\r\n" + "STYLE" + "\r\n" + 70 + "\r\n" + 0 + "\r\n" + 0 + "\r\n" + "STYLE" + "\r\n" + 2 + "\r\n" + "宋体" + "\r\n" + 70 + "\r\n" + 0 + "\r\n" +
            40 + "\r\n" + 0 + "\r\n" + 41 + "\r\n" + 0 + "\r\n" + 50 + "\r\n" + 0 + "\r\n" + 71 + "\r\n" + 0 + "\r\n" + 42 + "\r\n" + 0 + "\r\n" + 3 + "\r\n" + "COMPLEX" + "\r\n" + 4 + "\r\n" + "宋体" + "\r\n" + 0 + "\r\n" + "ENDTAB" + "\r\n" + 0 + "\r\n" + "ENDSEC" + "\r\n";

        private string HEADER = 0 + "\r\n" + "SECTION" + "\r\n" + 2 + "\r\n" + "HEADER" + "\r\n" + 9 + "\r\n" + "$LIMMIN" + "\r\n" + 10 + "\r\n" + 0 + "\r\n" + 20 + "\r\n" + 0 + "\r\n" + 9 + "\r\n" + "$LIMMAX" + "\r\n" + 10 + "\r\n" + 110 + "\r\n" + 20 + "\r\n" + 110 + "\r\n" + 0 + "\r\n" + "ENDSEC" + "\r\n";

        private string str;

        /// <summary>
        /// 绘制线函数
        /// </summary>
        /// <param name="x1">起点x</param>
        /// <param name="y1">起点y</param>
        /// <param name="x2">终点x</param>
        /// <param name="y2">终点y</param>
        /// <param name="c">颜色</param>
        /// <returns></returns>
        public string Line(double x1, double y1, double x2, double y2, int c)
        {
            string line = 0 + "\r\n" + "LINE" + "\r\n" + 8 + "\r\n" + 0 + "\r\n" + 62 + "\r\n" + c + "\r\n" + 10 + "\r\n" + x1 + "\r\n" + 20 + "\r\n" + y1 + "\r\n" + 30 + "\r\n" + 0 + "\r\n" +
                11 + "\r\n" + x2 + "\r\n" + 21 + "\r\n" + y2 + "\r\n" + 31 + "\r\n" + 0 + "\r\n";
            return line;
        }
        /// <summary>
        /// 绘制圆函数
        /// </summary>
        /// <param name="x1">圆心x</param>
        /// <param name="y1">圆心y</param>
        /// <param name="w">半径</param>
        /// <returns></returns>
        public string Circle(double x1, double y1, double r)
        {
            string circle = 0 + "\r\n" + "CIRCLE" + "\r\n" + 8 + "\r\n" + 0 + "\r\n" + 10 + "\r\n" + x1 + "\r\n" + 20 + "\r\n" + y1 + "\r\n" + 30 + "\r\n" + 0 + "\r\n" + 40 + "\r\n" + r + "\r\n";
            return circle;
        }

        /// <summary>
        /// 绘制标注函数
        /// </summary>
        /// <param name="x1">标注点x</param>
        /// <param name="y1">标注点y</param>
        /// <param name="h">字体高</param>
        /// <param name="c">字体颜色</param>
        /// <param name="T">文本内容</param>
        /// <returns></returns>
        public string Text(double x1, double y1, double h, int c, string T)
        {
            string txt = 0 + "\r\n" + "TEXT" + "\r\n" + 8 + "\r\n" + 0 + "\r\n" + 62 + "\r\n" + c + "\r\n" + 10 + "\r\n" + x1 + "\r\n" + 20 + "\r\n" + y1 + "\r\n" + 30 + "\r\n" + 0 + "\r\n" + 40 + "\r\n" + h + "\r\n" + 7 + "\r\n" + "宋体" + "\r\n" + 1 + "\r\n" + T + "\r\n";
            return txt;
        }
        public void Draw(List<POINT> p)
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (int i = 0; i < p.Count; i++)
            {
                x.Add(p[i].x);
                y.Add(p[i].y);
            }
            double xmax = x.Max();
            double xmin = x.Min();
            double ymax = y.Max();
            double ymin = y.Min();

            double xc = xmax - xmin;
            double yc = ymax - ymin;

            //坐标比例
            double xscale = (100) / (xc);
            double yscale = (100) / (yc);

            //坐标距离
            double xdis = (100) / 4.0;
            double ydis = (100) / 4.0;

            //坐标标注距离
            double xlab = xc / 4.0;
            double ylab = yc / 4.0;

            //绘制点
            List<double > xx = new List<double>();
            List<double > yy = new List<double>();
            for (int i = 0; i < p.Count; i++)
            {
                double x1 = (p[i].x - xmin) * xscale;
                double y1 = (p[i].y - ymin) * yscale;
                yy.Add(y1);
                str += Circle(x1, y1, 0.05);
                //str += Text(x1, y1, 2, 1, p[i].ID);
            }

            //绘制坐标轴及箭头
            //x
            str += Line(0, 0, 105, 0, 7);
            str += Text(108, -2, 2, 1, xname );
            str += Line(105, 0, 103, 1.5, 7);
            str += Line(105, 0, 103, -1.5, 7);
            //y
            str += Line(0, 0, 0, 105, 7);
            str += Text(-5, 108, 2, 1, yname);
            str += Line(0, 105, -1.5, 103, 7);
            str += Line(0, 105, 1.5, 103, 7);

            //绘制坐标间隔
            for (int i = 0; i < 5; i++)
            {
             
                //y
               double  x1 = 0;
               double  y1 = i * ydis;
               double  x2 = -3;
               double  y2 = i * ydis;
                str += Line(x1, y1, x2, y2, 7);
                str += Text(x2 - 5, y2 - 1, 2, 1, Math.Round(ymin + i * ylab, 1).ToString());
            }
           
            //连线
            for (int i = 1; i < p.Count; i++)
            {
                double x1 = (p[i - 1].x - xmin) * xscale;
                xx.Add(x1);
                double y1 = (p[i - 1].y - ymin) * yscale;
                double x2 = (p[i].x - xmin) * xscale;
                double y2 = (p[i].y - ymin) * yscale;
                str += Line(x1, y1, x2, y2, 3);
            }
            xx.Add((p[p.Count -1].x - xmin) * xscale);

            //绘制X轴标注
            for (int i = 0; i < xx.Count ; i++)
            {
                str += Line(xx[i],0,xx[i],1,7);
                if (i % 2 == 1)
                {
                    str += Text(xx[i]-1.5,-3,2,1,"Z"+(i +1).ToString ());
                }                
            }

            //标题
            str += Text(30, 110, 3, 7,dxfname );

        }
        /// <summary>
        /// 保存文件函数
        /// </summary>
        public void savefile()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "output.dxf";
            save.Filter = ".dxf|*.dxf";
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(save.FileName, false, Encoding.Default))
                    {
                        sw.Write(HEADER + style + begin + str + end);
                    }
                    System.Diagnostics.Process open = new System.Diagnostics.Process();
                    open.StartInfo.FileName = save.FileName;
                    open.Start();
                }
                catch
                {
                    MessageBox.Show("请检查数据");
                }
            }


        }


    }


}
