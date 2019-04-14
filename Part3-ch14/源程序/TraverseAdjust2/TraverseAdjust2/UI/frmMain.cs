using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;


namespace TraverseAdjust
{
    /*
     * 功能概要：程序主界面，是调用程序其他功能模块的总入口
     * 编号：TA_UI_001
     * 作者：廖振修
     *  创建日期:2016-06-09
     */

    public partial class frmMain : Form
    {
        //构造函数
        public frmMain()
        {
            InitializeComponent();
        }

        # region 窗体事件
        // 单击下拉菜单按钮
        private void DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemKey = ""; // item的Tag信息
            if (e.ClickedItem.Tag != null)
            {
                itemKey = e.ClickedItem.Tag.ToString();
                DoAction(itemKey);
            }
        }

        // 单击工具条按钮
        private void tbMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemKey = ""; // item的Tag信息
            if (e.ClickedItem.Tag != null)
            {
                itemKey = e.ClickedItem.Tag.ToString();
                DoAction(itemKey);
            }
        }

        // 关闭窗体前确认
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlgR = MessageBox.Show("确实要退出本程序吗？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlgR == DialogResult.Cancel)
                e.Cancel = true;
        }

        // 状态栏上显示当前日期及时间
        private void Timer1_Tick(object sender, EventArgs e)
        {
            sbLable_Time.Text = DateTime.Now.ToLongDateString() + "    " + DateTime.Now.ToLongTimeString();
        }
        # endregion

        # region 自定义函数
        /// <summary>
        /// 根据itemKey调用相应功能函数
        /// </summary>
        /// <param name="itemKey">菜单按钮或工具条按钮中的itemKey信息</param>
        private void DoAction(string itemKey)
        {
            switch (itemKey.ToUpper())
            {
                case "PROCESS": // 显示处理界面
                    {
                        DoProcess();
                        break;
                    }
                case "EXIT": // 退出主程序
                    {
                        mnuFile.HideDropDown();//退出前要隐藏下拉菜单，否则program会报错
                        this.Close();
                        break;
                    }

                case "DESIGNDOC": // 显示设计文档
                    {
                        ShowDesignDoc();
                        break;
                    }

                case "ABOUT": // 显示关于窗体
                    {
                        ShowAbout();
                        break;
                    }
            }//endswitch
        }

        /// <summary>
        ///  弹出处理界面窗体
        /// </summary>
        private void DoProcess()
        {
            Form frmObj = new frmProcess();
            frmObj.ShowDialog();
        }

        /// <summary>
        ///  调用外部程序显示设计文档
        /// </summary>
        private void ShowDesignDoc()
        {
            string fileName = Application.StartupPath + @"\程序开发文档.doc";
            try
            {
                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "文件打开错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///' 显示关于窗体
        /// </summary>
        private void ShowAbout()
        {
            Form frmObj = new frmAbout();
            frmObj.ShowDialog();
        }

        # endregion
        
    }//endclass
}//endspace
