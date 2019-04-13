using System;

namespace Iono
{
    public class C
    {
        public const double TOLERANCE=1.0e-12;
        #region 1-Geometry几何
        /// Conversion Factor from degrees to radians (units: degrees^-1)角度转化为弧度的转化因子
        public const double DEG_TO_RAD = 1.7453292519943e-2;
        /// Conversion Factor from radians to degrees (units: degrees)弧度转化为角度的转化因子
        public const double RAD_TO_DEG = 57.295779513082;
        #endregion

        #region 2-AstronomicalFunctions天文函数？天文运动
        //--------------Astronomical-----------------
        /// Astronomical Unit value (AU), in meters天文单位值，单位为米
        public const double AU_CONST = 1.49597870e11;

        /// Mean Earth-Moon barycenter (EMB) distance (AU)平均地月中心距离
        public const double MeanEarthMoonBary = 3.12e-5;

        /// Ratio of mass Sun to Earth太阳和地球的质量比
        public const double MU_SUN = 332946.0;

        /// Ratio of mass Moon to Earth月球和地球的质量比
        public const double MU_MOON = 0.01230002;

        /// Earth gravity acceleration on surface (m/s^2)地球表面重力加速度
        public const double EarthGrav = 9.80665;

        /// Degrees to radians角度转弧度
        public const double D2R = 0.0174532925199432957692369;

        /// Arcseconds to radians
        public const double DAS2R = 4.848136811095359935899141e-6;

        /// Seconds of time to radians时间上的秒转为弧度
        public const double DS2R = 7.272205216643039903848712e-5;

        /// Julian epoch of B1950不知道这是个啥
        public const double B1950 = 1949.9997904423;

        /// Earth equatorial radius in AU ( 6378.137 km / 149597870 km)地球赤道半径
        public const double ERADAU = 4.2635212653763e-5;
        #endregion

        #region 3-Time Constants时间恒定
        
        public static Time END_OF_TIME=new Time(2100,1,1);//结束时间
        public static Time BEGINNING_OF_TIME = new Time(1900, 1, 1);//开始时间
        //--------------Time constants-------------------------------
        /// Add this offset to convert Modified Julian Date to Julian Date.
        /// 添加此偏移量将修改后的Julian Date转换为Julian Date
        public const double MJD_TO_JD = 2400000.5;
        /// 'Julian day' offset from MJD
        public const long MJD_JDAY = 2400001L;
        /// Modified Julian Date of UNIX epoch (Jan. 1, 1970).
        public const long UNIX_MJD = 40587L;

        /// Seconds per half week.
        public const long HALFWEEK = 302400L;//每半周是这么多秒
        /// Seconds per whole week.
        public const long FULLWEEK = 604800L;//每周是这么多秒

        /// Seconds per day.
        public const long SEC_PER_DAY = 86400L;//每天是这么多秒
        /// Days per second.
        public const double DAY_PER_SEC = 1.0 / SEC_PER_DAY;//每秒是多少天

        /// Milliseconds in a second.
        public const long MS_PER_SEC = 1000L;//一秒是多少毫秒
        /// Seconds per millisecond.
        public const double SEC_PER_MS = 1.0 / MS_PER_SEC;//一毫秒是多少秒

        /// Milliseconds in a day.
        public const long MS_PER_DAY = MS_PER_SEC * SEC_PER_DAY;//一天是多少毫秒
        /// Days per milliseconds.
        public const double DAY_PER_MS = 1.0 / MS_PER_DAY;//一毫秒是多少天

        #endregion

        // system-specific constants
       
        #region 4-GPS Time Constants     GPS时
        // GPS -------------------------------------------
        /// 'Julian day' of GPS epoch (Jan. 6, 1980).   GPS时的儒略日
        public const double GPS_EPOCH_JD = 2444244.5;
        /// Modified Julian Date of GPS epoch (Jan. 6, 1980).  GPS时的修正儒略日
        public const long GPS_EPOCH_MJD = 44244L;
        /// Weeks per GPS Epoch   每一个GPS时是多少周
        public const long GPS_WEEK_PER_EPOCH = 1024L;

        /// Zcounts in a  day. 也不知道这是个啥
        public const long ZCOUNT_PER_DAY = 57600L;
        /// Days in a Zcount
        public const double DAY_PER_ZCOUNT = 1.0 / ZCOUNT_PER_DAY;
        /// Zcounts in a week.
        public const long ZCOUNT_PER_WEEK = 403200L;
        /// Weeks in a Zcount.
        public const double WEEK_PER_ZCOUNT = 1.0 / ZCOUNT_PER_WEEK;
        
