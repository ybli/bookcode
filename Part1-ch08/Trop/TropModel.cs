using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

// Model of the troposphere, used to compute non-dispersive delay of
// satellite signal as function of satellite elevation as seen at the
// receiver. Both wet and dry components are computed.
//
// The default model (implemented here) is a simple Black model.
//
// In this model (and many others), the wet and dry components are
// independent, the zenith delays depend only on the weather
// (temperature, pressure and humidity), and the mapping functions
// depend only on the elevation of the satellite as seen at the
// receiver. In general, this is not true; other models depend on,
// for example, latitude or day of year.
//
// Other models may be implemented by inheriting this class and
// redefining the virtual functions, and (perhaps) adding other
// 'set...()' routines as needed.
namespace Trop
{
    // Abstract base class for tropospheric models.
    // The wet and dry components of the tropospheric delay are each the
    // product of a zenith delay and a mapping function. Usually the zenith
    // delay depends only on the weather (temperature, pressure and humidity),
    // while the mapping function depends only on the satellite elevation, i.e.
    // the geometry of satellite and receiver. This may not be true in complex
    // models.
    // The full tropospheric delay is the sum of the wet and dry components.
    // A TropModel is valid only when all the necessary information
    // (weather + whatever else the model requires) is specified;
    // An InvalidTropModel exception will be thrown when any correction()
    // or zenith_delay() or mapping_function() routine is called for
    // an invalid TropModel.
    public abstract class TropModel
    {
        #region 1-Data Field
        // for temperature conversion from Celcius to Kelvin
        protected const double CELSIUS_TO_KELVIN = 273.15;
        protected double _temperature;
        // latest value of pressure (millibars)
        protected double _pressure;
        // latest value of relative humidity (percent)
        protected double _humidity;
        // Height
        protected double _height;

        protected bool validWeather;
        protected int doy;                      // day of year
        protected bool validDOY;


        #endregion

        #region 2-Correction

        /// Compute and return the full tropospheric delay
        /// @param elevation Elevation of satellite as seen at receiver, in degrees
        public double Correction(double elevation)
        {
            if (elevation < 0)
                return 0.0;
            var result = DryZenithDelay() * DryMappingFunction(elevation) +
                         WetZenithDelay() * WetMappingFunction(elevation);
            return result;
        }

        // Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite and the time tag. This version is most useful
        // within positioning algorithms, where the receiver position and timetag may
        // vary; it computes the elevation (and other receiver location information)
        // and passes them to appropriate set...() routines and the
        // correction(elevation) routine.
        // @param RX  Receiver position in ECEF cartesian coordinates (meters)
        // @param SV  Satellite position in ECEF cartesian coordinates (meters)
        public double Correction(Position RX, Position SV)
        {
            double result = Correction(RX.Elevation(SV));
            return result;
        }

        public double Correction(Position RX, Position SV, Time time)
        {
            SetDayOfYear(time.Doy);
            double result = Correction(RX.Elevation(SV));
            return result;
        }

        #endregion

        #region 3-abstract function

        /// Compute and return the zenith delay for dry component of the troposphere
        public abstract double DryZenithDelay();

        /// Compute and return the zenith delay for wet component of the troposphere
        public abstract double WetZenithDelay();

        /// Compute and return the mapping function for dry component of
        /// the troposphere.
        /// @param elevation Elevation of satellite as seen at receiver, in degrees
        public abstract double DryMappingFunction(double elevation);

        /// Compute and return the mapping function for wet component of
        /// the troposphere.
        /// @param elevation Elevation of satellite as seen at receiver, in degrees
        public abstract double WetMappingFunction(double elevation);

        #endregion

        #region 4-Set

        /// Re-define the tropospheric model with explicit weather data.
        /// Typically called just before correction().
        /// @param T temperature in degrees Celsius
        /// @param P atmospheric pressure in millibars
        /// @param H relative humidity in percent
        public void SetWeather(double temp, double pres, double humid)
        {
            ValidateTemp(temp);
            ValidatePres(pres);
            ValidateHumid(humid);
            _temperature = temp + CELSIUS_TO_KELVIN;
            _pressure = pres;
            _humidity = humid;

            validWeather = true;
        }

        /// get weather data by a standard atmosphere model
        /// reference to white paper of Bernese 5.0, P243
        /// @param ht     Height of the receiver in meters.
        /// @param T      temperature in degrees Celsius
        /// @param P      atmospheric pressure in millibars
        /// @param H      relative humidity in percent
        public void StandardAtmosphereModel(double ht)
        {
            // reference height and it's relate weather(T P H)
            const double h0 = 0.0;			   // meter
            const double Tr = +18.0;	           // Celsius
            const double pr = 1013.25;		   // millibarc
            const double Hr = 50;			   // humidity

            double T = Tr - 0.0065 * (ht - h0);
            double P = pr * Math.Pow((1 - 0.0000226 * (ht - h0)), 5.225);
            double H = Hr * Math.Exp(-0.0006396 * (ht - h0));

            SetWeather(T, P, H);
            validWeather = true;
        }

        /// get weather data by a standard atmosphere model
        /// reference to white paper of Bernese 5.0, P243
        /// @param ht     Height of the receiver in meters.
        /// @param T      temperature in degrees Celsius
        /// @param P      atmospheric pressure in millibars
        /// @param H      relative humidity in percent
        public void StandardAtmosphereModel(double ht, out double T, double P, double H)
        {
            // reference height and it's relate weather(T P H)
            const double h0 = 0.0;			   // meter
            const double Tr = +18.0;	           // Celsius
            const double pr = 1013.25;		   // millibarc
            const double Hr = 50;			   // humidity

            T = Tr - 0.0065 * (ht - h0);
            P = pr * Math.Pow((1 - 0.0000226 * (ht - h0)), 5.225);
            H = Hr * Math.Exp(-0.0006396 * (ht - h0));

            SetWeather(T, P, H);
            validWeather = true;
        }

        // Define the day of year; this is required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        public void SetDayOfYear(int doy)
        {

            if (doy > 0 && doy < 367)
                validDOY = true;
            else validDOY = false;

        }

        #endregion

        #region 5-Property

        public double Temperature
        {
            get { return _temperature; }
            set
            {
                ValidateTemp(value);
                _temperature = value;
            }
        }

        public double Pressure
        {
            get { return _pressure; }
            set
            {
                ValidatePres(value);
                _pressure = value;
            }
        }

        public double Humidity
        {
            get { return _humidity; }
            set
            {
                ValidateHumid(value);
                _humidity = value;
            }
        }

        #endregion

        #region 6-Validate

        private void ValidateTemp(double temp)
        {
            temp = temp + CELSIUS_TO_KELVIN;
            if (temp < 0.0)
            {
                string ex = "Invalid temperature parameter. --in " + FileName();
                throw new Exception(ex);
            }
        }

        private void ValidatePres(double press)
        {
            if (press < 0.0)
            {
                string ex = "Invalid pressure parameter. --in " + FileName();
                throw new Exception(ex);
            }
        }

        private void ValidateHumid(double humid)
        {
            if (humid < 0.0 || humid > 100.0)
            {
                string ex = "Invalid humidity parameter. --in " + FileName();
               
            }
        }
        #endregion

        protected string FileName()
        {
            return "File: TropModel.cs";
        }

