using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/********************************************************************************
** auth： 金蕾
** dire:  张金亭
** date： 2018/12/27
** desc： 示意图绘制视图
** Ver.:  1.0
*********************************************************************************/

/// <summary>
/// 示意图窗口,可以绘制高斯坐标下和大地坐标下的示意图,并可将其转出为DXF
/// </summary>
namespace CalculationOfControlArea
{
    public partial class ImageForm : Form
    {
        #region 定义相关结构体
        /// <summary>
        /// 屏幕线结构体,坐标为屏幕坐标,(x1,y1)为起点,(x2,y2)为终点,用于绘制DXF图形
        /// </summary>
        public struct Line
        {
            public double x1;
            public double y1;
            public double x2;
            public double y2;

            public Line(double x1, double y1, double x2, double y2)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }
        }

        /// <summary>
        /// 屏幕点结构体,坐标为屏幕坐标,用于绘制DXF图形
        /// </summary>
        public struct Point
        {
            public double x;
            public double y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// 屏幕注记结构体，位置为屏幕坐标用于输出DXF图形
        /// </summary>
        public struct Memo
        {
            public string text;
            public double x;
            public double y;

            public Memo(string text, double x, double y)
            {
                this.text = text;
                this.x = x;
                this.y = y;
            }
        }
        #endregion

        #region 储存绘制的数据
        /// <summary>
        /// 高斯坐标图中的线段集合
        /// </summary>
        private List<Line> gaussLines = new List<Line>();
        /// <summary>
        /// 高斯坐标图中的点集合
        /// </summary>
        private List<Point> gaussPoints = new List<Point>();
        /// <summary>
        /// 高斯坐标图中的注记集合
        /// </summary>
        private List<Memo> gaussMemos = new List<Memo>();
        /// <summary>
        /// 大地坐标图中的线段集合
        /// </summary>
        private List<Line> geoLines = new List<Line>();
        /// <summary>
        /// 大地坐标图中的点集合
        /// </summary>
        private List<Point> geoPoints = new List<Point>();
        /// <summary>
        /// 大地坐标图中的注记集合
        /// </summary>
        private List<Memo> geoMemos = new List<Memo>();
        #endregion

        /// <summary>
        /// 高斯坐标图中移动的起始坐标
        /// </summary>
        private Point startPoint1 = new Point(-1, -1);
        /// <summary>
        /// 大地坐标图中移动的起始坐标
        /// </summary>
        private Point startPoint2 = new Point(-1, -1);

        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 关闭时隐藏
        /// </summary>
        private void ImageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        #region 移动事件相关响应
        /// <summary>
        /// 在图1中按下鼠标,记录初始点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gaussPic_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint1 = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 判断是否移动,若移动还要清空初始点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gaussPic_MouseUp(object sender, MouseEventArgs e)
        {
            if (MainForm.isMove)
            {
                if (startPoint1.x != -1 && startPoint1.y != -1)
                {
                    this.gaussPic.Left += (int)(e.X - startPoint1.x);
                    this.gaussPic.Top += (int)(e.Y - startPoint1.y);
                    startPoint1.x = -1;
                    startPoint1.y = -1;
                }
            }
        }

        /// <summary>
        /// 在图2中按下鼠标,记录初始点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void geoPic_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint2 = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 判断是否移动,若移动还要清空初始点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void geoPic_MouseUp(object sender, MouseEventArgs e)
        {
            if (MainForm.isMove)
            {
                if (startPoint2.x != -1 && startPoint2.y != -1)
                {
                    this.geoPic.Left += (int)(e.X - startPoint2.x);
                    this.geoPic.Top += (int)(e.Y - startPoint2.y);
                    startPoint2.x = -1;
                    startPoint2.y = -1;
                }
            }
        }
        #endregion