        #endregion

        #region 5-Galileo Time Constants伽利略时
        
        // GAL -------------------------------------------
        /// 'Julian day' of GAL epoch (Aug 22 1999)
        public const double GAL_EPOCH_JD = 2451412.5;
        /// Modified Julian Date of GAL epoch (Aug 22 1999)
        public const long GAL_EPOCH_MJD = 51412L;
        /// Weeks per GAL Epoch
        public const long GAL_WEEK_PER_EPOCH = 4096L;
        #endregion

        #region 6-QZS Time Constants
        // QZS -------------------------------------------
        /// 'Julian day' of QZS epoch (Jan. 1, 1980).
        public const double QZS_EPOCH_JD = 2444244.5;
        /// Modified Julian Date of QZS epoch (Jan. 1, 1980).
        public const long QZS_EPOCH_MJD = 44244L;
        /// Weeks per QZS Epoch
        public const long QZS_WEEK_PER_EPOCH = 65535L;         // effectively no rollover
        #endregion

        #region 7-Beidou Time Constants北斗时
        
        // BDS -------------------------------------------
        /// 'Julian day' of BDS epoch (Jan. 1, 2006).
        public const double BDS_EPOCH_JD = 2453736.5;
        /// Modified Julian Date of BDS epoch (Jan. 1, 2006).
        public const long BDS_EPOCH_MJD = 53736L;
        /// Weeks per BDS Epoch
        public const long BDS_WEEK_PER_EPOCH = 8192L;

        #endregion

        #region 8-MAX_PRN
        //------------- GNSS Constants--------------------------------
        // The maximum number of active satellites in the GPS  GPS中活跃卫星的最大数量
        //constellation.(Old version of MAX_PRN_GPS) */
        public const long MAX_PRN = 32;

        #endregion

        #region 9-independent of GNSS  GNSS的独立
        
        // ---------------- independent of GNSS ----------------------
        /// GPS value of PI; also specified by GAL   PI的GPS值；也被GAL指定
        public const double PI = 3.1415926535898;

        /// GPS value of PI*2
        public const double TWO_PI = 6.2831853071796;

        /// GPS value of PI**0.5
        public const double SQRT_PI = 1.7724539;

        /// relativity constant (sec/sqrt(m))
        public const double REL_CONST = -4.442807633e-10;

        /// m/s, speed of light; this value defined by GPS but applies to GAL and GLO.
        public const double C_MPS = 2.99792458e8;

        #endregion

        #region 10-GPS
        
        // ---------------- GPS --------------------------------------
        /// Hz, GPS Oscillator or chip frequency
        public const double OSC_FREQ_GPS = 10.23e6;

        /// Hz, GPS chip rate of the P & Y codes
        public const double PY_CHIP_FREQ_GPS = OSC_FREQ_GPS;

        /// Hz, GPS chip rate of the C/A code
        public const double CA_CHIP_FREQ_GPS = OSC_FREQ_GPS / 10.0;

        /// Hz, GPS Base freq w/o relativisitic effects
        public const double RSVCLK_GPS = 10.22999999543e6;

        /// GPS L1 carrier frequency in Hz
        public const double L1_FREQ_GPS = 1575.42e6;

        /// GPS L2 carrier frequency in Hz
        public const double L2_FREQ_GPS = 1227.60e6;

        /// GPS L5 carrier frequency in Hz.
        public const double L5_FREQ_GPS = 1176.45e6;

        /// GPS L1 carrier wavelength in meters
        public const double L1_WAVELENGTH_GPS = 0.190293672798;

        /// GPS L2 carrier wavelength in meters
        public const double L2_WAVELENGTH_GPS = 0.244210213425;

        /// GPS L5 carrier wavelength in meters.
        public const double L5_WAVELENGTH_GPS = 0.254828049;

        /// GPS L1 frequency in units of oscillator frequency
        public const double L1_MULT_GPS = 154.0;

        /// GPS L2 frequency in units of oscillator frequency
        public const double L2_MULT_GPS = 120.0;

        /// GPS L5 frequency in units of oscillator frequency.
        public const double L5_MULT_GPS = 115.0;

        /// GPS Gamma constant
        public const double GAMMA_GPS = 1.646944444;

        /// Reference Semi-major axis. From IS-GPS-800 Table 3.5-2 in meters.
        public const double A_REF_GPS = 26559710.0;

        /// Omega reference value from Table 30-I converted to radians
        public const double OMEGADOT_REF_GPS = -2.6e-9 * PI;


