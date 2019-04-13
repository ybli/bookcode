using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapIcon
{
    //符号库
    class Symbol_Picture
    {
        public Graphics gp;

        //一般房屋
        public void D_FW(Pen p, List<double> x, List<double> y)
        {
            PointF[] P = new PointF[x.Count];

            for (int i = 0; i < x.Count; i++)
            {
                PointF p1 = new PointF(float.Parse((x[i]).ToString()), float.Parse((y[i]).ToString()));
                P[i] = p1;
            }

            gp.DrawPolygon(p, P);
        }

        //砼房屋
        public void T_FW(Pen p, List<double> x, List<double> y,string code,Brush Bs)
        {
            PointF[] P = new PointF[x.Count];

            for (int i = 0; i < x.Count; i++)
            {
                PointF p1 = new PointF(float.Parse((x[i]).ToString()), float.Parse((y[i]).ToString()));
                P[i] = p1;
            }

            gp.DrawPolygon(p, P);

            double x_c, y_c;

            x_c = (x.Max() + x.Min()) / 2;
            y_c = (y.Max() + y.Min()) / 2;

            double  d;

            if ((y.Max() - y.Min()) > (x.Max() - x.Min()))
            {
                d = (y.Max() - y.Min()) / 8;
            }
            else
            {
                d = (x.Max() - x.Min()) / 8;
            }

            string[] a = new string[2];
            a = code.Split('-');

            string A = "砼" + a[1];

            Font ft = new Font("宋体", (float)d, FontStyle.Regular);

            gp.DrawString(A, ft, Bs, float.Parse(x_c.ToString()), float.Parse(y_c.ToString()));
        }

        //小路
        public void XL(Pen p, List<double> x, List<double> y)
        {
            PointF[] P = new PointF[x.Count];

            for (int i = 0; i < x.Count; i++)
            {
                PointF p1 = new PointF(float.Parse((x[i]).ToString()), float.Parse((y[i]).ToString()));
                P[i] = p1;
            }

            gp.DrawLines(p, P);
        }

        //路灯
        public void LD(Pen p,double x,double y,double LdSize)
        {
            gp.DrawEllipse(p, (float)(x - LdSize / 4), (float)(y - LdSize / 4), (float)(LdSize / 2), (float)(LdSize / 2));
            gp.DrawLine(p, (float)(x), (float)(y - LdSize / 4), (float)(x), (float)(y - 1.5 * LdSize));

            double x1, y1, x2, y2;

            x1 = x - LdSize / 2;
            y1 = y - 1.5 * LdSize;
            x2 = x + LdSize / 2;
            y2 = y - 1.5 * LdSize;
            gp.DrawLine(p, (float)x1, (float)y1, (float)x2, (float)y2);

            double x3, y3, x4, y4;

            x3 = x1;
            y3 = y1 + LdSize / 2;

            x4 = x2;
            y4 = y1 + LdSize / 2;

            gp.DrawLine(p, (float)x1, (float)y1, (float)x3, (float)y3);
            gp.DrawLine(p, (float)x2, (float)y2, (float)x4, (float)y4);

            double x5, y5;

            x5 = x3 - LdSize / 4;
            y5 = y3;

            gp.DrawEllipse(p, (float)x5, (float)y5, (float)(LdSize / 2), (float)(LdSize / 2));

            double x6, y6;
            x6 = x4 - LdSize / 4;
            y6 = y4;

            gp.DrawEllipse(p, (float)x6, (float)y6, (float)(LdSize / 2), (float)(LdSize / 2));
        }

        //公路
        public void GL(Pen p,List<double>x,List<double>y, string Code,double Scale)
        {
            double k = 0;
            string[] A = new string[2];
            double a = 0;
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            PointF[] P = new PointF[x.Count];
            PointF[] P1 = new PointF[x.Count];

            for (int i = 0; i < x.Count; i++)
            {
                PointF p1 = new PointF(float.Parse((x[i]).ToString()), float.Parse((y[i]).ToString()));
                P1[i] = p1;
            }

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

            k *= Scale;

            //起点
            x1 = x[0]; y1 = y[0]; x2 = x[1]; y2 = y[1];
            a = Math.Atan(Math.Abs((y2 - y1) / (x2 - x1)));
            if (((y2 - y1) / (x2 - x1)) < 0)
            {
                PointF p3 = new PointF(float.Parse((x1 + k * Math.Sin(a)).ToString()), float.Parse((y1 + k * Math.Cos(a)).ToString()));
                P[0] = p3;
            }
            else
            {
                PointF p3 = new PointF(float.Parse((x1 - k * Math.Sin(a)).ToString()), float.Parse((y1 + k * Math.Cos(a)).ToString()));
                P[0] = p3;
            }

            //终点
            x1 = x[x.Count - 2]; y1 = y[x.Count - 2]; x2 = x[x.Count - 1]; y2 = y[x.Count - 1];
            a = Math.Atan(Math.Abs((y2 - y1) / (x2 - x1)));
            if (((y2 - y1) / (x2 - x1)) < 0)
            {
                PointF p4 = new PointF(float.Parse((x2 + k * Math.Sin(a)).ToString()), float.Parse((y2 + k * Math.Cos(a)).ToString()));
                P[x.Count - 1] = p4;
            }
            else
            {
                PointF p4 = new PointF(float.Parse((x2 - k * Math.Sin(a)).ToString()), float.Parse((y2 + k * Math.Cos(a)).ToString()));
                P[x.Count - 1] = p4;
            }

            for (int i = 0; i < x.Count - 2; i++)
            {
                x1 = x[i]; y1 = y[i]; x2 = x[i + 1]; y2 = y[i + 1];
                a = Math.Atan(Math.Abs((y1 - y2) / (x1 - x2)));

                PointF pp = new PointF(float.Parse((x2).ToString()), float.Parse((y2 + k / Math.Cos(a)).ToString()));

                P[i + 1] = pp;
            }

            //画第一条线
            gp.DrawLines(p, P1);

            //画第二条线 
            gp.DrawLines(p, P);
        }

        //点号
        public void Text(Brush Bs, double x, double y, Font ft, string a)
        {
            gp.DrawString(a, ft, Bs, float.Parse(x.ToString()), float.Parse(y.ToString()));
        }
    }
}
