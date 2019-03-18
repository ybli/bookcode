using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolution
{
    class Report
    {
        //报告矩阵输出
        public static string Text(string title, double[,] mat, int nrow, int ncol)
        {
            string line = title + "\n";
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    line += $"{mat[i, j]:f2}  ";
                }
                line += "\n";
            }

            return line;
        }
    }
}
