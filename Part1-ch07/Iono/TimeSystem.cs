using System;
using System.Collections.Generic;

namespace Iono
{
    /// list of time systems supported by this class
    public enum Systems
    {
        // add new systems BEFORE count, then
        // *** add to Strings[] in TimeSystem.cpp and make parallel to this enum. ***
        // Unknown MUST BE FIRST, and must = 0
        Unknown = 0, ///< unknown time frame; for legacy code compatibility
        Any,         ///< wildcard; allows comparison with any other type
        GPS,         ///< GPS system time
        GLO,         ///< GLONASS system time
        GAL,         ///< Galileo system time
        QZS,         ///< QZSS system Time
        BDT,         ///< BeiDou system Time
        UTC,         ///< Coordinated Universal Time (e.g., from NTP)
        TAI,         ///< International Atomic Time
        TT,          ///< Terrestrial time (used in IERS conventions)
        TDB,         ///< Barycentric dynamical time (JPL ephemeris); very near TT
        // count MUST BE LAST
        count        ///< the number of systems - not a system
    }
    /// This class encapsulates time systems, including string I/O. This is an
    /// example of a 'smart enum' class.
    public class TimeSystem
    {

        /// time system (= element of Systems enum) for this object
        Systems System { set; get; }

        public TimeSystem()
        {
            System = Systems.Unknown;
        }
        /// Constructor, including empty constructor
        public TimeSystem(Systems sys)
        {
            if (sys < Systems.Unknown || sys >= Systems.count)
                System = Systems.Unknown;
            else
                System = sys;
        }
        /// constructor from int
        public TimeSystem(int system)
        {
            System = Num2System(system);
        }

        public TimeSystem(string system)
        {
            System = Str2System(system);
        }
        private Systems Str2System(string str)
        {
            var result = Systems.Unknown;
            str = str.ToUpper();
            switch (str)
            {
                case "ANY":
                    result = Systems.Any; break;
                case "GPS":
                    result = Systems.GPS; break;
                case "GLO":
                    result = Systems.GLO; break;
                case "GAL":
                    result = Systems.GAL; break;
                case "QZS":
                    result = Systems.QZS; break;
                case "BDT":
                    result = Systems.BDT; break;
                case "UTC":
                    result = Systems.UTC; break;
                case "TAI":
                    result = Systems.TAI; break;
                case "TT":
                    result = Systems.TT; break;
                case "TDB":
                    result = Systems.TDB; break;
            }
            return result;
        }
        private string Systems2Str(Systems sys)
        {
            string result = "UNK";
            switch (sys)
            {
                case Systems.Any:
                    result = "ANY"; break;
                case Systems.GPS:
                    result = "GPS"; break;
                case Systems.GLO:
                    result = "GLO"; break;
                case Systems.GAL:
                    result = "GAL"; break;
                case Systems.QZS:
                    result = "QZS"; break;
                case Systems.BDT:
                    result = "BDT"; break;
                case Systems.UTC:
                    result = "UTC"; break;
                case Systems.TAI:
                    result = "TAI"; break;
                case Systems.TT:
                    result = "TT"; break;
                case Systems.TDB:
                    result = "TDB"; break;
            }
            return result;
        }
        private int Systems2Num(Systems sys)
        {
            int result = 0;
            switch (sys)
            {
                case Systems.Any:
                    result = 1; break;
                case Systems.GPS:
                    result = 2; break;
                case Systems.GLO:
                    result = 3; break;
                case Systems.GAL:
                    result = 4; break;
                case Systems.QZS:
                    result = 5; break;
                case Systems.BDT:
                    result = 6; break;
                case Systems.UTC:
                    result = 7; break;
                case Systems.TAI:
                    result = 8; break;
                case Systems.TT:
                    result = 9; break;
                case Systems.TDB:
                    result = 10; break;
            }
            return result;
        }

        private Systems Num2System(int num)
        {
            var result = Systems.Unknown;
            switch (num)
            {
                case 1:
                    result = Systems.Any; break;
                case 2:
                    result = Systems.GPS; break;
                case 3:
                    result = Systems.GLO; break;
                case 4:
                    result = Systems.GAL; break;
                case 5:
                    result = Systems.QZS; break;
                case 6:
                    result = Systems.BDT; break;
                case 7:
                    result = Systems.UTC; break;
                case 8:
                    result = Systems.TAI; break;
                case 9:
                    result = Systems.TT; break;
                case 10:
                    result = Systems.TDB; break;
            }
            return result;
        }

        /// boolean operator==
        public static bool operator ==(TimeSystem left, TimeSystem right)
        {
            return left.System == right.System;
        }

        public static bool operator !=(TimeSystem left, TimeSystem right)
        {
            return !(left == right);
        }

        /// boolean operator< (used by STL to sort)
        public static bool operator <(TimeSystem left, TimeSystem right)
        {
            return left.System < right.System;
        }

        public static bool operator >(TimeSystem left, TimeSystem right)
        {
            return left.System > right.System;
        }

        /// boolean operator>=
        public static bool operator >=(TimeSystem left, TimeSystem right)
        {
            return !(left < right);

        }

