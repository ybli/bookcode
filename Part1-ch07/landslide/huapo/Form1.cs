using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace landside
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<MoniterPoint> shuju;
        public string result;
        public string display;


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                shuju = FileHelper.Read(openFileDialog1.FileName);
                try
                {
                    var reader = new StreamReader(openFileDialog1.FileName);
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line.Length > 0) 
                        {
                            display += line;
                            display += "\r\n";
                        }
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                richTextBox1.Text =display ;
            }

        }

        private void caculate_Click(object sender, EventArgs e)
        {
            string maxname = "M01";
            double max = 0;
            int number=1;
            string res = "--------监测点位的变形速度--------\r\n";
            foreach (var d in shuju)
            {
                res += $"{d.Name}";
                for (int i = 0; i < d.Data.Count - 1; i++)
                {
                    double v = Algo.Velocity(d.Data[i], d[i + 1]);
                    if (v >= max)
                    {
                        maxname = d.Name;
                        max = v;
                        number = i + 1;
                    }
                    res += $",{v:f2}";
                }
                res += "\r\n";
            }

            res += "--------最大变形发生点位及发生时段--------\r\n";
            res += maxname+ $",{number}"+"--"+$"{number+1}\r\n";


            res += "--------相邻点组的应变--------\r\n";
            res += "M01-M02";
            for (int i = 0; i < shuju[0].Data.Count - 1; i++)
            {
                var c1 = shuju[0];
                var c2 = shuju[1];
                double s = Algo.Strain(c1.Data[i], c1.Data[i + 1], c2.Data[i], c2.Data[i + 1]);
                res += $",{s:f8}";

            }
            res += "\r\n";
            res += "M03-M04";
            for (int i = 0; i < shuju[0].Data.Count - 1; i++)
            {
                var c1 = shuju[2];
                var c2 = shuju[3];
                double s = Algo.Strain(c1.Data[i], c1.Data[i + 1], c2.Data[i], c2.Data[i + 1]);
                res += $",{s:f8}";
            }
            result = res;
            richTextBox1.Text = res;

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                FileHelper.Write (result,saveFileDialog1.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
