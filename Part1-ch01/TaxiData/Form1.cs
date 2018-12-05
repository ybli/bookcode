using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxiData
{
    public partial class Form1 : Form
    {
        private SessionList Data;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var epoches = FileHelper.Read("T2", openFileDialog1.FileName);
                Data=new SessionList(epoches);
                richTextBox1.Text = "数据读取完成！";
            }
        }

        private void toolCal_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Data.ToString();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Write(Data,saveFileDialog1.FileName);
            }
        }

        private void toolHelp_Click(object sender, EventArgs e)
        {
            string copyright ="《测绘程序设计试题集（试题1 出租车数据计算）》配套程序\n作者：李英冰\n";
            copyright+="武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.7.11";
            richTextBox1.Text = copyright;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
