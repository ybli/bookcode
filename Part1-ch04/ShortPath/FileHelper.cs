using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ShortPath
{
    /// <summary>
    /// 1. 读取原始文件
    /// 2. 写出计算成果
    /// </summary>
    class FileHelper
    {
        public static Graph Read(string filename)
        {
            var edges = new List<Edge>();
            try
            {
                var reader = new StreamReader(filename);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Length > 0)
                    {
                        var edge = new Edge();
                        edge.Parse(line);
                        edges.Add(edge);
                    }
                }
                reader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            var data = new Graph(edges);
            return data;
        }

        public static void Write(string text, string filename)
        {
            try
            {
                var writer=new StreamWriter(filename);
                writer.WriteLine(text);
                writer.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
