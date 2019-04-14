using System;
using System.Collections.Generic;
using System.Text;

namespace KDTreeDLL
{
    /// <summary>
    /// Hyper-Rectangle class supporting KDTree class
    /// </summary>
    class HRect
    {
        public HPoint min;
        public HPoint max;

        protected HRect(int ndims)
        {
            min = new HPoint(ndims);
            max = new HPoint(ndims);
        }

        protected HRect(HPoint vmin, HPoint vmax)
        {

            min = (HPoint)vmin.clone();
            max = (HPoint)vmax.clone();
        }

        public Object clone()
        {

            return new HRect(min, max);
        }

        // from Moore's eqn. 6.6
        public HPoint closest(HPoint t)
        {

            HPoint p = new HPoint(t.coord.Length);

            for (int i = 0; i < t.coord.Length; ++i)
            {
                if (t.coord[i] <= min.coord[i])
                {
                    p.coord[i] = min.coord[i];
                }
                else if (t.coord[i] >= max.coord[i])
                {
                    p.coord[i] = max.coord[i];
                }
                else
                {
                    p.coord[i] = t.coord[i];
                }
            }

            return p;
        }

        // used in initial conditions of KDTree.nearest()
        public static HRect infiniteHRect(int d)
        {

            HPoint vmin = new HPoint(d);
            HPoint vmax = new HPoint(d);

            for (int i = 0; i < d; ++i)
            {
                vmin.coord[i] = Double.NegativeInfinity;
                vmax.coord[i] = Double.PositiveInfinity;
            }

            return new HRect(vmin, vmax);
        }

        // currently unused
        protected HRect intersection(HRect r)
        {

            HPoint newmin = new HPoint(min.coord.Length);
            HPoint newmax = new HPoint(min.coord.Length);

            for (int i = 0; i < min.coord.Length; ++i)
            {
                newmin.coord[i] = Math.Max(min.coord[i], r.min.coord[i]);
                newmax.coord[i] = Math.Min(max.coord[i], r.max.coord[i]);
                if (newmin.coord[i] >= newmax.coord[i]) return null;
            }

            return new HRect(newmin, newmax);
        }

        // currently unused
        protected double area()
        {

            double a = 1;

            for (int i = 0; i < min.coord.Length; ++i)
            {
                a *= (max.coord[i] - min.coord[i]);
            }

            return a;
        }

        public String toString()
        {
            return min + "\n" + max + "\n";
        }

    }
}
