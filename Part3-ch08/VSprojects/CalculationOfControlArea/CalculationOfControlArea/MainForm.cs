using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
/********************************************************************************
** auth： Jin
** date： 2018/12/27
** desc： 主窗口
** Ver.:  1.0
*********************************************************************************/

namespace CalculationOfControlArea
{
    /// <summary>
    /// 主窗口类
    /// </summary>
    public partial class MainForm : Form
    {
        #region 声明成员变量及setter,getter
        // 创建子窗体
        private BoundaryForm boundaryForm = new BoundaryForm();
        private AreaForm areaForm = new AreaForm();
        private ImageForm imageForm = new ImageForm();
        private ReportForm reportForm = new ReportForm();
        /// <summary>
        /// 记录是否可以开始绘制高斯坐标系下的示意图
        /// </summary>
        public static bool canPaintGauss = false;
        /// <summary>
        /// 记录是否可以开始绘制大地坐标系下的示意图
        /// </summary>
        public static bool canPaintGeo = false;
        /// <summary>
        /// 记录是否处于移动状态
        /// </summary>
        public static bool isMove = false;
        /// <summary>
        /// 记录是否已经进行坐标反算
        /// </summary>
        private static bool ifStep1 = false;
        /// <summary>
        /// 记录是否已经完成所有操作
        /// </summary>
        private static bool ifStep4 = false;
        /// <summary>
        /// 保存所有行政区域
        /// </summary>
        private static List<AdminPolygon> polygons = new List<AdminPolygon>();
        /// <summary>
        /// 保存当前比例尺
        /// </summary>
        private static double meaScale = 0;
        /// <summary>
        /// 保存中文比例尺
        /// </summary>
        private static string meaScale2 = "";
        /// <summary>
        /// 保存图幅号
        /// </summary>
        private static string sheetNum = "";
        /// <summary>
        /// 保存图幅标准面积
        /// </summary>
        private static double sheetArea = 0;
        /// <summary>
        /// 理论面积与行政区域加和面积差
        /// </summary>
        private static double areaDiffer = 0;

        public static double AreaDiffer { get { return MainForm.areaDiffer; } set { MainForm.areaDiffer = value; } }
        public static double SheetArea { get { return MainForm.sheetArea; } set { MainForm.sheetArea = value; } }
        public static string SheetNum { get { return MainForm.sheetNum; } set { MainForm.sheetNum = value; } }
        public static string MeaScale2 { get { return MainForm.meaScale2; } set { MainForm.meaScale2 = value; } }
        public static double MeaScale { get { return MainForm.meaScale; } set { MainForm.meaScale = value; } }
        internal static List<AdminPolygon> Polygons { get { return polygons; } set { polygons = value; } }
        public static bool IfStep1 { get { return ifStep1; } set { ifStep1 = value; } }
        public static bool IfStep4 { get { return ifStep4; } set { ifStep4 = value; } }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数，关联子窗体
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            boundaryForm.MdiParent = this;
            areaForm.MdiParent = this;
            imageForm.MdiParent = this;
            reportForm.MdiParent = this;
        }
        #endregion

        #region 自定义操作,包括对视图的更新和初始化算法等
        /// <summary>
        /// 初始化新建数据列表
        /// </summary>
        private void IniNewData()
        {
            MessageBox.Show("将清空已有数据!");
            ClearData();
            UpdateAll();
            boundaryForm.Show();
        }

        /// <summary>
        /// 更新所有列表和ComboBox
        /// </summary>
        private void UpdateAll()
        {
            //boundaryForm.UpdateUpperArea();
            boundaryForm.UpdateCBB();
            boundaryForm.UpdateBoundaryList();
            areaForm.UpdateAreaList();
        }

        /// <summary>
        /// 清空所有数据
        /// </summary>
        private void ClearData()
        {
            polygons.Clear();
            boundaryForm.sheetNumText.Clear();
            boundaryForm.comboBox3.SelectedIndex = 0;
            ifStep1 = false;
            ifStep4 = false;
            canPaintGauss = false;
            canPaintGeo = false;
        }

