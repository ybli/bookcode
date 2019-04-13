using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace LevelLib
{
    public class DrawChart
    {

        public static void SaveChart(string inputpath,string outputpath)
        {
            DataCenter dataCenter = FileCenter.OpenFile(inputpath);
            Algorithm.PreProcess(ref dataCenter);
            Algorithm.ApproximateProcess(ref dataCenter);
            Algorithm.FinalProcess(ref dataCenter);
            Chart chart=new Chart();
            Draw(dataCenter, ref chart);
            chart.SaveImage(outputpath, ChartImageFormat.Jpeg);
        }
        public static void SaveChart(Chart chart,string filepath)
        {
            chart.SaveImage(filepath, ChartImageFormat.Jpeg);
        }
        public static void Draw(DataCenter dataCenter, ref Chart chart)
        {
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            double MinY;
            double MaxY;
            FindMinMax(out MinY, out MaxY, dataCenter);
            ChartArea chartArea = chart.ChartAreas.Add("ChartArea1");
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.Maximum = MaxY;
            chartArea.AxisY.Minimum = MinY;
            Series series = chart.Series.Add("series1");
            series.ChartType = SeriesChartType.Point;
            series.IsVisibleInLegend = false;
            int i = 0;
            DataPoint dataPoint = new DataPoint(0, dataCenter.KnownPoint1.H);
            dataPoint.Label = dataCenter.KnownPoint1.Name;
            series.Points.Add(dataPoint);
            for (; i < dataCenter.NewStations.Count-1; i++)
            {
                DataPoint dataPoint1 = new DataPoint(i+1, dataCenter.NewStations[i].Point2.realH);
                dataPoint1.Label = dataCenter.NewStations[i].Point2.Name;
                series.Points.Add(dataPoint1);
            }
            dataPoint = new DataPoint(dataCenter.NewStations.Count, dataCenter.KnownPoint2.H);
            dataPoint.Label = dataCenter.KnownPoint2.Name;
            series.Points.Add(dataPoint);
            series.ChartArea = "ChartArea1";
        }

        public static void FindMinMax(out double MinY,out double MaxY,DataCenter dataCenter)
        {
            MinY = dataCenter.NewStations[0].Point2.realH;
            MaxY = dataCenter.NewStations[0].Point2.realH;
            for (int i = 0; i < dataCenter.NewStations.Count-1; i++)
            {
                if (MinY > dataCenter.NewStations[i].Point2.realH)
                {
                    MinY = dataCenter.NewStations[i].Point2.realH;
                }
                if (MaxY < dataCenter.NewStations[i].Point2.realH)
                {
                    MaxY = dataCenter.NewStations[i].Point2.realH;
                }
            }
            if (MinY > dataCenter.KnownPoint1.H)
            {
                MinY = dataCenter.KnownPoint1.H;
            }
            if (MaxY < dataCenter.KnownPoint1.H)
            {
                MaxY = dataCenter.KnownPoint1.H;
            }
            if (MinY > dataCenter.KnownPoint2.H)
            {
                MinY = dataCenter.KnownPoint2.H;
            }
            if (MaxY < dataCenter.KnownPoint2.H)
            {
                MaxY = dataCenter.KnownPoint2.H;
            }
        }
    }
}
