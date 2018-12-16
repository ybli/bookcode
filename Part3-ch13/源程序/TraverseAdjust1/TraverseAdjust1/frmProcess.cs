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
using Word = Microsoft.Office.Interop.Excel;

using Map.BLL;
using Map.Entities;

namespace TraverseAdjust
{
    public partial class frmProcess:Form
    {
        private string mDspStr;  // 处理过后要返回的显示数据

        private int mNetType; // 路线类型（1—附和，2－闭合）；闭合导线作为附和导线的特例，后面2已知点与前已知点重合)
        private int mAngleType; // 角度观测类型（1－左角，2－右角）；导线测量顺序默认按点名数组角标顺序；闭合导线除连接角有左角右角之分外，输入的是内角，都是右角
        private string[] mPname; // 点名数组
        private double[] mX0 = new double[4]; // 已知点坐标数组，未知点坐标数组(首尾为已知点)
        private double[] mY0 = new double[4];
        private double[] mX;
        private double[] mY;
        private double[] mbb; // 观测角度、观测边长
        private double[] mSS;
        private double[] mAa; // 各边方位角(首尾为已知边)

         public frmProcess()
        {
            InitializeComponent();
        }

        // 窗体加载
        private void frmProcess_Load(object sender, EventArgs e)
        {
            gbInput.Enabled = false;
            gbResult.Enabled = false;
            gbOutput.Enabled = false;
            btCalc.Enabled = false;
        }