        public static short GetLegacyFitInterval(short iodc, short fiti)
        {
            //check the IODC 
            if (iodc < 0 || iodc > 1023)
            {
                // error in iodc, return minimum fit 
                return 4;
            }

            if ((((fiti == 0) && (iodc & 0xFF) < 240)
                 || (iodc & 0xFF) > 255))
            {
                // fit interval of 4 hours 
                return 4;
            }
            else if (fiti == 1)
            {
                if (((iodc & 0xFF) < 240 || (iodc & 0xFF) > 255))
                {
                    // fit interval of 6 hours 
                    return 6;
                }
                else if (iodc >= 240 && iodc <= 247)
                {
                    // fit interval of 8 hours 
                    return 8;
                }
                else if (((iodc >= 248) && (iodc <= 255)) || iodc == 496)
                {
                    // fit interval of 14 hours 
                    return 14;
                }
                else if ((iodc >= 497 && iodc <= 503) || (iodc >= 1021 && iodc <= 1023))
                {
                    // fit interval of 26 hours 
                    return 26;
                }
                else if (iodc >= 504 && iodc <= 510)
                {
                    // fit interval of 50 hours 
                    return 50;
                }
                else if (iodc == 511 || ((iodc >= 752) && (iodc <= 756)))
                {
                    // fit interval of 74 hours 
                    return 74;
                }
                else if (iodc == 757)
                {
                    /// fit interval of 98 hours 
                    return 98;
                }
                else
                {
                    throw new Exception("Invalid IODC Value For sv Block");
                }
            }
            else
            {
                // error in ephemeris/iodc, return minimum fit 
                return 4;
            }

        }
        #endregion

        #region 11-GLONASS
        
        // ---------------- GLONASS ----------------------------------
        /// GLO Fundamental chip rate in Hz.
        public const double OSC_FREQ_GLO = 5.11e6;
        /// GLO Chip rate of the P & Y codes in Hz.
        public const double PY_CHIP_FREQ_GLO = OSC_FREQ_GLO;
        /// GLO Chip rate of the C/A code in Hz.
        public const double CA_CHIP_FREQ_GLO = OSC_FREQ_GLO / 10.0;

        /// GLO Fundamental oscillator freq in Hz.
        public const double PSC_FREQ_GLO = 5.00e6;
        /// GLO Base freq w/o relativisitic effects in Hz.
        public const double RSVCLK_GLO = 4.99999999782e6;

        // GLO Frequency(Hz) f1 is 1602.0e6 + n*562.5e3 Hz = 9 * (178 + n*0.0625) MHz
        //                   f2    1246.0e6 + n*437.5e3 Hz = 7 * (178 + n*0.0625) MHz
        // where n is the time- and satellite-dependent 'frequency channel' -7 <= n <= 7
        /// GLO L1 carrier base frequency in Hz.
        public const double L1_FREQ_GLO = 1602.0e6;
        /// GLO L1 carrier frequency step size in Hz.
        public const double L1_FREQ_STEP_GLO = 562.5e3;
        /// GLO L1 carrier wavelength in meters.
        public const double L1_WAVELENGTH_GLO = 0.187136365793;
        /// GLO L2 carrier base frequency in Hz.
        public const double L2_FREQ_GLO = 1246.0e6;
        /// GLO L2 carrier frequency step size in Hz.
        public const double L2_FREQ_STEP_GLO = 437.5e3;
        /// GLO L2 carrier wavelength in meters.
        public const double L2_WAVELENGTH_GLO = 0.240603898876;
        /// GLO L1 multiplier.
        public const double L1_MULT_GLO = 320.4;
        /// GLO L2 multiplier.
        public const double L2_MULT_GLO = 249.2;

        /// Constant for the max array index in SV accuracy table.
        public const int SV_ACCURACY_GLO_INDEX_MAX = 15;
        /// Map from SV accuracy/URA flag to NOMINAL accuracy values in m.
        /// Further details in ICD-GLO-v5.0, Table 4.4 in Section 4.4.
        public static double[] SV_ACCURACY_GLO_INDEX = new double[]
           { 1.0,  2.0,   2.5,   4.0,   5.0, 7.0, 10.0,  12.0,  14.0,  16.0,
            32.0, 64.0, 128.0, 256.0, 512.0, 9.999999999999e99   };

        #endregion

        #region 12-Galileo
        
