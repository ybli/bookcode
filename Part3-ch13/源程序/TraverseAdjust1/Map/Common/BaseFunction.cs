using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Map.Common
{
    /*
    * 功能概要：图形操作基础功能类
    * 编号：Map_Common_001
    * 作者：廖振修
    *  创建日期:2016-06-09
    */
    public static class BaseFunction
    {
        public static double viewScale = 1.0; // 比例值
        public static double RX0 = 0.0; // 实际坐标系原点坐标
        public static double RY0 = 0.0; // 实际坐标系原点坐标
        public static double LX0 = 0.0; // 逻辑坐标系原点在页面坐标系下的坐标(以mm为单位)
        public static double LY0 = 0.0; // 逻辑坐标系原点在页面坐标系下的坐标(以mm为单位)
        public static double Kx = 1.0; // 像素到mm的转换比例
        public static double Ky = 1.0; // 像素到mm的转换比例

        /// <summary>
        ///     ''' 实际坐标点转设备坐标点
        ///     ''' </summary>
        ///     ''' <param name="rP">实际坐标点</param>
        ///     ''' <returns>设备坐标点</returns>
        ///     ''' <remarks></remarks>
        public static Point RPToDP(PointF rP)
        {
            Point dP;
            PointF lP;

            lP = RealToLogic(rP);
            dP = LogicToDevice(lP);
            return dP;
        }

        /// <summary>
        ///     ''' 设备坐标点转实际坐标点
        ///     ''' </summary>
        ///     ''' <param name="dP">设备坐标点</param>
        ///     ''' <returns>实际坐标点</returns>
        ///     ''' <remarks></remarks>
        public static PointF DPToRP(Point dP)
        {
            PointF rP;
            PointF lP;

            lP = DeviceToLogic(dP);
            rP = LogicToReal(lP);
            return rP;
        }

        /// <summary>
        ///     ''' 设备坐标距离转实际距离
        ///     ''' </summary>
        ///     ''' <param name="dLen">设备坐标距离</param>
        ///     ''' <returns>实际距离</returns>
        ///     ''' <remarks></remarks>
        public static double DPToRP(int dLen)
        {
            double rLen;

            rLen = dLen * Kx / viewScale;
            return rLen;
        }

        /// <summary>
        ///     ''' 实际坐标点转页面逻辑坐标点
        ///     ''' </summary>
        ///     ''' <param name="rP">实际坐标点</param>
        ///     ''' <returns>页面逻辑坐标点</returns>
        ///     ''' <remarks></remarks>
        private static PointF RealToLogic(PointF rP)
        {
            PointF lP = new PointF();

            lP.X =(float) ((rP.X - RX0) * viewScale);
            lP.Y = (float) ((rP.Y - RY0) * viewScale);

            return lP;
        }

        /// <summary>
        ///     ''' 页面逻辑坐标点转实际坐标点
        ///     ''' </summary>
        ///     ''' <param name="LP">页面逻辑坐标点</param>
        ///     ''' <returns>实际坐标点</returns>
        ///     ''' <remarks></remarks>
        private static PointF LogicToReal(PointF LP)
        {
            PointF rP = new PointF();

            rP.X =(float) ( RX0 + LP.X / viewScale);
            rP.Y =(float) ( RY0 + LP.Y / viewScale);
            return rP;
        }

        /// <summary>
        ///     ''' 页面逻辑坐标点转设备坐标点
        ///     ''' </summary>
        ///     ''' <param name="Lp">页面逻辑坐标点</param>
        ///     ''' <returns>设备坐标点</returns>
        ///     ''' <remarks></remarks>
        private static Point LogicToDevice(PointF Lp)
        {
            Point dP=new Point();

            dP.X = (int) ((Lp.X - LX0) / Kx);
            dP.Y = -(int) ((Lp.Y - LY0) / Ky);
            return dP;
        }

        /// <summary>
        ///     ''' 设备坐标点转逻辑坐标点
        ///     ''' </summary>
        ///     ''' <param name="dP">设备坐标点</param>
        ///     ''' <returns>转逻辑坐标点</returns>
        ///     ''' <remarks></remarks>
        private static PointF DeviceToLogic(Point dP)
        {
            PointF lP=new PointF();
            lP.X = (float) (dP.X * Kx + LX0);
            lP.Y = (float) (-dP.Y * Ky + LY0);

            return lP;
        }
    }
}