        public static bool operator <=(TimeSystem left, TimeSystem right)
        {
            return !(left > right);
        }


        public string ToString()
        {
            string result = Systems2Str(System);
            return result;
        }

        // epoch year, epoch month(1-12), delta t(sec), rate (sec/day) for [1960,1972).
        struct YMDR
        {
            public int year, month;
            public double delt, rate;

            public YMDR(int y, int m, double d, double r)
            {
                year = y;
                month = m;
                delt = d;
                rate = r;
            }
        }
        // Leap seconds history
        // ***** This table must be updated for new leap seconds **************
        struct YMN
        {
            public int year, month, nleap;

            public YMN(int y, int m, int n)
            {
                year = y;
                month = m;
                nleap = n;
            }
        }


        // NB. The table 'leaps' must be modified when a new leap second is announced.
        // Return the number of leap seconds between UTC and TAI, that is the
        // difference in time scales UTC-TAI at an epoch defined by (year, month, day).
        // NB. Input day in a floating quantity and thus any epoch may be represented;
        // this is relevant the period 1960 to 1972, when UTC-TAI was not integral.
        // NB. GPS = TAI - 19sec and so GPS-UTC = getLeapSeconds()-19.
        double GetLeapSeconds(int year, int month, double day)
        {
            // Leap second data --------------------------------------------------------
            // number of changes before leap seconds (1960-1971) - this should never change.
            const int NPRE = 14;

            List<YMDR> preleap = new List<YMDR>(NPRE);

            preleap[0] = new YMDR(1960, 1, 1.4178180, 0.0012960);
            preleap[1] = new YMDR(1961, 1, 1.4228180, 0.0012960);
            preleap[2] = new YMDR(1961, 8, 1.3728180, 0.0012960);
            preleap[3] = new YMDR(1962, 1, 1.8458580, 0.0011232);
            preleap[4] = new YMDR(1963, 11, 1.9458580, 0.0011232);
            preleap[5] = new YMDR(1964, 1, 3.2401300, 0.0012960);
            preleap[6] = new YMDR(1964, 4, 3.3401300, 0.0012960);
            preleap[7] = new YMDR(1964, 9, 3.4401300, 0.0012960);
            preleap[8] = new YMDR(1965, 1, 3.5401300, 0.0012960);
            preleap[9] = new YMDR(1965, 3, 3.6401300, 0.0012960);
            preleap[10] = new YMDR(1965, 7, 3.7401300, 0.0012960);
            preleap[11] = new YMDR(1965, 9, 3.8401300, 0.0012960);
            preleap[12] = new YMDR(1966, 1, 4.3131700, 0.0025920);
            preleap[13] = new YMDR(1968, 2, 4.2131700, 0.0025920);


            List<YMN> leaps = new List<YMN>(){
        new YMN(  1972,  1, 10 ),
        new YMN( 1972,  7, 11 ),
        new YMN( 1973,  1, 12 ),
        new YMN( 1974,  1, 13 ),
        new YMN( 1975,  1, 14 ),
        new YMN( 1976,  1, 15 ),
        new YMN( 1977,  1, 16 ),
        new YMN( 1978,  1, 17 ),
        new YMN( 1979,  1, 18 ),
        new YMN( 1980,  1, 19 ),
        new YMN( 1981,  7, 20 ),
        new YMN( 1982,  7, 21 ),
        new YMN( 1983,  7, 22 ),
        new YMN( 1985,  7, 23 ),
        new YMN( 1988,  1, 24 ),
        new YMN( 1990,  1, 25),
        new YMN( 1991,  1, 26 ),
        new YMN( 1992,  7, 27 ),
        new YMN( 1993,  7, 28 ),
        new YMN( 1994,  7, 29 ),
        new YMN( 1996,  1, 30 ),
        new YMN( 1997,  7, 31 ),
        new YMN( 1999,  1, 32 ),
        new YMN( 2006,  1, 33 ),
        new YMN( 2009,  1, 34 ),
        new YMN( 2012,  7, 35 ), // leave the last comma!
         // add new entry here, of the form:
         // { year, month(1-12), leap_sec }, // leave the last comma!
      };

            // the number of leaps (do not change this)
            int NLEAPS = leaps.Count;

            // last year in leaps
            //static const int MAXYEAR = leaps[NLEAPS-1].year;

            // END static data -----------------------------------------------------

            // search for the input year, month
            if (year < 1960)                        // pre-1960 no deltas
                ;
            else if (month < 1 || month > 12)       // blunder, should never happen - throw?
                ;
            else if (year < 1972)
            {                 // [1960-1972) pre-leap
                for (int i = NPRE - 1; i >= 0; i--)
                {
                    if (preleap[i].year > year ||
                       (preleap[i].year == year && preleap[i].month > month)) continue;

                    // found last record with < rec.year >= year and rec.month >= month
                    // watch out - cannot use CommonTime here
                    int iday = Convert.ToInt32(day);
                    double dday = (iday - day);
                    if (iday == 0) { iday = 1; dday = 1.0 - dday; }
                    double JD0 =Time.ConvertCalendarToJD(year, month, iday);
                    double JD = Time.ConvertCalendarToJD(preleap[i].year, preleap[i].month, 1);
                    return (preleap[i].delt + (Convert.ToDouble(JD0 - JD) + dday) * preleap[i].rate);
                }
            }
            else
            {                                    // [1972- leap seconds
                for (int i = NLEAPS - 1; i >= 0; i--)
                {
                    if (leaps[i].year > year ||
                       (leaps[i].year == year && leaps[i].month > month)) continue;
                    return Convert.ToDouble(leaps[i].nleap);
                }
            }
            return 0.0;
        }

