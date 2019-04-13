using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodesyCal
{
    public class Pointinfo
    {
        public string Name;
        public double B;
        public double L;

        public Pointinfo()
        {
            Name = "";
            B = L = 0;
        }
    }

    public class GeodesicInfo
    {
        public Pointinfo P1=new Pointinfo();
        public Pointinfo P2=new Pointinfo(); 
        public double A12;
        public double A21;
        public double S;
        
    }

    public class Ellipsoid
    {
        public double a;
        public double f;
        public double b;
        public double c;
        public double e1;
        public double e2;

        public Ellipsoid(double a, double f)
        {
            this.a = a;
            this.f = f;
            this.b = a * (1 - f);
            this.c = a * a / b;
            this.e1 = Math.Sqrt(a * a - this.b * this.b) / a;
            this.e2 = Math.Sqrt(a * a - this.b * this.b) / b;
        }
    }


}