        #region 绘制相关响应
        /*
        private double getMinX(List<BPoint> points)
        {
            double minX = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < minX && points[i].Y != 0)
                {
                    minX = points[i].Y;
                }
            }
            return minX;
        }
        private double getMinY(List<BPoint> points)
        {
            double minY = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < minY && points[i].X != 0)
                {
                    minY = points[i].X;
                }
            }
            return minY;
        }*/
        /*
        private void deleteZero(List<BPoint> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X == 0 || points[i].Y == 0)
                {
                    points.RemoveAt(i);
                    i--;
                }
            }
        }
         * */
        /// <summary>
        /// 绘制高斯坐标系下的示意图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gaussPic_Paint(object sender, PaintEventArgs e)
        {
            double height = gaussPic.Height;
            double width = gaussPic.Width;
            if (MainForm.canPaintGauss)
            {
                Graphics graphics = e.Graphics;
                double maxX = 0;
                double maxY = 0;
                double minX = double.MaxValue;
                double minY = double.MaxValue;
                AdminPolygon polygon = null;
                for (int i = 0; i < MainForm.Polygons.Count; i++)
                {
                    polygon = MainForm.Polygons[i];
                    if (polygon.BPoints2.Count == 0)
                    {
                        continue;
                    }
                    if (polygon.BPoints2.Max(x => x.Y) + 800 > maxX)
                    {
                        maxX = polygon.BPoints2.Max(x => x.Y) + 800;
                    }
                    if (polygon.BPoints2.Max(x => x.X) + 800 > maxY)
                    {
                        maxY = polygon.BPoints2.Max(x => x.X) + 800;
                    }
                    if (polygon.BPoints2.Min(x => x.Y) - 800 < minX)
                    {
                        minX = polygon.BPoints2.Min(x => x.Y) - 800;
                    }
                    if (polygon.BPoints2.Min(x => x.X) - 800 < minY)
                    {
                        minY = polygon.BPoints2.Min(x => x.X) - 800;
                    }
                }
                double scaleX = (width - 50) / (maxX - minX);
                double scaleY = (height - 50) / (maxY - minY);
                for (int i = 0; i < MainForm.Polygons.Count; i++)
                {
                    Pen pen;
                    Brush brush;
                    pen = Pens.Blue;
                    brush = Brushes.Blue;

                    polygon = MainForm.Polygons[i];
                    if (polygon.BPoints2.Count == 0)
                    {
                        continue;
                    }
                    for (int j = 0; j < polygon.BPoints2.Count; j++)
                    {
                        double x = (polygon.BPoints2[j].Y - minX) * scaleX + 25;
                        double y = height - (polygon.BPoints2[j].X - minY) * scaleY - 25;
                        gaussPoints.Add(new Point(x, y));
                        graphics.DrawEllipse(pen, (float)(x - 0.5), (float)(y - 0.5), 1, 1);
                        if (j != polygon.BPoints2.Count - 1)
                        {
                            double x1 = (polygon.BPoints2[j + 1].Y - minX) * scaleX + 25;
                            double y1 = height - (polygon.BPoints2[j + 1].X - minY) * scaleY - 25;
                            graphics.DrawLine(pen, (float)x, (float)y, (float)x1, (float)y1);
                            gaussLines.Add(new Line(x, y, x1, y1));
                        }
                    }

                    double x2 = (polygon.BPoints2.Average(x => x.Y) - minX) * scaleX + 25;
                    double y2 = height - (polygon.BPoints2.Average(x => x.X) - minY) * scaleY - 25;
                    graphics.DrawString(polygon.Code, new Font("宋体", 10), brush, (float)(x2 - 10), (float)y2);
                    gaussMemos.Add(new Memo(polygon.Code, x2, y2));
                }
                graphics.DrawLine(Pens.Black, (float)25, (float)25, (float)25, (float)height - 25);
                graphics.DrawLine(Pens.Black, (float)width - 25, (float)height - 25, (float)25, (float)height - 25);
                graphics.DrawLine(Pens.Black, (float)25, (float)25, (float)20, (float)30);
                graphics.DrawLine(Pens.Black, (float)25, (float)25, (float)30, (float)30);
                graphics.DrawLine(Pens.Black, (float)width - 25, (float)height - 25, (float)width - 35, (float)height - 20);
                graphics.DrawLine(Pens.Black, (float)width - 25, (float)height - 25, (float)width - 35, (float)height - 30);
                graphics.DrawString(((int)minY).ToString(), new Font("宋体", 10), Brushes.Black, (float)5, (float)height - 40);
                graphics.DrawString(((int)maxX).ToString(), new Font("宋体", 10), Brushes.Black, (float)width - 45, (float)height - 17);
                graphics.DrawString(((int)minX).ToString(), new Font("宋体", 10), Brushes.Black, (float)25, (float)height - 17);
                graphics.DrawString(((int)maxY).ToString(), new Font("宋体", 10), Brushes.Black, (float)5, (float)15);
            }
        }

