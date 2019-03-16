using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trop
{
    public partial class Form1 : Form
    {
        private DataEntity Data;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolCal_Click(object sender, EventArgs e)
        {
            Algo go =new Algo(Data);

            richTextBox1.Text = go.Compute();
        }

        private void toolHelp_Click(object sender, EventArgs e)
        {
            string copyright = "《测绘程序设计试题集（试题8 对流层改正）》配套程序\n作者：李英冰\n";
            copyright += "武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.7.29";
            richTextBox1.Text = copyright;
        }

        private void toolOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Data = FileHelper.Read(openFileDialog1.FileName);
                richTextBox1.Text = Data.ToString();
            }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Write(richTextBox1.Text, saveFileDialog1.FileName);
            }
        }
    }
}
