using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodesyCal
{
    /// <summary>
    /// Bessel大地主题正算类
    /// </summary>
    public class BesselDirect
    {
        private Ellipsoid Ell;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ell">椭球</param>
        public BesselDirect(Ellipsoid Ell)
        {
            this.Ell = Ell;
        }
        /// <summary>
        /// 计算归化纬度
        /// </summary>
        /// <param name="B1">大地纬度</param>
        /// <param name="sinu1">归化纬度sin</param>
        /// <param name="cosu1">归化纬度cos</param>
        private void CalReducedLat(double B1, ref double sinu1, ref double cosu1)
        {
            double e1 = Ell.e1;
            double W1 = GeoPro.GetW(e1, B1);
            sinu1 = Math.Sqrt(1 - e1 * e1) / W1 * Math.Sin(B1);
            cosu1 = Math.Cos(B1) / W1;
        }
        /// <summary>
        /// 计算A,B,C等参数
        /// </summary>
        /// <param name="sinA0">sinA0</param>
        /// <param name="ABC">ABC数组</param>
        /// <param name="alpha">alpha</param>
        /// <param name="beta">beta</param>
        private void CalABC_AlphaBeta(double sinA0, double[] ABC, ref double alpha, ref double beta, ref double gama)
        {
            double cos2_A0 = 1 - sinA0 * sinA0;
            double k_2 = 0;
            double e2 = Ell.e2, b = Ell.b;
            double e1 = Ell.e1;
            k_2 = GeoPro.Getk_2(e2, cos2_A0);
            GeoPro.GetABC(b, k_2, ABC);
            alpha = GeoPro.GetAlpha(e1, cos2_A0);
            beta = GeoPro.GetBeta(e1, cos2_A0);
            gama = GeoPro.GetGama(e1, cos2_A0);
        }
        /// <summary>
        /// 计算球面长度
        /// </summary>
        /// <param name="delta1">delta1</param>
        /// <param name="sin_2delta1">sin_2delta1</param>
        /// <param name="cos_2delta1">cos_2delta1</param>
        /// <param name="ABC">ABC数组</param>
        /// <param name="S">大地线长</param>
        /// <param name="sin_2delta_1_0">sin_2delta_1_0</param>
        /// <param name="delta">经度差</param>
        private void CalGeodesicLength(double delta1, double[] ABC, double S,
             ref double delta)
        {
            double deltat = 0;
            do
            {
                deltat = delta;
                delta = ABC[0] * S + ABC[1] * Math.Sin(deltat) * Math.Cos(2 * delta1 + deltat);
                delta = delta + ABC[2] * Math.Sin(2 * deltat) * Math.Cos(4 * delta1 + 2 * deltat);

            } while (Math.Abs(deltat - delta) > 0.000000001);
        }

        /// <summary>
        /// 单组数据正算
        /// </summary>
        /// <param name="geodesic">单组大地线数据</param>
        public void DirectSolution(GeodesicInfo geodesic)
        {
            //数据格式转换
            double B1 = GeoPro.DMS2RAD(geodesic.P1.B);
            double L1 = GeoPro.DMS2RAD(geodesic.P1.L);
            double A12 = GeoPro.DMS2RAD(geodesic.A12);
            double S = geodesic.S;
            //椭球参数
            double e1, e2, b, c;
            e1 = Ell.e1; e2 = Ell.e2;
            b = Ell.b; c = Ell.c;
            //计算归化纬度
            double sinu1 = 0, cosu1 = 0;
            CalReducedLat(B1, ref sinu1, ref cosu1);

            //计算辅助函数，解球面三角
            double sinA0 = cosu1 * Math.Sin(A12);
            double cot_delta1 = cosu1 * Math.Cos(A12) / sinu1;
            double delta1 = Math.Atan(1.0 / cot_delta1);

            //计算ABC及α和β
            double[] ABC = new double[3];
            double alpha = 0;
            double beta = 0;
            double gama = 0;
            CalABC_AlphaBeta(sinA0, ABC, ref alpha, ref beta, ref gama);

            //计算球面长度           
            double delta = ABC[0] * S;
            CalGeodesicLength(delta1, ABC, S, ref delta);

            //计算经差改正数
            double lamda_L = sinA0 * (alpha * delta + beta * Math.Sin(delta) * Math.Cos(2 * delta1 + delta)
                + gama * Math.Sin(2 * delta) * Math.Cos(4 * delta1 + 2 * delta));

            //计算终点大地坐标及大地方位角
            double sinu2 = sinu1 * Math.Cos(delta) + cosu1 * Math.Cos(A12) * Math.Sin(delta);
            double B2 = Math.Atan(1 / Math.Sqrt(1 - e1 * e1) * sinu2 / Math.Sqrt(1 - sinu2 * sinu2));

            double lamba = Math.Atan(Math.Sin(delta) * Math.Sin(A12) / (cosu1 * Math.Cos(delta) - sinu1 * Math.Sin(delta)
                * Math.Cos(A12)));
            lamba = GeoPro.DirJudgelamba(Math.Sin(A12), lamba);

            double L2 = L1 + lamba - lamda_L;

            double A21 = Math.Atan(cosu1 * Math.Sin(A12) / (cosu1 * Math.Cos(delta)
                * Math.Cos(A12) - sinu1 * Math.Sin(delta)));
            A21 = GeoPro.DirJudgeA2(Math.Sin(A12), A21);

            if (A21 > 2 * Math.PI ) A21 -= 2 * Math.PI;
            if (A21 <0) A21 += 2 * Math.PI;

            //角度转换
            if (A12 >= Math.PI && A21>=Math.PI) A21 = A21 - Math.PI;
            if (A12 < Math.PI && A21<Math.PI) A21 = A21 + Math.PI;
            //
            //  geodesic.P2 = new Pointinfo();
           
          

            geodesic.P2.B = GeoPro.RAD2DMS(B2);
            geodesic.P2.L = GeoPro.RAD2DMS(L2);
            geodesic.A21 = GeoPro.RAD2DMS(A21);
        }

        /// <summary>
        /// 多组大地线数据正算
        /// </summary>
        /// <param name="geodesics">大地线列表</param>
        public void DirecPro(List<GeodesicInfo> geodesics)
        {
            for (int i = 0; i < geodesics.Count; ++i)
            {
                DirectSolution(geodesics[i]);
                //Dirtest(geodesics[i]);
            }
        }


        private void Dirtest(GeodesicInfo geodesic)
        {
            //数据格式转换
            double B1 = GeoPro.DMS2RAD(geodesic.P1.B);
            double L1 = GeoPro.DMS2RAD(geodesic.P1.L);
            double A12 = GeoPro.DMS2RAD(geodesic.A12);
            double S = geodesic.S;
            //椭球参数  
            double eps, e2, b, c, a;
            eps = Ell.e2 * Ell.e2; e2 = Ell.e1 * Ell.e1;
            b = Ell.b; c = Ell.c; a = Ell.a;
            double u1 = Math.Atan(Math.Sqrt(1 - e2) * Math.Tan(B1));
            double sinA0 = Math.Cos(u1) * Math.Sin(A12);
            double cosA0 = Math.Sqrt(1 - sinA0 * sinA0);
            double sigma1 = Math.Atan(Math.Tan(u1) / Math.Cos(A12));
            double xk2 = eps * cosA0 * cosA0;
            double xk4 = xk2 * xk2;
            double xk6 = xk4 * xk2;
            double alpha = (1 - xk2 / 4 + 7 * xk4 / 64 - 15 * xk6 / 256) / b;
            double beta = xk2 / 4 - xk4 / 8 + 37 * xk6 / 512;
            double gamma = xk4 / 128 - xk6 / 128;

            double sigma = alpha * S;
            double Dsigma = 0;
            do
            {
                double sigma0 = sigma;
                sigma = alpha * S + beta * Math.Sin(sigma0) * Math.Cos(2 * sigma1 + sigma0);
                sigma = sigma + gamma * Math.Sin(2 * sigma0) * Math.Cos(4 * sigma1 + 2 * sigma0);
                Dsigma = Math.Abs(sigma - sigma0) * 206265;
            } while (Dsigma > 0.0001);
            double sinA2 = Math.Cos(u1) * Math.Sin(A12);
            double cosA2 = Math.Cos(u1) * Math.Cos(sigma) * Math.Cos(A12) - Math.Sin(u1) * Math.Sin(sigma);
            double tanA2 = sinA2 / cosA2; double A2 = Math.Abs(Math.Atan(sinA2 / cosA2));
            double sinA1 = Math.Sin(A12);
            if (sinA1 < 0 && tanA2 > 0) A2 = A2;
            if (sinA1 < 0 && tanA2 < 0) A2 = Math.PI - A2;
            if (sinA1 > 0 && tanA2 > 0) A2 = Math.PI + A2;
            if (sinA1 > 0 && tanA2 < 0) A2 = 2 * Math.PI - A2;

            double sinU2 = Math.Sin(u1) * Math.Cos(sigma) + Math.Cos(u1) * Math.Cos(A12) * Math.Sin(sigma);
            double B2 = Math.Atan(sinU2 / Math.Sqrt(1 - e2) / Math.Sqrt(1 - sinU2 * sinU2));

            double sinl = Math.Sin(A12) * Math.Sin(sigma);
            double cosl = Math.Cos(u1) * Math.Cos(sigma) - Math.Sin(u1) * Math.Sin(sigma) * Math.Cos(A12);
            double tanlambda = sinl / cosl; double lambda = Math.Abs(Math.Atan(sinl / cosl));
            if (tanlambda > 0 && sinA1 > 0) lambda = lambda;
            if (tanlambda < 0 && sinA1 > 0) lambda = Math.PI - lambda;
            if (tanlambda < 0 && sinA1 < 0) lambda = -lambda;
            if (tanlambda > 0 && sinA1 < 0) lambda = lambda - Math.PI;

            double e4 = e2 * e2;
            double e6 = e4 * e2;
            xk2 = e2 * cosA0 * cosA0;
            xk4 = xk2 * xk2;
            xk6 = xk4 * xk2;
            double alpha1 = (e2 / 2 + e4 / 8 + e6 / 16) - e2 * (1 + e2) * xk2 / 16 + 3 * xk4 * e2 / 128;
            double beta1 = e2 * (1 + e2) * xk2 / 16 - e2 * xk4 / 32;
            double gamma1 = e2 * xk4 / 256;
            double l0 = alpha1 * sigma + beta1 * Math.Sin(sigma) * Math.Cos(2 * sigma1 + sigma);
            l0 = l0 + gamma1 * Math.Sin(2 * sigma) * Math.Cos(4 * sigma1 + 2 * sigma);
            double ll = lambda - sinA0 * l0;
            double L2 = L1 + ll;



            geodesic.P2 = new Pointinfo();
            geodesic.P2.B = GeoPro.RAD2DMS(B2);
            geodesic.P2.L = GeoPro.RAD2DMS(L2);
            geodesic.A21 = GeoPro.RAD2DMS(A2);
        }
    }
}
