using System;

namespace Iono
{
    //This abstract class encapsulates ellipsoid models (e.g. WGS84,
    //GPS, etc
    public abstract  class EllipsoidModel
    {
        /// @return semi-major axis of Earth in meters.
        public abstract double a { get; }

        /// @return semi-major axis of Earth in km.
        public abstract double a_km { get; }

        /// @return flattening (ellipsoid parameter).
        public abstract double flattening { get; }

        /// @return eccentricity (ellipsoid parameter).
        public abstract double eccentricity { get; }

        /// @return eccentricity squared (ellipsoid parameter).
        public virtual double eccSquared
        {
            get { return eccentricity*eccentricity; }
        }

        /// @return angular velocity of Earth in radians/sec.
        public abstract double angVelocity { get; }

        /// @return geocentric gravitational constant in m**3 / s**2
        public abstract double gm{get;}

        /// @return geocentric gravitational constant in m**3 / s**2
        public abstract double gm_km { get; }

        /// @return Speed of light in m/s.
        public abstract double c { get; }

        /// @return Speed of light in km/s
        public abstract double c_km { get; }

        public static bool operator ==(EllipsoidModel left, EllipsoidModel right)
        {
            if (left == null || right == null)
                return false;
            return Math.Abs(left.a - right.a)<1e-5 && Math.Abs(left.eccentricity - right.eccentricity)<1e-8;
        }

        public static bool operator !=(EllipsoidModel left, EllipsoidModel right)
        {
            return !(left == right);
        }

        public string ToString()
        {
            var result = string.Format("a={0};flattening={1};eccentricity={2}\r\n", a, flattening, eccentricity);
            result += string.Format("angVelocity={0};gm={1};c={2}", angVelocity, gm, c);
            return result;
        }
        public static void Example()
        {
            EllipsoidModel model1=new WGS84Ellipsoid();
            Console.WriteLine(model1.ToString());
           // var model2=new GPSEllipsoid();
           /// Console.WriteLine(model2.ToString());
            
        }
    }

}
