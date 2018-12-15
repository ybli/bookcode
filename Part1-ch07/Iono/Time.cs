using System;

namespace Iono
{
    public class Time//定义时间
    {
        #region 1-Field

        private DateTime _date = DateTime.Now;//获取现在的时间
        private double _mjd;//定义儒略日
        private double _second;//定义秒

        private const double Tolerance = 1.0e-8;

        #endregion
         
        #region 2-Constructor
        public Time(double mjd)
        {
            Init(mjd);
        }

        public Time(int year, int month, int day, int hour, int minute, double second)
        {
            Init2(year, month, day, hour, minute, second);
        }
        public Time(int year, int month, int day)
        {
            Init2(year, month, day, 0, 0, 0);
        }

        public Time(int year, double doy)//年份，年积日
        {
            Init2(year, 1, 1, 0, 0, 0);
            AddDays(doy);
        }

        private void Init(double mjd)
        {
            _mjd = mjd;
            int year, month, day, hour, minute, second;
            ConvertJdtoCalendar(mjd, out year, out month, out day, out hour, out minute, out _second);
            second = (int)Math.Floor(_second);
            if (second < 0) second = 0;
            
            _date = new DateTime(year, month, day, hour, minute, second);
        }

        private void Init2(int year, int month, int day, int hour, int minute, double second)
        {
            int iSecond = (int)Math.Floor(second);
            _date = new DateTime(year, month, day, hour, minute, iSecond);
            _second = second;
            _mjd = ConvertCalendarToMjd(year, month, day, hour, minute, second);
        }
        #endregion

        #region 3-Property

        public double Mjd
        {
            set
            {
                 _mjd = value;
                Init(value);
            }
            get
            {
                return _mjd;
            }
        }

        public int Year
        {
            get { return _date.Year; }
        }

        public int YearShort
        {
            get
            {
                int year = _date.Year;
                if (year >= 2000)
                {
                    return year - 2000;
                }
                else
                {
                    return year - 1900;
                }
            }
        }
        public int Month
        {
            get { return _date.Month; }
        }

        public int Day
        {
            get { return _date.Day; }
        }

        public int Hour
        {
            get { return _date.Hour; }
        }

        public int Minute
        {
            get { return _date.Minute; }
        }

        public double Second
        {
            get { return _second; }
        }

        public double Jd
        {
            get { return _mjd + C.MJD_TO_JD; }
            set
            {
                _mjd = value - C.MJD_TO_JD;
                Init(_mjd);
            }
        }

        //Days of year
        public int Doy
        {
            get { return _date.DayOfYear; }
        }

        //Days of week
        public int Dow
        {
            get
            {
                int dow = 0;
                switch (_date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dow = 0; break;
                    case DayOfWeek.Monday:
                        dow = 1; break;
                    case DayOfWeek.Tuesday:
                        dow = 2; break;
                    case DayOfWeek.Wednesday:
                        dow = 3; break;
                    case DayOfWeek.Thursday:
                        dow = 4; break;
                    case DayOfWeek.Friday:
                        dow = 5; break;
                    case DayOfWeek.Saturday:
                        dow = 6; break;
                }
                return dow;
            }
        }

        //Seconds of a day
        public double SecondOfDay
        {
            get
            {
                return Hour * 3600 + Minute * 60 + Second;
            }
        }

        //Seconds of a week
        public double SecondOfWeek
        {
            get
            {
                double result = Dow * C.SEC_PER_DAY + SecondOfDay;
                return result;
            }
        }
        public int GPSWeek
        {
            get
            {
                double days = _mjd - C.GPS_EPOCH_MJD;
                int week = (int)days / 7;
                return week;
            }
        }

        //Weeks since Beidou Epoch
        public int BDSWeek
        {
            get
            {
                double days = _mjd - C.BDS_EPOCH_MJD;
                return (int)days / 7;
            }
        }

        //Weeks since  Galileo Epoch
        public int GALWeek
        {
            get
            {
                double days = _mjd - C.GAL_EPOCH_MJD;
                return (int)days / 7;
            }
        }

        public int QZSWeek
        {
            get
            {
                double days = _mjd - C.QZS_EPOCH_MJD;
                return (int)days / 7;
            }
        }

        #endregion

