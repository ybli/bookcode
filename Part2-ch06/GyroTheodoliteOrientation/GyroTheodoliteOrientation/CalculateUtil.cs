using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class CalculateUtil
    {

        /// <summary>
        /// GetCoorDelta
        /// 计算坐标差
        /// </summary>
        /// <param name="traverseEdge">导线边</param>
        /// <param name="xOrY">求X坐标还是Y坐标</param>
        /// <returns>坐标差</returns>
        public static double GetCoorDelta(List<TraverseEdge> traverseEdge, bool xOrY)
        {
            return GetMaxCoor(traverseEdge, xOrY) - GetMinCoor(traverseEdge, xOrY);
        }


        /// <summary>
        /// GetMaxCoor
        /// 求出最大坐标
        /// </summary>
        /// <param name="traverseEdge">导线边</param>
        /// <param name="xOrY">求X坐标还是Y坐标</param>
        /// <returns>坐标</returns>
        public static double GetMaxCoor(List<TraverseEdge> traverseEdge, bool xOrY)
        {
            double maxCoor;
            if (xOrY)
            {
                maxCoor = traverseEdge[0].StartPoint.XCoor;
                for (int i = 0; i < traverseEdge.Count; i++)
                {
                    if (traverseEdge[i].StartPoint.XCoor > maxCoor)
                    {
                        maxCoor = traverseEdge[i].StartPoint.XCoor;
                    }
                    if (traverseEdge[i].EndPoint.XCoor > maxCoor)
                    {
                        maxCoor = traverseEdge[i].EndPoint.XCoor;
                    }
                }
            }
            else
            {
                maxCoor = traverseEdge[0].StartPoint.YCoor;
                for (int i = 0; i < traverseEdge.Count; i++)
                {
                    if (traverseEdge[i].StartPoint.YCoor > maxCoor)
                    {
                        maxCoor = traverseEdge[i].StartPoint.YCoor;
                    }
                    if (traverseEdge[i].EndPoint.YCoor > maxCoor)
                    {
                        maxCoor = traverseEdge[i].EndPoint.YCoor;
                    }
                }
            }
            return maxCoor;
        }

        /// <summary>
        /// GetMinCoor
        /// 求出最小坐标
        /// </summary>
        /// <param name="traverseEdge">导线边</param>
        /// <param name="xOrY">求X坐标还是Y坐标</param>
        /// <returns>坐标</returns>
        public static double GetMinCoor(List<TraverseEdge> traverseEdge, bool xOrY)
        {
            double minCoor;
            if (xOrY)
            {
                minCoor = traverseEdge[0].StartPoint.XCoor;
                for (int i = 0; i < traverseEdge.Count; i++)
                {
                    if (traverseEdge[i].StartPoint.XCoor < minCoor)
                    {
                        minCoor = traverseEdge[i].StartPoint.XCoor;
                    }
                    if (traverseEdge[i].EndPoint.XCoor < minCoor)
                    {
                        minCoor = traverseEdge[i].EndPoint.XCoor;
                    }
                }
            }
            else
            {
                minCoor = traverseEdge[0].StartPoint.YCoor;
                for (int i = 0; i < traverseEdge.Count; i++)
                {
                    if (traverseEdge[i].StartPoint.YCoor < minCoor)
                    {
                        minCoor = traverseEdge[i].StartPoint.YCoor;
                    }
                    if (traverseEdge[i].EndPoint.YCoor < minCoor)
                    {
                        minCoor = traverseEdge[i].EndPoint.YCoor;
                    }
                }
            }
            return minCoor;
        }
    }
}
