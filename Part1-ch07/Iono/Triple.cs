using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Iono
{
    //Three element double vectors, for use with geodetic coordinate
    //  This class provides mathematical functions for 3D vectors, 
    //   including some functions specific to orbital tracking.
    public class Triple
    {
        private const double DEG_TO_RAD = Math.PI / 180.0;
        private const double RAD_TO_DEG = 180.0 / Math.PI;
        private readonly List<double> _values;

        public Triple()
        {
            _values = Vector();
        }
        public Triple(double a, double b, double c)
        {
            _values = Vector();
            _values[0] = a;
            _values[1] = b;
            _values[2] = c;
        }

        List<double> Vector()
        {
           var v = new List<double>();
            v.Add(0);
            v.Add(0);
            v.Add(0);
            return v;
        }

        public Triple(List<double> other)
        {
            if (other.Count == 3)
            {
                _values = other;
            }
            else
            {
                throw new Exception("The length of Vector should be 3");
            }
        }

        public Triple(Triple other)
        {
            _values = other._values;
        }
        public double[] Values
        {
            get
            {
                double[] v=new double[3];
                v[0] = _values[0];
                v[1] = _values[1];
                v[2] = _values[2];
                return v;
            }
        }

        public List<double> ToVector()
        {


            return _values;
        }

        public double this[int index]
        {
            get
            {
                ValidateRange(index);
                return _values[index];
            }

            set
            {
                ValidateRange(index);
                _values[index] = value;
            }
        }

        private void ValidateRange(int index)
        {
            if (index < 0 || index >= 3)
            {
                throw new ArgumentOutOfRangeException("index should be at[0-2]");
            }
        }

        public double At(int index)
        {
            ValidateRange(index);
            return _values[index];
        }
        public void At(int index, double value)
        {
            ValidateRange(index);
            _values[index] = value;
        }

        public virtual bool Equals(Triple other)
        {
            return _values.Equals(other._values);
        }

        public override int GetHashCode()
        {
            return _values.GetHashCode();
        }

        //returns the dot product of the two vectors
        public double Dot(Triple other)
        {
            var dot = 0.0;
            for (var i = 0; i < 3; i++)
            {
                dot += At(i) * other.At(i);
            }
            return dot;
        }

        //retuns v1 x v2 , vector cross product
        public Triple Cross(Triple other)
        {
            var result = new Triple();
            result[0] = At(1) * other[2] - At(2) * other[1];
            result[1] = At(2) * other[0] - At(0) * other[2];
            result[2] = At(0) * other[1] - At(1) * other[0];
            return result;
        }

        public double Mag()
        {
            return Math.Sqrt(Dot(this));
        }

        public Triple UnitVector()
        {
            double mag = Mag();
            if (mag <= 1e-14) throw new DivideByZeroException(mag.ToString());

            var result = new Triple();
            result[0] = At(0) / mag;
            result[1] = At(1) / mag;
            result[2] = At(2) / mag;
            return result;
        }

        // function that returns the cosine of angle between this and right
        public double CosVector(Triple other)
        {
            double rx, ry, cosvectors;
            rx = Dot(this);
            ry = other.Dot(other);

            if (rx <= 1e-14 || ry <= 1e-14)
                throw new DivideByZeroException();

            cosvectors = Dot(other) / Math.Sqrt(rx * ry);

            // this if checks for and corrects round off error 
            if (Math.Abs(cosvectors) > 1.0)
            {
                cosvectors = Math.Abs(cosvectors) / cosvectors;
            }
            return cosvectors;
        }
        //Computes the slant range between two vectors
        public double SlantRange(Triple other)
        {
            var result = new Triple();
            result = other - this;

            return result.Mag();
        }

        // Finds the elevation angle of the second point with respect to
        // the first point
        public double ElvAngle(Triple other)
        {
            var z = other - this;
            double c = z.CosVector(this);
            return 90.0 - Math.Acos(c) * RAD_TO_DEG;
        }


        //  Calculates a satellites azimuth from a station
        public double AzAngle(Triple right)
        {
            double xy, xyz, cosl, sinl, sint, xn1, xn2, xn3, xe1, xe2;
            double z1, z2, z3, p1, p2, test, alpha;

            xy = At(0) * At(0) + At(1) * At(1);
            xyz = xy + At(2) * At(2);
            xy = Math.Sqrt(xy);
            xyz = Math.Sqrt(xyz);

            if (xy <= 1e-14 || xyz <= 1e-14)
                throw new DivideByZeroException("Divide by Zero Error");

            cosl = At(0) / xy;
            sinl = At(1) / xy;
            sint = At(2) / xyz;

            xn1 = -sint * cosl;
            xn2 = -sint * sinl;
            xn3 = xy / xyz;

            xe1 = -sinl;
            xe2 = cosl;

            z1 = right[0] - At(0);
            z2 = right[1] - At(1);
            z3 = right[2] - At(2);

            p1 = (xn1 * z1) + (xn2 * z2) + (xn3 * z3);
            p2 = (xe1 * z1) + (xe2 * z2);

            test = Math.Abs(p1) + Math.Abs(p2);

            if (test < 1.0e-16)
            {
                throw new Exception("azAngle(), failed p1+p2 test.");
            }

            alpha = 90 - Math.Atan2(p1, p2) * RAD_TO_DEG;
            if (alpha < 0)
            {
                return alpha + 360;
            }
            else
            {
                return alpha;
            }
        }


        ///Computes rotation about axis X.
        // @param angle    Angle to rotate, in degrees
        // @return A triple which is the original triple rotated angle about X
        Triple R1(double angle)
        {
            double ang = angle * DEG_TO_RAD;
            double sinangle = Math.Sin(ang);
            double cosangle = Math.Cos(ang);

            var result = new Triple();
            result[0] = At(0);
            result[1] = cosangle * At(1) + sinangle * At(2);
            result[2] = -sinangle * At(1) + cosangle * At(2);
            return result;
        }


        ///Computes rotation about axis Y.
        // @param angle    Angle to rotate, in degrees
        // @return A triple which is the original triple rotated angle about Y
        // 
        Triple R2(double angle)
        {
            double ang = angle * DEG_TO_RAD;
            double sinangle = Math.Sin(ang);
            double cosangle = Math.Cos(ang);

            var result = new Triple();
            result[0] = cosangle * At(0) - sinangle * At(2);
            result[1] = At(1);
            result[2] = sinangle * At(0) + cosangle * At(2);
            return result;
        }


        // Computes rotation about axis Z.
        // @param angle    Angle to rotate, in degrees
        // @return A triple which is the original triple rotated angle about Z
        public Triple R3(double angle)
        {
            double ang = angle * DEG_TO_RAD;
            double sinangle = Math.Sin(ang);
            double cosangle = Math.Cos(ang);

            var result = new Triple();
            result[0] = cosangle * At(0) + sinangle * At(1);
            result[1] = -sinangle * At(0) + cosangle * At(1);
            result[2] = At(2);
            return result;
        }
        //public static bool operator ==(Triple left, Triple right)
        //{
        //    return left.Equals(right);
        //}

        //public static bool operator !=(Triple left, Triple right)
        //{
        //    return !(left.Equals(right));
        //}

        public static Triple operator +(Triple left, Triple right)
        {
            var result = new Triple();
            result[0] = left[0] + right[0];
            result[1] = left[1] + right[1];
            result[2] = left[2] + right[2];
            return result;
        }

        public static Triple operator +(double left, Triple right)
        {
            var result = new Triple();
            result[0] = left + right[0];
            result[1] = left + right[1];
            result[2] = left + right[2];
            return result;
        }

        public static Triple operator +(Triple left, double right)
        {
            var result = new Triple();
            result[0] = left[0] + right;
            result[1] = left[1] + right;
            result[2] = left[2] + right;
            return result;
        }

        public static Triple operator -(Triple left, Triple right)
        {
            var result = new Triple();
            result[0] = left[0] - right[0];
            result[1] = left[1] - right[1];
            result[2] = left[2] - right[2];
            return result;
        }

        public static Triple operator -(double left, Triple right)
        {
            var result = new Triple();
            result[0] = left + right[0];
            result[1] = left + right[1];
            result[2] = left + right[2];
            return result;
        }

        public static Triple operator -(Triple left, double right)
        {
            var result = new Triple();
            result[0] = left[0] + right;
            result[1] = left[1] + right;
            result[2] = left[2] + right;
            return result;
        }

        public override string ToString()
        {
            var result = string.Empty;

            if (_values.Count > 0)
            {
                result = "(" + At(0) + "," + At(1) + "," + At(2) + ")";
            }
            return result;
        }

        public static void Example()
        {
            var p1 = new Triple(1.1, 2.2, 3.3);
            var p2 = new Triple(4.4, 5.5, 6.6);
            Console.WriteLine("P1:{0}; P2:{1}\n",p1.ToString(),p2.ToString());
            Console.WriteLine("Elevation:{0}, Azimuth:{1}",p1.ElvAngle(p2),p1.AzAngle(p2));
        }
    }
}
