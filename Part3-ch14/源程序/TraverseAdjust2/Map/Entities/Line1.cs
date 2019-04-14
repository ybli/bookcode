using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Map.Common;

namespace Map.Entities
{
    /*
   * 功能概要：单线图元
   * 编号：Map_Entities_004
   * 作者：廖振修
   *  创建日期:2016-06-09
   */
    public class Line1 : GElement
    {
        private PointF mStartPt;
        private PointF mEndPt;

       /// <summary>
        ///  构造函数
       /// </summary>
       /// <param name="lineName">线名</param>
       /// <param name="startPt">开始点</param>
       /// <param name="endPt">结束点</param>
       /// <param name="color">颜色</param>
        public Line1(string lineName, PointF startPt, PointF endPt,Color color)
        {
            this.Name = lineName;
            this.mStartPt = startPt;
            this.mEndPt = endPt;
            this.Color = color;
        }

        /// <summary>
        ///     ''' 画单线
        ///     ''' </summary>
        ///     ''' <param name="g">Graphics对象</param>
        ///     ''' <remarks></remarks>
        public override void Draw(System.Drawing.Graphics g)
        {
            Point eb, ee; // 设备坐标下的线段起点，终点
            PointF ebF, eeF; // 实际坐标下的线段起点，终点
            ebF = new PointF(mStartPt.X, mStartPt.Y);
            eeF = new PointF(mEndPt.X, mEndPt.Y);
            eb = BaseFunction.RPToDP(ebF);
            ee = BaseFunction.RPToDP(eeF);

            Pen pen = new Pen(Color, 1.0f);
            g.DrawLine(pen, eb, ee);
        }
    }

}//endspace
