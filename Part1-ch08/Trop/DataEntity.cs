using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trop
{
    class DataEntity
    {
        public List<Point> Data;

        public DataEntity()
        {
            Data=new List<Point>();
        }
        public DataEntity(List<Point> data)
        {
            Data = data;
        }

        public void Add(Point pt )
        {
            Data.Add(pt);
        }

        public override string ToString()
        {
            string line = "测站，年，年积日, 经度（°），纬度（°），大地高（m）,，高度角（°）\n";
            foreach (var d in Data)
            {
                line += d.ToString() + "\n";
            }
            return line;
        }
    }
}
