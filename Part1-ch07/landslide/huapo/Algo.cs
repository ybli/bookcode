using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace landside
{
    public class Algo
    {
        /// <summary>
        /// 计算2点之间的距离
        /// </summary>
        public static double Distance(Coordinate c1, Coordinate c2)
        {
            double dx = c1.X - c2.X;
            double dy = c1.Y - c2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        /// <summary>
        /// 计算速度，同一点在两个不同时刻的位置之间的相对关系
        /// </summary>
        public static double Velocity(Coordinate c1, Coordinate c2)
        {
            double s = Distance(c1, c2) * 1000; //转换为以mm为单位
            double tm = (c2.Tm - c1.Tm) * 5;    //以天为单位

            return s / tm;
        }
        /// <summary>
        /// 计算应变
        /// </summary>
        public static double Strain(Coordinate c11, Coordinate c12, Coordinate c21, Coordinate c22)
        {
            double s1 = Distance(c11, c21);
            double s2 = Distance(c12, c22);
            return (s2 - s1) / s1;
        }
    }
}
