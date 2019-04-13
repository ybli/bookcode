using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelLib
{
    public class Report
    {
        public static string SaveReport(string filepath)
        {
            DataCenter dataCenter = FileCenter.OpenFile(filepath);
            Algorithm.PreProcess(ref dataCenter);
            Algorithm.ApproximateProcess(ref dataCenter);
            Algorithm.FinalProcess(ref dataCenter);
            return GetReport(dataCenter);
        }
        public static DataTable GetTable(DataCenter dataCenter)
        {
            DataTable table = new DataTable();
            GetHeaderTable(dataCenter, ref table);
            GetPartTable(dataCenter, ref table);
            return table;
        }

        public static DataTable GetAllTable(DataCenter dataCenter)
        {
            DataTable table = new DataTable();
            GetHeaderTable(dataCenter, ref table);
            GetTotalTable(dataCenter, ref table);
            return table;
        }

        public static string GetReport(DataCenter dataCenter)
        {
            string str = "测站数据\r\n";
            for(int i = 0; i < dataCenter.Stations.Count; i++)
            {
                for(int j = 0; j < dataCenter.Stations[i].list.Count; j++)
                {
                    str += dataCenter.Stations[i].list[j].ToString("F3") + " ";
                }
                str += "\r\n";
            }
            str += "水准路线闭合高差\r\n";
            str += dataCenter.fh+"\r\n";
            str += "高差改正数\r\n";
            for(int i = 0; i < dataCenter.Stations.Count; i++)
            {
                str += dataCenter.Stations[i].v+"\r\n";
            }
            str += "矩阵求逆\r\n";
            str += Matrix.getInv(dataCenter.A).printMatrix() + "\r\n";
            str += "矩阵求和\r\n";
            str += Matrix.multiply(dataCenter.A, dataCenter.B).printMatrix() + "\r\n";
            str += "矩阵求转置\r\n";
            str += Matrix.getT(dataCenter.A).printMatrix() + "\r\n";
            str += "L矩阵\r\n";
            str += dataCenter.L.printMatrix() + "\r\n";
            str += "x矩阵\r\n";
            str += dataCenter.x.printMatrix() + "\r\n";
            str += "invBTPB矩阵\r\n";
            str+= dataCenter.invBTPB.printMatrix() + "\r\n";
            str += "高程平差值计算";
            for (int i = 0; i < dataCenter.NewStations.Count - 1; i++)
            {
                str += dataCenter.NewStations[i].Point2.realH + "\r\n";
            }
                return str;
        }

        public static void SaveDXF(string filename, DataCenter dataCenter)
        {
            double h = 100;
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine("0");
            sw.WriteLine("SECTION");
            sw.WriteLine("2");
            sw.WriteLine("ENTITIES");
            sw.WriteLine("0");
            sw.WriteLine("POINT");
            sw.WriteLine("8");
            sw.WriteLine("pointLayer");
            sw.WriteLine("10");
            sw.WriteLine(0);
            sw.WriteLine("20");
            sw.WriteLine(dataCenter.KnownPoint1.H);

            sw.WriteLine("0");
            sw.WriteLine("TEXT");
            sw.WriteLine("8");
            sw.WriteLine("pointNameLayer");
            sw.WriteLine("10");
            sw.WriteLine(0);
            sw.WriteLine("20");
            sw.WriteLine(dataCenter.KnownPoint1.H);
            sw.WriteLine("40");
            sw.WriteLine(h);
            sw.WriteLine("1");
            sw.WriteLine(dataCenter.KnownPoint1.Name);

            for (int i=0; i < dataCenter.NewStations.Count - 1; i++)
            {
                sw.WriteLine("0");
                sw.WriteLine("SECTION");
                sw.WriteLine("2");
                sw.WriteLine("ENTITIES");
                sw.WriteLine("0");
                sw.WriteLine("POINT");
                sw.WriteLine("8");
                sw.WriteLine("pointLayer");
                sw.WriteLine("10");
                sw.WriteLine(i+1);
                sw.WriteLine("20");
                sw.WriteLine(dataCenter.NewStations[i].Point2.realH);

                sw.WriteLine("0");
                sw.WriteLine("TEXT");
                sw.WriteLine("8");
                sw.WriteLine("pointNameLayer");
                sw.WriteLine("10");
                sw.WriteLine(i+1);
                sw.WriteLine("20");
                sw.WriteLine(dataCenter.NewStations[i].Point2.realH);
                sw.WriteLine("40");
                sw.WriteLine(h);
                sw.WriteLine("1");
                sw.WriteLine(dataCenter.NewStations[i].Point2.Name);
            }
            sw.WriteLine("0");
            sw.WriteLine("SECTION");
            sw.WriteLine("2");
            sw.WriteLine("ENTITIES");
            sw.WriteLine("0");
            sw.WriteLine("POINT");
            sw.WriteLine("8");
            sw.WriteLine("pointLayer");
            sw.WriteLine("10");
            sw.WriteLine(dataCenter.NewStations.Count);
            sw.WriteLine("20");
            sw.WriteLine(dataCenter.KnownPoint2.H);

            sw.WriteLine("0");
            sw.WriteLine("TEXT");
            sw.WriteLine("8");
            sw.WriteLine("pointNameLayer");
            sw.WriteLine("10");
            sw.WriteLine(dataCenter.NewStations.Count);
            sw.WriteLine("20");
            sw.WriteLine(dataCenter.KnownPoint2.H);
            sw.WriteLine("40");
            sw.WriteLine(h);
            sw.WriteLine("1");
            sw.WriteLine(dataCenter.KnownPoint2.Name);

            sw.WriteLine("0");
            sw.WriteLine("ENDSEC");
            sw.WriteLine("0");
            sw.WriteLine("EOF");
            sw.Close();
        }    
        private static void GetHeaderTable(DataCenter dataCenter, ref DataTable table)
        {
            table.Columns.Add("后视点名", typeof(string));
            table.Columns.Add("前视点名", typeof(string));
            table.Columns.Add("后距1", typeof(double));
            table.Columns.Add("后距2", typeof(double));
            table.Columns.Add("前距1", typeof(double));
            table.Columns.Add("前距2", typeof(double));
            table.Columns.Add("距离差1", typeof(double));
            table.Columns.Add("距离差2", typeof(double));
            table.Columns.Add("距离差d", typeof(double));
            table.Columns.Add("Σd", typeof(double));
            table.Columns.Add("后视中丝1", typeof(double));
            table.Columns.Add("后视中丝2", typeof(double));
            table.Columns.Add("前视中丝1", typeof(double));
            table.Columns.Add("前视中丝2", typeof(double));
            table.Columns.Add("后视中丝差", typeof(double));
            table.Columns.Add("前视中丝差", typeof(double));
            table.Columns.Add("高差1", typeof(double));
            table.Columns.Add("高差2", typeof(double));
            table.Columns.Add("中丝差", typeof(double));
            table.Columns.Add("高差", typeof(double));
        }
        private static void GetPartTable(DataCenter dataCenter,ref DataTable table)
        {
            for (int i = 0; i < dataCenter.Stations.Count; i++)
            {
                DataRow row = table.NewRow();
                row["后视点名"] = dataCenter.Stations[i].Point1.Name;
                row["前视点名"] = dataCenter.Stations[i].Point2.Name;
                row["后距1"] = dataCenter.Stations[i].list[0];
                row["后距2"] = dataCenter.Stations[i].list[6];
                row["前距1"] = dataCenter.Stations[i].list[2];
                row["前距2"] = dataCenter.Stations[i].list[4];
                row["后视中丝1"] = dataCenter.Stations[i].list[1];
                row["后视中丝2"] = dataCenter.Stations[i].list[7];
                row["前视中丝1"] = dataCenter.Stations[i].list[3];
                row["前视中丝2"] = dataCenter.Stations[i].list[5];
                table.Rows.Add(row);
            }
        }
        private static void GetTotalTable(DataCenter dataCenter, ref DataTable table)
        {
            for (int i = 0; i < dataCenter.Stations.Count; i++)
            {
                DataRow row = table.NewRow();
                row["后视点名"] = dataCenter.Stations[i].Point1.Name;
                row["前视点名"] = dataCenter.Stations[i].Point2.Name;
                row["后距1"] = dataCenter.Stations[i].list[0].ToString("F3");
                row["后距2"] = dataCenter.Stations[i].list[6].ToString("F3");
                row["前距1"] = dataCenter.Stations[i].list[2].ToString("F3");
                row["前距2"] = dataCenter.Stations[i].list[4].ToString("F3");
                row["距离差1"] = dataCenter.Stations[i].list[11].ToString("F3");
                row["距离差2"] = dataCenter.Stations[i].list[12].ToString("F3");
                row["距离差d"] = dataCenter.Stations[i].list[13].ToString("F3");
                row["Σd"] = dataCenter.Stations[i].list[14].ToString("F3");
                row["后视中丝1"] = dataCenter.Stations[i].list[1].ToString("F3");
                row["后视中丝2"] = dataCenter.Stations[i].list[7].ToString("F3");
                row["前视中丝1"] = dataCenter.Stations[i].list[3].ToString("F3");
                row["前视中丝2"] = dataCenter.Stations[i].list[5].ToString("F3");
                row["后视中丝差"] = dataCenter.Stations[i].list[9].ToString("F3");
                row["前视中丝差"] = dataCenter.Stations[i].list[8].ToString("F3");
                row["高差1"] = dataCenter.Stations[i].list[15].ToString("F3");
                row["高差2"] = dataCenter.Stations[i].list[16].ToString("F3");
                row["中丝差"] = dataCenter.Stations[i].list[10].ToString("F3");
                row["高差"] = dataCenter.Stations[i].list[17].ToString("F3");
                table.Rows.Add(row);
            }
        }
    }
}