        /// <summary>
        /// 绘制大地坐标系下的示意图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void geoPic_Paint(object sender, PaintEventArgs e)
        {
            double height = geoPic.Height;
            double width = geoPic.Width;
            if (MainForm.canPaintGeo)
            {
                Graphics graphics = e.Graphics;
                double maxX = 0;
                double maxY = 0;
                double minX = double.MaxValue;
                double minY = double.MaxValue;
                AdminPolygon polygon = null;
                for (int i = 0; i < MainForm.Polygons.Count; i++)
                {
                    polygon = MainForm.Polygons[i];
                    if (polygon.BPoints2.Count == 0)
                    {
                        continue;
                    }
                    if (polygon.BPoints2.Max(x => x.L) + 0.0002 > maxX)
                    {
                        maxX = polygon.BPoints2.Max(x => x.L) + 0.0002;
                    }
                    if (polygon.BPoints2.Max(x => x.B) + 0.0002 > maxY)
                    {
                        maxY = polygon.BPoints2.Max(x => x.B) + 0.0002;
                    }
                    if (polygon.BPoints2.Min(x => x.L) - 0.0002 < minX)
                    {
                        minX = polygon.BPoints2.Min(x => x.L) - 0.0002;
                    }
                    if (polygon.BPoints2.Min(x => x.B) - 0.0002 < minY)
                    {
                        minY = polygon.BPoints2.Min(x => x.B) - 0.0002;
                    }
                }
                double scaleX = (width - 50) / (maxX - minX);
                double scaleY = (height - 50) / (maxY - minY);
                double latdiffer = 0, londiffer = 0;
                Tool.SetLatAndLonDif(MainForm.MeaScale, ref latdiffer, ref londiffer);
                latdiffer = latdiffer * Math.PI / 180;
                londiffer = londiffer * Math.PI / 180;
                for (double i = Math.Ceiling(minX / latdiffer) * latdiffer; i < maxX; i = i + londiffer)
                {
                    double x1 = (i - minX) * scaleX + 25;
                    double y1 = height - (maxY - minY) * scaleY - 25;
                    graphics.DrawLine(Pens.Yellow, (float)x1, (float)y1, (float)x1, (float)(height - 25));
                    geoLines.Add(new Line(x1, y1, x1, height - 25));
                }
                for (double i = Math.Ceiling(minY / londiffer) * londiffer; i < maxY; i = i + latdiffer)
                {
                    double x1 = (maxX - minX) * scaleX + 25;
                    double y1 = height - (i - minY) * scaleY - 25;
                    graphics.DrawLine(Pens.Yellow, (float)x1, (float)y1, (float)25, (float)y1);
                    geoLines.Add(new Line(x1, y1, 25, y1));
                }
                for (int i = 0; i < MainForm.Polygons.Count; i++)
                {
                    Pen pen;
                    Brush brush;
                    pen = Pens.Blue;
                    brush = Brushes.Blue;
                    polygon = MainForm.Polygons[i];
                    if (i == 0 && polygon.MapSheet.WSPoint1!=null)
                    {
                        double x = (polygon.MapSheet.WSPoint1.L - minX) * scaleX + 25;
                        double y = height - (polygon.MapSheet.WSPoint1.B - minY) * scaleY - 25;
                        gaussPoints.Add(new Point(x, y));
                        double x2 = (polygon.MapSheet.WSPoint1.L + londiffer - minX) * scaleX + 25;
                        double y2 = height - (polygon.MapSheet.WSPoint1.B - minY) * scaleY - 25;
                        gaussPoints.Add(new Point(x2, y2));
                        double x3 = (polygon.MapSheet.ENPoint1.L - minX) * scaleX + 25;
                        double y3 = height - (polygon.MapSheet.ENPoint1.B - minY) * scaleY - 25;
                        gaussPoints.Add(new Point(x3, y3));
                        double x4 = (polygon.MapSheet.WSPoint1.L - minX) * scaleX + 25;
                        double y4 = height - (polygon.MapSheet.WSPoint1.B + latdiffer - minY) * scaleY - 25;
                        gaussPoints.Add(new Point(x4, y4));
                        graphics.DrawLines(Pens.DarkMagenta, new PointF[5] { new PointF((float)x, (float)y), new PointF((float)x2, (float)y2), new PointF((float)x3, (float)y3), new PointF((float)x4, (float)y4), new PointF((float)x, (float)y)});

                    }
                    if (polygon.BPoints2.Count == 0)
                    {
                        continue;
                    }
                    for (int j = 0; j < polygon.BPoints2.Count; j++)
                    {
                        double x = (polygon.BPoints2[j].L - minX) * scaleX + 25;
                        double y = height - (polygon.BPoints2[j].B - minY) * scaleY - 25;
                        gaussPoints.Add(new Point(x, y));
                        graphics.DrawEllipse(pen, (float)(x - 0.5), (float)(y - 0.5), 1, 1);
                        if (j != polygon.BPoints2.Count - 1)
                        {
                            double x1 = (polygon.BPoints2[j + 1].L - minX) * scaleX + 25;
                            double y1 = height - (polygon.BPoints2[j + 1].B - minY) * scaleY - 25;
                            graphics.DrawLine(pen, (float)x, (float)y, (float)x1, (float)y1);
                            geoLines.Add(new Line(x, y, x1, y1));
                        }
                    }
                    double x5 = (polygon.BPoints2.Average(x => x.L) - minX) * scaleX + 25;
                    double y5 = height - (polygon.BPoints2.Average(x => x.B) - minY) * scaleY - 25;
                    graphics.DrawString(polygon.Code, new Font("宋体", 10), brush, (float)(x5 - 10), (float)y5);
                    geoMemos.Add(new Memo(polygon.Code, x5, y5));
                }

                graphics.DrawLine(Pens.Black, (float)25, (float)25, (float)25, (float)height - 25);
                graphics.DrawLine(Pens.Black, (float)width - 25, (float)height - 25, (float)25, (float)height - 25);
                graphics.DrawLine(Pens.Black, (float)25, (float)25, (float)20, (float)30);
                graphics.DrawLine(Pens.Black, (float)25, (float)25, (float)30, (float)30);
                graphics.DrawLine(Pens.Black, (float)width - 25, (float)height - 25, (float)width - 35, (float)height - 20);
                graphics.DrawLine(Pens.Black, (float)width - 25, (float)height - 25, (float)width - 35, (float)height - 30);
                graphics.DrawString((minY).ToString("F4"), new Font("宋体", 10), Brushes.Black, (float)5, (float)height - 40);
                graphics.DrawString((maxX).ToString("F4"), new Font("宋体", 10), Brushes.Black, (float)width - 45, (float)height - 17);
                graphics.DrawString((minX).ToString("F4"), new Font("宋体", 10), Brushes.Black, (float)25, (float)height - 17);
                graphics.DrawString((maxY).ToString("F4"), new Font("宋体", 10), Brushes.Black, (float)5, (float)15);
            }
        }

