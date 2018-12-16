using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using Excel= Microsoft.Office.Interop.Excel;

using TraverseAdjust.Entities;

namespace TraverseAdjust.DAL
{
    /*
   * 功能概要：实现对存储在文本文件或Excel 表格文件原始数据的读取
   * 编号：TA_DAL_001
   * 作者：廖振修
   *  创建日期:2016-06-09
   */
    public class DataInput
    {

        /// <summary>
        ///     ''' 从Text文件中获取原始数据对象
        ///     ''' </summary>
        ///     ''' <param name="fileName ">数据文件路径字符串</param>
        ///     ''' <param name="validateInfo ">数值检查结果字符串</param>
        ///     ''' <remarks></remarks>
        public static KnowedObsData InputDataFromTXTFile(string fileName, ref string validateInfo)
        {
            KnowedObsData theKnowedObsData = new KnowedObsData();
            string dataStr = "";

            try
            {
                dataStr = File.ReadAllText(fileName, Encoding.Default).Trim(); // 一次读取全部数据字符串
                // 以回车换行符作为分隔符分割数据文件成行
                string[] splitStr = new string[] { "\r\n" };
                string[] dataLines = dataStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                int pos=0; // 有效数据开始行数
                for (int i = 0; i <= dataLines.Length - 1; i++)
                {
                    if (dataLines[i].Contains("[数据]"))
                    {
                        pos = i + 1;
                        break;
                    }
                }
                // 处理有效数据
                theKnowedObsData.NetType = Convert.ToInt32(dataLines[pos]); // 导线类型
                // 获取点名
                string[] strInfo = dataLines[pos + 2].Split(',');
                theKnowedObsData.Pnames = new List<string>();
                theKnowedObsData.Pnames.Add(strInfo[0].Trim());
                theKnowedObsData.Pnames.Add(strInfo[1].Trim());
                if (theKnowedObsData.NetType == 1)
                {
                    for (int i = 2; i <= strInfo.Length - 3; i++)
                        theKnowedObsData.Pnames.Add(strInfo[i + 2].Trim());
                    theKnowedObsData.Pnames.Add(strInfo[2].Trim());
                    theKnowedObsData.Pnames.Add(strInfo[3].Trim());
                }
                else
                {
                    for (int i = 2; i <= strInfo.Length - 1; i++)
                        theKnowedObsData.Pnames.Add(strInfo[i].Trim());
                    theKnowedObsData.Pnames.Add(strInfo[1].Trim());
                    theKnowedObsData.Pnames.Add(strInfo[0].Trim());
                }

                // 获取已知点坐标
                theKnowedObsData.X0 = new List<double>();
                theKnowedObsData.Y0 = new List<double>();
                strInfo = dataLines[pos + 3].Split(',');
                theKnowedObsData.X0.Add(Convert.ToDouble(strInfo[0]));
                theKnowedObsData.Y0.Add(Convert.ToDouble(strInfo[1]));
                theKnowedObsData.X0.Add(Convert.ToDouble(strInfo[2]));
                theKnowedObsData.Y0.Add(Convert.ToDouble(strInfo[3]));
                if (theKnowedObsData.NetType == 1)
                {
                    theKnowedObsData.X0.Add(Convert.ToDouble(strInfo[4]));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(strInfo[5]));
                    theKnowedObsData.X0.Add(Convert.ToDouble(strInfo[6]));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(strInfo[7]));
                }
                else
                {
                    theKnowedObsData.X0.Add(Convert.ToDouble(strInfo[1]));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(strInfo[1]));
                    theKnowedObsData.X0.Add(Convert.ToDouble(strInfo[0]));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(strInfo[0]));
                }
                // 获取测角类型
                theKnowedObsData.AngleType = Convert.ToInt32(dataLines[pos + 4]);
                // 获取观测角数据
                theKnowedObsData.bb = new List<double>();
                strInfo = dataLines[pos + 5].Split(',');
                for (int i = 0; i <= strInfo.Length - 1; i++)
                    theKnowedObsData.bb.Add(Convert.ToDouble(strInfo[i]));
                // 获取观测边数据
                theKnowedObsData.SS = new List<double>();
                strInfo = dataLines[pos + 6].Split(',');
                for (int i = 0; i <= strInfo.Length - 1; i++)
                    theKnowedObsData.SS.Add(Convert.ToDouble(strInfo[i]));
            }
            catch (Exception ex)
            {
                validateInfo = ex.Message;
                theKnowedObsData = null/* TODO Change to default(_) if this is not a reference type */;
            }
            return theKnowedObsData;
        }

        /// <summary>
        ///     ''' 从XLS文件中获取原始数据对象
        ///     ''' </summary>
        ///     ''' <param name="fileName ">数据文件路径字符串</param>
        ///     ''' <param name="validateInfo ">数值检查结果字符串</param>
        ///     ''' <remarks></remarks>
        public static KnowedObsData InputDataFromXLSFile(string fileName, ref string validateInfo)
        {
            KnowedObsData theKnowedObsData = new KnowedObsData();

            Excel.Application xlsApp = new Excel.Application(); // Excel程序对象
            Excel.Workbook Wb=null; // Exce 工作簿对象
            Excel.Worksheet WSheet; // Exce工作表对象

            xlsApp.Visible = false; // Excel程序对象界面不可见
            xlsApp.DisplayAlerts = false; // 不显示系统提示
            try
            {
                Wb = xlsApp.Workbooks.Open(fileName); // 打开Excel文件
                WSheet = Wb.Sheets[1]; // 指定工作表

                // 获得数据表格中有效数据行数
                int n = 1;
                string tempstr = "";
                while (tempstr != null)
                {
                    tempstr =Convert.ToString( WSheet.Cells[n, 1].Value);
                    n += 1;
                }

                // 获取导线类型
                if (WSheet.Cells[1, 2].Value == "附和导线")
                {
                    theKnowedObsData.NetType = 1;
                    n = n - 12; // 总点数
                }
                else
                {
                    theKnowedObsData.NetType = 2;
                    n = n - 10; // 总点数
                }
                theKnowedObsData.Pnames = new List<string>();
                theKnowedObsData.X0 = new List<double>();
                theKnowedObsData.Y0 = new List<double>();
                theKnowedObsData.bb = new List<double>();
                theKnowedObsData.SS = new List<double>();
                // 获取数值
                if (theKnowedObsData.NetType == 1)
                {
                    // 赋值点名
                    theKnowedObsData.Pnames.Add(WSheet.Cells[4, 1].Value);
                    theKnowedObsData.Pnames.Add(WSheet.Cells[4, 1].Value);
                    for (int i = 2; i <= n - 3; i++)
                        theKnowedObsData.Pnames.Add(Convert.ToString(WSheet.Cells[11 + i, 1].Value));
                    theKnowedObsData.Pnames.Add(Convert.ToString(WSheet.Cells[6, 1].Value));
                    theKnowedObsData.Pnames.Add(Convert.ToString(WSheet.Cells[7, 1].Value));
                    // 赋值已知点坐标
                    theKnowedObsData.X0.Add(Convert.ToDouble(WSheet.Cells[4, 2].Value));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(WSheet.Cells[4, 3].Value));
                    theKnowedObsData.X0.Add(Convert.ToDouble(WSheet.Cells[5, 2].Value));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(WSheet.Cells[5, 3].Value));
                    theKnowedObsData.X0.Add(Convert.ToDouble(WSheet.Cells[6, 2].Value));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(WSheet.Cells[6, 3].Value));
                    theKnowedObsData.X0.Add(Convert.ToDouble(WSheet.Cells[7, 2].Value));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(WSheet.Cells[7, 3].Value));
                    // 获取夹角类型
                    if (WSheet.Cells[8, 2].Value == "左角")
                        theKnowedObsData.AngleType = 1;
                    else
                        theKnowedObsData.AngleType = 2;
                    // 赋值观测夹角
                    for (int i = 0; i <= n - 3; i++)
                        theKnowedObsData.bb.Add(Convert.ToDouble(WSheet.Cells[12 + i, 2].Value));
                    // 赋值观测边长
                    for (int i = 0; i <= n - 4; i++)
                        theKnowedObsData.SS.Add(Convert.ToDouble(WSheet.Cells[13 + i, 3].Value));
                }
                else
                {
                    // 赋值点名
                    theKnowedObsData.Pnames.Add(Convert.ToString(WSheet.Cells[4, 1].Value));
                    theKnowedObsData.Pnames.Add(Convert.ToString(WSheet.Cells[4, 1].Value));
                    for (int i = 2; i <= n - 3; i++)
                    {
                        theKnowedObsData.Pnames.Add(Convert.ToString(WSheet.Cells[9 + i, 1].Value));
                    }//endfor
                        
                    theKnowedObsData.Pnames.Add(theKnowedObsData.Pnames[1]);
                    theKnowedObsData.Pnames.Add(theKnowedObsData.Pnames[0]);

                    // 赋值已知点坐标
                    theKnowedObsData.X0.Add(Convert.ToDouble(WSheet.Cells[4, 2].Value));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(WSheet.Cells[4, 3].Value));
                    theKnowedObsData.X0.Add(Convert.ToDouble(WSheet.Cells[5, 2].Value));
                    theKnowedObsData.Y0.Add(Convert.ToDouble(WSheet.Cells[5, 3].Value));
                    theKnowedObsData.X0.Add(theKnowedObsData.X0[1]);
                    theKnowedObsData.Y0.Add(theKnowedObsData.Y0[1]);
                    theKnowedObsData.X0.Add(theKnowedObsData.X0[0]);
                    theKnowedObsData.Y0.Add(theKnowedObsData.Y0[0]);

                    // 获取夹角类型
                    if (WSheet.Cells[6, 2].Value == "左角")
                        theKnowedObsData.AngleType = 1;
                    else
                        theKnowedObsData.AngleType = 2;
                    // 赋值观测夹角
                    for (int i = 0; i <= n - 3; i++)
                        theKnowedObsData.bb.Add(Convert.ToDouble(WSheet.Cells[10 + i, 2].Value));
                    // 赋值观测边长
                    for (int i = 0; i <= n - 4; i++)
                        theKnowedObsData.SS.Add(Convert.ToDouble(WSheet.Cells[11 + i, 3].Value));
                }//endelse
            }
            catch (Exception ex)
            {
                validateInfo = ex.Message;
                theKnowedObsData = null;
            }
            finally
            {
                Wb.Close();
                xlsApp.Quit();
                Wb = null;
                xlsApp = null;
            }

            return theKnowedObsData;
        }
    }//endclass
}//endspace
