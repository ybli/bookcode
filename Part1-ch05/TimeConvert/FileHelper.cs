using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeConvert
{
    class FileHelper
    {
        public static List<Time> Read(string filename)
        {
            var data = new List<Time>();
            try
            {
                var reader = new StreamReader(filename);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Length > 0)
                    {
                        Time tm = Parse(line);
                        data.Add(tm);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return data;
        }

        private static Time Parse(string line)
        {
            int year = Convert.ToInt32(line.Substring(0, 4));
            int month = Convert.ToInt32(line.Substring(5, 2));
            int day = Convert.ToInt32(line.Substring(8, 2));
            int hour = Convert.ToInt32(line.Substring(11, 2));
            int min = Convert.ToInt32(line.Substring(14, 2));
            double sec = Convert.ToDouble(line.Substring(16, line.Length - 16));
            var tm = new Time(year, month, day, hour, min, sec);
            return tm;
        }

        public static void Write(string text, string filename)
        {
            var writer=new StreamWriter(filename);
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
