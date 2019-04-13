using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>
    /// 导线边
    /// </summary>
    class TraverseEdge
    {
        private TraversePoint startPoint;   // 起点
        private TraversePoint endPoint;     // 终点
        private bool isGyroDirEdge; // 是否是陀螺定向边 
        private int surveyNum;  //陀螺变观测次数
        private double  ma;  //井下一次定向中误差
        private double xcoor; //重心
        private double ycoor; //重心

        /// <summary>
        /// 构造器
        /// </summary>
        public TraverseEdge()
        {
            IsGyroDirEdge = false;
        }
        public TraverseEdge(TraversePoint startPoint,TraversePoint endPoint,bool isGyroDirEdge,int surveyNum)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            IsGyroDirEdge = isGyroDirEdge;
            SurveyNum = surveyNum;
        }

        /// <summary>
        /// 返回导线边与其贯通重要方向x之间的夹角
        /// </summary>
        /// <returns>导线边与x之间的夹角</returns>
        public double GetAzi()
        {
            return Math.Atan2(endPoint.YCoor - startPoint.YCoor,endPoint.XCoor - startPoint.XCoor);
        }

        public double Ma { get { return  ma;} set { ma = value;} }
        public int SurveyNum { get { return surveyNum;} set { surveyNum = value;} }
        public bool IsGyroDirEdge { get { return isGyroDirEdge;} set { isGyroDirEdge = value;} }
        public double Ycoor { get { return ycoor;} set { ycoor = value;} }
        public double Xcoor { get { return xcoor;} set { xcoor = value;} }
        internal TraversePoint StartPoint { get { return startPoint;} set { startPoint = value;} }
        internal TraversePoint EndPoint { get { return endPoint; } set { endPoint = value; } }
    }
}
