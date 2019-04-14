using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP
{
    class rtklibcmn
    {
        static double[] gpst0 = { 1980, 1, 6, 0, 0, 0 }; /* gps time reference */
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
        /* convert calendar day/time to time -------------------------------------------
         * convert calendar day/time to gtime_t struct
         * args   : double *ep       I   day/time {year,month,day,hour,min,sec}
        * return : gtime_t struct
        * notes  : proper in 1970-2037 or 1970-2099 (64bit time_t)
        *-----------------------------------------------------------------------------*/
        public static rtktime epoch2time(double[] ep)
        {
            int[] doy = { 1, 32, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };
            rtktime time = new rtktime();
            int days, sec, year = (int)ep[0], mon = (int)ep[1], day = (int)ep[2];

            if (year < 1970 || 2099 < year || mon < 1 || 12 < mon) return time;

            /* leap year if year%4==0 in 1901-2099 */
            days = (year - 1970) * 365 + (year - 1969) / 4 + doy[mon - 1] + day - 2 + (year % 4 == 0 && mon >= 3 ? 1 : 0);
            sec = (int)Math.Floor(ep[5]);
            time.time_int = (Int64)days * 86400 + (int)ep[3] * 3600 + (int)ep[4] * 60 + sec;
            time.sec = ep[5] - sec;
            return time;
        }
        public static void time2epoch(rtktime t, double[] ep)
        {
            int[] mday ={ /* # of days in a month */
        31,28,31,30,31,30,31,31,30,31,30,31,31,28,31,30,31,30,31,31,30,31,30,31,
        31,29,31,30,31,30,31,31,30,31,30,31,31,28,31,30,31,30,31,31,30,31,30,31
              };
            int days, sec, mon, day;

            /* leap year if year%4==0 in 1901-2099 */
            days = (int)(t.time_int / 86400);
            sec = (int)(t.time_int - (Int64)days * 86400);
            for (day = days % 1461, mon = 0; mon < 48; mon++)
            {
                if (day >= mday[mon]) day -= mday[mon]; else break;
            }
            ep[0] = 1970 + days / 1461 * 4 + mon / 12; ep[1] = mon % 12 + 1; ep[2] = day + 1;
            ep[3] = sec / 3600; ep[4] = sec % 3600 / 60; ep[5] = sec % 60 + t.sec;
        }
        public static double time2gpst(rtktime t, int[] week)
        {
            rtktime t0 = epoch2time(gpst0);
            Int64 sec = t.time_int - t0.time_int;
            int w = (int)(sec / (86400 * 7));

            if (week != null) week[0] = w;
            return (double)(sec - w * 86400 * 7) + t.sec;

        }
        public static rtktime timeadd(rtktime t, double sec)
        {
            double tt;
            rtktime tep = new rtktime();
            tep.time_int = t.time_int;
            tep.sec = t.sec;

            tep.sec += sec;
            tt = Math.Floor(tep.sec);
            tep.time_int += (int)tt;
            tep.sec -= tt;
            return tep;
        }
        public static double timediff(rtktime t1, rtktime t2)
        {
            return t1.time_int - t2.time_int + t1.sec - t2.sec;
        }
        public static rtktime gpst2utc(rtktime t)
        {
            rtktime tu = new rtktime();
            int i;

            for (i = 0; leaps[i][0] > 0; i++)
            {
                tu = timeadd(t, leaps[i][6]);//加上跳秒
                if (timediff(tu, epoch2time(leaps[i])) >= 0.0) return tu;
            }
            return t;
        }
        public static rtktime utc2gpst(rtktime t)
        {
            int i;

            for (i = 0; leaps[i][0] > 0; i++)
            {
                if (timediff(t, epoch2time(leaps[i])) >= 0.0) return timeadd(t, -leaps[i][6]);
            }
            return t;
        }
        /* time to day and sec -------------------------------------------------------*/
        public static double time2sec(rtktime t, rtktime day)
        {
            double[] ep = new double[6];
            double sec;
            time2epoch(t, ep);//转化为 y m d h  m s
            sec = ep[3] * 3600.0 + ep[4] * 60.0 + ep[5];
            ep[3] = ep[4] = ep[5] = 0.0;
            day = epoch2time(ep);
            return sec;
        }
        /* utc to gmst -----------------------------------------------------------------
        * convert utc to gmst (Greenwich mean sidereal time)
        * args   : gtime_t t        I   time expressed in utc
        *          double ut1_utc   I   UT1-UTC (s)
        * return : gmst (rad)
        *-----------------------------------------------------------------------------*/
        public static double utc2gmst(rtktime t, double ut1_utc)
        {
            double[] ep2000 = { 2000, 1, 1, 12, 0, 0 };
            rtktime tut = new rtktime(), tut0 = new rtktime();
            double ut, t1, t2, t3, gmst0, gmst;

            tut = timeadd(t, ut1_utc);//这里的t是UTC表示
            ut = time2sec(tut, tut0);
            t1 = timediff(tut0, epoch2time(ep2000)) / 86400.0 / 36525.0;
            t2 = t1 * t1; t3 = t2 * t1;
            gmst0 = 24110.54841 + 8640184.812866 * t1 + 0.093104 * t2 - 6.2E-6 * t3;
            gmst = gmst0 + 1.002737909350795 * ut;

            return (gmst % 86400.0) * Math.PI / 43200.0; /* 0 <= gmst <= 2*PI   fmod函数在c#中可用%代替*/
        }
        /*time to day of year*/
        public static double time2doy(rtktime t)
        {
            double[] ep = new double[6];
            time2epoch(t, ep);
            ep[1] = ep[2] = 1.0; ep[3] = ep[4] = ep[5] = 0.0;
            return timediff(t, epoch2time(ep)) / 86400.0 + 1.0;
        }

        public static void str2time(string line, rtktime t)
        {
            double[] ep = new double[6];
            string[] s = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 6; i++)
                ep[i] = double.Parse(s[i]);
            if (ep[0] < 100.0) ep[0] += ep[0] < 80.0 ? 2000.0 : 1900.0;
            rtktime t1 = epoch2time(ep);
            t.time_int = t1.time_int; t.sec = t1.sec;
        }

        public static rtktime gpst2time(int week, double sec)
        {
            rtktime t = epoch2time(gpst0);

            if (sec < -1E9 || 1E9 < sec) sec = 0.0;
            t.time_int += 86400 * 7 * week + (int)sec;
            t.sec = sec - (int)sec;
            return t;
        }
        public static rtktime adjweek(rtktime t, rtktime t0)
        {
            double tt = timediff(t, t0);//返回这两个时间所相差的秒数，t.sec是表示小于1秒的小数（double）,time_t是整形
            if (tt < -302400.0) return timeadd(t, 604800.0);
            if (tt > 302400.0) return timeadd(t, -604800.0);
            return t;
        }
    }
}
