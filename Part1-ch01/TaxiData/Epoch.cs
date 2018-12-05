using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiData
{
    /// <summary>
    /// 一个历元的数据结构
    /// </summary>
    class Epoch
    {
        public string Id;
        public int Status;
        public string TimeStr;
        public double Mjd;
        public double x;
        public double y;

        public void Parse(string line)
        {
            try
            {
                var buf = line.Split(',');
                Id = buf[0];
                Status = Convert.ToInt32(buf[1]);
                TimeStr = buf[2];
                x = Convert.ToDouble(buf[3]);
                y = Convert.ToDouble(buf[4]);
                GetMjd();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void GetMjd()
        {
            try
            {
                int timeZone = 8;
                int year = Convert.ToInt32(TimeStr.Substring(0, 4));
                int month = Convert.ToInt32(TimeStr.Substring(4, 2));
                int day = Convert.ToInt32(TimeStr.Substring(6, 2));
                int hour = Convert.ToInt32(TimeStr.Substring(8, 2));
                int min = Convert.ToInt32(TimeStr.Substring(10, 2));
                int sec = Convert.ToInt32(TimeStr.Substring(12, 2));

                Mjd = Algo.Mjd(year, month, day, hour, min, sec, timeZone);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
