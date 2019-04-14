using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TraverseAdjust.BLL;

namespace TraverseAdjust
{
    /*
     * 功能概要：数据处理界面，用于调用BLL层（Process）功能，实现程序数据处理功能
     * 编号：TA_UI_002
     * 作者：廖振修
     *  创建日期:2016-06-09
     */
    public partial class frmProcess:Form
    {
        private Process mDataProcess; // 数据处理对象

         public frmProcess()
        {
            InitializeComponent();
        }

        // 窗体加载
        private void frmProcess_Load(object sender, EventArgs e)
        {
            mDataProcess = new Process(); // 初始化处理对象

            gbInput.Enabled = false;
            gbResult.Enabled = false;
            gbOutput.Enabled = false;
            btCalc.Enabled = false;
        }

        // 单击文件导入按钮
        private void btInput_Click(object sender, EventArgs e)
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

            if (dlgR == DialogResult.Cancel)
                return;
            string validateInfo = "";
            mDataProcess.ShowInputDataFromFile(this, fileName, ref validateInfo); // 导入数据并显示
            if (validateInfo == "")
            {
                validateInfo = "导入成功!";
                btCalc.Enabled = true;
            }
            MessageBox.Show(validateInfo, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 文件输入/手工输入开关
        private void chkIsFileInput_CheckedChanged(object sender, EventArgs e)
        {
            gbInput.Enabled =!chkIsFileInput.Checked;
            btFileInput.Enabled = chkIsFileInput.Checked;
        }

        // 根据已知数据，输入观测数据
        private void btMannul_Click(object sender, EventArgs e)
        {
             mDataProcess.GetKnowedInfoFromWin(this) ;//从窗体界面获取已知信息

             mDataProcess.InitlGridObsData(this); //初始化观测值表格
             btCalc.Enabled = true;
             gbParam.Enabled = false;
        }

        // 开始计算
        private void btCalc_Click(object sender, EventArgs e)
        {
            mDataProcess.Calc(this);
        }

        // 输出成果信息
        private void btOutput_Click(object sender, EventArgs e)
        {
            string rType = ""; // 成果类型
            if (rMap.Checked) rType = "Map";
            if (rDXF.Checked) rType = "DXF";
            if (rTXT.Checked) rType = "TXT";
            if (rXLS.Checked) rType = "XLS";
            mDataProcess.Output(rType); // 导出平差成果
        }

        // 导线类型开关
        private void chkIsConnecting_CheckedChanged(object sender, EventArgs e)
        {
            gbPt3.Enabled = chkIsConnecting.Checked;
            gbPt4.Enabled = chkIsConnecting.Checked;
        }

    }//endclass
}//endspace
