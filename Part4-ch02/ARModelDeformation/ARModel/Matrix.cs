using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARModel
{
    //public class Matrix
    //{
    //    /// <summary>
    //    /// 矩阵类
    //    /// </summary>
    //    private int numColumns = 0;
    //    private int numRows = 0;
    //    private double eps = 0.0; // 缺省精度
    //    private double[,] elements = null;
    //    private bool flag = false;
    //    public bool Flag
    //    {
    //        get { return this.flag; }
    //        set { this.flag = value; }
    //    }

    //    /// <summary>
    //    /// 矩阵的列数
    //    /// </summary>                                         
    //    public int Columns
    //    {
    //        get { return numColumns; }
    //    }
    //    /// <summary>
    //    /// 矩阵的行数
    //    /// </summary>
    //    public int Rows
    //    {
    //        get { return numRows; }
    //    }
    //    /// <summary>
    //    /// 获取矩阵元素的索引器
    //    /// </summary>
    //    /// <param name="row"><行>
    //    /// <param name="col"><列>
    //    /// <returns></returns>
    //    public double this[int row, int col]
    //    {
    //        get { return elements[row, col]; }
    //        set { elements[row, col] = value; }
    //    }
    //    /// <summary>
    //    /// 对某一行进行赋值
    //    /// </summary>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    public Matrix this[int row]
    //    {
    //        set
    //        {
    //            double[,] m = value;
    //            Array.Copy(m, 0, elements, row * this.Columns, m.Length);
    //        }
    //        get
    //        { return this.GetR(row, row); }
    //    }



    //    /// <summary>
    //    ///1.基本构造函数
    //    /// </summary>
    //    public Matrix()
    //    {
    //        numColumns = 1;
    //        numRows = 1;
    //        this.elements = new double[1, 1];
    //    }
    //    /// <summary>
    //    /// 2.指定行列构造函数
    //    /// </summary>
    //    /// <param name="nRows"><行数>
    //    /// <param name="nCols"><列数>
    //    public Matrix(int nRows, int nCols)
    //    {
    //        this.numRows = nRows;
    //        this.numColumns = nCols;
    //        this.elements = new double[nRows, nCols];
    //    }
    //    /// <summary>
    //    /// 3.指定二维数组的构造函数
    //    /// </summary>
    //    /// <param name="value"></param>
    //    public Matrix(double[,] value)
    //    {
    //        this.numRows = value.GetLength(0);
    //        this.numColumns = value.GetLength(1);
    //        this.elements = value;
    //    }
    //    /// <summary>
    //    /// 4.指定值的一维数组构造函数
    //    /// </summary>
    //    /// <param name="nRows"><行>
    //    /// <param name="nCols"><列>
    //    /// <param name="value"><一维数组>
    //    public Matrix(int nRows, int nCols, double[] value)
    //    {
    //        this.numRows = nRows;
    //        this.numColumns = nCols;
    //        this.elements = new double[nRows, nCols];
    //        int n = 0;
    //        for (int i = 0; i < this.elements.GetLength(0); i++)
    //        {
    //            for (int j = 0; j < this.elements.GetLength(1); j++)
    //            {
    //                this.elements[i, j] = value[n++];
    //            }
    //        }
    //    }
    //    /// <summary>
    //    ///5. 指定大小的方阵构造函数
    //    /// </summary>
    //    /// <param name="nSize"><方阵的行列数>
    //    public Matrix(int nSize)
    //    {
    //        this.numRows = nSize;
    //        this.numColumns = nSize;
    //        this.elements = new double[nSize, nSize];
    //    }
    //    /// <summary>
    //    /// 6.方阵构造函数，将一维数组进行赋值
    //    /// </summary>
    //    /// <param name="nSize"><方阵的行列数>
    //    /// <param name="value"><一维数组>
    //    public Matrix(int nSize, double[] value)
    //    {
    //        this.numRows = nSize;
    //        this.numColumns = nSize;
    //        this.elements = new double[nSize, nSize];
    //        int n = 0;
    //        for (int i = 0; i < this.elements.GetLength(0); i++)
    //        {
    //            for (int j = 0; j < this.elements.GetLength(1); j++)
    //            {
    //                this.elements[i, j] = value[n++];
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 7.拷贝构造函数（浅拷贝，只拷贝值不拷贝地址）
    //    /// </summary>
    //    /// <param name="other"><原矩阵>
    //    public Matrix(Matrix other)
    //    {
    //        this.numColumns = other.Columns;
    //        this.numRows = other.Rows;
    //        this.elements = (double[,])other.elements.Clone();
    //    }
    //    /// <summary>
    //    /// +符号重载
    //    /// </summary>
    //    /// <param name="m1"><左矩阵>
    //    /// <param name="m2"><右矩阵>
    //    /// <returns><两矩阵之和>
    //    public static Matrix operator +(Matrix m1, Matrix m2)
    //    {
    //        return m1.Add(m2);
    //    }
    //    /// <summary>
    //    ///// 求逆运算符
    //    /// </summary>
    //    /// <param name="m"></param>
    //    /// <param name="m2"></param>
    //    /// <returns></returns>

    //    public static Matrix operator /(Matrix m, Matrix m2)
    //    {
    //        return m * MatrixInv(m2);
    //    }
    //    /// <summary>
    //    /// -符号重载
    //    /// </summary>
    //    /// <param name="m1"><左矩阵>
    //    /// <param name="m2"><右矩阵>
    //    /// <returns><两矩阵之差>
    //    public static Matrix operator -(Matrix m1, Matrix m2)
    //    {
    //        return m1.Subtract(m2);
    //    }
    //    /// <summary>
    //    /// *符号重载
    //    /// </summary>
    //    /// <param name="m1"><左矩阵>
    //    /// <param name="m2"><右矩阵>
    //    /// <returns><矩阵之积>
    //    public static Matrix operator *(Matrix m1, Matrix m2)
    //    {
    //        return m1.Multiply(m2);
    //    }
    //    /// <summary>
    //    ///矩阵的转置重载
    //    /// </summary>
    //    /// <param name="m1"></param>
    //    /// <param name="m2"></param>
    //    /// <returns></returns>
    //    public static Matrix operator ~(Matrix m2)
    //    {
    //        return Transpose(m2);
    //    }
    //    /// <summary>
    //    /// 数乘运算
    //    /// </summary>
    //    /// <param name="m1"><数>
    //    /// <param name="m2"><矩阵>
    //    /// <returns></returns>
    //    public static Matrix operator *(double m1, Matrix m2)
    //    {
    //        return m2.Multiply(m1);
    //    }
    //    /// <summary>
    //    /// 数乘运算
    //    /// </summary>
    //    /// <param name="m1"><数>
    //    /// <param name="m2"><矩阵>
    //    /// <returns></returns>
    //    public static Matrix operator *(Matrix m1, double m2)
    //    {
    //        return m1.Multiply(m2);
    //    }
    //    /// <summary>
    //    /// double[,]重载(矩阵转化成二维数组)
    //    /// </summary>
    //    /// <param name="m"><要转换的目标矩阵>
    //    public static implicit operator double[,] (Matrix m)
    //    {
    //        return m.elements;
    //    }
    //    /// <summary>
    //    /// [,]重载(二维数组转化成矩阵)
    //    /// </summary>
    //    /// <param name="D"><要转换的二维数组>
    //    public static implicit operator Matrix(double[,] D)
    //    {
    //        Matrix m = new Matrix(D);
    //        return m;
    //    }
    //    /// <summary>
    //    /// 将矩阵转化成一维数组
    //    /// </summary>
    //    /// <param name="m"><要转换的一维数组>
    //    public static implicit operator double[] (Matrix m)
    //    {
    //        double[] k = new double[m.Rows * m.Columns];
    //        int n = 0;
    //        for (int i = 0; i < m.Rows; i++)
    //        {
    //            for (int j = 0; j < m.Columns; j++)
    //            {
    //                k[n++] = m[i, j];
    //            }
    //        }
    //        return k;
    //    }
    //    /// <summary>
    //    /// 判断矩阵是否相同
    //    /// </summary>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public override bool Equals(object other)
    //    {
    //        Matrix matrix = other as Matrix;
    //        if (matrix == null)
    //            return false;
    //        // 首先检查行列数是否相等
    //        if (numColumns != matrix.Columns || numRows != matrix.Rows)
    //            return false;
    //        for (int i = 0; i < numRows; ++i)
    //        {
    //            for (int j = 0; j < numColumns; ++j)
    //            {
    //                if (Math.Abs(this[i, j] - matrix[i, j]) > eps)
    //                    return false;
    //            }
    //        }
    //        return true;
    //    }
    //    /// <summary>
    //    ///  GetHashCode()重载
    //    /// </summary>
    //    /// <returns></returns>
    //    public override int GetHashCode()
    //    {
    //        double sum = 0;
    //        for (int i = 0; i < numRows; ++i)
    //        {
    //            for (int j = 0; j < numColumns; ++j)
    //            {
    //                sum += Math.Abs(this[i, j]);
    //            }
    //        }
    //        return (int)Math.Sqrt(sum);
    //    }
    //    /// <summary>
    //    /// 矩阵的加法
    //    /// </summary>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public Matrix Add(Matrix other)
    //    {
    //        if (numColumns != other.Columns || numRows != other.Rows)
    //            throw new Exception("矩阵的行/列数不匹配。");
    //        Matrix result = new Matrix(this.Rows, this.Columns);
    //        for (int i = 0; i < numRows; i++)
    //        {
    //            for (int j = 0; j < numColumns; j++)
    //            {
    //                result[i, j] = this[i, j] + other[i, j];
    //            }
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 矩阵的减法
    //    /// </summary>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public Matrix Subtract(Matrix other)
    //    {
    //        if (this.numColumns != other.Columns || this.numRows != other.Rows)
    //            throw new Exception("矩阵的行/列数不匹配。");
    //        Matrix result = new Matrix(this.Rows, this.Columns); // 拷贝构造函数
    //        for (int i = 0; i < Rows; i++)
    //        {
    //            for (int j = 0; j < numColumns; j++)
    //            {
    //                result[i, j] = this[i, j] - other[i, j];
    //            }
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 矩阵的数乘
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public Matrix Multiply(double value)
    //    {
    //        // 构造目标矩阵
    //        Matrix result = new Matrix(this.Rows, this.Columns);
    //        // 进行数乘
    //        for (int i = 0; i < numRows; i++)
    //        {
    //            for (int j = 0; j < numColumns; j++)
    //            {
    //                result[i, j] = this[i, j] * value;
    //            }
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 矩阵的乘法
    //    /// </summary>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public Matrix Multiply(Matrix other)
    //    {
    //        if (this.numColumns != other.Rows)
    //            throw new Exception("矩阵的行/列数不匹配。");
    //        Matrix result = new Matrix(new double[this.numRows, other.Columns]);
    //        if (this.flag)
    //        {
    //            for (int i = 0; i < other.Rows; i++)
    //            {
    //                result[i] = this[i, i] * other[i];
    //            }
    //        }
    //        else
    //        {
    //            for (int i = 0; i < result.Rows; i++)
    //            {
    //                for (int k = 0; k < this.numColumns; k++)
    //                {
    //                    if (this[i, k] != 0)
    //                    {
    //                        for (int j = 0; j < other.Columns; j++)
    //                        {
    //                            result[i, j] += this[i, k] * other[k, j];
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        return result;
    //    }




    //    /// <summary>
    //    /// 获取矩阵指定行和列的集合
    //    /// </summary>
    //    /// <param name="r1"></param>
    //    /// <param name="r2"></param>
    //    /// <param name="c1"></param>
    //    /// <param name="c2"></param>
    //    /// <returns></returns>
    //    public Matrix GetRC(int r1, int r2, int c1, int c2)
    //    {
    //        Matrix p = new Matrix(new double[r2 - r1 + 1, c2 - c1 + 1]);
    //        for (int i = r1; i < r2 + 1; i++)
    //        {
    //            for (int j = c1; j < c2 + 1; j++)
    //            {
    //                p[i - r1, j - c1] = this[i, j];
    //            }
    //        }
    //        return p;
    //    }
    //    /// <summary>
    //    /// 获取指定行的集合
    //    /// </summary>
    //    /// <param name="r1"><起始行索引>
    //    /// <param name="r2"><终止行索引>
    //    /// <returns><指定行的矩阵>
    //    public Matrix GetR(int r1, int r2)
    //    {
    //        Matrix Target = new Matrix(new double[r2 - r1 + 1, this.Columns]);
    //        Array.Copy(this.elements, r1 * this.Columns, Target.elements, 0, (r2 - r1 + 1) * this.Columns);
    //        return Target;
    //    }
    //    /// <summary>
    //    /// 获取指定列的集合
    //    /// </summary>
    //    /// <param name="c1"><起始列索引>
    //    /// <param name="c2"><终止列索引>
    //    /// <returns></returns>
    //    public Matrix GetC(int c1, int c2)
    //    {
    //        Matrix p = new Matrix(new double[this.Rows, c2 - c1 + 1]);
    //        for (int i = 0; i < this.Rows; i++)
    //        {
    //            for (int j = c1; j < c2 + 1; j++)
    //            {
    //                p[i, j - c1] = this[i, j];
    //            }
    //        }
    //        return p;
    //    }
    //    /// <summary>
    //    /// 矩阵的转置
    //    /// </summary>
    //    /// <param name="a"></param>
    //    /// <returns></returns>
    //    public static Matrix Transpose(Matrix a)
    //    {
    //        Matrix Trans = new Matrix(new double[a.Columns, a.Rows]);
    //        for (int i = 0; i < Trans.Rows; i++)
    //        {
    //            for (int j = 0; j < Trans.Columns; j++)
    //            {
    //                Trans[i, j] = a[j, i];
    //            }
    //        }
    //        return Trans;
    //    }
    //    /// <summary>
    //    /// 矩阵的合并
    //    /// </summary>
    //    /// <param name="a"></param>
    //    /// <param name="b"></param>
    //    /// <param name="c"></param>
    //    /// <param name="d"></param>
    //    /// <returns></returns>
    //    public static Matrix Merge(Matrix a, Matrix b, Matrix c, Matrix d)
    //    {
    //        if (a.Rows == b.Rows && c.Rows == d.Rows && a.Columns == c.Columns && b.Columns == d.Columns)
    //        {
    //            double[,] merge1 = new double[a.Rows + c.Rows, a.Columns];
    //            double[,] merge2 = new double[a.Rows + c.Rows, b.Columns];
    //            double[,] merge = new double[a.Rows + c.Rows, a.Columns + b.Columns];
    //            Array.Copy(a.elements, merge1, a.elements.Length);
    //            Array.Copy(c.elements, 0, merge1, a.elements.Length, c.elements.Length);
    //            Array.Copy(b.elements, merge2, b.elements.Length);
    //            Array.Copy(d.elements, 0, merge2, b.elements.Length, d.elements.Length);
    //            for (int i = 0; i < merge.GetLength(0); i++)
    //            {
    //                Array.Copy(merge1, i * merge1.GetLength(1), merge, i * merge.GetLength(1), merge1.GetLength(1));
    //                Array.Copy(merge2, i * merge2.GetLength(1), merge, i * merge.GetLength(1) + merge1.GetLength(1), merge2.GetLength(1));
    //            }
    //            return new Matrix(merge);
    //        }
    //        else
    //        {
    //            throw new Exception("合并的矩阵中存在行列不匹配！");
    //        }
    //    }
    //    /// <summary>
    //    /// 矩阵进行横向合并
    //    /// </summary>
    //    /// <param name="a"></param>
    //    /// <param name="b"></param>
    //    /// <returns></returns>
    //    public static Matrix MergeR(Matrix a, Matrix b)
    //    {
    //        if (a.Rows == b.Rows)
    //        {
    //            Matrix merge = new double[a.Rows, a.Columns + b.Columns];
    //            for (int i = 0; i < merge.Rows; i++)
    //            {
    //                Array.Copy(a.elements, i * a.Columns, merge.elements, i * merge.Columns, a.Columns);
    //                Array.Copy(b.elements, i * b.Columns, merge.elements, i * merge.Columns + a.Columns, b.Columns);
    //            }
    //            return merge;
    //        }
    //        else
    //        {
    //            throw new Exception("两个矩阵的行数不等！");
    //        }
    //    }
    //    /// <summary>
    //    /// 矩阵进行纵向合并
    //    /// </summary>
    //    /// <param name="a"></param>
    //    /// <param name="b"></param>
    //    /// <returns></returns>
    //    public static Matrix MergeC(Matrix a, Matrix b)
    //    {
    //        if (a.Columns == b.Columns)
    //        {
    //            Matrix C = new double[a.Rows + b.Rows, a.Columns];
    //            Array.Copy(a.elements, C.elements, a.elements.Length);
    //            Array.Copy(b.elements, 0, C.elements, a.elements.Length, b.elements.Length);
    //            return C;
    //        }
    //        else
    //        {
    //            throw new Exception("两个矩阵的列数不等！");
    //        }
    //    }
    //    /// <summary>
    //    /// 获取单位矩阵
    //    /// </summary>
    //    /// <param name="s"></param>
    //    /// <returns></returns>
    //    public static Matrix Eyes(int s)
    //    {
    //        Matrix eye = new Matrix(s);
    //        for (int i = 0; i < eye.Rows; i++)
    //        {
    //            eye[i, i] = 1;
    //        }
    //        return eye;
    //    }
    //    /// <summary>
    //    /// 矩阵求逆(高斯法)
    //    /// </summary>
    //    /// <param name="Ma"></param>
    //    /// <returns></returns>
    //    public static Matrix MatrixInv(Matrix Ma)
    //    {
    //        int m = Ma.Rows;
    //        int n = Ma.Columns;
    //        if (m != n)
    //        {
    //            Exception myException = new Exception("数组维数不匹配");
    //            throw myException;
    //        }
    //        Matrix Mc = new Matrix(m, n);
    //        double[,] a = (double[,])Ma.elements.Clone();
    //        double[,] b = Mc.elements;
    //        int i, j, row, k;
    //        double max, temp;
    //        //单位矩阵
    //        for (i = 0; i < n; i++)
    //        {
    //            b[i, i] = 1;
    //        }
    //        for (k = 0; k < n; k++)
    //        {
    //            max = 0; row = k;
    //            //找最大元，其所在行为row
    //            for (i = k; i < n; i++)
    //            {
    //                temp = Math.Abs(a[i, k]);
    //                if (max < temp)
    //                {
    //                    max = temp;
    //                    row = i;
    //                }
    //            }
    //            if (max == 0)
    //            {
    //                //Exception myException = new Exception("没有逆矩阵");
    //                //throw myException;
    //                return Mc = null;
    //            }
    //            //交换k与row行
    //            if (row != k)
    //            {
    //                for (j = 0; j < n; j++)
    //                {
    //                    temp = a[row, j];
    //                    a[row, j] = a[k, j];
    //                    a[k, j] = temp;

    //                    temp = b[row, j];
    //                    b[row, j] = b[k, j];
    //                    b[k, j] = temp;
    //                }

    //            }
    //            //首元化为1
    //            for (j = k + 1; j < n; j++) a[k, j] /= a[k, k];
    //            for (j = 0; j < n; j++) b[k, j] /= a[k, k];

    //            a[k, k] = 1;

    //            //k列化为0
    //            //对a
    //            for (j = k + 1; j < n; j++)
    //            {
    //                for (i = 0; i < k; i++) a[i, j] -= a[i, k] * a[k, j];
    //                for (i = k + 1; i < n; i++) a[i, j] -= a[i, k] * a[k, j];
    //            }
    //            //对b
    //            for (j = 0; j < n; j++)
    //            {
    //                for (i = 0; i < k; i++) b[i, j] -= a[i, k] * b[k, j];
    //                for (i = k + 1; i < n; i++) b[i, j] -= a[i, k] * b[k, j];
    //            }
    //            for (i = 0; i < n; i++) a[i, k] = 0;
    //            a[k, k] = 1;
    //        }

    //        return Mc;
    //    }

    //}
}
