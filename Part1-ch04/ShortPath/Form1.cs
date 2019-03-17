using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortPath
{
    public partial class Form1 : Form
    {
        public string re;
        private Graph Data;
        public Form1()
        {
            InitializeComponent();
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
                FileHelper.Write(re, saveFileDialog1.FileName);
            }
        }

        private void toolCal_Click(object sender, EventArgs e)
        {
            Algo go=new Algo(Data);
            richTextBox1.Text = go.ToString();
            re = go.ToString();
        }

        private void toolHelp_Click(object sender, EventArgs e)
        {
            string copyright = "《测绘程序设计试题集（试题4 最短路径计算）》配套程序\n作者：李英冰\n";
            copyright += "武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.7.15";
            richTextBox1.Text = copyright;
        }
    }
}