        public static void Example()
        {
            double ht = 100;
            var time = new Time(2014, 1, 20, 14, 56, 10.0);
            double ele = 45;
            double T = 20, P = 1001, H = 45.0;
            var rx = new Position(30, 114, 30, CoordinateSystem.Geodetic);

            TropModel model = new SimpleTropModel();
            model.StandardAtmosphereModel(ht);

            Console.WriteLine("Time:{0}", time.ToYmdHmsString());
            Console.WriteLine("T:{0:0.00}K,P:{1:0.00}mb, H:{2:0.00}%", model.Temperature, model.Pressure, model.Humidity);

            Console.WriteLine("-------1: Simple Trop Model--------");
            Console.WriteLine("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model.DryZenithDelay(), model.WetZenithDelay(), model.Correction(ele));

            Console.WriteLine("-------2: GGTropModel--------");
            model = new GGTropModel();
            model.SetWeather(T, P, H);
            Console.WriteLine("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model.DryZenithDelay(), model.WetZenithDelay(), model.Correction(ele));

            Console.WriteLine("-------3: GGHeightTropModel--------");
            model = new GGHeightTropModel();
            model.SetWeather(T, P, H);
            Console.WriteLine("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model.DryZenithDelay(), model.WetZenithDelay(), model.Correction(ele));

            Console.WriteLine("-------4: NBTropModel--------");
            var model1 = new NBTropModel(rx.GeodeticLatitude, time.Doy);
            model1.SetWeather(T, P, H);
            model1.SetReceiverHeight(rx.Height);
            Console.WriteLine("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model1.DryZenithDelay(), model1.WetZenithDelay(), model1.Correction(ele));

            Console.WriteLine("-------5: GCATTropModel--------");
            model = new GCATTropModel(ht);
            Console.WriteLine("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model.DryZenithDelay(), model.WetZenithDelay(), model.Correction(ele));

            Console.WriteLine("-------6: MOPSTropModel--------");
            var model2 = new MOPSTropModel(rx,time);
            Console.WriteLine("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model.DryZenithDelay(), model.WetZenithDelay(), model.Correction(ele));

            Console.WriteLine("-------7: NeillTropModel--------");
            var model3 = new NeillTropModel(rx, time);
            model3.SetWeather(T,P,H);
            Console.WriteLine("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model.DryZenithDelay(), model.WetZenithDelay(), model.Correction(ele));
        }
    }

    /// The 'zero' trop model, meaning it always returns zero.
    public class ZeroTropModel : TropModel
    {


        public ZeroTropModel()
        {
            StandardAtmosphereModel(0);
        }
        public override double DryZenithDelay()
        {
            return 0;
        }

        public override double WetZenithDelay()
        {
            return 0;
        }

        public override double DryMappingFunction(double elevation)
        {
            return 0;
        }

        public override double WetMappingFunction(double elevation)
        {
            return 0;
        }
    }

    /// A simple Black model of the troposphere. temp is in Kelvin.
    /// Simple Black model. This has been used as the 'default' for many years.
    public class SimpleTropModel : TropModel
    {
        #region 1-Data Field
        double Cdrydelay;
        double Cwetdelay;
        double Cdrymap;
        double Cwetmap;

        #endregion

        #region 2-Constructor

        public SimpleTropModel()
        {
            base.SetWeather(20.0, 980.0, 50.0);
            Cwetdelay = 0.122382715318184;
            Cdrydelay = 2.235486646978727;
            Cwetmap = 1.000282213715744;
            Cdrymap = 1.001012704615527;
        }
        // Create a tropospheric model from explicit weather data
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public SimpleTropModel(double T, double P, double H)
        {
            SetWeather(T, P, H);
        }

        #endregion

        #region 3-Set

        // Re-define the tropospheric model with explicit weather data.
        // Typically called just before correction().
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public new void SetWeather(double T, double P, double H)
        {
            base.SetWeather(T, P, H);
            var ell = new WGS84Ellipsoid();
            Cdrydelay = 2.343 * (_pressure / 1013.25) * (_temperature - 3.96) / _temperature;
            double tks = _temperature * _temperature;
            Cwetdelay = 8.952 / tks * _humidity * Math.Exp(-37.2465 + 0.213166 * _temperature - (0.256908e-3) * tks);
            Cdrymap = 1.0 + (0.15) * 148.98 * (_temperature - 3.96) / ell.a;
            Cwetmap = 1.0 + (0.15) * 12000.0 / ell.a;
            validWeather = true;
        }

        #endregion

        #region 4-override

        // Compute and return the zenith delay for dry component of the troposphere
        public override double DryZenithDelay()
        {
            return Cdrydelay;
        }

        // Compute and return the zenith delay for wet component of the troposphere
        public override double WetZenithDelay()
        {
            return Cwetdelay;
        }

        // Compute and return the mapping function for dry component
        // of the troposphere
        // @param elevation is the Elevation of satellite as seen at receiver,
        //                  in degrees
        public override double DryMappingFunction(double elevation)
        {
            if (elevation < 0.0) return 0.0;

            double d = Math.Cos(elevation * C.DEG_TO_RAD);
            d /= Cdrymap;
            return (1.0 / Math.Sqrt(1.0 - d * d));
        }

        // Compute and return the mapping function for wet component
        // of the troposphere
        // @param elevation is the Elevation of satellite as seen at receiver,
        //                  in degrees
        public override double WetMappingFunction(double elevation)
        {
            if (elevation < 0.0) return 0.0;

            double d = Math.Cos(elevation * C.DEG_TO_RAD);
            d /= Cwetmap;
            return (1.0 / Math.Sqrt(1.0 - d * d));
        }

        #endregion

    }

    //  Tropospheric model based on Goad and Goodman(1974),
    //  "A Modified Hopfield Tropospheric Refraction Correction Model," Paper
    // presented at the Fall Annual Meeting of the American Geophysical Union,
    // San Francisco, December 1974, as presented in Leick, "GPS Satellite Surveying,"
    // Wiley, NY, 1990, Chapter 9 (note particularly Table 9.1).
    public class GGTropModel : TropModel
    {
        #region 1- Data Field

        const double GGdryscale = 8594.777388436570600;
        const double GGwetscale = 2540.042008403690900;
        double Cdrydelay;
        double Cwetdelay;
        double Cdrymap;
        double Cwetmap;

        public GGTropModel()
        {
            base.SetWeather(20.0, 980.0, 50.0);
            Cdrydelay = 2.59629761092150147e-4;    // zenith delay, dry
            Cwetdelay = 4.9982784999977412e-5;     // zenith delay, wet
            Cdrymap = 42973.886942182834900;       // height for mapping, dry
            Cwetmap = 12700.210042018454260;       // height for mapping, wet
        }

        #endregion

        #region 2- Constructor
        // Create a tropospheric model from explicit weather data
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public GGTropModel(double T, double P, double H)
        {
            SetWeather(T, P, H);
        }
        #endregion

        #region 3-override

        public override double DryZenithDelay()
        {
            return (Cdrydelay * GGdryscale);
        }

        public override double WetZenithDelay()
        {
            return (Cwetdelay * GGwetscale);
        }

        public override double DryMappingFunction(double elevation)
        {
            if (elevation < 0.0) return 0.0;

            var ell = new WGS84Ellipsoid();
            double ce = Math.Cos(elevation * C.DEG_TO_RAD), se = Math.Sin(elevation * C.DEG_TO_RAD);
            double ad = -se / Cdrymap;
            double bd = -ce * ce / (2.0 * ell.a * Cdrymap);
            double Rd = Math.Sqrt((ell.a + Cdrymap) * (ell.a + Cdrymap)
                      - ell.a * ell.a * ce * ce) - ell.a * se;

            double[] Ad = new double[9];
            double ad2 = ad * ad, bd2 = bd * bd;
            Ad[0] = 1.0;
            Ad[1] = 4.0 * ad;
            Ad[2] = 6.0 * ad2 + 4.0 * bd;
            Ad[3] = 4.0 * ad * (ad2 + 3.0 * bd);
            Ad[4] = ad2 * ad2 + 12.0 * ad2 * bd + 6.0 * bd2;
            Ad[5] = 4.0 * ad * bd * (ad2 + 3.0 * bd);
            Ad[6] = bd2 * (6.0 * ad2 + 4.0 * bd);
            Ad[7] = 4.0 * ad * bd * bd2;
            Ad[8] = bd2 * bd2;

            // compute dry component of the mapping function
            double sumd = 0.0;
            for (int j = 9; j >= 1; j--)
            {
                sumd += Ad[j - 1] / j;
                sumd *= Rd;
            }
            return sumd / GGdryscale;
        }

        // compute wet component of the mapping function
        public override double WetMappingFunction(double elevation)
        {
            if (elevation < 0.0) return 0.0;

            var ell = new WGS84Ellipsoid();
            double ce = Math.Cos(elevation * C.DEG_TO_RAD), se = Math.Sin(elevation * C.DEG_TO_RAD);
            double aw = -se / Cwetmap;
            double bw = -ce * ce / (2.0 * ell.a * Cwetmap);
            double Rw = Math.Sqrt((ell.a + Cwetmap) * (ell.a + Cwetmap)
                      - ell.a * ell.a * ce * ce) - ell.a * se;

            double[] Aw = new double[9];
            double aw2 = aw * aw, bw2 = bw * bw;
            Aw[0] = 1.0;
            Aw[1] = 4.0 * aw;
            Aw[2] = 6.0 * aw2 + 4.0 * bw;
            Aw[3] = 4.0 * aw * (aw2 + 3.0 * bw);
            Aw[4] = aw2 * aw2 + 12.0 * aw2 * bw + 6.0 * bw2;
            Aw[5] = 4.0 * aw * bw * (aw2 + 3.0 * bw);
            Aw[6] = bw2 * (6.0 * aw2 + 4.0 * bw);
            Aw[7] = 4.0 * aw * bw * bw2;
            Aw[8] = bw2 * bw2;

            double sumw = 0.0;
            for (int j = 9; j >= 1; j--)
            {
                sumw += Aw[j - 1] / j;
                sumw *= Rw;
            }
            return sumw / GGwetscale;
        }

        #endregion

        #region 4-Set

        public new void SetWeather(double T, double P, double H)
        {
            base.SetWeather(T, P, H);
            double th = 300.0 / _temperature;
            // water vapor partial pressure (mb)
            // this comes from Leick and is not good.
            // double wvpp=6.108*(RHum*0.01)*exp((17.15*Tk-4684.0)/(Tk-38.45));
            double wvpp = 2.409e9 * _humidity * th * th * th * th * Math.Exp(-22.64 * th);
            Cdrydelay = 7.7624e-5 * _pressure / _temperature;
            Cwetdelay = 1.0e-6 * (-12.92 + 3.719e+05 / _temperature) * (wvpp / _temperature);
            Cdrymap = (5.0 * 0.002277 * _pressure) / Cdrydelay;
            Cwetmap = (5.0 * 0.002277 / Cwetdelay) * (1255.0 / _temperature + 0.5) * wvpp;
            validWeather = true;
        }

        #endregion

    }

    //---------------------------------------------------------------------------------
    // Tropospheric model with heights based on Goad and Goodman(1974),
    //  "A Modified Hopfield Tropospheric Refraction Correction Model," Paper
    //  presented at the Fall Annual Meeting of the American Geophysical Union,
    //  San Francisco, December 1974.
    // 
    //  (Not the same as GGTropModel because this has height dependence, and the
    //  computation of this model does not break cleanly into wet and dry components.)
    // 
    //  NB this model requires heights, both of the weather parameters,
    //    and of the receiver.
    //  Thus, usually, caller will set heights at the same time the weather is set:
    //  NB setReceiverHeight(ht) sets the 'weather heights' as well, if they are not
    //    already defined.
    public class GGHeightTropModel : TropModel
    {

        #region 1-Data Field

        bool validHeights = false; //
        bool validRxHeight = false;
        double htemp;                 // height (m) at which temp applies   
        double hpress;                // height (m) at which press applies
        double hhumid;                // height (m) at which humid applies

        #endregion

        #region 2-Constructor
        // Default constructor
        public GGHeightTropModel()
        {
            SetWeather(20.0, 980.0, 50.0);
            SetReceiverHeight(0.0);
        }

        // Create a tropospheric model from explicit weather data
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public GGHeightTropModel(double T, double P, double H)
        {
            validRxHeight = validHeights = false;
            SetWeather(T, P, H);
        }

        public GGHeightTropModel(double T, double P, double H, double rxHeight)
        {
            SetWeather(T, P, H);
            SetReceiverHeight(rxHeight);
        }

        // Create a valid model from explicit input.
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        // @param hT height at which temperature applies in meters.
        // @param hP height at which atmospheric pressure applies in meters.
        // @param hH height at which relative humidity applies in meters.
        public GGHeightTropModel(double T, double P, double H,
                                               double hT, double hP, double hH)
        {
            validRxHeight = false;
            SetWeather(T, P, H);
            SetHeights(hT, hP, hH);
        }
        #endregion

        #region 3-Set
        // Re-define the heights at which the weather parameters apply.
        // Typically called just before correction().
        // @param hT height (m) at which temperature applies
        // @param hP height (m) at which atmospheric pressure applies
        // @param hH height (m) at which relative humidity applies
        public void SetHeights(double hT, double hP, double hH)
        {
            htemp = hT;                 // height (m) at which temp applies
            hpress = hP;                // height (m) at which press applies
            hhumid = hH;                // height (m) at which humid applies
            validHeights = true;
        }

        // Define the receiver height; this required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        public void SetReceiverHeight(double ht)
        {
            _height = ht;
            validRxHeight = true;
            if (!validHeights)
            {
                htemp = hpress = hhumid = ht;
                validHeights = true;
            }
        }
        #endregion

        #region 4-validate
        private void ValideHeight()
        {
            if (!validHeights)
            {
                throw new Exception("Invalid GGH trop model: Heights. --in " + FileName());
            }
        }


        private void ValidateRxHeight()
        {

            if (!validRxHeight)
            {
                throw new Exception("Invalid GGH trop model: Rx Height. --in " + FileName());
            }
        }
        #endregion

        #region 5-correction

        // re-define this to get the throws
        public new double Correction(double elevation)
        {
            ValideHeight();
            ValidateRxHeight();
            return base.Correction(elevation);
        }
        // Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite and the time tag. This version is most useful
        // within positioning algorithms, where the receiver position and timetag
        // may vary; it computes the elevation (and other receiver location
        // information) and passes them to appropriate set...() routines and
        // the correction(elevation) routine.
        // @param RX  Receiver position
        // @param SV  Satellite position
        public new double Correction(Position RX, Position SV)
        {
            ValideHeight();
            ValidateRxHeight();
            // compute height from RX
            SetReceiverHeight(RX.Altitude);

            return base.Correction(RX.Elevation(SV));

        }
        #endregion

        #region 6-override

        // Compute and return the zenith delay for dry component of the troposphere
        public override double DryZenithDelay()
        {
            ValideHeight();
            ValidateRxHeight();
            double hrate = 6.5e-3;
            double Ts = _temperature + hrate * _height;
            double em = 978.77 / (2.8704e4 * hrate);
            double Tp = Ts - hrate * hpress;
            double ps = _pressure * Math.Pow(Ts / Tp, em) / 1000.0;
            double rs = 77.624e-3 / Ts;
            double ho = 11.385 / rs;
            rs *= ps;
            double zen = (ho - _height) / ho;
            zen = rs * zen * zen * zen * zen;
            // normalize
            zen *= (ho - _height) / 5;
            return zen;
        }

        // Compute and return the zenith delay for wet component of the troposphere
        public override double WetZenithDelay()
        {
            ValideHeight();
            ValidateRxHeight();
            double hrate = 6.5e-3; //   deg K / m
            double Th = _temperature - 273.15 - hrate * (hhumid - htemp);
            double Ta = 7.5 * Th / (237.3 + Th);
            // water vapor partial pressure
            double e0 = 6.11e-5 * _humidity * Math.Pow(10.0, Ta);
            double Ts = _temperature + hrate * htemp;
            double em = 978.77 / (2.8704e4 * hrate);
            double Tk = Ts - hrate * hhumid;
            double es = e0 * Math.Pow(Ts / Tk, 4.0 * em);
            double rs = (371900.0e-3 / Ts - 12.92e-3) / Ts;
            double ho = 11.385 * (1255 / Ts + 0.05) / rs;
            double zen = (ho - _height) / ho;
            zen = rs * es * zen * zen * zen * zen;
            //normalize
            zen *= (ho - _height) / 5;
            return zen;
        }

        // Compute and return the mapping function for dry component
        // of the troposphere
        // @param elevation Elevation of satellite as seen at receiver,
        //                  in degrees
        public override double DryMappingFunction(double elevation)
        {
            ValideHeight();
            ValidateRxHeight();

            if (elevation < 0.0) return 0.0;

            double hrate = 6.5e-3;
            double Ts = _temperature + hrate * htemp;
            double ho = (11.385 / 77.624e-3) * Ts;
            double se = Math.Sin(elevation * C.DEG_TO_RAD);
            if (se < 0.0) se = 0.0;

            var ell = new WGS84Ellipsoid();
            double rt, a, b;
            double[] rn = new double[8];
            double[] al = new double[8];
            double er = ell.a;
            rt = (er + ho) / (er + _height);
            rt = rt * rt - (1.0 - se * se);
            if (rt < 0) rt = 0.0;
            rt = (er + _height) * (Math.Sqrt(rt) - se);
            a = -se / (ho - _height);
            b = -(1.0 - se * se) / (2.0 * er * (ho - _height));
            rn[0] = rt * rt;
            for (int j = 1; j < 8; j++) rn[j] = rn[j - 1] * rt;
            al[0] = 2 * a;
            al[1] = 2 * a * a + 4 * b / 3;
            al[2] = a * (a * a + 3 * b);
            al[3] = a * a * a * a / 5 + 2.4 * a * a * b + 1.2 * b * b;
            al[4] = 2 * a * b * (a * a + 3 * b) / 3;
            al[5] = b * b * (6 * a * a + 4 * b) * 0.1428571;
            if (b * b > 1.0e-35)
            {
                al[6] = a * b * b * b / 2;
                al[7] = b * b * b * b / 9;
            }
            else
            {
                al[6] = 0.0;
                al[7] = 0.0;
            }
            double map = rt;
            for (int k = 0; k < 8; k++) map += al[k] * rn[k];
            // normalize
            double norm = (ho - _height) / 5;
            return map / norm;

        }

        // Compute and return the mapping function for wet component
        // of the troposphere
        // @param elevation Elevation of satellite as seen at receiver,
        //                  in degrees
        public override double WetMappingFunction(double elevation)
        {
            ValideHeight();
            ValidateRxHeight();

            if (elevation < 0.0) return 0.0;

            double hrate = 6.5e-3;
            double Ts = _temperature + hrate * htemp;
            double rs = (371900.0e-3 / Ts - 12.92e-3) / Ts;
            double ho = 11.385 * (1255 / Ts + 0.05) / rs;
            double se = Math.Sin(elevation * C.DEG_TO_RAD);
            if (se < 0.0) se = 0.0;

            var ell = new WGS84Ellipsoid();
            double rt, a, b, er = ell.a;
            double[] rn = new double[8];
            double[] al = new double[8];
            rt = (er + ho) / (er + _height);
            rt = rt * rt - (1.0 - se * se);
            if (rt < 0) rt = 0.0;
            rt = (er + _height) * (Math.Sqrt(rt) - se);
            a = -se / (ho - _height);
            b = -(1.0 - se * se) / (2.0 * er * (ho - _height));
            rn[0] = rt * rt;
            for (int i = 1; i < 8; i++) rn[i] = rn[i - 1] * rt;
            al[0] = 2 * a;
            al[1] = 2 * a * a + 4 * b / 3;
            al[2] = a * (a * a + 3 * b);
            al[3] = a * a * a * a / 5 + 2.4 * a * a * b + 1.2 * b * b;
            al[4] = 2 * a * b * (a * a + 3 * b) / 3;
            al[5] = b * b * (6 * a * a + 4 * b) * 0.1428571;
            if (b * b > 1.0e-35)
            {
                al[6] = a * b * b * b / 2;
                al[7] = b * b * b * b / 9;
            }
            else
            {
                al[6] = 0.0;
                al[7] = 0.0;
            }
            double map = rt;
            for (int j = 0; j < 8; j++) map += al[j] * rn[j];
            // normalize map function
            double norm = (ho - _height) / 5;
            return map / norm;
        }
        #endregion

    }

    ////---------------------------------------------------------------------------------
    //  Tropospheric model developed by University of New Brunswick and described in
    //  "A Tropospheric Delay Model for the User of the Wide Area Augmentation
    //  System," J. Paul Collins and Richard B. Langley, Technical Report No. 187,
    //  Dept. of Geodesy and Geomatics Engineering, University of New Brunswick,
    //  1997. See particularly Appendix C.
    // 
    //  This model includes a wet and dry component, and was designed for the user
    //  without access to measurements of temperature, pressure and relative humidity
    //  at ground level. Input of the receiver latitude, day of year and height
    //  above the ellipsoid are required, because the mapping functions depend on
    //  these quantities. In addition, if the weather (T,P,H) are not explicitly
    //  provided, this model interpolates a table of values, using latitude and day
    //  of year, to get the ground level weather parameters.
    // 
    // NB in this model, units of temp are degrees Celsius, and humid is the water
    // vapor partial pressure.

    public class NBTropModel : TropModel
    {
        #region 1-Data Field

        bool interpolateWeather;      // if true, compute T,P,H from latitude,doy
        double height;                // height (m) of the receiver
        double latitude;              // latitude (deg) of receiver
        bool validRxLatitude;
        bool validRxHeight;


        const double NBRd = 287.054;   // J/kg/K = m*m*K/s*s
        const double NBg = 9.80665;    // m/s*s
        const double NBk1 = 77.604;    // K/mbar
        const double NBk3p = 382000;   // K*K/mbar

        static double[] NBLat = new double[5] { 15.0, 30.0, 45.0, 60.0, 75.0 };

        // zenith delays, averages
        static double[] NBZP0 = new double[5] { 1013.25, 1017.25, 1015.75, 1011.75, 1013.00 };
        static double[] NBZT0 = new double[5] { 299.65, 294.15, 283.15, 272.15, 263.65 };
        static double[] NBZW0 = new double[5] { 26.31, 21.79, 11.66, 6.78, 4.11 };
        static double[] NBZB0 = new double[5] { 6.30e-3, 6.05e-3, 5.58e-3, 5.39e-3, 4.53e-3 };
        static double[] NBZL0 = new double[5] { 2.77, 3.15, 2.57, 1.81, 1.55 };

        // zenith delays, amplitudes
        static double[] NBZPa = new double[] { 0.0, -3.75, -2.25, -1.75, -0.50 };
        static double[] NBZTa = new double[] { 0.0, 7.0, 11.0, 15.0, 14.5 };
        static double[] NBZWa = new double[] { 0.0, 8.85, 7.24, 5.36, 3.39 };
        static double[] NBZBa = new double[] { 0.0, 0.25e-3, 0.32e-3, 0.81e-3, 0.62e-3 };
        static double[] NBZLa = new double[] { 0.0, 0.33, 0.46, 0.74, 0.30 };

        // mapping function, dry, averages
        static double[] NBMad = new double[]{1.2769934e-3,1.2683230e-3,1.2465397e-3,1.2196049e-3,
                            1.2045996e-3};
        static double[] NBMbd = new double[]{2.9153695e-3,2.9152299e-3,2.9288445e-3,2.9022565e-3,
                            2.9024912e-3};
        static double[] NBMcd = new double[]{62.610505e-3,62.837393e-3,63.721774e-3,63.824265e-3,
                                 64.258455e-3};

        // mapping function, dry, amplitudes
        static double[] NBMaa = new double[]{0.0,1.2709626e-5,2.6523662e-5,3.4000452e-5,
                            4.1202191e-5};
        static double[] NBMba = new double[]{0.0,2.1414979e-5,3.0160779e-5,7.2562722e-5,
                            11.723375e-5};
        static double[] NBMca = new double[]{0.0,9.0128400e-5,4.3497037e-5,84.795348e-5,
                                 170.37206e-5};

        // mapping function, wet, averages (there are no amplitudes for wet)
        static double[] NBMaw = new double[]{5.8021897e-4,5.6794847e-4,5.8118019e-4,5.9727542e-4,
                     6.1641693e-4};
        static double[] NBMbw = new double[]{1.4275268e-3,1.5138625e-3,1.4572752e-3,1.5007428e-3,
                     1.7599082e-3};
        static double[] NBMcw = new double[]{4.3472961e-2,4.6729510e-2,4.3908931e-2,4.4626982e-2,
                           5.4736038e-2};

        // labels for use with the interpolation routine
        enum TableEntry { ZP = 1, ZT, ZW, ZB, ZL, Mad, Mbd, Mcd, Maw, Mbw, Mcw };

        // the interpolation routine
        double NB_Interpolate(double lat, int doy, TableEntry entry)
        {
            double[] pave = new double[5];
            double[] pamp = new double[5];
            double ret, day = doy;

            // assign pointer to the right array
            switch (entry)
            {
                case TableEntry.ZP: pave = NBZP0; pamp = NBZPa; break;
                case TableEntry.ZT: pave = NBZT0; pamp = NBZTa; break;
                case TableEntry.ZW: pave = NBZW0; pamp = NBZWa; break;
                case TableEntry.ZB: pave = NBZB0; pamp = NBZBa; break;
                case TableEntry.ZL: pave = NBZL0; pamp = NBZLa; break;
                case TableEntry.Mad: pave = NBMad; pamp = NBMaa; break;
                case TableEntry.Mbd: pave = NBMbd; pamp = NBMba; break;
                case TableEntry.Mcd: pave = NBMcd; pamp = NBMca; break;
                case TableEntry.Maw: pave = NBMaw; break;
                case TableEntry.Mbw: pave = NBMbw; break;
                case TableEntry.Mcw: pave = NBMcw; break;
            }

            // find the index i such that NBLat[i] <= lat < NBLat[i+1]
            int i = (int)(Math.Abs(lat) / 15.0) - 1;

            if (i >= 0 && i <= 3)
            {               // mid-latitude -> regular interpolation
                double m = (Math.Abs(lat) - NBLat[i]) / (NBLat[i + 1] - NBLat[i]);
                ret = pave[i] + m * (pave[i + 1] - pave[i]);
                if (entry < TableEntry.Maw)
                    ret -= (pamp[i] + m * (pamp[i + 1] - pamp[i]))
                       * Math.Cos(C.TWO_PI * (day - 28.0) / 365.25);
            }
            else
            {                           // < 15 or > 75 -> simpler interpolation
                if (i < 0) i = 0; else i = 4;
                ret = pave[i];
                if (entry < TableEntry.Maw)
                    ret -= pamp[i] * Math.Cos(C.TWO_PI * (day - 28.0) / 365.25);
            }

            return ret;

        }
        #endregion

        #region 2-Constructor
        // Default constructor
        public NBTropModel()
        {
            validWeather = false;
            validRxLatitude = false;
            validDOY = false;
            validRxHeight = false;
        }
        // Create a trop model using the minimum information: latitude and doy.
        // Interpolate the weather unless setWeather (optional) is called.
        // @param lat Latitude of the receiver in degrees.
        // @param day Day of year.
        public NBTropModel(double lat, int day)
        {
            validRxHeight = false;
            SetReceiverLatitude(lat);
            SetDayOfYear(day);
        }


        // Create a valid model from explicit input (weather will be estimated
        // internally by this model).
        // @param ht Height of the receiver in meters.
        // @param lat Latitude of the receiver in degrees.
        // @param d Day of year.
        NBTropModel(double ht, double lat, int day)
        {
            SetReceiverHeight(ht);
            SetReceiverLatitude(lat);
            SetDayOfYear(day);
        }

        // Create a tropospheric model from explicit weather data
        // @param lat Latitude of the receiver in degrees.
        // @param day Day of year.
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public NBTropModel(double lat, int day,
                                 double T, double P, double H)
        {
            validRxHeight = false;
            SetReceiverLatitude(lat);
            SetDayOfYear(day);
            SetWeather(T, P, H);

        }
        #endregion

        #region 3-Set
        // Re-define the weather data.
        // If called, typically called before any calls to correction().
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public new void SetWeather(double T, double P, double H)
        {
            interpolateWeather = false;
            base.SetWeather(T, P, H);
            // humid actually stores water vapor partial pressure
            double th = 300.0 / _temperature;
            _humidity = 2.409e9 * H * th * th * th * th * Math.Exp(-22.64 * th);
            validWeather = true;
        }

        // configure the model to estimate the weather from the internal model,
        // using lat and doy
        void SetWeather()
        {
            interpolateWeather = true;
            if (!validRxLatitude)
                throw new Exception("NBTropModel must have Rx latitude before interpolating weather --In " + FileName());
            if (!validDOY)
                throw new Exception("NBTropModel must have day of year before interpolating weather. --In " + FileName());

            _temperature = NB_Interpolate(latitude, doy, TableEntry.ZT);
            _pressure = NB_Interpolate(latitude, doy, TableEntry.ZP);
            _humidity = NB_Interpolate(latitude, doy, TableEntry.ZW);
            validWeather = true;

        }

        // Define the receiver height; this required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        public void SetReceiverHeight(double ht)
        {
            height = ht;
            validRxHeight = true;

        }
        // Define the latitude of the receiver; this is required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        public void SetReceiverLatitude(double lat)
        {
            latitude = lat;
            validRxLatitude = true;
        }

        #endregion

        #region 4-Validate
        private void Validate()
        {
            if (!validRxLatitude)
                throw new Exception("Invalid NB trop model: Rx Latitude. --In " + FileName());
            if (!validRxHeight)
                throw new Exception("Invalid NB trop model: Rx Height. --In " + FileName());
            if (!validDOY)
                throw new Exception("Invalid NB trop model: day of year. --In " + FileName());

        }
        #endregion

        #region 5-Correction
        public new double Correction(double elevation)
        {
            Validate();
            SetWeather();
            return base.Correction(elevation);
        }
        // Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite and the time tag. This version is most useful
        // within positioning algorithms, where the receiver position and timetag
        // may vary; it computes the elevation (and other receiver location
        // information) and passes them to appropriate set...() routines
        // and the correction(elevation) routine.
        // @param RX  Receiver position
        // @param SV  Satellite position
        // @param tt  Time tag of the signal
        public new double Correction(Position RX, Position SV, Time time)
        {

            SetDayOfYear(time.Doy);
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);

            return Correction(RX.Elevation(SV));
        }
        public new double Correction(Position RX, Position SV)
        {
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);
            return Correction(RX.Elevation(SV));
        }
        #endregion

        #region 6-override
        // Compute and return the zenith delay for dry component of the troposphere
        public override double DryZenithDelay()
        {
            Validate();
            double beta = NB_Interpolate(latitude, doy, TableEntry.ZB);
            double gm = 9.784 * (1.0 - 2.66e-3 * Math.Cos(2.0 * latitude * C.DEG_TO_RAD) - 2.8e-7 * height);

            // scale factors for height above mean sea level
            // if weather is given, assume it's measured at ht -> kw=kd=1
            double kd = 1, base1 = Math.Log(1.0 - beta * height / _temperature);
            if (interpolateWeather)
                kd = Math.Exp(base1 * NBg / (NBRd * beta));

            // compute the zenith delay for dry component
            return ((1.0e-6 * NBk1 * NBRd / gm) * kd * _pressure);
        }

        // Compute and return the zenith delay for wet component of the troposphere
        public override double WetZenithDelay()
        {
            Validate();
            double beta = NB_Interpolate(latitude, doy, TableEntry.ZB);
            double lam = NB_Interpolate(latitude, doy, TableEntry.ZL);
            double gm = 9.784 * (1.0 - 2.66e-3 * Math.Cos(2.0 * latitude * C.DEG_TO_RAD) - 2.8e-7 * height);

            // scale factors for height above mean sea level
            // if weather is given, assume it's measured at ht -> kw=kd=1
            double kw = 1, base1 = Math.Log(1.0 - beta * height / _temperature);
            if (interpolateWeather)
                kw = Math.Exp(base1 * (-1.0 + (lam + 1) * NBg / (NBRd * beta)));

            // compute the zenith delay for wet component
            return ((1.0e-6 * NBk3p * NBRd / (gm * (lam + 1) - beta * NBRd)) * kw * _humidity / _temperature);
        }
        // Compute and return the mapping function for dry component
        // of the troposphere
        // @param elevation Elevation of satellite as seen at receiver,
        //                  in degrees
        public override double DryMappingFunction(double elevation)
        {
            Validate();

            if (elevation < 0.0) return 0.0;

            double a, b, c, se, map;
            se = Math.Sin(elevation * C.DEG_TO_RAD);

            a = NB_Interpolate(latitude, doy, TableEntry.Mad);
            b = NB_Interpolate(latitude, doy, TableEntry.Mbd);
            c = NB_Interpolate(latitude, doy, TableEntry.Mcd);
            map = (1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c)));

            a = 2.53e-5;
            b = 5.49e-3;
            c = 1.14e-3;
            if (Math.Abs(elevation) <= 0.001) se = 0.001;
            map += ((1.0 / se) - (1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c)))) * height / 1000.0;

            return map;
        }

        // Compute and return the mapping function for wet component
        // of the troposphere
        // @param elevation Elevation of satellite as seen at receiver,
        //                  in degrees
        public override double WetMappingFunction(double elevation)
        {
            Validate();
            if (elevation < 0.0) return 0.0;

            double a, b, c, se;
            se = Math.Sin(elevation * C.DEG_TO_RAD);
            a = NB_Interpolate(latitude, doy, TableEntry.Maw);
            b = NB_Interpolate(latitude, doy, TableEntry.Mbw);
            c = NB_Interpolate(latitude, doy, TableEntry.Mcw);

            return ((1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c))));
        }

