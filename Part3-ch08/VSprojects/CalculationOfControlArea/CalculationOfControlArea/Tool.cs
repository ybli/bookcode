using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************
** auth： Jin
** date： 2018/12/27
** desc： 工具类
** Ver.:  1.0
*********************************************************************************/

namespace CalculationOfControlArea
{
    /// <summary>
    /// 工具类,用于进行角度转换,面积平差等
    /// </summary>
    class Tool
    {
        /// <summary>
        /// 度为单位的角度转换为DD.MMSS
        /// </summary>
        /// <param name="angle">度为单位的角度</param>
        /// <returns>dd.mmss格式的角度</returns>
        static public string AngleToDMS(double angle)
        {
            string angle0;
            string degree = ((int)Math.Floor(angle)).ToString();
            string minute = "";
            if ((int)Math.Floor((angle - Math.Floor(angle)) * 60) < 10)
            {
                minute = "0";
            }
            minute += ((int)Math.Floor((angle - Math.Floor(angle)) * 60)).ToString();
            string second = "";
            if ((((angle - Math.Floor(angle)) * 3600) % 60) < 10)
            {
                second = "0";
            }
            second += ((int)Math.Round(((angle - Math.Floor(angle)) * 3600) % 60)).ToString();
            angle0 = degree + "." + minute + second;
            return angle0;
        }

        /// <summary>
        /// 设置比例尺以及比例尺文本
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="meaScale"></param>
        /// <param name="meascaleText"></param>
        /// <returns></returns>
        static public void GetMeascale(string sheetNum,ref double meaScale,ref string meascaleText)
        {
            char[] alpha = {'B','C','D','E','F','G','H' };
            double[] meaScaleArray = { 1.0 / (5 * Math.Pow(10, 5)), 1.0 / (2.5 * Math.Pow(10, 5)), 1.0 / (1 * Math.Pow(10, 5)),
                                1.0 / (5 * Math.Pow(10, 4)),1.0 / (2.5 * Math.Pow(10, 4)),1.0 / (1 * Math.Pow(10, 4)),1.0 / (5 * Math.Pow(10, 3))};
            string[] meaScaleTextArray = {"1:500000","1:250000","1:100000", "1:50000","1:25000","1:10000","1:5000" };
            if (sheetNum.Length <= 3)
            {
                meaScale =  1.0 / Math.Pow(10, 6);
                meascaleText = "1:1000000";
            }
            else
            {
                string meaNum = sheetNum.Substring(3, 1);
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (alpha[i].ToString().Equals(meaNum))
                    {
                        meaScale = meaScaleArray[i];
                        meascaleText = meaScaleTextArray[i];
                    }
                }
            }
        }

        /// <summary>
        /// 面积平差
        /// </summary>
        /// <param name="areaList">各个多边形的面积</param>
        static public void AreaAdjustment(List<AdminPolygon> polyList)
        {
            List<AdminPolygon> polygons = MainForm.Polygons;
            double sumArea = 0;
            for (int i = 0; i < polygons.Count; i++)
            {
                sumArea += polygons[i].MapSheet.CalArea;
            }
            double dArea = sumArea - MainForm.SheetArea;
            MainForm.AreaDiffer = dArea;
            for (int i = 0; i < polygons.Count; i++)
            {
                polygons[i].DArea = dArea / sumArea * polygons[i].MapSheet.CalArea;
                polygons[i].AreaAfterControl = polygons[i].MapSheet.CalArea - polygons[i].DArea;
            }
        }

        /// <summary>
        /// 检查点集是否闭合
        /// </summary>
        /// <param name="bPoints"></param>
        static public void CheckClose(List<BPoint> bPoints)
        {
            double x1 = bPoints[0].L;
            double y1 = bPoints[0].B;
            double x2 = bPoints[bPoints.Count - 1].L;
            double y2 = bPoints[bPoints.Count - 1].B;
            if (x1 != x2 || y1 != y2)
            {
                bPoints.Add(bPoints[0]);
            }
        }

       
        /// <summary>
        /// 设置经差和纬差
        /// </summary>
        /// <param name="meaScale"></param>
        /// <param name="latDiffer"></param>
        /// <param name="lonDiffer"></param>
        static public void SetLatAndLonDif(double meaScale, ref double latDiffer, ref double lonDiffer)
        {
            if (meaScale == 1.0 / Math.Pow(10, 6))
            {//比例尺为1:1000000
                latDiffer = 4;
                lonDiffer = 6;
            }
            else if (meaScale == 1.0 / (5 * Math.Pow(10, 5)))
            {//比例尺为1:5000000
                latDiffer = 2;
                lonDiffer = 3;
            }
            else if (meaScale == 1.0 / (2.5 * Math.Pow(10, 5)))
            {//比例尺为1:250000
                latDiffer = 1;
                lonDiffer = 1.5;
            }
            else if (meaScale == 1.0 / (1 * Math.Pow(10, 5)))
            {//比例尺为1:100000
                latDiffer = (1.0 / 3);
                lonDiffer = 0.5;
            }
            else if (meaScale == 1.0 / (5 * Math.Pow(10, 4)))
            {//比例尺为1:50000
                latDiffer = (1.0 / 6);
                lonDiffer = (1.0 / 4);
            }
            else if (meaScale == 1.0 / (2.5 * Math.Pow(10, 4)))
            {//比例尺为1:25000
                latDiffer = (5.0 / 60);
                lonDiffer = (7.0 / 60 + 30.0 / 3600);
            }
            else if (meaScale == 1.0 / (1 * Math.Pow(10, 4)))
            {//比例尺为1:10000
                latDiffer = (2.0 / 60 + 30.0 / 3600);
                lonDiffer = (3.0 / 60 + 45.0 / 3600);
            }
            else if (meaScale == 1.0 / (5 * Math.Pow(10, 3)))
            {//比例尺为1:5000
                latDiffer = (1.0 / 60 + 15.0 / 3600);
                lonDiffer = (1.0 / 60 + 52.5 / 3600);
            }

        }

    }
}
