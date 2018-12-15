using System;


namespace Iono
{
    /// The coordinate systems supported by Position
    public enum CoordinateSystem
    {
        Unknown = 0,  ///< unknown coordinate system
        Geodetic,   ///< geodetic latitude, longitude, and height above ellipsoid
        Geocentric, ///< geocentric (regular spherical coordinates)
        Cartesian,  ///< cartesian (Earth-centered, Earth-fixed)
        Spherical   ///< spherical coordinates (theta,phi,radius)
    }
    //   A position representation class for common 3D geographic position formats,
    //   including geodetic (geodetic latitude, longitude, and height above the ellipsoid)
    //   geocentric (geocentric latitude, longitude, and radius from Earth's center),
    //   cartesian (Earth-centered, Earth-fixed) and spherical (theta,phi,radius).
    //  
    //   Internally, the representation of Position consists of three coordinate
    //   values (double), two doubles from a ellipsoid model (see below, storing these
    //   doubles is preferred over adding EllipsoidModel to calling arguments everywhere),
    //   a flag of type 'enum CoordinateSystem' giving the coordinate system, and a
    //   tolerance for use in comparing Positions. Class Position inherits from class
    //   Triple, which is how the coordinate values are stored (Triple actually uses
    //   std::valarray<double> of length 3). It is important to note that
    //   Triple:: routines are properly used by Positions ONLY in the Cartesian
    //   coordinate system.
    //  
    //   Only geodetic coordinates depend on a ellipsoid, and then
    //   only on the semi-major axis of the Earth and the square of its
    //   eccentricity. Input of this ellipsoid information (usually a pointer to a
    //   EllipsoidModel) is required by functions involving constructors of, or
    //   transformation to or from, Geodetic coordinates. However since a default
    //   is supplied (WGS84), the user need never deal with geiods unless desired.
    //   In fact, if the geodetic coordinate system is avoided, the Position class
    //   can be interpreted simply as 3D vectors in any context, particularly since
    //   the class inherits from Triple, which includes many vector manipulation
    //   routines (although the Triple:: routines assume Cartesian coordinates).
    //   Even the requirement that lengths (radius, height and the cartesian
    //   coordinates) have units of meters is required only if geodetic coordinates
    //   are used (because the semi-major axis in EllipsoidModel is in meters);
    //   without using Geodetic one could apply the class using any units for
    //   length as long as setTolerance() is called appropriately.
    //  
    //   Position relies on a series of fundamental routines to transform from
    //   one coordinate system to another, these include, for example
    //   void Position::convertGeodeticToCartesian(const Triple& llh, Triple& xyz,
    //      const double A, const double eccSq);
    //   void Position::convertSphericalToCartesian(const Triple& tpr, Triple& xyz);
    //   These functions use Triple in the calling arguments.
    //  
    //   Position will throw exceptions (gpstk::GeometryException) on bad input
    //   (e.g. negative radius or latitude > 90 degrees); otherwise the class
    //   attempts to handle all points, even the pole and the origin, consistently
    //   and without throwing exceptions.
    //   At or very near the poles, the transformation routines will set
    //   latitude = +/-90 degrees, which is theta = 0 or 180, and (arbitrarily)
    //   longitude = 0. At or very near the origin, the transformation routines
    //   will set latitude = 0, which is theta = 90, and (arbitrarily) longitude = 0;
    //   radius will be set to zero and geodetic height will be set to
    //   -radius(Earth) (= -6378137.0 in WGS84). The tolerance used in testing
    //   'at or near the pole or origin' is radius < POSITION_TOLERANCE/5.
    //   Note that this implies that a Position that is very near the origin may
    //   be SET to the exact origin by the transformation routines, and that
    //   thereby information about direction (e.g. latitude and longitude)
    //   may be LOST. The user is warned to be very careful when working
    //   near either the pole or the origin.
    //  
    //   Position includes setToString() and printf() functions similar to those
    //   in gpstk::CommonTime; this allows flexible and powerful I/O of Position to
    //   strings and streams.
    //  
    //   @sa positiontest.cpp for examples.
    public class Position : Triple
    {
        #region 1-Coordinate Systems

        // ----------- Part  1: coordinate systems --------------------------
        public EllipsoidModel Ellipsoid { get; set; }

        /// see #CoordinateSystem
        public CoordinateSystem System { get; set; }

        #endregion

        #region 2-Tolerance
        // ----------- Part  2: tolerance -----------------------------------------
        // One millimeter tolerance.
        const double ONE_MM_TOLERANCE = 0.001;
        // One centimeter tolerance.
        const double ONE_CM_TOLERANCE = 0.01;
        // One micron tolerance.
        const double ONE_UM_TOLERANCE = 0.000001;
        // Default tolerance for time equality in meters.
        double POSITION_TOLERANCE = ONE_MM_TOLERANCE;

        /// tolerance used in comparisons
        public double Tolerance { get; set; }


        #endregion

        #region 3-Constructors
        // ----------- Part  3: member functions: constructors --------------------
        //
        // Default constructor.
        public Position()
        {
            Ellipsoid = new WGS84Ellipsoid();
            Initialize(0.0, 0.0, 0.0, CoordinateSystem.Unknown, Ellipsoid);
        }

