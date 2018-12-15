using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDW
{
    class Point
    {
        public string Id;
        public double X;
        public double Y;
        public double H;
        public double Dist;

        public Point()
        {
            X = Y = H = Dist = 0;
        }

        public Point(string id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
        public void Parse(string line)
        {
            var buf = line.Split(',');
            Id = buf[0];
            X = Convert.ToDouble(buf[1]);
            Y = Convert.ToDouble(buf[2]);
            H = Convert.ToDouble(buf[3]);
        }

        public override string ToString()
        {
            return $"{Id}   {X:F3}   {Y:F3}   {H:F3}";
        }
    }
}
