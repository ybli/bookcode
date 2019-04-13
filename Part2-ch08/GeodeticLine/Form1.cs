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

namespace geodetic_line
{
    public partial class Form1 : Form
    {
        string[] all_lines = null;
        Input input = new Input();//输入数据
        Output output = new Output();//输出数据
        My_Founctions mf = new My_Founctions();
        Caculate ca = new Caculate();
        public static double Geodesy;
        public static string report;
        public bool call = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 2;
            dataGridView2.RowCount = 1;
            dataGridView1[0, 0].Value = "P" + "1";
            dataGridView1[0, 1].Value = "P" + "2";
            input.e2 = 0.006693421622966;
            input.c = 6399698.9017827110;
            Geodesy = 1;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = true;
            iUGG1975ToolStripMenuItem1.Checked = false;
            cGCS2000ToolStripMenuItem.Checked = false;
            toolStripStatusLabel2.Text = "克拉索夫斯基椭球";
        }
        public string reportstr(bool call)
        {
            string report = null;
            report = "                    计算报告                    \r\n";
            if (克拉索夫斯基椭球ToolStripMenuItem1.Checked == true)
            {
                report += "克拉索夫斯基椭球\n\r";
            }
            if (iUGG1975ToolStripMenuItem1.Checked == true)
            {
                report += "iUGG1975椭球\n\r";
            }
            report += "P1点坐标（dd.mmsss）：" + dataGridView1[1, 0].Value.ToString() + "   " + dataGridView1[2, 0].Value.ToString() + "       B/L" + "\r\n";
            report += "P2点坐标（dd.mmss）：" + dataGridView1[1, 1].Value.ToString() + "   " + dataGridView1[2, 1].Value.ToString() + "       B/L" + "\r\n";
            report += "-------------------------结果-----------------------------\r\n";
            report += "大地线长（m）：" + dataGridView2[0, 0].Value.ToString() + "\r\n";
            return report;
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "txt|*.txt";
            if (op.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(op.FileName);
                all_lines = File.ReadAllLines(op.FileName, Encoding.Default);
                Reader.Close();
            }
            else
            {
                return;
            }
            try
            {
                string[] temp = null;
                all_lines[0] += (" " + all_lines[1]);//将两行数据合并为一行
                temp = all_lines[0].Split(new char[] { ' ' });
                int m = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (m == 0 && temp[i] != "")
                    {
                        dataGridView1[1, 0].Value = temp[i].Trim();
                        input.B1 = mf.ddmmssTorad(double.Parse(temp[i].Trim()));
                        m++;
                        continue;
                    }
                    if (m == 1 && temp[i] != "")
                    {
                        dataGridView1[2, 0].Value = temp[i].Trim();
                        input.L1 = mf.ddmmssTorad(double.Parse(temp[i].Trim()));
                        m++;
                        continue;
                    }
                    if (m == 2 && temp[i] != "")
                    {
                        dataGridView1[1, 1].Value = temp[i].Trim();
                        input.B2 = mf.ddmmssTorad(double.Parse(temp[i].Trim()));
                        m++;
                        continue;
                    }
                    if (m == 3 && temp[i] != "")
                    {
                        dataGridView1[2, 1].Value = temp[i].Trim();
                        input.L2 = mf.ddmmssTorad(double.Parse(temp[i].Trim()));
                        continue;
                    }
                }
            }
            catch
            {
                MessageBox.Show("文件格式错误！");
                toolStripStatusLabel1.Text = "文件格式错误！";
                return;
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "txt|*.txt";
            if (sa.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sa.FileName);
                try
                {
                    sw.WriteLine(reportstr(call));
                    sw.Close();
                    toolStripStatusLabel1.Text = "文件保存成功！";
                }
                catch
                {
                    MessageBox.Show("保存数据有误！请检查");
                    toolStripStatusLabel1.Text = "保存数据有误！";
                    return;
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void 克拉索夫斯基椭球ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            input.e2 = 0.006693421622966;
            input.c = 6399698.9017827110;
            Geodesy = 1;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = true;
            iUGG1975ToolStripMenuItem1.Checked = false;
            cGCS2000ToolStripMenuItem.Checked = false;
            toolStripStatusLabel2.Text = "克拉索夫斯基椭球";
        }

        private void iUGG1975ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            input.e2 = 0.006694384999588;
            input.c = 6399596.6519880105;
            Geodesy = 2;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = false;
            iUGG1975ToolStripMenuItem1.Checked = true;
            cGCS2000ToolStripMenuItem.Checked = false;
            toolStripStatusLabel2.Text = "IUGG 1975椭球";
        }

        private void cGCS2000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            input.e2 = 0.00669438002290;
            input.c = 6399593.62586;
            Geodesy = 3;
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = false;
            iUGG1975ToolStripMenuItem1.Checked = false;
            cGCS2000ToolStripMenuItem.Checked = true;
            toolStripStatusLabel2.Text = "CGCS2000 椭球";
        }

        private void 清空数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.RowCount = 2;
            dataGridView2.RowCount = 1;
            all_lines = null;
            input = new Input();
            output = new Output();
            克拉索夫斯基椭球ToolStripMenuItem1.Checked = false;
            iUGG1975ToolStripMenuItem1.Checked = false;
        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null; dataGridView2.CurrentCell = null;
            try
            {

                input.B1 = mf.ddmmssTorad(double.Parse(dataGridView1[1, 0].Value.ToString().Trim()));
                input.L1 = mf.ddmmssTorad(double.Parse(dataGridView1[2, 0].Value.ToString().Trim()));
                input.B2 = mf.ddmmssTorad(double.Parse(dataGridView1[1, 1].Value.ToString().Trim()));
                input.L2 = mf.ddmmssTorad(double.Parse(dataGridView1[2, 1].Value.ToString().Trim()));
                output = ca.Bessal_P(input, Geodesy);
                dataGridView2[0, 0].Value = output.S.ToString("f4");
                call = true;
                toolStripStatusLabel1.Text = "计算成功！";
            }
            catch
            {
                MessageBox.Show("输入数据有误！");
                toolStripStatusLabel1.Text = "输入数据有误！";
                return;
            }
            report = reportstr(call);
        }

        private void 报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report r = new Report();
            r.ShowDialog();
        }
    }
}
