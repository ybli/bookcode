using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iono
{

    public enum Frequency
    {
        L1,

        ///< L1 frequency (1575.42 MHz)
        L2 ///< L2 frequency (1227.60 MHz)
    }

    public class IonoModel
    {
        private double[] _alpha;
        private double[] _beta;
        // constructor.
        // Creates a valid model with satellite transmitted alpha
        // and beta parameters provided from almanac.
        // \param a an array containing the four alpha terms
        // \param b an array containing the four beta terms
        public IonoModel(double[] alpha, double[] beta)
        {
            SetModel(alpha, beta);
        }

        public void SetModel(double[] alpha, double[] beta)
        {
            if (alpha.Length != 4 || beta.Length != 4)
            {
                var ex = "Alpha and beta parameters is invalid. --in Method: SetModle, " + FileName();
                throw new Exception(ex);
            }
            _alpha = alpha;
            _beta = beta;
        }

        //  get the ionospheric correction value for L1 frequency
        //  \param time the time of the observation
        //  \param rxgeo the WGS84 geodetic position of the receiver
        //  \param svel the elevation angle between the rx and SV (degrees)
        //  \param svaz the azimuth angle between the rx and SV (degrees)
        //  \return the ionospheric correction (meters)
        public double GetCorrectionL1(Time time, Position rxgeo,
            double svel, double svaz)
        {

            // all angle units are in semi-circles (radians / TWO_PI)
            // Note: math functions (cos, sin, etc.) require arguments in
            // radians so all semi-circles must be multiplied by TWO_PI

            double azRad = svaz * C.DEG_TO_RAD;
            double svE = svel / 180.0;

            double phi_u = rxgeo.GeodeticLatitude / 180.0;
            double lambda_u = rxgeo.Longitude / 180.0;

            double psi = (0.0137 / (svE + 0.11)) - 0.022;

            double phi_i = phi_u + psi * Math.Cos(azRad);
            if (phi_i > 0.416) phi_i = 0.416;
            if (phi_i < -0.416) phi_i = -0.416;

            double lambda_i = lambda_u + psi * Math.Sin(azRad) / Math.Cos(phi_i * C.PI);
            double phi_m = phi_i + 0.064 * Math.Cos((lambda_i - 1.617) * C.PI);

            double iAMP = 0.0;
            double iPER = 0.0;
            iAMP = _alpha[0] + phi_m * (_alpha[1] + phi_m * (_alpha[2] + phi_m * _alpha[3]));
            iPER = _beta[0] + phi_m * (_beta[1] + phi_m * (_beta[2] + phi_m * _beta[3]));

            if (iAMP < 0.0)
                iAMP = 0.0;
            if (iPER < 72000.0)
                iPER = 72000.0;

            double t = 43200.0 * lambda_i + time.SecondOfDay;
            if (t >= 86400.0)
                t -= 86400.0;
            if (t < 0)
                t += 86400.0;

            double x = C.TWO_PI * (t - 50400.0) / iPER; // x is in radians

            double iF = 1.0 + 16.0 * (0.53 - svE) * (0.53 - svE) * (0.53 - svE);

            double t_iono = 0.0;
            if (Math.Abs(x) < 1.57)
                t_iono = iF * (5.0e-9 + iAMP * (1 + x * x * (-0.5 + x * x / 24.0)));
            else
                t_iono = iF * 5.0e-9;


            double correction = t_iono * C.C_MPS;
            return correction;
        }

        //  get the ionospheric correction value for L2 frequency
        //  \param time the time of the observation
        //  \param rxgeo the WGS84 geodetic position of the receiver
        //  \param svel the elevation angle between the rx and SV (degrees)
        //  \param svaz the azimuth angle between the rx and SV (degrees)
        //  \return the ionospheric correction (meters)
        public double GetCorrectionL2(Time time, Position rxgeo,
            double svel, double svaz)
        {
            double result = GetCorrectionL1(time, rxgeo, svel, svaz);
            return result *= C.GAMMA_GPS; //  GAMMA_GPS = (fL1 / fL2)^2
        }

        public string FileName()
        {
            return "File: IonoModel.cs";
        }

        public string ToString()
        {
            var result = string.Format("alpha:{0},{1},{2},{3}\n",
                _alpha[0], _alpha[1], _alpha[2], _alpha[3]);
            result += string.Format("beta: {0},{1},{2},{3}",
                _beta[0], _beta[1], _beta[2], _beta[3]);
            return result;
        }

        public static bool operator ==(IonoModel left, IonoModel right)
        {
            if (left == null || right == null)
                return false;
            if (left._alpha != right._alpha || left._beta != right._beta)
                return false;
            return true;

        }

        public static bool operator !=(IonoModel left, IonoModel right)
        {
            return !(left == right);
        }

        public static void Example()
        {
            double[] alpha = new double[]
            {0.1397E-07, -0.7451E-08, -0.5960E-07, 0.1192E-06};
            double[] beta = new double[]
            { 0.1270E+06, -0.1966E+06, 0.6554E+05, 0.2621E+06 };

            var model = new IonoModel(alpha, beta);
            Console.WriteLine(model.ToString());
            var wgs84 = new WGS84Ellipsoid();
            var rx = new Position(31, 114, 30, CoordinateSystem.Geodetic, wgs84);
            Console.WriteLine("Position(XYZ):{0}", rx.ToString(CoordinateSystem.Cartesian));
            Console.WriteLine("Position(BLH):{0}", rx.ToString(CoordinateSystem.Geodetic));

            var time = new Time(2014, 1, 17, 0, 0, 1.1);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        time.AddHours(i * 6);
                        double svel = 15 + j * 40;
                        double svaz = 180 * k;
                        double c1 = model.GetCorrectionL1(time, rx, svel, svaz);
                        double c2 = model.GetCorrectionL2(time, rx, svel, svaz);
                        Console.WriteLine("Time:{0}; EL:{1}; AZ:{2}; L1:{3}; L2:{4}",
                            time.ToHmsString(), svel, svaz, c1, c2);
                    }
                }
            }
        }
    }
}
