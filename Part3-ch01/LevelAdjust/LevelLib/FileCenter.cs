using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelLib
{
    public class FileCenter
    {
        public static DataCenter OpenFile(string filepath)
        {
            DataCenter dataCenter = new DataCenter();
            StreamReader streamReader = new StreamReader(filepath);
            string str;
            string[] strs;
            str = streamReader.ReadLine();
            strs = str.Split(',');
            dataCenter.KnownPoint1 = new PointInfo(strs[0], Convert.ToDouble(strs[1]));
            str = streamReader.ReadLine();
            strs = str.Split(',');
            dataCenter.KnownPoint2 = new PointInfo(strs[0], Convert.ToDouble(strs[1]));
            streamReader.ReadLine();
            dataCenter.Stations = new List<Station>();
            dataCenter.NewStations = new List<Station>();
            dataCenter.Points = new List<PointInfo>();
            while ((str =streamReader.ReadLine()) != "")
            {
                strs = str.Split(',');
                Station station = new Station();
                station.Point1 = new PointInfo(strs[0]);
                station.Point2 = new PointInfo(strs[1]);
                station.list = Station.InitList(strs);
                dataCenter.Stations.Add(station);
            }
            Matrix A=new Matrix();
            while ((str = streamReader.ReadLine()) != "")
            {
                Console.Write(str);
                strs = str.Split(',');
                A.N = strs.Length;
                A.M++;
                for (int i = 0; i < A.N; i++)
                {
                    
                    A.array.Add(Convert.ToDouble(strs[i]));
                }
            }
            dataCenter.A = A;
            Matrix B = new Matrix();
            while ((str = streamReader.ReadLine()) != null)
            {
                strs = str.Split(',');
                B.N = strs.Length;
                B.M++;
                for (int i = 0; i < B.N; i++)
                {
                    B.array.Add(Convert.ToDouble(strs[i]));
                }
            }
            dataCenter.B = B;
            return dataCenter;
        }
    }
}