        #region 4-operator
        public static bool operator ==(Time left, Time right)
        {
            if (Math.Abs(left.Mjd - right.Mjd) < Tolerance)
                return true;
            return false;
        }

        public static bool operator !=(Time left, Time right)
        {
            return !(left == right);
        }

        public static bool operator <(Time left, Time right)
        {
            if (left.Mjd < right.Mjd)
                return true;
            return false;
        }

        public static bool operator >(Time left, Time right)
        {
            if (left.Mjd > right.Mjd)
                return true;
            return false;
        }


        public static bool operator <=(Time left, Time right)
        {
            if (!(left.Mjd > right.Mjd))
                return true;
            return false;
        }

        public static bool operator >=(Time left, Time right)
        {
            if (!(left.Mjd < right.Mjd))
                return true;
            return false;
        }
        //返回值以秒（second）为单位
        public static double operator -(Time left, Time right)
        {
            return (left.Mjd - right.Mjd)*C.SEC_PER_DAY;
        }
        #endregion

        #region 5-Add
        public void AddYears(int year)
        {
            _date = _date.AddYears(year);
            Init2(Year, Month, Day, Hour, Minute, Second);
        }

        public void AddMonths(int month)
        {
            _date = _date.AddMonths(month);
            Init2(Year, Month, Day, Hour, Minute, Second);
        }

        public void AddDays(double day)
        {
            _mjd += day;
            Init(_mjd);
        }

        public void AddHours(double hour)
        {
            _mjd += hour / 24.0;
            Init(_mjd);
        }

        public void AddMinutes(double minute)
        {
            _mjd += minute / 1440.0;
            Init(_mjd);
        }

        public void AddSeconds(double second)
        {
            _mjd += second / C.SEC_PER_DAY;
            Init(_mjd);
        }
        #endregion

        #region 6-Set

        public void SetYMDHMS(int year, int month, int day, int hour, int minute, double second)
        {
            Init2(year, month, day, hour, minute, second);
        }

        public void SetGPSWeekSecond(int week, double sow)
        {
            double days = week * 7 + sow / C.SEC_PER_DAY;
            double mjd = C.GPS_EPOCH_MJD + days;
            Init(mjd);
        }

        #endregion

        #region 7-ToString

        public string ToYmdString()
        {
            var result = string.Format("{0} {1:00} {2:00}", Year, Month, Day);
            return result;
        }

        public string ToHmsString()
        {
            var result = string.Format("{0:00}:{1:00}:{2:00.000000}", Hour, Minute, Second);
            return result;
        }

        public string ToYmdHmsString()
        {
            var result = string.Format("{0} {1:00} {2:00} ", Year, Month, Day);
            result += string.Format("{0:00}:{1:00}:{2:00.000000}", Hour, Minute, Second);
            return result;
        }

        public static string FileName()
        {
            return "File:Time.cs";
        }

        #endregion


        #region 10-TimeConvert
        //   Routine to convert a calender date with hours, minutes and
        //   seconds to a Modified Julian date. The calender date is
        //   ordered as year, month, day of month, hours, and
        //   minutes. 
        public static double ConvertCalendarToMjd(int year, int month, int day, int hour,
            int minute, double second)//将日历格式转化为儒略日的格式
        {
            if (year < 50)
                year += 2000;
            else if (year>=50 && year < 100)
                year += 1900;

            if (month <= 0 || month > 12)
            {
                string ex = "Invalid Month  in method: ConvertCalendarToMJD, in " + FileName();
                throw new Exception(ex);
            }

            var date = new DateTime(year, month, day);
            var date2000 = new DateTime(2000, 1, 1);

            TimeSpan dt = date - date2000;
            int daysFrom2000 = dt.Days;

            double fraction = second /C.SEC_PER_DAY + minute / 1440.0 + hour / 24.0;
            double mjd = 51544.0 + daysFrom2000 + fraction;

            return mjd;
        }

       
        // Routine to convert a  Modified Julian date (with fractions of a day)
        // to a calender data with hours and minutes, and seconds 
        public static void ConverMjdtotCalendar(double mjd, out int year, out int month, out int day, out  int hour,
            out int minute, out double second)//将儒略日转化为时分秒的日历格式
        {
            var date2000 = new DateTime(2000, 1, 1, 0, 0, 0);
            double mjd2000 = 51544.0;
            double daysAfter2000 = mjd - mjd2000;
            var date = date2000.AddDays(daysAfter2000);
            year = date.Year;
            month = date.Month;
            day = date.Day;
            hour = date.Hour;
            minute = date.Minute;
            second = 86400.0 * (mjd - Math.Floor(mjd)) - hour * 3600.0 - minute * 60.0;
        }

