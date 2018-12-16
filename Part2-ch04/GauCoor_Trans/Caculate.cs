using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GauCoor_Trans
{
    public struct ell
    {
        public double a;
        public double e2;
        public double e12;
    }
    class Caculate
    {
        /// <summary>
        /// 度转弧度
        /// </summary>
        /// <param name="Dms">特殊形式的度分秒(xx.xxxxxx)</param>
        /// <returns></returns>
        public double DmsToRad(double Dms)
        {
            double degree;
            double minute;
            double second;
            if (Dms >= 0)
            {
                degree = Math.Floor(Dms);
                minute = Math.Floor((Dms - degree) * 100);
                second = (Dms - degree - minute / 100.0) * 10000;
                return (degree + minute / 60.0 + second / 3600.0) / 180.0 * Math.PI;
            }
            else
            {
                degree = -Math.Floor(-Dms);
                minute = -Math.Floor((degree - Dms) * 100);
                second = -(degree + minute / 100.0 - Dms) * 10000;
                return (degree + minute / 60.0 + second / 3600.0) / 180.0 * Math.PI;
            }
        }
        /// <summary>
        /// 弧度转度
        /// </summary>
        /// <param name="rad">弧度</param>
        /// <returns></returns>
        public double RadToDMS(double rad)
        {
            int d, m;
            double s, dms;
            double du = rad / Math.PI * 180;
            d = (int)du;
            m = (int)((du - d) * 60);
            s = (du - d) * 3600 - m * 60;
            dms = d + m / 100.0 + s / 10000.0;
            dms = Math.Round(dms, 6);
            return dms;
        }
        /// <summary>
        /// 高斯正算
        /// </summary>
        /// <param name="Geodesy">椭球参数</param>
        /// <param name="B">纬度</param>
        /// <param name="L">经度</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="y1"></param>
        /// <param name="is3">是否选择3度带</param>
        /// <param name="is2">椭球选择参数</param>
        /// <param name="is4">经度选择参数</param>
        public static void Gauss_P(ell Geodesy, double B, double L, ref double x, ref double y, ref double y1, int is3, int is2, int is4,double L0,int n)
        {
            try
            {
                double W;
                double N;
                double l;
                double m;
                double t;
                double η;
                double X = 0;
                W = Math.Sqrt(1 - Geodesy.e2 * Math.Sin(B) * Math.Sin(B));
                N = Geodesy.a / W;
                l = L - L0;
                m = Math.Cos(B) * l;
                t = Math.Tan(B);
                η = Math.Sqrt(Geodesy.e12) * Math.Cos(B);
                switch (is2)//判断选择哪个椭球,确定相应的子午线弧长公式
                {
                    case 1: X = 111134.8611 * B * 180 / Math.PI - 16036.4803 * Math.Sin(2 * B) + 16.8281 * Math.Sin(4 * B) - 0.0220 * Math.Sin(6 * B); break;
                    case 2: X = 111133.0047 * B * 180 / Math.PI - 16038.5282 * Math.Sin(2 * B) + 16.8326 * Math.Sin(4 * B) - 0.0220 * Math.Sin(6 * B); break;
                    case 3:
                        X = 111132.95254700 * B * 180 / Math.PI - 16038.508741268 * Math.Sin(2 * B) + 16.832613326622 * Math.Sin(4 * B) - 0.021984374201268 * Math.Sin(6 * B)
                  + 3.1141625291648e-5 * Math.Sin(8 * B); break;
                    default: MessageBox.Show("请选择相应椭球参数！"); break;
                }
                if (is4 == 1)//选择精度为0.001m时的高斯投影正算公式
                {
                    x = X + N * t * (1.0 / 2 * m * m + 1.0 / 24 * (5 - t * t + 9 * η * η + 4 * Math.Pow(η, 4)) * Math.Pow(m, 4)
                        + 1.0 / 720 * (61 - 58 * t * t + Math.Pow(t, 4)) * Math.Pow(m, 6));
                    y = N * (m + 1.0 / 6 * (1 - t * t + η * η) * Math.Pow(m, 3) +
                        1.0 / 120 * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * η * η - 58 * η * η * t * t) * Math.Pow(m, 5));
                }
                else if (is4 == 2)
                {
                    x = X + N * t * (1.0 / 2 * m * m + 1.0 / 24 * (5 - t * t + 9 * η * η + 4 * Math.Pow(η, 4)) * Math.Pow(m, 4));
                    y = N * (m + 1.0 / 6 * (1 - t * t + η * η) * Math.Pow(m, 3) + 1.0 / 120 * (5 - 18 * t * t + Math.Pow(t, 4)) * Math.Pow(m, 5));
                }
                if (L == 0 && is3 == 6)
                    y1 = 60 * 1000000 + 500000 + y;
                if (L <= 1.5 && L >= 0 && is3 == 3)
                    y1 = 120 * 1000000 + 500000 + y;
                else
                    y1 = n * 1000000 + 500000 + y;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }

        }
        /// <summary>
        /// 高斯反算
        /// </summary>
        /// <param name="Geodesy">椭球参数</param>
        /// <param name="B">纬度</param>
        /// <param name="L">经度</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="y1"></param>
        /// <param name="is3">是否选择3度带</param>
        /// <param name="is2">椭球选择参数</param>
        /// <param name="is4">经度选择参数</param>
        public static void Gauss_N(ell Geodesy, ref double B, ref double L, double x, double y, double y1, int is3, int is2, int is4)
        {
            double s = 0;
            double W;
            double N;
            double V;
            double t;
            double η;
            double Bf0;//迭代初始值
            double Bf1 = 0;//迭代值
            double FB = 0;
            double L0=0;//中央经线经度

            try
            {
                switch (is2)//选择迭代首项
                {
                    case 1: s = 111134.8611; break;
                    case 2: s = 111133.0047; break;
                    case 3: s = 111132.95254700; break;
                    default: MessageBox.Show("请选择椭球!"); break;
                }
                if (is3==3)
                    L0 = (Math.Floor(y1 / 1000000.0) * 3) * Math.PI / 180.0;
                else if(is3==6)
                    L0 = (Math.Floor(y1 / 1000000.0) * 6 - 3) * Math.PI / 180.0;
                Bf0 = x / s * Math.PI / 180.0;
                double num = 0;//迭代次数


                do//迭代求解纬度Bf
                {
                    if (num != 0)
                        Bf0 = Bf1;
                    switch (is2)
                    {
                        case 1:
                            FB = -(32005.7799 * Math.Sin(Bf0) + 133.9238 * Math.Pow(Math.Sin(Bf0), 3) + 0.6973 * Math.Pow(Math.Sin(Bf0), 5)
                             + 0.0039 * Math.Pow(Math.Sin(Bf0), 7)) * Math.Cos(Bf0); break;
                        case 2: FB = -16038.5282 * Math.Sin(2 * Bf0) + 16.8326 * Math.Sin(4 * Bf0) - 0.022 * Math.Sin(6 * Bf0); break;
                        case 3:
                            FB = -16038.508741268 * Math.Sin(2 * Bf0) + 16.832613326622 * Math.Sin(4 * Bf0)
                             - 0.021984374201268 * Math.Sin(6 * Bf0) + 3.1141625291648e-5 * Math.Sin(8 * Bf0); break;
                        default: MessageBox.Show("请选择相应椭球参数！"); break;
                    }
                    Bf1 = (x - FB) / s * Math.PI / 180.0;
                    num++;
                } while (Math.Abs(Bf1 - Bf0) >= 1e-8);



                W = Math.Sqrt(1 - Geodesy.e2 * Math.Sin(Bf1) * Math.Sin(Bf1));
                V = Math.Sqrt(1 - Geodesy.e12 * Math.Sin(Bf1) * Math.Sin(Bf1));
                N = Geodesy.a / W;
                t = Math.Tan(Bf1);
                η = Math.Sqrt(Geodesy.e12) * Math.Cos(Bf1);
                if (is4 == 3)
                {
                    B = Bf1 - 1.0 / 2 * V * V * t * (Math.Pow(y / N, 2) - 1.0 / 12 * (5 + 3 * t * t + η * η - 9 * η * η * t * t) * Math.Pow(y / N, 4)
                + 1.0 / 360 * (61 + 90 * t * t + 45 * Math.Pow(t, 4)) * Math.Pow(y / N, 6));
                    L = (y / N - 1.0 / 6 * (1 + 2 * t * t + η * η) * Math.Pow(y / N, 3)
                        + 1.0 / 120 * (5 + 28 * t * t + 24 * Math.Pow(t, 4) + 6 * η * η + 8 * η * η * t * t) * Math.Pow(y / N, 5)) / Math.Cos(Bf1);
                }
                else
                {
                    B = Bf1 - 1.0 / 2 * V * V * t * (Math.Pow(y / N, 2) - 1.0 / 12 * (5 + 3 * t * t + η * η - 9 * η * η * t * t) * Math.Pow(y / N, 4));
                    L = (y / N - 1.0 / 6 * (1 + 2 * t * t + η * η) * Math.Pow(y / N, 3)
                        + 1.0 / 120 * (5 + 28 * t * t + 24 * Math.Pow(t, 4)) * Math.Pow(y / N, 5)) / Math.Cos(Bf1);
                }
                L += L0;
            }
            catch
            {

            }
        }
        /// <summary>
        /// 高斯正算
        /// </summary>
        /// <param name="Geodesy">椭球参数</param>
        /// <param name="B">唯、维度</param>
        /// <param name="L">经度</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="L0">中央子午线经度</param>
        public static void To_Gauss(ell Geodesy, double B, double L, ref double x, ref double y,double L0)
        {
            double AA, BB, CC, DD, EE, X, e, e1;
            double dl, n, t, N;
            double a = Geodesy.a;
            e = Math.Sqrt(Geodesy.e2);
            e1 = Math.Sqrt(Geodesy.e12);
            AA = 1 + 3.0/ 4.0* Math.Pow(e, 2.0) + 45.0/ 64.0* Math.Pow(e, 4.0) + 175.0/ 256.0* Math.Pow(e, 6.0);
            AA += 11025.0/ 16384.0* Math.Pow(e, 8.0);
            BB = 3.0/ 4.0* Math.Pow(e, 2.0) + 15.0/ 16.0* Math.Pow(e, 4.0) + 525.0/ 512.0* Math.Pow(e, 6.0);
            BB += 2205.0/ 2048.0* Math.Pow(e, 8.0);
            CC = 15.0/ 64.0* Math.Pow(e, 4.0) + 105.0/ 256.0* Math.Pow(e, 6.0) + 2205.0/ 4096.0* Math.Pow(e, 8.0);
            DD = 35.0/ 512.0* Math.Pow(e, 6.0) + 315.0/ 2048.0* Math.Pow(e, 8.0);
            EE = 315.0/ 16384.0* Math.Pow(e, 8.0);
            X = a * (1 - Math.Pow(e, 2.0)) * (AA * B - BB / 2 * Math.Sin(2 * B) + CC / 4 * Math.Sin(4 * B)
                - DD / 6 * Math.Sin(6 * B) + EE / 8 * Math.Sin(8 * B));
            dl = L - L0;
            n = e1 * Math.Cos(B);
            t = Math.Tan(B);
            N = a / Math.Sqrt(1 - e * e * Math.Pow(Math.Sin(B), 2.0));
            x = X;
            x+= N / 2 * Math.Sin(B) * Math.Cos(B) * Math.Pow(dl, 2.0);
            x+= N / 24 * Math.Sin(B) * Math.Pow(Math.Cos(B), 3.0) * (5 - t * t + 9 * n * n + 4 * Math.Pow(n, 4.0)) * Math.Pow(dl, 4.0);
            x+= N / 720 * Math.Sin(B) * Math.Pow(Math.Cos(B), 5.0) * (61 - 58 * Math.Pow(t, 2.0) + Math.Pow(t, 4.0)) * Math.Pow(dl, 6.0);
            y= N * Math.Cos(B) * dl;
            y+= N / 6 * Math.Pow(Math.Cos(B), 3.0) * (1 - t * t + n * n) * Math.Pow(dl, 3.0);
            y+= N / 120 * Math.Pow(Math.Cos(B), 5.0) * (5 - 18 * t * t + Math.Pow(t, 4.0) + 14 * n * n - 58 * n * n * t * t) * Math.Pow(dl, 5.0);
        }
        /// <summary>
        /// 高斯反算
        /// </summary>
        /// <param name="Geodesy">椭球参数</param>
        /// <param name="B"></param>
        /// <param name="L"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="L0"></param>
        public static void Gauss_To(ell Geodesy, ref double B, ref double L, double x, double y, double L0)
        {
            double AA, BB, CC, DD, EE, e, e1;
            double Bf, Bf0, b, dl, nf, tf, Nf, Mf, Wf;
            double a = Geodesy.a;
            e = Math.Sqrt(Geodesy.e2);
            e1 = Math.Sqrt(Geodesy.e12);
            AA = 1 + 3.0/ 4.0* Math.Pow(e, 2.0) + 45.0/ 64.0* Math.Pow(e, 4.0) + 175.0/ 256.0* Math.Pow(e, 6.0);
            AA += 11025.0/ 16384.0* Math.Pow(e, 8.0);
            BB = 3.0/ 4.0* Math.Pow(e, 2.0) + 15.0/ 16.0* Math.Pow(e, 4.0) + 525.0/ 512.0* Math.Pow(e, 6.0);
            BB += 2205.0/ 2048.0* Math.Pow(e, 8.0);
            CC = 15.0/ 64.0* Math.Pow(e, 4.0) + 105.0/ 256.0* Math.Pow(e, 6.0) + 2205.0/ 4096.0* Math.Pow(e, 8.0);
            DD = 35.0/ 512.0* Math.Pow(e, 6.0) + 315.0/ 2048.0* Math.Pow(e, 8.0);
            EE = 315.0/ 16384.0* Math.Pow(e, 8.0);
            Bf0 = x / AA / a / (1 - e * e);//初值
            do
            {
                Bf = x / a / (1 - e * e) / AA + (BB / 2 * Math.Sin(2 * Bf0) - CC / 4 * Math.Sin(4 * Bf0)
                    + DD / 6 * Math.Sin(6 * Bf0) - EE / 8 * Math.Sin(8 * Bf0)) / AA;
                if (Math.Abs(Bf - Bf0) < 0.0000000000001)break;
                Bf0 = Bf;
            } while (true);
            tf = Math.Tan(Bf);
            Wf = Math.Sqrt(1 - e * e * Math.Sin(Bf) * Math.Sin(Bf));
            Mf = a * (1 - e * e) / Math.Pow(Wf, 3.0);
            Nf = a / Wf;
            nf = e1 * Math.Cos(Bf);
            B = Bf - tf / 2 / Mf / Nf * y * y;
            B += tf / 24 / Mf * (5 + 3 * Math.Pow(tf, 2.0) + nf * nf - 9 * nf * nf * tf * tf) * Math.Pow(y / Nf, 3.0) * y;
            B += -tf / 720 / Mf * (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4.0)) * Math.Pow(y / Nf, 5.0) * y;
            dl = y / Nf / Math.Cos(Bf);
            dl += -Math.Pow(y / Nf, 3.0) / 6 / Math.Cos(Bf) * (1 + 2 * tf * tf + nf * nf);
            dl += Math.Pow(y / Nf, 5.0) / 120 / Math.Cos(Bf) * (5 + 28 * tf * tf + 24 * Math.Pow(tf, 4.0) + 6 * nf * nf + 8 * nf * nf * tf * tf);
            L = L0 + dl;
        }
    }
}