        /// <summary>
        /// 形状改变时重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gaussPic_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// 形状改变时重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void geoPic_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
        #endregion

        /// <summary>
        /// 输出高斯坐标系下的示意图
        /// </summary>
        /// <param name="streamWriter"></param>
        public void ToDxfGauss(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("0");
            streamWriter.WriteLine("SECTION");
            streamWriter.WriteLine("2");
            streamWriter.WriteLine("ENTITIES");


            foreach (var i in gaussPoints)
            {
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("POINT");
                streamWriter.WriteLine("8");
                streamWriter.WriteLine("Points");
                streamWriter.WriteLine("10");
                streamWriter.WriteLine(i.x);
                streamWriter.WriteLine("20");
                streamWriter.WriteLine(i.y);
            }
            foreach (var i in gaussMemos)
            {
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("TEXT");
                streamWriter.WriteLine("8");
                streamWriter.WriteLine("Memos");
                streamWriter.WriteLine("10");
                streamWriter.WriteLine(i.x);
                streamWriter.WriteLine("20");
                streamWriter.WriteLine(i.y);
                streamWriter.WriteLine("40");
                streamWriter.WriteLine(1);
                streamWriter.WriteLine("1");
                streamWriter.WriteLine(i.text);
            }
            foreach (var i in gaussLines)
            {
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("LINE");
                streamWriter.WriteLine("8");
                streamWriter.WriteLine("Lines");
                streamWriter.WriteLine("10");
                streamWriter.WriteLine(i.x1);
                streamWriter.WriteLine("20");
                streamWriter.WriteLine(i.y1);
                streamWriter.WriteLine("11");
                streamWriter.WriteLine(i.x2);
                streamWriter.WriteLine("21");
                streamWriter.WriteLine(i.y2);
            }
            streamWriter.WriteLine("0");
            streamWriter.WriteLine("ENDSEC");
            streamWriter.WriteLine("0");
            streamWriter.WriteLine("EOF");

            streamWriter.Close();
        }

