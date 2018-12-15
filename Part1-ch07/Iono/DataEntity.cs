using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iono
{
    class DataEntity
    {
        public Time Time;
        public List<Point> Data;

        public DataEntity()
        {
            Time=new Time(2017,7,26);
           Data=new List<Point>();
        }

        public override string ToString()
        {
            string line = Time.ToYmdHmsString()+"\r\n";
            foreach (var d in Data)
            {
                line += d.ToString() + "\r\n";
            }
            return line;
        }
    }
}
