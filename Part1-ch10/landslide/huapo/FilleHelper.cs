using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace landside
{
    class FileHelper
    {
        public static List<MoniterPoint> Read(string filename)
        {
            var dataa = new List<MoniterPoint>();
            MoniterPoint data = new landside.MoniterPoint();
            try
            {
                var rd = new StreamReader(filename);
                string line = rd.ReadLine();
                int N = Convert.ToInt32(line);
                for (int i = 0; i < N; i++)
                {
                    line = rd.ReadLine();
                    var buf = line.Split(',');
                    string name = buf[0];
                    var mp = new MoniterPoint();
                    mp.Name = name;
                    int K = Convert.ToInt32(buf[1]);
                    for (int j = 0; j < K; j++)
                    {
                        line = rd.ReadLine();
                        Coordinate c = new Coordinate();
                        c.Parse(line);
                        mp.Data.Add(c);
                    }
                    dataa.Add(mp);
                }
                rd.Close();
                return dataa;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static void Write(string res, string file)
        {
            
            try
            {
                var writer = new StreamWriter(file);
                writer.Write(res);
                writer.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
