using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

using DiniRaw2XLS.Entities;

namespace DiniRaw2XLS
{
    public partial class frmMain : Form
    {
        private string mOutputPath = "";//成果输出路径
        public frmMain()
        {
            InitializeComponent();
        }

        //导入Dini03原始数据
        private void btInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg= new OpenFileDialog();
            openDlg.DefaultExt = "dat";
            openDlg.Filter = "天宝Dini数据文件(*.dat)|*.dat";
            openDlg.Title = "导入Dini原始数据文件";
            DialogResult dlgR= openDlg.ShowDialog();
            if (dlgR == DialogResult.Cancel) return;

            string fileName = "";
            fileName = openDlg.FileName;
            mOutputPath=Path.GetDirectoryName(fileName);

            this.txtRawData.LoadFile(fileName, RichTextBoxStreamType.PlainText);//导入数据文件到文本框

            btOutput.Enabled = true;
        }

        //导出手簿
        private void btOutput_Click(object sender, EventArgs e)
        {
            string errInfo = "";
            LineInfo line = Processor.GetLineInfo(txtRawData.Lines,ref errInfo);
            if (errInfo != "")
            {
                MessageBox.Show("原始观测数据格式有误！\r\n\r\n 错误信息：" + errInfo);
                return;
            }//endif

           
            mOutputPath +=  "\\手簿";
            if (!Directory.Exists(mOutputPath)) Directory.CreateDirectory(mOutputPath);//创建成果目录

            string unitName=txtUnit.Text.Trim();

            Excel.Application excelApp=new Excel.Application();
            excelApp.Visible = true; // Exce不l可见
            excelApp.DisplayAlerts = false;//不显示提醒消息

            string fileName = "";
            for (int i = 0; i < line.PartList.Count; i++)
            {
                Application.DoEvents();
                fileName = mOutputPath + "\\手簿_" + line.PartList[i].StartPtName + "&" + line.PartList[i].EndPtName;
                part2Excel(excelApp, fileName, line.PartList[i], unitName);
            }

            excelApp.Quit();
            MessageBox.Show("导出成功！");
        }

