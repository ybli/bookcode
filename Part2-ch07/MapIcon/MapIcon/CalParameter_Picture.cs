using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapIcon
{
    //绘图参数计算
    class CalParameter_Picture
    {
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
    }
}
