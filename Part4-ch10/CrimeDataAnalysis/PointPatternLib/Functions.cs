using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPattern
{
    /// <summary>
    /// G、F、K函数的计算主体
    /// </summary>
    public class Functions
    {

        /// <summary>
        /// 计算每个点周围半径为d的距离范围内点的个数，K函数中用到
        /// </summary>
        /// <param name="pointList">输入的点集</param>
        /// <param name="pointIndex">点的位置</param>
        /// <param name="d">距离</param>
        /// <returns>在距离范围内的点的个数</returns>
        private static int PointNumInCircle(List<PointInfo> pointList, int pointIndex, double d)
        {
            int num = -1;
            num =
                pointList[pointIndex].distanceArr.ToList().Count(n => n <= d);
            return (num - 1);//用求得的数量减去自己
        }

        /// <summary>
        /// 计算K函数
        /// </summary>
        /// <param name="pointList">输入的点集</param>
        /// <param name="area">该点集围成的面积</param>
        /// <param name="d">距离</param>
        /// <returns>K函数值</returns>
        public static double Kfunction(List<PointInfo> pointList, double area, double d)
        {
            double K = -1;
            int n = pointList.Count;
            int sumPointNumInCircle = 0;
            for (int i = 0; i < n; i++)
            {
                sumPointNumInCircle += PointNumInCircle(pointList, i, d);
            }
            K = area / (n * n) * sumPointNumInCircle;
            return K;
        }

        /// <summary>
        /// 计算G函数
        /// </summary>
        /// <param name="pointList">输入的点集</param>
        /// <param name="d">距离</param>
        /// <returns>G函数值</returns>
        public static double Gfunction(List<PointInfo> pointList, double d)
        {
            double G = -1;
            int countNum = 0;
            foreach (var pt in pointList)
            {
                if (pt.nearestDistance < d)
                    countNum++;
            }
            G = (double)countNum / (pointList.Count);
            return G;
        }

        /// <summary>
        /// 计算F函数
        /// </summary>
        /// <param name="pointList">输入的点集</param>
        /// <param name="d">距离</param>
        /// <returns>F函数值</returns>
        public static double Ffunction(List<PointInfo> pointList, double d)
        {
            double F = -1;
            int countNum = 0;
            foreach (var pt in pointList)
            {
                if (pt.nearestDistanceToRandPonints <= d)
                    countNum++;
            }
            F = (double)countNum / (pointList.Count);
            return F;
        }
    }
}
