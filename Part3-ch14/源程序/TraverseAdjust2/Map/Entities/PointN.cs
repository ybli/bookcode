using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Map.Common;

namespace Map.Entities
{
    /*
    * 功能概要：已知点图元
    * 编号：Map_Entities_002
    * 作者：廖振修
    *  创建日期:2016-06-09
    */
    public class PointN : GElement
    {
        public PointF Point;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pName">名称</param>
        /// <param name="point">对象</param>
        /// <param name="color">颜色</param>
        public PointN(string pName,PointF point,Color color )
        {
            Point = point;
            this.Name = pName;
            this.Color = color;
        }

        /// <summary>
        ///     ''' 画已知点
        ///     ''' </summary>
        ///     ''' <param name="g">Graphics对象</param>
        ///     ''' <remarks></remarks>
        public override void Draw(System.Drawing.Graphics g)
        {
            PointF centerPtF = new PointF(Point.X, Point.Y); // 中心点（实际坐标)
            Point centerPt = BaseFunction.RPToDP(centerPtF); // 中心点（设备坐标)

            SolidBrush brush = new SolidBrush(Color);
            g.FillRectangle(brush, centerPt.X - 5, centerPt.Y - 5, 10, 10); // 已半径2像素的正方形代表点
            g.DrawString(Name, new Font("宋体", 20), brush, centerPt); // 画点名
        }
    }
}//endspace
