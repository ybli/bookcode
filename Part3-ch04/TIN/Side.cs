using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN
{
    internal class Side
    {
        private double hr;

        public Tpoint p1;
        public Tpoint p2;

        public Tpoint Incut
        {
            get
            {
                if (hr >= TIN.Min(p1.h, p2.h) && hr <= TIN.Max(p1.h, p2.h))
                {
                    double x = p1.x + Math.Abs(hr - p1.h) / Dh * (p2.x - p1.x);
                    double y = p1.y + Math.Abs(hr - p1.h) / Dh * (p2.y - p1.y);
                    return new Tpoint(x, y, hr);
                }
                else
                    return new Tpoint(-1, "-1", 0, 0, 0);
            }
        }

        public double Dh
        {
            get
            {
                return Math.Abs(p1.h - p2.h);
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(p1.x * p1.x + p1.y * p1.y);
            }
        }

        public Side(Tpoint p1, Tpoint p2,double hr)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.hr = hr;
        }

        public Side(Tpoint p1, Tpoint p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public static bool operator ==(Side A, Side B)
        {
            bool IsSame = new bool();
            if ((A.p1 == B.p1 && A.p2 == B.p2) || (A.p1 == B.p2 && A.p2 == B.p1))
                IsSame = true;
            else
                IsSame = false;
            return IsSame;
        }
        public static bool operator !=(Side A, Side B)
        {
            return !(A == B);
        }
    }
}
