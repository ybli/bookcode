using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geodetic_line
{
    public struct Input
    {
        /// <summary>
        /// 离心率
        /// </summary>
        public double e2;
        /// <summary>
        /// 第一偏心率
        /// </summary>
        public double c;
        public double L1;
        public double B1;
        /// <summary>
        /// 大地方位角
        /// </summary>
        public double A12;
        /// <summary>
        /// 大地线长
        /// </summary>
        public double S;
        //反算
        public double L2;
        public double B2;
    }
    public struct Output
    {
        public double B2;
        public double L2;
        /// <summary>
        /// 大地方位角
        /// </summary>
        public double A21;
        //反算
        /// <summary>
        /// 大地线长
        /// </summary>
        public double S;
        /// <summary>
        /// 大地方位角
        /// </summary>
        public double A12;
    }
    class Caculate
    {
        /// <summary>
        /// 大地问题反算
        /// </summary>
        /// <param name="input">需要输入的数据</param>
        /// <param name="Geodesy">选择椭球</param> 
        /// <returns></returns>
        public Output Bessal_P(Input input, double Geodesy)
        {
            Output output = new Output();
            #region 变量
            double W1;
            double W2;
            double Sin_u1;//u1为归化纬度
            double Sin_u2;
            double Cos_u1;
            double Cos_u2;
            double L;
            double a1;
            double a2;
            double b1;
            double b2;
            double delta0 = 0;
            double delta;
            double p;
            double q;
            double lamda;
            double sigema;
            double Sin_sigema;
            double Cos_sigema;
            double Sin_A0;
            double Cos_A0_2;
            double x;
            double alpha=0;
            double belta=0;
            double A=0;
            double B=0;
            double C=0;
            double y;
            #endregion

            //辅助计算
            W1 = Math.Sqrt(1 - input.e2 * Math.Pow(Math.Sin(input.B1), 2));
            W2 = Math.Sqrt(1 - input.e2 * Math.Pow(Math.Sin(input.B2), 2));
            Sin_u1 = Math.Sin(input.B1) * Math.Sqrt(1 - input.e2) / W1;
            Sin_u2 = Math.Sin(input.B2) * Math.Sqrt(1 - input.e2) / W2;
            Cos_u1 = Math.Cos(input.B1) / W1;
            Cos_u2 = Math.Cos(input.B2) / W2;
            L = input.L2 - input.L1;
            a1 = Sin_u1 * Sin_u2;
            a2 = Cos_u1 * Cos_u2;
            b1 = Cos_u1 * Sin_u2;
            b2 = Sin_u1 * Cos_u2;
            lamda = L + delta0;
            #region  用逐次趋近法计算大地方位角、球面长度及经差
            do
            {
                p = Cos_u2 * Math.Sin(lamda);
                q = b1 - b2 * Math.Cos(lamda);
                output.A12 = Math.Atan(p/ q);
                if (p > 0 && q > 0)
                    output.A12 = Math.Abs(output.A12);
                else if (p > 0 && q < 0)
                    output.A12 = Math.PI - Math.Abs(output.A12);
                else if (p < 0 && q < 0)
                    output.A12 = Math.Abs(output.A12) + Math.PI;
                else
                    output.A12 = 2 * Math.PI - Math.Abs(output.A12);
                Sin_sigema = p * Math.Sin(output.A12) + q * Math.Cos(output.A12);
                Cos_sigema = a1 + a2 * Math.Cos(lamda);
                sigema = Math.Atan(Sin_sigema/ Cos_sigema);
                if (Cos_sigema > 0)
                    sigema = Math.Abs(sigema);
                else
                    sigema = Math.PI - Math.Abs(sigema);
                Sin_A0 = Cos_u1 * Math.Sin(output.A12);
                Cos_A0_2 = (1 - Sin_A0 * Sin_A0);
                x = 2 * a1 - Cos_A0_2 * Cos_sigema;
                if (Geodesy == 1)
                {
                    alpha = (33523299 - (28189 - 70 * Cos_A0_2) * Cos_A0_2) * 1e-10;
                    belta = (28189 - 94 * Cos_A0_2) * 1e-10;
                }
                else if (Geodesy == 2)
                {
                    alpha = (33528130 - (28190 - 70 * Cos_A0_2) * Cos_A0_2) * 1e-10;
                    belta = (28190 - 93.4 * Cos_A0_2) * 1e-10;
                }
                else if (Geodesy == 3)
                {
                    alpha = (33528130 - (28190 - 70 * Cos_A0_2) * Cos_A0_2) * 1e-10;
                    belta = (28190 - 93.4 * Cos_A0_2) * 1e-10;
                }
                delta = (alpha * sigema - belta * x * Sin_sigema) * Sin_A0;
                lamda = L + delta;
                if (Math.Abs(delta - delta0) < 1e-12)
                    break;
                delta0 = delta;
            } while (true);
            #endregion
            //计算系数A、B、C及大地线长度S
            if (Geodesy == 1)
            {
                A = 6356863.020 + (10708.949 - 13.474 * Cos_A0_2) * Cos_A0_2;
                B = 10708.938 - 17.956 * Cos_A0_2;
                C = 4.487;
            }
            else if (Geodesy == 2)
            {
                A = 6356755.288 + (10710.341 - 13.534 * Cos_A0_2) * Cos_A0_2;
                B = 10710.342 - 18.046 * Cos_A0_2;
                C = 4.512;
            }
            else if (Geodesy == 3)
            {
                A = 6356755.288 + (10710.341 - 13.534 * Cos_A0_2) * Cos_A0_2;
                B = 10710.342 - 18.046 * Cos_A0_2;
                C = 4.512;
            }
            y = (Math.Pow(Cos_A0_2, 2) - 2 * x * x) * Cos_sigema;
            output.S = A * sigema + (B * x + C * y) * Sin_sigema;
            output.A21 = Math.Atan(Cos_u1 * Math.Sin(lamda)/( b1 * Math.Cos(lamda) - b2));
            if (output.A21 < 0)
            {
                output.A21 += 2 * Math.PI;
            }
            if (output.A21 > 2 * Math.PI)
            {
                output.A21 -= 2 * Math.PI;
            }
            if (output.A12 < Math.PI && output.A21 < Math.PI)
            {
                output.A21 += Math.PI;
            }
            if (output.A12 > Math.PI && output.A21 > Math.PI)
            {
                output.A21 -= Math.PI;
            }
            return output;
        }
    }
}
