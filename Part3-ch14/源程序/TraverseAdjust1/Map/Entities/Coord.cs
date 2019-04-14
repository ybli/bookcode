using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Map.Common;

namespace Map.Entities
{
    /*
  * 功能概要：坐标系图形对象
  * 编号：Map_Entities_006
  * 作者：廖振修
  *  创建日期:2016-06-09
  */

    public class Coord : GElement
    {
        private PointF mPointSW, mPointNE; // 显示区域的西南点,东北点
        private PointF mOPoint;  // 坐标轴交叉点(实际坐标)
        private float mMaxX, mMaxY; // 坐标轴最大值

        private int mXParts;  // X轴分段数
        private int mYParts;  // Y轴分段数
        private double mLen; // 分段长度


        /// <summary>
        ///带参数构造函数 
        ///</summary>
        ///<param name="pointSW">西南点</param>
        /// <param name="pointNE">东北点</param>
        /// <param name="pointNE">颜色</param>
        public Coord(PointF pointSW, PointF pointNE,Color color)
        {
            this.Color = color;
            mPointSW = new PointF();
            mPointNE = new PointF();
            // 坐标取整10
            mPointSW.X = Convert.ToInt32(pointSW.X);
            mPointSW.Y = Convert.ToInt32(pointSW.Y);
            mPointNE.X = Convert.ToInt32(pointNE.X);
            mPointNE.Y = Convert.ToInt32(pointNE.Y);
        }

        /// <summary>
        ///画坐标系
        /// </summary>
        /// <param name="g">Graphics对象</param>
        public override void Draw(System.Drawing.Graphics g)
        {
            Initl();
            DrawXLine(g);
            DrawYLine(g);
        }

        // 初始化
        private void Initl()
        {
            int parts = 5; // 坐标分段数（坐标范围小的轴)
            PointF startPtF = new PointF(mPointSW.X, mPointSW.Y); // 坐标原点（实际坐标)
            Point startPt = BaseFunction.RPToDP(startPtF); // 中心点（设备坐标)
            PointF endPtF = new PointF(mPointNE.X, mPointNE.Y); // 坐标轴最大点（实际坐标)
            Point endPt = BaseFunction.RPToDP(endPtF); // 坐标轴最大点（设备坐标)

            // 坐标轴范围内缩30个像素
            int offsetVal = 30;
            startPt.X += offsetVal;
            startPt.Y -= offsetVal;
            endPt.X -= offsetVal;
            endPt.Y += offsetVal;
            // 再转回实际坐标
            startPtF = BaseFunction.DPToRP(startPt);
            endPtF = BaseFunction.DPToRP(endPt);

            mOPoint.X =(float)(Math.Ceiling(startPtF.X / (double)10) * 10);
            mOPoint.Y = (float)(Math.Ceiling(startPtF.Y / (double)10) * 10);
            mMaxX = (float)(Math.Ceiling(endPtF.X / (double)10) * 10);
            mMaxY = (float)(Math.Ceiling(endPtF.Y / (double)10) * 10);

            double lenX = endPtF.X - mOPoint.X;
            double lenY = endPtF.Y - mOPoint.Y;
            if (lenX < lenY)
            {
                mXParts = parts;
                mLen = Math.Floor(lenX / (double)(parts * 10)) * 10; // 刻度间隔长度,取整10
                mYParts = (int)Math.Ceiling(lenY / (double)mLen);
            }
            else
            {
                mYParts = parts;
                mLen = Math.Floor(lenY / (double)(parts * 10)) * 10; // 刻度间隔长度,取整10
                mXParts =(int) Math.Ceiling(lenX / (double)mLen);
            }
        }