        /// <summary>
        /// 按测段导出二等水准手簿
        /// </summary>
        /// <param name="excelApp">Excel对象</param>
        /// <param name="fileName">成果文件名</param>
        /// <param name="partInfo">测段对象</param>
        /// <param name="unitName">测量单位名称</param>
        private void part2Excel(Excel.Application excelApp, string fileName, PartInfo partInfo, string unitName)
        {
            string strExcelFile = Path.GetDirectoryName(Application.ExecutablePath) + @"\观测手簿模版.xlt";
            Excel.Workbook  excelWB = excelApp.Workbooks.Add(strExcelFile);
            Excel.Worksheet excelSheet = excelWB.Sheets[1];// 工作表

            excelSheet.Cells[2, 3].Value =unitName; // 单位名称

            // 获得测站数据范围和数据总结范围
            Excel.Range theRange, theRange1, theRange2;

            theRange1 = excelSheet.Range[excelSheet.Cells[7, 1], excelSheet.Cells[9, 10]]; // 测站数据范围
            theRange2 = excelSheet.Range[excelSheet.Cells[10, 1], excelSheet.Cells[14, 10]]; //线路汇总区域范围

            int n = partInfo.StationCount; // 测站数
            theRange2.Cut(excelSheet.Range[excelSheet.Cells[10 + (n - 1) * 3, 1], excelSheet.Cells[14 + (n - 1) * 3, 10]]); // 先剪切赋值线路汇总区域范围

            string fName0, bName0;// 开始已知点点名，结束已知点点名
            fName0= bName0="";
            string fName, bName;//前视点点名，后视点点名
            double bPtH, fPtH;//前视点高程，后视点高程
            bPtH = fPtH = 0.0;
            fName=bName="";
            double Rf1, Rf2, Rb1, Rb2;//中丝读数
            double deltRf, deltRb;//读数差
            double Df1, Df2, Db1, Db2;//前后视距
            double deltD;//视距差
            double deltH1, deltH2, deltH;//单次高差，平均高差
            
            double  SumdeltD=0.0;//累计视距差

            theRange1.Copy();
            // 开始赋值观测手簿
            for (int i = 0; i < n; i++)
            {
                Application.DoEvents();

                fName = partInfo.StationList[i].FPtName;
                bName =partInfo.StationList[i].BPtName;
                if (i == 0) bName0 = bName; // 开始点
                if (i == n-1) fName0 = fName; // 结束点’

                //中丝读数
                Rf1 = partInfo.StationList[i].Rf1;
                Rf2 = partInfo.StationList[i].Rf2;
                Rb1 = partInfo.StationList[i].Rb1;
                Rb2 = partInfo.StationList[i].Rb2;
                //读数差
                deltRf = Rf1 - Rf2;
                deltRb = Rb1 - Rb2;
                //高差及平均高差
                deltH1 = Rb1 - Rf1;
                deltH2 = Rb2 - Rf2;
                deltH = partInfo.StationList[i].DeltH;
                //前、后视点高程
                bPtH = partInfo.StationList[i].BPtH;
                fPtH = partInfo.StationList[i].FPtH;
                //视距
                Db1 = partInfo.StationList[i].Db1;
                Db2 = partInfo.StationList[i].Db2;
                Df1 = partInfo.StationList[i].Df1;
                Df2 = partInfo.StationList[i].Df2;
                deltD = partInfo.StationList[i].DeltD;
                SumdeltD = SumdeltD + deltD;

                theRange = excelSheet.Range[excelSheet.Cells[7 +i * 3, 1], excelSheet.Cells[9 + i * 3, 10]];
                theRange.PasteSpecial(Excel.XlPasteType.xlPasteFormats);

                excelSheet.Cells[7 + i * 3, 1].Value = i + 1; // 序号

                excelSheet.Cells[7 + i * 3, 2].Value = bName; // 后视点
                excelSheet.Cells[7 + i * 3 + 1, 2].Value = fName; // 前视点

                excelSheet.Cells[7 + i * 3, 3].Value = Db1.ToString("0.00000"); // 后视距1
                excelSheet.Cells[7 + i * 3 + 1, 3].Value = Df1.ToString("0.00000"); // 前视距1
                excelSheet.Cells[7 + i * 3 + 2, 3].Value = deltD.ToString("0.00000"); // 视距差

                excelSheet.Cells[7 + i * 3, 4].Value = Db2.ToString("0.00000"); // 后视距2
                excelSheet.Cells[7 + i * 3 + 1, 4].Value = Df2.ToString("0.00000"); // 前视距2
                excelSheet.Cells[7 + i * 3 + 2, 4].Value = SumdeltD.ToString("0.00000"); // 累计视距差

                excelSheet.Cells[7 + i * 3, 5].Value = Rb1.ToString("0.00000"); // 后视读数1
                excelSheet.Cells[7 + i * 3 + 1, 5].Value = Rf1.ToString("0.00000"); // 前视读数1
                excelSheet.Cells[7 + i * 3 + 2, 5].Value = deltH1.ToString("0.00000"); // 高差1

                excelSheet.Cells[7 + i * 3, 6].Value = Rb2.ToString("0.00000"); // 后视读数2
                excelSheet.Cells[7 + i * 3 + 1, 6].Value = Rf2.ToString("0.00000"); // 前视读数2
                excelSheet.Cells[7 + i * 3 + 2, 6].Value = deltH2.ToString("0.00000"); // 高差2

                excelSheet.Cells[7 + i * 3, 7].Value = (deltRb * 1000).ToString("0.00"); // 后视读数差
                excelSheet.Cells[7 + i * 3 + 1, 7].Value = (deltRf * 1000).ToString("0.00"); // 前视读数差
                excelSheet.Cells[7 + i * 3 + 2, 7].Value = ((deltH1 - deltH2) * 1000).ToString("0.00"); // 高差互差

                excelSheet.Cells[7 + i * 3 + 2, 8].Value = deltH.ToString("0.00000"); // 高差

                excelSheet.Cells[7 + i * 3, 9].Value = bPtH.ToString("0.00000"); // 后视点高程
                excelSheet.Cells[7 + i * 3 + 1, 9].Value = fPtH.ToString("0.00000"); // 前视点高程
            }//endfor

            excelSheet.Cells[10 + (n - 1) * 3, 3].Value = partInfo.StartPtName;//线路开始点名
            excelSheet.Cells[10 + (n - 1) * 3 + 1, 3].Value = partInfo.EndPtName; //线路结束点名
            excelSheet.Cells[10 + (n - 1) * 3 + 2, 3].Value = (partInfo.Df / 1000).ToString("0.00000"); // 累计前视距
            excelSheet.Cells[10 + (n - 1) * 3 + 3, 3].Value = (partInfo.Db / 1000).ToString("0.00000"); // 累计后视距

            excelSheet.Cells[10 + (n - 1) * 3 + 1, 6].Value = (partInfo.Db-partInfo.Df).ToString("0.00000"); // 累计视距差
            excelSheet.Cells[10 + (n - 1) * 3 + 2, 6].Value = partInfo.dz.ToString("0.00000"); // 累计高差
            excelSheet.Cells[10 + (n - 1) * 3 + 3, 6].Value = ((partInfo.Db + partInfo.Df )/ 1000).ToString("0.00000"); // 测段距离
            
            //合并单元格
            for (int i = 0; i <= n; i++)
            {
                theRange = excelSheet.Range[excelSheet.Cells[7 + i * 3, 1], excelSheet.Cells[9 + i * 3, 1]];
                theRange.Merge();
            }//endfor

             excelWB.SaveAs(fileName);//保存文件
             excelWB.Close();
        }
    }//endclass
}//endspace
