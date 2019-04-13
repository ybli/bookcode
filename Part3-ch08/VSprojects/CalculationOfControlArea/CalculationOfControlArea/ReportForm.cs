using System;
using System.Windows.Forms;

/********************************************************************************
** auth： 金蕾
** dire:  张金亭
** date： 2018/12/27
** desc： 报告视图
** Ver.:  1.0
*********************************************************************************/

namespace CalculationOfControlArea
{
    /// <summary>
    /// 报告窗体，输出和报告
    /// </summary>
    public partial class ReportForm : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 关闭图像时隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// 根据计算信息显示报告
        /// </summary>
        public void ShowReport()
        {
            if (MainForm.IfStep4)
            {
                txtReport.Text = "";
                txtReport.Text += "******************************************************" + "\r\n";
                txtReport.Text += "********************** 计算报告 **********************" + "\r\n";
                txtReport.Text += "******************************************************" + "\r\n" + "\r\n" + "\r\n";
                txtReport.Text += "-------------行政区域与图幅交区域点坐标信息-------------" + "\r\n" + "\r\n";
                txtReport.Text += "########################" + "\r\n";
                txtReport.Text += "计算比例尺: " + MainForm.MeaScale2 + "\r\n";
                txtReport.Text += "########################" + "\r\n";
                for (int i = 0; i < MainForm.Polygons.Count; i++)
                {
                    txtReport.Text += "--" + MainForm.Polygons[i].Code + "--" + "\r\n";
                    txtReport.Text += "序号\t" +
                        "大地坐标B(dd.mmss)\t大地坐标L(dd.mmss)" + "\r\n";
                    for (int j = 0; j < MainForm.Polygons[i].BPoints.Count; j++)
                    {
                        txtReport.Text += (j + 1).ToString() + "\t"  +
                            "\t" + Tool.AngleToDMS((MainForm.Polygons[i].BPoints[j].B / Math.PI * 180)) +
                            "\t\t" + Tool.AngleToDMS((MainForm.Polygons[i].BPoints[j].L / Math.PI * 180)) + "\r\n";
                    }
                }
                txtReport.Text += "\r\n" + "-------------行政区域面积及平差信息表-------------" + "\r\n" + "\r\n";
                txtReport.Text += "行政区域代码\t行政区域计算面积(m²)\t" +
                "平差配赋面积(m²)\t平差后面积(m²)" + "\r\n";
                for (int i = 0; i < MainForm.Polygons.Count; i++)
                {
                    txtReport.Text += MainForm.Polygons[i].Code + "\t\t";
                    txtReport.Text += MainForm.Polygons[i].MapSheet.CalArea.ToString("F4") + "\t\t";
                    if (MainForm.Polygons[i].DArea != -1)
                        txtReport.Text += MainForm.Polygons[i].DArea.ToString("F4") + "\t\t\t" +
                            MainForm.Polygons[i].AreaAfterControl.ToString("F4") + "\r\n";
                    else
                        txtReport.Text += "\r\n";
                }

                txtReport.Text += "\r\n" + "*********" + "\r\n";
                txtReport.Text += "\r\n" + "-------------图幅信息信息表-------------" + "\r\n";
                txtReport.Text += "\r\n" + "--------" + "\r\n";
                txtReport.Text += "图幅代码\t图幅西南角坐标\t\t" +
            "图幅面积(m²)" + "\r\n";

                txtReport.Text += MainForm.Polygons[0].MapSheet.SheetNum +
                        "\t" + MainForm.Polygons[0].MapSheet.WSPoint1.StrB + "," + MainForm.Polygons[0].MapSheet.WSPoint1.StrL +
                        "   " + MainForm.SheetArea + "\r\n";


            }
        }
    }
}