        public Position(Position other)
        {
            new Position(other.Values, other.System, other.Ellipsoid);
        }

        public Position(double a, double b, double c, CoordinateSystem s,
                   EllipsoidModel ell)
        {
            Initialize(a, b, c, s, ell);
        }
        public Position(double a, double b, double c, CoordinateSystem s)
        {
            var ell = new WGS84Ellipsoid();
            Initialize(a, b, c, s, ell);
        }
        public Position(double[] ABC, CoordinateSystem s, EllipsoidModel ell)
        {
            double a = ABC[0];
            double b = ABC[1];
            double c = ABC[2];
            Initialize(a, b, c, s, ell);
        }
        public Position(double[] ABC, CoordinateSystem s)
        {
            double a = ABC[0];
            double b = ABC[1];
            double c = ABC[2];
            var ell = new WGS84Ellipsoid();
            Initialize(a, b, c, s, ell);
        }
        public Position(Triple ABC, CoordinateSystem s, EllipsoidModel ell)
        {
            double a = ABC[0];
            double b = ABC[1];
            double c = ABC[2];
            Initialize(a, b, c, s, ell);
        }
        public Position(Triple ABC, CoordinateSystem s)
        {
            double a = ABC[0];
            double b = ABC[1];
            double c = ABC[2];
            var ell = new WGS84Ellipsoid();
            Initialize(a, b, c, s, ell);
        }
    
        #endregion

        #region 4-arithmetic
        // ----------- Part  4: member functions: arithmetic ----------------------
        public static Position operator -(Position left, Position right)
        {
            // convert both to Cartesian
            left.TransformTo(CoordinateSystem.Cartesian);
            right.TransformTo(CoordinateSystem.Cartesian);
            // difference
            left[0] -= right[0];
            left[1] -= right[1];
            left[2] -= right[2];

            return left;
        }

        public static Position operator +(Position left, Position right)
        {

            // convert both to Cartesian
            left.TransformTo(CoordinateSystem.Cartesian);
            right.TransformTo(CoordinateSystem.Cartesian);
            // add
            left[0] += right[0];
            left[1] += right[1];
            left[2] += right[2];

            return left;
        }

        #endregion

        #region 5-Comparisons