        // Compute the conversion (in seconds) from one time system (inTS) to another
        // (outTS), given the year and month of the time to be converted.
        // Result is to be added to the first time (inTS) to yield the converted (outTS),
        // that is t(outTS) = t(inTS) + correction(inTS,outTS).
        // NB. the caller must not forget to change to outTS after adding this correction.
        // @param TimeSystem inTS, input system
        // @param TimeSystem outTS, output system
        // @param int year, year of the time to be converted.
        // @param int month, month (1-12) of the time to be converted.
        // @return double dt, correction (sec) to be added to t(in) to yield t(out).
        // @throw if input system(s) are invalid or Unknown.
        public double Correction(TimeSystem inTS, TimeSystem outTS,
                                       int year, int month, double day)
        {
            double dt = 0.0;

            // identity
            if (inTS == outTS)
                return dt;

            // cannot convert unknowns
            if (inTS.System == Systems.Unknown || outTS.System == Systems.Unknown)
            {
                throw new Exception("Cannot compute correction for TimeSystem::Unknown");
            }

            // compute TT-TDB here; ref Astronomical Almanac B7
            double TDBmTT = 0.0;
            if (inTS.System == Systems.TDB || outTS.System == Systems.TDB)
            {
                int iday = (int)Math.Floor(day);
                double jday =Time. ConvertCalendarToJD(year, month, iday);
                double frac = day - iday;
                double TJ2000 = jday - 2451545.5 + frac;     // t-J2000
                //       0.0001657 sec * sin(357.53 + 0.98560028 * TJ2000 deg)
                frac = (0.017201969994578 * TJ2000) % (6.2831853071796);
                TDBmTT = 0.0001657 * Math.Sin(6.240075674 + frac);
                //        0.000022 sec * sin(246.11 + 0.90251792 * TJ2000 deg)
                frac = (0.015751909262251 * TJ2000) % (6.2831853071796);
                TDBmTT += 0.000022 * Math.Sin(4.295429822 + frac);
            }

            // -----------------------------------------------------------
            // conversions: first convert inTS->TAI ...
            // TAI = GPS + 19s
            // TAI = UTC + getLeapSeconds()
            // TAI = TT - 32.184s
            if (inTS.System == Systems.GPS ||       // GPS -> TAI
               inTS.System == Systems.GAL)         // GAL -> TAI
                dt = 19.0;
            else if (inTS.System == Systems.UTC |   // UTC -> TAI
                    inTS.System == Systems.BDT |   // BDT -> TAI           // TD is this right?
                    inTS.System == Systems.GLO)    // GLO -> TAI
                dt = GetLeapSeconds(year, month, day);
            //else if(inTS == BDT)    // BDT -> TAI         // RINEX 3.02 seems to say this
            //   dt = 34.;
            else if (inTS.System == Systems.TAI)    // TAI
                ;
            else if (inTS.System == Systems.TT)     // TT -> TAI
                dt = -32.184;
            else if (inTS.System == Systems.TDB)    // TDB -> TAI
                dt = -32.184 + TDBmTT;
            else
            {                              // other
                throw new Exception("Invalid input TimeSystem " + inTS.ToString());
            }

            // -----------------------------------------------------------
            // ... then convert TAI->outTS
            // GPS = TAI - 19s
            // UTC = TAI - getLeapSeconds()
            // TT = TAI + 32.184s
            if (outTS.System == Systems.GPS ||      // TAI -> GPS
               outTS.System == Systems.GAL)        // TAI -> GAL
                dt -= 19.0;
            else if (outTS.System == Systems.UTC |  // TAI -> UTC
                    outTS.System == Systems.BDT |  // TAI -> BDT
                    outTS.System == Systems.GLO)   // TAI -> GLO
                dt -= GetLeapSeconds(year, month, day);
            //else if(outTS == BDT)   // TAI -> BDT
            //   dt -= 34.;
            else if (outTS.System == Systems.TAI)   // TAI
                ;
            else if (outTS.System == Systems.TT)    // TAI -> TT
                dt += 32.184;
            else if (outTS.System == Systems.TDB)   // TAI -> TDB
                dt += 32.184 - TDBmTT;
            else if (outTS.System == Systems.GAL)   // TD
                dt = 0.0;
            else
            {                              // other
                throw new Exception("Invalid output TimeSystem " + outTS.ToString());
            }

            return dt;
        }

      

        public static void Example()
        {
            //int year = 2001;
            //int month = 1;
            //int day = 1;
            //int hour = 8;
            //int min = 18;
            //double second = 28.8;

         
        }
        
    }
}
