using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP
{
  public  class matrix
    {
        private int row, column;
        private double[] A;

        public matrix(int r, int c)
        {
            row = r; column = c;
            A = new double[r * c];
        }
        public double this[int r, int c]
        {
            get { return A[(r - 1) * column + c - 1]; }
            set { A[(r - 1) * column + c - 1] = value; }
        }

        public int rows
        {
            get { return row; }
        }

        public int columns
        {
            get { return column; }
        }

        /*单位矩阵*/
        public static matrix eyes(int n)
        {
            matrix m = new matrix(n, n);
            for (int i = 1; i <= n; i++)
                m[i, i] = 1;
            return m;
        }
        /*矩阵拷贝*/
        public static matrix matcpy(matrix a)
        {
            matrix b = new matrix(a.rows, a.columns);
            for (int i = 1; i <= a.rows; i++)
                for (int j = 1; j <= a.columns; j++)
                    b[i, j] = a[i, j];
            return b;
        }
        /*矩阵加运算*/
        public static matrix operator +(matrix a, matrix b)
        {
            int row = a.rows, column = a.columns;
            if (row == b.rows && column == b.columns)
            {
                matrix c = new matrix(row, column);
                for (int i = 1; i <= row; i++)
                    for (int j = 1; j <= column; j++)
                        c[i, j] = a[i, j] + b[i, j];
                return c;
            }
            else { return null; }
        }
        /*矩阵减运算*/
        public static matrix operator -(matrix a, matrix b)
        {
            int row = a.rows, column = a.columns;
            if (row == b.rows && column == b.columns)
            {
                matrix c = new matrix(row, column);
                for (int i = 1; i <= row; i++)
                    for (int j = 1; j <= column; j++)
                        c[i, j] = a[i, j] - b[i, j];
                return c;
            }
            else { return null; }
        }
        /*矩阵乘法 矩阵与矩阵相乘*/
        public static matrix operator *(matrix a, matrix b)
        {
            int rows = a.rows, columns = b.columns;
            if (a.columns == b.rows)
            {
                matrix c = new matrix(rows, columns);
                int n = a.columns;

                for (int i = 1; i <= c.rows; i++)
                    for (int j = 1; j <= c.columns; j++)
                    {
                        double sum = 0;
                        for (int k = 1; k <= n; k++)
                            sum = sum + a[i, k] * b[k, j];
                        c[i, j] = sum;
                    }
                return c;
            }
            else
            {
                return null;
            }
        }
        /*矩阵乘法 矩阵与常量相乘*/
        public static matrix operator *(matrix a, double l)
        {
            int rows = a.rows, columns = a.columns;

            matrix c = new matrix(rows, columns);
            int n = a.columns;

            for (int i = 1; i <= c.rows; i++)
                for (int j = 1; j <= c.columns; j++)
                {
                    c[i, j] = l * a[i, j];
                }
            return c;
        }
        /*矩阵转置*/
        public static matrix transp(matrix a)
        {
            matrix b = new matrix(a.columns, a.rows);

            for (int i = 1; i <= b.rows; i++)
                for (int j = 1; j <= b.columns; j++)
                    b[i, j] = a[j, i];
            return b;

        }
        /*矩阵求逆 LUP分解*/
        public static matrix inv(matrix a)
        {
            int n = a.rows;
            if (a.rows == a.columns)//是否为方阵
            {
                matrix b = new matrix(n, n);
                matrix A = matrix.matcpy(a);
                matrix L = new matrix(n, n);
                matrix U = new matrix(n, n);
                int[] p = new int[n];
                matrix L1 = matrix.eyes(n);
                matrix U1 = matrix.eyes(n);
                matrix P1 = new matrix(n, n);
                #region
                
                #endregion
                for (int i = 0; i < n; i++)
                    p[i] = i + 1;//置换矩阵
                for (int j = 1; j < n; j++)
                {
                    double tmpmax = 0;
                    int imax = 0, itmp = 0;
                    for (int i = j; i <= n; i++)
                    {
                        if (Math.Abs(A[i, j]) > tmpmax)
                        { tmpmax = A[i, j]; imax = i; }
                    }
                    if (tmpmax == 0) { return null; }
                    if (imax != j)//交换
                    {
                        for (int k = 1; k <= n; k++)
                        { tmpmax = A[j, k]; A[j, k] = A[imax, k]; A[imax, k] = tmpmax; }
                        itmp = p[j - 1]; p[j - 1] = p[imax - 1]; p[imax - 1] = itmp;
                    }

                    for (int i = j + 1; i <= n; i++)//计算LU元素
                    {
                        A[i, j] = A[i, j] / A[j, j];
                        for (int k = j + 1; k <= n; k++)
                            A[i, k] = A[i, k] - A[i, j] * A[j, k];
                    }

                }
                //
                for (int i = 1; i <= n; i++)//分别得到LU
                {
                    L[i, i] = 1;
                    for (int j = 1; j <= n; j++)
                    {
                        if (i > j) L[i, j] = A[i, j];
                        else { U[i, j] = A[i, j]; }

                    }
                    if (U[i, i] == 0) return null;
                }
                for (int i = 1; i <= n; i++)//U矩阵化为单位上三角矩阵
                {
                    double t2 = U[i, i];
                    for (int j = 1; j <= n; j++)
                    {
                        U[i, j] = U[i, j] / t2;
                        U1[i, j] = U1[i, j] / t2;
                    }
                }
                for (int j = n; j > 1; j--)//U1，外循环从最后列开始
                {
                    for (int i = j - 1; i >= 1; i--)
                    {
                        for (int k = 1; k <= n; k++)
                            U1[i, k] = U1[i, k] - U[i, j] * U1[j, k];
                    }

                }
                for (int j = 1; j < n; j++)//L的逆，外循环从第一列开始
                {
                    for (int i = j + 1; i <= n; i++)
                    {
                        for (int k = 1; k <= n; k++)
                        { L1[i, k] = L1[i, k] - L[i, j] * L1[j, k]; }
                    }
                }

                for (int i = 1; i <= n; i++)//P1
                {
                    P1[i, p[i - 1]] = 1;
                }
                b = U1 * L1 * P1;
                return b;
            }
            else { return null; }
        }
        /*列向量点积运算*/
        public static double dotvector(matrix a, matrix b)//列向量
        {

            double value = 0;
            if (a.rows < a.columns || a.columns != 1) return -1;
            else
            {
                for (int i = 1; i <= a.rows; i++)
                    value += a[i, 1] * b[i, 1];
            }
            return value;
        }
        /*一维数组转换为列向量*/
        public static matrix Array2matrix(double[] r)
        {
            matrix A = new matrix(r.Length, 1);//列向量
            int n = r.Length;
            for (int i = 0; i < n; i++)
                A[i + 1, 1] = r[i];
            return A;
        }
        /*列向量转换为一维数组*/
        public static double[] matrx2Array(matrix a)//列向量转换为一维数组
        {
            int n = a.rows, c = a.columns;
            double[] r = null;
            if (c == 1)
            {
                r = new double[n];
                for (int i = 0; i < n; i++)
                    r[i] = a[i + 1, 1];
            }
            return r;
        }

        /*输出矩阵至控制台*/
        public static void matprint(matrix a)
        {
            for (int i = 1; i <= a.rows; i++)
            {
                for (int j = 1; j <= a.columns; j++)
                    Console.Write(a[i, j] + " ");
                Console.WriteLine();
            }

        }
    }
}