        // ---------------- Galileo ----------------------------------
        /// GAL L1 (E1) carrier frequency in Hz
        public const double L1_FREQ_GAL = L1_FREQ_GPS;
        /// GAL L5 (E5a) carrier frequency in Hz.
        public const double L5_FREQ_GAL = L5_FREQ_GPS;
        /// GAL L6 (E6) carrier frequency in Hz.
        public const double L6_FREQ_GAL = 1278.75e6;
        /// GAL L7 (E5b) carrier frequency in Hz.
        public const double L7_FREQ_GAL = 1207.140e6;
        /// GAL L8 (E5a+E5b) carrier frequency in Hz.
        public const double L8_FREQ_GAL = 1191.795e6;

        /// GAL L1 carrier wavelength in meters
        public const double L1_WAVELENGTH_GAL = L1_WAVELENGTH_GPS;
        /// GAL L5 carrier wavelength in meters.
        public const double L5_WAVELENGTH_GAL = L5_WAVELENGTH_GPS;
        /// GAL L6 carrier wavelength in meters.
        public const double L6_WAVELENGTH_GAL = 0.234441805;
        /// GAL L7 carrier wavelength in meters.
        public const double L7_WAVELENGTH_GAL = 0.24834937;
        /// GAL L8 carrier wavelength in meters.
        public const double L8_WAVELENGTH_GAL = 0.251547001;

        #endregion

        #region 13-SBAS
        // ---------------- Geostationary (SBAS) ---------------------
        /// GEO L1 carrier frequency in Hz
        public const double L1_FREQ_GEO = L1_FREQ_GPS;
        /// GEO L5 carrier frequency in Hz.
        public const double L5_FREQ_GEO = L5_FREQ_GPS;

        /// GEO L1 carrier wavelength in meters
        public const double L1_WAVELENGTH_GEO = L1_WAVELENGTH_GPS;
        /// GEO L5 carrier wavelength in meters.
        public const double L5_WAVELENGTH_GEO = L5_WAVELENGTH_GPS;

        #endregion

        #region 14-BeiDou
        
        // ---------------- BeiDou ----------------------------------
        /// BDS L1 (B1) carrier frequency in Hz.
        public const double L1_FREQ_BDS = 1561.098e6;
        /// BDS L2 (B2) carrier frequency in Hz.
        public const double L2_FREQ_BDS = L7_FREQ_GAL;
        /// BDS L3 (B3) carrier frequency in Hz.
        public const double L3_FREQ_BDS = 1268.52e6;

        /// BDS L1 carrier wavelength in meters.
        public const double L1_WAVELENGTH_BDS = 0.192039486310276;
        /// BDS L2 carrier wavelength in meters.
        public const double L2_WAVELENGTH_BDS = L7_WAVELENGTH_GAL;
        /// BDS L3 carrier wavelength in meters.
        public const double L3_WAVELENGTH_BDS = 0.236332464604421;

        #endregion

        #region 15-QZSS
        
        // ---------------- QZSS ----------------------------------
        /// QZS L1 carrier frequency in Hz.
        public const double L1_FREQ_QZS = L1_FREQ_GPS;
        /// QZS L2 carrier frequency in Hz.
        public const double L2_FREQ_QZS = L2_FREQ_GPS;
        /// QZS L5 carrier frequency in Hz.
        public const double L5_FREQ_QZS = L5_FREQ_GPS;
        /// QZS LEX(6) carrier frequency in Hz.
        public const double L6_FREQ_QZS = L6_FREQ_GAL;

        /// QZS L1 carrier wavelength in meters.
        public const double L1_WAVELENGTH_QZS = L1_WAVELENGTH_GPS;
        /// QZS L2 carrier wavelength in meters.
        public const double L2_WAVELENGTH_QZS = L2_WAVELENGTH_GPS;
        /// QZS L5 carrier wavelength in meters.
        public const double L5_WAVELENGTH_QZS = L5_WAVELENGTH_GPS;
        /// QZS LEX(6) carrier wavelength in meters.
        public const double L6_WAVELENGTH_QZS = L6_WAVELENGTH_GAL;

        #endregion

       

        public static void Example()
        {
            //var gps1 = new SatId(2, SatSys.GPS);
            //int n1 = 1;
            //int n2 = 5;
            //Console.WriteLine("Wavelength:{0}", C.GetWavelength(gps1, n1));
            //Console.WriteLine("Wavelength:{0}", C.GetWavelength(gps1, n2));
            //Console.WriteLine("Alpha= ratio of 2 frequencies fb/fa :{0}", C.GetAlpha(gps1, n1, n2));
            //Console.WriteLine("Beta=(beta^2-1) = ((fa/fb)^2-1):{0}", C.GetBeta(gps1, n1, n2));

        }
    }
}
