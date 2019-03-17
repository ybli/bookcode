using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolution
{
    class FileHelper
    {

        /// <summary>
        /// 读取M文件
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>所读入的文件内容</returns>
        public static double[,] ReadM(string file)
        {
            int n = 3;
            var M = new double[n, n];
            try
            {
                var reader = new StreamReader(file);
                for (int i = 0; i < n; i++)
                {
                    string line = reader.ReadLine();
                    line = line.Replace("	", " ");
                    var buf = line.Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        M[i, j] = Convert.ToDouble(buf[j]);
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return M;
        }
        /// <summary>
        /// 读入N文件
        /// </summary>
        /// <param name="file">文件内容</param>
        /// <returns>文件的内容</returns>
        public static double[,] ReadN(string file)
        {
            int n = 10;
            var N = new double[n, n];
            try
            {
                var reader = new StreamReader(file);
                for (int i = 0; i < n; i++)
                {
                    string line = reader.ReadLine();
                    line = line.Replace("	", " ");
                    var buf = line.Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        N[i, j] = Convert.ToDouble(buf[j]);
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return N;
        }
        /// <summary>
        /// 写出计算结果
        /// </summary>
        /// <param name="text">计算结果</param>
        /// <param name="filename">文件路径</param>
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
