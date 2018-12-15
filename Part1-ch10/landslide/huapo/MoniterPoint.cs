using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace landside
{
    /// <summary>
    /// 监测点
    /// </summary>
    public class MoniterPoint
    {
        public string Name { get; set; }
        public List<Coordinate> Data { get; set; }

        public MoniterPoint()
        {
            Name = string.Empty;
            Data = new List<Coordinate>();
        }

        public Coordinate this[int i]
        {
            set { Data[i] = value; }
            get { return Data[i]; }
        }

        public override string ToString()
        {
            string res = "测站    X（m）    Y（m)    \n";
            foreach (var d in Data)
            {
                res += d.ToString() + "\n";
            }
            return res;
        }
    }
}