        // 单击文件导入按钮
        private void btFileInput_Click(object sender, EventArgs e)
        {
            string fileName = "";
            OpenFileDialog openDlg = new OpenFileDialog();
            DialogResult dlgR;
            {
                var withBlock = openDlg;
                withBlock.Title = "选择原始数据文件";
                withBlock.Filter = "文本文件(*.txt)|*.txt|Excel表格文件(*.xls)|*.xls";
                dlgR = withBlock.ShowDialog();
                fileName = withBlock.FileName;
            }

            if (dlgR == DialogResult.Cancel) return;
            string validateInfo = "";
            if (fileName.ToLower().Contains(".txt"))
                InputDataFromTxtFile(fileName, ref  validateInfo); // 从文本文件中导入数据
            if (fileName.ToLower().Contains(".xls"))
                InputDataFromXLSFile(fileName, ref validateInfo); // 从XLS文件中导入数据
            if ((validateInfo == ""))
            {
                validateInfo = "导入成功!";
                ShowInputData();
            }
            MessageBox.Show(validateInfo, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btCalc.Enabled = true;
        }

        // 文件输入/手工输入开关
        private void chkIsFileInput_CheckedChanged(object sender, EventArgs e)
        {
            gbInput.Enabled = !chkIsFileInput.Checked;
            btFileInput.Enabled = chkIsFileInput.Checked;
        }

        // 根据已知数据，输入观测数据
        private void btMannul_Click(object sender, EventArgs e)
        {
            GetKnowedInfoFromWin(); // 从窗体界面获取已知信息
            // 初始化观测值输入表格
            InitlGridObsData();
            btCalc.Enabled = true;
            gbParam.Enabled = false;
        }

        // 开始计算
        private void btCalc_Click(object sender, EventArgs e)
        {
            // 从观测列表中获取最终输入数据
            GetObsDataFromGrid();
            // 平差计算
            gbOutput.Enabled = Adjust();
            gbResult.Enabled = true;
        }

        // 输出成果信息
        private void btOutput_Click(object sender, EventArgs e)
        {
            string rType = ""; // 成果类型
            if (rMap.Checked) rType = "Map";
            if (rDXF.Checked) rType = "DXF";
            if (rTXT.Checked) rType = "TXT";
            if (rXLS.Checked) rType = "XLS";

            switch (rType)
            {
                case "Map":
                    Output2Map();
                        break;
                case "DXF":
                        Output2DXF();
                        break;
                case "XLS":
                        Output2XLS();
                        break;
                case "TXT":
                        Output2TXT();
                        break;
            }//endswitch
        }

        // 导线类型开关
        private void chkIsConnecting_CheckedChanged(object sender, EventArgs e)
        {
            gbPt3.Enabled = chkIsConnecting.Checked;
            gbPt4.Enabled = chkIsConnecting.Checked;
        }

        // 从窗体界面获取已知信息
        private void GetKnowedInfoFromWin()
        {
            if (chkIsConnecting.Checked)
                mNetType = 1;
            else
                mNetType = 2;
            mX0[0] = Convert.ToDouble(txtX1.Text.Trim());
            mX0[1] = Convert.ToDouble(txtX2.Text.Trim());
            mY0[0] = Convert.ToDouble(txtY1.Text.Trim());
            mY0[1] = Convert.ToDouble(txtY2.Text.Trim());
            if (mNetType == 1)
            {
                mX0[2] = Convert.ToDouble(txtX3.Text.Trim());
                mX0[3] = Convert.ToDouble(txtX4.Text.Trim());
                mY0[2] = Convert.ToDouble(txtY3.Text.Trim());
                mY0[3] = Convert.ToDouble(txtY4.Text.Trim());
            }
            else
            {
                mX0[2] = Convert.ToDouble(txtX2.Text.Trim());
                mX0[3] = Convert.ToDouble(txtX1.Text.Trim());
                mY0[2] = Convert.ToDouble(txtY2.Text.Trim());
                mY0[3] = Convert.ToDouble(txtY1.Text.Trim());
            }

            if (chkLeftAngle.Checked)
                mAngleType = 1;
            else
                mAngleType = 2;
            int un = Convert.ToInt32(txtUnknowPtNum.Text.Trim()); // 未知点数
            mAa = new double[un + 2 + 1]; // 边长方位角(头尾是已知边)
            mX = new double[un + 1 + 1];
            mY = new double[un + 1 + 1]; // 未知点坐标(头尾是已知点)
            mbb = new double[un + 1 + 1]; // 观测夹角数，比未知点数多2
            mSS = new double[un + 1]; // 观测边数,，比未知点数多1
            mPname = new string[un + 3 + 1]; // 点名(4个已知点+未知点)

            mPname[0] = txtP1Name.Text.Trim();
            mPname[1] = txtP2Name.Text.Trim();
            for (int i = 0; i <= un - 1; i++)
                mPname[2 + i] = "P" + i + 1;
            if (mNetType == 1)
            {
                mPname[un + 2] = txtP3Name.Text.Trim();
                mPname[un + 3] = txtP4Name.Text.Trim();
            }
            else
            {
                mPname[un + 2] = txtP2Name.Text.Trim();
                mPname[un + 3] = txtP1Name.Text.Trim();
            }
        }

        // 初始化观测值表格
        private void InitlGridObsData()
        {
            int n;
            n = mPname.Length; // 总点数（已知+未知)
            gridObsData.Rows.Clear(); // 清空原有行
            gridObsData.Rows.Add(n); // 增加数据行
            // 设置已知点单元格格式
            // 前面两已知点
            gridObsData.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            gridObsData.Rows[0].ReadOnly = true;
            gridObsData.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
            gridObsData.Rows[1].ReadOnly = true;
            gridObsData.Rows[1].Cells[2].Style.BackColor = Color.White;
            gridObsData.Rows[1].Cells[2].ReadOnly = false;
            // 后面两已知点
            gridObsData.Rows[n - 1].DefaultCellStyle.BackColor = Color.LightGray;
            gridObsData.Rows[n - 1].ReadOnly = true;
            gridObsData.Rows[n - 2].Cells[1].Style.BackColor = Color.LightGray;
            gridObsData.Rows[n - 2].Cells[1].ReadOnly = true;

            // 赋值序号及点号
            for (int i = 0; i <= n - 1; i++)
            {
                gridObsData.Rows[i].Cells[0].Value = i + 1;
                gridObsData.Rows[i].Cells[1].Value = mPname[i];
            }
            // 赋值已知点距离
            double dist;
            dist =BaseFunction.DistAB(mX0[0], mY0[0], mX0[1], mY0[1]);
            gridObsData.Rows[1].Cells[3].Value = dist.ToString("0.000");
            dist =BaseFunction.DistAB(mX0[2], mY0[2], mX0[3], mY0[3]);
            gridObsData.Rows[n - 1].Cells[3].Value = dist.ToString("0.000");

            // 赋值观测角度默认值
            for (int i = 1; i <= n - 2; i++)
                gridObsData.Rows[i].Cells[2].Value = "0.0000";
            // 赋值观测距离默认值
            for (int i = 2; i <= n - 2; i++)
                gridObsData.Rows[i].Cells[3].Value = "0.000";
        }

        // 从观测值列表中获得观测数据数据
        private void GetObsDataFromGrid()
        {
            int n = mPname.Length;
            // 点名
            for (int i = 0; i <= n - 1; i++)
                mPname[i] = (gridObsData.Rows[i].Cells[1].Value).ToString();
            // 夹角观测值
            for (int i = 0; i <= n - 3; i++)
            {
                mbb[i] = Convert.ToDouble(gridObsData.Rows[i + 1].Cells[2].Value);
                mbb[i] = BaseFunction.DMS2Hu(mbb[i]);
            }
            // 距离观测值
            for (int i = 0; i <= n - 4; i++)
                mSS[i] = Convert.ToDouble(gridObsData.Rows[i + 2].Cells[3].Value);
        }

        /// <summary>
        ///     ''' 从Text文件中读入原始数据
        ///     ''' </summary>
        ///     ''' <param name="fileName ">数据文件路径字符串</param>
        ///     ''' <param name="validateInfo ">数值检查结果字符串</param>
        ///     ''' <remarks></remarks>
        private void InputDataFromTxtFile(string fileName, ref string validateInfo)
        {
            string dataStr = "";
            try
            {
                dataStr = File.ReadAllText(fileName, Encoding.Default).Trim(); // 一次读取全部数据字符串
                // 以回车换行符作为分隔符分割数据文件成行
                string[]  splitStr =new string[]{"\r\n"};
                string[] dataLines = dataStr.Split(splitStr,StringSplitOptions.RemoveEmptyEntries);
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
                mNetType = Convert.ToInt32(dataLines[pos]); // 导线类型
                var un = Convert.ToInt32(dataLines[pos + 1]); // 未知点数
                mAa = new double[un + 2 + 1]; // 边长方位角(头尾是已知边)
                mX = new double[un + 1 + 1];
                mY = new double[un + 1 + 1]; // 未知点坐标(头尾是已知点)
                // 获取点名
                string[] strInfo = dataLines[pos + 2].Split(',');
                mPname = new string[un + 3 + 1];
                mPname[0] = strInfo[0].Trim();
                mPname[1] = strInfo[1].Trim();
                if (mNetType == 1)
                {
                    for (int i = 2; i <= strInfo.Length - 3; i++)
                        mPname[i] = strInfo[i + 2].Trim();
                    mPname[un + 2] = strInfo[2].Trim();
                    mPname[un + 3] = strInfo[3].Trim();
                }
                else
                {
                    for (int i = 2; i <= strInfo.Length - 1; i++)
                        mPname[i] = strInfo[i];
                    mPname[un + 2] = strInfo[1].Trim();
                    mPname[un + 3] = strInfo[0].Trim();
                }

                // 获取已知点坐标
                strInfo = dataLines[pos + 3].Split(',');
                mX0[0] = Convert.ToDouble(strInfo[0]);
                mY0[0] = Convert.ToDouble(strInfo[1]);
                mX0[1] = Convert.ToDouble(strInfo[2]);
                mY0[1] = Convert.ToDouble(strInfo[3]);
                if (mNetType == 1)
                {
                    mX0[2] = Convert.ToDouble(strInfo[4]);
                    mY0[2] = Convert.ToDouble(strInfo[5]);
                    mX0[3] = Convert.ToDouble(strInfo[6]);
                    mY0[3] = Convert.ToDouble(strInfo[7]);
                }
                else
                {
                    mX0[2] = mX0[1];
                    mY0[2] = mY0[1];
                    mX0[3] = mX0[0];
                    mY0[3] = mY0[0];
                }
                // 获取测角类型
                mAngleType = Convert.ToInt32(dataLines[pos + 4]);
                // 获取观测角数据
                strInfo = dataLines[pos + 5].Split(',');
                mbb = new double[strInfo.Length - 1 + 1];
                for (int i = 0; i <= mbb.Length - 1; i++)
                    mbb[i] = Convert.ToDouble(strInfo[i]);
                // 获取观测边数据
                strInfo = dataLines[pos + 6].Split(',');
                mSS = new double[strInfo.Length - 1 + 1];
                for (int i = 0; i <= mSS.Length - 1; i++)
                    mSS[i] = Convert.ToDouble(strInfo[i]);
            }
            catch (Exception ex)
            {
                validateInfo = ex.Message;
            }
        }

        /// <summary>
        ///     ''' 从XLS文件中读入原始数据
        ///     ''' </summary>
        ///     ''' <param name="fileName ">数据文件路径字符串</param>
        ///     ''' <param name="validateInfo ">数值检查结果字符串</param>
        ///     ''' <remarks></remarks>
        public void InputDataFromXLSFile(string fileName, ref string validateInfo)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application(); // Excel程序对象
            Microsoft.Office.Interop.Excel.Workbook Wb=null; // Exce 工作簿对象
            Microsoft.Office.Interop.Excel.Worksheet WSheet=null; // Exce工作表对象

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
                    mNetType = 1;
                    n = n - 12; // 总点数
                }
                else
                {
                    mNetType = 2;
                    n = n - 10; // 总点数
                }
                mPname = new string[n - 1 + 1];
                mAa = new double[n - 2 + 1]; // 边长方位角(头尾是已知边)
                mX = new double[n - 3 + 1];
                mY = new double[n - 3 + 1]; // 未知点坐标(头尾是已知点)
                mbb = new double[n - 3 + 1]; // 观测夹角
                mSS = new double[n - 4 + 1]; // 观测边长
                // 获取数值
                if (mNetType == 1)
                {
                    // 赋值已知点名
                    mPname[0] = WSheet.Cells[4, 1].Value;
                    mPname[1] = WSheet.Cells[5, 1].Value;
                    mPname[n - 2] = WSheet.Cells[6, 1].Value;
                    mPname[n - 1] = WSheet.Cells[7, 1].Value;
                    // 赋值已知点坐标
                    mX0[0] = Convert.ToDouble(WSheet.Cells[4, 2].Value);
                    mY0[0] = Convert.ToDouble(WSheet.Cells[4, 3].Value);
                    mX0[1] = Convert.ToDouble(WSheet.Cells[5, 2].Value);
                    mY0[1] = Convert.ToDouble(WSheet.Cells[5, 3].Value);
                    mX0[2] = Convert.ToDouble(WSheet.Cells[6, 2].Value);
                    mY0[2] = Convert.ToDouble(WSheet.Cells[6, 3].Value);
                    mX0[3] = Convert.ToDouble(WSheet.Cells[7, 2].Value);
                    mY0[3] = Convert.ToDouble(WSheet.Cells[7, 3].Value);
                    // 赋值未知点名
                    for (int i = 2; i <= n - 3; i++)
                        mPname[i] =Convert.ToString( WSheet.Cells[11 + i, 1].Value);
                    // 获取夹角类型
                    if (WSheet.Cells[8, 2].Value== "左角")
                        mAngleType = 1;
                    else
                        mAngleType = 2;
                    // 赋值观测夹角
                    for (int i = 0; i <= n - 3; i++)
                        mbb[i] = Convert.ToDouble(WSheet.Cells[12 + i, 2].Value);
                    // 赋值观测边长
                    for (int i = 0; i <= n - 4; i++)
                        mSS[i] = Convert.ToDouble(WSheet.Cells[13 + i, 3].Value);
                }
                else
                {
                    // 赋值已知点名
                    mPname[0] = WSheet.Cells[4, 1].Value;
                    mPname[1] = WSheet.Cells[5, 1].Value;
                    mPname[n - 2] = mPname[1];
                    mPname[n - 1] = mPname[0];
                    // 赋值已知点坐标
                    mX0[0] = Convert.ToDouble(WSheet.Cells[4, 2].Value);
                    mY0[0] = Convert.ToDouble(WSheet.Cells[4, 3].Value);
                    mX0[1] = Convert.ToDouble(WSheet.Cells[5, 2].Value);
                    mY0[1] = Convert.ToDouble(WSheet.Cells[5, 3].Value);
                    mX0[2] = mX0[1];
                    mY0[2] = mY0[1];
                    mX0[3] = mY0[0];
                    mY0[3] = mY0[0];
                    // 赋值未知点名
                    for (int i = 2; i <= n - 3; i++)
                        mPname[i] = Convert.ToString( WSheet.Cells[9 + i, 1].Value);
                    // 获取夹角类型
                    if (WSheet.Cells[6, 2].Value == "左角")
                        mAngleType = 1;
                    else
                        mAngleType = 2;
                    // 赋值观测夹角
                    for (int i = 0; i <= n - 3; i++)
                        mbb[i] = Convert.ToDouble(WSheet.Cells[10 + i, 2].Value);
                    // 赋值观测边长
                    for (int i = 0; i <= n - 4; i++)
                        mSS[i] = Convert.ToDouble(WSheet.Cells[11 + i, 3].Value);
                }
            }
            catch (Exception ex)
            {
                validateInfo = ex.Message;
            }
            finally
            {
                Wb.Close();
                xlsApp.Quit();
                Wb = null;
                xlsApp = null;
            }
        }

        // 显示导入的文件数据
        private void ShowInputData()
        {
            txtUnknowPtNum.Text = (mPname.Length - 4).ToString();
            if (mNetType == 1)
                chkIsConnecting.Checked = true;
            else
                chkIsConnecting.Checked = false;
            if (mAngleType == 1)
                chkLeftAngle.Checked = true;
            else
                chkLeftAngle.Checked = false;
            txtP1Name.Text = mPname[0];
            txtX1.Text = mX0[0].ToString("0.000");
            txtY1.Text = mY0[0].ToString("0.000");
            txtP2Name.Text = mPname[1];
            txtX2.Text = mX0[1].ToString("0.000");
            txtY2.Text = mY0[1].ToString("0.000");
            if (mNetType == 1)
            {
                txtP3Name.Text = mPname[mPname.Length - 2];
                txtX3.Text = mX0[2].ToString("0.000");
                txtY3.Text = mY0[2].ToString("0.000");
                txtP4Name.Text = mPname[mPname.Length - 1];
                txtX4.Text = mX0[3].ToString("0.000");
                txtY4.Text = mY0[3].ToString("0.000");
            }
            // 初始化观测值表格
            InitlGridObsData();
            // 赋值夹角观测值
            for (int i = 0; i <= mbb.Length - 1; i++)
                gridObsData.Rows[i + 1].Cells[2].Value = mbb[i].ToString("0.0000");
            // 赋值边长观测值
            for (int i = 0; i <= mSS.Length - 1; i++)
                gridObsData.Rows[i + 2].Cells[3].Value = mSS[i].ToString("0.000");
        }

        // 计算近似方位角
        private void CalcDirect0()
        {
            int i;
            double aa0; // 起算方位角

            aa0 = BaseFunction.DirectAB(mX0[0], mY0[0], mX0[1], mY0[1]);
            mAa[0] = aa0;
            mDspStr = ">>>  1.近似方位角计算  <<<\r\n";
            // 计算近似方位角
            if (mNetType == 1)
            {
                for (i = 1; i <= mAa.Length - 1; i++)
                {
                    if (mAngleType == 1)
                    {
                        mAa[i] = mAa[i - 1] + Math.PI + mbb[i - 1];
                        if (mAa[i] >= 2 * Math.PI)
                            mAa[i] = mAa[i] - 2 * Math.PI;
                        if (mAa[i] >= 2 * Math.PI)
                            mAa[i] = mAa[i] - 2 * Math.PI; // 最多不超4PI
                    }
                    else
                    {
                        mAa[i] = mAa[i - 1] - Math.PI - mbb[i - 1];
                        if (mAa[i] < 0)
                            mAa[i] = mAa[i] + 2 * Math.PI;
                        if (mAa[i] < 0)
                            mAa[i] = mAa[i] + 2 * Math.PI; // 最多不超-4PI
                    }
                    mDspStr = mDspStr + mPname[i] + mPname[i + 1] + "  边的近似方位角=" + BaseFunction.Hu2DMS(mAa[i]).ToString("0.0000") + "\r\n";
                }
            }
            else
            {
                if (mAngleType == 1)
                    mbb[0] = 2 * Math.PI - mbb[0];
                mbb[mbb.Length - 1] = mbb[mbb.Length - 1] + (2 * Math.PI - mbb[0]); // 最后一个夹角（转为附和导线)
                if (mbb[mbb.Length - 1] >= 2 * Math.PI)
                    mbb[mbb.Length - 1] = mbb[mbb.Length - 1] - 2 * Math.PI;
                for (i = 1; i <= mAa.Length - 1; i++)
                {
                    mAa[i] = mAa[i - 1] - Math.PI - mbb[i - 1];
                    if (mAa[i] < 0)
                        mAa[i] = mAa[i] + 2 * Math.PI;
                    if (mAa[i] < 0)
                        mAa[i] = mAa[i] + 2 * Math.PI; // 最多不超-4PI
                    mDspStr = mDspStr + mPname[i] + mPname[i + 1] + "  边的近似方位角=" + BaseFunction.Hu2DMS(mAa[i]).ToString("0.0000") + "\r\n";
                }
            }
            mDspStr = mDspStr + "\r\n";
        }

        // 改正方位角
        private bool AdjDirect()
        {
            double aaE; // 终止方位角
            double fbeta; // 方位角闭合差
            double fbeta0 = 24 * Math.Sqrt(mAa.Length - 1) / 10000; // 方位角闭合差限差(三级导线),转为dd.mmss形式

            mDspStr = mDspStr + ">>>  2.方位角近似平差  <<<\r\n";
            aaE = BaseFunction.DirectAB(mX0[2], mY0[2], mX0[3], mY0[3]);
            // 显示终止方位角
            mDspStr = mDspStr + " 终止方位角: " + BaseFunction.Hu2DMS(aaE).ToString("0.0000") + "\r\n";
            // 计算角度闭合差
            fbeta = mAa[mAa.Length - 1] - aaE;
            mDspStr = mDspStr + "  角度闭合差=" + BaseFunction.Hu2DMS(fbeta).ToString("0.0000") + "  限差=" + fbeta0.ToString("0.0000") + "\r\n";
            if (Math.Abs(BaseFunction.Hu2DMS(fbeta)) > fbeta0)
            {
                mDspStr = mDspStr + "方位角闭合差超限!\r\n";
                return false;
            }

            // 改正后的方位角
            double Vbeta0 = -fbeta / (mAa.Length - 1);
            for (var i = 1; i <= mAa.Length - 1; i++)
            {
                mAa[i] += i * Vbeta0;
                if (mAa[i] >= 2 * Math.PI)
                    mAa[i] -= 2 * Math.PI;
                if (mAa[i] < 0)
                    mAa[i] += 2 * Math.PI;
                mDspStr = mDspStr + mPname[i] + mPname[i + 1] + "  边改正后的方位角=" + BaseFunction.Hu2DMS(mAa[i]).ToString("0.0000") + "\r\n";
            }
            mDspStr = mDspStr + "\r\n";
            return true;
        }

        // 计算近似坐标（方位角改正后）
        private void CalcCoord0()
        {
            mDspStr = mDspStr + ">>>  3.近似坐标计算  <<<\r\n";
            // 计算改正角度后的近似坐标
            mX[0] = mX0[1];
            mY[0] = mY0[1];
            for (var i = 1; i <= mX.Length - 1; i++)
            {
                mX[i] = mX[i - 1] + mSS[i - 1] * Math.Cos(mAa[i]);
                mY[i] = mY[i - 1] + mSS[i - 1] * Math.Sin(mAa[i]);
                mDspStr = mDspStr + mPname[i + 1] + " 点的近似坐标为 X=" + mX[i].ToString("0.000") + "  Y=" + mY[i].ToString("0.000") + "\r\n";
            }
            mDspStr = mDspStr + "\r\n";
        }

        // 坐标平差
        private bool AdjCoord()
        {
            mDspStr = mDspStr + ">>>  4.坐标近似平差计算  <<<\r\n";
            double fx, fy, fs;
            double totalS=0.0;
            for (int i = 0; i <= mSS.Length - 1; i++)
                totalS += mSS[i];
            double fs0 =  5000.0;

            fx = mX[mX.Length - 1] - mX0[2];
            fy = mY[mY.Length - 1] - mY0[2];
            fs = Math.Sqrt(fx * fx + fy * fy);
            fs = Math.Floor(1 / (fs / totalS));
            mDspStr = mDspStr + " 坐标闭合差fx=" + (fx * 1000).ToString("0.0") + "mm  fy=" + (fy * 1000).ToString("0.0") + "mm   边长相对误差fs=1/" + fs.ToString("0") + "  限差=1/" + fs0.ToString("0") + "\r\n";
            if (fs < fs0)
            {
                mDspStr = mDspStr + "坐标闭合差超限!\r\n";
                return false;
            }
            // 计算坐标改正数
            mDspStr = mDspStr + "坐标改正数\r\n";
            double[] Vx = new double[mX.Length - 1 + 1], Vy = new double[mX.Length - 1 + 1];
            double Vx0 = -fx / totalS;
            double Vy0 = -fy / totalS;
            for (int i = 0; i <= mSS.Length - 1; i++)
            {
                Vx[i] = Vx0 * mSS[i];
                Vy[i] = Vy0 * mSS[i];
                mDspStr = mDspStr + mPname[i + 2] + " 点 的坐标改正数Vx=" + (Vx[i] * 1000).ToString("0.0") + "mm  Vy=" + (Vy[i] * 1000).ToString("0.0") + "mm \r\n";
            }

            // 计算坐标改正数
            mDspStr = mDspStr + "坐标平差值\r\n";
            double totalVx, totalVy;
            totalVx = totalVy = 0.0;
            for (int i = 1; i <= mX.Length - 1; i++)
            {
                totalVx += Vx[i - 1];
                totalVy += Vy[i - 1];
                mX[i] += totalVx;
                mY[i] += totalVy;
                mDspStr = mDspStr + mPname[i + 1] + " 点 平差坐标X=" + mX[i].ToString("0.000") + "  Y=" + mY[i].ToString("0.000") + "\r\n";
            }
            return true;
        }

        // 近似平差计算
        private bool Adjust()
        {
            CalcDirect0(); // 计算近似方位角
            if (!AdjDirect())
            {
                txtResult.Text = mDspStr; // 显示平差结果
                return false;
            }
            CalcCoord0(); // 计算近似坐标
            if (!AdjCoord())
            {
                txtResult.Text = mDspStr; // 显示平差结果
                return false;
            }
            txtResult.Text = mDspStr; // 显示平差结果
            return true;
        }

        //图形绘制
        private void Output2Map()
        {
            Process mapProcess = new Process();
            List<GElement> gList = OutputGraphics();
            mapProcess.ShowMap(gList);
        }
        // 输出到DXF文件
        private void Output2DXF()
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
            File.WriteAllText(fileName, DXFInfo, ASCIIEncoding.Default); // 保存DXF文件，用ANSI编码
            MessageBox.Show("转存DXF文件成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 输出到XLS文件
        private void Output2XLS()
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
            if (dlgR == DialogResult.Cancel) return;
            SaveXLSFile(fileName);
            MessageBox.Show("转存XLS文件成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 输出到TXT文件
        private void Output2TXT()
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

            int n = mX.Length;
            GElement tempGe = null;//临时图元变量
            PointF startPt, endPt;//直线端点

            //获得线图元
            //第1条已知边
            startPt = new PointF();
            endPt = new PointF();
            startPt.X = (float)mX0[0];
            startPt.Y = (float)mY0[0];
            endPt.X = (float)mX0[1];
            endPt.Y = (float)mY0[1];
            tempGe = new Line2(null, startPt, endPt, Color.Red);

            gList.Add(tempGe);
            //中间量测边
            for (int i = 0; i < n-1; i++)
            {
                startPt = new PointF();
                endPt = new PointF();
                startPt.X = (float)mX[i];
                startPt.Y = (float)mY[i];
                endPt.X = (float)mX[i+1];
                endPt.Y = (float)mY[i+1];
                tempGe = new Line1(null, startPt, endPt, Color.White);

                gList.Add(tempGe);
            }//endfor
            if (mNetType == 1)//附和导线,最后一条已知边
            {
                startPt = new PointF();
                endPt = new PointF();
                startPt.X = (float)mX0[2];
                startPt.Y = (float)mY0[2];
                endPt.X = (float)mX0[3];
                endPt.Y = (float)mY0[3];
                tempGe = new Line2(null, startPt, endPt, Color.Red);

                gList.Add(tempGe);
            }//endif

            //获得点图元
            //获得已知点
            PointF thePt = new PointF();
            thePt.X = (float)mX0[0];
            thePt.Y = (float)mY0[0];
            tempGe = new PointN(mPname[0], thePt, Color.Red);
            gList.Add(tempGe);
            thePt = new PointF();
            thePt.X = (float)mX0[1];
            thePt.Y = (float)mY0[1];
            tempGe = new PointN(mPname[1], thePt, Color.Red);
            gList.Add(tempGe);

            for (int i = 1; i < n - 1; i++)
            {
                thePt = new PointF();
                thePt.X = (float)mX[i];
                thePt.Y = (float)mY[i];
                tempGe = new PointUn(mPname[i+1], thePt, Color.White);
                gList.Add(tempGe);
            }//endif

            if (mNetType == 1)//附和导线,最后一条已知边上2个已知点
            {
                thePt = new PointF();
                thePt.X = (float)mX0[2];
                thePt.Y = (float)mY0[2];
                tempGe = new PointN(mPname[mPname.Length - 2], thePt, Color.Red);
                gList.Add(tempGe);
                thePt = new PointF();
                thePt.X = (float)mX0[3];
                thePt.Y = (float)mY0[3];
                tempGe = new PointN(mPname[mPname.Length-1], thePt, Color.Red);
                gList.Add(tempGe);
            }//endif
            return gList;
        }

        // 输出DXF文件的成果数据
        public string OutputDXFInfo()
        {
            string outputInfo = "";
            // 赋值文件头
            outputInfo += "0\r\n" + "SECTION\r\n" + "2\r\n" + "ENTITIES\r\n";

            // 画点
            outputInfo += ToDXF_Point(mX0[0], mY0[0], true);
            outputInfo += ToDXF_Point(mX0[1], mY0[1], true);
            for (int i = 1; i <= mX.Length - 2; i++)
                outputInfo += ToDXF_Point(mX[i], mY[i], false);
            if (mNetType == 1)
            {
                outputInfo += ToDXF_Point(mX0[2], mY0[2], true);
                outputInfo += ToDXF_Point(mX0[3], mY0[3], true);
            }
            // 画点名
            outputInfo += ToDXF_PointName(mPname[0], mX0[0], mY0[0], true);
            outputInfo += ToDXF_PointName(mPname[1], mX0[1], mY0[1], true);
            for (int i = 1; i <= mX.Length - 2; i++)
                outputInfo += ToDXF_PointName(mPname[i + 1], mX[i], mY[i], false);
            if (mNetType == 1)
            {
                outputInfo += ToDXF_PointName(mPname[mPname.Length - 2], mX0[2], mY0[2], true);
                outputInfo += ToDXF_PointName(mPname[mPname.Length - 1], mX0[3], mY0[3], true);
            }
            // 画点连线
            outputInfo += ToDXF_Line(mX0[0], mY0[0], mX0[1], mY0[1], true);
            for (int i = 0; i <= mX.Length - 2; i++)
                outputInfo += ToDXF_Line(mX[i], mY[i], mX[i + 1], mY[i + 1], false);
            if (mNetType == 1)
                outputInfo += ToDXF_Line(mX0[2], mY0[2], mX0[3], mY0[3], true);

            // 赋值文件尾
            outputInfo += "0\r\n" + "ENDSEC\r\n" + "0\r\n" + "EOF\r\n";
            return outputInfo;
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


            DXFStr = "0\r\n" + "POINT\r\n" + "100\r\n" + "AcDbEntity\r\n" + "8\r\n" + "0\r\n" + "6\r\n" + "Continuous\r\n" + "62\r\n" + colorCode + "\r\n" + "100\r\n" + "AcDbPoint\r\n" + "10\r\n" + x.ToString("0.000") + "\r\n" + "20\r\n" + y.ToString("0.000") + "\r\n";
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
            DXFStr = "0\r\n" + "TEXT\r\n" + "100\r\n" + "AcDbEntity\r\n" + "8\r\n" + "0\r\n" + "6\r\n" + "Continuous\r\n" + "62\r\n" + colorCode + "\r\n" + "100\r\n" + "AcDbText\r\n" + "10\r\n" + x.ToString("0.000") + "\r\n" + "20\r\n" + y.ToString("0.000") + "\r\n" + "40\r\n" + "10.0\r\n" + "1\r\n" + pName + "\r\n" + "7\r\n" + "宋体\r\n" + "100\r\n" + "AcDbText\r\n";
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
            DXFStr = "0\r\n" + "LINE\r\n" + "100\r\n" + "AcDbEntity\r\n" + "8\r\n" + "0\r\n" + "6\r\n" + "Continuous\r\n" + "62\r\n" + colorCode + "\r\n" + "100\r\n" + "AcDbLine\r\n" + "10\r\n" + x1.ToString("0.000") + "\r\n" + "20\r\n" + y1.ToString("0.000") + "\r\n" + "11\r\n" + x2.ToString("0.000") + "\r\n" + "21\r\n" + y2.ToString("0.000") + "\r\n";
            return DXFStr;
        }

        // 计算结果保存为XLs文件
        private void SaveXLSFile(string fileName)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application(); // Excel程序对象
            Microsoft.Office.Interop.Excel.Workbook Wb=null; // Exce 工作簿对象
            Microsoft.Office.Interop.Excel.Worksheet WSheet=null; // Exce工作表对象

            xlsApp.Visible = false; // Excel程序对象界面不可见
            xlsApp.DisplayAlerts = false; // 不显示系统提示
            try
            {
                string tempXLSFile = Application.StartupPath + @"\Output.xls"; // 打开XLS输出模版
                Wb = xlsApp.Workbooks.Open(tempXLSFile); // 打开Excel文件
                WSheet = Wb.Sheets[1]; // 指定工作表
                Microsoft.Office.Interop.Excel.Range rang = WSheet.Rows[3]; // 选择第3行
                rang.Copy(); // 复制
                // 插入比数据行数少1行的XLs表格行（因为已经有1行)
                for (int i = 0; i <= mX.Length; i++)
                    rang.Insert();
                // 赋值XLS表格
                WSheet.Cells[3, 1].Value = mPname[0];
                WSheet.Cells[3, 2].Value = "已知";
                WSheet.Cells[3, 3].Value = mX0[0];
                WSheet.Cells[3, 4].Value = mY0[0];
                WSheet.Cells[4, 1].Value = mPname[1];
                WSheet.Cells[4, 2].Value = "已知";
                WSheet.Cells[4, 3].Value = mX0[1];
                WSheet.Cells[4, 4].Value = mY0[1];
                for (int i = 1; i <= mX.Length - 2; i++)
                {
                    WSheet.Cells[4 + i, 1].Value = mPname[1 + i];
                    WSheet.Cells[4 + i, 2].Value = "未知";
                    WSheet.Cells[4 + i, 3].Value = mX[i];
                    WSheet.Cells[4 + i, 4].Value = mY[i];
                }
                if (mNetType == 1)
                {
                    WSheet.Cells[2 + mPname.Length - 1, 1].Value = mPname[mPname.Length - 2];
                    WSheet.Cells[2 + mPname.Length - 1, 2].Value = "已知";
                    WSheet.Cells[2 + mPname.Length - 1, 3].Value = mX0[2];
                    WSheet.Cells[2 + mPname.Length - 1, 4].Value = mY0[2];
                    WSheet.Cells[2 + mPname.Length, 1].Value = mPname[mPname.Length - 1];
                    WSheet.Cells[2 + mPname.Length, 2].Value = "已知";
                    WSheet.Cells[2 + mPname.Length, 3].Value = mX0[3];
                    WSheet.Cells[2 + mPname.Length, 4].Value = mY0[3];
                }
                Wb.SaveAs(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Wb.Close();
            xlsApp.Quit();
            Wb = null;
            xlsApp = null;
        }

        // 计算结果保存为TXT文件
        private void SaveTXTFile(string fileName)
        {
            string outputInfo = "";
            outputInfo = "***  导线简易平差成果文件  ***\r\n";
            outputInfo += "点名,类型,X坐标,Y坐标\r\n";
            outputInfo += mPname[0] + ",已知," + mX0[0].ToString("0.000") + "," + mY0[0].ToString("0.000") + "\r\n";
            outputInfo += mPname[1] + ",已知," + mX0[1].ToString("0.000") + "," + mY0[1].ToString("0.000") + "\r\n";
            for (int i = 1; i <= mX.Length - 2; i++)
                outputInfo += mPname[1 + i] + ",未知," + mX[i].ToString("0.000") + "," + mY[i].ToString("0.000") + "\r\n";
            if (mNetType == 1)
            {
                outputInfo += mPname[mPname.Length - 2] + ",已知," + mX0[2].ToString("0.000") + "," + mY0[2].ToString("0.000") + "\r\n";
                outputInfo += mPname[mPname.Length - 1] + ",已知," + mX0[3].ToString("0.000") + "," + mY0[3].ToString("0.000") + "\r\n";
            }
            File.WriteAllText(fileName, outputInfo, Encoding.Default);
        }
    }//endclass
}//endspace
