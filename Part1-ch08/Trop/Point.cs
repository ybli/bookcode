using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trop
{
    class Point
    {
        public string Id;
        public Time Time;
        public double Lat;
        public double Lon;
        public double H;
        public double Elv;
        public void Parse(string line)
        {
            var buf = line.Split(',');
            Id = buf[0];
            Time = GetTime(buf[1]);
            Lon = Convert.ToDouble(buf[2]);
            Lat = Convert.ToDouble(buf[3]);
            H = Convert.ToDouble(buf[4]);
            Elv = Convert.ToDouble(buf[5]);
        }

        private Time GetTime(string s)
        {
            int y = Convert.ToInt32(s.Substring(0, 4));
            int m = Convert.ToInt32(s.Substring(4, 2));
            int d = Convert.ToInt32(s.Substring(6, 2));

            var t=new Time(y,m,d);
            return t;
        }

        public override string ToString()
        {
            string line = $"{Id},{Time.Year},{Time.Doy},{Lon:f6},{Lat:f6},{H:F3},{Elv:F3}";
            return line;
        }
    }
}
