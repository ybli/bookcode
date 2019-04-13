using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodesyCal  
{
    /// <summary>
    /// Bessel大地主题反算类
    /// </summary>
    public class BesselInverse
    {
        private Ellipsoid Ell;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ell">椭球</param>
        public BesselInverse(Ellipsoid Ell)
        {
            this.Ell = Ell;
        }
        /// <summary>
        /// 计算a-b参数
        /// </summary>
        /// <param name="sinu1">sinu1</param>
        /// <param name="sinu2">sinu2</param>
        /// <param name="cosu1">cosu1</param>
        /// <param name="cosu2">cosu2</param>
        /// <returns>ab参数数组</returns>
        private double [] CalPara(double u1,double u2)
        {
            double sinu1 = Math.Sin(u1);
            double sinu2 = Math.Sin(u2);
            double cosu1 = Math.Cos(u1);
            double cosu2 = Math.Cos(u2);
            double []ab=new double[4];
            ab[0] = sinu1 * sinu2; 
            ab[1] = cosu1 * cosu2;  
            ab[2] = cosu1 * sinu2; 
            ab[3] = sinu1 * cosu2;
            return ab;
        }

        /// <summary>
        /// 趋近法算角度
        /// </summary>
        /// <param name="dL">初始经度差</param>
        /// <param name="cosu2">cosu2</param>
        /// <param name="cosu1">cosu1</param>
        /// <param name="ab">ab参数数组</param>
        /// <param name="lamda">lamda经度差估计值</param>
        /// <param name="A1">A1坐标方位角</param>
        /// <param name="del">del</param>
        /// <param name="cos2_A0">cos2_A0</param>
        /// <param name="x">x</param>
        private void CalA1_Lamda(double dL, double u2, double u1,double []ab,ref double lamda, ref double A1, 
            ref double  del,ref double cos2_A0)
        {
            double deltat = 0, delta = 0;
            double cos_del = 0, sin_del = 0;
            double e1 = Ell.e1;
            double alpha = 0, beta = 0, gama=0;
            double sinA0;
            double p =0;
            double q = 0;
            lamda = dL;
            do
            {
                deltat = delta;
                p = Math.Cos(u2) * Math.Sin(lamda);
                q = ab[2] - ab[3] * Math.Cos(lamda);
                A1 = Math.Abs(Math.Atan(p / q));
                A1 = GeoPro.InvJudgeA1A2(p, q, A1);

                sin_del = p * Math.Sin(A1) + q * Math.Cos(A1);
                cos_del = ab[0] + ab[1] * Math.Cos(lamda);
                del = Math.Atan(sin_del / cos_del);
                del = GeoPro.InvJudgedel(del, cos_del);
                 
                sinA0 = Math.Cos(u1) * Math.Sin(A1);
                double del1 = Math.Atan(Math.Tan(u1) / Math.Cos(A1));
                cos2_A0 = 1 - sinA0 * sinA0;
                 
                alpha = GeoPro.GetAlpha(e1, cos2_A0);
                beta = GeoPro.GetBeta(e1, cos2_A0);
                gama = GeoPro.GetGama(e1, cos2_A0);
                
                delta = (alpha * del + (beta) * Math.Cos(2*del1+del) * Math.Sin(del)
                    +gama*Math.Sin(2*del)*Math.Cos(4*del1+2*del)) * sinA0;
                lamda = dL+delta;
            } while (Math.Abs(delta - deltat)*206265 > 0.00001);
        }

        /// <summary>
        /// 单组数据反算
        /// </summary>
        /// <param name="geodesic">单组大地线数据</param>
        public void InverseSolution(GeodesicInfo geodesic)
        {
            //数据格式转换
            double B1 = GeoPro.DMS2RAD(geodesic.P1.B);
            double L1 = GeoPro.DMS2RAD(geodesic.P1.L);
            double B2 = GeoPro.DMS2RAD(geodesic.P2.B); 
            double L2 = GeoPro.DMS2RAD(geodesic.P2.L); 
            double A12=0, A21=0, S=0;
            //椭球参数
            double e1, e2, b, c,a;
            e1 = Ell.e1; e2 = Ell.e2;
            b = Ell.b; c = Ell.c;a=Ell.a;
            //辅助计算

            double u1 = Math.Atan(Math.Sqrt(1-e1*e1)*Math.Tan(B1));
            double u2 = Math.Atan(Math.Sqrt(1 - e1 * e1) * Math.Tan(B2));
            double dL =L2 - L1;
            double[] ab = CalPara(u1,u2);
            if (u1 == 0 && u2 == 0)
            {
                if (dL > 0)
                {
                    S = a * dL;
                    A12 = Math.PI / 2;
                    A21 = Math.PI * 3 / 2;
                }
                else
                {
                    S = a * dL;
                    A21 = Math.PI / 2;
                    A12 = Math.PI * 3 / 2;
                }
            }
            else
            {
                //逐次趋近法同时计算起点大地方位角，球面长度及经度差lamda
                double del = 0;
                double lamda = 0; 
                double cos2_A0 = 0;

                CalA1_Lamda(dL, u2, u1, ab, ref lamda, ref A12, ref del, ref cos2_A0);

                //计算S
                double[] ABC = new double[3];
                double k_2 = GeoPro.Getk_2(e2, cos2_A0);
                GeoPro.GetABC(b, k_2, ABC);

                double del1 = Math.Atan(Math.Tan(u1) / Math.Cos(A12));
                double xs12 = ABC[2] * Math.Sin(2 * del) * Math.Cos(4 * del1 + 2 * del);

                S = (del - ABC[1] * Math.Sin(del) * Math.Cos(2 * del1 + del) - xs12) / ABC[0];
                //计算A2
                A21 = Math.Atan(Math.Cos(u1) * Math.Sin(lamda) / (ab[2] * Math.Cos(lamda) - ab[3]));
                A21 = GeoPro.InvJudgeA1A2(Math.Cos(u1) * Math.Sin(lamda), (ab[2] * Math.Cos(lamda) - ab[3]), A21);
                //
                if (A12 >= Math.PI) A21 = A21 - Math.PI;
                if (A12 < Math.PI) A21 = A21 + Math.PI;
            }

            //
            geodesic.A12 = GeoPro.RAD2DMS(A12);
            geodesic.A21 = GeoPro.RAD2DMS(A21);
            geodesic.S = S;
        }

        /// <summary>
        /// 多组大地线数据反算
        /// </summary>
        /// <param name="geodesics">多组大地线数据</param>
        public void InversePro(List<GeodesicInfo> geodesics)
        {
            for (int i = 0; i < geodesics.Count; ++i)
            {
                InverseSolution(geodesics[i]);
                //Invertest(geodesics[i]);
            }
        }


        public void Invertest(GeodesicInfo geodesic)
        {
            //数据格式转换
            double B1 = GeoPro.DMS2RAD(geodesic.P1.B);
            double L1 = GeoPro.DMS2RAD(geodesic.P1.L);
            double B2 = GeoPro.DMS2RAD(geodesic.P2.B);
            double L2 = GeoPro.DMS2RAD(geodesic.P2.L);
            double A12 = 0, A21 = 0, S = 0;
            //椭球参数
            double eps, e2,  a,b;
            eps = Ell.e2* Ell.e2; e2 = Ell.e1*Ell.e1;
            a = Ell.a; b = Ell.b;
            double u1 = Math.Atan(Math.Sqrt(1 - e2) * Math.Tan(B1));
            double u2 = Math.Atan(Math.Sqrt(1 - e2) * Math.Tan(B2));
            double DL = L2 - L1;
            double  sa1 =  Math.Sin(u1) *  Math.Sin(u2);
            double sa2 =  Math.Cos(u1) * Math. Cos(u2);
            double cb1 =  Math.Cos(u1) *  Math.Sin(u2);
            double cb2 =  Math.Sin(u1) *  Math.Cos(u2);
            double lambda = DL;
   // '-------特殊情况点位判断----------------
         if (u1 == 0 && u2 == 0)
            {
                if (lambda > 0)
                {
                    S = a * lambda;
                    A12 = Math.PI / 2;
                    A21 = Math.PI * 3 / 2;
                }
                else
                {
                    S = a * lambda;
                    A21 = Math.PI / 2;
                    A12 = Math.PI * 3 / 2;
                }
            }
            else
            {
             double Dlambda=0;
             double sinA0 = 0; double sigma = 0; double sigma1 = 0;
               do
              {
                   double lambda0 = lambda;
                   double p = Math.Cos(u2) * Math.Sin(lambda0);
                   double q = cb1 - cb2 * Math.Cos(lambda0);
                   A12 = Math.Abs(Math.Atan(p / q));
                   if(p>0&&q>0)A12=A12;
                   if(p>0&&q<0)A12=Math.PI-A12;
                   if(p<0&&q<0)A12=Math.PI+A12;
                   if(p<0&&q>0)A12=2*Math.PI-A12;

                    double Ssigma = p * Math.Sin(A12) + q * Math.Cos(A12);
                   double csigma = sa1 + sa2 * Math.Cos(lambda0);
                    sigma = Math.Abs(Math.Atan(Ssigma / csigma));
                   if(csigma>0)sigma=sigma;
                   if(csigma<0)sigma=Math.PI-sigma;
                    sinA0 = Math.Cos(u1) * Math.Sin(A12);
                    sigma1 = Math.Atan(Math.Tan(u1) / Math.Cos(A12));

                    double cosA0 = Math.Sqrt(1 - sinA0 * sinA0);
                    double e4 = e2 * e2;
                    double e6 = e4 * e2;
                    double xk2 = e2 * cosA0 * cosA0;
                     double xk4 = xk2 * xk2;
                    double xk6 = xk4 * xk2;
                   double alpha1 = (e2 / 2 + e4 / 8 + e6 / 16) - e2 * (1 + e2) * xk2 / 16 + 3 * xk4 * e2 / 128;
                  double beta1 = e2 * (1 + e2) * xk2 / 16 - e2 * xk4 / 32;
                 double gamma1 = e2 * xk4 / 256;
                 double xxy = alpha1 * sigma + beta1 * Math.Sin(sigma) * Math.Cos(2 * sigma1 + sigma);
                  xxy = xxy + gamma1 * Math.Sin(2 * sigma) * Math.Cos(4 * sigma1 + 2 * sigma);
                  lambda = DL + sinA0 * xxy;
                  Dlambda = Math.Abs(lambda - lambda0) * 206265;

              } while (Dlambda>0.000001);
             double cosA00 = Math.Sqrt(1 - sinA0 * sinA0);
              double xk20 = eps * cosA00 * cosA00;
              double xk40 = xk20 * xk20;
             double xk60 = xk20 * xk40;
             double alpha = (1 - xk20 / 4 + 7 * xk40 / 64 - 15 * xk60 / 256) / b;
             double beta = xk20 / 4 - xk40 / 8 + 37 * xk60 / 512;
             double gamma = xk40 / 128 - xk60 / 128;
             double xs12 = gamma * Math.Sin(2 * sigma) * Math.Cos(4 * sigma1 + 2 * sigma);
            S = (sigma - beta * Math.Sin(sigma) * Math.Cos(2 * sigma1 + sigma) - xs12) / alpha;
         
             double sinA2 = Math.Cos(u1) *  Math.Sin(A12);
             double cosA2 = Math.Cos(u1) * Math.Cos(sigma) * Math.Cos(A12) - Math.Sin(u1) * Math.Sin(sigma);
    double tanA2 = sinA2 / cosA2;
             A21 =  Math.Abs( Math.Atan(sinA2 / cosA2));
    double sinA1 =  Math.Sin(A12);
             if(sinA2>0&&cosA2>0)A21=A21;
               if(sinA2>0&&cosA2<0)A21=Math.PI-A21;
               if(sinA2<0&&cosA2<0)A21=Math.PI+A21;
               if(sinA2<0&&cosA2>0)A21=2*Math.PI-A21;
         
         }

            geodesic.A12 = GeoPro.RAD2DMS(A12);
            geodesic.A21 = GeoPro.RAD2DMS(A21);
            geodesic.S = S;

        }
    }
}
