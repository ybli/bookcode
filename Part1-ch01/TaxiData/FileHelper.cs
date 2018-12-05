using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaxiData
{
    /// <summary>
    /// （1）读文件
    /// （2）保存计算结果
    /// </summary>
    class FileHelper
    {
        /// <summary>
        /// 读取文件，将标识为Id的记录列表返回
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pathname"></param>
        /// <returns></returns>
        public static List<Epoch> Read(string Id, string pathname)
        {
            var data = new List<Epoch>();
            try
            {
                var reader = new StreamReader(pathname);
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Length > 0)
                    {
                        var ep = new Epoch();
                        ep.Parse(line);
                        if (Id.Equals(ep.Id))
                        {
                            data.Add(ep);
                        }
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

        public static void Write(SessionList data, string filename)
        {
            var writer = new StreamWriter(filename);
            writer.Write(data.ToString());
            writer.Close();
        }
    }
}
