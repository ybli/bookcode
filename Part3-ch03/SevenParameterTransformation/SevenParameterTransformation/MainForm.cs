using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SevenParameterTransformation
{
    public partial class MainForm : Form
    {
        List<Point> knownPoints;
        List<Point> unknownPoints;
        double[,] V, B, N, sevenPara, testA, testB;
        string report;

        private bool isOpen = false;
        private bool isComputeSevenPara = false;
        private bool isTransformation = false;

        public MainForm()
        {
            InitializeComponent();
        }

        #region 文件
        private void menuReadData_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "(txt文件)|*.txt";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileHandle.ReadData(openFileDialog1.FileName, out knownPoints, out unknownPoints);
                    dataGridView1.DataSource = FileHandle.ToDataTable(knownPoints, unknownPoints);
                    testA = FileHandle.A;
                    testB = FileHandle.B;

                    isOpen = true;
                    isComputeSevenPara = false;
                    isTransformation = false;
                    chart1.Series.Clear();
                    MessageBox.Show("打开成功");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("打开失败\n错误信息：" + ex.Message);
            }
        }

        private void menuSaveReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (isTransformation)
                {
                    saveFileDialog1.Filter = "(txt文件)|*.txt";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FileHandle.SaveReport(saveFileDialog1.FileName, report);
                        MessageBox.Show("保存成功");
                    }
                }
                else
                    MessageBox.Show("请先完成计算");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败\n错误信息：" + ex.Message);
            }
        }

        private void menuSaveDXF_Click(object sender, EventArgs e)
        {
            try
            {
                if (isTransformation)
                {
                    saveFileDialog1.Filter = "(dxf文件)|*.dxf";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FileHandle.SaveDxf(saveFileDialog1.FileName, knownPoints, unknownPoints);
                        MessageBox.Show("保存成功");
                    }
                }
                else
                    MessageBox.Show("请先完成计算");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败\n错误信息：" + ex.Message);
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 计算
        private void menuComputeParameter_Click(object sender, EventArgs e)
        {
            if (isOpen)
            {
                if (knownPoints.Count < 3)
                {
                    MessageBox.Show("已知点个数小于3 无法完成计算");
                }
                else
                {
                    sevenPara = Calculate.ComputeSevenParameter(knownPoints, out B, out N);
                    MessageBox.Show(string.Format("七参数计算成功\n delta X:{0:f6}(m)\n delta Y:{1:f6}(m)\n delta Z:{2:f6}(m)\n " +
                        "epsilon X:{3:f6}(s)\n epsilon Y:{4:f6}(s)\n epsilon Z:{5:f6}(s)\n m:{6:f6}(ppm)",
                        sevenPara[0, 0], sevenPara[1, 0], sevenPara[2, 0], sevenPara[3, 0]*206265,
                        sevenPara[4, 0]*206265, sevenPara[5, 0]*206265, sevenPara[6, 0]*1000000));

                    isComputeSevenPara = true;
                }
            }
            else
                MessageBox.Show("请先导入数据");
        }

        private void menuTransform_Click(object sender, EventArgs e)
        {
            if (isComputeSevenPara)
            {
                Calculate.Transform(knownPoints, unknownPoints, sevenPara);
                dataGridView1.DataSource = FileHandle.ToDataTable(knownPoints, unknownPoints);  //更新datagridview
                MessageBox.Show("坐标转换成功");

                DrawChart.GetGraph(knownPoints, unknownPoints, chart1);
                tabControl1.SelectedIndex = 1;
                MessageBox.Show("图形生成成功");

                V = Calculate.GetV(knownPoints);
                report = FileHandle.WriteReport(unknownPoints, B, V, sevenPara, N,
                    new Martix(testA).Transpose().Element, (new Martix(testA) * new Martix(testB)).Element,
                    new Martix(testA).Inverse(testA).Element);
                txtReport.Text = report;
                tabControl1.SelectedIndex = 2;
                MessageBox.Show("报告生成成功");

                isTransformation = true;
            }
            else
                MessageBox.Show("请先计算七参数");
        }

        private void menuDoAll_Click(object sender, EventArgs e)
        {
            menuComputeParameter_Click(sender, e);
            menuTransform_Click(sender, e);
        }
        #endregion

        #region 查看
        /// <summary>
        /// 查看数据
        /// </summary>
        private void menuViewData_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        /// <summary>
        /// 查看图形
        /// </summary>
        private void menuViewChart_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// 查看报告
        /// </summary>
        private void menuViewReport_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        /// <summary>
        /// 图形放大
        /// </summary>
        private void menuZoomIn_Click(object sender, EventArgs e)
        {
            chart1.Width = Convert.ToInt32(chart1.Width * 1.2);
            chart1.Height = Convert.ToInt32(chart1.Height * 1.2);
        }

        /// <summary>
        /// 图形缩小
        /// </summary>
        private void menuZoomOut_Click(object sender, EventArgs e)
        {
            chart1.Width = Convert.ToInt32(chart1.Width * 0.8);
            chart1.Height = Convert.ToInt32(chart1.Height * 0.8);
        }
        #endregion

        #region 帮助
        /// <summary>
        /// 帮助
        /// </summary>
        private void menuHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("更多信息 请查阅开发文档\n作者:陈艳红、曾相航、苟十权\n2019.01.07");
        }
        #endregion

    }
}