        /// <summary>
        ///     ''' 画X轴
        ///     ''' </summary>
        ///     ''' <param name="g"></param>
        ///     ''' <param name="parts">分段数</param>
        ///     ''' <remarks></remarks>
        private void DrawXLine(System.Drawing.Graphics g)
        {
            Point startPt = BaseFunction.RPToDP(mOPoint); // 起点（设备坐标)
            PointF endPtF = new PointF(mMaxX, mOPoint.Y);
            Point endPt = BaseFunction.RPToDP(endPtF); // 终点（设备坐标)

            // 画轴
            Pen pen = new Pen(Color, 1.0f);
            SolidBrush brush = new SolidBrush(Color);
            g.DrawLine(pen, startPt, endPt); // 主轴
            g.DrawLine(pen, new Point(endPt.X - 10, endPt.Y - 2), endPt); // 箭头上线
            g.DrawLine(pen, new Point(endPt.X - 10, endPt.Y + 2), endPt); // 箭头下线

            // 画刻度线及刻度值
            PointF ptF1; // 刻度线点(实际坐标)，用于显示刻度值
            Point pt1, pt2; // 刻度线点(设备坐标坐标)，用于画刻度线
            StringFormat drawFormat = new StringFormat(); // 文字样式
            for (int i = 0; i <= mXParts - 1; i++)
            {
                ptF1 = new PointF();
                ptF1.X =(float)( mOPoint.X + mLen * i);
                ptF1.Y = mOPoint.Y;
                pt1 = BaseFunction.RPToDP(ptF1);
                pt2 = new Point(pt1.X, pt1.Y - 4);
                string sx = ptF1.X.ToString("0");
                g.DrawLine(pen, pt1, pt2);
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Near;
                g.DrawString(sx, new Font("宋体", 8.0F), brush, new Point(pt1.X, pt2.Y + 10), drawFormat);
            }
            // drawFormat.Alignment = StringAlignment.Far
            // drawFormat.LineAlignment = StringAlignment.Far
            g.DrawString("X轴", new Font("宋体 ", 10.0F), brush, new Point(endPt.X, endPt.Y + 15), drawFormat);
        }


        /// <summary>
        ///     ''' 画Y轴
        ///     ''' </summary>
        ///     ''' <param name="g"></param>
        ///     ''' <param name="parts">分段数</param>
        ///     ''' <remarks></remarks>
        private void DrawYLine(System.Drawing.Graphics g)
        {
            Point startPt = BaseFunction.RPToDP(mOPoint); // 起点（设备坐标)
            PointF endPtF = new PointF(mOPoint.X, mMaxY);
            Point endPt = BaseFunction.RPToDP(endPtF); // 终点（设备坐标)
            // 画轴
            Pen pen = new Pen(Color, 1.0f);
            SolidBrush brush = new SolidBrush(Color);
            g.DrawLine(pen, startPt, endPt); // 主轴
            g.DrawLine(pen, new Point(endPt.X - 2, endPt.Y + 10), endPt); // 箭头左线
            g.DrawLine(pen, new Point(endPt.X + 2, endPt.Y + 10), endPt); // 箭头右线

            // 画刻度线及刻度值
            PointF ptF1; // 刻度线点(实际坐标)，用于显示刻度值
            Point pt1, pt2; // 刻度线点(设备坐标坐标)，用于画刻度线
            StringFormat drawFormat = new StringFormat(); // 文字样式
            for (int i = 0; i <= mYParts - 1; i++)
            {
                ptF1 = new PointF();
                ptF1.X = mOPoint.X;
                ptF1.Y = (float)(mOPoint.Y + mLen * i);
                pt1 = BaseFunction.RPToDP(ptF1);
                pt2 = new Point(pt1.X + 4, pt1.Y);
                string sx = ptF1.Y.ToString("0");
                g.DrawLine(pen, pt1, pt2);
                drawFormat.Alignment = StringAlignment.Near;
                drawFormat.LineAlignment = StringAlignment.Near;
                g.DrawString(sx, new Font("宋体", 8.0F), brush, new Point(pt1.X + 2, pt2.Y - 10), drawFormat);
            }
            drawFormat.Alignment = StringAlignment.Far;
            drawFormat.LineAlignment = StringAlignment.Far;
            g.DrawString("Y轴", new Font("宋体 ", 10.0F), brush, new Point(endPt.X - 2, endPt.Y), drawFormat);
        }
    }//endclass
}//endspace
