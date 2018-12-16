using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringPointsForDesignedPoints
{
    public class CzxData
    {
        public string Name;

        public double X;

        public double Y;

        public bool IsSame(CzxData d)
        {
            if (d.Name.Equals(Name))
            {
                return true;
            }
            return false;
        }

    }
}
