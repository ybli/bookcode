using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Contour_Line
{
    //绘制DXF文件
    class Class_Draw_DXF
    {
        public StreamWriter sw;
        public double minx, miny, maxx, maxy;
        public double d;
        //计算比例系数
        public void CalRatio(List<double> x, List<double> y)
        {
            minx = x.Min();
            miny = y.Min();
            maxx = x.Max();
            maxy = y.Max();
            double t1, t2;
            t1 = (maxx - minx) / 100;
            t2 = (maxy - miny) / 100;
            if (t1 > t2)
            {
                d = t1;
            }
            else
            {
                d = t2;
            }
        }
        //限定画图范围
        public void ConstRange()
        {
            sw.WriteLine(0);
            sw.WriteLine("SECTION");
            sw.WriteLine(2);
            sw.WriteLine("HEADER");
            sw.WriteLine(9);
            sw.WriteLine("$LIMMIN");
            sw.WriteLine(10);
            sw.WriteLine(minx);
            sw.WriteLine(20);
            sw.WriteLine(miny);
            sw.WriteLine(9);
            sw.WriteLine("$LIMMAX");
            sw.WriteLine(10);
            sw.WriteLine(maxx);
            sw.WriteLine(20);
            sw.WriteLine(maxy);
            sw.WriteLine(0);
            sw.WriteLine("ENDSEC");
            sw.WriteLine(0);
            sw.WriteLine("SECTION");
            sw.WriteLine(2);
            sw.WriteLine("ENTITIES");
        }
        //画圆
        public void D_Circle(double x, double y)
        {
            sw.WriteLine(0);
            sw.WriteLine("Circle");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(2);
            sw.WriteLine(10);
            sw.WriteLine(x);
            sw.WriteLine(20);
            sw.WriteLine(y);
            sw.WriteLine(40);
            sw.WriteLine(d);
        }
        //画线
        public void D_Line(double x1, double y1, double z1,double x2, double y2,double z2,int Color)
        {
            sw.WriteLine(0);
            sw.WriteLine("LINE");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(Color);
            sw.WriteLine(10);
            sw.WriteLine(x1);
            sw.WriteLine(20);
            sw.WriteLine(y1);
            sw.WriteLine(30);
            sw.WriteLine(z1);
            sw.WriteLine(11);
            sw.WriteLine(x2);
            sw.WriteLine(21);
            sw.WriteLine(y2);
            sw.WriteLine(31);
            sw.WriteLine(z2);
        }
        //写文字
        public void D_Text(double x, double y, string s)
        {
            sw.WriteLine(0);
            sw.WriteLine("TEXT");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(1);
            sw.WriteLine(10);
            sw.WriteLine(x);
            sw.WriteLine(20);
            sw.WriteLine(y);
            sw.WriteLine(40);
            sw.WriteLine(d);
            sw.WriteLine(1);
            sw.WriteLine(s);
        }
        //写结尾
        public void D_End()
        {
            sw.WriteLine(0);
            sw.WriteLine("ENDSEC");
            sw.WriteLine(0);
            sw.WriteLine("EOF");
            sw.Close();
        }
    }
}
