using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/********************************************************************************
** auth： Jin
** dire:  张金亭
** date： 2019/01/13
** desc： 图幅类
** Ver.:  1.0
*********************************************************************************/
namespace CalculationOfControlArea
{
    class MapSheet
    {
        #region 成员变量及setter,getter方法
        /// <summary>
        /// 图幅编号
        /// </summary>
        private string sheetNum = "";
        /// <summary>
        /// 图幅包含行政区域的点
        /// </summary>
        private List<BPoint> bPoints = new List<BPoint>();
        /// <summary>
        /// 图幅理论面积
        /// </summary>
        private double theoryArea;
        /// <summary>
        /// 多边形在图幅内的区域的计算面积
        /// </summary>
        private double calArea = 0;
        /// <summary>
        /// 计算多边形面积时选定的起始经线，经度值应比所有坐标点的最小经度更小
        /// </summary>
        private double l0 = -1;
        /// <summary>
        /// 图幅西南点
        /// </summary>
        private BPoint WSPoint;
        /// <summary>
        /// 图幅东北点
        /// </summary>
        private BPoint ENPoint;

        internal BPoint ENPoint1 { get { return ENPoint; } set { ENPoint = value; } }

        public double TheoryArea { get { return theoryArea; } set { theoryArea = value; } }
        public double CalArea { get { return calArea; } set { calArea = value; } }

        internal BPoint WSPoint1 { get { return WSPoint; } set { WSPoint = value; } }
        public string SheetNum { get { return sheetNum; } set { sheetNum = value; } }
        internal List<BPoint> BPoints { get { return bPoints; } set { bPoints = value; } }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sheetNum">图幅编号</param>
        public MapSheet(string sheetNum)
        {
            this.sheetNum = sheetNum;
        }

        /// <summary>
        /// 图幅理论面积计算
        /// </summary>
        /// <returns></returns>
        public double SheetTheoryArea()
        {
            double dl, bm, db, a0, b0, e2, s;
            double a, b, c, d, e, b1, l1, b2, l2;
            b1 = WSPoint.B;
            l1 = WSPoint.L;
            b2 = ENPoint.B;
            l2 = ENPoint.L;
            //东西图廓经差，单位：分
            dl = Math.Abs(l2 - l1) * 180 * 60 / Math.PI;
            bm = (b1 + b2) / 2.0;
            //南北图廓纬差，单位：弧度
            db = Math.Abs(b2 - b1) / 2.0;
            //CGCS2000椭球参数
            //a0 = 6378137;
            //b0 = 6356752;
            //1980西安椭球参数
            a0 = 6378140;
            b0 = 6356755.29;
            e2 = (a0 * a0 - b0 * b0) / (a0 * a0);
            a = 1 + (3.0 / 6.0) * e2 + (30.0 / 80.0) * e2 * e2 + (35.0 / 112.0) * e2 * e2 * e2
                + (630.0 / 2304.0) * e2 * e2 * e2 * e2;
            b = (1.0 / 6.0) * e2 + (15.0 / 80.0) * e2 * e2 + (21.0 / 112.0) * e2 * e2 * e2
                + (420.0 / 2304.0) * e2 * e2 * e2 * e2;
            c = (3.0 / 80.0) * e2 * e2 + (7.0 / 112.0) * e2 * e2 * e2
                + (180.0 / 2304.0) * e2 * e2 * e2 * e2;
            d = (1.0 / 112.0) * e2 * e2 * e2
                + (45.0 / 2304.0) * e2 * e2 * e2 * e2;
            e = (5.0 / 2304.0) * e2 * e2 * e2 * e2;
            s = (4 * Math.PI / (360 * 60)) * b0 * b0 * dl * (a * Math.Sin(db) * Math.Cos(bm) - b * Math.Sin(3 * db) * Math.Cos(3 * bm)
                + c * Math.Sin(5 * db) * Math.Cos(5 * bm) - d * Math.Sin(7 * db) * Math.Cos(7 * bm)
                + e * Math.Sin(9 * db) * Math.Cos(9 * bm));
            this.theoryArea = s;
            return s;
        }

