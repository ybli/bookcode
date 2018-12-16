using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorLib
{
    /// <summary>
    ///高斯正反算
    /// </summary>
    public class Gauss
    {
        private Ellipsoid ell;
        private double L0;               //中央子午线
        private double Y0 = 500000.0;    //Y方向平移量（以m为单位）
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ellipsoid">椭球</param>
        public Gauss(Ellipsoid ellipsoid, double midLon)
        {
            this.ell = ellipsoid;
            L0 = midLon;
        }
        /// <summary>
        /// 高斯正算
        /// </summary>
        /// <param name="Pbl">点</param>
        public void BL2xy(double B, double L, out double x, out double y)
        {

            double dl = L - L0;

            double[] c = new double[6];
            Coefficient(ref c);
            double X = 0;
            X = c[0] * B + c[1] * Math.Sin(2 * B) + c[2] * Math.Sin(4 * B)
                + c[3] * Math.Sin(6 * B) + c[4] * Math.Sin(8 * B) + c[5] * Math.Sin(10 * B);

            double[] a = new double[7];
            CoeffA(X, B, a);
            x = a[0] + a[2] * dl * dl + a[4] * Math.Pow(dl, 4) + a[6] * Math.Pow(dl, 6);
            y = a[1] * dl + a[3] * Math.Pow(dl, 3) + a[5] * Math.Pow(dl, 5);
            y = y + Y0;
        }

        /// <summary>
        /// 计算高斯投影反算
        /// </summary>
        /// <param name="Pxy">点</param>
        public void xy2BL(double x, double y, out double B, out double L)
        {

            y = y - Y0;

            double[] c = new double[6];
            Coefficient(ref c);

            double Bf1 = x / c[0];
            EndPointLat(c, x, ref Bf1);

            double[] b = new double[7];
            CoeffB(Bf1, b);

            B = b[0] + b[2] * y * y + b[4] * Math.Pow(y, 4) + b[6] * Math.Pow(y, 6);
            double dl = b[1] * y + b[3] * Math.Pow(y, 3) + b[5] * Math.Pow(y, 5);
            L = L0 + dl;

        }

        ///// <summary>
        ///// 计算从赤道到投影点的子午线弧长
        ///// </summary>
        ///// <param name="lat"> 纬度 （以度为单位） </param>
        ///// <returns></returns>
        //private double MeridianArclength(double lat)
        //{
        //    double B = lat;
        //    double e2 = ell.eccSq;
        //    double e4 = e2 * e2;
        //    double e6 = e4 * e2;
        //    double e8 = e6 * e2;
        //    double e10 = e8 * e2;
        //    double M = ell.a * (1 - e2);
        //    double A0 = M * (1.0 + 0.75 * e2 + 45.0 / 64.0 * e4 + 175.0 / 256.0 * e6
        //        + 11025.0 / 16384.0 * e8 + 43659.0 / 65536.0 * e10);
        //    double A1 = -M / 2 * (0.75 * e2 + 15.0 / 16.0 * e4 + 525.0 / 512.0 * e6
        //        + 2205.0 / 2048.0 * e8 + 72765.0 / 65536.0 * e10);
        //    double A2 = M / 4 * (15.0 / 64.0 * e4 + 105.0 / 256.0 * e6
        //        + 2205.0 / 4096.0 * e8 + 10395.0 / 16384.0 * e10);
        //    double A3 = -M / 6 * (35.0 / 512.0 * e6 + 315.0 / 2048.0 * e8 + 31185.0 / 131072.0 * e10);
        //    double A4 = M / 8 * (315.0 / 16384.0 * e8 + 3465.0 / 65536.0 * e10);
        //    double A5 = -M / 10 * (693.0 / 131072.0 * e10);

        //    double X = A0 * B + A1 * Math.Sin(2 * B) + A2 * Math.Sin(4 * B) + A3 * Math.Sin(6 * B)
        //        + A4 * Math.Sin(8 * B) + A5 * Math.Sin(10 * B);
        //    return X;
        //}

        ////计算底点维度
        //private double EndPointLat(double x)
        //{
        //    double e2 = ell.eccSq;
        //    double e4 = e2 * e2;
        //    double e6 = e4 * e2;
        //    double e8 = e6 * e2;
        //    double e10 = e8 * e2;

        //    double A0 = ell.a * (1 - e2) * (1.0 + 0.75 * e2 + 45.0 / 64.0 * e4
        //        + 350 / 512.0 * e6 + 11025.0 / 16384.0 * e8);
        //    double K0 = 0.5 * (0.75 * e2 + 45.0 / 64.0 * e4 + 350 / 512.0 * e6 + 11025.0 / 16384.0 * e8);
        //    double K2 = -1 / 3 * (63.0 / 64.0 * e4 + 1108 / 512.0 * e6 + 58239.0 / 16384.0 * e8);
        //    double K4 = 1 / 3 * (604 / 512.0 * e6 + 68484.0 / 16384.0 * e8);
        //    double K6 = -1 / 3 * (26328.0 / 16384.0 * e8);

        //    double B0 = x / A0;
        //    double delta = 100;
        //    do
        //    {
        //        double slat2 = Math.Sin(B0) * Math.Sin(B0);
        //        double B = B0 + Math.Sin(2 * B0) * (K0 + slat2 * (K2 + slat2 * (K4 + K6 * slat2)));

        //        delta = Math.Abs(B - B0);
        //        B0 = B;
        //    } while (delta < 1E-20);

        //    return B0;
        //}

        /// <summary>
        /// 计算底点纬度
        /// </summary>
        /// <param name="Coeff">系数数组</param>
        /// <param name="Pxy">点</param>
        /// <param name="Bf1">底点纬度</param>
        private void EndPointLat(double[] Coeff, double x, ref double Bf1)
        {
            double X = x, dX = 0;
            double Bf0 = 0;
            do
            {
                Bf0 = Bf1;
                dX = Coeff[1] * Math.Sin(2 * Bf0) + Coeff[2] * Math.Sin(4 * Bf0)
                + Coeff[3] * Math.Sin(6 * Bf0) + Coeff[4] * Math.Sin(8 * Bf0) + Coeff[5] * Math.Sin(10 * Bf0);
                Bf1 = (X - dX) / Coeff[0];
            } while (Math.Abs(Bf1 - Bf0) > 0.0000000000001);  // ((Math.Abs(Bf1 - Bf0) * 180 / Math.PI * 3600) > 1.0 / 300000);
        }
        /////<summary>
        ///// BL --> xy
        ///// 转换B,L 到x,y(高斯平面坐标系)
        ///// </summary>
        ///// <param name="B"> 纬度（以弧度单位）</param>
        ///// <param name="L"> 经度（以弧度为单位），</param>      
        ///// <param name="x">x方向坐标（以m为单位）</param>
        ///// <param name="y">y方向坐标（以m为单位）</param>
        //public void GeodeticToGrid(double B, double L, out double x, out double y)
        //{
        //    double N = ell.N(B);  //A / Math.Sqrt(1.0 - eccSq * slat * slat);
        //    double X = MeridianArclength(B);

        //    L = (L - L0);
        //    double slat = Math.Sin(B);

        //    double m = L * Math.Cos(B);
        //    double m2 = m * m;
        //    double m3 = m2 * m;
        //    double m4 = m3 * m;
        //    double m5 = m4 * m;
        //    double m6 = m5 * m;

        //    double t = Math.Tan(B);
        //    double t2 = t * t;
        //    double t4 = t2 * t2;

        //    double eta = Math.Sqrt(ell.eccSq / (1 - ell.eccSq)) * Math.Cos(B);
        //    double eta2 = eta * eta;
        //    double eta4 = eta2 * eta2;

        //    x = X + N * t * (0.5 * m2 + 1 / 24 * (5 - t2 + 9 * eta2 + 4 * eta4) * m4
        //        + 1 / 720 * (61 - 58 * t2 + t4) * m6);
        //    y = N * (m + 1 / 6 * (1 - t2 + eta2) * m3 +
        //        1 / 120 * (5 - 18 * t2 + t4 + 14 * eta2 - 58 * eta2 * t2) * m5);

        //    y = y + Y0;
        //}

        ///// <summary>
        ///// xy -->BL
        ///// 高斯平面坐标转换为大地坐标
        ///// </summary>
        ///// <param name="x">x坐标（以m为单位）</param>
        ///// <param name="y">y坐标（以m为单位）</param>
        ///// <param name="lat">纬度（以°为单位）</param>
        ///// <param name="lon">经度（以°为单位）</param>
        //public void GridToGeodetic(double x, double y, out double lat, out double lon)
        //{
        //    //底点维度    EndPointLat(x,eccSq,B);
        //    double B_f = EndPointLat(x);
        //    double slat = Math.Sin(B_f);
        //    double clat = Math.Cos(B_f);

        //    double N = ell.a / Math.Sqrt(1.0 - ell.eccSq * slat * slat);
        //    double t = Math.Tan(B_f);
        //    double t2 = t * t;
        //    double t4 = t2 * t2;

        //    double etaSq = ell.eccSq / (1 - ell.eccSq) * clat * clat;

        //    double Y1 = (y - Y0) / N;
        //    double Y2 = Y1 * Y1;
        //    double Y3 = Y2 * Y1;
        //    double Y4 = Y3 * Y1;
        //    double Y5 = Y4 * Y1;
        //    double Y6 = Y5 * Y1;
        //    double V = 1 + etaSq;

        //    lat = B_f - 0.5 * t * V * Y2 + 1 / 24 * (5 + 3 * t2 + etaSq -
        //        9 * etaSq * t2) * V * t * Y4 - 1 / 720 * (61 + 90 * t2 + 45 * t4) * V * t * Y6;
        //    lon = 1 / clat * (Y1 - 1 / 6 * (1 + 2 * t2 + etaSq) * Y3 +
        //        1 / 120 * (5 + 28 * t2 + 24 * t4 + 6 * etaSq + 8 * etaSq * t2) * Y5);

        //    lon += L0;

        //}



        /// <summary>
        /// 计算系数
        /// </summary>
        /// <param name="X">弧度长</param>
        /// <param name="B">维度</param>
        /// <param name="aCoef">系数</param>
        private void CoeffA(double X, double B, double[] aCoef)
        {
            double a0, a1, a2, a3, a4, a5, a6;
            double N, t, eta;
            N = ell.N(B);
            t = ell.Tan(B);
            eta = ell.Eta(B);
            a0 = X;
            a1 = N * Math.Cos(B);
            a2 = N * t * Math.Pow(Math.Cos(B), 2) / 2.0;
            a3 = N * (1 - t * t + eta * eta) * Math.Pow(Math.Cos(B), 3) / 6.0;
            a4 = N * t * (5 - t * t + 9 * eta * eta + 4 * Math.Pow(eta, 4)) * Math.Pow(Math.Cos(B), 4) / 24.0;
            a5 = N * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * eta * eta - 58 * Math.Pow(eta, 2) * Math.Pow(t, 2)) * Math.Pow(Math.Cos(B), 5) / 120.0;
            a6 = N * t * (61 - 58 * t * t + Math.Pow(t, 4) + 270 * eta * eta - 330 * Math.Pow(eta, 2) * Math.Pow(t, 2)) * Math.Pow(Math.Cos(B), 6) / 720.0;
            aCoef[0] = a0;
            aCoef[1] = a1;
            aCoef[2] = a2;
            aCoef[3] = a3;
            aCoef[4] = a4;
            aCoef[5] = a5;
            aCoef[6] = a6;
        }

        /// <summary>
        /// 计算系数b数组
        /// </summary>
        /// <param name="Bf">低点纬度</param>
        /// <param name="bCoeff">数组</param>
        private void CoeffB(double Bf, double[] bCoeff)
        {
            double Nf = ell.N(Bf);
            //double Wf = ell.W(Bf);
            double Mf = ell.M(Bf);
            double tf = ell.Tan(Bf);
            double etaf = ell.Eta(Bf);
            double b0, b1, b2, b3, b4, b5, b6;
            b0 = Bf;
            b1 = 1.0 / (Nf * Math.Cos(Bf));
            b2 = -tf / (2 * Mf * Nf);
            b3 = -(1 + 2 * tf * tf + etaf * etaf) * b1 / 6.0 / Nf / Nf;
            b4 = -(5 + 3 * tf * tf + etaf * etaf - 9 * tf * tf * etaf * etaf) * b2 / 12.0 / Nf / Nf;
            b5 = -(5 + 28 * tf * tf + 24 * Math.Pow(tf, 4) + 6 * etaf * etaf + 8 * tf * tf * etaf * etaf) * b1 / 120.0 / Math.Pow(Nf, 4);
            b6 = (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * b2 / 360.0 / Math.Pow(Nf, 4);
            bCoeff[0] = b0;
            bCoeff[1] = b1;
            bCoeff[2] = b2;
            bCoeff[3] = b3;
            bCoeff[4] = b4;
            bCoeff[5] = b5;
            bCoeff[6] = b6;
        }

        ///// <summary>
        ///// 根据经度计算三度带带号
        ///// </summary>
        ///// <param name="L">L</param>
        ///// <returns>带号</returns>
        //private int GetZone(double L)
        //{
        //    int zone = 0;
        //    L = L / Math.PI * 180;
        //    zone = (int)Math.Floor((L - 1.5) / 3 + 1);
        //    return zone;
        //}

        ///// <summary>
        ///// 计算中央子午线经度
        ///// </summary>
        ///// <param name="zone">带号</param>
        ///// <returns></returns>
        //private double GetL0(int zone)
        //{
        //    double l0 = zone * 3;
        //    l0 = l0 / 180.0 * Math.PI;
        //    return l0;
        //}


       
        /// <summary>
        /// 计算相关参数
        /// </summary>
        /// <param name="ell">椭球</param>
        /// <param name="coef">参数数组</param>
        private void Coefficient(ref double[] coef)//计算相关的系数
        {
            // double e1 = Ell.e1;
            double M0 = ell.M0;
            double A, B, C, D, E, F;


            double e2 = ell.eccSq;
            double e4 = e2 * e2;
            double e6 = e4 * e2;
            double e8 = e4 * e4;
            double e10 = e6 * e4;

            A = 1 + 3.0 / 4.0 * e2 + 45.0 / 64.0 * e4 + 175.0 / 256.0 * e6 +
                11025.0 / 16384.0 * e8 + 43659.0 / 65536.0 * e10;
            B = 3.0 / 4.0 * e2 + 15.0 / 16.0 * e4 + 525.0 / 512.0 * e6 +
                2205.0 / 2048.0 * e8 + 72765.0 / 65536.0 * e10;
            C = 15.0 / 64.0 * e4 + 105.0 / 256.0 * e6 + 2205.0 / 4096.0 * e8 +
                10395.0 / 16384.0 * e10;
            D = 35.0 / 512.0 * e6 + 315.0 / 2048.0 * e8 + 31185.0 / 131072.0 * e10;
            E = 315.0 / 16384.0 * e8 + 3465.0 / 65536.0 * e10;
            F = 693.0 / 131072.0 * e10;


            coef[0] = A * M0;
            coef[1] = -B * M0 / 2.0;
            coef[2] = C * M0 / 4.0;
            coef[3] = -D * M0 / 6.0;
            coef[4] = E * M0 / 8.0;
            coef[5] = -F * M0 / 10.0;
        }
    }
}
