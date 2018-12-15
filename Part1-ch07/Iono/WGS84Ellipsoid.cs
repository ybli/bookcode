namespace Iono
{
    /// This class represents the ellipsoid model defined in NIMA
    /// TR8350.2, "Department of Defense World Geodetic System 1984".
    public class WGS84Ellipsoid : EllipsoidModel
    {
        /// Defined in TR8350.2, Appendix A.1
        /// @return semi-major axis of Earth in meters.
        public override double a
        {
            get { return 6378137.0; }
        }
        /// Derived from TR8350.2, Appendix A.1
        /// @return semi-major axis of Earth in km.
        public override double a_km
        {
            get { return a / 1000.0; }
        }
        // Derived from TR8350.2, Appendix A.1
        // @return flattening (ellipsoid parameter).
        public override double flattening
        {
            get { return 0.335281066475e-2; }
        }
        /// Defined in TR8350.2, Table 3.3
        /// @return eccentricity (ellipsoid parameter).
        public override double eccentricity
        {
            get { return 8.1819190842622e-2; }
        }
        /// Defined in TR8350.2, Table 3.3
        /// @return eccentricity squared (ellipsoid parameter).
        public override double eccSquared
        {
            get { return 6.69437999014e-3; }
        }
        /// Defined in TR8350.2, 3.2.4 line 3-6, or Table 3.1
        /// @return angular velocity of Earth in radians/sec.
        public override double angVelocity
        {
            get { return 7.292115e-5; }
        }
        /// Defined in TR8350.2, Table 3.1
        /// @return geocentric gravitational constant in m**3 / s**2
        public override double gm
        {
            get { return 3986004.418e8; }
        }
        /// Derived from TR8350.2, Table 3.1
        /// @return geocentric gravitational constant in km**3 / s**2
        public override double gm_km
        {
            get { return 398600.4418; }
        }
        /// Defined in TR8350.2, 3.3.2 line 3-11
        /// @return Speed of light in m/s.
        public override double c
        {
            get { return 299792458; }
        }
        /// Derived from TR8350.2, 3.3.2 line 3-11
        /// @return Speed of light in km/s
        public override double c_km
        {
            get { return c / 1000.0; }
        }
    }
}
