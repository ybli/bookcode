using System;
using System.Collections.Generic;
using System.Linq;

/********************************************************************************
** auth： 金蕾
** dire:  张金亭
** date： 2018/12/27
** desc： 行政区域多边形类
** Ver.:  1.0
*********************************************************************************/

namespace CalculationOfControlArea
{
    /// <summary>
    /// 行政区域多边形类，用于储存多边形边界数据，并进行相关计算
    /// </summary>
    class AdminPolygon
    {

        #region 成员变量及setter,getter
        /// <summary>
        /// 该行政区域代码
        /// </summary>
        private string code = "";
        /// <summary>
        /// 该行政区域的在图幅内的所有点
        /// </summary>
        private List<BPoint> bPoints = new List<BPoint>();
        /// <summary>
        /// 该行政区域的所有边界点
        /// </summary>
        private List<BPoint> bPoints2 = new List<BPoint>();

        
        /// <summary>
        /// 该行政区域在图幅内的面积
        /// </summary>
        private double area = 0;
        /// <summary>
        /// 记录平差时为该区域配赋的值
        /// </summary>
        private double dArea = 0;
        /// <summary>
        /// 记录平差后的面积值
        /// </summary>
        private double areaAfterControl = 0;
        /// <summary>
        /// 破图幅面积
        /// </summary>
        private double brokenarea = 0;
        /// <summary>
        /// 储存图幅
        /// </summary>
        private MapSheet mapSheet = new MapSheet("");

        internal List<BPoint> BPoints2
        {
            get { return bPoints2; }
            set { bPoints2 = value; }
        }
        internal MapSheet MapSheet
        {
            get { return mapSheet; }
            set { mapSheet = value; }
        }
        public double Brokenarea { get { return brokenarea; } set { brokenarea = value; } }
        //public string Code { get => code; set => code = value; }
        public string Code { get { return code; } set { code = value; } }
        //public double Area { get => area; set => area = value; }
        public double Area { get { return area; } set { area = value; } }
        //internal List<BPoint> BPoints { get => bPoints; }
        internal List<BPoint> BPoints { get { return bPoints; } }
        //public double DArea { get => dArea; set => dArea = value; }
        public double DArea { get { return dArea; } set { dArea = value; } }
        //public double AreaAfterControl { get => areaAfterControl; set => areaAfterControl = value; }
        public double AreaAfterControl { get { return areaAfterControl; } set { areaAfterControl = value; } }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        public AdminPolygon(string code)
        {
            this.code = code;
        }
        #region 多边形操作


        /// <summary>
        /// 对行政区域的每条边上的所有点的坐标进行坐标反算
        /// </summary>
        public void AdverseCalculate()
        {
            for (int i = 0; i < bPoints.Count; i++)
            {
                bPoints[i].XYtoBL();
            }
        }


        /// <summary>
        /// 求相交区域的面积以及图幅标准面积
        /// </summary>
        public void IntercectArea()
        {
            for (int i = 1; i <= 4; i++)
            {
                ClipEdge(i);
            }
            //计算图幅面积
            mapSheet.AreaCalculate();
            //计算图幅理论面积
            MainForm.SheetArea = mapSheet.SheetTheoryArea();
        }

        /// <summary>
        /// 判断点是否在图幅内
        /// </summary>
        /// <param name="edge">边代号</param>
        /// <param name="pt">某点</param>
        /// <returns></returns>
        public bool InSide(int edge, BPoint pt)
        {
            double latdiffer = 0, londiffer = 0;
            Tool.SetLatAndLonDif(MainForm.MeaScale, ref latdiffer, ref londiffer);
            mapSheet.CalculateSheetPoints(latdiffer, londiffer);
            double minx, miny, maxx, maxy;
            minx = mapSheet.WSPoint1.L;
            maxx = mapSheet.ENPoint1.L;
            miny = mapSheet.WSPoint1.B;
            maxy = mapSheet.ENPoint1.B;
            if (edge == 1 && pt.L < minx || edge == 3 && pt.L > maxx || edge == 2 && pt.B < miny || edge == 4 && pt.B > maxy)
                return false;
            return true;
        }

        /// <summary>
        /// 求图幅与区域交点
        /// </summary>
        /// <param name="edge">边代号</param>
        /// <param name="p1">起点</param>
        /// <param name="p2">终点</param>
        /// <returns></returns>
        public BPoint FindIntersection(int edge, BPoint p1, BPoint p2)
        {
            double B=0, L=0;
            double minx, miny, maxx, maxy;
            minx = mapSheet.WSPoint1.L;
            maxx = mapSheet.ENPoint1.L;
            miny = mapSheet.WSPoint1.B;
            maxy = mapSheet.ENPoint1.B;
            if (edge == 1)
            {
                L = minx;
                B = p2.B + (p2.B - p1.B) * (minx - p2.L) / (p2.L - p1.L);
            }
            else if (edge == 3)
            {
                L = maxx;
                B = p2.B + (p2.B - p1.B) * (maxx - p2.L) / (p2.L - p1.L);
            }
            else if (edge == 2)
            {
                B = miny;
                L = p2.L + (miny - p2.B) * (p2.L - p1.L) / (p2.B - p1.B);
            }
            else if (edge == 4)
            {
                B = maxy;
                L = p2.L + (maxy - p2.B) * (p2.L - p1.L) / (p2.B - p1.B);
            }
            return new BPoint(B,L);
        }


        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="edge">边的带号,1、2、3、4分别代表左、下、右、上</param>
        private void ClipEdge(int edge)
        {
            bool isP1In, isP2In;
            int indexOfPolygon = bPoints.Count;
            List<BPoint> polygonTemp = new List<BPoint>();
            for (int i = 0; i < indexOfPolygon; i++)
            {
                isP1In = InSide(edge,bPoints[i]);
                isP2In = InSide(edge, bPoints[(i + 1) % indexOfPolygon]);
                if (isP1In && isP2In)
                    polygonTemp.Add(bPoints[(i + 1) % indexOfPolygon]);
                else if (isP1In)
                    polygonTemp.Add(FindIntersection(edge, bPoints[i], bPoints[(i + 1) % indexOfPolygon]));
                else if (isP2In)
                {
                    polygonTemp.Add(FindIntersection(edge, bPoints[i], bPoints[(i + 1) % indexOfPolygon]));
                    polygonTemp.Add(bPoints[(i + 1) % indexOfPolygon]);
                }
            }
            indexOfPolygon = polygonTemp.Count;
            bPoints.Clear();
            for (int i = 0; i < indexOfPolygon; i++)
                bPoints.Add(polygonTemp[i]);
            this.mapSheet.BPoints = bPoints;
        }
        #endregion
    }
}