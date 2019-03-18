using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortPath
{
    /// <summary>
    /// 1. 实现最短路径计算
    /// 2. 记录最短路径所经过的顶点[待进一步完善]
    /// </summary>
    class Algo
    {
        string[,] path;
        Graph Data;

        private List<Vertex> Result;
        public Algo(Graph data)
        {
            Data = data;

            InitPath();

            GetShortPath();
        }

        /// <summary>
        /// 计算最短路径
        /// </summary>
        void GetShortPath()
        {
            Result = Data.Vertexes;
            int n = Result.Count;
            Result[0].Weight = 0;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    foreach (var d in Data.Edges)
                    {
                        if (d.Start == Result[i].Name && d.End == Result[j].Name)
                        {
                            double weight = Result[i].Weight + d.Length;
                            if (weight < Result[j].Weight)
                            {
                                Result[j].Weight = weight;
                            }
                        }
                    }
                }
            }

        }

        
        void InitPath()
        {
            int n = Data.Vertexes.Count;
            path = new string[n, n];
            for (int i = 0; i < n; i++)
            {

            }
        }

        public override string ToString()
        {
            string line="------------最短路径计算结果----------\r\n";
            foreach (var d in Result)
            {
                line += d.ToString() + "\r\n";
            }
            return line;
        }
    }
}
