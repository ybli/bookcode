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
    /// F函数的计算类
    /// </summary>
    public class FFunc
    {
        public KDTree kdTree = new KDTree(2);

        /// <summary>
        /// 串行建立KD树，随机点集的KD树
        /// </summary>
        /// <param name="randPtList">输入的随机点集</param>
        public FFunc(List<PointInfo> randPtList)
        {
            int countNum = 0;
            foreach (var pt in randPtList)
            {
                double[] hpoint = new double[] { pt.x2one, pt.y2one };//这时是归一化坐标
                kdTree.insert(hpoint, countNum);
                countNum++;
            }
        }

        /// <summary>
        /// 获取某点与随机点集中点的最邻近距离
        /// </summary>
        /// <param name="index">该点的位置</param>
        /// <param name="randPointList">输入的随机点集</param>
        /// <returns></returns>
        public double GetMinDistanceToRandPt(ref PointInfo pt, List<PointInfo> randPointList)
        {
            double[] curKey = new double[] { pt.x2one, pt.y2one };
            Object resIndex = kdTree.nearest(curKey);
            pt.nearestPointInRandPonints = randPointList[(int)resIndex];
            pt.nearestDistanceToRandPonints = Algorithm.P2PdistanceCompute2one(pt, pt.nearestPointInRandPonints);
            return pt.nearestDistanceToRandPonints;
        }

        /// <summary>
        /// 并行获取所有点的最邻近点和距离
        /// </summary>
        /// <param name="randPointList">随机点集</param>
        /// <param name="pointList">输入的点集</param>
        /// <returns></returns>
        public double[] GetMinDisArrToRandPt(List<PointInfo> randPointList,ref List<PointInfo> pointList)
        {
            double[] minDisArr = new double[pointList.Count];
            Parallel.ForEach<PointInfo>(pointList, (pt, state, i) =>
            {
                minDisArr[(int)i] = GetMinDistanceToRandPt(ref pt, randPointList);
            });
            return minDisArr;
        }
    }
}
