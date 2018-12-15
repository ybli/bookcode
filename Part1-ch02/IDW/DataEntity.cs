using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IDW
{
    class DataEntity
    {
        public List<Point> Data;

        public int Count => Data.Count;

        public DataEntity()
        {
            Data = new List<Point>();
        }

        public void Add(Point pt)
        {
            Data.Add(pt);
        }

        public Point this[int i]
        {
            get { return Data[i]; }
            set { Data[i] = value; }
        }
        public override string ToString()
        {
            string res = "测站    X（m）    Y（m)      H(m)\n";
            foreach (var d in Data)
            {
                res += d.ToString() + "\n";
            }
            return res;
        }
    }
}
