using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PointPattern;


namespace FileHelper
{
    /// <summary>
    /// 该类用于生成表格和报告
    /// </summary>
    public class Report
    {
        public struct countanalysis
        {
            public double distance;
            public int number;
            public double frequency;
        }
        public static DataTable InitTable(List<CrimeDataPoint> crimeDataPointList)
        {
            DataTable table = new DataTable();
            string[] strarr = new string[] { "incident_id", "incident_datetime", "incident_type_primary",
                "latitude", "longitude", "hour_of_day", "day_of_week" };
            //写表头
            foreach(var str in strarr)
            {
                table.Columns.Add(str);
            }
            //写内容
            foreach (var item in crimeDataPointList)
            {
                DataRow row = table.NewRow();
                row["incident_id"] = item.incident_id;
                row["incident_datetime"] = item.incident_datetime;
                row["incident_type_primary"] = item.incident_type_primary;
                row["latitude"] = item.latitude;
                row["longitude"] = item.longitude;
                row["hour_of_day"] = item.hour_of_day;
                row["day_of_week"] = item.day_of_week;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable InitTable(List<CrimeDataPoint> crimeDataPointList,
            List<PointInfo> pointInfoList)
        {
            DataTable table = new DataTable();
            string[] strarr = new string[] { "incident_id", "incident_datetime", "incident_type_primary",
                "latitude", "longitude", "hour_of_day", "day_of_week","x","y" };
            //写表头
            foreach (var str in strarr)
            {
                table.Columns.Add(str);
            }
            //写内容
            int num = 0;
            foreach (var item in crimeDataPointList)
            {
                DataRow row = table.NewRow();
                row["incident_id"] = item.incident_id;
                row["incident_datetime"] = item.incident_datetime;
                row["incident_type_primary"] = item.incident_type_primary;
                row["latitude"] = item.latitude;
                row["longitude"] = item.longitude;
                row["hour_of_day"] = item.hour_of_day;
                row["day_of_week"] = item.day_of_week;
                row["x"] = pointInfoList[num].x;
                row["y"] = pointInfoList[num].y;
                table.Rows.Add(row);
                num++;
            }
            return table;
        }


        public static void CountAnalysis(int m_t, List<double> dList, List<double> GList, List<double> FList, List<double> KList, out countanalysis[] Gcutana, out countanalysis[] Fcutana, out countanalysis[] Kcutana)
        {

            Gcutana = new countanalysis[dList.Count()];
            for (int i = 0; i < dList.Count(); i++)
            {
                Gcutana[i].distance = dList[i];
                Gcutana[i].number = (int)(GList[i] * m_t);
                Gcutana[i].frequency = GList[i];
            }
            Fcutana = new countanalysis[dList.Count()];
            for (int i = 0; i < dList.Count(); i++)
            {
                Fcutana[i].distance = dList[i];
                Fcutana[i].number = (int)(FList[i] * m_t);
                Fcutana[i].frequency = FList[i];
            }
            Kcutana = new countanalysis[dList.Count()];
            for (int i = 0; i < dList.Count(); i++)
            {
                Kcutana[i].distance = dList[i];
                Kcutana[i].number = (int)(KList[i] * m_t);
                Kcutana[i].frequency = KList[i];
            }
        }
    }
}
