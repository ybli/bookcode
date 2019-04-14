using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorTran
{
    //核心算法;
    public class Algorithm
    {
        //输入：一个椭球参数与一个点的某些值;
        //输出：该个点的另一些值;
        public static void BL2xy(EarthPara earth, SpacePoint pt)
        {
            double B = pt.B;
            double N = earth.N(B);
            double t = earth.t(B);
            double eta = Math.Sqrt(earth.Eta2(B));
            //double Y0 = 500000.0;//Y方向的平移量;

            double[] coef = new double[6];
            CoefCalculator(earth, coef);

            double meridianX = coef[0] * B + coef[1] * Math.Sin(2 * B) + coef[2] * Math.Sin(4 * B) +
                coef[3] * Math.Sin(6 * B) + coef[4] * Math.Sin(8 * B) + coef[5] * Math.Sin(10 * B);//求子午线弧长;
            double dl = pt.L - earth.L0 * Math.PI / 180;//经差;
            double[] an = new double[7];
            an[0] = meridianX;
            an[1] = N * Math.Cos(B);
            an[2] = N * t * Math.Pow(Math.Cos(B), 2) / 2.0;
            an[3] = N * (1 - t * t + eta * eta) * Math.Pow(Math.Cos(B), 3) / 6.0;
            an[4] = N * t * (5 - t * t + 9 * eta * eta + 4 * Math.Pow(eta, 4)) * Math.Pow(Math.Cos(B), 4) / 24.0;
            an[5] = N * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * eta * eta - 58 * Math.Pow(eta, 2) * Math.Pow(t, 2)) * Math.Pow(Math.Cos(B), 5) / 120.0;
            an[6] = N * t * (61 - 58 * t * t + Math.Pow(t, 4) + 270 * eta * eta - 330 * Math.Pow(eta, 2) * Math.Pow(t, 2)) * Math.Pow(Math.Cos(B), 6) / 720.0;

            pt.x = an[0] + an[2] * dl * dl + an[4] * Math.Pow(dl, 4) + an[6] * Math.Pow(dl, 6);
            pt.y = an[1] * dl + an[3] * Math.Pow(dl, 3) + an[5] * Math.Pow(dl, 5);
        }

        public static void xy2BL(EarthPara earth, SpacePoint pt)
        {
            double[] coef = new double[6];
            CoefCalculator(earth, coef);

            double addNum = 0;
            double X = pt.x + addNum;
            double y = pt.y + addNum;

            double B0 = X / coef[0];
            double delta = coef[1] * Math.Sin(2 * B0) + coef[2] * Math.Sin(4 * B0) +
                coef[3] * Math.Sin(6 * B0) + coef[4] * Math.Sin(8 * B0) + coef[5] * Math.Sin(10 * B0);
            double Bf = (X - delta) / coef[0];

            if (Math.Abs(Bf - B0) > 1.0e-8)
            {
                do
                {
                    B0 = Bf;
                    delta = coef[1] * Math.Sin(2 * B0) + coef[2] * Math.Sin(4 * B0) +
                        coef[3] * Math.Sin(6 * B0) + coef[4] * Math.Sin(8 * B0) + coef[5] * Math.Sin(10 * B0);
                    Bf = (X - delta) / coef[0];
                } while (Math.Abs(Bf - B0) > 1.0e-8);
            }

            //求解b的系数;
            double Nf = earth.N(Bf);
            //double Wf = ell.W(Bf);
            double Mf = earth.M(Bf);
            double tf = earth.t(Bf);
            double etaf = Math.Sqrt(earth.Eta2(Bf));
            double[] bn = new double[7];
            bn[0] = Bf;
            bn[1] = 1.0 / (Nf * Math.Cos(Bf));
            bn[2] = -tf / (2 * Mf * Nf);
            bn[3] = -(1 + 2 * tf * tf + etaf * etaf) * bn[1] / 6.0 / Nf / Nf;
            bn[4] = -(5 + 3 * tf * tf + etaf * etaf - 9 * tf * tf * etaf * etaf) * bn[2] / 12.0 / Nf / Nf;
            bn[5] = -(5 + 28 * tf * tf + 24 * Math.Pow(tf, 4) + 6 * etaf * etaf + 8 * tf * tf * etaf * etaf) * bn[1] / 120.0 / Math.Pow(Nf, 4);
            bn[6] = (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * bn[2] / 360.0 / Math.Pow(Nf, 4);


            double B = bn[0] + bn[2] * y * y + bn[4] * Math.Pow(y, 4) + bn[6] * Math.Pow(y, 6);
            double dl = bn[1] * y + bn[3] * Math.Pow(y, 3) + bn[5] * Math.Pow(y, 5);
            double L = earth.L0 * Math.PI / 180 + dl;
            pt.B = B;
            pt.L = L;
        }

        static void CoefCalculator(EarthPara earth, double[] coef)
        {
            double Ac, Bc, Cc, Dc, Ec, Fc;
            double M0 = earth.M0;
            double e2 = earth.e1Square;
            double e4 = e2 * e2;
            double e6 = e4 * e2;
            double e8 = e4 * e4;
            double e10 = e6 * e4;

            Ac = 1 + 3.0 / 4.0 * e2 + 45.0 / 64.0 * e4 + 175.0 / 256.0 * e6 +
                11025.0 / 16384.0 * e8 + 43659.0 / 65536.0 * e10;
            Bc = 3.0 / 4.0 * e2 + 15.0 / 16.0 * e4 + 525.0 / 512.0 * e6 +
                2205.0 / 2048.0 * e8 + 72765.0 / 65536.0 * e10;
            Cc = 15.0 / 64.0 * e4 + 105.0 / 256.0 * e6 + 2205.0 / 4096.0 * e8 +
                10395.0 / 16384.0 * e10;
            Dc = 35.0 / 512.0 * e6 + 315.0 / 2048.0 * e8 + 31185.0 / 131072.0 * e10;
            Ec = 315.0 / 16384.0 * e8 + 3465.0 / 65536.0 * e10;
            Fc = 693.0 / 131072.0 * e10;

            //coef = new double[6];//用来存储相应的系数;
            coef[0] = Ac * M0;
            coef[1] = -Bc * M0 / 2.0;
            coef[2] = Cc * M0 / 4.0;
            coef[3] = -Dc * M0 / 6.0;
            coef[4] = Ec * M0 / 8.0;
            coef[5] = -Fc * M0 / 10.0;
        }

        //弧度、角度互相转换
        public static double D2R(double degree)
        {
            double radian = -1;
            radian = degree / 180 * Math.PI;
            return radian;
        }

        public static double R2D(double radian)
        {
            double degree = -1;
            degree = radian / Math.PI * 180;
            return degree;
        }
    }
}