        #endregion
    }

    // ------------------------------------------------------------------------
    // Saastamoinen tropospheric model.
    // This model needs work; it is not the Saastamoinen model, but appears to be
    // a combination of the Neill mapping functions and an unknown delay model.
    // Based on Saastamoinen, J., 'Atmospheric
    // Correction for the Troposphere and Stratosphere in Radio Ranging of
    // Satellites,' Geophysical Monograph 15, American Geophysical Union, 1972,
    // and Ch. 9 of McCarthy, D. and Petit, G., IERS Conventions (2003), IERS
    // Technical Note 32, IERS, 2004. The mapping functions are from
    // Neill, A.E., 1996, 'Global Mapping Functions for the Atmosphere Delay of
    // Radio Wavelengths,' J. Geophys. Res., 101, pp. 3227-3246 (also see IERS TN 32).
    //
    // This model includes a wet and dry component, and requires input of the
    // geodetic latitude, day of year and height above the ellipsoid of the receiver.
    public class SaasTropModel : TropModel
    {
        #region 1-Data Field
        double latitude;              /// latitude (deg) of receiver
        int doy;                      /// day of year
        bool validRxLatitude;
        bool validRxHeight;
        bool validDOY;
        // constants for wet mapping function
        static double[] SaasWetA = new double[5] { 0.00058021897, 0.00056794847, 0.00058118019, 0.00059727542, 0.00061641693 };
        static double[] SaasWetB = new double[5] { 0.0014275268, 0.0015138625, 0.0014572752, 0.0015007428, 0.0017599082 };
        static double[] SaasWetC = new double[5] { 0.043472961, 0.046729510, 0.043908931, 0.044626982, 0.054736038 };

        // constants for dry mapping function
        static double[] SaasDryA = new double[5] { 0.0012769934, 0.0012683230, 0.0012465397, 0.0012196049, 0.0012045996 };
        static double[] SaasDryB = new double[5] { 0.0029153695, 0.0029152299, 0.0029288445, 0.0029022565, 0.0029024912 };
        static double[] SaasDryC = new double[5] { 0.062610505, 0.062837393, 0.063721774, 0.063824265, 0.064258455 };

        static double[] SaasDryA1 = new double[5] { 0.0, 0.000012709626, 0.000026523662, 0.000034000452, 0.000041202191 };
        static double[] SaasDryB1 = new double[5] { 0.0, 0.000021414979, 0.000030160779, 0.000072562722, 0.00011723375 };
        static double[] SaasDryC1 = new double[5] { 0.0, 0.000090128400, 0.000043497037, 0.00084795348, 0.0017037206 };

        #endregion

        #region 2-Constructor
        // Default constructor
        public SaasTropModel()
        {
            validWeather = false;
            validRxLatitude = false;
            validDOY = false;
            validRxHeight = false;
        }

        // Create a trop model using the minimum information: latitude and doy.
        // Interpolate the weather unless setWeather (optional) is called.
        // @param lat Latitude of the receiver in degrees.
        // @param day Day of year.
        public SaasTropModel(double lat, int day)
        {
            validWeather = false;
            validRxHeight = false;
            SetReceiverLatitude(lat);
            SetDayOfYear(day);
        } // end SaasTropModel::SaasTropModel


        // Create a tropospheric model from explicit weather data
        // @param lat Latitude of the receiver in degrees.
        // @param day Day of year.
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public SaasTropModel(double lat, int day, double T,
                                      double P, double H)
        {
            validRxHeight = false;
            SetReceiverLatitude(lat);
            SetDayOfYear(day);
            SetWeather(T, P, H);
        }
        #endregion

        #region 3-Set

        // Re-define the weather data.
        // If called, typically called before any calls to correction().
        // @param T temperature in degrees Celsius
        // @param P atmospheric pressure in millibars
        // @param H relative humidity in percent
        public new void SetWeather(double T, double P, double H)
        {
            _temperature = T;
            _pressure = P;
            // humid actually stores water vapor partial pressure
            double exp = 7.5 * T / (T + 237.3);
            _humidity = 6.11 * (H / 100.0) * Math.Pow(10.0, exp);

            validWeather = true;

        }


        // Define the receiver height; this required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        public void SetReceiverHeight(double ht)
        {
            _height = ht;
            validRxHeight = true;

        }

        // Define the latitude of the receiver; this is required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        public void SetReceiverLatitude(double lat)
        {
            latitude = lat;
            validRxLatitude = true;
        }

        // Define the day of year; this is required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        public void SetDayOfYear(int d)
        {
            doy = d;
            if (doy > 0 && doy < 367)
                validDOY = true;
            else
                validDOY = false;
        }

        #endregion

        #region 4-Validate

        private void Validate()
        {
            if (!validWeather)
                throw new Exception("Invalid Saastamoinen trop model: weather. --In " + FileName());
            if (!validRxLatitude)
                throw new Exception("Invalid Saastamoinen trop model: Rx Latitude. --In " + FileName());
            if (!validRxHeight)
                throw new Exception("Invalid Saastamoinen trop model: Rx Height. --In " + FileName());
            if (!validDOY)
                throw new Exception("Invalid Saastamoinen trop model: day of year. --In " + FileName());

        }
        #endregion

        #region 5-Correction

        // re-define this to get the throws correct
        public new double Correction(double elevation)
        {
            Validate();

            return base.Correction(elevation);

        }
        // Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite and the time tag. This version is most useful
        // within positioning algorithms, where the receiver position and timetag
        // may vary; it computes the elevation (and other receiver location
        // information) and passes them to appropriate set...() routines
        // and the correction(elevation) routine.
        // @param RX  Receiver position
        // @param SV  Satellite position
        // @param tt  Time tag of the signal
        public double Correction(Position RX, Position SV, Time tt)
        {
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);
            SetDayOfYear(tt.Doy);

            Validate();

            return Correction(RX.Elevation(SV));
        }
        public double Correction(Position RX, Position SV)
        {
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);
            Validate();
            return Correction(RX.Elevation(SV));
        }
        #endregion

        #region 6-Override

        // Compute and return the zenith delay for dry component of the troposphere
        public override double DryZenithDelay()
        {
            Validate();
            double delay = 0.0022768 * _pressure //_at_h
              / (1 - 0.00266 * Math.Cos(2 * latitude * C.DEG_TO_RAD) - 0.00028 * _height / 1000.0);

            return delay;
        }

        // Compute and return the zenith delay for wet component of the troposphere
        public override double WetZenithDelay()
        {
            Validate();
            // press is zero for the wet component
            double delay = 0.0022768 * _humidity * 1255 / ((_temperature + CELSIUS_TO_KELVIN) + 0.05)
                  / (1 - 0.00266 * Math.Cos(2 * latitude * C.DEG_TO_RAD) - 0.00028 * _height / 1000.0);

            return delay;
        }

        // Compute and return the mapping function for dry component of the troposphere
        // @param elevation Elevation of satellite as seen at receiver, in degrees
        public override double DryMappingFunction(double elevation)
        {
            Validate();

            if (elevation < 0.0) return 0.0;

            double lat, t, ct;
            lat = Math.Abs(latitude);         // degrees
            t = doy - 28.0;                // mid-winter
            if (latitude < 0)              // southern hemisphere
                t += 365.25 / 2.0;
            t *= 360.0 / 365.25;            // convert to degrees
            ct = Math.Cos(t * C.DEG_TO_RAD);

            double a, b, c;
            if (lat < 15.0)
            {
                a = SaasDryA[0];
                b = SaasDryB[0];
                c = SaasDryC[0];
            }
            else if (lat < 75.0)
            {          // coefficients are for 15,30,45,60,75 deg
                int i = (int)(lat / 15.0) - 1;
                double frac = (lat - 15.0 * (i + 1)) / 15.0;
                a = SaasDryA[i] + frac * (SaasDryA[i + 1] - SaasDryA[i]);
                b = SaasDryB[i] + frac * (SaasDryB[i + 1] - SaasDryB[i]);
                c = SaasDryC[i] + frac * (SaasDryC[i + 1] - SaasDryC[i]);

                a -= ct * (SaasDryA1[i] + frac * (SaasDryA1[i + 1] - SaasDryA1[i]));
                b -= ct * (SaasDryB1[i] + frac * (SaasDryB1[i + 1] - SaasDryB1[i]));
                c -= ct * (SaasDryC1[i] + frac * (SaasDryC1[i + 1] - SaasDryC1[i]));
            }
            else
            {
                a = SaasDryA[4] - ct * SaasDryA1[4];
                b = SaasDryB[4] - ct * SaasDryB1[4];
                c = SaasDryC[4] - ct * SaasDryC1[4];
            }

            double se = Math.Sin(elevation * C.DEG_TO_RAD);
            double map = (1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c)));

            a = 0.0000253;
            b = 0.00549;
            c = 0.00114;
            map += (_height / 1000.0) * (1.0 / se - (1 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c))));

            return map;
        }

        // Compute and return the mapping function for wet component of the troposphere
        // @param elevation Elevation of satellite as seen at receiver, in degrees.
        public override double WetMappingFunction(double elevation)
        {
            Validate();

            if (elevation < 0.0) return 0.0;

            double a, b, c, lat;
            lat = Math.Abs(latitude);         // degrees
            if (lat < 15.0)
            {
                a = SaasWetA[0];
                b = SaasWetB[0];
                c = SaasWetC[0];
            }
            else if (lat < 75.0)
            {          // coefficients are for 15,30,45,60,75 deg
                int i = (int)(lat / 15.0) - 1;
                double frac = (lat - 15.0 * (i + 1)) / 15.0;
                a = SaasWetA[i] + frac * (SaasWetA[i + 1] - SaasWetA[i]);
                b = SaasWetB[i] + frac * (SaasWetB[i + 1] - SaasWetB[i]);
                c = SaasWetC[i] + frac * (SaasWetC[i + 1] - SaasWetC[i]);
            }
            else
            {
                a = SaasWetA[4];
                b = SaasWetB[4];
                c = SaasWetC[4];
            }

            double se = Math.Sin(elevation * C.DEG_TO_RAD);
            double map = (1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c)));

            return map;
        }
        #endregion

    }

    //  Tropospheric model implemented in "GPS Code Analysis Tool" (GCAT)
    //  software.
    // 
    //  This model is described in the book "GPS Data processing: code and
    //  phase Algorithms, Techniques and Recipes" by Hernandez-Pajares, M.,
    //  J.M. Juan-Zornoza and Sanz-Subirana, J. See Chapter 5.
    // 
    //  This book and associated software are freely available at:
    // 
    //  http://gage152.upc.es/~manuel/tdgps/tdgps.html
    // 
    //  This is a simple but efective model composed of the wet and dry
    //  vertical tropospheric delays as defined in Gipsy/Oasis-II GPS
    //  analysis software, and the mapping function as defined by Black and
    //  Eisner (H. D. Black, A. Eisner. Correcting Satellite Doppler
    //  Data for Tropospheric Effects. Journal of  Geophysical Research.
    //  Vol 89. 1984.) and used in MOPS (RTCA/DO-229C) standards.
    // 
    //  Usually, the caller will set the receiver height using
    //  setReceiverHeight() method, and then call the correction() method
    //  with the satellite elevation as parameter.
    public class GCATTropModel : TropModel
    {
        #region 1-Data Field & Construct
        /// Receiver height
        double gcatHeight;
        // @param ht Height of the receiver above mean sea level, in meters.
        public GCATTropModel(double ht)
        {
            SetReceiverHeight(ht);
        }
        #endregion

        #region 2-Correction
        ///Compute and return the full tropospheric delay. The receiver height
        // must has been provided before, whether using the appropriate
        // constructor or with the setReceiverHeight() method.
        //
        // @param elevation  Elevation of satellite as seen at receiver, in
        //                   degrees
        public new double Correction(double elevation)
        {
            if (elevation < 5.0) return 0.0;

            return ((DryZenithDelay() + WetZenithDelay()) *
                     MappingFunction(elevation));
        }

        //  Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite. This version is most useful within positioning
        // algorithms, where the receiver position may vary; it computes the
        // elevation and the receiver height and passes them to appropriate
        // set...() routines and the correction(elevation) routine.
        //
        // @param RX  Receiver position in ECEF cartesian coordinates (meters)
        // @param SV  Satellite position in ECEF cartesian coordinates (meters)
        public new double Correction(Position RX, Position SV)
        {
            SetReceiverHeight(RX.Altitude);
            double result = Correction(RX.ElevationGeodetic(SV));
            return result;
        }
        #endregion

        #region 3-override
        // Compute and return the zenith delay for the dry component of the
        // troposphere.
        public override double DryZenithDelay()
        {
            double ddry = 2.29951 * Math.Exp((-0.000116 * gcatHeight));
            return ddry;
        }
        // Compute and return the mapping function of the troposphere
        //@param elevation  Elevation of satellite as seen at receiver,
        //                  in degrees
        public double MappingFunction(double elevation)
        {
            if (elevation < 5.0) return 0.0;

            double d = Math.Sin(elevation * C.DEG_TO_RAD);
            d = Math.Sqrt(0.002001 + (d * d));
            return (1.001 / d);
        }
        public override double WetZenithDelay()
        {
            return 0.1;
        }

        public override double DryMappingFunction(double elevation)
        {
            return MappingFunction(elevation);
        }

        public override double WetMappingFunction(double elevation)
        {
            return MappingFunction(elevation);
        }
        #endregion

        #region 4-Set
        // Define the receiver height; this is required before calling
        // correction() or any of the zenith_delay or mapping_function routines.
        // @param ht Height of the receiver above mean sea level, in meters.
        void SetReceiverHeight(double ht)
        {
            gcatHeight = ht;
        }
        #endregion
    }


    //  Tropospheric model implemented in the RTCA "Minimum Operational
    //  Performance Standards" (MOPS), version C.
    // 
    // This model is described in the RTCA "Minimum Operational Performance
    // Standards" (MOPS), version C (RTCA/DO-229C), in Appendix A.4.2.4.
    // Although originally developed for SBAS systems (EGNOS, WAAS), it may
    // be suitable for other uses as well.
    // 
    // This model needs the day of year, satellite elevation (degrees),
    // receiver height over mean sea level (meters) and receiver latitude in
    // order to start computing.
    // 
    // On the other hand, the outputs are the tropospheric correction (in
    // meters) and the sigma-squared of tropospheric delay residual error
    // (meters^2).

    public class MOPSTropModel : TropModel
    {
        #region 1-Data Field
        double MOPSHeight;
        double MOPSLat;
        int MOPSTime;
        bool validHeight;
        bool validLat;
        bool validTime;
        double[,] avr;
        double[,] svr;
        double[] fi0;
        double[] MOPSParameters;

        // Some specific constants
        const double MOPSg = 9.80665;
        const double MOPSgm = 9.784;
        const double MOPSk1 = 77.604;
        const double MOPSk2 = 382000.0;
        const double MOPSRd = 287.054;
        #endregion

        #region 2-Constructor
        public MOPSTropModel()
        {
            validHeight = false;
            validLat = false;
            validTime = false;
        }
        // Constructor to create a MOPS trop model providing the height of
        // the receiver above mean sea level (as defined by ellipsoid model),
        // its latitude and the day of year.
        //
        //@param ht   Height of the receiver above mean sea level, in meters.
        //@param lat  Latitude of receiver, in degrees.
        //@param doy  Day of year.
        public MOPSTropModel(double ht, double lat, int doy)
        {
            SetReceiverHeight(ht);
            SetReceiverLatitude(lat);
            SetDayOfYear(doy);
        }

        //Constructor to create a MOPS trop model providing the position of
        // the receiver and current time.
        //
        //@param RX   Receiver position.
        //@param time Time.
        public MOPSTropModel(Position RX, Time time)
        {
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);
            SetDayOfYear(time);
        }
        #endregion

        #region 3-Validate
        private void Validate()
        {
            if (!validLat)
            {
                throw new Exception(
               "MOPSTropModel must have Rx latitude before computing weather. --In " + FileName());
            }

            if (!validTime)
            {
                throw new Exception(
                   "MOPSTropModel must have day of year before computing weather. --In " + FileName());
            }

        }
        #endregion

        #region 4-Set

        // This method configure the model to estimate the weather using height,
        // latitude and day of year (DOY). It is called automatically when
        // setting those parameters.
        void SetWeather()
        {
            Validate();

            // In order to compute tropospheric delay we need to compute some
            // extra parameters
            PrepareParameters();

        }

        // Define the receiver height; this is required before calling
        //  correction() or any of the zenith_delay routines.
        //
        // @param ht   Height of the receiver above mean sea level, in meters.
        ///
        public void SetReceiverHeight(double ht)
        {
            MOPSHeight = ht;
            validHeight = true;

            // Change the value of field "valid" if everything is already set
            bool valid = validHeight && validLat && validTime;

            // If model is valid, set the appropriate parameters
            if (valid) SetWeather();

        }

        // Define the receiver latitude; this is required before calling
        //  correction() or any of the zenith_delay routines.
        //
        // @param lat  Latitude of receiver, in degrees.
        ///
        public void SetReceiverLatitude(double lat)
        {
            MOPSLat = lat;
            validLat = true;

            // Change the value of field "valid" if everything is already set
            bool valid = validHeight && validLat && validTime;

            // If model is valid, set the appropriate parameters
            if (valid) SetWeather();

        }


        // Set the time when tropospheric correction will be computed for, in
        //  days of the year.
        //
        // @param doy  Day of the year.
        ///
        public void SetDayOfYear(int doy)
        {

            if ((doy >= 1) && (doy <= 366))
            {
                validTime = true;
            }
            else
            {
                validTime = false;
            }

            MOPSTime = doy;

            // Change the value of field "valid" if everything is already set
            bool valid = validHeight && validLat && validTime;

            // If model is valid, set the appropriate parameters
            if (valid) SetWeather();
        }


        /// Set the time when tropospheric correction will be computed for, in
        //   days of the year.
        // 
        //  @param time  Time object.
        //
        void SetDayOfYear(Time time)
        {
            MOPSTime = time.Doy;
            validTime = true;

            // Change the value of field "valid" if everything is already set
            bool valid = validHeight && validLat && validTime;

            // If model is valid, set the appropriate parameters
            if (valid) SetWeather();

        }


        // Convenient method to set all model parameters in one pass.
        //
        // @param time  Time object.
        // @param rxPos Receiver position object.
        public void SetAllParameters(Time time, Position rxPos)
        {

            MOPSTime = time.Doy;
            validTime = true;
            MOPSLat = rxPos.GeodeticLatitude;
            validHeight = true;
            MOPSHeight = rxPos.Height;
            validLat = true;

            // Change the value of field "valid" if everything is already set
            bool valid = validHeight && validLat && validTime;

            // If model is valid, set the appropriate parameters
            if (valid) SetWeather();

        }

        #endregion

        #region 5-PrepareData

        // Compute and return the sigma-squared value of tropospheric delay
        // residual error (meters^2)
        // @param elevation  Elevation of satellite as seen at receiver,
        //                   in degrees
        public double MOPSsigma2(double elevation)
        {

            double map_f;

            // If elevation is below bounds, fail in a sensible way returning a
            // very big sigma value
            if (elevation < 5.0)
            {
                return 9.9e9;
            }
            else
            {
                map_f = MappingFunction(elevation);
            }

            // Compute residual error for tropospheric delay
            double MOPSsigma2trop = (0.12 * map_f) * (0.12 * map_f);

            return MOPSsigma2trop;

        }  // end MOPSTropModel::MOPSsigma(elevation)


        // The MOPS tropospheric model needs to compute several extra parameters
        private void PrepareParameters()
        {


            // We need to read some data
            PrepareTables();

            // Declare some variables
            int idmin, j, index = 0;
            double fact, axfi;
            var avr0 = new double[5];
            var svr0 = new double[5];

            // Resize MOPSParameters as appropriate
            MOPSParameters = new double[5];

            if (MOPSLat >= 0.0)
            {
                idmin = 28;
            }
            else
            {
                idmin = 211;
            }

            // Fraction of the year in radians
            fact = 2.0 * Math.PI * ((double)(MOPSTime - idmin)) / 365.25;

            axfi = Math.Abs(MOPSLat);

            if (axfi <= 15.0) index = 0;
            if ((axfi > 15.0) && (axfi <= 30.0)) index = 1;
            if ((axfi > 30.0) && (axfi <= 45.0)) index = 2;
            if ((axfi > 45.0) && (axfi <= 60.0)) index = 3;
            if ((axfi > 60.0) && (axfi < 75.0)) index = 4;
            if (axfi >= 75.0) index = 5;

            for (j = 0; j < 5; j++)
            {
                if (index == 0)
                {
                    avr0[j] = avr[index, j];
                    svr0[j] = svr[index, j];
                }
                else
                {
                    if (index < 5)
                    {
                        avr0[j] = avr[index - 1, j] + (avr[index, j] - avr[index - 1, j]) *
                                  (axfi - fi0[index - 1]) / (fi0[index] - fi0[index - 1]);

                        svr0[j] = svr[index - 1, j] + (svr[index, j] - svr[index - 1, j]) *
                                  (axfi - fi0[index - 1]) / (fi0[index] - fi0[index - 1]);
                    }
                    else
                    {
                        avr0[j] = avr[index - 1, j];
                        svr0[j] = svr[index - 1, j];
                    }
                }

                MOPSParameters[j] = avr0[j] - svr0[j] * Math.Cos(fact);
            }



        }  // end MOPSTropModel::prepareParameters()


        // The MOPS tropospheric model uses several predefined data tables
        private void PrepareTables()
        {
            avr = new double[5,5];
            svr = new double[5,5];
            fi0 = new double[5];


            // Table avr (Average):

            avr[0, 0] = 1013.25; avr[0, 1] = 299.65; avr[0, 2] = 26.31;
            avr[0, 3] = 0.0063; avr[0, 4] = 2.77;

            avr[1, 0] = 1017.25; avr[1, 1] = 294.15; avr[1, 2] = 21.79;
            avr[1, 3] = 0.00605; avr[1, 4] = 3.15;

            avr[2, 0] = 1015.75; avr[2, 1] = 283.15; avr[2, 2] = 11.66;
            avr[2, 3] = 0.00558; avr[2, 4] = 2.57;

            avr[3, 0] = 1011.75; avr[3, 1] = 272.15; avr[3, 2] = 6.78;
            avr[3, 3] = 0.00539; avr[3, 4] = 1.81;

            avr[4, 0] = 1013.00; avr[4, 1] = 263.65; avr[4, 2] = 4.11;
            avr[4, 3] = 0.00453; avr[4, 4] = 1.55;


            // Table svr [Seasonal Variation]:

            svr[0, 0] = 0.00; svr[0, 1] = 0.00; svr[0, 2] = 0.00;
            svr[0, 3] = 0.00000; svr[0, 4] = 0.00;

            svr[1, 0] = -3.75; svr[1, 1] = 7.00; svr[1, 2] = 8.85;
            svr[1, 3] = 0.00025; svr[1, 4] = 0.33;

            svr[2, 0] = -2.25; svr[2, 1] = 11.00; svr[2, 2] = 7.24;
            svr[2, 3] = 0.00032; svr[2, 4] = 0.46;

            svr[3, 0] = -1.75; svr[3, 1] = 15.00; svr[3, 2] = 5.36;
            svr[3, 3] = 0.00081; svr[3, 4] = 0.74;

            svr[4, 0] = -0.50; svr[4, 1] = 14.50; svr[4, 2] = 3.39;
            svr[4, 3] = 0.00062; svr[4, 4] = 0.30;


            // Table fi0 [Latitude bands]:

            fi0[0] = 15.0; fi0[1] = 30.0; fi0[2] = 45.0;
            fi0[3] = 60.0; fi0[4] = 75.0;

        }

        #endregion

        #region 6-Correction
        // Compute and return the full tropospheric delay. The receiver height,
        // latitude and Day oy Year must has been set before using the
        // appropriate constructor or the provided methods.
        // @param elevation Elevation of satellite as seen at receiver, in
        // degrees
        public new double Correction(double elevation)
        {
            Validate();

            if (elevation < 5.0) return 0.0;

            double map = MappingFunction(elevation);

            // Compute tropospheric delay
            double tropDelay = (DryZenithDelay() + WetZenithDelay()) * map;

            return tropDelay;

        }
        // Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite. This version is most useful within
        // positioning algorithms, where the receiver position may vary; it
        // computes the elevation (and other receiver location information as
        // height and latitude) and passes them to appropriate methods. You must
        // set time using method setDayOfYear() before calling this method.
        // @param RX  Receiver position
        // @param SV  Satellite position
        public new double Correction(Position RX, Position SV)
        {
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);
            SetWeather();
            return Correction(RX.ElevationGeodetic(SV));
        }


        // Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite and the time tag. This version is most useful
        // within positioning algorithms, where the receiver position may vary;
        // it computes the elevation (and other receiver location information as
        // height  and latitude) and passes them to appropriate methods.
        // @param RX  Receiver position in ECEF cartesian coordinates (meters)
        // @param SV  Satellite position in ECEF cartesian coordinates (meters)
        // @param tt  Time (CommonTime object).
        public double Correction(Position RX, Position SV, Time time)
        {
            SetDayOfYear(time.Doy);
            return Correction(RX, SV);
        }

        // Compute and return the full tropospheric delay, given the positions of
        // receiver and satellite and the day of the year. This version is most
        // useful within positioning algorithms, where the receiver position may
        // vary; it computes the elevation (and other receiver location
        // information as height and latitude) and passes them to appropriate
        // methods.
        // @param RX  Receiver position in ECEF cartesian coordinates (meters)
        // @param SV  Satellite position in ECEF cartesian coordinates (meters)
        // @param doy Day of year.
        public double Correction(Position RX, Position SV, int doy)
        {
            SetDayOfYear(doy);

            return Correction(RX, SV);
        }
        #endregion

        #region 7-override
        public override double DryZenithDelay()
        {
            Validate();
            double ddry, zh_dry, exponent;

            // Set the extra parameters
            double P = MOPSParameters[0];
            double T = MOPSParameters[1];
            double beta = MOPSParameters[3];

            // Zero-altitude dry zenith delay:
            zh_dry = 0.000001 * (MOPSk1 * MOPSRd) * P / MOPSgm;

            // Zenith delay terms at MOPSHeight meters of height above mean sea
            // level
            exponent = MOPSg / MOPSRd / beta;
            ddry = zh_dry * Math.Pow((1.0 - beta * MOPSHeight / T), exponent);

            return ddry;
        }

        public override double WetZenithDelay()
        {

            Validate();

            double dwet, zh_wet, exponent;

            // Set the extra parameters
            double T = MOPSParameters[1];
            double e = MOPSParameters[2];
            double beta = MOPSParameters[3];
            double lambda = MOPSParameters[4];

            // Zero-altitude wet zenith delay:
            zh_wet = (0.000001 * MOPSk2) * MOPSRd / (MOPSgm * (lambda + 1.0) - beta * MOPSRd) * e / T;

            // Zenith delay terms at MOPSHeight meters of height above mean sea
            // level
            exponent = ((lambda + 1.0) * MOPSg / MOPSRd / beta) - 1.0;
            dwet = zh_wet * Math.Pow((1.0 - beta * MOPSHeight / T), exponent);

            return dwet;
        }

        // Compute and return the mapping function of the troposphere
        //@param elevation  Elevation of satellite as seen at receiver,
        //                  in degrees
        public double MappingFunction(double elevation)
        {
            if (elevation < 5.0) return 0.0;

            double d = Math.Sin(elevation * C.DEG_TO_RAD);
            d = Math.Sqrt(0.002001 + (d * d));
            return (1.001 / d);
        }

        public override double DryMappingFunction(double elevation)
        {
            return MappingFunction(elevation);
        }

        public override double WetMappingFunction(double elevation)
        {
            return MappingFunction(elevation);
        }
        #endregion
    }

    // Tropospheric model based in the Neill mapping functions.
    // This model uses the mapping functions developed by A.E. Niell and
    // published in Neill, A.E., 1996, 'Global Mapping Functions for the
    // Atmosphere Delay of Radio Wavelengths,' J. Geophys. Res., 101,
    // pp. 3227-3246 (also see IERS TN 32).
    //
    // The coefficients of the hydrostatic mapping function depend on the
    // latitude and height above sea level of the receiver station, and on
    // the day of the year. On the other hand, the wet mapping function
    // depends only on latitude.
    //
    // This mapping is independent from surface meteorology, while having
    // comparable accuracy and precision to those that require such data.
    // This characteristic makes this model very useful, and it is
    // implemented in geodetic software such as JPL's Gipsy/OASIS.
    //    @warning The Neill mapping functions are defined for elevation
    // angles down to 3 degrees.
    public class NeillTropModel : TropModel
    {
        #region 1-Data Field
        double NeillHeight;
        double NeillLat;
        int NeillDOY;
        bool validHeight;
        bool validLat;
        bool validDOY;

        // Parameters borrowed from Saastamoinen tropospheric model
        // Constants for wet mapping function
        static double[] NeillWetA = new double[5] 
                         { 0.00058021897, 0.00056794847, 0.00058118019,
                           0.00059727542, 0.00061641693 };
        static double[] NeillWetB = new double[5]
                         { 0.0014275268, 0.0015138625, 0.0014572752,
                           0.0015007428, 0.0017599082 };
        static double[] NeillWetC = new double[5]
                              { 0.043472961, 0.046729510, 0.043908931,
                                0.044626982, 0.054736038 };

        // constants for dry mapping function
        static double[] NeillDryA = new double[5]
                        { 0.0012769934, 0.0012683230, 0.0012465397,
                          0.0012196049, 0.0012045996 };
        static double[] NeillDryB = new double[5]
                        { 0.0029153695, 0.0029152299, 0.0029288445,
                          0.0029022565, 0.0029024912 };
        static double[] NeillDryC = new double[5]
                        { 0.062610505, 0.062837393, 0.063721774,
                          0.063824265, 0.064258455 };

        static double[] NeillDryA1 = new double[5]
                        { 0.0, 0.000012709626, 0.000026523662,
                          0.000034000452, 0.000041202191 };
        static double[] NeillDryB1 = new double[5]
                        { 0.0, 0.000021414979, 0.000030160779,
                          0.000072562722, 0.00011723375 };
        static double[] NeillDryC1 = new double[5]
                              { 0.0, 0.000090128400, 0.000043497037,
                                0.00084795348, 0.0017037206 };

        #endregion

        #region 2-Constructor

        /// Default constructor
        public NeillTropModel()
        {
            validHeight = false;
            validLat = false;
            validDOY = false;
        }

        /// Constructor to create a Neill trop model providing just the
        /// height of the receiver above mean sea level. The other
        /// parameters must be set with the appropriate set methods before
        /// calling correction methods.
        ///
        /// @param ht   Height of the receiver above mean sea level, in
        ///             meters.
        public NeillTropModel(double ht)
        {
            SetReceiverHeight(ht);

        }
        /// Constructor to create a Neill trop model providing the height of
        /// the receiver above mean sea level (as defined by ellipsoid
        /// model), its latitude and the day of year.
        ///
        /// @param ht   Height of the receiver above mean sea level,
        ///             in meters.
        /// @param lat  Latitude of receiver, in degrees.
        /// @param doy  Day of year.
        public NeillTropModel(double ht, double lat, int doy)
        {
            SetReceiverHeight(ht);
            SetReceiverLatitude(lat);
            SetDayOfYear(doy);
        }
        public NeillTropModel(Position RX, Time time)
        {
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);
            SetDayOfYear(time);
        }
        #endregion

        #region 3-validate
        // This method configure the model to estimate the weather using height,
        // latitude and day of year (DOY). It is called automatically when
        // setting those parameters.
        public void Validate()
        {

            if (!validLat)
            {
                throw new Exception("Invalid Neill trop model: Rx Latitude. --in  " + FileName());
            }
            if (!validDOY)
            {
                throw new Exception("Invalid Neill trop model: day of year. --in  " + FileName());
            }
            if (!validHeight)
            {
                throw new Exception("Invalid Neill trop model: Rx Height. --in  " + FileName());
            }

        }

        #endregion

        #region 4-Set

        // Define the receiver height; this is required before calling
        // correction() or any of the zenith_delay routines.
        //
        // @param ht   Height of the receiver above mean sea level,
        //             in meters.
        public void SetReceiverHeight(double ht)
        {
            NeillHeight = ht;
            validHeight = true;

        }


        // Define the receiver latitude; this is required before calling
        // correction() or any of the zenith_delay routines.
        //
        // @param lat  Latitude of receiver, in degrees.
        public void SetReceiverLatitude(double lat)
        {
            NeillLat = lat;
            validLat = true;

        }


        // Set the time when tropospheric correction will be computed for,
        // in days of the year.
        //
        // @param doy  Day of the year.
        public void SetDayOfYear(int doy)
        {

            if ((doy >= 1) && (doy <= 366))
            {
                validDOY = true;
            }
            else
            {
                validDOY = false;
            }

            NeillDOY = doy;


        }


        // Set the time when tropospheric correction will be computed for,
        // in days of the year.
        //
        // @param time  Time object.
        public void SetDayOfYear(Time time)
        {

            NeillDOY = time.Doy;
            validDOY = true;
        }


        //Convenient method to set all model parameters in one pass.
        //@param time  Time object.
        //@param rxPos Receiver position object.
        public void SetAllParameters(Time time, Position rxPos)
        {

            NeillDOY = time.Doy;
            validDOY = true;
            NeillLat = rxPos.GeodeticLatitude;
            validHeight = true;
            NeillLat = rxPos.Height;
            validLat = true;

        }
        #endregion

        #region 5-Correction

        // Compute and return the full tropospheric delay. The receiver height,
        // latitude and Day oy Year must has been set before using the
        // appropriate constructor or the provided methods.
        //
        // @param elevation Elevation of satellite as seen at receiver,
        // in degrees
        public new double Correction(double elevation)
        {
            Validate();
            // Neill mapping functions work down to 3 degrees of elevation
            if (elevation < 3.0)
            {
                return 0.0;
            }

            double map_dry = DryMappingFunction(elevation);
            double map_wet = WetMappingFunction(elevation);

            // Compute tropospheric delay
            double tropDelay = DryZenithDelay() * map_dry +
                             WetZenithDelay() * map_wet;

            return tropDelay;
        }

        //  Compute and return the full tropospheric delay, given the
        // positions of receiver and satellite.
        //
        // This version is more useful within positioning algorithms, where
        // the receiver position may vary; it computes the elevation (and
        // other receiver location information as height and latitude) and
        // passes them to appropriate methods.
        //
        // You must set time using method setReceiverDOY() before calling
        // this method.
        //
        // @param RX  Receiver position.
        // @param SV  Satellite position.
        public new double Correction(Position RX, Position SV)
        {
            SetReceiverHeight(RX.Altitude);
            SetReceiverLatitude(RX.GeodeticLatitude);
            return Correction(RX.ElevationGeodetic(SV));
        }

        // Compute and return the full tropospheric delay, given the
        // positions of receiver and satellite and the time tag.
        //
        // This version is more useful within positioning algorithms, where
        // the receiver position may vary; it computes the elevation (and
        // other receiver location information as height and latitude), and
        // passes them to appropriate methods.
        //
        // @param RX  Receiver position.
        // @param SV  Satellite position.
        // @param tt  Time (CommonTime object).
        public double Correction(Position RX, Position SV, Time tt)
        {
            SetDayOfYear(tt);
            return Correction(RX, SV);

        }

        //Compute and return the full tropospheric delay, given the
        //positions of receiver and satellite and the day of the year.
        //
        //This version is more useful within positioning algorithms, where
        //the receiver position may vary; it computes the elevation (and
        //other receiver location information as height and latitude), and
        //passes them to appropriate methods.
        //
        //@param RX  Receiver position.
        //@param SV  Satellite position.
        //@param doy Day of year.
        //
        public double Correction(Position RX, Position SV, int doy)
        {
            SetDayOfYear(doy);
            return Correction(RX, SV);
        }

        #endregion

        #region 6-override

        public override double DryZenithDelay()
        {
            // Note: 1.013*2.27 = 2.29951
            double ddry = 2.29951 * Math.Exp((-0.000116 * NeillHeight));
            return ddry;
        }

        public override double WetZenithDelay()
        {
            return 0.1;
        }

        public override double DryMappingFunction(double elevation)
        {
            Validate();

            if (elevation < 3.0)
            {
                return 0.0;
            }

            double lat, t, ct;
            lat = Math.Abs(NeillLat);         // degrees
            t = NeillDOY - 28.0;  // mid-winter

            if (NeillLat < 0.0)              // southern hemisphere
            {
                t += 365.25 / 2.0;
            }

            t *= 360.0 / 365.25;            // convert to degrees
            ct = Math.Cos(t * C.DEG_TO_RAD);

            double a, b, c;
            if (lat < 15.0)
            {
                a = NeillDryA[0];
                b = NeillDryB[0];
                c = NeillDryC[0];
            }
            else if (lat < 75.0)      // coefficients are for 15,30,45,60,75 deg
            {
                int i = (int)(lat / 15.0) - 1;
                double frac = (lat - 15.0 * (i + 1)) / 15.0;
                a = NeillDryA[i] + frac * (NeillDryA[i + 1] - NeillDryA[i]);
                b = NeillDryB[i] + frac * (NeillDryB[i + 1] - NeillDryB[i]);
                c = NeillDryC[i] + frac * (NeillDryC[i + 1] - NeillDryC[i]);

                a -= ct * (NeillDryA1[i] + frac * (NeillDryA1[i + 1] - NeillDryA1[i]));
                b -= ct * (NeillDryB1[i] + frac * (NeillDryB1[i + 1] - NeillDryB1[i]));
                c -= ct * (NeillDryC1[i] + frac * (NeillDryC1[i + 1] - NeillDryC1[i]));
            }
            else
            {
                a = NeillDryA[4] - ct * NeillDryA1[4];
                b = NeillDryB[4] - ct * NeillDryB1[4];
                c = NeillDryC[4] - ct * NeillDryC1[4];
            }

            double se = Math.Sin(elevation * C.DEG_TO_RAD);
            double map = (1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c)));

            a = 0.0000253;
            b = 0.00549;
            c = 0.00114;
            map += (NeillHeight / 1000.0) *
                   (1.0 / se - ((1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c)))));

            return map;
        }

        public override double WetMappingFunction(double elevation)
        {
            Validate();
            if (elevation < 3.0)
            {
                return 0.0;
            }

            double a, b, c, lat;
            lat = Math.Abs(NeillLat);         // degrees
            if (lat < 15.0)
            {
                a = NeillWetA[0];
                b = NeillWetB[0];
                c = NeillWetC[0];
            }
            else if (lat < 75.0)          // coefficients are for 15,30,45,60,75 deg
            {
                int i = (int)(lat / 15.0) - 1;
                double frac = (lat - 15.0 * (i + 1)) / 15.0;
                a = NeillWetA[i] + frac * (NeillWetA[i + 1] - NeillWetA[i]);
                b = NeillWetB[i] + frac * (NeillWetB[i + 1] - NeillWetB[i]);
                c = NeillWetC[i] + frac * (NeillWetC[i + 1] - NeillWetC[i]);
            }
            else
            {
                a = NeillWetA[4];
                b = NeillWetB[4];
                c = NeillWetC[4];
            }

            double se = Math.Sin(elevation * C.DEG_TO_RAD);
            double map = (1.0 + a / (1.0 + b / (1.0 + c))) / (se + a / (se + b / (se + c)));

            return map;
        }

        #endregion
    }

}
