using System.Collections.Generic;
using System.Windows.Forms;

/********************************************************************************
** auth： 金蕾
** dire:  张金亭
** date： 2018/12/27
** desc： 各区域面积显示视图
** Ver.:  1.0
*********************************************************************************/

namespace CalculationOfControlArea
{
    /// <summary>
    /// 面积信息显示视图，并有更新数据等功能
    /// </summary>
    public partial class AreaForm : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AreaForm()
        {
            InitializeComponent();
        }

        #region 数据列表更新和处理操作
        /// <summary>
        /// 初始化面积信息列表
        /// </summary>
        public void IniList()
        {
            bPointList.Clear();
            bPointList.Columns.Add("行政区域代码", 100);
            bPointList.Columns.Add("面积(m²)", 150);
            bPointList.Columns.Add("平差配赋面积(m²)", 150);
            bPointList.Columns.Add("平差后面积(m²)", 150);
        }

        /// <summary>
        /// 更新面积列表
        /// </summary>
        public void UpdateAreaList()
        {
            IniList();
            List<AdminPolygon> polygons = MainForm.Polygons;
            for (int i = 0; i < polygons.Count; i++)
            {
                ListViewItem item = new ListViewItem(polygons[i].Code);

                if (polygons[i].MapSheet.CalArea != 0)
                {
                    item.SubItems.Add(polygons[i].MapSheet.CalArea.ToString("F4"));//+ "," +polygons[i].Area.ToString("F4"));
                }
                else
                {
                    item.SubItems.Add("");
                }
                if (MainForm.IfStep4 && polygons[i].MapSheet.CalArea != 0)
                {
                    item.SubItems.Add(polygons[i].DArea.ToString("F4"));
                    item.SubItems.Add(polygons[i].AreaAfterControl.ToString("F4"));
                }
                else
                {
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                }
                bPointList.Items.Add(item);
            }
            txtName.Text = MainForm.SheetNum.ToString();
            txtArea.Text = MainForm.SheetArea.ToString("F4");
            if (MainForm.IfStep4)
            {
                textBox3.Text = MainForm.AreaDiffer.ToString("F7");
                if (MainForm.AreaDiffer > 0.001)
                {
                    textBox4.Text = "需要平差";
                    MessageBox.Show("平差完成!");
                }
                else
                {
                    textBox4.Text = "不需要平差";
                    MessageBox.Show("不需要平差!");
                }

            }
                
        }
        #endregion

    }
}
