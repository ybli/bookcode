using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GauCoor_Trans
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Caculate ca = new Caculate();
        bool isNull;//是否正反
        public static int is2 = 0;
        int is3 = 0;//是否选择3度带
        int Row_count = 20;//计算表的行数
        double[] B = null;
        double[] L = null;
        double[] X = null;
        double[] Y = null;
        double[] Y_pu = null;//假定坐标
        string[] all_lines = null;
        bool file_resolve;//判断是否为文件解算
        List<BL> BL = new List<BL>();
        List<Data> in_data = new List<Data>();
        List<Data> Result = new List<Data>();
        Read_in rin = new Read_in();
        public static ell el = new ell();
        public bool call = false;
        public static string report = null;
        int width = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            width = dataGridView2.Width;
            Creat_form(Row_count);
            度带坐标转6度带坐标ToolStripMenuItem.Checked = true;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = true;
            is2 = 1;
            el.a = 6378245;
            el.e2 = 0.00669342162297;
            el.e12 = 0.00673852541468;
            toolStripStatusLabel1.Text = "就绪！";
            toolStripStatusLabel2.Text = "克拉索夫斯基椭球";
            toolStripStatusLabel3.Text = "3度带转6度带";
        }
        public string getreportstr(bool cal)
        {
            if (cal == false)
            {
                return "0";
            }
            string report = null;
            report = "                    计算报告                    \n";
            if (克拉索夫斯基椭球ToolStripMenuItem1.Checked == true)
            {
                report += "克拉索夫斯基椭球\n";
            }
            if (gRS75椭球ToolStripMenuItem1.Checked == true)
            {
                report += "IUGG 1975椭球\n";
            }
            if (gRS80椭球ToolStripMenuItem1.Checked == true)
            {
                report += "CGCS 2000椭球\n";
            }
            if (度带坐标转6度带坐标ToolStripMenuItem.Checked)
            {
                report += "3度带转6度带\n";
                report += "\n";
                report += "3度带坐标\n";
            }
            if (度带坐标转3度带坐标ToolStripMenuItem.Checked)
            {
                report += "6度带转3度带\n";
                report += "\n";
                report += "6度带坐标\n";
            }
            report += "-------------------------------\n";
            report += dataGridView1.Columns[0].HeaderText;
            report += "    ";
            report += dataGridView1.Columns[1].HeaderText;
            report += "    ";
            report += dataGridView1.Columns[2].HeaderText;
            report += "    ";
            report += dataGridView1.Columns[3].HeaderText;
            report += "\n";
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string temp = null;
                temp = dataGridView1[0, i].Value.ToString() + "       " + dataGridView1[1, i].Value.ToString() + "       " + dataGridView1[2, i].Value.ToString() + "       "
                    + dataGridView1[3, i].Value.ToString() + "\n";
                report += temp;
            }
            report += "-------------------------------\n";
            if (度带坐标转6度带坐标ToolStripMenuItem.Checked)
            {
                report += "6度带坐标\n";
            }
            if (度带坐标转3度带坐标ToolStripMenuItem.Checked)
            {
                report += "3度带坐标\n";
            }
            report += "-------------------------------\n";
            report += dataGridView1.Columns[0].HeaderText;
            report += "    ";
            report += dataGridView1.Columns[1].HeaderText;
            report += "    ";
            report += dataGridView1.Columns[2].HeaderText;
            report += "    ";
            report += dataGridView1.Columns[3].HeaderText;
            report += "\n";
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string temp = null;
                temp = dataGridView1[0, i].Value.ToString() + "       " + dataGridView1[1, i].Value.ToString() + "       " + dataGridView1[2, i].Value.ToString() + "       "
                    + dataGridView1[3, i].Value.ToString() + "\n";
                report += temp;
            }
            report += "-------------------------------\n";
            return report;
        }

        /// <summary>
        /// 3度带坐标转6度带坐标表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Creat_form(int n)
        {
            dataGridView2.ColumnCount = 3;
            dataGridView2.RowCount = 1;
            dataGridView2.Width = width - 20;
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.RowHeadersWidth = 10;
            dataGridView2.ColumnHeadersHeight = 50;
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = dataGridView2.Columns[2].Width = (dataGridView2.Width - 52) / 2;
            dataGridView2.Columns[0].Name = "";
            dataGridView2.Columns[1].Name = "3度带坐标";
            dataGridView2.Columns[2].Name = "6度带坐标";

            dataGridView1.ColumnCount = 7;
            dataGridView1.RowCount = n + 1;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersWidth = 10;
            dataGridView1.ColumnHeadersHeight = 20;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = dataGridView1.Columns[2].Width = dataGridView1.Columns[3].Width = dataGridView1.Columns[4].Width = dataGridView1.Columns[5].Width = dataGridView1.Columns[6].Width = (dataGridView1.Width - 70) / 6;
            dataGridView1.Columns[0].Name = "点号";
            dataGridView1.Columns[1].Name = "X坐标（m）";
            dataGridView1.Columns[2].Name = "Y坐标（m）";
            dataGridView1.Columns[3].Name = "Y通用坐标（m）";
            dataGridView1.Columns[4].Name = "X坐标（m）";
            dataGridView1.Columns[5].Name = "Y坐标（m）";
            dataGridView1.Columns[6].Name = "Y通用坐标（m）";
        }
        /// <summary>
        /// 6度带坐标转3度带坐标表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Creat_form6(int n)
        {
            dataGridView2.ColumnCount = 3;
            dataGridView2.RowCount = 1;
            dataGridView2.Width = width - 20;
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.RowHeadersWidth = 10;
            dataGridView2.ColumnHeadersHeight = 50;
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = dataGridView2.Columns[2].Width = (dataGridView2.Width - 52) / 2;
            dataGridView2.Columns[0].Name = "";
            dataGridView2.Columns[1].Name = "6度带坐标";
            dataGridView2.Columns[2].Name = "3度带坐标";

            dataGridView1.ColumnCount = 7;
            dataGridView1.RowCount = n + 1;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersWidth = 10;
            dataGridView1.ColumnHeadersHeight = 20;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = dataGridView1.Columns[2].Width = dataGridView1.Columns[3].Width = dataGridView1.Columns[4].Width = dataGridView1.Columns[5].Width = dataGridView1.Columns[6].Width = (dataGridView1.Width - 70) / 6;
            dataGridView1.Columns[0].Name = "点号";
            dataGridView1.Columns[1].Name = "X坐标（m）";
            dataGridView1.Columns[2].Name = "Y坐标（m）";
            dataGridView1.Columns[3].Name = "Y通用坐标（m）";
            dataGridView1.Columns[4].Name = "X坐标（m）";
            dataGridView1.Columns[5].Name = "Y坐标（m）";
            dataGridView1.Columns[6].Name = "Y通用坐标（m）";
        }

        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "*.txt,*.xls|*.txt;*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(openFileDialog1.FileName);
                all_lines = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                Reader.Close();
                file_resolve = true;
                if (度带坐标转6度带坐标ToolStripMenuItem.Checked == true || 度带坐标转3度带坐标ToolStripMenuItem.Checked == true)
                {
                    if (all_lines.Length == 0)
                    {
                        MessageBox.Show("输入数据不能为空");
                        toolStripStatusLabel1.Text = "数据为空！";
                        return;
                    }
                    int num = 0;
                    for (int i = 0; i < all_lines.Length; i++)
                    {
                        if (all_lines[i] != "")
                        {
                            num++;
                        }
                    }
                    dataGridView1.RowCount = num;
                    in_data = rin.get_XY(all_lines);
                    for (int i = 0; i < in_data.Count; i++)
                    {
                        dataGridView1[0, i].Value = in_data[i].Name;
                        dataGridView1[1, i].Value = in_data[i].XY.X.ToString("f3");
                        dataGridView1[2, i].Value = in_data[i].XY.Y.ToString("f3");
                        dataGridView1[3, i].Value = in_data[i].Yi.ToString("f3");

                    }
                }
                else
                {
                    MessageBox.Show("请先选择功能！");
                    toolStripStatusLabel1.Text = "未选择功能！";
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt|*.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(save.FileName);
                    sw.WriteLine(report);
                    sw.Close();
                    MessageBox.Show("文件保存成功！");
                    toolStripStatusLabel1.Text = "文件保存成功！";
                    return;
                }
                catch
                {
                    MessageBox.Show("文件保存失败！");
                    toolStripStatusLabel1.Text = "文件保存失败！";
                    return;
                }
            }
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void 解算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] num = new int[Row_count];
            int j = 0;
            if (el.a == 0)
            {
                MessageBox.Show("请选择椭球参数！");
                toolStripStatusLabel1.Text = "未选择椭球！";
                return;
            }
            in_data = new List<Data>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                Data data = new Data();
                if (dataGridView1[1, i].Value.ToString() != "" && dataGridView1[3, i].Value.ToString() != "" && dataGridView1[2, i].Value.ToString() != "")
                {
                    data.Name = dataGridView1[0, i].Value.ToString().Trim();
                    data.XY.X = double.Parse(dataGridView1[1, i].Value.ToString().Trim());
                    data.XY.Y = double.Parse(dataGridView1[2, i].Value.ToString().Trim());
                    data.Yi = double.Parse(dataGridView1[3, i].Value.ToString().Trim());
                    in_data.Add(data);
                    num[j] = i; j++;
                }
                else if ((dataGridView1[1, i].Value.ToString() == "" && dataGridView1[3, i].Value.ToString() != "") || (dataGridView1[1, i].Value.ToString() != "" && dataGridView1[3, i].Value.ToString() == ""))
                {
                    MessageBox.Show("输入数据不完整！");
                    toolStripStatusLabel1.Text = "输入数据不完整！";
                    return;
                }
            }
            if (度带坐标转6度带坐标ToolStripMenuItem.Checked)
            {
                try
                {
                    for (int i = 0; i < in_data.Count; i++)
                    {
                        double B = 0;
                        double L = 0;
                        double x = 0, y = 0, yi = 0;
                        double L0 = 0;
                        int n = 0;
                        Data out_data = new Data();
                        L0 = (Math.Floor(in_data[i].Yi / 1000000.0) * 3) * Math.PI / 180.0;
                        Caculate.Gauss_To(el, ref B, ref L, in_data[i].XY.X, in_data[i].XY.Y, L0);
                        n = (int)(L * 180 / Math.PI / 6) + 1;
                        L0 = (6 * n - 3) * Math.PI / 180;
                        Caculate.To_Gauss(el, B, L, ref x, ref y, L0);
                        if (L == 0)
                            yi = 60 * 1000000 + 500000 + y;
                        else
                            yi = n * 1000000 + 500000 + y;
                        out_data.Name = in_data[i].Name;
                        out_data.XY.X = x;
                        out_data.XY.Y = y;
                        out_data.Yi = yi;
                        Result.Add(out_data);
                        dataGridView1[4, i].Value = x.ToString("f6");
                        dataGridView1[5, i].Value = y.ToString("f6");
                        dataGridView1[6, i].Value = yi.ToString("f6");
                        call = true;
                        toolStripStatusLabel1.Text = "计算成功！";
                    }
                }
                catch
                {
                    MessageBox.Show("请输入有效数据！");
                    toolStripStatusLabel1.Text = "数据无效！";
                    return;
                }
            }
            else if (度带坐标转3度带坐标ToolStripMenuItem.Checked)
            {
                try
                {
                    for (int i = 0; i < in_data.Count; i++)
                    {
                        double B = 0;
                        double L = 0;
                        double x = 0, y = 0, yi = 0;
                        double L0 = 0;
                        int n = 0;
                        Data out_data = new Data();
                        L0 = (Math.Floor(in_data[i].Yi / 1000000.0) * 6 - 3) * Math.PI / 180.0;
                        Caculate.Gauss_To(el, ref B, ref L, in_data[i].XY.X, in_data[i].XY.Y, L0);
                        n = (int)((L * 180 / Math.PI - 1.5) / 3) + 1;
                        L0 = (3 * n) * Math.PI / 180;
                        Caculate.To_Gauss(el, B, L, ref x, ref y, L0);
                        if (L <= 1.5 && L >= 0 && is3 == 3)
                            yi = 120 * 1000000 + 500000 + y;
                        else
                            yi = n * 1000000 + 500000 + y;
                        out_data.Name = in_data[i].Name;
                        out_data.XY.X = x;
                        out_data.XY.Y = y;
                        out_data.Yi = yi;
                        Result.Add(out_data);
                        dataGridView1[4, i].Value = x.ToString("f6");
                        dataGridView1[5, i].Value = y.ToString("f6");
                        dataGridView1[6, i].Value = yi.ToString("f6");
                        call = true;
                        toolStripStatusLabel1.Text = "计算成功！";
                    }
                }
                catch
                {
                    MessageBox.Show("请输入有效数据！");
                    toolStripStatusLabel1.Text = "输入数据无效！";
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择功能！");
                toolStripStatusLabel1.Text = "未选择功能！";
                return;
            }
            report = null;
            report = getreportstr(call);
        }

        private void 度带坐标转6度带坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            度带坐标转6度带坐标ToolStripMenuItem.Checked = true;
            度带坐标转3度带坐标ToolStripMenuItem.Checked = false;
            toolStripStatusLabel3.Text = "3度带转6度带";
            Creat_form(20);
        }

        private void 度带坐标转3度带坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            度带坐标转6度带坐标ToolStripMenuItem.Checked = false;
            度带坐标转3度带坐标ToolStripMenuItem.Checked = true;
            toolStripStatusLabel3.Text = "6度带转3度带";
            Creat_form6(20);
        }

        private void 克拉索夫斯基椭球ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            is2 = 1;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = true;
            gRS75椭球ToolStripMenuItem1.Checked = false;
            gRS80椭球ToolStripMenuItem1.Checked = false;
            double f = 1.0 / 298.3;
            el.a = 6378245;
            double b = (1 - f) * el.a;
            double temp = Math.Sqrt((el.a * el.a - b * b) / el.a / el.a);
            el.e2 = temp * temp;
            temp = Math.Sqrt((el.a * el.a - b * b) / b / b);
            el.e12 = temp * temp;
            toolStripStatusLabel2.Text = "克拉索夫斯基椭球";
        }

        private void gRS75椭球ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            is2 = 2;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = false;
            gRS75椭球ToolStripMenuItem1.Checked = true;
            gRS80椭球ToolStripMenuItem1.Checked = false;
            double f = 1.0 / 298.257;
            el.a = 6378140;
            double b = (1 - f) * el.a;
            double temp = Math.Sqrt((el.a * el.a - b * b) / el.a / el.a);
            el.e2 = temp * temp;
            temp = Math.Sqrt((el.a * el.a - b * b) / b / b);
            el.e12 = temp * temp;
            toolStripStatusLabel2.Text = "IUGG 1975椭球";
        }

        private void gRS80椭球ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            is2 = 3;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = false;
            gRS75椭球ToolStripMenuItem1.Checked = false;
            gRS80椭球ToolStripMenuItem1.Checked = true;
            double f = 1.0 / 298.257222101;
            el.a = 6378137;
            double b = (1 - f) * el.a;
            double temp = Math.Sqrt((el.a * el.a - b * b) / el.a / el.a);
            el.e2 = temp * temp;
            temp = Math.Sqrt((el.a * el.a - b * b) / b / b);
            el.e12 = temp * temp;
            toolStripStatusLabel2.Text = "CGCS2000 椭球";
        }

        private void 清空数据ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = false;
            gRS75椭球ToolStripMenuItem1.Checked = false;
            gRS80椭球ToolStripMenuItem1.Checked = false;
            度带坐标转6度带坐标ToolStripMenuItem.Checked = false;
            度带坐标转3度带坐标ToolStripMenuItem.Checked = false;
            dataGridView1.Rows.Clear();
            Row_count = 20;
            Creat_form(Row_count);
            all_lines = null;
            BL.Clear();
            in_data = new List<Data>();
            Result = new List<Data>();
        }

        private void 邻带换算ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trans t = new Trans();
            t.ShowDialog();
        }

        private void 报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report r = new Report();
            r.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "*.txt,*.xls|*.txt;*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(openFileDialog1.FileName);
                all_lines = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                Reader.Close();
                file_resolve = true;
                if (度带坐标转6度带坐标ToolStripMenuItem.Checked == true || 度带坐标转3度带坐标ToolStripMenuItem.Checked == true)
                {
                    if (all_lines.Length == 0)
                    {
                        MessageBox.Show("输入数据不能为空");
                        toolStripStatusLabel1.Text = "数据为空！";
                        return;
                    }
                    int num = 0;
                    for (int i = 0; i < all_lines.Length; i++)
                    {
                        if (all_lines[i] != "")
                        {
                            num++;
                        }
                    }
                    dataGridView1.RowCount = num;
                    in_data = rin.get_XY(all_lines);
                    for (int i = 0; i < in_data.Count; i++)
                    {
                        dataGridView1[0, i].Value = in_data[i].Name;
                        dataGridView1[1, i].Value = in_data[i].XY.X.ToString("f3");
                        dataGridView1[2, i].Value = in_data[i].XY.Y.ToString("f3");
                        dataGridView1[3, i].Value = in_data[i].Yi.ToString("f3");

                    }
                }
                else
                {
                    MessageBox.Show("请先选择功能！");
                    toolStripStatusLabel1.Text = "未选择功能！";
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt|*.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(save.FileName);
                    sw.WriteLine(report);
                    sw.Close();
                    MessageBox.Show("文件保存成功！");
                    toolStripStatusLabel1.Text = "文件保存成功！";
                }
                catch
                {
                    MessageBox.Show("文件保存失败！");
                    toolStripStatusLabel1.Text = "文件保存失败！";
                    return;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int[] num = new int[Row_count];
            int j = 0;
            if (el.a == 0)
            {
                MessageBox.Show("请选择椭球参数！");
                toolStripStatusLabel1.Text = "未选择椭球！";
                return;
            }
            in_data = new List<Data>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                Data data = new Data();
                if (dataGridView1[1, i].Value.ToString() != "" && dataGridView1[3, i].Value.ToString() != "" && dataGridView1[2, i].Value.ToString() != "")
                {
                    data.Name = dataGridView1[0, i].Value.ToString().Trim();
                    data.XY.X = double.Parse(dataGridView1[1, i].Value.ToString().Trim());
                    data.XY.Y = double.Parse(dataGridView1[2, i].Value.ToString().Trim());
                    data.Yi = double.Parse(dataGridView1[3, i].Value.ToString().Trim());
                    in_data.Add(data);
                    num[j] = i; j++;
                }
                else if ((dataGridView1[1, i].Value.ToString() == "" && dataGridView1[3, i].Value.ToString() != "") || (dataGridView1[1, i].Value.ToString() != "" && dataGridView1[3, i].Value.ToString() == ""))
                {
                    MessageBox.Show("输入数据不完整！");
                    toolStripStatusLabel1.Text = "输入数据不完整！";
                    return;
                }
            }
            if (度带坐标转6度带坐标ToolStripMenuItem.Checked)
            {
                try
                {
                    for (int i = 0; i < in_data.Count; i++)
                    {
                        double B = 0;
                        double L = 0;
                        double x = 0, y = 0, yi = 0;
                        double L0 = 0;
                        int n = 0;
                        Data out_data = new Data();
                        L0 = (Math.Floor(in_data[i].Yi / 1000000.0) * 3) * Math.PI / 180.0;
                        Caculate.Gauss_To(el, ref B, ref L, in_data[i].XY.X, in_data[i].XY.Y, L0);
                        n = (int)(L * 180 / Math.PI / 6) + 1;
                        L0 = (6 * n - 3) * Math.PI / 180;
                        Caculate.To_Gauss(el, B, L, ref x, ref y, L0);
                        if (L == 0)
                            yi = 60 * 1000000 + 500000 + y;
                        else
                            yi = n * 1000000 + 500000 + y;
                        out_data.Name = in_data[i].Name;
                        out_data.XY.X = x;
                        out_data.XY.Y = y;
                        out_data.Yi = yi;
                        Result.Add(out_data);
                        dataGridView1[4, i].Value = x.ToString("f6");
                        dataGridView1[5, i].Value = y.ToString("f6");
                        dataGridView1[6, i].Value = yi.ToString("f6");
                        call = true;
                        toolStripStatusLabel1.Text = "计算成功！";
                    }
                }
                catch
                {
                    MessageBox.Show("请输入有效数据！");
                    toolStripStatusLabel1.Text = "数据无效！";
                    return;
                }
            }
            else if (度带坐标转3度带坐标ToolStripMenuItem.Checked)
            {
                try
                {
                    for (int i = 0; i < in_data.Count; i++)
                    {
                        double B = 0;
                        double L = 0;
                        double x = 0, y = 0, yi = 0;
                        double L0 = 0;
                        int n = 0;
                        Data out_data = new Data();
                        L0 = (Math.Floor(in_data[i].Yi / 1000000.0) * 6 - 3) * Math.PI / 180.0;
                        Caculate.Gauss_To(el, ref B, ref L, in_data[i].XY.X, in_data[i].XY.Y, L0);
                        n = (int)((L * 180 / Math.PI - 1.5) / 3) + 1;
                        L0 = (3 * n) * Math.PI / 180;
                        Caculate.To_Gauss(el, B, L, ref x, ref y, L0);
                        if (L <= 1.5 && L >= 0 && is3 == 3)
                            yi = 120 * 1000000 + 500000 + y;
                        else
                            yi = n * 1000000 + 500000 + y;
                        out_data.Name = in_data[i].Name;
                        out_data.XY.X = x;
                        out_data.XY.Y = y;
                        out_data.Yi = yi;
                        Result.Add(out_data);
                        dataGridView1[4, i].Value = x.ToString("f6");
                        dataGridView1[5, i].Value = y.ToString("f6");
                        dataGridView1[6, i].Value = yi.ToString("f6");
                        call = true;
                        toolStripStatusLabel1.Text = "计算成功！";
                    }
                }
                catch
                {
                    MessageBox.Show("请输入有效数据！");
                    toolStripStatusLabel1.Text = "输入数据无效！";
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择功能！");
                toolStripStatusLabel1.Text = "未选择功能！";
                return;
            }
            report = null;
            report = getreportstr(call);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = false;
            gRS75椭球ToolStripMenuItem1.Checked = false;
            gRS80椭球ToolStripMenuItem1.Checked = false;
            度带坐标转6度带坐标ToolStripMenuItem.Checked = false;
            度带坐标转3度带坐标ToolStripMenuItem.Checked = false;
            dataGridView1.Rows.Clear();
            Row_count = 20;
            Creat_form(Row_count);
            all_lines = null;
            BL.Clear();
            in_data = new List<Data>();
            Result = new List<Data>();
            toolStripStatusLabel1.Text = "就绪！";
        }
    }
}
