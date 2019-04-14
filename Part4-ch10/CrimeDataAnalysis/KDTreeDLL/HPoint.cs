using System;

namespace KDTreeDLL
{
    /// <summary>
    /// Hyper-Point class supporting KDTree class
    /// </summary>
    class HPoint
    {
        public double[] coord;

        public HPoint(int n)
        {
            coord = new double[n];
        }

        public HPoint(double[] x)
        {
            coord = new double[x.Length];
            for (int i = 0; i < x.Length; ++i) coord[i] = x[i];
        }

        public Object clone()
        {

            return new HPoint(coord);
        }

        public bool equals(HPoint p)
        {

            // seems faster than java.util.Arrays.equals(), which is not 
            // currently supported by Matlab anyway
            for (int i = 0; i < coord.Length; ++i)
                if (coord[i] != p.coord[i])
                    return false;

            return true;
        }

        public static double sqrdist(HPoint x, HPoint y)
        {

            double dist = 0;

            for (int i = 0; i < x.coord.Length; ++i)
            {
                double diff = (x.coord[i] - y.coord[i]);
                dist += (diff * diff);
            }

            return dist;

        }

        public static double eucdist(HPoint x, HPoint y)
        {

            return Math.Sqrt(sqrdist(x, y));
        }

        public String toString()
        {
            String s = "";
            for (int i = 0; i < coord.Length; ++i)
            {
                s = s + coord[i] + ",";
            }
            return s;
        }
    }
}
