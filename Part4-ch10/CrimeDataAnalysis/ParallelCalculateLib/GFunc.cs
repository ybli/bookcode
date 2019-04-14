using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDTreeDLL;
using PointPattern;

namespace ParallelCalculate
{
    /// <summary>
    /// G函数的计算类
    /// </summary>
    public class GFunc
    {
        public KDTree kdTree = new KDTree(2);

        /// <summary>
        /// 串行建立KD树，输入点集本身的KD树
        /// </summary>
        /// <param name="pointList"></param>
        public GFunc(List<PointInfo> pointList)
        {
            int countNum = 0;
            foreach (var pt in pointList)
            {
                double[] hpoint = new double[] { pt.x2one, pt.y2one };//这时是归一化坐标
                kdTree.insert(hpoint, countNum);
                countNum++;
            }
        }

        /// <summary>
        /// 获取某点与点集中其他点的最邻近距离
        /// </summary>
        /// <param name="index">该点的位置</param>
        /// <param name="pointList">输入的点集</param>
        /// <returns></returns>
        public double GetMinDistance(int index,ref List<PointInfo> pointList)
        {
            PointInfo curPt = pointList[index];
            double[] curKey = new double[] { curPt.x2one, curPt.y2one };
            Object[] resIndex = kdTree.nearest(curKey, 2);
            pointList[index].nearestPoint = pointList[(int)resIndex[1]];
            pointList[index].nearestDistance = Algorithm.P2PdistanceCompute2one(curPt, pointList[index].nearestPoint);
            return pointList[index].nearestDistance;
        }

        /// <summary>
        /// 并行获取所有点的最邻近点和距离
        /// </summary>
        /// <param name="pointList"></param>
        /// <returns></returns>
        public double[] GetMinDisArr(List<PointInfo> pointList)
        {
            double[] minDisArr = new double[pointList.Count];
            Parallel.ForEach<PointInfo>(pointList, (pt, state, i) =>
            {
                minDisArr[(int)i] = GetMinDistance((int)i, ref pointList);
            });
            return minDisArr;
        }
    }
}
