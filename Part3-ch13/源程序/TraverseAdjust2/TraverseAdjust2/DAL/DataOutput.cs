using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

using TraverseAdjust.Entities;
using Map.BLL;
using Map.Entities;

namespace TraverseAdjust.DAL
{
    /*
  * 功能概要：实现对平差结果的文件保存功能（DXF图像交换文件、文本文件和Excel表格文件）
  * 编号：TA_DAL_002
  * 作者：廖振修
  *  创建日期:2016-06-09
  */

    public class DataOutPut
    {
        private TraverseLine mTraverseLine; // 导线实体对象

        // 构造函数
        public DataOutPut(TraverseLine tranverLine)
        {
            mTraverseLine = tranverLine;
        }

        //图形显示
        public void OutPut2Map()
        {
            Process mapProcess = new Process();
            List<GElement> gList = OutputGraphics();
            mapProcess.ShowMap(gList);
        }

        // 输出到DXF文件
        public void Outpt2DXF()
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            string fileName = "";
            DialogResult dlgR;
            {
                var withBlock = saveDlg;
                withBlock.Title = "保存为DXF文件";
                withBlock.Filter = "DXF文件(*.dxf)|*.dxf";
                dlgR = withBlock.ShowDialog();
                fileName = withBlock.FileName;
            }
            if (dlgR == DialogResult.Cancel) return;

