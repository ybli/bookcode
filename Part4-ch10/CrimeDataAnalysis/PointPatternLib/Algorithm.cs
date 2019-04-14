using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPattern
{
    /// <summary>
    /// 点模式分析中常用算法库
    /// </summary>
    public class Algorithm
    {
        #region 常用方法
        /// <summary>
        /// 计算两个点之间的距离
        /// </summary>
        /// <param name="pt1">起点</param>
        /// <param name="pt2">终点</param>
        /// <returns>两个点的距离</returns>
        public static double P2Pdistance(PointInfo pt1, PointInfo pt2)
        {
            double s = -1;
            double x1 = pt1.x, y1 = pt1.y,
                x2 = pt2.x, y2 = pt2.y;
            double dx = x1 - x2, dy = y1 - y2;
            s = Math.Sqrt(dx * dx + dy * dy);
            return s;
        }

        /// <summary>
        /// 计算两个点之间归一化的距离
        /// </summary>
        /// <param name="pt1">起点</param>
        /// <param name="pt2">终点</param>
        /// <returns>两个点归一化的距离</returns>
        public static double P2PdistanceCompute2one(PointInfo pt1, PointInfo pt2)
        {
            double s = -1;
            double x1 = pt1.x2one, y1 = pt1.y2one,
                x2 = pt2.x2one, y2 = pt2.y2one;
            double dx = x1 - x2, dy = y1 - y2;
            s = Math.Sqrt(dx * dx + dy * dy);
            return s;
        }
        /// <summary>
        /// 找出pointInfoList中x、y的最大最小值
        /// </summary>
        /// <param name="pointInfoList">输入的点集</param>
        /// <param name="xMin">点集中x的最小值</param>
        /// <param name="xMax">点集中x的最大值</param>
        /// <param name="yMin">点集中y的最小值</param>
        /// <param name="yMax">点集中y的最大值</param>
        public static void BoundValue(List<PointInfo> pointInfoList,
            out double xMin,out double xMax,out double yMin,out double yMax)
        {
            xMin = double.MaxValue; xMax = double.MinValue;
            yMin = double.MaxValue; yMax = double.MinValue;
            int n = pointInfoList.Count;
            for (int i = 0; i < n; i++)
            {
                xMin = (xMin <= pointInfoList[i].x) ? xMin : pointInfoList[i].x;
                xMax = (xMax >= pointInfoList[i].x) ? xMax : pointInfoList[i].x;
                yMin = (yMin <= pointInfoList[i].y) ? yMin : pointInfoList[i].y;
                yMax = (yMax >= pointInfoList[i].y) ? yMax : pointInfoList[i].y;
            }
        }

        /// <summary>
        /// 计算点集铺满的面积
        /// </summary>
        /// <param name="pointInfoList">输入的点集</param>
        /// <returns>点集围成的面积</returns>
        public static double AreaCalculate(List<PointInfo> pointInfoList)
        {
            double S = -1;//点集围成的面积
            double xMin, xMax, yMin, yMax;
            BoundValue(pointInfoList, out xMin,out xMax,out yMin,out yMax);
            double dx = xMax - xMin, dy = yMax - yMin;
            S = dx * dy;
            return S;
        }

        #endregion

        #region 与G函数、K函数相关的方法
        /// <summary>
        /// 计算点集中某点到其他点的距离，归一化后的，与K函数相关，G函数也可以用来求最邻近距离
        /// </summary>
        /// <param name="index">该点在点集的位置</param>
        /// <param name="pointInfoList">输入的点集</param>
        private static void P2PdistanceArray(int index,ref List<PointInfo> pointInfoList)
        {
            int n = pointInfoList.Count;
            //只求一部分，因为是两点之间距离只需要求一次
            pointInfoList[index].distanceArr = new double[pointInfoList.Count];
            pointInfoList[index].distanceArr[index] = 0;
            for (int i = index + 1; i < n; i++)
            {
                pointInfoList[index].distanceArr[i] =
                    P2PdistanceCompute2one(pointInfoList[index], pointInfoList[i]);
            }
        }
        /// <summary>
        /// 调用P2PdistanceArray方法把所有点的距离矩阵求出，与K函数相关
        /// </summary>
        /// <param name="pointInfoList">输入的点集</param>
        public static void AllP2PdistanceArray(ref List<PointInfo> pointInfoList)
        {
            int n = pointInfoList.Count;
            for (int i = 0; i < n; i++)
            {
                P2PdistanceArray(i, ref pointInfoList);
                //用矩阵的对称性求出当前点与，该点之前的点，的距离
                for (int j = 0; j < i; j++)
                {
                    pointInfoList[i].distanceArr[j] = pointInfoList[j].distanceArr[i];
                }
            }
        }

        /// <summary>
        /// 求点集中某点到其他点的最邻近距离，与G函数相关
        /// </summary>
        /// <param name="index">该点在点集中的位置</param>
        /// <param name="pointInfoList">输入的点集</param>
        /// <returns>该点的最最邻近距离</returns>
        public static double MinDistance(int index,ref List<PointInfo> pointInfoList)
        {
            double minDis = -1;
            List<double> minDisList;
            if (pointInfoList[index].distanceArr.Count()>0)
            {
                minDisList = pointInfoList[index].distanceArr.ToList();
            }
            else
            {
                AllP2PdistanceArray(ref pointInfoList);
                minDisList= pointInfoList[index].distanceArr.ToList();
            }
            List<double> tempList = new List<double>(minDisList);
            tempList.RemoveAt(index);
            minDis= tempList.Min();
            pointInfoList[index].nearestDistance = minDis;
            int minDisIndex = minDisList.IndexOf(pointInfoList[index].nearestDistance);//求最邻近距离的索引，便于求最邻近点
            pointInfoList[index].nearestPoint = pointInfoList[minDisIndex];
            return minDis;
        }

        #endregion


        #region 与F函数相关的方法
        /// <summary>
        /// 生成随机点集x、y均在0-1之间
        /// </summary>
        /// <param name="size">随机点集的大小</param>
        /// <returns>随机点集的列表</returns>
        public static List<PointInfo> GenerateRandPointList(int size)
        {
            List<PointInfo> randPointList = new List<PointInfo>();
            for (int i = 0; i < size; i++)
            {
                Random ra = new Random();
                PointInfo pt = new PointInfo();
                pt.x2one = ra.NextDouble();
                pt.y2one = ra.NextDouble();
                randPointList.Add(pt);
            }
            return randPointList;
        }

        /// <summary>
        /// 计算某点到随机点集的距离数组(归一化后的)，与F函数相关
        /// </summary>
        /// <param name="pt">输入点</param>
        /// <param name="randPointList">随机点集</param>
        /// <returns>输入点与随机点集的距离数组</returns>
        public static double[] P2RandPtDistanceArr(ref PointInfo pt,List<PointInfo> randPointList)
        {
            double[] distanceToRandPonintsArr = new double[randPointList.Count];
            for (int i = 0; i < randPointList.Count; i++)
            {
                double s = P2PdistanceCompute2one(pt, randPointList[i]);
                distanceToRandPonintsArr[i] = s;
            }
            pt.distanceToRandPonintsArr = distanceToRandPonintsArr;
            return distanceToRandPonintsArr;
        }

        /// <summary>
        /// 计算某点到随机点集的最邻近距离(归一化后的)，与F函数相关
        /// </summary>
        /// <param name="pt">输入点</param>
        /// <param name="randPointList">随机点集</param>
        /// <returns>输入点与随机点集的最邻近距离</returns>
        public static double MinDistanceToRandPt(ref PointInfo pt,List<PointInfo> randPointList)
        {
            double minDis = -1;
            List<double> minDisList;
            //判断是否已经求得该点到随机点集的距离矩阵
            if (pt.distanceToRandPonintsArr.Count()>0)
            {
                minDisList = pt.distanceToRandPonintsArr.ToList();
            }
            else
            {
                minDisList = P2RandPtDistanceArr(ref pt, randPointList).ToList();
            }
            minDis = minDisList.Min();
            int minIndex = minDisList.IndexOf(minDis);
            pt.nearestDistanceToRandPonints = minDis;
            pt.nearestPointInRandPonints = randPointList[minIndex];
            return minDis;
        }

        #endregion
    }
}
