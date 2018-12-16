using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geodetic_line
{
    class My_Founctions
    {
        /// <summary>
        /// 将dd.mmss转化为弧度
        /// </summary>
        /// <param name="dd">dd.mmss</param>
        /// <returns></returns>
        public double ddmmssTorad(double dd)
        {
            double rad;
            double deg;
            double min;
            double sec;
            deg = (int)(dd);
            min = (int)((dd - deg) * 100);
            sec = dd * 10000 - deg * 10000 - min * 100;
            rad = (deg + min / 60.0 + sec / 3600.0) / 180.0 * Math.PI;
            return rad;
        }
        /// <summary>
        /// 将弧度转为dd.mmss
        /// </summary>
        /// <param name="rad">弧度值</param>
        /// <returns></returns>
        public string radToddmmss(double rad)
        {
            double deg;
            double min;
            double sec;
            string dd;
            rad = rad / Math.PI * 180;
            deg = (int)(rad);
            min = (int)((rad - deg) * 60);
            sec = (int)(rad - deg - min / 60) * 3600;
            dd = deg.ToString("f0") + "." + min.ToString("f0") + Math.Floor(sec).ToString("0") + ((sec - Math.Floor(sec)) * 10000).ToString("f0");
            return dd;
        }
    }
}
