using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XYZ2NEU
{
    class XYZPoint
    {
        public string name { set; get; }
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
    }

    class BLHPoint
    {
        public string name { set; get; }
        public double B { set; get; }
        public double L { set; get; }
        public double H { set; get; }
    }

    class NEUPoint
    {
        public string name { set; get; }
        public double N { set; get; }
        public double E { set; get; }
        public double U { set; get; }
    }
}