        //Routine to convert a calender date with hours, minutes and
        //seconds to a Julian date. The calender date is
        //ordered as year, month, day of month, hours, and
        //minutes, and seconds
        public static double ConvertCalendarToJd(int year, int month, int day, int hour,
            int minute, double second)
        {
            double mjd = ConvertCalendarToMjd(year, month, day, hour, minute, second);
            double jd = mjd + C.MJD_TO_JD;
            return jd;
        }

        // Routine to convert a  (Modified) Julian date (with fractions of a day)
        // to a calender data with hours and minutes, and seconds 
        public static void ConvertJdtoCalendar(double jd, out int year, out int month, out int day, out int hour,
            out int minute, out double second)
        {
            double mjd = 0;
            if (jd > C.MJD_TO_JD)
                mjd = jd - C.MJD_TO_JD;
            else
                mjd = jd;
            ConverMjdtotCalendar(jd, out year, out month, out day, out hour, out minute, out second);
        }

        // These two routines convert 'integer JD' and calendar time; they were
        // derived from Sinnott, R. W. "Bits and Bytes" Sky & Telescope Magazine,
        // Vol 82, p. 183, August 1991, and The Astronomical Almanac, published
        // by the U.S. Naval Observatory.
        // NB range of applicability of this routine is from 0JD (4713BC)
        // to approx 3442448JD (4713AD).
        public static void ConvertJDtoCalendar(double jd,
                                out  int year, out  int month, out  int day)
        {
            int hour, min;
            double sec;
            ConvertJdtoCalendar(jd, out year, out month, out day, out hour, out min, out sec);
        }

        public static double ConvertCalendarToJD(int year, int month, int day)
        {
            return ConvertCalendarToJd(year, month, day, 0, 0, 0.0);
        }

        public static void ConvertSODtoTime(double sod,
                               out int hh, out int mm, out double sec)
        {
            // Get us to within one day.
            if (sod < 0)
            {
                sod += (1 + Convert.ToInt64(sod / C.SEC_PER_DAY)) * C.SEC_PER_DAY;
            }
            else if (sod >= C.SEC_PER_DAY)
            {
                sod -= Convert.ToInt64(sod / C.SEC_PER_DAY) * C.SEC_PER_DAY;
            }

            long seconds = (long)Math.Floor(sod); // get  a real integer
            sod = sod - seconds;   // sod holds the fraction

            hh = (int)Math.Floor(seconds / 3600.0);
            mm = (int)Math.Floor((seconds % 3600) / 60.0);
            sec = Convert.ToDouble((seconds % 60) + sod);

        }

        public static double ConvertTimeToSOD(int hour,
                                   int minute, double second)
        {
            return (second + 60.0 * (minute + 60.0 * hour));
        }
        #endregion


        /// Function to convert from UTC to sidereal time
        //  @param t         Epoch
        // 
        //  @return sidereal time in hours.
        public static double UTC2SID(Time time)
        {

            // Hours of day (decimal)
            double h = time.SecondOfDay / 3600.0;

            // Fraction of day
            double frofday = time.SecondOfDay / 86400.0;

            // Compute Julian Day, including decimals
            double jd = time.Jd;

            // Temporal value, in centuries
            double tt = (jd - 2451545.0) / 36525.0;

            double sid = (24110.54841 + tt * ((8640184.812866) +
                        tt * ((0.093104) - (6.2e-6 * tt))));

            sid = sid / 3600.0 + h;
            sid = MiscMath.Fmod(sid, 24.0);

            if (sid < 0.0)
            {
                sid += 24.0;
            }
            return sid;
        }

        public static void Example()
        {
           // double mjd = 53736;
            //var time = new Time(mjd);
            var time = new Time(2014, 1, 1, 0, 0, 0.0);

            Console.WriteLine(time.ToYmdHmsString());
        }

    }
}
