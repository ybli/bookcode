using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using PointPattern;
using CoorTran;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace FileHelper
{
    /// <summary>
    /// 控制文件的输入输出
    /// </summary>
    public class FileIO
    {
        /// <summary>
        /// 读取一个txt文件，返回一个数据集
        /// </summary>
        /// <param name="filePath">输入的文件路径</param>
        /// <returns>返回的数据集</returns>
        public static DataCenter ReadFile(string filePath)
        {
            DataCenter dataCenter = new DataCenter();
            List<PointInfo> pointInfoList = new List<PointInfo>();
            List<CrimeDataPoint> crimeDataPointList = new List<CrimeDataPoint>();
            string lineStr;
            string[] strArray;
            StreamReader sr = new StreamReader(filePath);
            lineStr = sr.ReadLine();
            //while(!string.IsNullOrEmpty(lineStr=sr.ReadLine())
            while ((lineStr=sr.ReadLine())!=null)
            {
                strArray = lineStr.Split('\t');
                CrimeDataPoint cdpt = new CrimeDataPoint(strArray);
                PointInfo pt = cdpt.ParseXY();//导入txt数据时已经进行了坐标转换
                pointInfoList.Add(pt);
                crimeDataPointList.Add(cdpt);
            }
            sr.Close();
            dataCenter.pointInfoList = pointInfoList;
            dataCenter.crimeDataPointList = crimeDataPointList;
            return dataCenter;
        }

        /// <summary>
        /// 导出表格中的数据，储存为txt
        /// </summary>
        /// <param name="filePath">保存的文件路径</param>
        /// <param name="datatable">数据源表格</param>
        public static void SaveTxt(string filePath, DataTable datatable)
        {
            StreamWriter sw = new StreamWriter(filePath);
            //写表头;
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                sw.Write(datatable.Columns[i].ColumnName + "\t");
            }
            sw.Write("\r\n");
            //写内容;
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                for (int j = 0; j < datatable.Columns.Count; j++)
                {
                    sw.Write(datatable.Rows[i][j] + "\t");
                }
                sw.Write("\r\n");
            }
            sw.Close();
        }

        /// <summary>
        /// 导出表格中的数据，储存为excel文件
        /// </summary>
        /// <param name="filePath">保存的文件路径</param>
        /// <param name="datatable">数据源表格</param>
        public static void SaveXlsx(string filePath, DataTable datatable)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook book = excel.Workbooks.Add(Missing.Value);
            Excel.Worksheet sheet = (Excel.Worksheet)book.ActiveSheet;
            excel.Visible = false;

            //写表头;
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                sheet.Cells[1, i + 1] = datatable.Columns[i].ColumnName;
            }
            //写内容;
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                for (int j = 0; j < datatable.Columns.Count; j++)
                {
                    sheet.Cells[i + 2, j + 1] = datatable.Rows[i][j];
                }
            }

            book.SaveAs(filePath, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlShared,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            excel.Workbooks.Close();
            excel.Quit();
        }

    }
}
