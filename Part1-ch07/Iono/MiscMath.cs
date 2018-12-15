using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iono
{
    //Miscellaneous mathematical algorithms
    public class MiscMath
    {
    

        public static void Swap(ref double x, ref double y)
        {
            double tmp;
            tmp = x;
            x = y;
            y = tmp;
        }

        /// Perform the root sum square of aa, bb and cc
        public static double RSS(double aa, double bb, double cc)
        {
            double a = Math.Abs(aa);
            double b = Math.Abs(bb);
            double c = Math.Abs(cc);
            if (a < b) Swap(ref a, ref b);
            if (a < c) Swap(ref a, ref c);
            if (a == 0)
                return 0;
            return a * Math.Sqrt(1 + (b / a) * (b / a) + (c / a) * (c / a));
        }

        /// Perform the root sum square of aa, bb
        public static double RSS(double aa, double bb)
        {
            return RSS(aa, bb, 0);
        }

        /// Perform the root sum square of aa, bb, cc and dd
        public static double RSS(double aa, double bb, double cc, double dd)
        {
            double a = Math.Abs(aa), b = Math.Abs(bb), c = Math.Abs(cc), d = Math.Abs(dd);
            // For numerical reason, let's just put the biggest in "a" (we are not sorting)
            if (a < b) Swap(ref a, ref b);
            if (a < c) Swap(ref a, ref c);
            if (a < d) Swap(ref a, ref d);
            if (a == 0) return 0;
            return a * Math.Sqrt(1 + (b / a) * (b / a) + (c / a) * (c / a) + (d / a) * (d / a));
        }

        public static double Range(Triple left, Triple right)
        {
            double s = left.SlantRange(right);
            return s;
        }

        public static double Round(double x)
        {
            return Math.Floor(x + 0.5);
        }

        //Compute remainder of division
        public static double Fmod(double numer, double denom)
        {
            double result = numer/denom;
            double reminder = numer - Math.Floor(result)*denom;
            return reminder;
        }

        public static void Example()
        {
            Console.WriteLine(MiscMath.RSS(0,3,4));
        }
    }
}
