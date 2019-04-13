using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
/********************************************************************************
** auth： 金蕾
** dire:  张金亭
** date： 2018/12/27
** desc： 边界点显示视图
** Ver.:  1.0
*********************************************************************************/

namespace CalculationOfControlArea
{
    /// <summary>
    /// 边界点数据窗体,可以录入和删除数据
    /// </summary>
    public partial class BoundaryForm : Form
    {
        private bool hasUpper = false;
        /// <summary>
        /// 标志位，用于防止比例尺变动
        /// </summary>
        private bool tag = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BoundaryForm()
        {
            InitializeComponent();
            UpdateList();
        }

        #region 数据表视图操作
        /// <summary>
        /// 初始化边界点列表
        /// </summary>
        public void IniList()
        {
            bPointList.Clear();
            bPointList.Columns.Add("序号", 50);
            bPointList.Columns.Add("高斯坐标Y(m)", 120);
            bPointList.Columns.Add("高斯坐标X(m)", 120);
            bPointList.Columns.Add("大地坐标B(dd.mmss)", 200);
            bPointList.Columns.Add("大地坐标L(dd.mmss)", 200);
            /*
            isControl.Items.Clear();
            isControl.Items.Add("非上层区域");
            isControl.Items.Add("上层区域");
            isControl.SelectedIndex = 0;
            upperAreaBox.ReadOnly = true;
             * */
        }

