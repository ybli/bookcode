using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace GeodesyCal
{
    public class FileHelper
    {
        public static List<GeodesicInfo> ReadNegData(string filepath, ref Ellipsoid ellipsoid)
        {
            List<GeodesicInfo> dataInfo = new List<GeodesicInfo>();
            try
            {
                StreamReader sr = new StreamReader(filepath, Encoding.Default);
                string line = "";
                string[] info;

                //读椭球参数
                line = sr.ReadLine();
                info = line.Split(',');

                double a = double.Parse(info[0]);
                double f = 1.0 / double.Parse(info[1]);
                ellipsoid = new Ellipsoid(a, f);


                //读取计算点对
                while ((line = sr.ReadLine()) != null)
                {
                    GeodesicInfo date = new GeodesicInfo();
                    info = line.Split(',');
                    //第一点
                    Pointinfo p1 = new Pointinfo();
                    p1.Name = info[0];
                    p1.B = double.Parse(info[1]);
                    p1.L = double.Parse(info[2]);

                    //第二点
                    Pointinfo p2 = new Pointinfo();
                    p2.Name = info[3];
                    p2.B = double.Parse(info[4]);
                    p2.L = double.Parse(info[5]);

                    date.P1 = p1;
                    date.P2 = p2;

                    dataInfo.Add(date);
                }

                sr.Close();

                return dataInfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<GeodesicInfo> ReadPosData(string filepath, ref Ellipsoid ellipsoid)
        {
            List<GeodesicInfo> dataInfo = new List<GeodesicInfo>();
            try
            {
                StreamReader sr = new StreamReader(filepath, Encoding.Default);
                string line = "";
                string[] info;

                //读椭球参数
                line = sr.ReadLine();
                info = line.Split(',');

                double a = double.Parse(info[0]);
                double f = 1.0 / double.Parse(info[1]);
                ellipsoid = new Ellipsoid(a, f);


                //读取点
                while ((line = sr.ReadLine()) != null)
                {
                    GeodesicInfo data = new GeodesicInfo();
                    info = line.Split(',');
                    //第一点
                    Pointinfo p1 = new Pointinfo();
                    p1.Name = info[0];
                    p1.B = double.Parse(info[1]);
                    p1.L = double.Parse(info[2]);
                    data.A12 = double.Parse(info[3]);
                    data.S = double.Parse(info[4]);


                    //第二点
                    Pointinfo p2 = new Pointinfo();
                    p2.Name = info[5];

                    data.P1 = p1;
                    data.P2 = p2;

                    dataInfo.Add(data);
                }

                sr.Close();

                return dataInfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveDataTable(string filepath, DataTable table)
        {
            try
            {

                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Add(Missing.Value);
                Excel.Worksheet sheet = (Excel.Worksheet)workbook.ActiveSheet;

                app.Visible = false;

                //写表头
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sheet.Cells[1, i + 1] = table.Columns[i].ColumnName;
                }

                //写数据
                DataRow dr;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dr = table.Rows[i];
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        sheet.Cells[i + 2, j + 1] = dr[j];
                    }
                }

                workbook.SaveAs(filepath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value
                    , Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                workbook.Close(Missing.Value, Missing.Value, Missing.Value);
                app.Workbooks.Close();
                app.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static StringBuilder GetReport(Ellipsoid ellipsoid, List<GeodesicInfo> data, int type)
        {
            StringBuilder sb = new StringBuilder();
            string line;

            line = "**********************************************************************\r\n\r\n";
            sb.Append(line);
            line = "\t\t\t\t大地主题解算报告\r\n\r\n";
            sb.Append(line);
            line = "**********************************************************************\r\n\r\n\r\n";
            sb.Append(line);
            line = "------------------------------统计数据---------------------------------\r\n\r\n";
            sb.Append(line);
            line = "\t计算点对总数：".PadRight(20) + data.Count + "\r\n\r\n";
            sb.Append(line);
            line = "\t椭球长半轴：".PadRight(20) + ellipsoid.a + "\r\n\r\n";
            sb.Append(line);
            line = "\t椭球扁率：".PadRight(20) + ellipsoid.f + "\r\n\r\n";
            sb.Append(line);
            if (type == 1)
            {
                line = "\t计算类型：".PadRight(20) + "大地主题正算\t\r\n\r\n";
            }
            else
            {
                line = "\t计算类型：".PadRight(20) + "大地主题反算\t\r\n\r";
            }
            sb.Append(line);
            line = "\n\n";
            sb.Append(line);


            line = "------------------------------计算结果--------------------------------------\r\n\r\n";
            sb.Append(line);
            line = "\t点名\t纬度（B）\t\t经度 （L）\t\t大地方位角（A）\t\t大地线（S）\r\n\r\n";
            sb.Append(line);
            for (int i = 0; i < data.Count; i++)
            {
                GeodesicInfo info = data[i];
                Pointinfo p1 = info.P1;
                Pointinfo p2 = info.P2;
                line = "\n";
                sb.Append(line);
                line = "\t" + p1.Name.PadRight(10) + GeoPro.DMS2String(p1.B).PadRight(15) + GeoPro.DMS2String(p1.L).PadRight(15) + GeoPro.DMS2String(info.A12).PadRight(15) + info.S.ToString("0.000") + "\t\t\r\n\r\n";
                sb.Append(line);
                line = "\t" + p2.Name.PadRight(10) + GeoPro.DMS2String(p2.B).PadRight(15) + GeoPro.DMS2String(p2.L).PadRight(15) + GeoPro.DMS2String(info.A21).PadRight(15) + info.S.ToString("0.000") + "\t\t\r\n\r\n";
                sb.Append(line);
                
            }



            line = "\n";
            sb.Append(line);
            return sb;
        }

        public static void SaveReport(string filepath, StringBuilder report)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filepath);
                sw.Write(report.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
