using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARModel
{
    class StatisticsTest
    {
        /// <summary>
        /// 计算  n/2 的Γ函数值: Γ(n/2)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double gam(int n)
        {
            int i;
            double k;

            if ((n % 2) == 1) // n为奇数
            {
                k = 1.772453850905516;
                i = 1;
            }
            else
            {
                k = 1.0;
                i = 2;
            }

            while (i <= n - 2)
            {
                k *= i / 2.0;
                i += 2;
            }

            return k;
        }


        /// <summary>
        ///  正态分布函数值:  p(-∞,u)
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static double norm(double u)
        {
            if (u < -5.0) return 0.0;
            if (u > 5.0) return 1.0;

            double y = System.Math.Abs(u) / System.Math.Sqrt(2.0);

            double p = 1.0 + y * (0.0705230784 + y * (0.0422820123 + y * (0.0092705272 +
                y * (0.0001520143 + y * (0.0002765672 + y * 0.0000430638)))));

            double er = 1 - System.Math.Pow(p, -16.0);
            p = (u < 0.0) ? 0.5 - 0.5 * er : 0.5 + 0.5 * er;
            return p;
        }



        /// <summary>
        /// 正态分布的反函数, p(-∞,u)=p ; 已知p, 返回u
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double re_norm(double p)
        {
            if (p == 0.5) return 0.0;
            if (p > 0.9999997) return 5.0;
            if (p < 0.0000003) return -5.0;
            if (p < 0.5) return -re_norm(1.0 - p);

            double y = -System.Math.Log(4.0 * p * (1.0 - p));
            y = y * (1.570796288 + y * (0.3706987906e-1
                + y * (-0.8364353589e-3 + y * (-0.2250947176e-3
                + y * (0.6841218299e-5 + y * (0.5824238515e-5
                + y * (-0.1045274970e-5 + y * (0.8360937017e-7
                + y * (-0.3231081277e-8 + y * (0.3657763036e-10
                + y * 0.6936233982e-12))))))))));

            return System.Math.Sqrt(y);

        }




        /// <summary>
        /// chi2分布函数值及密度值:
        /// </summary>
        /// <param name="n"></param>
        /// <param name="x"></param>
        /// <param name="f">密度值</param>
        /// <returns>区间(0,x)上的概率p</returns>
        public static double chi2(int n, double x, out double f)
        {
            f = 0;
            double iai;
            double p, Ux;
            double pi = Math.PI;

            double y = x / 2.0;
            if ((n % 2) == 1)
            {
                Ux = System.Math.Sqrt(y / pi) * System.Math.Exp(-y);
                p = 2.0 * norm(System.Math.Sqrt(x)) - 1.0;
                iai = 0.5;
            }
            else
            {
                Ux = y * System.Math.Exp(-y);
                p = 1.0 - System.Math.Exp(-y);
                iai = 1.0;
            }

            while (iai != 0.5 * n)
            {
                p = p - Ux / iai;
                Ux = Ux * y / iai;
                iai += 1.0;
            }
            f = Ux / x;
            return p;

        }





        /// <summary>
        /// chi方分布的反函数:  p=F(0,x)
        /// </summary>
        /// <param name="n">自由度n</param>
        /// <param name="p">已知概率值p</param>
        /// <returns>反求x</returns>
        public static double re_chi2(int n, double p)
        {
            if (p > 0.9999999) p = 0.9999999;
            if (n == 1)
            {
                double x = re_norm((1.0 - p) / 2.0);
                return x * x;
            }

            if (n == 2) return -2.0 * System.Math.Log(1.0 - p);

            double u = re_norm(p);
            double w = 2.0 / (9.0 * n);
            double x0 = 1.0 - w + u * System.Math.Sqrt(w);
            x0 = n * x0 * x0 * x0;

            while (true)
            {
                double f;
                double pp = chi2(n, x0, out f);
                if (f + 1.0 == 1.0) return x0;
                double xx = x0 - (pp - p) / f;
                if (System.Math.Abs(x0 - xx) < 0.001) return xx;

                x0 = xx;
            }

        }


        //////////////////////////////////////////////////////////////////////////
        //  
        //  已知x,n1,n2, 求p,f
        //  f: 
        /// <summary>
        /// 计算F分布函数值: 区间(0,x)上的概率p
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="x"></param>
        /// <param name="f">x处的概率密度值</param>
        /// <returns></returns>
        public static double F(int n1, int n2, double x, out double f)
        {
            double y = n1 * x / (n2 + n1 * x);
            double Ux;
            double p = B(n1, n2, y, out Ux);

            f = Ux / x;
            return p;
        }


        /// <summary>
        /// F分布的反函数：p=F(0,x), 已知p,反求x
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double re_F(int n1, int n2, double p)
        {
            double d, f0;

            double a = 2.0 / (9.0 * n1);
            double b = 2.0 / (9.0 * n2);
            double a1 = 1.0 - a;
            double b1 = 1.0 - b;
            double u = re_norm(p);
            double e = b1 * b1 - b * u * u;

            if (e <= 0.8)
            {
                double a11 = 2.0 * System.Math.Pow(n2 + 0.0, 0.5 * n2 - 1.0);
                double a2 = System.Math.Pow(n1 + 0.0, 0.5 * n2);
                double B = gam(n1) * gam(n2) / gam(n1 + n2);
                double f = a11 / a2 / B / (1 - p);
                f0 = System.Math.Pow(f, 2.0 / n2);

            }
            else
            {
                f0 = (a1 * b1 + u * System.Math.Sqrt(a1 * a1 * b + a * e)) / e;
                f0 = f0 * f0 * f0;
            }

            if (f0 < 0.0) f0 = 0.01;

            while (true)
            {
                double pp = F(n1, n2, f0, out d);
                double df = (p - pp) / d;

                while (System.Math.Abs(f0) < System.Math.Abs(df)) df /= 2.0;
                f0 = f0 + df;

                if (System.Math.Abs(df) / f0 < 0.0001 || System.Math.Abs(df) < 0.0001) //按有效位数决定是否退出计算
                {
                    return f0;
                }
            }

        }


        //////////////////////////////////////////////////////////////////////////
        //      Ux: 
        //   2008-3-5,  增加了B分布函数,

        /// <summary>
        /// B分布函数值: 区间(0,x)上的概率p  已知x,n1,n2, 求q,Ux
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="x"></param>
        /// <param name="Ux">x处的概率密度值</param>
        /// <returns></returns>
        public static double B(int n1, int n2, double x, out double Ux)
        {
            Ux = 0;
            int m1 = 0, m2 = 0;
            double Ix = 0;

            double pi = 3.14159265358979312;

            if ((n1 % 2) == 1 && (n2 % 2) == 1)   /*  n1,n2均是奇数  */
            {
                Ux = System.Math.Sqrt(x * (1.0 - x)) / pi;
                Ix = 1.0 - 2.0 * System.Math.Atan(System.Math.Sqrt((1.0 - x) / x)) / pi;
                m1 = m2 = 1;
            }

            if ((n1 % 2) == 1 && (n2 % 2) != 1) /*  n1是奇数,n2是偶数  */
            {
                Ux = System.Math.Sqrt(x) * (1.0 - x) / 2.0;
                Ix = System.Math.Sqrt(x);
                m1 = 1;
                m2 = 2;
            }

            if ((n1 % 2) != 1 && n2 % 2 == 1)  /*  n1是偶数,n2是奇数  */
            {
                Ux = x * System.Math.Sqrt(1.0 - x) / 2.0;
                Ix = 1.0 - System.Math.Sqrt(1.0 - x);
                m1 = 2;
                m2 = 1;
            }

            if ((n1 % 2) != 1 && (n2 % 2) != 1)  /*  n1,n2均是偶数  */
            {
                Ux = x * (1.0 - x);
                Ix = x;
                m1 = m2 = 2;
            }

            while (m2 != n2)
            {
                Ix = Ix + 2.0 * Ux / m2;
                Ux = Ux * (1.0 + m1 / (m2 + 0.0)) * (1.0 - x);
                m2 = m2 + 2;
            }

            while (m1 != n1)
            {
                Ix = Ix - 2.0 * Ux / m1;
                Ux = Ux * (1.0 + m2 / (0.0 + m1)) * x;
                m1 = m1 + 2;
            }

            return Ix;
        }






        /// <summary>
        /// t分布的分布函数值（负无穷到t的积分值）
        /// </summary>
        /// <param name="nn">自由度</param>
        /// <param name="t"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static double t(int nn, double t, out double f)
        {
            f = 0.0;
            if (t + 1.0 == 1.0) return 0.5;

            double x = t * t / (nn + t * t);

            double P = 0.5 * B(1, nn, x, out f);

            if (x < 0.0) P = 0.5 - P;
            else P = 0.5 + P;

            f = f / System.Math.Abs(t);

            return P;
        }



        /// <summary>
        /// t分布的反函数：p=F(x),已知p，反求x
        /// </summary>
        /// <param name="n"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double re_t(int n, double p)
        {
            if (p < 0.5)
            {
                return -re_t(n, 1.0 - p);
            }
            double x;
            double pi = 3.14159265358979312;

            if (n == 1)
            {
                x = System.Math.Tan(pi * (p - 0.5));
            }
            else if (n == 2)
            {
                double u = 2.0 * p - 1.0;
                u = u * u;
                x = System.Math.Sqrt(2.0 * u / (1.0 - u));
            }
            else
            {
                x = re_norm(p) * System.Math.Sqrt(n / (n - 2.0));

                while (true)
                {
                    double f; //密度值
                    double F = t(n, x, out f);
                    double dx = (F - p) / f;
                    x = x - dx;
                    if (System.Math.Abs(dx) < 0.001) break;
                }
            }
            return x;
        }


    }
}
