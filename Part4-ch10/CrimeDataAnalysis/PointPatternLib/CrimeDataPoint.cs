using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoorTran;

namespace PointPattern
{
    /// <summary>
    /// 一条犯罪数据，共7列
    /// </summary>
    public class CrimeDataPoint
    {
        public string incident_id, incident_datetime, incident_type_primary, day_of_week;
        public double latitude, longitude;
        public int hour_of_day;

        public CrimeDataPoint()
        {
            incident_id = string.Empty;
            incident_datetime = string.Empty;
            incident_type_primary = string.Empty;
            day_of_week = string.Empty;
            latitude = -1;
            longitude = -1;
            hour_of_day = -1;
        }

        public CrimeDataPoint(string[] strarr)
        {
            incident_id = strarr[0];
            incident_datetime = strarr[1];
            incident_type_primary = strarr[2];
            latitude = Convert.ToDouble(strarr[3]);
            longitude = Convert.ToDouble(strarr[4]);
            hour_of_day = Convert.ToInt32(strarr[5]);
            day_of_week = strarr[6];
        }

        /// <summary>
        /// 从一条犯罪数据中计算出其高斯平面坐标
        /// </summary>
        /// <returns>空间的二维点</returns>
        public PointInfo ParseXY()
        {
            PointInfo pt = new PointInfo();
            SpacePoint spt = new SpacePoint();
            spt.B = CoorTran.Algorithm.D2R(latitude);
            spt.L = CoorTran.Algorithm.D2R(longitude);
            CoorTran.Algorithm.BL2xy(new EarthPara(), spt);
            pt.x = spt.x;
            pt.y = spt.y;
            pt.pointID = incident_id;
            return pt;
        }
    }
}
