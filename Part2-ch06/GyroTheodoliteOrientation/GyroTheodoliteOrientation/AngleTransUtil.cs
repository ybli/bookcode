using System;

namespace WindowsFormsApp1
{
    /// <summary>
    /// 角度格式转换工具
    /// </summary>
    class AngleTransUtil
    {
        /// <summary>
        /// 弧度转度
        /// </summary>
        /// <param name="rand">弧度</param>
        /// <returns>度</returns>
        public static double RandToDegree(double rand)
        {
            return rand / Math.PI * 180;
        }

        /// <summary>
        /// 度转弧度
        /// </summary>
        /// <param name="degree">度</param>
        /// <returns>弧度</returns>
        public static double DegreeToRand(double degree)
        {
            return degree / 180 * Math.PI;
        }

        /// <summary>
        /// 度转度分秒
        /// </summary>
        /// <param name="degree">度</param>
        /// <returns>度分秒</returns>
        public static double DegreeToDMS(double degree)
        {
            if (degree > 0)
            {
                int iDegree = (int)Math.Floor(degree);
                int iMinute = (int)Math.Floor((degree - iDegree) * 60);
                double second = ((degree - iDegree) * 60 - iMinute) * 60;
                return iDegree + iMinute / 100.0 + second / 10000;
            }
            else
            {
                degree = -degree;
                int iDegree = (int)Math.Floor(degree);
                int iMinute = (int)Math.Floor((degree - iDegree) * 60);
                double second = ((degree - iDegree) * 60 - iMinute) * 60;
                return -(iDegree + iMinute / 100.0 + second / 10000);
            }
        }

        /// <summary>
        /// 度分秒转度
        /// </summary>
        /// <param name="dms">度分秒</param>
        /// <returns>度</returns>
        public static double DMSToDegree(double dms)
        {
            if (dms > 0)
            {
                int iDegree = (int)Math.Floor(dms);
                int iMinute = (int)Math.Floor((dms - iDegree) * 100);
                double second = ((dms - iDegree) * 100 - iMinute) * 100;
                return iDegree + iMinute / 60.0 + second / 3600;
            }
            else
            {
                dms = -dms;
                int iDegree = (int)Math.Floor(dms);
                int iMinute = (int)Math.Floor((dms - iDegree) * 100);
                double second = ((dms - iDegree) * 100 - iMinute) * 100;
                return -(iDegree + iMinute / 60.0 + second / 3600);
            }
        }

        /// <summary>
        /// 度分(dd.mmmm)转度
        /// </summary>
        /// <param name="dm"></param>
        /// <returns>度</returns>
        public static double DMToDegree(double dm)
        {
            if (dm > 0)
            {
                int iDegree = (int)Math.Floor(dm);
                double minute = ((dm - iDegree) * 100);
                return iDegree + minute / 60.0;
            }
            else
            {
                dm = -dm;
                int iDegree = (int)Math.Floor(dm);
                double minute = ((dm - iDegree) * 100);
                return -(iDegree + minute / 60.0);
            }
        }

        /// <summary>
        /// 度分秒转字符串
        /// </summary>
        /// <param name="dms">度分秒</param>
        /// <returns>字符串</returns>
        public static string DMSToStr(double dms)
        {
            if (dms > 0)
            {
                int iDegree = (int)Math.Floor(dms);
                double iMinute = (int)Math.Floor((dms - iDegree) * 100);
                double second = ((dms - iDegree) * 100 - iMinute) * 100;
                return iDegree + "°" + iMinute + "′" + Math.Round(second, 2) + "″";
            }
            else
            {
                dms = -dms;
                int iDegree = (int)Math.Floor(dms);
                int iMinute = (int)Math.Floor((dms - iDegree) * 100);
                double second = ((dms - iDegree) * 100 - iMinute) * 100;
                return "-" + iDegree + "°" + iMinute + "′" + string.Format("{0:N2}", second) + "″";
            }
        }
    }
}
