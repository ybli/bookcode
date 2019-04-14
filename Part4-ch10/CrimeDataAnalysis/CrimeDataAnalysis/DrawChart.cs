using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using PointPattern;

namespace CrimeDataAnalysis
{
    /// <summary>
    /// 用于主窗体chart控件的绘图
    /// </summary>
    class DrawChart
    {
        #region 画点模式分析图表
        /// <summary>
        /// 点模式分析图表的前期准备，公共部分
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="mainForm"></param>
        public static void ClearDataPoints_PointPatternAnalysis(ref Chart chart1,MainForm mainForm)
        {
            //清空数据点
            chart1.Series[0].Points.Clear();//画K函数的图
            chart1.Series[1].Points.Clear();//画G函数的图
            chart1.Series[2].Points.Clear();//画F函数的图
            //顺便设置标题
            chart1.Titles[0].Text = "点模式分析";
            //图表类型
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[1].ChartType = SeriesChartType.Spline;
            chart1.Series[2].ChartType = SeriesChartType.Spline;
            //使图例可见
            chart1.Series[0].IsVisibleInLegend = true;
            chart1.Series[1].IsVisibleInLegend = true;
            chart1.Series[2].IsVisibleInLegend = true;
            //使网格可见
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;

            chart1.Series[0].Enabled = true;
            chart1.Series[1].Enabled = true;
            chart1.Series[2].Enabled = true;

            chart1.Series[0].Label = string.Empty;
            chart1.Series[0].ToolTip = string.Empty;
            chart1.Series[1].Label = string.Empty;
            chart1.Series[1].ToolTip = string.Empty;
            chart1.Series[2].Label = string.Empty;
            chart1.Series[2].ToolTip = string.Empty;

            //设置画图时的datacenter
            mainForm.m_displayDataCenter = DataCenter.GetDisplayDataCenter((DataTable)mainForm.dataGridView1.DataSource);
            mainForm.tabControl1.SelectedIndex = 2;
        }
        #endregion


        #region 画统计分析图表
        /// <summary>
        /// 统计分析图表的前期准备，公共部分
        /// </summary>
        /// <param name="chart1"></param>
        /// <param name="mainForm"></param>
        public static void ClearDataPoints_StatisticalAnalysis(ref Chart chart1,MainForm mainForm)
        {
            //清空数据点
            chart1.Series[0].Points.Clear();//画犯罪类型的图
            chart1.Series[1].Points.Clear();//画hour_of_day的图
            chart1.Series[2].Points.Clear();//画day_of_week的图
            //顺便设置标题
            chart1.Titles[0].Text = "统计分析";
            //图表类型
            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.Series[1].ChartType = SeriesChartType.Column;
            chart1.Series[2].ChartType = SeriesChartType.Column;
            //使网格不可见
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //使图例不可见
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[1].IsVisibleInLegend = false;
            chart1.Series[2].IsVisibleInLegend = false;

            //设置画图时的datacenter
            mainForm.m_displayDataCenter = DataCenter.GetDisplayDataCenter((DataTable)mainForm.dataGridView1.DataSource);
            mainForm.tabControl1.SelectedIndex = 2;
        }
        #endregion

        #region 绘制POI统计分析的图
        public static void DrawPOI(ref Chart chart1, string POI_type)
        {
            //清空数据点
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            //顺便设置标题
            chart1.Titles[0].Text = "兴趣点(" + POI_type + ")上犯罪数量统计";
            //图表类型
            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[2].ChartType = SeriesChartType.Column;
            //使网格不可见
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //使图例不可见
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[1].IsVisibleInLegend = false;
            chart1.Series[2].IsVisibleInLegend = false;

            //设置两个轴
            chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.Auto;
            chart1.Series[0].YAxisType = AxisType.Primary;
            chart1.Series[1].YAxisType = AxisType.Secondary;
            //Series[0]画该兴趣点的各种犯罪的数量
            //Series[1]画该兴趣点的各种犯罪的数量的比例

            chart1.Series[0].Enabled = true;
            chart1.Series[1].Enabled = true;
            chart1.Series[2].Enabled = false;
            chart1.ChartAreas[0].AxisX.Title = "incident_type_primary";
            chart1.ChartAreas[0].AxisY.Title = "record_number";
            chart1.ChartAreas[0].AxisY2.Title = "record_ratio(%)";
        }
        #endregion
    }
}
