using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodesyCal
{
    /// <summary>
    /// Geo辅助函数类
    /// </summary>
    public class GeoPro
    {
        /// <summary>
        /// Dms格式转Rad格式数据
        /// </summary>
        /// <param name="dms">dms格式数据</param>
        /// <returns>rad格式数据</returns>
        public static double DMS2RAD(double dmsvalue)
        {
            int degvalue, minvalue, sign;
            double radvalue = 0, secvalue;
            sign = 1;
            if (dmsvalue < 0)
            {
                sign = -1;
                dmsvalue = System.Math.Abs(dmsvalue);
            }
            degvalue = (int)(dmsvalue);
            minvalue = (int)((dmsvalue - degvalue) * 100 + 0.0001);
            secvalue = (dmsvalue - degvalue - minvalue / 100.0) * 10000.0;
            radvalue = (degvalue + minvalue / 60.0 + secvalue / 3600.0) * Math.PI / 180.0;
            radvalue = radvalue * sign;
            return radvalue;
        }
        /// <summary>
        /// Rad格式转Dms格式数据
        /// </summary>
        /// <param name="rad">Rad格式数据</param>
        /// <returns>Dms格式数据</returns>
        public static double RAD2DMS(double radvalue)
        {
            if (radvalue > 2 * Math.PI)
                radvalue = radvalue - 2 * Math.PI;
            if (radvalue < -2 * Math.PI)
                radvalue = radvalue + 2 * Math.PI;

            int degvalue, minvalue, sign;
            double secvalue, dmsvalue = 0;
            sign = 1;
            if (radvalue < 0)
            {
                sign = -1;
                radvalue = System.Math.Abs(radvalue);
            }
            secvalue = radvalue * 180.0 / Math.PI * 3600.0;
            degvalue = (int)(secvalue / 3600 + 0.0001);
            minvalue = (int)((secvalue - degvalue * 3600.0) / 60.0 + 0.0001);
            secvalue = secvalue - degvalue * 3600.0 - minvalue * 60.0;
            if (secvalue < 0) secvalue = 0;
            dmsvalue = degvalue + minvalue / 100.0 + secvalue / 10000.0;
            dmsvalue = dmsvalue * sign;
            return dmsvalue;

        }
        /// <summary>
        /// 获取ita值
        /// </summary>
        /// <param name="e2">椭球第二偏心率</param>
        /// <param name="B">维度</param>
        /// <returns>返回ita</returns>
        public static double GetIta(double e2, double B)
        {
            double ita = 0;
            ita = Math.Sqrt(e2 * e2 * Math.Cos(B) * Math.Cos(B));
            return ita;
        }
        /// <summary>
        /// 获取辅助参数W
        /// </summary>
        /// <param name="e1">椭球第一偏心率</param>
        /// <param name="B">维度</param>
        /// <returns>参数W</returns>
        public static double GetW(double e1, double B)
        {
            double W = 0;
            W = Math.Sqrt(1 - e1 * e1 * Math.Sin(B) * Math.Sin(B));
            return W;
        }
        /// <summary>
        /// 获取参数k*k
        /// </summary>
        /// <param name="e2">椭球第二偏心率</param>
        /// <param name="cos2_A0">cosA0*cosA0</param>
        /// <returns>返回k*k</returns>
        public static double Getk_2(double e2, double cos2_A0)
        {
            double k_2 = 0;
            k_2 = e2 * e2 * cos2_A0;
            return k_2;
        }
        /// <summary>
        /// 获取ABC参数
        /// </summary>
        /// <param name="b">椭球短半轴</param>
        /// <param name="k_2">参数k*k</param>
        /// <param name="ABC">ABC参数数组</param>
        public static void GetABC(double b, double k_2, double[] ABC)
        {
            ABC[0] = (1 - k_2 / 4 + 7.0 * k_2 * k_2 / 64 - 15.0 * k_2 * k_2 * k_2 / 256) / b;
            ABC[1] = (k_2 / 4 - k_2 * k_2 / 8 + 37.0 * k_2 * k_2 * k_2 / 512);
            ABC[2] = (k_2 * k_2 / 128 - k_2 * k_2 * k_2 / 128);
        }
        /// <summary>
        /// 获取Alpha参数
        /// </summary>
        /// <param name="e1">椭球第一偏心率</param>
        /// <param name="cos2_A0">cosA0*cosA0</param>
        /// <returns>Alpha参数</returns>
        public static double GetAlpha(double e1, double cos2_A0)
        {
            double alpha = 0;
            alpha = (e1 * e1 / 2 + Math.Pow(e1, 4) / 8 + Math.Pow(e1, 6) / 16) - (Math.Pow(e1, 4) / 16 +
                Math.Pow(e1, 6) / 16) * cos2_A0 + (3 * Math.Pow(e1, 6) / 128) * cos2_A0 * cos2_A0;
            return alpha;
        }
        /// <summary>
        /// 获取Beta参数
        /// </summary>
        /// <param name="e1">椭球第一偏心率</param>
        /// <param name="cos2_A0"></param>
        /// <returns></returns>
        public static double GetBeta(double e1, double cos2_A0)
        {
            double Beta = 0;
            Beta = (Math.Pow(e1, 4) / 16 + Math.Pow(e1, 6) / 16) * cos2_A0
                - (Math.Pow(e1, 6) / 32) * cos2_A0 * cos2_A0;
            return Beta;
        }
        public static double GetGama(double e1, double cos2_A0)
        {
            double Gama = 0;
            Gama = (Math.Pow(e1, 6) / 256) * cos2_A0 * cos2_A0;
            return Gama;
        }
        /// <summary>
        /// Lamba角象限判断
        /// </summary>
        /// <param name="sinA1">sinA1</param>
        /// <param name="lamba0">lamba值</param>
        /// <returns>划分象限后的lamba</returns>
        public static double DirJudgelamba(double sinA1, double lamba0)
        {
            double lamba = Math.Abs(lamba0);
            if (sinA1 > 0 && Math.Tan(lamba0) > 0) lamba = Math.Abs(lamba0);
            if (sinA1 > 0 && Math.Tan(lamba0) < 0) lamba = Math.PI - Math.Abs(lamba0);
            if (sinA1 < 0 && Math.Tan(lamba0) < 0) lamba = -Math.Abs(lamba0);
            if (sinA1 < 0 && Math.Tan(lamba0) > 0) lamba = Math.Abs(lamba0) - Math.PI;
            return lamba;
        }
        /// <summary>
        /// A21角象限判断
        /// </summary>
        /// <param name="sinA1">sinA1</param>
        /// <param name="A2">A2值</param>
        /// <returns>划分象限后的A21</returns>
        public static double DirJudgeA2(double sinA1, double A2)
        {
            double A2_ = Math.Abs(A2);
            if (sinA1 < 0 && Math.Tan(A2) > 0) A2_ = Math.Abs(A2);
            if (sinA1 < 0 && Math.Tan(A2) < 0) A2_ = Math.PI - Math.Abs(A2);
            if (sinA1 > 0 && Math.Tan(A2) > 0) A2_ = Math.PI + Math.Abs(A2);
            if (sinA1 > 0 && Math.Tan(A2) < 0) A2_ = 2 * Math.PI - Math.Abs(A2);

            return A2_;
        }
        /// <summary>
        /// A1A2角象限判断
        /// </summary>
        /// <param name="up">分子</param>
        /// <param name="down">分母</param>
        /// <param name="A1_2">初始值</param>
        /// <returns>判定值</returns>
        public static double InvJudgeA1A2(double up, double down, double A1_2)
        {
            double A = Math.Abs(A1_2);
            if (up > 0 && down > 0) A = Math.Abs(A1_2);
            else if (up > 0 && down < 0) A = Math.PI - Math.Abs(A1_2);
            else if (up < 0 && down < 0) A = Math.PI + Math.Abs(A1_2);
            else A = 2 * Math.PI - Math.Abs(A1_2);
            return A;
        }
        /// <summary>
        /// del角象限判断
        /// </summary>
        /// <param name="del">del</param>
        /// <param name="cosdel">cosdel</param>
        /// <returns></returns>
        public static double InvJudgedel(double del, double cosdel)
        {
            double del_ = 0;
            if (cosdel > 0) del_ = Math.Abs(del);
            if (cosdel < 0) del_ = Math.PI - Math.Abs(del);
            return del_;
        }


        public static string DMS2String(double arc)
        {

            string str = "";
            double d = arc;
            double dd, mm, ss; int sign = 1;
            if (d < 0)
            {
                d = -d; sign = -1;
            }
            dd = (int)d;
            mm = (int)((d - dd) * 100);
            ss = (d - dd - mm / 100) * 10000;
            str = (sign * dd).ToString() + "°" + mm.ToString() + "′" + ss.ToString("0.0") + "″";
            return str;

        }
    }
}
