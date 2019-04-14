using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointPattern
{
    /// <summary>
    /// 空间中的一个点，二维点
    /// </summary>
    public class PointInfo
    {
        public double x, y;
        public string pointID;

        #region 在点集中才有的字段或属性
        /// <summary>
        /// 归一化的坐标，在点集中才有的字段
        /// </summary>
        public double x2one, y2one;
        /// <summary>
        /// 该点在点集中的最邻近点，与G函数相关的属性
        /// </summary>
        public PointInfo nearestPoint;
        public double nearestDistance;
        /// <summary>
        /// 该点在随机点集中的最邻近点，与F函数相关的属性
        /// </summary>
        public PointInfo nearestPointInRandPonints;
        public double nearestDistanceToRandPonints;
        /// <summary>
        /// 该点在点集中与其他点的距离，储存在一个数组中，包含与自身的距离。
        /// 即是距离矩阵的一行，与K函数相关，G函数也能用来求最邻近距离
        /// </summary>
        public double[] distanceArr;
        /// <summary>
        /// 该点与随机点集的距离，储存为一个数组，与F函数相关
        /// </summary>
        public double[] distanceToRandPonintsArr;
        #endregion

        public PointInfo()
        {
            x = -1;y = -1;
            pointID = string.Empty;

            //nearestPoint = new PointInfo();
            //nearestDistance = -1;
            //nearestPointInRandPonints = new PointInfo();
            //nearestDistanceToRandPonints = -1;
        }

        public PointInfo(string _pointID,double _x,double _y)
        {
            pointID = _pointID;
            x = _x;y = _y;
        }
    }
}
