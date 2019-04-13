using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Contour_Line
{

    //界面绘图
    class Class_Draw
    {
        public Graphics gp;

        //中心坐标
        public double x_c, y_c;

        //比例系数
        public double t;

        //转换后的坐标
        public List<double> px = new List<double>();
        public List<double> py = new List<double>();


        //计算缩放系数
        public void CalRatio(List<double> x, List<double> y, double w, double h)
        {
            //坐标的四个角点
            double minx, miny, maxx, maxy;

            minx = x.Min();
            miny = y.Min();
            maxx = x.Max();
            maxy = y.Max();

            //计算中心坐标
            x_c = (minx + maxx) / 2;
            y_c = (miny + maxy) / 2;

            //计算缩放系数
            double t1, t2;
            t1 = w / (maxx - minx);
            t2 = h / (maxy - miny);
            if (t1 < t2)
            {
                t = t1;
            }
            else
            {
                t = t2;
            }
        }

        //坐标转换
        public void Change(List<double> x, List<double> y, double w_c, double h_c)
        {
            px.Clear();
            py.Clear();

            for (int i = 0; i < x.Count; i++)
            {
                px.Add(w_c + (x[i] - x_c) * t);
                py.Add(h_c - (y[i] - y_c) * t);
            }
        }

        //画直线
        public void W_line(double x1, double y1, double x2, double y2, Pen pen1)
        {
            PointF p1 = new PointF(float.Parse((x1).ToString()), float.Parse((y1).ToString()));
            PointF p2 = new PointF(float.Parse((x2).ToString()), float.Parse((y2).ToString()));
            gp.DrawLine(pen1, p1, p2);
        }

        //画圆
        public void D_circle(double x, double y, float r)
        {

            gp.FillEllipse(Brushes.Red, float.Parse((x - r).ToString()), float.Parse((y - r).ToString()), 2 * r, 2 * r);
        }


        //画三角形
        public void D_triangle(double x, double y, int k)
        {
            PointF[] p = new PointF[3];
            p[0] = new PointF(float.Parse((x).ToString()), float.Parse((y - k).ToString()));
            p[1] = new PointF(float.Parse((x - k * Math.Sqrt(3) / 2).ToString()), float.Parse((y + k / 2).ToString()));
            p[2] = new PointF(float.Parse((x + k * Math.Sqrt(3) / 2).ToString()), float.Parse((y + k / 2).ToString()));
            gp.FillPolygon(Brushes.Red, p);
        }

        //画文字
        public void D_text(double x, double y, string a,Font ft)
        {
            gp.DrawString(a, ft, Brushes.Red, float.Parse(x.ToString()), float.Parse(y.ToString()));
        }

        //画曲线
        public void D_Curve(List<double> x, List<double> y,Pen p1)
        {

            if (x.Count > 1 && x.Count < 4)
            {
                PointF[] P = new PointF[x.Count];

                for (int i = 0; i < x.Count; i++)
                {
                    P[i] = new PointF(float.Parse(x[i].ToString()), float.Parse(y[i].ToString()));
                }

                gp.DrawCurve(p1, P, (float)0.1);
            }
            else
            {

                //调用曲线类
                Class_Spline BSpline = new Class_Spline();

                BSpline.D_BSpline(x, y);

                PointF[] P = new PointF[BSpline.Bspline_X.Count];

                for (int i = 0; i < BSpline.Bspline_X.Count; i++)
                {
                    P[i] = new PointF(float.Parse(BSpline.Bspline_X[i].ToString()), float.Parse(BSpline.Bspline_Y[i].ToString()));
                }

                gp.DrawLines(p1, P);
            }
        }
    }
}