        /// <summary>
        /// 计算角点
        /// </summary>
        /// <param name="latDiffer"></param>
        /// <param name="lonDiffer"></param>
        public void CalculateSheetPoints(double latDiffer, double lonDiffer)
        {
            char[] alpha = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' };
            string row = sheetNum.Substring(0, 1);
            int a = 0, b = 0, c = 0, d = 0;
            for (int i = 0; i < 22; i++)
            {
                if (row.Equals(alpha[i].ToString()))
                {
                    a = i + 1;
                }
            }
            b = int.Parse(sheetNum.Substring(1, 2));
            double latitude = (a - 1) * 4;
            double longitude = (b - 31) * 6;
            if (sheetNum.Length > 3)
            {
                c = int.Parse(sheetNum.Substring(4, 3));
                d = int.Parse(sheetNum.Substring(7, 3));
                latitude = (a - 1) * 4 + (4 / latDiffer - c) * latDiffer;
                longitude = (b - 31) * 6 + (d - 1) * lonDiffer;
            }
            //计算西南角的经纬度坐标
            //经纬度，单位：度
            //转为弧度
            latitude = latitude * Math.PI / 180;
            longitude = longitude * Math.PI / 180;
            latDiffer = latDiffer * Math.PI / 180;
            lonDiffer = lonDiffer * Math.PI / 180;
            WSPoint = new BPoint(latitude, longitude);
            ENPoint = new BPoint(latitude + latDiffer, lonDiffer + longitude);
        }
        /// <summary>
        /// 根据点集数据计算L0
        /// </summary>
        private void FindL0()
        {
            l0 = this.bPoints.Min(x => x.L) - 0.001;
        }

        /// <summary>
        /// 计算椭球上的梯形图块的面积，以对象的l0变量为基准
        /// </summary>
        /// <param name="b1">第一点大地坐标B</param>
        /// <param name="l1">第一点大地坐标L</param>
        /// <param name="b2">第二点大地坐标B</param>
        /// <param name="l2">第二点大地坐标L</param>
        /// <returns>little梯形图块面积</returns>
        private double EchelonAreaCalculate(double b1, double l1, double b2, double l2)
        {
            double dl, bm, db, a0, b0, e2, s;
            double a, b, c, d, e;
            dl = (l1 + l2) / 2.0 - l0;
            bm = (b1 + b2) / 2.0;
            db = (b2 - b1) / 2.0;
            //CGCS2000椭球参数
            //a0 = 6378137;
            //b0 = 6356752;
            //1980西安椭球参数
            a0 = 6378140;
            b0 = 6356755.29;
            e2 = (a0 * a0 - b0 * b0) / (a0 * a0);
            a = 1 + (3.0 / 6.0) * e2 + (30.0 / 80.0) * e2 * e2 + (35.0 / 112.0) * e2 * e2 * e2
                + (630.0 / 2304.0) * e2 * e2 * e2 * e2;
            b = (1.0 / 6.0) * e2 + (15.0 / 80.0) * e2 * e2 + (21.0 / 112.0) * e2 * e2 * e2
                + (420.0 / 2304.0) * e2 * e2 * e2 * e2;
            c = (3.0 / 80.0) * e2 * e2 + (7.0 / 112.0) * e2 * e2 * e2
                + (180.0 / 2304.0) * e2 * e2 * e2 * e2;
            d = (1.0 / 112.0) * e2 * e2 * e2
                + (45.0 / 2304.0) * e2 * e2 * e2 * e2;
            e = (5.0 / 2304.0) * e2 * e2 * e2 * e2;
            s = 2 * b0 * b0 * dl * (a * Math.Sin(db) * Math.Cos(bm) - b * Math.Sin(3 * db) * Math.Cos(3 * bm)
                + c * Math.Sin(5 * db) * Math.Cos(5 * bm) - d * Math.Sin(7 * db) * Math.Cos(7 * bm)
                + e * Math.Sin(9 * db) * Math.Cos(9 * bm));
            return s;
        }
        /// <summary>
        /// 计算行政区域在此图幅内的面积
        /// </summary>
        /// <returns></returns>
        public double AreaCalculate()
        {
            if (this.bPoints.Count == 0)
            {
                this.calArea = 0;
                return this.calArea;
            }
            Tool.CheckClose(this.bPoints);
            FindL0();
            double area = 0;
            for (int i = 0; i < bPoints.Count - 1; i++)
            {
                area += EchelonAreaCalculate(bPoints[i].B, bPoints[i].L, bPoints[i + 1].B, bPoints[i + 1].L);
            }
            area = Math.Abs(area);
            this.calArea = area;
            return area;
        }
    }
}
