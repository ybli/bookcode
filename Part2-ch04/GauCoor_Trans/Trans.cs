using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GauCoor_Trans
{
    public partial class Trans : Form
    {
        public Trans()
        {
            InitializeComponent();
        }

        Caculate ca = new Caculate();
        bool call = false;
        public string getreportstr(bool cal)
        {
            if (cal == false)
            {
                return "0";
            }
            string report = null;
            report = "                    计算报告                    \n";
            if (Form1.is2 == 1)
            {
                report += "克拉索夫斯基椭球\n";
            }
            if (Form1.is2 == 2)
            {
                report += "IUGG 1975椭球\n";
            }
            if (Form1.is2 == 3)
            {
                report += "CGCS 2000椭球\n";
            }
            if (radioButton1.Checked)
            {
                report += "3度带邻带换算\n";
            }
            if (radioButton2.Checked)
            {
                report += "6度带邻带换算\n";
            }
            string temp;
            temp = string.Format("X(m):{0}\n", textBox3.Text.Trim());
            report += temp;
            report += string.Format("Y(m):{0}\n", textBox8.Text.Trim());
            report += "----------------结果---------------\r\n";
            report += string.Format("左侧邻带：  {0}   {1}       (X/Y)\n", textBox5.Text.Trim(), textBox4.Text.Trim());
            report += string.Format("右侧邻带：  {0}   {1}       (X/Y)\n", textBox10.Text.Trim(), textBox9.Text.Trim());
            report += "-----------------------------------\n";
            return report;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != null && textBox8.Text != null && (radioButton2.Checked || radioButton1.Checked))
            {
                try
                {
                    double x = double.Parse(textBox3.Text.Trim());
                    double y1 = double.Parse(textBox8.Text.Trim());
                    double y = y1 - (Math.Floor(y1 / 1000000)) * 1000000 - 500000;
                    double B = 0;
                    double L = 0;
                    double left_x = 0;
                    double left_y = 0;
                    double left_y1 = 0;
                    double right_x = 0;
                    double right_y = 0;
                    double right_y1 = 0;
                    double L0 = 0;
                    int n = 0;
                    if (radioButton1.Checked)
                    {
                        L0 = (Math.Floor(y1 / 1000000.0) * 3) * Math.PI / 180.0;
                        Caculate.Gauss_To(Form1.el, ref B, ref L, x, y, L0);
                        n = (int)((L * 180 / Math.PI - 1.5) / 3) + 1;
                        L0 = (3 * n) * Math.PI / 180;
                        Caculate.To_Gauss(Form1.el, B, L, ref right_x, ref right_y, L0 + 3.0 / 180 * Math.PI);
                        if (L <= 1.5 && L >= 0 )
                            right_y1 = 120 * 1000000 + 500000 + right_y;
                        else
                            right_y1 = (n+1) * 1000000 + 500000 + right_y;
                        Caculate.To_Gauss(Form1.el, B, L, ref left_x, ref left_y, L0 - 3.0 / 180 * Math.PI);
                        if (L <= 1.5 && L >= 0)
                            left_y1 = 120 * 1000000 + 500000 + left_y;
                        else
                            left_y1 = (n-1) * 1000000 + 500000 + left_y;
                    }
                    if (radioButton2.Checked)
                    {
                        L0 = (Math.Floor(y1 / 1000000.0) * 6 - 3) * Math.PI / 180.0;
                        Caculate.Gauss_To(Form1.el, ref B, ref L, x, y, L0);
                        n = (int)(L * 180 / Math.PI / 6) + 1;
                        L0 = (6 * n - 3) * Math.PI / 180;
                        Caculate.To_Gauss(Form1.el, B, L, ref right_x, ref right_y, L0 + 6.0 / 180 * Math.PI);
                        if (L == 0)
                            right_y1 = 60 * 1000000 + 500000 + right_y;
                        else
                            right_y1 = (n+1) * 1000000 + 500000 + right_y;
                        Caculate.To_Gauss(Form1.el, B, L, ref left_x, ref left_y, L0 - 6.0 / 180 * Math.PI);
                        if (L == 0)
                            left_y1 = 60 * 1000000 + 500000 + left_y;
                        else
                            left_y1 = (n-1) * 1000000 + 500000 + left_y;
                    }
                    textBox5.Text = left_x.ToString("f4");
                    textBox4.Text = left_y1.ToString("f4");
                    textBox10.Text = right_x.ToString("f4");
                    textBox9.Text = right_y1.ToString("f4");
                    call = true;
                    Form1.report = null;
                    Form1.report = getreportstr(call);
                }
                catch
                {
                    MessageBox.Show("请输入有效数据！");
                    return;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report r = new Report();
            r.ShowDialog();
        }

        private void Trans_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
    }
}
