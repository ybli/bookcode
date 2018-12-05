using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiData
{
    /// <summary>
    /// 主要算法：时间转换
    /// </summary>
    class Algo
    {
        public static double Mjd(int year, int month, int day, int hour, int min, int sec, int timeZone)
        {
            double mjd = -678987 + 367.0*year;
            mjd -= Convert.ToInt32(7.0/4.0*(year + Convert.ToInt32((month + 9.0)/12.0)));
            mjd += Convert.ToInt32((275.0*month)/9.0);
            mjd += day + (hour - timeZone)/24.0 + min/1440.0+sec/86400.0;
            return mjd;
        }
    }
}