        /// <summary>
        /// 输出大地坐标系下的示意图
        /// </summary>
        /// <param name="streamWriter"></param>
        public void ToDxfGeo(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("0");
            streamWriter.WriteLine("SECTION");
            streamWriter.WriteLine("2");
            streamWriter.WriteLine("ENTITIES");


            foreach (var i in geoPoints)
            {
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("POINT");
                streamWriter.WriteLine("8");
                streamWriter.WriteLine("Points");
                streamWriter.WriteLine("10");
                streamWriter.WriteLine(i.x);
                streamWriter.WriteLine("20");
                streamWriter.WriteLine(i.y);
            }
            foreach (var i in geoMemos)
            {
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("TEXT");
                streamWriter.WriteLine("8");
                streamWriter.WriteLine("Memos");
                streamWriter.WriteLine("10");
                streamWriter.WriteLine(i.x);
                streamWriter.WriteLine("20");
                streamWriter.WriteLine(i.y);
                streamWriter.WriteLine("40");
                streamWriter.WriteLine(1);
                streamWriter.WriteLine("1");
                streamWriter.WriteLine(i.text);
            }
            foreach (var i in geoLines)
            {
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("LINE");
                streamWriter.WriteLine("8");
                streamWriter.WriteLine("Lines");
                streamWriter.WriteLine("10");
                streamWriter.WriteLine(i.x1);
                streamWriter.WriteLine("20");
                streamWriter.WriteLine(i.y1);
                streamWriter.WriteLine("11");
                streamWriter.WriteLine(i.x2);
                streamWriter.WriteLine("21");
                streamWriter.WriteLine(i.y2);
            }
            streamWriter.WriteLine("0");
            streamWriter.WriteLine("ENDSEC");
            streamWriter.WriteLine("0");
            streamWriter.WriteLine("EOF");

            streamWriter.Close();
        }
    }
}
