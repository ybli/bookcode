using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convolution
{
    class Algo
    {
        double[,] M;
        double[,] N;

        public Algo(double[,] M, double[,] N)
        {
            this.M = M;
            this.N = N;
        }

        public double[,] Algo1()
        {

            int n = 10;
            double[,] V = new double[n, n];
            for (int I = 0; I < n; I++)
            {
                for (int J = 0; J < n; J++)
                {
                    V[I, J] = V1(I, J);
                }
            }

            return V;
        }

        public double[,] Algo2()
        {

            int n = 10;
            double[,] V = new double[n, n];
            for (int I = 0; I < n; I++)
            {
                for (int J = 0; J < n; J++)
                {
                    V[I, J] = V2(I, J);
                }
            }

            return V;
        }


        private double V1(int I, int J)
        {
            int m = 3;
            double v = 0;
            double upper = 0;
            double under = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                   double mij = M_IJ(I, J, i, j);
                    double eps = 1e-10;
                    if (Math.Abs(mij )> eps)
                    {
                        upper += mij * N[I - i - 1, J - j - 1];
                        under += mij;
                    }

                }
            }
            return upper / under;
        }


        private double V2(int I, int J)
        {
            int m = 3;
            double v = 0;
            double upper = 0;
            double under = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double mij = M_IJ(I, J, i, j);
                    double eps = 1e-10;
                    if (Math.Abs(mij) > eps)
                    {
                        upper += mij * N[9-(I - i - 1),9-( J - j - 1)];
                        under += mij;
                    }

                }
            }
            return upper / under;
        }

        private double M_IJ(int I, int J, int i, int j)
        {
            int n = 10;
            double res = 0;
            if (I - i - 1 < 0 || J - j - 1 < 0
                || I - i - 1 > n - 1 || J - j - 1 > 9)
               res = 0;
            else
            {
               res= M[i, j];
            }
            return res;
        }
       
    }
}
