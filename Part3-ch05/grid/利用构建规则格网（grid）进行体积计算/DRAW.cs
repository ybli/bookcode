using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 利用构建规则格网_grid_进行体积计算
{
    class DRAW
    {
        public Bitmap get_picture(List<given_point> g_point, List<given_point> convex_hull,float grid_spacing,float[,]minmax)
        {
            double  w_num = Math.Ceiling((minmax[1, 0] - minmax[0, 0]) / grid_spacing);    //图片中的横轴的网格数
            double h_num = Math.Ceiling((minmax[1, 1] - minmax[0, 1]) / grid_spacing);     //图片中纵轴的网格数
            Bitmap bmap = new Bitmap(650, 400);                             
            Graphics gph = Graphics.FromImage(bmap);                        
            gph.Clear(Color.White);                                 //清空背景
            Brush bru = new SolidBrush(Color.Blue );               //定义画笔

            PointF p0=new PointF ();//画图的基准点
            double w_shrink= (minmax[1, 0] - minmax[0, 0]+grid_spacing) / 600;
            double h_shrink = (minmax[1, 1] - minmax[0, 1]+grid_spacing) / 350;
            double shrink = Math.Max(w_shrink , h_shrink );   //确定缩小比例
            if (shrink == w_shrink)                    //使图形中心在图片的中心
            {
                p0.X = 25;
                p0.Y = 400 - float.Parse(((400 - h_num *grid_spacing / shrink) / 2).ToString());
            }
            else
            {
                p0.Y = 400 - 25;
                p0.X = float.Parse(((650 - w_num *grid_spacing / shrink) / 2).ToString());
            }
            //绘制网格
           for (int i = 0; i <= h_num; i++)    //绘制横线
            {
                gph.DrawLine(Pens.Black, p0.X, float .Parse ((p0.Y - i * grid_spacing/shrink ).ToString ()),
                    float.Parse((p0.X +  w_num *grid_spacing / shrink).ToString()),
                    float.Parse((p0.Y - i * grid_spacing / shrink).ToString()));
            }
            for( int i = 0;i <= w_num;i++ )    //绘制纵线
            {
                gph.DrawLine(Pens.Black, float.Parse((p0.X + i * grid_spacing / shrink).ToString()), p0.Y,
                     float.Parse((p0.X + i * grid_spacing / shrink).ToString()), 
                     float.Parse((p0.Y - h_num *grid_spacing  / shrink).ToString()));
            }
            //画出散点
            for (int i = 0; i < g_point.Count; i++)
            {
                gph.FillEllipse(bru, float .Parse ((  p0.X  + (g_point[i].X - minmax[0, 0]) / shrink - 2.5).ToString ()),//画点
                    float.Parse((p0.Y  - (g_point[i].Y - minmax[0, 1]) / shrink - 2.5).ToString ()), 5, 5);       
                gph.DrawString(g_point[i].point_name, new Font("宋体", 8f), new SolidBrush(Color.Blue),        //画点名
                   float.Parse((p0.X + (g_point[i].X - minmax[0, 0]) / shrink -4).ToString()),
                    float.Parse((p0.Y - (g_point[i].Y - minmax[0, 1]) / shrink + 5).ToString()));

            }
            PointF[] close_hull = new PointF[convex_hull.Count];                   //定义图中的凸包多边形
            for (int i = 0; i < convex_hull.Count; i++)                            
            {
                close_hull[i].X = float.Parse((p0.X + (convex_hull[i].X - minmax[0, 0]) / shrink).ToString());//  生成图中的凸包多边形
                close_hull [i].Y = float.Parse((p0.Y - (convex_hull[i].Y - minmax[0, 1]) / shrink).ToString());
            }
            gph.FillPolygon(new SolidBrush(Color.FromArgb(128, Color.Red)), close_hull);      //填充图形，（为半透明色）
                //下面的已被上面一步覆盖掉
                //连接凸包点，生成凸包
                 /*     for (int i = 0; i < convex_hull.Count - 1; i++)
                {
                gph.DrawLine(Pens.Red ,
                    float.Parse((p0.X +(convex_hull[i].X - minmax[0, 0]) / shrink).ToString()),
                    float.Parse((p0.Y - (convex_hull[i].Y - minmax[0, 1]) / shrink ).ToString()), 
                    float.Parse((p0.X + (convex_hull[i+1].X - minmax[0, 0]) / shrink ).ToString()),
                    float.Parse((p0.Y - (convex_hull[i+1].Y - minmax[0, 1]) / shrink ).ToString()) );
                 }   */               
            return bmap ;     //
        }
        public void save_dxf(string path ,List<given_point> g_point, List<given_point> convex_hull, float[,] minmax,float  grid_size)     //保存dxf文件
        {
            StreamWriter sw = new StreamWriter(path);            //(同步读写)
            writeline(sw, "0", "SECTION");            //开始建立图层
            writeline(sw, "2", "TABLES");
            writeline(sw, "0", "TABLE");
            writeline(sw, "2", "LAYER");
            layer(sw, "point", 70);
            layer(sw, "hull", 10);
            layer(sw, "p_name", 50);
            writeline(sw,"0","ENDTAB");
            writeline(sw, "0", "ENDSEC");           //结束建立图层
            writeline(sw, "0", "SECTION");           //开始画图
            writeline(sw, "2", "ENTITIES");
            /*画出网格(白色的图层)*/
            int w_grid =(int)((minmax[1, 0] - minmax[0, 0]) / grid_size)+1;
            int h_grid = (int)((minmax[1, 1] - minmax[0, 1]) / grid_size) + 1;
            for(int i=0;i<=w_grid;i++)
            { line(sw, minmax [0,0]+grid_size *i, minmax[0, 1] , minmax[0, 0] + grid_size * i,minmax [0,1]+h_grid *grid_size , "grid"); }
            for (int i = 0; i <= h_grid; i++)
            { line(sw, minmax[0, 0], minmax[0, 1] + grid_size * i, minmax[0, 0] + grid_size * w_grid , minmax[0, 1] + grid_size  * i, "grid"); }
            ////画格网完毕

            for (int i = 0; i < g_point.Count(); i++)           //在point图层画出点和在p_name图层画出文字
            {
                point(sw, g_point[i].X, g_point[i].Y, "point");
                text(sw, g_point[i].X+float .Parse ((0.3).ToString ()), g_point[i].Y, "p_name", g_point[i].point_name, float .Parse ((0.5).ToString ()));
            }
            for(int i=0;i<convex_hull.Count ()-1; i++ )         //在hull图层画出凸包
            {
                line(sw, convex_hull[i].X, convex_hull[i].Y, convex_hull[i + 1].X, convex_hull[i + 1].Y, "hull");
           }
            writeline(sw, "0", "ENDSEC");
            writeline(sw, "0", "EOF");
            sw.Close();
        }
        void point(StreamWriter sw, float ax, float ay, string layer_name)//.dxf文件中画点
        {
            writeline(sw, "0", "POINT");
            writeline(sw, "8", layer_name);
            sw.WriteLine("10");
            sw.WriteLine(ax);
            sw.WriteLine("20");
            sw.WriteLine(ay);
        }
        void text(StreamWriter sw, float ax, float ay, string layer_name, string txt, float size)//.dxf文件中写文字
        {
            writeline(sw, "0", "TEXT");
            writeline(sw, "8", layer_name);
            sw.WriteLine("10");
            sw.WriteLine(ax);
            sw.WriteLine("20");
            sw.WriteLine(ay);
            writeline(sw, "1", txt);
            sw.WriteLine("40");
            sw.WriteLine(size);
        }
        void line(StreamWriter sw, float ax, float ay, float bx, float by, string layer_name)//.dxf文件中画线
        {
            writeline(sw, "0", "LINE");
            writeline(sw, "8", layer_name);
            sw.WriteLine("10");
            sw.WriteLine(ax);
            sw.WriteLine("20");
            sw.WriteLine(ay);
            sw.WriteLine("11");
            sw.WriteLine(bx);
            sw.WriteLine("21");
            sw.WriteLine(by);
        }
         void writeline(StreamWriter sw, string a, string b)
        {
            sw.WriteLine(a);
            sw.WriteLine(b);
        }
         void layer(StreamWriter sw, string layer_name, int layer_color)   //.dxf文件中定义图层
        {
            sw.WriteLine("0");
            sw.WriteLine("LAYER");
            sw.WriteLine("70");
            sw.WriteLine("0");
            sw.WriteLine("2");
            sw.WriteLine(layer_name );
            sw.WriteLine("62");
            sw.WriteLine(layer_color);
            sw.WriteLine("6");
            sw.WriteLine("CONTINUOUS");
        }
    }
    
}
