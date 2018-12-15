using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeConvert
{
    class Algo
    {
        List<Time> Data;
        public Algo(List<Time> data)
        {
            Data = data;
        }

        public string ToReport1()
        {
            string line = "-------原始数据内容-----------\r\n";
            foreach (var d in Data)
            {
                line += d.ToYmdHmsString() + "\r\n";
            }
            return line;
        }

        public string ToReport()
        {
            string line = "-------JD-----------\r\n";
            foreach (var d in Data)
            {
                line += $"{d.Jd:f5}\r\n";
            }

            line += "-------公历（年 月 日 时：分：秒)----------\r\n";
            foreach (var d in Data)
            {
                line += d.ToYmdHmsString() + "\r\n";
            }

            line += "-------年积日----------\r\n";
            foreach (var d in Data)
            {
                line += d.Doy + "\r\n";
            }

            line += "-------三天打鱼两天晒网----------\r\n";
            foreach (var d in Data)
            {
                line += FishingDay(d)+ "\r\n";
            }
            return line;
        }

        public  string FishingDay(Time tm)
        {
            string line = "";
            var d2016=new Time(2016,1,1,0,0,0.0);
            int total = Convert.ToInt32(Math.Floor(tm.Jd - d2016.Jd));
            int res = total%5;
            if (res < 3)
            {
                line = tm.ToYmdString()+",打鱼日";
            }
            else
            {
                line = tm.ToYmdString() + ",晒网日";
            }
            return line;
        }
    }
}
