using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP
{
    public class time
    {
        public double[] calend = new double[6];//格里高利历 yyyy年月日，时分秒.
        public int weeks;//GPS周
        public double tow;//GPS周内秒
        public double gpsec;
        public double utc = 0;
        public double ut1 = 0;
        public double jd;
        static double[][] leaps = new double[19][]{ /* leap seconds (y,m,d,h,m,s,utc-gpst) */
                                   new double[7] {2017,1,1,0,0,0,-18},
                                   new double[7] {2015,7,1,0,0,0,-17},
                                   new double[7] {2012,7,1,0,0,0,-16},
                                   new double[7] {2009,1,1,0,0,0,-15},
                                   new double[7] {2006,1,1,0,0,0,-14},
                                   new double[7] {1999,1,1,0,0,0,-13},
                                   new double[7] {1997,7,1,0,0,0,-12},
                                   new double[7] {1996,1,1,0,0,0,-11},
                                   new double[7] {1994,7,1,0,0,0,-10},
                                   new double[7] {1993,7,1,0,0,0, -9},
                                   new double[7] {1992,7,1,0,0,0, -8},
                                   new double[7] {1991,1,1,0,0,0, -7},
                                   new double[7] {1990,1,1,0,0,0, -6},
                                   new double[7] {1988,1,1,0,0,0, -5},
                                   new double[7] {1985,7,1,0,0,0, -4},
                                   new double[7]  {1983,7,1,0,0,0, -3},
                                   new double[7]  {1982,7,1,0,0,0, -2},
                                   new double[7]  {1981,7,1,0,0,0, -1},
                                   new double[7]  {0,0,0,0,0,0,0}
                        };
        public time()
        {
            for (int i = 0; i < 6; i++)
                calend[i] = 0;
            tow = 0;
            jd = 0;
            gpsec = 0;
            utc = 0;
        }

        public time(double[] ep)
        {
            if (ep[0] < 100) ep[0] += 2000;
            for (int i = 0; i < ep.Length; i++)
                calend[i] = ep[i];
            cal2gpst(ep);
        }

        public time(string s, string type)
        {
            string[] str = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string type1 = type.Trim().ToLower();
            switch (type1)
            {
                case "o":
                    for (int i = 0; i < str.Length; i++)
                        calend[i] = double.Parse(str[i]);
                    calend[0] += 2000;
                    cal2gpst(calend);
                    break;
                case "sp3":
                    for (int i = 1; i < str.Length; i++)
                        calend[i - 1] = double.Parse(str[i]);
                    cal2gpst(calend);
                    break;
                case "clk":
                    for (int i = 2; i <= 7; i++)
                        calend[i - 2] = double.Parse(str[i]);
                    cal2gpst(calend);
                    break;
                case "n":
                    for (int i = 0; i < str.Length; i++)
                        calend[i] = double.Parse(str[i]);
                    calend[0] += 2000;
                    cal2gpst(calend);
                    break;
                case "atx":
                    for (int i = 0; i < str.Length; i++)
                        calend[i] = double.Parse(str[i]);
                    cal2gpst(calend);
                    break;
            }
        }


        public void Caltime2jd(double[] t)
        {
            if (t[1] <= 2)
            {
                jd = Math.Floor(365.25 * (t[0] - 1)) + Math.Floor(30.6001 * (t[1] + 12 + 1)) + t[2] + t[3] / 24 + t[4] / 1440 + t[5] / 86400 + 1720981.5;
            }
            else jd = Math.Floor(365.25 * t[0]) + Math.Floor(30.6001 * (t[1] + 1)) + t[2] + t[3] / 24 + t[4] / 1440 + t[5] / 86400 + 1720981.5;
        }

        public void cal2gpst(double[] t)
        {
            int[] doy = { 1, 32, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };
            int days, year, month, day;
            year = (int)t[0]; month = (int)t[1]; day = (int)t[2];
            days = (year - 1980) * 365 + (year - 1977) / 4 + doy[month - 1] + day - 7 + (year % 4 == 0 && month >= 3 ? 1 : 0);
            weeks = (int)days / 7;
            tow = (days % 7) * 86400 + t[3] * 3600 + t[4] * 60 + t[5];
            gpsec = weeks * 604800 + tow;
        }

        public void gpst2utc()
        {
            int i;
            double tu = 0;
            for (i = 0; leaps[i][0] > 0; i++)
            {
                double[] ep = new double[6];
                time t1 = new time();
                for (int j = 0; j < 6; j++)
                    ep[j] = leaps[i][j];
                t1 = new time(ep);
                tu = gpsec + leaps[i][6];
                if ((tu - t1.gpsec) > 0) break;
            }
            utc = tu;
        }

        public static double gpst2utc(double t)
        {
            int i;
            double tu = 0;
            for (i = 0; leaps[i][0] > 0; i++)
            {
                double[] ep = new double[6];
                time t1 = new time();
                for (int j = 0; j < 6; j++)
                    ep[j] = leaps[i][j];
                t1 = new time(ep);
                tu = t + leaps[i][6];
                if ((tu - t1.gpsec) > 0) break;
            }
            return tu;
        }

        public static double utc2gmst(time t, double ut1_utc)
        {
            double[] ep2000 = new double[6] { 2000, 1, 1, 12, 0, 0 }, ep = new double[6];
            double[] tut0 = new double[6];
            double tut;
            double ut, t1, t2, t3, gmst0, gmst;


            tut = t.utc + ut1_utc;
            //time.time2calend(t, ep);
            time2calend(tut, ep);
            //ut = t.calend[3] * 3600.0 + t.calend[4] * 60.0 + t.calend[5] + t.utc - t.gpsec;//ut表示秒
            ut = ep[3] * 3600.0 + ep[4] * 60.0 + ep[5];
            tut0[0] = ep[0]; tut0[1] = ep[1]; tut0[2] = ep[2];
            time tut0_ = new time(tut0);//tu0_将t的 年 月 日化成秒，以GPS时为时间基准
            time tep = new time(ep2000);
            t1 = (tut0_.gpsec - tep.gpsec) / 86400.0 / 36525.0;

            t2 = t1 * t1; t3 = t2 * t1;
            gmst0 = 24110.54841 + 8640184.812866 * t1 + 0.093104 * t2 - 6.2E-6 * t3;
            gmst = gmst0 + 1.002737909350795 * ut;

            return (gmst % 86400.0) * Math.PI / 43200.0; /* 0 <= gmst <= 2*PI */
        }

        public static void time2calend(double t, double[] ep)
        {
            int[] mday = new int[48]{ /* # of days in a month */
        31,29,31,30,31,30,31,31,30,31,30,31,31,28,31,30,31,30,31,31,30,31,30,31,
        31,28,31,30,31,30,31,31,30,31,30,31,31,28,31,30,31,30,31,31,30,31,30,31
            };
            int days, sec, mon, day;
            double tt = Math.Floor(t), tsec;
            tsec = t - tt;
            /* leap year if year%4==0 in 1901-2099 */
            days = (int)(tt / 86400) + 5;
            sec = (int)(tt - (days - 5) * 86400);
            for (day = days % 1461, mon = 0; mon < 48; mon++)
            {
                if (day >= mday[mon]) day -= mday[mon]; else break;
            }
            ep[0] = 1980 + days / 1461 * 4 + mon / 12; ep[1] = mon % 12 + 1; ep[2] = day + 1;
            ep[3] = sec / 3600; ep[4] = (sec % 3600 / 60); ep[5] = sec % 60 + tsec;
        }
    }

    public class rtktime
    {
        public Int64 time_int;
        public double sec;
    }
}
