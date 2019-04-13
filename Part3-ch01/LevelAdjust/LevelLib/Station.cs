using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelLib
{
    public class Station
    {
        public PointInfo Point1;
        public PointInfo Point2;
        public List<double> list = new List<double>();
        public double D;
        public double deltaH;
        public double v;
        public static List<double> InitList(string[] a)
        {
            List<double> array = new List<double>();
            for(int i = 2; i<a.Length; i++)
            {
                array.Add(Convert.ToDouble(a[i]));
            }
            return array;
        }
    }
}
