using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Map.Common;

namespace Map.Entities
{
    /*
   * 功能概要：双线图元
   * 编号：Map_Entities_005
   * 作者：廖振修
   *  创建日期:2016-06-09
   */
    public class Line2 : GElement
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
        public Line2(string lineName, PointF startPt, PointF endPt,Color color)
        {
            this.Name = lineName;
            this.mStartPt = startPt;
            this.mEndPt = endPt;
            this.Color = color;
        }

        /// <summary>
        ///     ''' 画双线
        ///     ''' </summary>
        ///     ''' <param name="g">Graphics对象</param>
        ///     ''' <remarks></remarks>
        public override void Draw(System.Drawing.Graphics g)
        {
            Point eb, ee; // 设备坐标下的线段起点，终点
            PointF ebF, eeF; // 实际坐标下的线段起点，终点
            PointF ebF2, eeF2; // 实际坐标下的线段起点，终点(双线)


            ebF = new PointF(mStartPt.X, mStartPt.Y);
            eeF = new PointF(mEndPt.X, mEndPt.Y);
            double angleA = GetAngle(ebF, eeF); // 求直线倾角(0-2Pi）
            double d = BaseFunction.DPToRP(4); // 直线偏移距离(4个像素)
            ebF2 = new PointF((float)(ebF.X + d * Math.Sin(angleA)), (float)(ebF.Y + d * Math.Cos(angleA)));
            eeF2 = new PointF((float)(eeF.X + d * Math.Sin(angleA)), (float)(eeF.Y + d * Math.Cos(angleA)));

            eb = BaseFunction.RPToDP(ebF);
            ee = BaseFunction.RPToDP(eeF);
            Pen pen = new Pen(Color, 1.0f);
             g.DrawLine(pen, eb, ee); // 画第1条线

            eb = BaseFunction.RPToDP(ebF2);
            ee = BaseFunction.RPToDP(eeF2);
            g.DrawLine(pen, eb, ee); // 画第2条线
        }

        /// <summary>
        /// 求直线两点倾角(0--2Pi)
        /// </summary>
        /// <param name="startP">起点</param>
        /// <param name="endP">终点</param>
        /// <returns>直线两点倾角(0--2Pi)</returns>
        private double GetAngle(PointF startP, PointF endP)
        {
            double deltX = endP.X - startP.X; // 坐标X增量
            double deltY = endP.X - startP.X; // 坐标Y增量
            double angleA = Math.Atan2(deltY, deltX); // 直线倾角
            if ((deltX < 0 && deltY > 0) || (deltX < 0 && deltY < 0))
                angleA = Math.PI + angleA; // 第2、3象限
            if (deltX > 0 && deltY < 0)
                angleA = 2 * Math.PI + angleA; // 第2、3象限
            return angleA;
        }
    }//endclass
}//endspace
