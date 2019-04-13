using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorLib
{
    public class Ellipsoid//定义地球椭球，确定基本参数
    {
        public double a;      //长半轴      
        public double f;      //扁率
        public double eccSq;  //第一偏心率的平方
        public double ecc2Sq;   //第二偏心率的平方
        public double M0;

        public Ellipsoid()
        {
            a = 6378137.0;
            f = 0.335281066475e-2;
            Init();
        }
        /// <summary>
        /// 椭圆的构造函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="f"></param>
        public Ellipsoid(double a, double invf)
        {
            this.a = a;
            f = 1.0 / invf;
            Init();
        }

        void Init()
        {
            double b = a * (1 - f);
            eccSq = (a * a - b * b) / a/a;
            ecc2Sq = (a * a - b * b) / b/b;
            M0 = a * (1 - eccSq);//BLH->XYZ
        }
        /// <summary>
        /// 获取W参数
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        /// <returns>W参数</returns>
        public double W(double B)
        {
            double W = Math.Sqrt(1 - eccSq * Math.Pow(Math.Sin(B), 2));//第一基本纬度函数
            return W;//BLH->XYZ
        }

        /// <summary>
        /// 获取eta参数
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        /// <returns>eta</returns>
        public double Eta(double B)
        {
            double Eta = Math.Sqrt(ecc2Sq * Math.Pow(Math.Cos(B), 2));//存疑，也属于基本参数，引入符号中的第三个
            return Eta;
        }

        /// <summary>
        /// 获取tan(B)
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        /// <returns>tanB</returns>
        public double Tan(double B)
        {
            double res = Math.Tan(B);//基本参数，引入符号中第二个
            return res;
        }

        /// <summary>
        /// 获取卯酉圈半径
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        // <returns></returns>        
        public double N(double B)
        {
            double W = Math.Sqrt(1 - eccSq * Math.Pow(Math.Sin(B), 2));
            return a / W;
        }

        /// <summary>
        /// 计算M，子午圈曲率半径
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        /// <param name="W">W</param>
        /// <returns></returns>
        public double M(double B)
        {
            double over = a * (1 - eccSq);
            double w = W(B);
            double res = over / (w * w * w);
            return res;
        }
    }
}
