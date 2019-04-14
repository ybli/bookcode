using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基于hausdorff_Frechet距离的线匹配算法
{
    class DRAW
    {        
        public Bitmap get_picture(List<double>rangeX,List<double>rangeY, shpfile shp1, shpfile shp2)                //在画布上画出图形，并得到画布
        {
            Bitmap bmap = new Bitmap(835,470);
            Graphics gph = Graphics.FromImage(bmap);
            gph.Clear(Color.White);                                 //清空背景
            Brush bru = new SolidBrush(Color.Blue);

            double smX = (rangeX.Max() - rangeX.Min()) / 800.0;
            double smY = (rangeY.Max() - rangeY.Min()) / 450;
            double scaling_magnification=Math.Max(smX,smY);   //统一X，Y轴的缩放比例
            PointF base_shp_p = new PointF((float)rangeX.Min() ,(float)rangeY .Min());               //两个图层的基点（x最小和y最小）
            PointF base_p = new PointF(15,7);
            if (scaling_magnification == smX)
            {
                base_p.Y = (float)(base_p.Y - (450-(rangeY.Max() - rangeY.Min()) / scaling_magnification) / 2.0);
            }
            else
            {
                base_p.X = (float)(base_p.X + (800 - (rangeX.Max() - rangeX.Min()) / scaling_magnification) / 2.0);
            }
            Pen pen = new Pen(Color.Blue, (float)0.001);
             for (int i = 0; i < shp1.line.Count(); i++)                                           //画图层1
            {
                for (int j = 0; j < shp1.line[i].numpoints - 1; j++)
                {
                    gph.DrawLine(pen,fun1(base_p,shp1.line[i].ponits[j],base_shp_p , scaling_magnification),
                       fun1(base_p, shp1.line[i].ponits[j+1], base_shp_p, scaling_magnification));                             //画折线
                    gph.FillEllipse(new SolidBrush(Color.Blue),
                        fun1(base_p, shp1.line[i].ponits[j], base_shp_p, scaling_magnification).X- (float)1 * (float)scaling_magnification/2,
                        fun1(base_p, shp1.line[i].ponits[j], base_shp_p, scaling_magnification).Y- (float)1 * (float)scaling_magnification/2, 
                        (float)1 * (float)scaling_magnification, (float)1 * (float)scaling_magnification);
                //    gph.DrawString(shp1.line[i].ponits[j].name, new Font("宋体", 8f), bru,                                     //画点名(如果线段很多，它的点名重复，图像会变得非常模糊)
                   //     fun1(base_p, shp1.line[i].ponits[j], base_shp_p, scaling_magnification));       
                }
                gph.FillEllipse(new SolidBrush(Color.Blue), 
                    fun1(base_p, shp1.line[i].ponits[shp1.line[i].numpoints - 1], base_shp_p, scaling_magnification).X- (float)1 * (float)scaling_magnification/2,
                        fun1(base_p, shp1.line[i].ponits[shp1.line[i].numpoints - 1], base_shp_p, scaling_magnification).Y- (float)1 * (float)scaling_magnification/2, 
                        (float)1 * (float)scaling_magnification, (float)1 * (float)scaling_magnification);
                //  gph.DrawString(shp1.line[i].ponits[shp1.line[i].numpoints - 1].name, new Font("宋体", 8f), bru,
                //      fun1(base_p, shp1.line[i].ponits[shp1.line[i].numpoints - 1], base_shp_p, scaling_magnification));
            }
            pen = new Pen(Color.Red, (float)0.001);
            for (int i = 0; i < shp2.line.Count(); i++)                                            //画图层2
            {
                for (int j = 0; j < shp2.line[i].numpoints - 1; j++)
                {
                    gph.DrawLine(pen, fun1(base_p, shp2.line[i].ponits[j], base_shp_p, scaling_magnification),
                       fun1(base_p, shp2.line[i].ponits[j + 1], base_shp_p, scaling_magnification));                             //画折线
                    gph.FillEllipse(   new SolidBrush(Color.Red),                                                                 //画线实体的转折点
                        fun1(base_p, shp2.line[i].ponits[j], base_shp_p, scaling_magnification).X - (float)1 * (float)scaling_magnification/2,
                       fun1(base_p, shp2.line[i].ponits[j], base_shp_p, scaling_magnification).Y- (float)1* (float)scaling_magnification/2
                       , (float)1 * (float)scaling_magnification, (float)1 * (float)scaling_magnification);
                    //        gph.DrawString(shp2.line[i].ponits[j].name, new Font("宋体", 8f), bru,                                     //画点名(如果线段很多，它的点名重复，图像会变得非常模糊)
                    //    fun1(base_p, shp2.line[i].ponits[j], base_shp_p, scaling_magnification));
                }
                gph.FillEllipse(new SolidBrush(Color.Red), fun1(base_p, 
                    shp2.line[i].ponits[shp2.line[i].numpoints - 1], base_shp_p, scaling_magnification).X- (float)1 * (float)scaling_magnification/2,
                      fun1(base_p, shp2.line[i].ponits[shp2.line[i].numpoints - 1], base_shp_p, scaling_magnification).Y- (float)1 * (float)scaling_magnification/2,
                      (float)1 * (float)scaling_magnification, (float)1 * (float)scaling_magnification);
                //  gph.DrawString(shp2.line[i].ponits[shp2.line[i].numpoints - 1].name, new Font("宋体", 8f), new SolidBrush(Color.Red),
                //      fun1(base_p, shp2.line[i].ponits[shp2.line[i].numpoints - 1], base_shp_p, scaling_magnification));
            }           
            return bmap;
        }
        public PointF fun1(PointF base_p,PointD p1, PointF p0,double sc)//实际点坐标转换到画布上的点坐标
        {
            PointF p = new PointF((float)(base_p.X + (p1.X - p0.X) / sc), (float)(base_p.Y + 450-(p1.Y - p0.Y) / sc));
            return p;
        }                                           
        public void save_dxf(string path, List<double> rangeX, List<double> rangeY, shpfile shp1,shpfile shp2)// 保存.dxf文件
         {
            double smX = (rangeX.Max() - rangeX.Min()) ;
            double smY = (rangeY.Max() - rangeY.Min()) ;
            double scaling_magnification = Math.Max(smX, smY)/100;


            StreamWriter sw = new StreamWriter(path);            //(同步读写)
             writeline(sw, "0", "SECTION");            //开始建立图层
             writeline(sw, "2", "TABLES");
             writeline(sw, "0", "TABLE");
             writeline(sw, "2", "LAYER");
             layer(sw, "shp1", 70);
             layer(sw, "shp2", 10);
             layer(sw, "shp1_name", 50);
             layer(sw, "shp2_name", 110);
             writeline(sw, "0", "ENDTAB");
             writeline(sw, "0", "ENDSEC");           //结束建立图层
             writeline(sw, "0", "SECTION");           //开始画图
             writeline(sw, "2", "ENTITIES");
             //画出图层1
             for (int i = 0; i <shp1.line.Count ; i++)
             {
                for (int j = 0; j < shp1.line[i].numpoints - 1; j++)
                {
                    line(sw,(float)shp1.line[i].ponits[j].X, (float)shp1.line[i].ponits[j].Y,
                       (float)shp1.line[i].ponits[j + 1].X, (float)shp1.line[i].ponits[j + 1].Y,"shp1");
                    text(sw, (float)((shp1.line[i].ponits[j].X+ shp1.line[i].ponits[j+1].X)/2.0 )+ float.Parse((0.3).ToString()), 
                        (float)((shp1.line[i].ponits[j].Y+shp1.line[i].ponits[j+1].Y)/2.0) , "shp1",
                        shp1.line [i].ponits[j].name, float.Parse( scaling_magnification.ToString()));
                }
               // text(sw, (float)shp1.line[i].ponits[shp1.line[i].numpoints - 1].X + float.Parse((0.3).ToString()), (float)shp1.line[i].ponits[shp1.line[i].numpoints - 1].Y, "shp1",
                       //  shp1.line[i].ponits[shp1.line[i].numpoints - 1].name, float.Parse((5).ToString()));
            }
            //画出图层2
            for (int i = 0; i < shp2.line.Count; i++)
            {
                for (int j = 0; j < shp2.line[i].numpoints - 1; j++)
                {
                    line(sw, (float)shp2.line[i].ponits[j].X, (float)shp2.line[i].ponits[j].Y,
                       (float)shp2.line[i].ponits[j + 1].X, (float)shp2.line[i].ponits[j + 1].Y, "shp2");
                    text(sw, (float)((shp2.line[i].ponits[j].X + shp2.line[i].ponits[j+1].X)/2.0) + float.Parse((0.3).ToString()), 
                        (float)((shp2.line[i].ponits[j].Y+shp2.line[i].ponits[j+1].Y)/2.0), "shp2",
                        shp2.line[i].ponits[j].name, float.Parse(scaling_magnification.ToString()));
                }
             //   text(sw, (float)shp2.line[i].ponits[shp2.line[i].numpoints - 1].X + float.Parse((0.3).ToString()), (float)shp2.line[i].ponits[shp2.line[i].numpoints - 1].Y, "shp2",
                    //     shp2.line[i].ponits[shp2.line[i].numpoints - 1].name, float.Parse((5).ToString()));
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
         void writeline(StreamWriter sw, string a, string b)      //.dxf文件中画线
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
             sw.WriteLine(layer_name);
             sw.WriteLine("62");
             sw.WriteLine(layer_color);
             sw.WriteLine("6");
             sw.WriteLine("CONTINUOUS");
         }
    }

}
