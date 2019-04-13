
//引用自 https://github.com/ybli/bookcode/tree/master/Part1-ch13

using System;
using System.Windows.Forms;
namespace SevenParameterTransformation
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
            int columns = martix.GetLength(1);
            Element = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    Element[i, j] = martix[i, j];
        }
        /// <summary>
        /// 输入为交错数组
        /// </summary>
        /// <param name="martix"></param>
        public Martix(double[][] martix)
        {
            int rows = martix.GetLength(0);
            int columns = martix.GetLength(1);
            Element = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
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
                        {
                            result = complement(martix).Transpose() * (1 / Determinant(martix));
                        }
                        else
                            result.Element[0, 0] = 1 / martix[0, 0];
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
            //注：运用此方法求解行列式，有时结果错误。下例：det(test1)求解结果正确，但det(test2)结果错误。原因不明。
            //double[,] test1 = { { 1, 2, 3, 5,1,2,5 }, { 3, 4, 5, 9,5,9,7 }, { 8, 6, 9, 10,10,25,36 }, { 12, 23, 15, 20 ,15,20,12} ,
            //    { 12,14,15,52,36,25,14},{ 48,56,19,47,23,14,15},{ 42,20,23,12,14,15,26} };
            //double[,] test2 = {
            //    { 6, 0, 0, 0, -24457908.6589, 26916348.4769, -11760075.3881},
            //    { 0,6 ,0,24457908.6589,0,11760075.3881,26916348.4769},
            //    { 0,0,6 ,-26916348.4769,-11760075.3881,0,24457908.6589},
            //    { 0,24457908.6589,-26916348.476900,22044684068435,52756432634080.4,47937688228728.8,0},
            //    { -24457908.6589,0,-11760075.388100,52756432634080.4,122748456455284 ,-109719457014783 ,0},
            //    { 26916348.4769,11760075.3881,0,47937688228728.8,-109719457014783 ,143798427582131 ,0},
            //    { -11760075.3881,26916348.4769,24457908.6589,0,0,0,243496862360883 } };


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


        public Martix Inverse(double[,] Array)
        {
            //引用自：https://www.cnblogs.com/renge-blogs/p/6308912.html
            int m = 0;
            int n = 0;
            m = Array.GetLength(0);
            n = Array.GetLength(1);
            double[,] array = new double[2 * m + 1, 2 * n + 1];
            for (int k = 0; k < 2 * m + 1; k++)  //初始化数组
            {
                for (int t = 0; t < 2 * n + 1; t++)
                {
                    array[k, t] = 0.00000000;
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = Array[i, j];
                }
            }

            for (int k = 0; k < m; k++)
            {
                for (int t = n; t <= 2 * n; t++)
                {
                    if ((t - k) == m)
                    {
                        array[k, t] = 1.0;
                    }
                    else
                    {
                        array[k, t] = 0;
                    }
                }
            }
            //得到逆矩阵
            for (int k = 0; k < m; k++)
            {
                if (array[k, k] != 1)
                {
                    double bs = array[k, k];
                    array[k, k] = 1;
                    for (int p = k + 1; p < 2 * n; p++)
                    {
                        array[k, p] /= bs;
                    }
                }
                for (int q = 0; q < m; q++)
                {
                    if (q != k)
                    {
                        double bs = array[q, k];
                        for (int p = 0; p < 2 * n; p++)
                        {
                            array[q, p] -= bs * array[k, p];
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            double[,] NI = new double[m, n];
            for (int x = 0; x < m; x++)
            {
                for (int y = n; y < 2 * n; y++)
                {
                    NI[x, y - n] = array[x, y];
                }
            }
            return new Martix(NI);
        }

    }
}
