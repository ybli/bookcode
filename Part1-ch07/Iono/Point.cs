using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iono
{
    class Point
    {
        public string Id;
        public double X;
        public double Y;
        public double Z;

        //卫星标识(第1-3列)、x-坐标（第4-17列，以km为单位）、
        //y-坐标（第18-31列，以km为单位）、z-坐标（第32-45列，以km为单位）。
        public void Parse(string line)
        {
            Id = line.Substring(0, 3);
            X = Convert.ToDouble(line.Substring(3, 14)) * 1000;
            Y = Convert.ToDouble(line.Substring(17, 14)) * 1000;
            Z = Convert.ToDouble(line.Substring(31, 14)) * 1000;
        }

        public override string ToString()
        {
            string line = $"{Id},{X},{Y},{Z}";
            return line;
        }
    }
}
