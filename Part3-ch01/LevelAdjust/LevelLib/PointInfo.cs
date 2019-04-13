using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelLib
{
    public class PointInfo
    {
        public string Name;
        public double H;
        public double realH;

        public PointInfo() { }

        public PointInfo(string name)
        {
            Name = name;
        }
        public PointInfo(string name,double h)
        {
            Name = name;
            H = h;
        }

    }
}
