using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace 矩阵运算
{
    /// <summary>
    /// 文件名:二维矩阵类/
    /// 功能描述:二维矩阵的常用运算，加减乘除，转置，求逆等/
    /// 
    /// Copyright(C)xzj/
    /// 2018.10.7/
    /// 修改:/
    /// 
    /// </summary>

    class Martix
    {
        #region 构造函数
        /// <summary>
        /// 输入为二维数组
        /// </summary>
        /// <param name="martix"></param>
        public Martix(double[,] martix)
        {
            int rows = martix.GetLength(0);
            int lines = martix.GetLength(1);
            Element = new double[rows, lines];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < lines; j++)
                    Element[i, j] = martix[i, j];
        }
        /// <summary>
        /// 输入为交错数组
        /// </summary>
        /// <param name="martix"></param>
        public Martix(double[][] martix)
        {
            int rows = martix.GetLength(0);
            int lines = martix.GetLength(1);
            Element = new double[rows, lines];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < lines; j++)
                    Element[i, j] = martix[i][j];
        }
        #endregion

        /// <summary>
        /// 矩阵对应的二维数组
        /// </summary>
        public double[,] Element { get; set; }
        /// <summary>
        /// 二维矩阵的行数
        /// </summary>
        public int Rows
        {
            get
            {
                //_rows = Element.GetLength(0);
                //return _rows;
                return Element.GetLength(0);
            }
            //private set
            //{
            //    _rows = value;
            //}
        }
        /// <summary>
        /// 二维矩阵的列数
        /// </summary>
        public int Columns
        {
            get
            {
                //_columns = Element.GetLength(1);
                //return _columns;
                return Element.GetLength(1);
            }
            //private set
            //{
            //    _columns = value;
            //}
        }
        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                if (i < Rows && j < Columns)
                    return Element[i, j];
                else
                {
                    System.Exception ex = new Exception("索引超出界限!");
                    throw ex;
                }
            }
            set
            {
                Element[i, j] = value;
            }
        }
        /// <summary>
        /// 获取二维矩阵的行数
        /// </summary>
        /// <returns></returns>
        public int GetRows()
        {
            return Element.GetLength(0);
        }
        /// <summary>
        /// 获取二维矩阵的列数
        /// </summary>
        /// <returns></returns>
        public int GetColumns()
        {
            return Element.GetLength(1);
        }

        #region 矩阵运算
        /// <summary>
        /// 二维矩阵加法运算
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="martix2"></param>
        /// <returns></returns>
        public static Martix operator +(Martix martix1, Martix martix2)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            if (martix1.Rows == martix2.Rows && martix1.Columns == martix2.Columns)
                for (int i = 0; i < martix1.Rows; i++)
                    for (int j = 0; j < martix1.Columns; j++)
                        result[i, j] = martix1[i, j] + martix2[i, j];
            else if (martix1.Rows != martix2.Rows)
            {
                MessageBox .Show ("加法运算行数不匹配!");
            }
            else
            {
                MessageBox.Show("加法运算列数不匹配!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵减法运算
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="martix2"></param>
        /// <returns></returns>
        public static Martix operator -(Martix martix1, Martix martix2)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            try
            {
                if (martix1.Rows == martix2.Rows && martix1.Columns == martix2.Columns)
                    for (int i = 0; i < martix1.Rows; i++)
                        for (int j = 0; j < martix1.Columns; j++)
                            result[i, j] = martix1[i, j] - martix2[i, j];
            }
            catch
            {
                if (martix1.Rows != martix2.Rows)
                {
                    MessageBox.Show("减法运算行数不匹配!");
                }
                if (martix1.Columns != martix2.Columns)
                {
                    MessageBox.Show("减法运算列数不匹配!");
                }
            }
            return result;
        }

        #region 二维矩阵乘法运算
        /// <summary>
        /// 二维矩阵乘法运算，矩阵相乘
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="martix2"></param>
        /// <returns></returns>
        public static Martix operator *(Martix martix1, Martix martix2)
        {
            double[,] array = new double[martix1.Rows, martix2.Columns];
            Martix result = new Martix(array);
            try
            {
                if (martix1.Columns == martix2.Rows)
                    for (int row1 = 0; row1 < martix1.Rows; row1++)
                    {
                        int row2 = 0;
                        for (int column2 = 0; column2 < martix2.Columns; column2++)
                        {
                            double Sum = 0;
                            for (int column1 = 0; column1 < martix1.Columns; column1++)
                            {
                                Sum += martix1[row1, column1] * martix2[column1, row2];
                            }
                            result[row1, column2] = Sum;
                            row2++;
                        }
                    }
                else
                {
                    MessageBox.Show("矩阵乘法运算两个矩阵行列不匹配!");
                }
            }
            catch
            {
                MessageBox.Show("矩阵乘法运算错误!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵乘法运算,矩阵左乘一个数
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Martix operator *(Martix martix1, double num)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            try
            {
                for (int i = 0; i < martix1.Rows; i++)
                {
                    for (int j = 0; j < martix1.Columns; j++)
                    {
                        result[i, j] = martix1[i, j] * num;
                    }
                }
            }
            catch
            {
                MessageBox.Show("矩阵左乘一个数错误!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵乘法运算,矩阵右乘一个数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="martix1"></param>
        /// <returns></returns>
        public static Martix operator *(double num, Martix martix1)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            try
            {
                for (int i = 0; i < martix1.Rows; i++)
                {
                    for (int j = 0; j < martix1.Columns; j++)
                    {
                        result[i, j] = martix1[i, j] * num;
                    }
                }
            }
            catch
            {
                MessageBox.Show("矩阵右乘一个数错误!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵乘法运算,矩阵左乘一个数
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Martix operator *(Martix martix1, int num)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            try
            {
                for (int i = 0; i < martix1.Rows; i++)
                {
                    for (int j = 0; j < martix1.Columns; j++)
                    {
                        result[i, j] = martix1[i, j] * num;
                    }
                }
            }
            catch
            {
                MessageBox.Show("矩阵左乘一个数错误!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵乘法运算,矩阵右乘一个数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="martix1"></param>
        /// <returns></returns>
        public static Martix operator *(int num, Martix martix1)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            try
            {
                for (int i = 0; i < martix1.Rows; i++)
                {
                    for (int j = 0; j < martix1.Columns; j++)
                    {
                        result[i, j] = martix1[i, j] * num;
                    }
                }
            }
            catch
            {
                MessageBox.Show("矩阵右乘一个数错误!");
            }
            return result;
        }
        #endregion

        #region 矩阵除法运算
        /// <summary>
        /// 二维矩阵除法运算
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="martix2"></param>
        /// <returns></returns>
        public static Martix operator /(Martix martix1, Martix martix2)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            if (martix2.Rows == martix2.Columns)
            {
                result = martix1 * martix2.Inverse();
            }
            else
            {
                MessageBox.Show("矩阵除法运算右边矩阵必须为方阵!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵除法运算,矩阵除以一个数
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Martix operator /(Martix martix1, double num)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            try
            {
                if (num != 0)
                {
                    result = martix1 * (1.0 / num);
                }
                else
                {
                    MessageBox.Show("除数不能为零!");
                }
            }
            catch
            {
                MessageBox.Show("矩阵除法运算错误!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵除法运算,矩阵除以一个数
        /// </summary>
        /// <param name="martix1"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Martix operator /(Martix martix1, int num)
        {
            double[,] array = new double[martix1.Rows, martix1.Columns];
            Martix result = new Martix(array);
            try
            {
                if (num != 0)
                {
                    result = martix1 *( 1.0 / num);
                }
                else
                {
                    MessageBox.Show("除数不能为零!");
                }
            }
            catch
            {
                MessageBox.Show("矩阵除法运算错误!");
            }
            return result;

        }
        #endregion

        /// <summary>
        /// 二维矩阵转置运算
        /// </summary>
        /// <returns></returns>
        public Martix Transpose()
        {
            double[,] array = new double[Element.GetLength(1), Element.GetLength(0)];
            Martix result = new Martix(array);
            try
            {
                for (int i = 0; i < Element.GetLength(0); i++)
                {
                    for (int j = 0; j < Element.GetLength(1); j++)
                    {
                        result[j, i] = Element[i, j];
                    }
                }
            }
            catch
            {
                MessageBox.Show("矩阵转置错误!");
            }
            return result;
        }
        /// <summary>
        /// 二维矩阵求逆运算
        /// </summary>
        /// <returns></returns>
        public Martix Inverse()
        {
            double[,] array = new double[Element.GetLength(1), Element.GetLength(0)];
            Martix result = new Martix(array);
            try
            {

                if (Element.GetLength(0) == Element.GetLength(1))//方阵
                {
                    Martix martix = new Martix(Element);
                    if (Determinant(martix) != 0)
                    {
                        if (martix.Rows > 1)
                            result = complement(martix).Transpose() * (1 / Determinant(martix));
                        else
                            result.Element [0,0] = 1 / martix[0, 0];
                    }
                    else
                    {
                        MessageBox.Show("矩阵行列式为0!");
                    }
                }
                else
                {
                    MessageBox.Show("非方阵矩阵无法求逆!");
                }
            }
            catch
            {
                MessageBox.Show("矩阵求逆错误!");
            }
            return result;
        }
        /// <summary>
        /// 递归计算方阵的行列式,代数余子式法
        /// </summary>
        /// <param name="martix"></param>
        /// <returns></returns>
        public double Determinant(Martix martix)
        {
            double sum = 0;
            try
            {
                int sign = 1;
                if (martix.Rows == 1)//递归出口
                {
                    return martix[0, 0] ;
                }
                for (int i = 0; i < martix.Rows; i++)
                {
                    double[,] _tempmatrix = new double[martix.Rows - 1, martix.Columns - 1];
                    Martix tempmatrix = new Martix(_tempmatrix);
                    for (int j = 0; j < martix.Rows - 1; j++)
                        for (int k = 0; k < martix.Columns - 1; k++)
                            tempmatrix[j, k] = martix[j + 1, k >= i ? k + 1 : k];
                    sum += sign * martix[0, i] * Determinant(tempmatrix);//递归，调用函数自身
                    sign = sign * (-1);
                }
               
            }
            catch
            {
                MessageBox.Show("矩阵求行列式错误!");
            }
            return sum;
        }
        /// <summary>
        /// 计算方阵伴随矩阵
        /// </summary>
        /// <param name="martix"></param>
        /// <returns></returns>
        private Martix complement(Martix martix)
        {
            double[,] array = new double[martix.Rows, martix.Columns];
            Martix result = new Martix(array);
            try
            {
                if (martix.Rows == martix.Columns)//方阵
                {

                    for (int i = 0; i < martix.Rows; i++)
                        for (int j = 0; j < martix.Columns; j++)
                        {
                            //生成aij的余子式矩阵
                            double[,] complement = new double[martix.Rows - 1, martix.Columns - 1];//n-1阶
                            Martix martix1 = new Martix(complement);//aij的余子式矩阵
                            int row = 0;
                            for (int k = 0; k < martix.Rows; k++)
                            {
                                int column = 0;
                                if (k == i)//去除第i行
                                    continue;
                                for (int l = 0; l < martix.Rows; l++)
                                {
                                    if (l == j)//去除第j列
                                        continue;
                                    martix1[row, column++] = martix[k, l];
                                }
                                row++;
                            }
                            result[i, j] = Math.Pow(-1, i + j) * Determinant(martix1);
                        }
                }
            }
            catch
            {
                MessageBox.Show("非方阵无法求伴随矩阵!");
            }
            return result;
        }
        #endregion
    }
}
