using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringPointsForDesignedPoints
{
    public class InfoData
    {
        public string Name; // 交点号

        public double X;

        public double Y;

        public double Mil; // 里程

        public double erfa; // 偏角(°.′″)

        public double K;

        public double Rad; // 半径(m)

        public double l0; // 缓和曲线长(m)

        public bool IsSame(InfoData d)
        {
            if (d.Name.Equals(Name))
            {
                return true;
            }
            return false;
        }

    }
}