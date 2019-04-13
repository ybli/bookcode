using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.ShowDialog();
            StreamReader sr = new StreamReader(openfile.FileName, Encoding.Default);
            try
            {
                textBox1.Clear();
                string file = sr.ReadToEnd();
                file = file.Trim();
                textBox1.Text = file;
            }
            catch (Exception ex) { MessageBox.Show("error!" + ex.Message); }
            finally { sr.Close(); }
        }

        List<string[]> array = new List<string[]>();
        int row = 0;
        int col = 0;
        double sum = 0;
        string sum_output;
        private void button2_Click(object sender, EventArgs e)
        {
            row = textBox1.Lines.Length;
            string[] data;
            for (int i = 0; i < row; i++)
            {
                string nextline = textBox1.Lines[i];
                nextline = Regex.Replace(nextline, @" +", " ");
                data = nextline.Split(',');
                col = data.Length;
                string[] temp = new string[col];
                for (int j = 0; j < col; j++)
                {
                    temp[j] = data[j];
                }
                array.Add(temp);
            }

            double[] s = new double[13];
            for (int i = 0; i < 7; i++)
            {
                double x1=double.Parse(array[i+1][1]);
                double y1=double.Parse(array[i+1][2]);
                double x2=double.Parse(array[i+2][1]);
                double y2=double.Parse(array[i+2][2]);
                s[i] = len(x1, y1, x2, y2);
            }
            s[7] = len(double.Parse(array[1][1]), double.Parse(array[1][2]), double.Parse(array[8][1]), double.Parse(array[8][2]));
            s[8] = len(double.Parse(array[2][1]), double.Parse(array[2][2]), double.Parse(array[8][1]), double.Parse(array[8][2]));
            s[9] = len(double.Parse(array[3][1]), double.Parse(array[3][2]), double.Parse(array[8][1]), double.Parse(array[8][2]));
            s[10] = len(double.Parse(array[3][1]), double.Parse(array[3][2]), double.Parse(array[7][1]), double.Parse(array[7][2]));
            s[11] = len(double.Parse(array[4][1]), double.Parse(array[4][2]), double.Parse(array[7][1]), double.Parse(array[7][2]));
            s[12] = len(double.Parse(array[4][1]), double.Parse(array[4][2]), double.Parse(array[6][1]), double.Parse(array[6][2]));



            double[] area_s = new double[6];
            area_s[0] = area(s[0], s[7], s[8]);
            area_s[1] = area(s[1], s[8], s[9]);
            area_s[2] = area(s[9], s[6], s[10]);
            area_s[3] = area(s[2], s[10], s[11]);
            area_s[4] = area(s[5], s[11], s[12]);
            area_s[5] = area(s[3], s[4], s[12]);

            for (int i = 0; i < 6; i++)
            {
                sum = sum + area_s[i];
            }
            sum = Math.Floor(sum * 1000) / 1000;
            sum_output = sum.ToString();

            for (int i = 0; i < 13; i++)
            {
                s[i] = Math.Floor(s[i] * 1000) / 1000;
            }
                //textBox2.AppendText(sum_output);
                textBox2.AppendText("三角形序号  边1的长度(m)  边2的长度(m)  边3的长度(m)  面积(m2)\r\n");
            textBox2.AppendText("1" + "            " + s[0] + "      " + s[7] + "        " + s[8] + "      " + Math.Floor(area_s[0] * 1000) / 1000 + "\r\n");
            textBox2.AppendText("2" + "            " + s[1] + "      " + s[8] + "        " + s[9] + "      " + Math.Floor(area_s[1] * 1000) / 1000 + "\r\n");
            textBox2.AppendText("3" + "            " + s[6] + "      " + s[9] + "        " + s[10] + "      " + Math.Floor(area_s[2] * 1000) / 1000 + "\r\n");
            textBox2.AppendText("4" + "            " + s[2] + "      " + s[10] + "        " + s[11] + "      " + Math.Floor(area_s[3] * 1000) / 1000 + "\r\n");
            textBox2.AppendText("5" + "            " + s[5] + "      " + s[11] + "        " + s[12] + "      " + Math.Floor(area_s[4] * 1000) / 1000 + "\r\n");
            textBox2.AppendText("6" + "            " + s[3] + "      " + s[4] + "        " + s[12] + "      " + Math.Floor(area_s[5] * 1000) / 1000 + "\r\n");
            textBox2.AppendText("\r\n");
            textBox2.AppendText("地块总面积为：\r\n");
            textBox2.AppendText(sum_output);
            

        }

        string myfile;
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title = "保存文件";
            sf.Filter = "文本文件*.txt|*.txt";
            sf.FilterIndex = 2;
            sf.ShowDialog();
            if(sf.FileName!=null)
            {
                myfile=sf.FileName;
            }
            else{MessageBox.Show("文件名不能为空");}
            StreamWriter sw = new StreamWriter(myfile, false, Encoding.Default);
            try
            {
                foreach (string line in textBox2.Lines)
                {
                    sw.Write(line + "\r\n", Encoding.Default);
                }
                sw.Write("23-郑宇航");
                sw.Flush();
            }
            catch (Exception ex) { MessageBox.Show("error!" + ex.Message); }
            finally { sw.Close(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        double len(double x1,double y1,double x2,double y2)
        {
            double ll=Math.Sqrt((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2));
            return ll;
        }

        double area(double a, double b, double c)
        {
            double ss = (a + b + c) / 2;
            double sx = Math.Sqrt(ss * (ss - a) * (ss - b) * (ss - c));
            return sx;
        }
    }
}
