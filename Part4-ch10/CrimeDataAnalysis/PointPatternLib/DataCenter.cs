using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PointPattern
{
    /// <summary>
    /// 包含一个点集的所有属性。
    /// 一个计算过程中，数据的交换中心
    /// </summary>
    public class DataCenter
    {
        public List<PointInfo> pointInfoList;
        public double area;//研究区域的面积
        public List<CrimeDataPoint> crimeDataPointList;
        public int pointNum;//点的数量

        /// <summary>
        /// 一组数据的边界值
        /// </summary>
        public double xMin, xMax, yMin, yMax;
        /// <summary>
        /// 最近距离的矩阵，计算G、F函数时再计算
        /// </summary>
        public double[] minDisArr, minDisToRandPtArr;
        /// <summary>
        /// 这组数据对应的随机点集
        /// </summary>
        public List<PointInfo> randPtlist;

        /// <summary>
        /// 无参构造，作初始化
        /// </summary>
        public DataCenter()
        {
            pointInfoList = new List<PointInfo>();
            pointNum = -1;

            xMin = -1;xMax = -1;
            yMin = -1;yMax = -1;
            area = -1;

            crimeDataPointList = new List<CrimeDataPoint>();
        }

        //用两个list构造dataCenter
        public DataCenter(List<CrimeDataPoint> _crimeDataPointList, List<PointInfo> _pointInfoList)
        {
            pointInfoList = _pointInfoList;
            crimeDataPointList = _crimeDataPointList;
            pointNum = pointInfoList.Count;
            minDisArr = new double[pointNum];
            minDisToRandPtArr = new double[pointNum];

            Algorithm.BoundValue(pointInfoList, out xMin, out xMax, out yMin, out yMax);
            area = Algorithm.AreaCalculate(pointInfoList);
            Compute2one();
            //InitPointList();
            randPtlist = Algorithm.GenerateRandPointList(pointNum);
        }

        void InitPointList()
        {
            foreach (var pt in pointInfoList)
            {
                pt.distanceArr = new double[pointNum];
                pt.distanceToRandPonintsArr = new double[pointNum];
            }
        }

        /// <summary>
        /// 将坐标归一化，计算x2one、y2one
        /// </summary>
        private void Compute2one()
        {
            foreach (var pt in pointInfoList)
            {
                pt.x2one = (pt.x - xMin) / (xMax - xMin);
                pt.y2one = (pt.y - yMin) / (yMax - yMin);
            }
        }

        /// <summary>
        /// 用当前展示的表格来构造数据集
        /// </summary>
        /// <param name="dtable"></param>
        /// <returns></returns>
        public static DataCenter GetDisplayDataCenter(DataTable dtable)
        {
            List<CrimeDataPoint> crimeDataPointList = new List<CrimeDataPoint>();
            List<PointInfo> pointInfoList = new List<PointInfo>();
            //DataTable dtable = (DataTable)dataGridView1.DataSource;
            int rowNum = dtable.Rows.Count;
            for (int i = 0; i < rowNum; i++)
            {
                CrimeDataPoint cdpt = new CrimeDataPoint();
                PointInfo pt = new PointInfo();
                cdpt.incident_id = dtable.Rows[i]["incident_id"].ToString();
                cdpt.incident_datetime = dtable.Rows[i]["incident_datetime"].ToString();
                cdpt.incident_type_primary = dtable.Rows[i]["incident_type_primary"].ToString();
                cdpt.latitude = Convert.ToDouble(dtable.Rows[i]["latitude"].ToString());
                cdpt.longitude = Convert.ToDouble(dtable.Rows[i]["longitude"].ToString());
                cdpt.hour_of_day = Convert.ToInt32(dtable.Rows[i]["hour_of_day"].ToString());
                cdpt.day_of_week = dtable.Rows[i]["day_of_week"].ToString();
                pt = cdpt.ParseXY();//已经进行坐标转换
                crimeDataPointList.Add(cdpt);
                pointInfoList.Add(pt);
            }
            DataCenter displayDataCenter = new DataCenter();
            displayDataCenter = new DataCenter(crimeDataPointList, pointInfoList);
            return displayDataCenter;
        }

        public double[] GetMinDisArr()
        {
            Algorithm.AllP2PdistanceArray(ref pointInfoList);//求出每个点到其他点的距离矩阵，但是没有求出最邻近距离
            for (int i = 0; i < pointNum; i++)
            {
                minDisArr[i] = Algorithm.MinDistance(i, ref pointInfoList);//求出最邻近距离和最邻近点
            }
            return minDisArr;
        }

        public double[] GetMinDisToRandPtArr()
        {
            for (int i = 0; i < pointNum; i++)
            {
                PointInfo pt = pointInfoList[i];
                Algorithm.P2RandPtDistanceArr(ref pt, randPtlist);
                minDisToRandPtArr[i] = Algorithm.MinDistanceToRandPt(ref pt, randPtlist);
                pointInfoList[i].nearestDistanceToRandPonints = pt.nearestDistanceToRandPonints;
                pointInfoList[i].nearestPointInRandPonints = pt.nearestPointInRandPonints;
            }
            return minDisToRandPtArr;
        }
    }
}
