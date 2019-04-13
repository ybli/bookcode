using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Reflection;
//using Excel = Microsoft.Office.Interop.Excel;

namespace CoorLib
{
    public class FileHelper
    {
        public static ObsData ReadFile( string filepath)
        {
            ObsData data = new ObsData();
            try
            {
                string line;
                string[] strs;
                StreamReader sr = new StreamReader(filepath);
                //读椭球参数
                line = sr.ReadLine();
                strs = line.Split(',');
                double a = double.Parse(strs[1]);

                line = sr.ReadLine();
                strs = line.Split(',');
                double invF = double.Parse(strs[1]);
                var ell = new Ellipsoid(a, invF);
                data.Datum = ell;

                line = sr.ReadLine();
                strs = line.Split(',');
            
                data.L0 = GeoPro.Dms2Rad(double.Parse(strs[1]));
                line = sr.ReadLine();

                PointInfo p;
                while ((line = sr.ReadLine()) != null)
                {
                    p = new PointInfo();
                    strs = line.Split(',');
                    p.Name = strs[0];
                    p.B = GeoPro.Dms2Rad(double.Parse(strs[1]));
                    p.L = GeoPro.Dms2Rad(double.Parse(strs[2]));
                    p.H = double.Parse(strs[3]);
                   data.Data.Add(p);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public static void Savexls(string filepath, DataTable datatable)
        {
            //try
            //{
            //    Excel.Application excel = new Excel.Application();
            //    Excel.Workbook book = excel.Workbooks.Add(Missing.Value);
            //    Excel.Worksheet sheet = (Excel.Worksheet)book.ActiveSheet;

            //    excel.Visible = false;

            //    //写表头
            //    for (int i = 0; i < datatable.Columns.Count; i++)
            //    {
            //        sheet.Cells[1, i + 1] = datatable.Columns[i].ColumnName;

            //    }
            //    //写内容

            //    for (int i = 0; i < datatable.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < datatable.Columns.Count; j++)
            //        {
            //            sheet.Cells[i + 2, j + 1] = datatable.Rows[i][j];

            //        }
            //    }

            //    book.SaveAs(filepath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            //    excel.Workbooks.Close();
            //    excel.Quit();

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        public static void Savedxf(List<PointInfo> points, string filepath)
        {
            int h = 10;
            try
            {
                StreamWriter sw = new StreamWriter(filepath);
                sw.WriteLine("0");
                sw.WriteLine("SECTION");
                sw.WriteLine("2");
                sw.WriteLine("ENTITIES");
                for (int i = 0; i < points.Count; i++)
                {
                    sw.WriteLine("0");
                    sw.WriteLine("POINT");
                    sw.WriteLine("8");
                    sw.WriteLine("点层");
                    sw.WriteLine("10");
                    sw.WriteLine(points[i].x);
                    sw.WriteLine("20");
                    sw.WriteLine(points[i].y);

                    sw.WriteLine("0");
                    sw.WriteLine("TEXT");
                    sw.WriteLine("8");
                    sw.WriteLine("注记");
                    sw.WriteLine("10");
                    sw.WriteLine(points[i].x);
                    sw.WriteLine("20");
                    sw.WriteLine(points[i].y);
                    sw.WriteLine("40");
                    sw.WriteLine(h);
                    sw.WriteLine("1");
                    sw.WriteLine(points[i].Name);
                }
                sw.WriteLine("0");
                sw.WriteLine("ENDSEC");
                sw.WriteLine("0");
                sw.WriteLine("EOF");
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