            string DXFInfo = OutputDXFInfo();
            File.WriteAllText(fileName, DXFInfo, System.Text.ASCIIEncoding.Default); // 保存DXF文件，用ANSI编码
            MessageBox.Show("转存DXF文件成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 输出到XLS文件
        public void Outpt2XLS()
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            string fileName = "";
            DialogResult dlgR;
            {
                var withBlock = saveDlg;
                withBlock.Title = "保存为XLS文件";
                withBlock.Filter = "XLS文件(*.xls)|*.xls";
                dlgR = withBlock.ShowDialog();
                fileName = withBlock.FileName;
            }
            if (dlgR == DialogResult.Cancel)  return;

            SaveXLSFile(fileName);
            MessageBox.Show("转存XLS文件成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 输出到TXT文件
        public void Outpt2TXT()
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            string fileName = "";
            DialogResult dlgR;
            {
                var withBlock = saveDlg;
                withBlock.Title = "保存为TXT文件";
                withBlock.Filter = "TXT文件(*.txt)|*.txt";
                dlgR = withBlock.ShowDialog();
                fileName = withBlock.FileName;
            }
            if (dlgR == DialogResult.Cancel) return;
            SaveTXTFile(fileName);
            MessageBox.Show("转存txt文件成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      
        //输出画图图元列表
        private List<GElement> OutputGraphics()
        {
            List<GElement> gList = new List<GElement>(); // 图元列表

            string outputInfo = "";
            int n = mTraverseLine.Lines.Count;
            GElement tempGe = null;//临时图元变量
            PointF startPt, endPt;//直线端点
            //获得线图元
            for (int i = 0; i <= n - 2; i++)
            {

                if (i == 0)//开头已知边
                {
                    startPt = new PointF();
                    endPt = new PointF();
                    startPt.X = (float)mTraverseLine.Lines[i].StartPt.X;
                    startPt.Y = (float)mTraverseLine.Lines[i].StartPt.Y;
                    endPt.X = (float)mTraverseLine.Lines[i].EndPt.X;
                    endPt.Y = (float)mTraverseLine.Lines[i].EndPt.Y;
                    tempGe = new Line2(null, startPt, endPt, Color.Red);
                }
                else//中间量测边
                {
                    startPt = new PointF();
                    endPt = new PointF();
                    startPt.X = (float)mTraverseLine.Lines[i].StartPt.X;
                    startPt.Y = (float)mTraverseLine.Lines[i].StartPt.Y;
                    endPt.X = (float)mTraverseLine.Lines[i].EndPt.X;
                    endPt.Y = (float)mTraverseLine.Lines[i].EndPt.Y;
                    tempGe = new Line1(null, startPt, endPt, Color.White);
                }//endelse

                gList.Add(tempGe);
            }//endfor
            if (mTraverseLine.NetType == 1)//附和导线,最后一条已知边
            {
                startPt = new PointF();
                endPt = new PointF();
                startPt.X = (float)mTraverseLine.Lines[n - 1].StartPt.X;
                startPt.Y = (float)mTraverseLine.Lines[n - 1].StartPt.Y;
                endPt.X = (float)mTraverseLine.Lines[n - 1].EndPt.X;
                endPt.Y = (float)mTraverseLine.Lines[n - 1].EndPt.Y;
                tempGe = new Line2(null, startPt, endPt, Color.Red);
                gList.Add(tempGe);
            }//endif

            //获得点图元
            for (int i = 0; i <= n - 3; i++)
            {

                if (i == 0)//开头已知边端点
                {
                    startPt = new PointF();
                    endPt = new PointF();
                    startPt.X = (float)mTraverseLine.Lines[i].StartPt.X;
                    startPt.Y = (float)mTraverseLine.Lines[i].StartPt.Y;
                    endPt.X = (float)mTraverseLine.Lines[i].EndPt.X;
                    endPt.Y = (float)mTraverseLine.Lines[i].EndPt.Y;

                    tempGe = new PointN(mTraverseLine.Lines[i].StartPt.Name, startPt, Color.Red);
                    gList.Add(tempGe);
                    tempGe = new PointN(mTraverseLine.Lines[i].EndPt.Name, endPt, Color.Red);
                    gList.Add(tempGe);
                }
                else//中间量测边端点
                {
                    endPt = new PointF();
                    endPt.X = (float)mTraverseLine.Lines[i].EndPt.X;
                    endPt.Y = (float)mTraverseLine.Lines[i].EndPt.Y;
                    tempGe = new PointUn(mTraverseLine.Lines[i].EndPt.Name, endPt, Color.White);
                    gList.Add(tempGe);
                }//endelse
            }//endfor
            if (mTraverseLine.NetType == 1)//附和导线,最后一条已知边端点
            {
                startPt = new PointF();
                endPt = new PointF();
                startPt.X = (float)mTraverseLine.Lines[n - 1].StartPt.X;
                startPt.Y = (float)mTraverseLine.Lines[n - 1].StartPt.Y;
                endPt.X = (float)mTraverseLine.Lines[n - 1].EndPt.X;
                endPt.Y = (float)mTraverseLine.Lines[n - 1].EndPt.Y;

                tempGe = new PointN(mTraverseLine.Lines[n - 1].StartPt.Name, startPt, Color.Red);
                gList.Add(tempGe);
                tempGe = new PointN(mTraverseLine.Lines[n - 1].EndPt.Name, endPt, Color.Red);
                gList.Add(tempGe);
            }//endif

            return gList;
        }

        // 输出DXF文件的成果数据
        private string OutputDXFInfo()
        {
            string outputInfo = "";
            int n = mTraverseLine.Lines.Count;
            // 赋值文件头
            outputInfo += "0\r\nSECTION\r\n2\r\nENTITIES\r\n";
            if (mTraverseLine.NetType == 1)//附和导线
            {
                for (int i = 0; i <= n - 1; i++)
                {
                    outputInfo += ToDXF_Line(mTraverseLine.Lines[i]);
                }//endfor
                // 画最后一个已知点
                outputInfo += ToDXF_Point(mTraverseLine.Lines[n - 1].EndPt.X, mTraverseLine.Lines[n - 1].EndPt.Y, true);
                outputInfo += ToDXF_PointName(mTraverseLine.Lines[n - 1].EndPt.Name, mTraverseLine.Lines[n - 1].EndPt.X, mTraverseLine.Lines[n - 1].EndPt.Y, true);
            }
            else//闭合
            {
                for (int i = 0; i <= n - 2; i++)
                {
                    outputInfo += ToDXF_Line(mTraverseLine.Lines[i]);
                }//endfor
            }//endelse
            // 赋值文件尾
            outputInfo += "0\r\nENDSEC\r\n0\r\nEOF\r\n";
            return outputInfo;
        }

        // 转换为DXF文件中的点及点名信息
        private string ToDXF_Line(MyLine line)
        {
            string DXFStr = "";
            bool isKnowed = false;
            // 画点及点名
            if (line.StartPt.Type == 1) isKnowed = true;
            DXFStr = ToDXF_Point(line.StartPt.X, line.StartPt.Y, isKnowed);
            DXFStr += ToDXF_PointName(line.StartPt.Name, line.StartPt.X, line.StartPt.Y, isKnowed);
            // 画线
            isKnowed = false;
            if (line.Type == 1) isKnowed = true;
            DXFStr += ToDXF_Line(line.StartPt.X, line.StartPt.Y, line.EndPt.X, line.EndPt.Y, isKnowed);
            return DXFStr;
        }

        // 转换为DXF文件中的点信息
        private string ToDXF_Point(double x, double y, bool isKnowed)
        {
            string DXFStr = "";
            string colorCode = "";
            if (isKnowed)
                colorCode = "1"; // 红色
            else
                colorCode = "7";// 白色


            DXFStr = "0\r\nPOINT\r\n100\r\nAcDbEntity\r\n8\r\n0\r\n6\r\nContinuous\r\n62\r\n" + colorCode + "\r\n"
                + "100\r\nAcDbPoint\r\n10\r\n" + x.ToString("0.000") + "\r\n20\r\n" + y.ToString("0.000") + "\r\n";
            return DXFStr;
        }

        // 转换为DXF文件中的点名信息
        private string ToDXF_PointName(string pName, double x, double y, bool isKnowed)
        {
            string DXFStr = "";
            string colorCode = "";
            if (isKnowed)
                colorCode = "1"; // 红色
            else
                colorCode = "7";// 白色
            DXFStr = "0\r\nTEXT\r\n100\r\nAcDbEntity\r\n8\r\n0\r\n6\r\nContinuous\r\n62\r\n" + colorCode + "\r\n"
                + "100\r\nAcDbText\r\n10\r\n" + x.ToString("0.000") + "\r\n20\r\n" + y.ToString("0.000") + "\r\n"
                + "40\r\n10.0\r\n1\r\n" + pName + "\r\n7\r\n宋体\r\n100\r\nAcDbText\r\n";
            return DXFStr;
        }

        // 转换为DXF文件中的直线信息
        private string ToDXF_Line(double x1, double y1, double x2, double y2, bool isKnowed)
        {
            string DXFStr = "";
            string colorCode = "";
            if (isKnowed)
                colorCode = "1"; // 红色
            else
                colorCode = "7";// 白色
            DXFStr = "0\r\nLINE\r\n100\r\nAcDbEntity\r\n8\r\n0\r\n6\r\nContinuous\r\n62\r\n" + colorCode + "\r\n"
                + "100\r\nAcDbLine\r\n10\r\n" + x1.ToString("0.000") + "\r\n20\r\n" + y1.ToString("0.000") + "\r\n"
                + "11\r\n" + x2.ToString("0.000") + "\r\n21\r\n" + y2.ToString("0.000") + "\r\n";
            return DXFStr;
        }

        // 计算结果保存为XLs文件
        private void SaveXLSFile(string fileName)
        {
            Excel.Application xlsApp = new Excel.Application(); // Excel程序对象
            Excel.Workbook Wb=null; // Exce 工作簿对象
            Excel.Worksheet WSheet; // Exce工作表对象

            xlsApp.Visible = false; // Excel程序对象界面不可见
            xlsApp.DisplayAlerts = false; // 不显示系统提示
            try
            {
                string tempXLSFile = Application.StartupPath + @"\Output.xls"; // 打开XLS输出模版
                Wb = xlsApp.Workbooks.Open(tempXLSFile); // 打开Excel文件
                WSheet = Wb.Sheets[1]; // 指定工作表
                Excel.Range rang = WSheet.Rows[3]; // 选择第3行
                rang.Copy(); // 复制
                // 插入比数据行数少1行的XLs表格行（因为已经有1行)
                int n = mTraverseLine.Lines.Count; // 导线直线段数量,比总点数少1
                if (mTraverseLine.NetType == 1)
                {
                    for (int i = 0; i <= n - 1; i++)
                        rang.Insert();
                }
                else
                    for (int i = 0; i <= n - 3; i++)
                        rang.Insert();

                // 赋值XLS表格
                WSheet.Cells[3, 1].value = mTraverseLine.Lines[0].StartPt.Name;
                WSheet.Cells[3, 2].value = "已知";
                WSheet.Cells[3, 3].value = mTraverseLine.Lines[0].StartPt.X;
                WSheet.Cells[3, 4].value = mTraverseLine.Lines[0].StartPt.Y;
                WSheet.Cells[4, 1].value = mTraverseLine.Lines[1].StartPt.Name;
                WSheet.Cells[4, 2].value = "已知";
                WSheet.Cells[4, 3].value = mTraverseLine.Lines[1].StartPt.X;
                WSheet.Cells[4, 4].value = mTraverseLine.Lines[1].StartPt.Y;
                for (int i = 2; i <= n - 2; i++)
                {
                    WSheet.Cells[3 + i, 1].value = mTraverseLine.Lines[i].StartPt.Name;
                    WSheet.Cells[3 + i, 2].value = "未知";
                    WSheet.Cells[3 + i, 3].value = mTraverseLine.Lines[i].StartPt.X;
                    WSheet.Cells[3 + i, 4].value = mTraverseLine.Lines[i].StartPt.Y;
                }
                if (mTraverseLine.NetType == 1)//附和
                {
                    WSheet.Cells[2 + n, 1].value = mTraverseLine.Lines[n - 1].StartPt.Name;
                    WSheet.Cells[2 + n, 2].value = "已知";
                    WSheet.Cells[2 + n, 3].value = mTraverseLine.Lines[n - 1].StartPt.X;
                    WSheet.Cells[2 + n, 4].value = mTraverseLine.Lines[n - 1].StartPt.Y;
                    WSheet.Cells[2 + n + 1, 1].value = mTraverseLine.Lines[n - 1].EndPt.Name;
                    WSheet.Cells[2 + n + 1, 2].value = "已知";
                    WSheet.Cells[2 + n + 1, 3].value = mTraverseLine.Lines[n - 1].EndPt.X;
                    WSheet.Cells[2 + n + 1, 4].value = mTraverseLine.Lines[n - 1].EndPt.Y;
                }
                Wb.SaveAs(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Wb.Close();
            xlsApp.Quit();
            Wb = null/* TODO Change to default(_) if this is not a reference type */;
            xlsApp = null/* TODO Change to default(_) if this is not a reference type */;
        }

        // 计算结果保存为TXT文件
        private void SaveTXTFile(string fileName)
        {
            string outputInfo = "";
            outputInfo = "***  导线简易平差成果文件  ***\r\n";
            outputInfo += "点名,类型,X坐标,Y坐标\r\n";
            outputInfo += mTraverseLine.Lines[0].StartPt.Name + ",已知," +
                mTraverseLine.Lines[0].StartPt.X.ToString("0.000") + "," + mTraverseLine.Lines[0].StartPt.Y.ToString("0.000") +"\r\n";
            outputInfo += mTraverseLine.Lines[1].StartPt.Name + ",已知," +
                mTraverseLine.Lines[1].StartPt.X.ToString("0.000") + "," + mTraverseLine.Lines[1].StartPt.Y.ToString("0.000") + "\r\n";
            int n = mTraverseLine.Lines.Count; // 导线直线段数量,比总点数少1
            for (int i = 2; i <= n - 2; i++)
                outputInfo += mTraverseLine.Lines[i].StartPt.Name + ",未知," +
                    mTraverseLine.Lines[i].StartPt.X.ToString("0.000") + "," + mTraverseLine.Lines[i].StartPt.Y.ToString("0.000") + "\r\n";
            if (mTraverseLine.NetType == 1)
            {
                outputInfo += mTraverseLine.Lines[n - 1].StartPt.Name + ",已知," +
                    mTraverseLine.Lines[n - 1].StartPt.X.ToString("0.000") + "," + mTraverseLine.Lines[n - 1].StartPt.Y.ToString("0.000") + "\r\n";
                outputInfo += mTraverseLine.Lines[n - 1].EndPt.Name + ",已知," +
                    mTraverseLine.Lines[n - 1].EndPt.X.ToString("0.000") + "," + mTraverseLine.Lines[n - 1].EndPt.Y.ToString("0.000") + "\r\n";
            }
            File.WriteAllText(fileName, outputInfo,  Encoding.Default);
        }
    }//endclass
}//endspace