        /// <summary>
        /// 初始化读入数据,并显示表格
        /// </summary>
        private void IniOpenData()
        {
            MessageBox.Show("将清空已有数据!");
            ClearData();
            boundaryForm.Hide();
            areaForm.Hide();
            imageForm.Hide();
            reportForm.Hide();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                //读取图幅号
                string str = streamReader.ReadLine();
                MainForm.SheetNum = str;
                double meaScale = 0;
                string meaScaleText = "";
                Tool.GetMeascale(sheetNum, ref meaScale, ref meaScaleText);
                MainForm.MeaScale = meaScale;
                MainForm.MeaScale2 = meaScaleText;
                boundaryForm.comboBox3.Text = meaScaleText;
                boundaryForm.sheetNumText.Text = str;

                str = streamReader.ReadLine();
                string[] data = null;
                while (str != null)
                {
                    data = str.Split(',');
                    // 除去字符串中的空格
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Replace(" ", "");
                    }
                    if (data.Count() == 1)
                    {
                        if (!data[0].Equals(""))
                        {
                            polygons.Add(new AdminPolygon(data[0]));
                        }
                    }
                    else if (data.Count() == 2)
                    {

                        polygons[polygons.Count - 1].BPoints.Add(new BPoint(double.Parse(data[1]), double.Parse(data[0]), 0));
                    }
                    else
                    {
                        MessageBox.Show("请检查数据" + str + "是否有误！", "提示");
                        break;
                    }
                    str = streamReader.ReadLine();
                }
            }
            for (int i = 0; i < MainForm.polygons.Count; i++)
            {
                for (int j = 0; j < polygons[i].BPoints.Count; j++)
                    polygons[i].BPoints2.Add(polygons[i].BPoints[j]);
            }
            UpdateAll();
            boundaryForm.Show();
        }
        #endregion

        #region 文件菜单目录下的操作
        /// <summary>
        /// 新建数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem11_Click(object sender, EventArgs e)
        {
            IniNewData();
        }

        /// <summary>
        /// 导入原始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem12_Click(object sender, EventArgs e)
        {
            IniOpenData();

        }

        /// <summary>
        /// 导出计算报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem13_Click(object sender, EventArgs e)
        {
            if (!reportForm.txtReport.Text.Equals(""))
            {
                saveFileDialog.Filter = "文本文件|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fileStream = File.Open(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.Write(reportForm.txtReport.Text);
                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
                    MessageBox.Show("保存成功！", "提示");
                }
            }
            else
            {
                MessageBox.Show("请先生成计算报告！", "提示");
            }
        }

        /// <summary>
        /// 输出高斯坐标系下的示意图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem141_Click(object sender, EventArgs e)
        {
            if (ifStep1)
            {

                saveFileDialog.Filter = "DXF文件|*.dxf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                    imageForm.ToDxfGauss(streamWriter);
                    MessageBox.Show("保存成功！", "提示");
                }
                else
                {
                    MessageBox.Show("请先计算！", "提示");
                }
            }
        }

        /// <summary>
        /// 输出大地坐标系下的示意图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem142_Click(object sender, EventArgs e)
        {
            if (ifStep1)
            {
                saveFileDialog.Filter = "DXF文件|*.dxf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                    imageForm.ToDxfGeo(streamWriter);
                    MessageBox.Show("保存成功！", "提示");
                }
                else
                {
                    MessageBox.Show("请先计算！", "提示");
                }
            }
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem15_Click(object sender, EventArgs e)
        {
            boundaryForm.Close();
            areaForm.Close();
            imageForm.Close();
            reportForm.Close();
            Application.Exit();
        }
        #endregion

        #region 计算菜单下的操作
        /// <summary>
        /// 坐标反算,从高斯平面坐标(X,Y)到大地坐标(B.L)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem22_Click(object sender, EventArgs e)
        {
            if ("".Equals(sheetNum))
            {
                MessageBox.Show("图幅信息不完整!");
                return;
            }

            if (polygons.Count != 0)
            {
                for (int i = 0; i < polygons.Count; i++)
                {
                    polygons[i].AdverseCalculate();
                    //设置区域所包含的图幅
                    MapSheet mapSheet = new MapSheet(sheetNum);
                    polygons[i].MapSheet = mapSheet;

                }
                boundaryForm.UpdateBoundaryList();
                ifStep1 = true;
                canPaintGeo = true;
                canPaintGauss = true;
                imageForm.Refresh();
                imageForm.Show();
            }
            else
            {
                MessageBox.Show("无数据!");
            }
        }

        /// <summary>
        /// 计算各个行政区域的面积
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem23_Click(object sender, EventArgs e)
        {
            if (ifStep1)
            {
                // 求面积
                for (int i = 0; i < polygons.Count; i++)
                {
                    // 设置行政区域与图幅的交点，并计算图幅面积
                    polygons[i].IntercectArea();
                }
                //canPaintGauss = false;
                //imageForm.Refresh();
                //imageForm.Show();
                UpdateAll();
                areaForm.Show();
            }
            else
            {
                MessageBox.Show("请先进行坐标反算!");
            }
        }

        /// <summary>
        /// 根据上级行政区域的面积进行平差
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem24_Click(object sender, EventArgs e)
        {
            if (polygons[0].MapSheet.TheoryArea != 0)
            {
                Tool.AreaAdjustment(polygons);
                ifStep4 = true;
                UpdateAll();
                areaForm.Show();
            }
            else
            {
                MessageBox.Show("请先进行面积计算!");
            }
        }

        /// <summary>
        /// 生成报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem25_Click(object sender, EventArgs e)
        {
            if (ifStep4)
            {
                reportForm.ShowReport();
                reportForm.Show();
            }
            else
            {
                MessageBox.Show("请先进行计算！");
            }
        }
        #endregion

        #region 视图菜单下的操作
        /// <summary>
        /// 全部显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem31_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.Show();
            }
        }

        /// <summary>
        /// 全部隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem32_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.Hide();
            }
        }

        /// <summary>
        /// 全部最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem33_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 全部最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem34_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.WindowState = FormWindowState.Minimized;
            }
        }

        /// <summary>
        /// 全部正常显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem35_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.WindowState = FormWindowState.Normal;
            }
        }

        /// <summary>
        /// 水平显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem36_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// 垂直显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem37_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// 层叠显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem38_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }
        #endregion

        #region 工具菜单下的操作
        /// <summary>
        /// 放大操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem41_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                imageForm.gaussPic.Width = (int)(imageForm.gaussPic.Width * 1.1);
                imageForm.gaussPic.Height = (int)(imageForm.gaussPic.Height * 1.1);
                imageForm.geoPic.Width = (int)(imageForm.geoPic.Width * 1.1);
                imageForm.geoPic.Height = (int)(imageForm.geoPic.Height * 1.1);
            }
            else if (this.ActiveMdiChild is ReportForm)
            {
                reportForm.Font = new Font("楷体", reportForm.Font.Size - 1);
            }
        }

        /// <summary>
        /// 缩小操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem42_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ReportForm)
            {
                if (reportForm.Font.Size > 1)
                {
                    reportForm.Font = new Font("楷体", reportForm.Font.Size - 1);
                }
                else
                {
                    MessageBox.Show("不能再缩小了！", "提示");
                }
            }
            else if (this.ActiveMdiChild is ImageForm)
            {
                imageForm.gaussPic.Width = (int)(imageForm.gaussPic.Width * 0.9);
                imageForm.gaussPic.Height = (int)(imageForm.gaussPic.Height * 0.9);
                imageForm.geoPic.Width = (int)(imageForm.gaussPic.Width * 0.9);
                imageForm.geoPic.Height = (int)(imageForm.gaussPic.Height * 0.9);
            }
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem43_Click(object sender, EventArgs e)
        {
            this.toolItem33.Checked = !this.toolItem33.Checked;
            isMove = this.toolItem33.Checked;
            if (isMove)
            {
                if (this.ActiveMdiChild is ImageForm)
                {
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region 工具条中的操作
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolItem11_Click(object sender, EventArgs e)
        {
            menuItem12_Click(sender, e);
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolItem12_Click(object sender, EventArgs e)
        {
            menuItem11_Click(sender, e);
        }

        /// <summary>
        /// 保存当前活跃窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolItem13_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                if (canPaintGeo)
                    menuItem142_Click(sender, e);
            }
            if (ActiveMdiChild is ReportForm)
            {
                if (!reportForm.txtReport.Text.Equals(""))
                    menuItem13_Click(sender, e);
            }
        }

        /// <summary>
        /// 一键计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolItem2_Click(object sender, EventArgs e)
        {
            menuItem22_Click(sender, e);
            menuItem23_Click(sender, e);
            menuItem24_Click(sender, e);
            menuItem25_Click(sender, e);
        }

        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolItem31_Click(object sender, EventArgs e)
        {
            menuItem41_Click(sender, e);
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolItem32_Click(object sender, EventArgs e)
        {
            menuItem42_Click(sender, e);
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolItem33_Click(object sender, EventArgs e)
        {
            menuItem43_Click(sender, e);
        }
        #endregion
    }
}
