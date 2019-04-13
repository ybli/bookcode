using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MapIcon
{
    class Symbol_DXF
    {
        public StreamWriter sw;

        //多点房屋
        public void DF(int Color, List<double> x, List<double> y, List<double> z)
        {
            x.Add(x[0]);
            y.Add(y[0]);
            z.Add(z[0]);
            for (int i = 0; i < x.Count - 1; i++)
            {
                sw.WriteLine(0);
                sw.WriteLine("LINE");
                sw.WriteLine(8);
                sw.WriteLine("0");
                sw.WriteLine(62);
                sw.WriteLine(Color);
                sw.WriteLine(10);
                sw.WriteLine(x[i]);
                sw.WriteLine(20);
                sw.WriteLine(y[i]);
                sw.WriteLine(30);
                sw.WriteLine(z[i]);
                sw.WriteLine(11);
                sw.WriteLine(x[i + 1]);
                sw.WriteLine(21);
                sw.WriteLine(y[i + 1]);
                sw.WriteLine(31);
                sw.WriteLine(z[i + 1]);
            }
        }

        //砼房屋
        public void TF(int Color, List<double> x, List<double> y, List<double> z,string Code,double FontSize)
        {
            x.Add(x[0]);
            y.Add(y[0]);
            z.Add(z[0]);
            for (int i = 0; i < x.Count - 1; i++)
            {
                sw.WriteLine(0);
                sw.WriteLine("LINE");
                sw.WriteLine(8);
                sw.WriteLine("0");
                sw.WriteLine(62);
                sw.WriteLine(Color);
                sw.WriteLine(10);
                sw.WriteLine(x[i]);
                sw.WriteLine(20);
                sw.WriteLine(y[i]);
                sw.WriteLine(30);
                sw.WriteLine(z[i]);
                sw.WriteLine(11);
                sw.WriteLine(x[i + 1]);
                sw.WriteLine(21);
                sw.WriteLine(y[i + 1]);
                sw.WriteLine(31);
                sw.WriteLine(z[i + 1]);
            }

            double x_c, y_c;

            x_c = (x.Max() + x.Min()) / 2;
            y_c = (y.Max() + y.Min()) / 2;

            string[] a = new string[2];
            a = Code.Split('-');

            string A = "T" + a[1];

            sw.WriteLine(0);
            sw.WriteLine("TEXT");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(Color);
            sw.WriteLine(10);
            sw.WriteLine(x_c);
            sw.WriteLine(20);
            sw.WriteLine(y_c);
            sw.WriteLine(40);
            sw.WriteLine(FontSize);
            sw.WriteLine(1);
            sw.WriteLine(A);
        }

        //小路
        public void XL(int Color, List<double> x, List<double> y, List<double> z)
        {
            for (int i = 0; i < x.Count - 1; i++)
            {
                sw.WriteLine(0);
                sw.WriteLine("LINE");
                sw.WriteLine(8);
                sw.WriteLine("0");
                sw.WriteLine(62);
                sw.WriteLine(Color);
                sw.WriteLine(10);
                sw.WriteLine(x[i]);
                sw.WriteLine(20);
                sw.WriteLine(y[i]);
                sw.WriteLine(30);
                sw.WriteLine(z[i]);
                sw.WriteLine(11);
                sw.WriteLine(x[i + 1]);
                sw.WriteLine(21);
                sw.WriteLine(y[i + 1]);
                sw.WriteLine(31);
                sw.WriteLine(z[i + 1]);
            }
        }

        //公路
        public void GL(int Color, List<double> x, List<double> y, List<double> z, string Code)
        {
            double k = 0;
            string[] A = new string[2];
            double a = 0;
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0;


            //画第一条
            for (int i = 0; i < x.Count - 1; i++)
            {
                sw.WriteLine(0);
                sw.WriteLine("LINE");
                sw.WriteLine(8);
                sw.WriteLine("0");
                sw.WriteLine(62);
                sw.WriteLine(Color);
                sw.WriteLine(10);
                sw.WriteLine(x[i]);
                sw.WriteLine(20);
                sw.WriteLine(y[i]);
                sw.WriteLine(30);
                sw.WriteLine(z[i]);
                sw.WriteLine(11);
                sw.WriteLine(x[i + 1]);
                sw.WriteLine(21);
                sw.WriteLine(y[i + 1]);
                sw.WriteLine(31);
                sw.WriteLine(z[i + 1]);
            }

            //确定路宽
            if (Code.Contains("+"))
            {
                A = Code.Split('+');
                k = Convert.ToDouble(A[1]);
            }
            else if (Code.Contains("-"))
            {
                A = Code.Split('-');
                k = -Convert.ToDouble(A[1]);
            }

            //第二条线
            List<double> xP = new List<double>();
            List<double> yP = new List<double>();

            //起点
            x1 = x[0]; y1 = y[0]; x2 = x[1]; y2 = y[1];
            a = Math.Atan(Math.Abs((y2 - y1) / (x2 - x1)));
            if (((y2 - y1) / (x2 - x1)) < 0)
            {
                xP.Add(x1 - k * Math.Sin(a));
                yP.Add(y1 - k * Math.Cos(a));
            }
            else
            {
                xP.Add(x1 + k * Math.Sin(a));
                yP.Add(y1 - k * Math.Cos(a));
            }

            //中间点
            for (int i = 0; i < x.Count - 2; i++)
            {
                x1 = x[i]; y1 = y[i]; x2 = x[i + 1]; y2 = y[i + 1];
                a = Math.Atan(Math.Abs((y1 - y2) / (x1 - x2)));

                xP.Add(x2);
                yP.Add(y2 - k / Math.Cos(a));
            }

            //终点
            x1 = x[x.Count - 2]; y1 = y[x.Count - 2]; x2 = x[x.Count - 1]; y2 = y[x.Count - 1];
            a = Math.Atan(Math.Abs((y2 - y1) / (x2 - x1)));
            if (((y2 - y1) / (x2 - x1)) < 0)
            {
                xP.Add(x2 - k * Math.Sin(a));
                yP.Add(y2 - k * Math.Cos(a));
            }
            else
            {
                xP.Add(x2 + k * Math.Sin(a));
                yP.Add(y2 - k * Math.Cos(a));
            }

            //画第二条
            for (int i = 0; i < x.Count - 1; i++)
            {
                sw.WriteLine(0);
                sw.WriteLine("LINE");
                sw.WriteLine(8);
                sw.WriteLine("0");
                sw.WriteLine(62);
                sw.WriteLine(Color);
                sw.WriteLine(10);
                sw.WriteLine(xP[i]);
                sw.WriteLine(20);
                sw.WriteLine(yP[i]);
                sw.WriteLine(30);
                sw.WriteLine(z[i]);
                sw.WriteLine(11);
                sw.WriteLine(xP[i + 1]);
                sw.WriteLine(21);
                sw.WriteLine(yP[i + 1]);
                sw.WriteLine(31);
                sw.WriteLine(z[i + 1]);
            }
        }

        //路灯
        public void LD(int Color, double x, double y, double z, double LdSize)
        {
            sw.WriteLine(0);
            sw.WriteLine("Circle");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(Color);
            sw.WriteLine(10);
            sw.WriteLine(x);
            sw.WriteLine(20);
            sw.WriteLine(y);
            sw.WriteLine(40);
            sw.WriteLine(LdSize / 4);

            sw.WriteLine(0);
            sw.WriteLine("LINE");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(Color);
            sw.WriteLine(10);
            sw.WriteLine(x);
            sw.WriteLine(20);
            sw.WriteLine(y + LdSize / 4);
            sw.WriteLine(11);
            sw.WriteLine(x);
            sw.WriteLine(21);
            sw.WriteLine(y + 1.5 * LdSize);

            double x1, y1, x2, y2;

            x1 = x - LdSize / 2;
            y1 = y + 1.5 * LdSize;
            x2 = x + LdSize / 2;
            y2 = y + 1.5 * LdSize;

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
            sw.WriteLine(11);
            sw.WriteLine(x2);
            sw.WriteLine(21);
            sw.WriteLine(y2);

            double x3, y3, x4, y4;

            x3 = x1;
            y3 = y1 - LdSize / 2;

            x4 = x2;
            y4 = y1 - LdSize / 2;

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
            sw.WriteLine(11);
            sw.WriteLine(x3);
            sw.WriteLine(21);
            sw.WriteLine(y3);

            sw.WriteLine(0);
            sw.WriteLine("LINE");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(Color);
            sw.WriteLine(10);
            sw.WriteLine(x2);
            sw.WriteLine(20);
            sw.WriteLine(y2);
            sw.WriteLine(11);
            sw.WriteLine(x4);
            sw.WriteLine(21);
            sw.WriteLine(y4);

            double x5, y5;

            x5 = x3;
            y5 = y3 - LdSize / 4;

            sw.WriteLine(0);
            sw.WriteLine("Circle");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(Color);
            sw.WriteLine(10);
            sw.WriteLine(x5);
            sw.WriteLine(20);
            sw.WriteLine(y5);
            sw.WriteLine(40);
            sw.WriteLine(LdSize / 4);

            double x6, y6;
            x6 = x4;
            y6 = y4 - LdSize / 4;

            sw.WriteLine(0);
            sw.WriteLine("Circle");
            sw.WriteLine(8);
            sw.WriteLine("0");
            sw.WriteLine(62);
            sw.WriteLine(Color);
            sw.WriteLine(10);
            sw.WriteLine(x6);
            sw.WriteLine(20);
            sw.WriteLine(y6);
            sw.WriteLine(40);
            sw.WriteLine(LdSize / 4);
        }
    }
}
