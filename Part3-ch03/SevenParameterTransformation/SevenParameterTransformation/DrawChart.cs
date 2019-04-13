using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
namespace SevenParameterTransformation
{
    class DrawChart//画图的类
    {
        public static void GetGraph(List<Point> knownPoints, List<Point> unknownPoints, Chart chart)
        {
            List<Point> points = new List<Point>();
            points.AddRange(knownPoints);
            points.AddRange(unknownPoints);

            chart.Series.Clear();
            AxisRange(chart, points);
            Series series;
            for (int i = 0; i < points.Count; i++)
            {
                series = new Series();
                series.ChartType = SeriesChartType.Point;
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerColor = Color.Red;
                series.MarkerSize = 8;
                series.Label = points[i].name;
                series.Points.AddXY(points[i].Y1, points[i].X1);
                chart.Series.Add(series);

                chart.ChartAreas[0].AxisX.Title = "Y(m)";
                chart.ChartAreas[0].AxisY.Title = "X(m)";
            }
            chart.DataBind();
        }


        private static void AxisRange(Chart chart, List<Point> points)
        {
            double xMax = points.Max(u => u.X1);
            double yMax = points.Max(u => u.Y1);
            double xMin = points.Min(u => u.X1);
            double yMin = points.Min(u => u.Y1);

            chart.ChartAreas[0].AxisY.Maximum = xMax + (xMax - xMin) / points.Count;
            chart.ChartAreas[0].AxisX.Maximum = yMax + (yMax - yMin) / points.Count;
            chart.ChartAreas[0].AxisY.Minimum = xMin - (xMax - xMin) / points.Count;
            chart.ChartAreas[0].AxisX.Minimum = yMin - (yMax - yMin) / points.Count;

        }
    }
}
