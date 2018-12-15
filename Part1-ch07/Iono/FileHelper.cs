using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Iono
{
    class FileHelper
    {
        public static DataEntity Read(string filename)
        {
            DataEntity data=new DataEntity();
            try
            {
                var reader=new StreamReader(filename);
                string line = reader.ReadLine();
                Time tm = GetTime(line);

                data.Time = tm;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    Point pt=new Point();
                    pt.Parse(line);
                    data.Data.Add(pt);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return data;
        }

        //*  2016  8 16 10 45  0.00000000
        private static Time GetTime(string line)
        {
            int year = Convert.ToInt32(line.Substring(3, 4));
            int month = Convert.ToInt32(line.Substring(8, 2));
            int day = Convert.ToInt32(line.Substring(11, 2));
            int hour = Convert.ToInt32(line.Substring(14, 2));
            int min = Convert.ToInt32(line.Substring(17, 2));
            double second = Convert.ToDouble(line.Substring(20, line.Length - 21));

            Time tm=new Time(year,month,day,hour,min,second);
            return tm;
        }

        public static void Write(string text, string filename)
        {
            var writer = new StreamWriter(filename);
            writer.Write(text);
            writer.Close();
        }
    }
}
