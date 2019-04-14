using matrix;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARModel
{
    class CARFuction
    {

        /// <summary>
        /// 求二维数组中最小值的索引，返回一维数组，第一个元素为row，第二个元素为col
        /// </summary>
        /// <param name="arrys">目标二维数组</param>
        /// <returns></returns>
        public static int[] FindMin(double[,] arrys)
        {
            double[] Source = new double[arrys.GetLength(0) * arrys.GetLength(1)];
            int k = 0;
            for (int i = 0; i < arrys.GetLength(0); i++)
            {
                for (int j = 0; j < arrys.GetLength(1); j++)
                {
                    Source[k++] = arrys[i, j];
                }
            }
            double Min = Source.Min();
            int[] FindPositon = new int[2];
            for (int i = 0; i < arrys.GetLength(0); i++)
            {
                for (int j = 0; j < arrys.GetLength(1); j++)
                {
                    if (Min == arrys[i, j])
                    {
                        FindPositon[0] = i;
                        FindPositon[1] = j;
                        break;
                    }
                }
            }
            return FindPositon;
        }

        /// <summary>
        /// 对目标矩阵进行倒序排列，注意目标矩阵的列数为1
        /// </summary>
        /// <param name="matrix">目标矩阵</param>
        /// <returns></returns>
        public static Matrix Sort(Matrix matrix)
        {
            Matrix _matrix = new Matrix(matrix.Rows, matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                _matrix[i, 0] = matrix[matrix.Rows - i - 1, 0];

            }
            return _matrix;
        }

        /// <summary>
        /// 求目标列向量的平均值，返回与目标向量相同长度的列向量，每个元素的值都为目标向量的平均值
        /// </summary>
        /// <param name="matrix">目标矩阵</param>
        /// <returns></returns>
        public static Matrix mean(Matrix matrix)
        {
            double ave = 0.0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                ave += matrix[i, 0];
            }
            ave /= matrix.Rows;
            Matrix mat = new Matrix(matrix.Rows, 1);
            for (int i = 0; i < matrix.Rows; i++)
            {
                mat[i, 0] = ave;
            }
            return mat;
        }
        /// <summary>
        /// 去趋势项
        /// </summary>
        /// <param name="Y">目标观测序列</param>
        /// <returns></returns>
        public static Matrix RemovePeridTerm(double[,] Y, out Matrix m_PeriodicTerm)//去趋势项(周期项)
        {
            Matrix A = new double[Y.GetLength(0), 4];//构造系数矩阵
            for (int i = 0; i < Y.GetLength(0); i++)
            {
                A[i, 0] = 1;
                A[i, 1] = (i + 1) * 1.0 / 365;
                A[i, 2] = Math.Sin(2 * Math.PI * (i + 1) / 365.0);
                A[i, 3] = Math.Cos(2 * Math.PI * (i + 1) / 365.0);
            }
            Matrix phi = Matrix.MatrixInv(~A * A) * ~A * Y;
            m_PeriodicTerm = A * phi;
            Matrix Deal_Y = Y - A * phi;
            return Deal_Y;
        }
        /// <summary>
        /// 求ACF
        /// </summary>
        /// <param name="Deal_Y_Model"></param>
        /// <param name="T_lag"></param>
        /// <returns></returns>
        public static Matrix GetACF(Matrix Deal_Y_Model, int T_lag)
        {

            Matrix PCF = new Matrix(1, T_lag + 1);
            PCF[0, 0] = 1;
            double R0 = (~Deal_Y_Model * Deal_Y_Model)[0, 0] / Deal_Y_Model.Rows;   ///序列方差
            for (int i = 0; i < T_lag; i++)
            {
                double R = (~Deal_Y_Model.GetR(0, Deal_Y_Model.Rows - 1 - i - 1) * Deal_Y_Model.GetR(i + 1, Deal_Y_Model.Rows - 1))
                      [0, 0] / (Deal_Y_Model.Rows - i - 1);
                PCF[0, i + 1] = R / R0;
            }
            return PCF;
        }
        /// <summary>
        /// 求PACF
        /// </summary>
        /// <param name="T_lag"></param>
        /// <param name="PCF"></param>
        /// <returns></returns>
        public static Matrix GetPACF(int T_lag, Matrix PCF)
        {
            Matrix PACF = new Matrix(1, T_lag + 1);
            PACF[0, 0] = PCF[0, 0];
            for (int i = 0; i < T_lag; i++)
            {
                Matrix TR = Matrix.Eyes(i + 1);
                for (int j = 0; j <= i; j++)
                {
                    double k = PCF[0, j];
                    TR[0, j] = PCF[0, j];
                }
                for (int m = 1; m <= i; m++)
                {
                    Matrix _ACFm = new Matrix();
                    _ACFm[0, 0] = PCF[0, m];
                    TR[m] = Matrix.MergeR(_ACFm, TR.GetRC(m - 1, m - 1, 0, i - 1));//构造Yule-walker方程系数阵
                }
                Matrix Phi = Matrix.MatrixInv(TR) * ~PCF.GetC(1, i + 1);//得到方程系数
                PACF[0, i + 1] = Phi[i, 0];
            }
            return PACF;
        }
        /// <summary>
        /// 模型定阶，求BIC和AIC
        /// </summary>
        /// <param name="Deal_Y_Model"></param>
        /// <param name="T_lag"></param>
        /// <param name="AIC"></param>
        /// <returns></returns>
        public static Matrix GetBICAndAIC(Matrix Deal_Y_Model, int T_lag, out Matrix AIC)
        {
            AIC = new Matrix(T_lag, 1);
            Matrix BIC = new Matrix(T_lag, 1);
            for (int i = 0; i < T_lag; i++)
            {
                Matrix A = null;
                for (int m = 0; m <= i; m++)
                {
                    if (A != null)
                        A = Matrix.MergeR(Deal_Y_Model.GetR(m, Deal_Y_Model.Rows - 1 - i + m - 1), A);
                    else
                    {
                        A = Deal_Y_Model.GetR(m, Deal_Y_Model.Rows - 1 - i + m - 1);
                    }
                    Matrix PHI = Matrix.MatrixInv(~A * A) * ~A * Deal_Y_Model.GetR(i + 1, Deal_Y_Model.Rows - 1);
                    Matrix Error = Deal_Y_Model.GetR(i + 1, Deal_Y_Model.Rows - 1) - A * PHI;
                    double E = 0.0;
                    for (int q = 0; q < Error.Rows; q++)
                    {
                        E += Math.Pow(Error[q, 0], 2);
                    }

                    AIC[i, 0] = Math.Log(E / Error.Rows) + 2 * (i + 1) / Deal_Y_Model.Rows;
                    BIC[i, 0] = Math.Log(E / Error.Rows) + (i + 1) * Math.Log(Deal_Y_Model.Rows)
                                                                            / Deal_Y_Model.Rows;
                }
            }
            return BIC;
        }
        /// <summary>
        /// 模型的最小二乘估计
        /// </summary>
        /// <param name="m_Order"></param>
        /// <param name="Deal_Y_Model"></param>
        /// <param name="_A"></param>
        /// <returns></returns>
        public static Matrix LSE(int m_Order, Matrix Deal_Y_Model, out Matrix _A)
        {
            _A = null;
            for (int i = 0; i < m_Order; i++)
            {
                if (_A != null)
                {
                    _A = Matrix.MergeR(Deal_Y_Model.GetR(i, Deal_Y_Model.Rows - 1 - m_Order + 1 + i - 1), _A);
                }
                else
                {
                    _A = Deal_Y_Model.GetR(i, Deal_Y_Model.Rows - 1 - m_Order + 1 + i - 1);
                }
            }
            Matrix m_Perement = Matrix.MatrixInv(~_A * _A) * ~_A * Deal_Y_Model.GetR(m_Order, Deal_Y_Model.Rows - 1);
            return m_Perement;
        }
        /// <summary>
        /// 模型检验
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="m_Order"></param>
        /// <param name="FitY"></param>
        /// <param name="m_Perement"></param>
        /// <param name="_A"></param>
        /// <param name="dFP"></param>
        /// <returns></returns>
        public static double[] ModingTest(Matrix Y, int m_Order, Matrix FitY, Matrix m_Perement, Matrix _A, out double dFP)
        {
            double dCy1 = (~(FitY - CARFuction.mean(FitY)) * (FitY - CARFuction.mean(FitY)))[0, 0];
            double dCy2 = (~(Y - CARFuction.mean(Y)) * (Y - CARFuction.mean(Y)))[0, 0];
            double dQr = (~FitY * FitY)[0, 0];
            double dQy = (~Y * Y)[0, 0];
            double dQe = (~(FitY - Y) * (FitY - Y))[0, 0];
            double dR2 = dQr / dQy;
            double dD2 = dQe / FitY.Rows;//方差
            double dF = dQr / m_Order / (dQe / (FitY.Rows - m_Order - 1));
            double df;
            if (FitY.Rows - m_Order - 1 <= 0)
            {
                dFP = 0;
                return null;
            }
            dFP = 1 - StatisticsTest.F(m_Order, FitY.Rows - m_Order - 1, dF, out df); //F检验的概率值
            Matrix Qphi = Matrix.MatrixInv(~_A * _A);
            double[] T = new double[m_Order];
            double[] m_TP = new double[m_Order];
            for (int i = 0; i < m_Order; i++)
            {
                T[i] = m_Perement[i, 0] / (Math.Sqrt(Qphi[i, i] * dD2));
                m_TP[i] = 1 - StatisticsTest.t(FitY.Rows - m_Order - 1, T[i], out df);
            }
            return m_TP;
        }

        /// <summary>
        /// 多步预测
        /// </summary>
        /// <returns></returns>
        public static void MulPre(int m_Order, int PreDays, int m_DeformationModelDays, Matrix Deal_Y, Matrix m_Perement, Matrix m_PeriodicTerm, int i_PreStep, out Matrix[] AR_PreDataSource, out Matrix[] AR_Pre, out Matrix[] AR_PreEoror)
        {
            List<Matrix[]> Pre = new List<Matrix[]>();
            Matrix Mid_Deal_Pre_Data = Deal_Y.GetR(m_DeformationModelDays - m_Order, m_DeformationModelDays + PreDays - 1);
            Matrix[] B1 = new Matrix[m_Order];
            for (int i = 0; i < m_Order; i++)
            {
                B1[i] = Mid_Deal_Pre_Data.GetR(m_Order - i - 1, Mid_Deal_Pre_Data.Rows - 1 - i - 1);
            }

            AR_Pre = new Matrix[i_PreStep];
            AR_PreDataSource = new Matrix[i_PreStep];
            AR_PreEoror = new Matrix[i_PreStep];
            for (int i = 0; i < i_PreStep; i++)
            {
                Matrix ARPre = null;
                for (int j = 0; j < m_Order; j++)
                {
                    if (j == 0)
                        ARPre = new Matrix(B1[j].Rows, 1);
                    ARPre = ARPre + B1[j] * m_Perement[j];
                }
                for (int p = 0; p < B1.Length - 1; p++)
                {
                    B1[p + 1] = B1[p];
                }
                B1[0] = ARPre;
                for (int k = 0; k < B1.Length; k++)
                {
                    B1[k] = B1[k].GetR(0, B1[k].Rows - 1 - 1);
                }

                AR_Pre[i] = ARPre;
                AR_Pre[i] = AR_Pre[i] + m_PeriodicTerm.GetR(m_DeformationModelDays + i, m_DeformationModelDays + PreDays - 1);
                AR_PreDataSource[i] = m_PeriodicTerm.GetR(m_DeformationModelDays + i, m_DeformationModelDays + PreDays - 1) +
                    Deal_Y.GetR(m_DeformationModelDays + i, m_DeformationModelDays + PreDays - 1);
                AR_PreEoror[i] = AR_Pre[i] - AR_PreDataSource[i];
            }        
            
        }
        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <returns></returns>
        public static string HelpWord()
        {
            string str = null;
            string n = "\r\n";
            str += string.Format("{0,-8}", "Step 1：") + n;
            str += string.Format("{0,-15}", "单击文件下拉菜单的导入数据选项，导入观测数据") + n;
            str += n;

            str += string.Format("{0,-8}", "Step 2：") + n;
            str += "建模期数和预测期数设置（注意：建模期数必须大于40）" + n;
            str += n;

            str += string.Format("{0,-8}", "Step 3：") + n;
            str += string.Format("{0,-6}", "预测步数设置（注意：①预测步数不能大于预测期数。②当不设置预测步数时，默认预测步数为1）") + n;
            str += n;

            str += string.Format("{0,-8}", "Step 4：") + n;
            str += string.Format("{0,-6}", "单击AR建模") + n;
            str += n;

            str += string.Format("{0,-8}", "Step 5：") + n;
            str += "单击绘图下拉菜单的模型拟合图和预测图可以查看建模结果（注意：预测图为一步预测的结果）" + n;
            str += n;

            str += string.Format("{0,-8}", "Step 6：") + n;
            str += "多步预测结果展示，选择步数会在预测精度选项卡显示被选步数的预测结果" + n;
            str += n;

            str += string.Format("{0,-8}", "Step 7：") + n;
            str += "单击文件下拉菜单的成果导出选项，对建模结果报表" + n;
            str += "如果预测步数设置为1时会直接报表，如果步数大于1时，会弹出Export窗体提供选择报表的预测步数，报表会对小于等于被选择的步数预测结果进行报表" + n;
            str += n;

            str += string.Format("{0,-8}", "Step 8：") + n;
            str += "单击文件下拉菜单的退出选项，退出程序" + n;
            str += n;
            return str;
        }
        /// <summary>
        /// 关于
        /// </summary>
        /// <returns></returns>

        public static string Regarding()
        {
            string str = null;
            string n = "\r\n";
            str += "作者：杨志佳" + n;
            str += "单位：中南大学" + n;
            str += "邮箱：zhijiayang@csu.edu.cn" + n;
            str += n;
            return str;
        }
    }

}
