using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convolution
{
    public partial class Form1 : Form
    {
        private double[,] M;
        private double[,] N;
        private double[,] V1;
        private double[,] V2;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolOpenM_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                M = FileHelper.ReadM(openFileDialog1.FileName);

                int n = 3;
                string text = Report.Text("N矩阵", M, n, n);

                richTextBox1.Text = text;
            }
        }

        private void toolOpenN_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                N = FileHelper.ReadN(openFileDialog1.FileName);

                int n = 10;
                string text = Report.Text("N矩阵", N, n, n);

                richTextBox1.Text = text;
            }
        }

        private void toolCal_Click(object sender, EventArgs e)
        {
            Algo go=new Algo(M,N);
            V1 = go.Algo1();
            V2 = go.Algo2();
            int n = 10;
            string text = Report.Text("算法1结果", V1, n, n);
            text += Report.Text("算法2结果", V2, n, n);

            richTextBox1.Text = text;
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Write(richTextBox1.Text, saveFileDialog1.FileName);
            }
        }

        private void toolHelp_Click(object sender, EventArgs e)
        {
            string copyright = "《测绘程序设计试题集（试题2 卷积计算）》配套程序\n作者：李英冰\n";
            copyright += "武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.7.8";
            richTextBox1.Text = copyright;
        }
    }
}