        /// <summary>
        /// 更新ComboBox中的数据，即区域代码的列表
        /// </summary>
        public void UpdateCBB()
        {
            //清空comboBox1、comboBox2数据
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            List<AdminPolygon> polygons = MainForm.Polygons;
            for (int i = 0; i < polygons.Count; i++)
            {
                comboBox1.Items.Add(polygons[i].Code);
                comboBox2.Items.Add(polygons[i].Code);
            }
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        
        /// <summary>
        /// 更新边界点列表
        /// </summary>
        public void UpdateBoundaryList()
        {
            IniList();
            List<AdminPolygon> polygons = MainForm.Polygons;
            for (int i = 0; i < polygons.Count; i++)
            {
                if (comboBox1.Text.Equals(polygons[i].Code))
                {
                    /*
                    if (polygons[i].BPoints.Count == 0)
                    {
                        if (polygons[i].IfControl != true)
                            polygons.RemoveAt(i);
                        //UpdateCBB();
                        break;
                    }
                     * */
                    for (int j = 0; j < polygons[i].BPoints2.Count; j++)
                    {
                        ListViewItem item = new ListViewItem((j + 1).ToString());
                        item.SubItems.Add(polygons[i].BPoints2[j].Y.ToString("F4"));
                        item.SubItems.Add(polygons[i].BPoints2[j].X.ToString("F4"));
                        if (polygons[i].BPoints2[j].B != -1 && polygons[i].BPoints2[j].L != -1)
                        {
                            item.SubItems.Add(polygons[i].BPoints2[j].StrB);
                            item.SubItems.Add(polygons[i].BPoints2[j].StrL);
                        }
                        else
                        {
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                        }
                        bPointList.Items.Add(item);
                    }
                }
            }
        }
        #endregion

        #region 事件响应
        /// <summary>
        /// 在上方的ComboBox选项改变时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateUpperArea();
            UpdateBoundaryList();
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void admitBtn_Click(object sender, EventArgs e)
        {
            if (!MainForm.IfStep1)
            {
                List<AdminPolygon> polygons = MainForm.Polygons;
                bool found = false;
                bool ifParse = Regex.IsMatch(textX.Text, @"^[+-]?\d*[.]?\d*$");
                bool ifParse2 = Regex.IsMatch(textY.Text, @"^[+-]?\d*[.]?\d*$");
                if (ifParse && ifParse2 && !textX.Text.Equals("") && !textY.Text.Equals(""))
                {
                    for (int i = 0; i < polygons.Count; i++)
                    {
                        if (comboBox2.Text.Equals(polygons[i].Code))
                        {
                            polygons[i].BPoints.Add(new BPoint(double.Parse(textX.Text),
                                double.Parse(textY.Text), 0));
                            polygons[i].BPoints2.Add(new BPoint(double.Parse(textX.Text),
                                double.Parse(textY.Text), 0));
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        AdminPolygon polygon = new AdminPolygon(comboBox2.Text);
                        polygon.BPoints.Add(new BPoint(double.Parse(textX.Text),
                                double.Parse(textY.Text), 0));
                        polygon.BPoints2.Add(new BPoint(double.Parse(textX.Text),
                                double.Parse(textY.Text), 0));
                        polygons.Add(polygon);
                        UpdateCBB();
                    }

                    UpdateBoundaryList();
                }
                else
                {
                    MessageBox.Show("请输入正确格式的坐标数据！");
                }
            }
            else
            {
                MessageBox.Show("开始计算后请不要修改数据！若要更改数据，请重新导入数据或新建数据！");
            }
        }

        /// <summary>
        /// 删除选中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (!MainForm.IfStep1)
            {
                if (bPointList.SelectedItems.Count == 0)
                {
                    MessageBox.Show("未选中任何项!");
                    return;
                }
                List<AdminPolygon> polygons = MainForm.Polygons;
                AdminPolygon polygon = null;
                for (int i = 0; i < polygons.Count; i++)
                {
                    if (comboBox1.Text.Equals(polygons[i].Code))
                    {
                        polygon = polygons[i];
                    }
                }
                if (polygon != null)
                {
                    for (int i = 0; i < bPointList.SelectedItems.Count; i++)
                    {
                        int index = int.Parse(bPointList.SelectedItems[i].Text);
                        polygon.BPoints.RemoveAt(index - 1);
                    }
                    UpdateBoundaryList();
                }
            }
            else
            {
                MessageBox.Show("开始计算后请不要修改数据！若要更改数据，请重新导入数据或新建数据！");
            }
        }

        /// <summary>
        /// 关闭时隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoundaryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        #endregion

        public void UpdateList()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("1:1000000");
            comboBox3.Items.Add("1:500000");
            comboBox3.Items.Add("1:250000");
            comboBox3.Items.Add("1:100000");
            comboBox3.Items.Add("1:50000");
            comboBox3.Items.Add("1:25000");
            comboBox3.Items.Add("1:10000");
            comboBox3.Items.Add("1:5000");
            comboBox3.SelectedIndex = 0;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.Enabled = false; 
        }
        /*
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tag)
            {
                tag = false;
                return;
            }
            if (MainForm.IfStep1)
            {
                MessageBox.Show("开始计算后请不要修改比例尺!");
                tag = true;
                comboBox3.Text = MainForm.MeaScale2;
                return;
            }
            string meaScale = comboBox3.Text;
            double meaScale2 = 1 / double.Parse(meaScale.Substring(2));
            MainForm.MeaScale = meaScale2;
            MainForm.MeaScale2 = meaScale;
        }
        */
        private void sub_Click(object sender, EventArgs e)
        {
            if (MainForm.IfStep1)
            {
                MessageBox.Show("开始计算后请不要修改图幅信息!");
                sheetNumText.Text = MainForm.SheetNum;
                return;
            }
            string sheetNum = sheetNumText.Text;
            try
            {
                MainForm.SheetNum = sheetNum;
                double meaScale = 0;
                string meaScaleText="";
                Tool.GetMeascale(sheetNum,ref meaScale,ref meaScaleText);
                MainForm.MeaScale = meaScale;
                comboBox3.Text = meaScaleText;
                MainForm.MeaScale2 = meaScaleText;
                MessageBox.Show("图幅已更新!");
                MessageBox.Show("比例尺已更新!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("图幅数据无法解析!");
            }
            
        }

    }

}
