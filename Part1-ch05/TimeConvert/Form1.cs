using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeConvert
{
    public partial class Form1 : Form
    {
        private List<Time> Data;
        public Form1()
        {
            InitializeComponent();
        }
        string result;
        private void toolOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Data = FileHelper.Read(openFileDialog1.FileName);
                Algo go = new Algo(Data);
                richTextBox1.Text = go.ToReport1();
            }
        }

        private void toolCal_Click(object sender, EventArgs e)
        {
            Algo go = new Algo(Data);
            result= go.ToReport();
            richTextBox1.Text = go.ToReport();
            
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Write(result,saveFileDialog1.FileName);
            }
        }

        private void toolHelp_Click(object sender, EventArgs e)
        {
            string copyright = "《测绘程序设计试题集（试题5 时间系统转换）》配套程序\n作者：李英冰\n";
            copyright += "武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.7.16";
            richTextBox1.Text = copyright;
        }
    }
}
