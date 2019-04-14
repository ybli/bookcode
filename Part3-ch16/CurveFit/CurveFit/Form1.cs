using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CurveFit
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 全局变量
        /// </summary>
        #region
        public List<MyPoint> global_mypoint_list = new List<MyPoint>();
        public List<MyCurve> global_mycurve_list = new List<MyCurve>();
        #endregion



        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string file_path = openFileDialog();
            try
            {
                global_mypoint_list = readPointFile(file_path);
            }
            catch
            {
                MessageBox.Show("文件有误!");
                return;
            }
            updateTable(global_mypoint_list);
            updateChart(global_mypoint_list);
        }

        /// <summary>
        /// 更新表格
        /// </summary>
        /// <param name="mypoint_list"></param>
        public void updateTable(List<MyPoint> mypoint_list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", Type.GetType("System.String"));
            table.Columns.Add("x", Type.GetType("System.String"));
            table.Columns.Add("y", Type.GetType("System.String"));

            foreach (MyPoint po in mypoint_list)
            {
                DataRow row = table.NewRow();
                row["ID"] = po.ID;
                row["x"] = po.x.ToString();
                row["y"] = po.y.ToString();
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
        }


        /// <summary>
        /// 更新画图
        /// </summary>
        /// <param name="mypoint_list"></param>
        public void updateChart(List<MyPoint> mypoint_list, List<MyCurve> mycurve_list = null)
        {
            chart2.Titles.Clear();

            chart2.ChartAreas[0].AxisX.Title = "曲线拟合";
            chart2.ChartAreas[0].AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart2.ChartAreas[0].AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            this.chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            this.chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;

            int count = chart2.Series.Count;
            for (int j = 0; j < count; j++)
            {
                chart2.Series.RemoveAt(0);
            }
            int i = 0;
            foreach (MyPoint po in mypoint_list)
            {
                chart2.Series.Add(po.ID);

                chart2.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                chart2.Series[i].Points.Clear();
                chart2.Series[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;

                chart2.Series[i].Points.AddXY(po.x, po.y);
                chart2.Series[i].MarkerSize = 7;
                chart2.Series[i].Label = i.ToString();

                chart2.Series[i].IsVisibleInLegend = false;

                chart2.Series[i].Color = Color.Red;
                i++;
            }

            if (mycurve_list != null)
            {
                chart2.Series.Add("line");
                chart2.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart2.Series[i].Points.Clear();
                chart2.Series[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
                chart2.Series[i].IsVisibleInLegend = false;
                chart2.Series[i].Color = Color.Blue;

                foreach (MyCurve mycurve in mycurve_list)
                {
                    double z = 0;

                    while (z < 1)
                    {
                        double x = mycurve.p0 + mycurve.p1 * z + mycurve.p2 * z * z + mycurve.p3 * z * z * z;
                        double y = mycurve.q0 + mycurve.q1 * z + mycurve.q2 * z * z + mycurve.q3 * z * z * z;
                        chart2.Series[i].Points.AddXY(x, y);
                        z += 0.01;
                    }
                }
            }

        }

        /// <summary>
        /// 用户选择文件
        /// </summary>
        /// <returns></returns>
        public string openFileDialog()
        {
            string file_path = "";
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "文件(*.txt)|*.txt";
            op.ShowDialog();
            file_path = op.FileName;
            return file_path;
        }

        /// <summary>
        /// 读输入文件
        /// </summary>
        /// <param name="file_path"></param>
        /// <returns></returns>
        public List<MyPoint> readPointFile(string file_path)
        {
            List<MyPoint> mypoint_list = new List<MyPoint>();
            StreamReader sr = new StreamReader(file_path);
            while (!sr.EndOfStream)
            {
                string text = sr.ReadLine();
                string[] str_split = text.Split(',');
                mypoint_list.Add(new MyPoint(str_split[0], double.Parse(str_split[1]), double.Parse(str_split[2])));
            }
            return mypoint_list;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //global_mycurve_list = PointToCurve.builtCurve(global_mypoint_list, false);
            global_mycurve_list = PointToCurve.builtCurve(global_mypoint_list, true);

            updateChart(global_mypoint_list, global_mycurve_list);
            updateReport(global_mypoint_list, global_mycurve_list, true);
        }

        public void updateReport(List<MyPoint> mypoint_list, List<MyCurve> mycurve_list, bool is_close)
        {
            double x_min = 0, x_max = 0, y_min = 0, y_max = 0;
            getBorder(mypoint_list, ref x_min, ref x_max, ref y_min, ref y_max);
            textBox1.Text = "";

            textBox1.Text += "\t\t结果报告\r\n";
            textBox1.Text += "------------基本信息------------\r\n";
            textBox1.Text += "总点数:" + mypoint_list.Count.ToString() + "\r\n";
            textBox1.Text += "x边界:" + x_min.ToString() + "至" + x_max.ToString() + "\r\n";
            textBox1.Text += "y边界:" + y_min.ToString() + "至" + y_max.ToString() + "\r\n";

            if (is_close)
            {
                textBox1.Text += "是否闭合:是\r\n\r\n";
            }
            else
            {
                textBox1.Text += "是否闭合:否\r\n\r\n";
            }

            textBox1.Text += "------------计算结果------------\r\n";
            textBox1.Text += "说明:两点之间的曲线方程为:\r\n";
            textBox1.Text += "x=p0+p1*z+p2*z*z+p3*z*z*z\r\ny=q0+q1*z+q2*z*z+q3*z*z*z\r\n其中z为两点之间的弦长变量[0,1]\r\n" + "\r\n";
            textBox1.Text += "起点ID\t起点x\t起点y\t终点ID\t终点x\t终点y\tp0\tp1\tp2\tp3\tq0\tq1\tq2\tq3\r\n";
            foreach (MyCurve mycurve in mycurve_list)
            {
                textBox1.Text += mycurve.mypoint_start.ID + "\t";
                textBox1.Text += mycurve.mypoint_start.x.ToString("0.000") + "\t";
                textBox1.Text += mycurve.mypoint_start.y.ToString("0.000") + "\t";

                textBox1.Text += mycurve.mypoint_end.ID + "\t";
                textBox1.Text += mycurve.mypoint_end.x.ToString("0.000") + "\t";
                textBox1.Text += mycurve.mypoint_end.y.ToString("0.000") + "\t";

                textBox1.Text += mycurve.p0.ToString("0.000") + "\t";
                textBox1.Text += mycurve.p1.ToString("0.000") + "\t";
                textBox1.Text += mycurve.p2.ToString("0.000") + "\t";
                textBox1.Text += mycurve.p3.ToString("0.000") + "\t";
                textBox1.Text += mycurve.q0.ToString("0.000") + "\t";
                textBox1.Text += mycurve.q1.ToString("0.000") + "\t";
                textBox1.Text += mycurve.q2.ToString("0.000") + "\t";
                textBox1.Text += mycurve.q3.ToString("0.000") + "\r\n\r\n";

            }
            textBox1.Text += "保留三位小数";
        }


        public void getBorder(List<MyPoint> mypoint_list, ref double x_min, ref double x_max,
            ref double y_min, ref double y_max)
        {
            x_max = double.MinValue;
            x_min = double.MaxValue;
            y_max = double.MinValue;
            y_min = double.MaxValue;

            foreach (MyPoint point in mypoint_list)
            {
                if (point.x < x_min)
                {
                    x_min = point.x;
                }
                if (point.y < y_min)
                {
                    y_min = point.y;
                }
                if (point.y > y_max)
                {
                    y_max = point.y;
                }
                if (point.x > x_max)
                {
                    x_max = point.x;
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            global_mycurve_list = PointToCurve.builtCurve(global_mypoint_list, false);

            updateChart(global_mypoint_list, global_mycurve_list);
            updateReport(global_mypoint_list, global_mycurve_list, false);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string file_path = saveFileDialog("dxf");
            if (file_path == "")
            {
                return;
            }
            if (global_mycurve_list.Count==0)
            {
                MessageBox.Show("不存在可保存的曲线!");
                return;
            }
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(generateDxf(global_mypoint_list,global_mycurve_list));
            sw.Close();
        }


        public string generateDxf(List<MyPoint> mypoint_list, List<MyCurve> mycurve_list)
        {
            
            string str_dxf = "";
            str_dxf += "  0\r\n";
            str_dxf += "SECTION\r\n";
            str_dxf += "  2\r\n";
            str_dxf += "ENTITIES\r\n";

            //点
            for (int i = 0; i < mypoint_list.Count; i++)
            {
                str_dxf += "  0\r\n";
                str_dxf += "POINT\r\n";
                str_dxf += "  8\r\n";
                str_dxf += "0\r\n";
                str_dxf += " 10\r\n";
                str_dxf += mypoint_list[i].x.ToString() + "\r\n";
                str_dxf += " 20\r\n";
                str_dxf += mypoint_list[i].y.ToString() + "\r\n";
            }

            //线
            foreach (MyCurve mycurve in mycurve_list)
            {
                double z = 0;
                double z_next = 0.01;
                while (z < 1)
                {
                    double x = mycurve.p0 + mycurve.p1 * z + mycurve.p2 * z * z + mycurve.p3 * z * z * z;
                    double y = mycurve.q0 + mycurve.q1 * z + mycurve.q2 * z * z + mycurve.q3 * z * z * z;

                    double x_next = mycurve.p0 + mycurve.p1 * z_next + mycurve.p2 * z_next * z_next + mycurve.p3 * z_next * z_next * z_next;
                    double y_next = mycurve.q0 + mycurve.q1 * z_next + mycurve.q2 * z_next * z_next + mycurve.q3 * z_next * z_next * z_next;

                    str_dxf += "  0\r\n";
                    str_dxf += "LINE\r\n";
                    str_dxf += "  8\r\n";
                    str_dxf += "0\r\n";
                    str_dxf += " 10\r\n";
                    str_dxf += x.ToString() + "\r\n";
                    str_dxf += " 20\r\n";
                    str_dxf += y.ToString() + "\r\n";
                    str_dxf += " 11\r\n";
                    str_dxf += x_next.ToString() + "\r\n";
                    str_dxf += " 21\r\n";
                    str_dxf += y_next.ToString() + "\r\n";

                    z_next += 0.01;
                    z += 0.01;
                }
            }
            str_dxf += "  0\r\n";
            str_dxf += "ENDSEC\r\n";
            str_dxf += "  0\r\n";
            str_dxf += "EOF\r\n";
            return str_dxf;
        }

        /// <summary>
        /// 让用户选择保存路径
        /// </summary>
        /// <param name="type">保存文件的后缀</param>
        /// <returns></returns>
        public string saveFileDialog(string type)
        {
            string localFilePath = "";
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "文件（*." + type + " ）|*." + type;

            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
            }

            return localFilePath;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string file_path = saveFileDialog("txt");
            if (file_path == "")
            {
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("不存在可保存的报告!");
                return;
            }
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(textBox1.Text);
            sw.Close();
        }

        private void 打开点txt文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_path = openFileDialog();
            try
            {
                global_mypoint_list = readPointFile(file_path);
            }
            catch
            {
                MessageBox.Show("文件有误!");
                return;
            }
            updateTable(global_mypoint_list);
            updateChart(global_mypoint_list);
        }

        private void 保存报告txt文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_path = saveFileDialog("txt");
            if (file_path == "")
            {
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("不存在可保存的报告!");
                return;
            }
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(textBox1.Text);
            sw.Close();
        }

        private void 保存图形dxf文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_path = saveFileDialog("dxf");
            if (file_path == "")
            {
                return;
            }
            if (global_mycurve_list.Count == 0)
            {
                MessageBox.Show("不存在可保存的曲线!");
                return;
            }
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(generateDxf(global_mypoint_list, global_mycurve_list));
            sw.Close();
        }

        private void 闭合拟合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global_mycurve_list = PointToCurve.builtCurve(global_mypoint_list, true);

            updateChart(global_mypoint_list, global_mycurve_list);
            updateReport(global_mypoint_list, global_mycurve_list, true);
        }

        private void 不闭合拟合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global_mycurve_list = PointToCurve.builtCurve(global_mypoint_list, false);

            updateChart(global_mypoint_list, global_mycurve_list);
            updateReport(global_mypoint_list, global_mycurve_list, false);
        }

        private void 原始点数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void 图形界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void 计算报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            double width = chart2.ChartAreas[0].AxisX.Maximum - chart2.ChartAreas[0].AxisX.Minimum;
            double height = chart2.ChartAreas[0].AxisY.Maximum - chart2.ChartAreas[0].AxisY.Minimum;
            double x_cen = chart2.ChartAreas[0].AxisX.Maximum - width / 2;
            double y_cen = chart2.ChartAreas[0].AxisY.Maximum - height / 2;

            chart2.ChartAreas[0].AxisX.Maximum = x_cen + width / 2 * 0.8;
            chart2.ChartAreas[0].AxisX.Minimum = x_cen - width / 2 * 0.8;
            chart2.ChartAreas[0].AxisY.Maximum = y_cen + height / 2 * 0.8;
            chart2.ChartAreas[0].AxisY.Minimum = y_cen - height / 2 * 0.8;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            double width = chart2.ChartAreas[0].AxisX.Maximum - chart2.ChartAreas[0].AxisX.Minimum;
            double height = chart2.ChartAreas[0].AxisY.Maximum - chart2.ChartAreas[0].AxisY.Minimum;
            double x_cen = chart2.ChartAreas[0].AxisX.Maximum - width / 2;
            double y_cen = chart2.ChartAreas[0].AxisY.Maximum - height / 2;

            chart2.ChartAreas[0].AxisX.Maximum = x_cen + width / 2 * 1.2;
            chart2.ChartAreas[0].AxisX.Minimum = x_cen - width / 2 * 1.2;
            chart2.ChartAreas[0].AxisY.Maximum = y_cen + height / 2 * 1.2;
            chart2.ChartAreas[0].AxisY.Minimum = y_cen - height / 2 * 1.2;
        }
    }
}
