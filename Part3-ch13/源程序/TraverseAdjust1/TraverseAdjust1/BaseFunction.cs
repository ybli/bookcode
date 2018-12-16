using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraverseAdjust
{
    /* 功能概要：基础功能函数类
     * 作者：廖振修
     * 创建日期:2016-06-09
     */
    public static class BaseFunction
    {

        // 计算两点距离
        public static double DistAB(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        // 计算方位角
        public static double DirectAB(double x1, double y1, double x2, double y2)
        {
            double directVal = Math.Atan2(y2 - y1, x2 - x1);
            if (directVal < 0) directVal += Math.PI * 2;
            return directVal;
        }

        // DMS转弧度
        public static double DMS2Hu(double dms)
        {
            int d, m;
            double s;
            d =(int) Math.Floor(dms);
            m =(int) Math.Floor((dms - d) * 100);
            s = ((dms - d) * 100 - m) * 100;
            double val;
            val = (d + m / 60.0 + s / 3600.0) * Math.PI / 180;
            return val;
        }

        // DMS转弧度
        public static double DMS2Hu(int d, int m, double s)
        {
            double val;
            val = (d + m / 60.0 + s / 3600.0) * Math.PI / 180;
            return val;
        }

        // 弧度转度分秒,以dd.mmss表示
        public static double Hu2DMS(double hu)
        {
            double d10 = Math.Abs(hu) * 180 / Math.PI; // 十进制度
            int d, m;
            double s;

            d = (int)Math.Floor(d10);
            m =(int) Math.Floor((d10 - d) * 60);
            s = ((d10 - d) * 60 - m) * 60;
            double val = Convert.ToDouble(d.ToString() + "." + m.ToString("00") + s.ToString().Replace(".", ""));
            if (hu < 0) val = -val;
            return val;
        }

    }//endclass
}//endspace