        // ----------- Part  5: member functions: comparisons ---------------------
        //
        // Equality operator. Returns false if ell values differ.
        public static bool operator ==(Position left, Position right)
        {
            if (left == null || right == null)
                return false;

            if (left.Ellipsoid != right.Ellipsoid)
                return false;
            left.TransformTo(CoordinateSystem.Cartesian);
            right.TransformTo(CoordinateSystem.Cartesian);
            if (MiscMath.Range(left, right) < left.Tolerance)
                return true;
            else
                return false;
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        #endregion

        #region 6-Coordinate Transformation
        // ----------- Part  6: member functions: coordinate transformations ------
        //
        // Transform coordinate system. Does nothing if sys already matches the
        // current value of member CoordinateSystem 'system'.
        // @param sys coordinate system into which *this is to be transformed.
        // @return *this
        public void TransformTo(CoordinateSystem sys)
        {
            if (sys == CoordinateSystem.Unknown || sys == System)
                return;

            // this copies geoid information and tolerance
            Triple target = this;

            // transform target.theArray and set target.system
            switch (System)
            {
                case CoordinateSystem.Unknown:
                    return;
                case CoordinateSystem.Geodetic:
                    // --------------- Geodetic to ... ------------------------
                    switch (sys)
                    {
                        case CoordinateSystem.Unknown:
                        case CoordinateSystem.Geodetic: return;
                        case CoordinateSystem.Geocentric:
                            ConvertGeodeticToGeocentric(this, out target, Ellipsoid.a, Ellipsoid.eccSquared);
                            break;
                        case CoordinateSystem.Cartesian:
                            ConvertGeodeticToCartesian(this, out target, Ellipsoid.a, Ellipsoid.eccSquared);
                            break;
                        case CoordinateSystem.Spherical:
                            ConvertGeodeticToGeocentric(this, out target, Ellipsoid.a, Ellipsoid.eccSquared);
                            target[0] = 90 - target[0];   // geocen -> sph
                            break;
                    }
                    break;
                case CoordinateSystem.Geocentric:
                    // --------------- Geocentric to ... ----------------------
                    switch (sys)
                    {
                        case CoordinateSystem.Unknown:
                        case CoordinateSystem.Geocentric: return;
                        case CoordinateSystem.Geodetic:
                            convertGeocentricToGeodetic(this, out target, Ellipsoid.a, Ellipsoid.eccSquared);
                            break;
                        case CoordinateSystem.Cartesian:
                            ConvertGeocentricToCartesian(this, out target);
                            break;
                        case CoordinateSystem.Spherical:
                            target[0] = 90 - target[0];   // geocen -> sph
                            break;
                    }
                    break;
                case CoordinateSystem.Cartesian:
                    // --------------- Cartesian to ... -----------------------
                    switch (sys)
                    {
                        case CoordinateSystem.Unknown:
                        case CoordinateSystem.Cartesian: return;
                        case CoordinateSystem.Geodetic:
                            convertCartesianToGeodetic(this, out target, Ellipsoid.a, Ellipsoid.eccSquared);
                            break;
                        case CoordinateSystem.Geocentric:
                            ConvertCartesianToGeocentric(this, out target);
                            break;
                        case CoordinateSystem.Spherical:
                            ConvertCartesianToSpherical(this, out target);
                            break;
                    }
                    break;
                case CoordinateSystem.Spherical:
                    // --------------- Spherical to ... -----------------------
                    switch (sys)
                    {
                        case CoordinateSystem.Unknown:
                        case CoordinateSystem.Spherical: return;
                        case CoordinateSystem.Geodetic:
                            this[0] = 90 - this[0];   // sph -> geocen
                            convertGeocentricToGeodetic(this, out target, Ellipsoid.a, Ellipsoid.eccSquared);
                            break;
                        case CoordinateSystem.Geocentric:
                            target[0] = 90 - target[0];   // sph -> geocen
                            break;
                        case CoordinateSystem.Cartesian:
                            ConvertSphericalToCartesian(this, out target);
                            break;
                    }
                    break;
            }  // end switch(system)

            this[0] = target[0];
            this[1] = target[1];
            this[2] = target[2];
            System = sys;
        }

        #endregion

        #region 7-get
        // ----------- Part  7: member functions: get -----------------------------
        //
        // These routines retrieve coordinate values in all coordinate systems.
        // Note that calling these will transform the Position to another coordinate
        // system if that is required.
        //

        public EllipsoidModel EllipsoidModel
        {
            get { return Ellipsoid; }
            set
            {
                if (value == null)
                    throw new Exception("Given EllipsodModel is null");
                Ellipsoid = value;
            }
        }

        // Get X coordinate (meters)
        public double X
        {
            get
            {
                if (System == CoordinateSystem.Cartesian)
                    return this[0];
                TransformTo(CoordinateSystem.Cartesian);
                return this[0];
            }

        }

        // Get Y coordinate (meters)
        public double Y
        {
            get
            {
                if (System == CoordinateSystem.Cartesian)
                    return this[1];

                TransformTo(CoordinateSystem.Cartesian);
                return this[1];
            }
        }

        // Get Z coordinate (meters)
        public double Z
        {
            get
            {
                if (System == CoordinateSystem.Cartesian)
                    return this[2];
                TransformTo(CoordinateSystem.Cartesian);
                return this[2];
            }
        }

        // Get geodetic latitude (degrees North).
        public double GeodeticLatitude
        {
            get
            {
                if (System == CoordinateSystem.Geodetic)
                    return this[0];
                TransformTo(CoordinateSystem.Geodetic);
                return this[0];
            }
        }

        public double geodeticLatitude()
        {
            return GeodeticLatitude;
        }
        // Get geocentric latitude (degrees North),
        // equal to 90 degress - theta in regular spherical coordinates.
        public double GeocentricLatitude
        {
            get
            {
                if (System == CoordinateSystem.Geocentric)
                    return this[0];
                TransformTo(CoordinateSystem.Geocentric);
                return this[0];
            }
        }

        // Get spherical coordinate theta in degrees
        public double Theta
        {
            get
            {
                if (System == CoordinateSystem.Spherical)
                    return this[0];
                TransformTo(CoordinateSystem.Spherical);
                return this[0];
            }
        }

        // Get spherical coordinate phi in degrees
        public double Phi
        {
            get
            {
                if (System == CoordinateSystem.Spherical)
                    return this[1];
                TransformTo(CoordinateSystem.Spherical);
                return this[1];
            }
        }

        // Get longitude (degrees East),
        // equal to phi in regular spherical coordinates.
        public double Longitude
        {
            get
            {
                if (System != CoordinateSystem.Spherical)
                    return this[1];
                TransformTo(CoordinateSystem.Spherical);
                return this[1];
            }
        }

        // Get radius or distance from the center of Earth (meters),
        // Same as radius in spherical coordinates.
        public double Radius
        {
            get
            {
                if (System == CoordinateSystem.Spherical || System == CoordinateSystem.Geocentric)
                    return this[2];
                TransformTo(CoordinateSystem.Spherical);
                return this[2];
            }
        }

        // Get height above ellipsoid (meters) (Geodetic).
        public double Height
        {
            get
            {
                if (System == CoordinateSystem.Geodetic)
                    return this[2];

                TransformTo(CoordinateSystem.Geodetic);
                return this[2];
            }
        }

        public double Altitude
        {
            get
            {
                return Height;
            }
        }
        #endregion

        #region 8-set

        // ----------- Part  8: member functions: set -----------------------------
        // Set the Position given geodetic coordinates, system is set to Geodetic.
        // @param lat geodetic latitude in degrees North
        // @param lon geodetic longitude in degrees East
        // @param ht height above the ellipsoid in meters
        // @return a reference to this object.
        // @throw GeometryException on invalid input
        public void SetGeodetic(double lat, double lon, double ht,
                                            EllipsoidModel ell)
        {
            if (lat > 90 || lat < -90)
            {
                throw new Exception("Invalid latitude in setGeodetic ");
            }
            this[0] = lat;

            this[1] = lon;
            if (this[1] < 0)
                this[1] += 360 * (1 - Math.Ceiling(this[1] / 360));
            else if (this[1] >= 360)
                this[1] -= 360 * Math.Floor(this[1] / 360);

            this[2] = ht;

            if (ell != null)
            {
                Ellipsoid = ell;
            }
            System = CoordinateSystem.Geodetic;

        }

        // Set the Position given geocentric coordinates, system is set to Geocentric
        // @param lat geocentric latitude in degrees North
        // @param lon geocentric longitude in degrees East
        // @param rad radius from the Earth's center in meters
        // @return a reference to this object.
        // @throw GeometryException on invalid input
        public void SetGeocentric(double lat, double lon, double rad)
        {
            if (lat > 90 || lat < -90)
            {
                throw new Exception("Invalid latitude in setGeocentric ");
            }
            if (rad < 0)
            {
                throw new Exception("Invalid radius in setGeocentric");
            }
            this[0] = lat;
            this[1] = lon;
            this[2] = rad;

            if (this[1] < 0)
                this[1] += 360 * (1 - Math.Ceiling(this[1] / 360));
            else if (this[1] >= 360)
                this[1] -= 360 * Math.Floor(this[1] / 360);
            System = CoordinateSystem.Geocentric;

        }

        // Set the Position given spherical coordinates, system is set to Spherical
        // @param theta angle from the Z-axis (degrees)
        // @param phi angle from the X-axis in the XY plane (degrees)
        // @param rad radius from the center in meters
        // @return a reference to this object.
        // @throw GeometryException on invalid input
        public void SetSpherical(double theta, double phi, double rad)
        {
            if (theta < 0 || theta > 180)
            {
                throw new Exception("Invalid theta in setSpherical");
            }
            if (rad < 0)
            {
                throw new Exception("Invalid radius in setSpherical ");
            }

            this[0] = theta;
            this[1] = phi;
            this[2] = rad;

            if (this[1] < 0)
                this[1] += 360 * (1 - Math.Ceiling(this[1] / 360));
            else if (this[1] >= 360)
                this[1] -= 360 * Math.Floor(this[1] / 360);
            System = CoordinateSystem.Spherical;
        }

        // Set the Position given ECEF coordinates, system is set to Cartesian.
        // @param X ECEF X coordinate in meters.
        // @param Y ECEF Y coordinate in meters.
        // @param Z ECEF Z coordinate in meters.
        // @return a reference to this object.
        public void SetECEF(double X, double Y, double Z)
        {
            this[0] = X;
            this[1] = Y;
            this[2] = Z;
            System = CoordinateSystem.Cartesian;
        }

        #endregion

        #region 9-ToString

        //  %X %Y %Z  (cartesian or ECEF)
        //  %x %y %z  (cartesian or ECEF)
        //  %a %l %r  (geocentric)
        //  %A %L %h  (geodetic)
        //  %t %p %r  (spherical)
        public string ToString(CoordinateSystem sys)
        {
            var result = string.Empty;
            TransformTo(sys);
            switch (sys)
            {
                case CoordinateSystem.Cartesian:
                    result = string.Format("({0:0.0000},{1:0.0000},{2:0.0000})", X, Y, Z);
                    break;
                case CoordinateSystem.Geocentric:
                    result = string.Format("({0:0.000000},{1:0.000000},{2:0.0000})",
                        GeocentricLatitude, Longitude, Radius);
                    break;
                case CoordinateSystem.Geodetic:
                    result = string.Format("({0:0.000000},{1:0.000000},{2:0.0000})",
                        GeodeticLatitude, Longitude, Height);
                    break;
                case CoordinateSystem.Spherical:
                    result = string.Format("({0:0.000000},{1:0.000000},{2:0.0000})",
                        Theta, Phi, Radius);
                    break;
                case CoordinateSystem.Unknown:
                    result = string.Format("({0:0.0000},{1:0.0000},{2:0000})", this[0], this[1], this[2]);
                    break;
            }
            return result;
        }

        #endregion

        #region 10-Fundamental Conversions

        // ----------- Part 10: functions: fundamental conversions ---------------
        //
        // Fundamental conversion from spherical to cartesian coordinates.
        // @param trp (input): theta, phi, radius
        // @param xyz (output): X,Y,Z in units of radius
        // Algorithm references: standard geometry.
        public void ConvertSphericalToCartesian(Triple tpr, out Triple xyz)
        {
            xyz = new Triple();
            double st = Math.Sin(tpr[0] * C.DEG_TO_RAD);
            xyz[0] = tpr[2] * st * Math.Cos(tpr[1] * C.DEG_TO_RAD);
            xyz[1] = tpr[2] * st * Math.Sin(tpr[1] * C.DEG_TO_RAD);
            xyz[2] = tpr[2] * Math.Cos(tpr[0] * C.DEG_TO_RAD);
        }

        // Fundamental routine to convert cartesian to spherical coordinates.
        // @param xyz (input): X,Y,Z
        // @param trp (output): theta, phi (deg), radius in units of input
        // Algorithm references: standard geometry.
        public void ConvertCartesianToSpherical(Triple xyz, out Triple tpr)
        {
            tpr = new Triple();
            tpr[2] = MiscMath.RSS(xyz[0], xyz[1], xyz[2]);
            if (tpr[2] <= POSITION_TOLERANCE / 5)
            { // zero-length Cartesian vector
                tpr[0] = 90;
                tpr[1] = 0;
                return;
            }
            tpr[0] = Math.Acos(xyz[2] / tpr[2]);
            tpr[0] *= C.RAD_TO_DEG;
            if (MiscMath.RSS(xyz[0], xyz[1]) < POSITION_TOLERANCE / 5)
            {       // pole
                tpr[1] = 0;
                return;
            }
            tpr[1] = Math.Atan2(xyz[1], xyz[0]);
            tpr[1] *= C.RAD_TO_DEG;
            if (tpr[1] < 0) tpr[1] += 360;
        }

        // Fundamental routine to convert cartesian (ECEF) to geodetic coordinates,
        // (Geoid specified by semi-major axis and eccentricity squared).
        // @param xyz (input): X,Y,Z in meters
        // @param llh (output): geodetic lat(deg N), lon(deg E),
        //                             height above ellipsoid (meters)
        // @param A (input) Earth semi-major axis
        // @param eccSq (input) square of Earth eccentricity
        // Algorithm references:
        public void convertCartesianToGeodetic(Triple xyz, out Triple llh,
                                                  double A, double eccSq)
        {
            llh = new Triple();

            double p, slat, N, htold, latold;
            p = Math.Sqrt(xyz[0] * xyz[0] + xyz[1] * xyz[1]);
            if (p < POSITION_TOLERANCE / 5)
            {  // pole or origin
                llh[0] = (xyz[2] > 0 ? 90.0 : -90.0);
                llh[1] = 0;                            // lon undefined, really
                llh[2] = Math.Abs(xyz[2]) - A * Math.Sqrt(1.0 - eccSq);
                return;
            }
            llh[0] = Math.Atan2(xyz[2], p * (1.0 - eccSq));
            llh[2] = 0;
            for (int i = 0; i < 5; i++)
            {
                slat = Math.Sin(llh[0]);
                N = A / Math.Sqrt(1.0 - eccSq * slat * slat);
                htold = llh[2];
                llh[2] = p / Math.Cos(llh[0]) - N;
                latold = llh[0];
                llh[0] = Math.Atan2(xyz[2], p * (1.0 - eccSq * (N / (N + llh[2]))));
                if (Math.Abs(llh[0] - latold) < 1.0e-9 && Math.Abs(llh[2] - htold) < 1.0e-9 * A) break;
            }
            llh[1] = Math.Atan2(xyz[1], xyz[0]);
            if (llh[1] < 0.0) llh[1] += C.TWO_PI;
            llh[0] *= C.RAD_TO_DEG;
            llh[1] *= C.RAD_TO_DEG;
        }

        // Fundamental routine to convert geodetic to cartesian (ECEF) coordinates,
        // (Geoid specified by semi-major axis and eccentricity squared).
        // @param llh (input): geodetic lat(deg N), lon(deg E),
        //            height above ellipsoid (meters)
        // @param xyz (output): X,Y,Z in meters
        // @param A (input) Earth semi-major axis
        // @param eccSq (input) square of Earth eccentricity
        // Algorithm references:
        public void ConvertGeodeticToCartesian(Triple llh, out Triple xyz,
                      double A, double eccSq)
        {
            xyz = new Triple();
            double slat = Math.Sin(llh[0] * C.DEG_TO_RAD);
            double clat = Math.Cos(llh[0] * C.DEG_TO_RAD);
            double N = A / Math.Sqrt(1.0 - eccSq * slat * slat);
            xyz[0] = (N + llh[2]) * clat * Math.Cos(llh[1] * C.DEG_TO_RAD);
            xyz[1] = (N + llh[2]) * clat * Math.Sin(llh[1] * C.DEG_TO_RAD);
            xyz[2] = (N * (1.0 - eccSq) + llh[2]) * slat;
        }

        // Fundamental routine to convert cartesian (ECEF) to geocentric coordinates.
        // @param xyz (input): X,Y,Z in meters
        // @param llr (output):
        //            geocentric lat(deg N),lon(deg E),radius (units of input)
        public void ConvertCartesianToGeocentric(Triple xyz, out Triple llr)
        {
            ConvertCartesianToSpherical(xyz, out llr);
            llr[0] = 90 - llr[0];         // convert theta to latitude
        }

        // Fundamental routine to convert geocentric to cartesian (ECEF) coordinates.
        // @param llr (input): geocentric lat(deg N),lon(deg E),radius
        // @param xyz (output): X,Y,Z (units of radius)
        public void ConvertGeocentricToCartesian(Triple llr, out Triple xyz)
        {
            var llh = new Triple(llr);
            llh[0] = 90 - llh[0];         // convert latitude to theta
            ConvertSphericalToCartesian(llh, out xyz);
        }

        // Fundamental routine to convert geocentric to geodetic coordinates.
        // @param llr (input): geocentric Triple: lat(deg N),lon(deg E),radius (meters)
        // @param llh (output): geodetic latitude (deg N),
        //            longitude (deg E), and height above ellipsoid (meters)
        // @param A (input) Earth semi-major axis
        // @param eccSq (input) square of Earth eccentricity
        public void convertGeocentricToGeodetic(Triple llr, out Triple llh,
                                                     double A, double eccSq)
        {
            llh = new Triple();
            double cl, p, sl, slat, N, htold, latold;
            llh[1] = llr[1];   // longitude is unchanged
            cl = Math.Sin((90 - llr[0]) * C.DEG_TO_RAD);
            sl = Math.Cos((90 - llr[0]) * C.DEG_TO_RAD);
            if (llr[2] <= POSITION_TOLERANCE / 5)
            {
                // radius is below tolerance, hence assign zero-length
                // arbitrarily set latitude = longitude = 0
                llh[0] = llh[1] = 0;
                llh[2] = -A;
                return;
            }
            else if (cl < 1.0e-10)
            {
                // near pole ... note that 1mm/radius(Earth) = 1.5e-10
                if (llr[0] < 0) llh[0] = -90;
                else llh[0] = 90;
                llh[1] = 0;
                llh[2] = llr[2] - A * Math.Sqrt(1 - eccSq);
                return;
            }
            llh[0] = Math.Atan2(sl, cl * (1.0 - eccSq));
            p = cl * llr[2];
            llh[2] = 0;
            for (int i = 0; i < 5; i++)
            {
                slat = Math.Sin(llh[0]);
                N = A / Math.Sqrt(1.0 - eccSq * slat * slat);
                htold = llh[2];
                llh[2] = p / Math.Cos(llh[0]) - N;
                latold = llh[0];
                llh[0] = Math.Atan2(sl, cl * (1.0 - eccSq * (N / (N + llh[2]))));
                if (Math.Abs(llh[0] - latold) < 1.0e-9 && Math.Abs(llh[2] - htold) < 1.0e-9 * A) break;
            }
            llh[0] *= C.RAD_TO_DEG;
        }

        // Fundamental routine to convert geodetic to geocentric coordinates.
        // @param geodeticllh (input): geodetic latitude (deg N),
        //            longitude (deg E), and height above ellipsoid (meters)
        // @param llr (output): geocentric lat (deg N),lon (deg E),radius (meters)
        // @param A (input) Earth semi-major axis
        // @param eccSq (input) square of Earth eccentricity
        public void ConvertGeodeticToGeocentric(Triple llh, out Triple llr,
                                                     double A, double eccSq)
        {
            llr = new Triple();
            double slat = Math.Sin(llh[0] * C.DEG_TO_RAD);
            double N = A / Math.Sqrt(1.0 - eccSq * slat * slat);
            // longitude is unchanged
            llr[1] = llh[1];
            // radius
            llr[2] = Math.Sqrt((N + llh[2]) * (N + llh[2]) + N * eccSq * (N * eccSq - 2 * (N + llh[2])) * slat * slat);
            if (llr[2] <= POSITION_TOLERANCE / 5)
            {
                // radius is below tolerance, hence assign zero-length
                // arbitrarily set latitude = longitude = 0
                llr[0] = llr[1] = llr[2] = 0;
                return;
            }
            if (1 - Math.Abs(slat) < 1.0e-10)
            {             // at the pole
                if (slat < 0) llr[0] = -90;
                else llr[0] = 90;
                llr[1] = 0.0;
                return;
            }
            // theta
            llr[0] = Math.Acos((N * (1 - eccSq) + llh[2]) * slat / llr[2]);
            llr[0] *= C.RAD_TO_DEG;
            llr[0] = 90 - llr[0];
        }

        #endregion

        #region 11-Useful function
        // ----------- Part 11:  useful functions -------------
        // Compute the range in meters between this Position and
        // the Position passed as input.
        // @param right Position to which to find the range
        // @return the range (in meters)
        // @throw GeometryException if ell values differ
        public static double Range(Position left, Position right)
        {
            if (left.Ellipsoid != right.Ellipsoid)
            {
                throw new Exception("Unequal geoids");
            }
            left.TransformTo(CoordinateSystem.Cartesian);
            right.TransformTo(CoordinateSystem.Cartesian);
            double result = MiscMath.RSS(left.X - right.X, left.X - right.Y, left.Z - right.Z);
            return result;
        }

        // Compute the radius of the ellipsoidal Earth, given the geodetic latitude.
        // @param geolat geodetic latitude in degrees
        // @return the Earth radius (in meters)
        public static double RadiusEarth(double geolat, double A, double eccSq)
        {
            double slat = Math.Sin(C.DEG_TO_RAD * geolat);
            double e = (1.0 - eccSq);
            double f = (1.0 + (e * e - 1.0) * slat * slat) / (1.0 - eccSq * slat * slat);
            return (A * Math.Sqrt(f));
        }

        // A member function that computes the elevation of the input
        // (Target) position as seen from this Position.
        // @param Target the Position which is observed to have the
        //        computed elevation, as seen from this Position.
        // @return the elevation in degrees
        public double Elevation(Position target)
        {
            target.TransformTo(CoordinateSystem.Cartesian);
            TransformTo(CoordinateSystem.Cartesian);
            // use Triple:: functions in cartesian coordinates (only)
            double elevation;
            try
            {
                elevation = ElvAngle(target);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return elevation;
        }

        // A member function that computes the elevation of the input
        // (Target) position as seen from this Position, using a Geodetic
        // (i.e. ellipsoidal) system.
        // @param Target the Position which is observed to have the
        //        computed elevation, as seen from this Position.
        // @return the elevation in degrees
        public double ElevationGeodetic(Position target)
        {
            double latGeodetic = GeodeticLatitude * C.DEG_TO_RAD;
            double longGeodetic = Longitude * C.DEG_TO_RAD;
            double localUp;
            double cosUp;
            TransformTo(CoordinateSystem.Cartesian);
            target.TransformTo(CoordinateSystem.Cartesian);

            // Let's get the slant vector
            Triple z = target - this;
            if (z.Mag() <= 1e-4) // if the positions are within .1 millimeter
            {
                throw new Exception("Positions are within .1 millimeter");
            }
            // Compute k vector in local North-East-Up (NEU) system
            var kVector = new Triple(Math.Cos(latGeodetic) * Math.Cos(longGeodetic),
                Math.Cos(latGeodetic) * Math.Sin(longGeodetic),
                Math.Sin(latGeodetic));

            // Take advantage of dot method to get Up coordinate in local NEU system
            localUp = z.Dot(kVector);
            // Let's get cos(z), being z the angle with respect to local vertical (Up);
            cosUp = localUp / z.Mag();
            return 90.0 - (Math.Acos(cosUp) * C.RAD_TO_DEG);
        }

        // A member function that computes the azimuth of the input
        // (Target) position as seen from this Position.
        // @param Target the Position which is observed to have the
        //        computed azimuth, as seen from this Position.
        // @return the azimuth in degrees
        public double Azimuth(Position target)
        {
            TransformTo(CoordinateSystem.Cartesian);
            target.TransformTo(CoordinateSystem.Cartesian);
            // use Triple:: functions in cartesian coordinates (only)
            double az;
            try
            {
                az = AzAngle(target);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return az;
        }

        // A member function that computes the azimuth of the input
        // (Target) position as seen from this Position, using a Geodetic
        // (i.e. ellipsoidal) system.
        // @param Target the Position which is observed to have the
        //        computed azimuth, as seen from this Position.
        // @return the azimuth in degrees
        public double AzimuthGeodetic(Position Target)
        {
            double latGeodetic = GeodeticLatitude * C.DEG_TO_RAD;
            double longGeodetic = Longitude * C.DEG_TO_RAD;
            double localN, localE;
            TransformTo(CoordinateSystem.Cartesian);
            Target.TransformTo(CoordinateSystem.Cartesian);
            Triple z;
            // Let's get the slant vector
            z = Target - this;

            if (z.Mag() <= 1e-4) // if the positions are within .1 millimeter
            {
                throw new Exception("Positions are within .1 millimeter");
            }

            // Compute i vector in local North-East-Up (NEU) system
            var iVector = new Triple(Math.Sin(latGeodetic) * Math.Cos(longGeodetic),
                -Math.Sin(latGeodetic) * Math.Sin(longGeodetic), Math.Cos(latGeodetic));

            // Compute j vector in local North-East-Up (NEU) system
            var jVector = new Triple(-Math.Sin(longGeodetic), Math.Cos(longGeodetic), 0);

            // Now, let's use dot product to get localN and localE unitary vectors
            localN = (z.Dot(iVector)) / z.Mag();
            localE = (z.Dot(jVector)) / z.Mag();

            // Let's test if computing azimuth has any sense
            double test = Math.Abs(localN) + Math.Abs(localE);

            // Warning: If elevation is very close to 90 degrees, we will return azimuth = 0.0
            if (test < 1.0e-16) return 0.0;

            double alpha = ((Math.Atan2(localE, localN)) * C.RAD_TO_DEG);
            if (alpha < 0.0)
            {
                return alpha + 360.0;
            }
            else
            {
                return alpha;
            }
        }

        // A member function that computes the point at which a signal, which
        // is received at *this Position and there is observed at the input
        // azimuth and elevation, crosses a model ionosphere that is taken to
        // be a uniform thin shell at the input height. This algorithm is done
        // in geocentric coordinates.
        // A member function that computes the point at which a signal, which
        // is received at *this Position and there is observed at the input
        // azimuth and elevation, crosses a model ionosphere that is taken to
        // be a uniform thin shell at the input height. This algorithm is done
        // in geocentric coordinates.
        // @param elev elevation angle of the signal at reception, in degrees
        // @param azim azimuth angle of the signal at reception, in degrees
        // @param ionoht height of the ionosphere, in meters
        // @return Position IPP the position of the ionospheric pierce point,
        //     in the same coordinate system as *this; *this is not modified.
        public Position IonosphericPiercePoint(double elev, double azim, double ionoht)
        {
            // convert to Geocentric
            TransformTo(CoordinateSystem.Geocentric);

            // compute the geographic pierce point
            Position IPP = new Position(this);                   // copy system and geoid
            double el = elev * C.DEG_TO_RAD;
            // p is the angle subtended at Earth center by Rx and the IPP
            double p = Math.PI / 2.0 - el - Math.Asin(Ellipsoid.a * Math.Cos(el) / (Ellipsoid.a + ionoht));
            double lat = this[0] * C.DEG_TO_RAD;
            double az = azim * C.DEG_TO_RAD;
            IPP[0] = Math.Asin(Math.Sin(lat) * Math.Cos(p) + Math.Cos(lat) * Math.Sin(p) * Math.Cos(az));
            IPP[1] = this[1] * C.DEG_TO_RAD
               + Math.Asin(Math.Sin(p) * Math.Sin(az) / Math.Cos(IPP[0]));

            IPP[0] *= C.RAD_TO_DEG;
            IPP[1] *= C.RAD_TO_DEG;
            IPP[2] = Ellipsoid.a + ionoht;

            // transform back
            IPP.TransformTo(System);
            return IPP;
        }

        //A member function that computes the radius of curvature of the 
        //meridian (Rm) corresponding to this Position.
        //@return radius of curvature of the meridian (in meters)
        public double CurvMeridian()
        {

            double slat = Math.Sin(GeodeticLatitude * C.DEG_TO_RAD);
            double W = 1.0 / Math.Sqrt(1.0 - Ellipsoid.eccSquared * slat * slat);

            return Ellipsoid.a * (1.0 - Ellipsoid.eccSquared) * W * W * W;

        }

        //A member function that computes the radius of curvature in the 
        //prime vertical (Rn) corresponding to this Position.
        //@return radius of curvature in the prime vertical (in meters)
        //
        public double CurvPrimeVertical()
        {

            double slat = Math.Sin(GeodeticLatitude * C.DEG_TO_RAD);
            return Ellipsoid.a / Math.Sqrt(1.0 - Ellipsoid.eccSquared * slat * slat);

        }
        #endregion

        #region 12-Private function
        // ----------- Part 12: private functions and member data -----------------
        //
        // Initialization function, used by the constructors.
        // @param a coordinate [ X(m), or latitude (degrees N) ]
        // @param b coordinate [ Y(m), or longitude (degrees E) ]
        // @param c coordinate [ Z, height above ellipsoid or radius, in m ]
        // @param s CoordinateSystem, defaults to Cartesian
        // @param geiod pointer to a GeoidModel, default NULL (WGS84)
        // @throw GeometryException on invalid input.
        private void Initialize(double a, double b, double c,
                       CoordinateSystem s, EllipsoidModel ell)
        {

            double bb = b;
            if (s == CoordinateSystem.Geodetic || s == CoordinateSystem.Geocentric)
            {
                if (a > 90 || a < -90)
                {
                    throw new Exception("Invalid latitude in constructor: " + a.ToString());
                }
                if (bb < 0)
                    bb += 360 * (1 - Math.Ceiling(bb / 360));
                else if (bb >= 360)
                    bb -= 360 * Math.Floor(bb / 360);
            }
            if (s == CoordinateSystem.Geocentric || s == CoordinateSystem.Spherical)
            {
                if (c < 0)
                {
                    throw new Exception("Invalid radius in constructor: " + c);
                }
            }
            if (s == CoordinateSystem.Spherical)
            {
                if (a < 0 || a > 180)
                {
                    throw new Exception("Invalid theta in constructor: " + a);
                }
                if (bb < 0)
                    bb += 360 * (1 - Math.Ceiling(bb / 360));
                else if (bb >= 360)
                    bb -= 360 * Math.Floor(bb / 360);
            }

            this[0] = a;
            this[1] = bb;
            this[2] = c;

            System = s;
            Ellipsoid = ell;
            Tolerance = POSITION_TOLERANCE;

        }
        #endregion

        // Function to change from CIS to CTS(ECEF) coordinate system
        // (coordinates in meters)
        // @param posCis    Coordinates in CIS system (in meters).
        // @param time       Epoch
        // @return Triple in CTS(ECEF) coordinate system.
        public static Triple CIS2CTS(Triple posCIS, Time time)
        {
            // Angle of Earth rotation, in radians
            double ts = (Time.UTC2SID(time) * C.TWO_PI / 24.0);

            var result = new Triple();
            result[0] = Math.Cos(ts) * posCIS[0] + Math.Sin(ts) * posCIS[1];
            result[1] = -Math.Sin(ts) * posCIS[0] + Math.Cos(ts) * posCIS[1];
            result[2] = posCIS[2];

            return result;
        } 


        public static void Example()
        {
            EllipsoidModel wgs84 = new WGS84Ellipsoid();
            var buin = new Position(-2953118.4465, 5078909.8076, 2474538.1820,
                CoordinateSystem.Cartesian, wgs84);
            Console.WriteLine(buin.ToString(CoordinateSystem.Cartesian));
            Console.WriteLine(buin.ToString(CoordinateSystem.Geodetic));
            Console.WriteLine(buin.ToString(CoordinateSystem.Geocentric));
            Console.WriteLine(buin.ToString(CoordinateSystem.Spherical));

            Console.WriteLine("CurvMeridan:{0}", buin.CurvMeridian());
            Console.WriteLine("CurvPrimeVertical:{0}", buin.CurvPrimeVertical());
        }

    }
}
