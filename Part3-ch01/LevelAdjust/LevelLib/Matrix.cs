using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelLib
{
    public class Matrix
    {
        public int M;
        public int N;
        public List<double> array = new List<double>();

        public Matrix()
        {
            N = 0;
            array = new List<double>();
        }
        public Matrix(string filepath)
        {
            StreamReader stream = new StreamReader(filepath);
            string str = "";
            while ((str = stream.ReadLine()) != null)
            {
                string[] strs =str.Split(' ');
                N = strs.Length;
                M++;
                for(int i = 0; i < N; i++)
                {
                    array.Add(Convert.ToDouble(strs[i]));
                }
            }
            stream.Close();
        }

        public void print()
        {
            Console.WriteLine(M + " " + N);
        }

        public string printMatrix()
        {
            string str = "";
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    str += array[i * N + j] + " ";
                }
                str += "\r\n";
            }
            return str;
        }
        public static Matrix getInv(Matrix matrix)
        {
            Matrix div = new Matrix();
            div.N = matrix.N;
            div.M = matrix.M;
            double fenMu = getValue(matrix);
            for (int i = 0; i < matrix.N; i++)
            {
                for (int j = 0; j < matrix.N; j++)
                {
                    div.array.Add(getK(0, i) * getValue(getAccompany(matrix, j, i)) / fenMu);
                }
            }
            return div;
        }
        public static double getValue(Matrix matrix)
        {
            double value = 0;
            int N = matrix.N;
            if (N <= 1)
            {
                return matrix.array[0];
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    double t = matrix.array[i] * getK(0, i) * getValue(getAccompany(matrix, 0, i));
                    value += t;
                    //Console.WriteLine(t);
                }
                return value;
            }
        }

        public static int getK(int i,int j)
        {
            return (int)Math.Pow(-1, (i + j));
        }
        public static Matrix getAccompany(Matrix matrix,int i,int j)
        {
            int N = matrix.N;
            Matrix accompany=new Matrix();
            accompany.N = matrix.N - 1;
            for(int m = 0; m < N; m++)
            {
                for(int n = 0; n < N; n++)
                {
                    if (m!=i && n != j){
                        accompany.array.Add(matrix.array[m * N + n]);
                        //Console.WriteLine(matrix.array[m * N + n]);
                    }
                }
            }
            return accompany;
        }
        public void output(string filepath)
        {
            StreamWriter streamWriter = new StreamWriter(filepath);
            for(int i = 0; i < M; i++)
            {
                string str = "";
                for (int j = 0; j < N; j++)
                {
                    str += array[i * N + j] + " ";
                }
                streamWriter.WriteLine(str);
            }
            streamWriter.Close();
        }

        public static Matrix getT(Matrix matrix)
        {
            Matrix maT=new Matrix();
            maT.M = matrix.N;
            maT.N = matrix.M;
            for(int i = 0; i < maT.M; i++)
            {
                for(int j = 0; j < maT.N; j++)
                {
                    maT.array.Add(matrix.array[maT.M * j + i]);
;                }
            }
            return maT;
        }

        public static Matrix multiply(Matrix m1, Matrix m2)
        {
            Matrix newM = new Matrix();
            newM.M = m1.M;
            newM.N = m2.N;
            for(int k = 0; k < newM.M; k++)
            {
                for(int j = 0; j < newM.N; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < m1.N; i++)
                    {
                        sum += m1.array[k * m1.N + i] * m2.array[i *m2.N +j];
                    }
                    newM.array.Add(sum);
                }
            }
            return newM;
        }
    }
}
