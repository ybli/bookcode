using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniClosedLoopSearch
{
    /// <summary>
    /// 水准观测线
    /// </summary>
   public  class LevelingLine
    {
        int leveingLineNum;//观测的水准路线的编号。
        /// <summary>
        /// //观测的水准路线的编号。
        /// </summary>
        public int LeveingLineNum
        {
            get { return leveingLineNum; }
            set { leveingLineNum = value; }
        }
        LevelingPoint starPoint;//水准路线的起点。
        /// <summary>
        /// //水准路线的起点。
        /// </summary>
        internal LevelingPoint StarPoint
        {
            get { return starPoint; }
            set { starPoint = value; }
        }
        LevelingPoint endPoint;//水准路线的终点。
        /// <summary>
        /// 水准路线的终点。
        /// </summary>
        internal LevelingPoint EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        double leveingHeightDifferent;//水准路线的观测高差。
        /// <summary>
        /// 水准路线的观测高差。
        /// </summary>
        public double LeveingHeightDifferent
        {
            get { return leveingHeightDifferent; }
            set { leveingHeightDifferent = value; }
        }
        double leveingRoadLength;//水准路线的长度。
        /// <summary>
        /// 水准路线的长度。
        /// </summary>
        public double LeveingRoadLength
        {
            get { return leveingRoadLength; }
            set { leveingRoadLength = value; }
        }


    }
}
