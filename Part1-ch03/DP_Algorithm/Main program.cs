using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string strResult; 
        public Form1()
        {
            InitializeComponent();
            
        }

        private void toolOpen_Click(object sender, EventArgs e)
        {
            //设置OpenFileDialog空间的属性
            openFileDialog1.Filter = "*.txt|*.txt";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            string strpath = openFileDialog1.FileName;
            //读取原始数据文件
            if (strpath != "")
            {
                StreamReader SR = new StreamReader(strpath);
                string strline = SR.ReadLine();
                string[] sw;
                sw = strline.Split(',');
                double H0 = Convert.ToDouble(sw[1]);
                int i = 0;
                //将原始线状要素的节点信息读入表格中
                while (strline != null)
                {
                    try
                    {
                        sw = strline.Split(',');
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = sw[0];
                        dataGridView1.Rows[i].Cells[1].Value = sw[1];
                        dataGridView1.Rows[i].Cells[2].Value = sw[2];
                        i++;
                        strline = SR.ReadLine();
                    }
                    catch
                    {
                        MessageBox.Show("数据文件格式不正确，请重新选择！", "错误提示");
                    }
                }
                SR.Close();
            }
            //在报告文本控件中输出原始数据的信息
            textBox1.Text = "数据读取成功，共读取线状要素节点为：" + (dataGridView1.Rows.Count -1).ToString() + "个；";
            tabControl1.SelectedIndex = 0;
        }

        private void toolJS_Click(object sender, EventArgs e)
        {
            //从表格中读取节点数据到原始节点的数组中
            int intoriPntNum = dataGridView1.Rows.Count - 1;
            Pnt[] oriPnt = new Pnt[intoriPntNum];
            for (int i = 0; i <= intoriPntNum - 1; i++)
            {
                oriPnt[i] = new Pnt();
                oriPnt[i].ID = Convert.ToInt16 (dataGridView1.Rows[i].Cells[0].Value);
                oriPnt[i].x = Convert.ToDouble (dataGridView1.Rows[i].Cells[1].Value);
                oriPnt[i].y = Convert.ToDouble (dataGridView1.Rows[i].Cells[2].Value);
            }
            //DP算法
            //声明变量和堆栈
            Pnt P0 = new Pnt();//声明线段起点
            Pnt P1 = new Pnt();//声明线段终点
            Stack <Pnt> proPnt= new Stack<Pnt>();//待检查的节点堆栈
            Stack<Pnt> rePnt = new Stack<Pnt>();//压缩结果的堆栈
            int intYuZhi = Convert.ToInt16(toolStripComboBox1.Text);//读取阈值
            P0 = oriPnt[0];//设置初始起点
            P1 = oriPnt[intoriPntNum - 1];//设置初始终点
            rePnt.Push(P0); //初始起点压入结果堆栈
            proPnt.Push(P1);//初始终点压入待检查的节点堆栈
            while (proPnt.Count != 0)//当待检查的节点堆栈元素不为空时，执行循环；若元素为空，则说明压缩算法执行完毕。
            {   //判断起点和终点之间是否还有其他节点，如果有则执行：
                if (P0.ID != P1.ID - 1)
                {
                    //设置一个线段距离最大的节点，并初始化它到线段的距离最小
                    Pnt maxPnt = new Pnt();
                    maxPnt.Dis = 0;
                    //循环线段起点和终点之间的所有其他节点，若距离大于maxPnt则代替它
                    for (int j = P0.ID + 1; j <= P1.ID - 1; j++)
                    {
                        Pnt tempPnt = new Pnt();
                        tempPnt = oriPnt[j];
                        tempPnt.Dis = Distanct(P0, P1, tempPnt);//调用一个自定义函数Distanct，返回值为点到线段的距离。
                        if (tempPnt.Dis > maxPnt.Dis)
                        {
                            maxPnt = tempPnt;
                        }
                    }
                    //找出距离线段距离最远的节点以后，比较其距离和阈值的大小
                    //若大于阈值，则将节点压入到待检查的节点堆栈，并将该节点设置为线段的终点
                    if (maxPnt.Dis > intYuZhi)
                    {
                        P1 = maxPnt;
                        proPnt.Push(maxPnt);
                    }
                    //若小于阈值，则将终点设置为线段起点，并压入到结果堆栈中，并将待检查堆栈顶部的节点设置为线段终点
                    else
                    {
                        P0 = P1;
                        rePnt.Push(P0);
                        proPnt.Pop();
                        if (proPnt.Count != 0)
                        {
                            P1 = proPnt.Peek();
                        }
                    }
                }
                //判断起点和终点之间是否还有其他节点，如果没有则执行：
                else
                {
                    P0 = P1;
                    rePnt.Push(P0);
                    proPnt.Pop();
                    if (proPnt.Count != 0)
                    {
                        P1 = proPnt.Peek();
                    }
                }
            }
            //置换堆栈中节点的顺序
            while (rePnt.Count != 0)
            {
                proPnt.Push(rePnt.Pop());
            }
            //按照从线状要素起点到终点的顺序输出压缩后节点的信息
            textBox1.Text = textBox1.Text + "\r\n" + "压缩成功，压缩后线状要素节点为：" + proPnt.Count + "个；";
            textBox1.Text = textBox1.Text + "\r\n" + "----------------------------------------------------";
            textBox1.Text = textBox1.Text + "\r\n" + "压缩后保留的节点为：\r\n" + "点号（ID），X坐标，Y坐标";
            while (proPnt.Count != 0)
            {
                P0 = proPnt.Pop();
                strResult = strResult + "\r\n" + P0.ID + "," + P0.x + "," + P0.y;
            }
            textBox1.Text = textBox1.Text  + strResult;
            tabControl1.SelectedIndex = 1;
        }
        //自定义函数，计算节点到线段的距离
        private double Distanct(Pnt P0, Pnt P1, Pnt tempPnt)
        {
            double L0,L1,L2;
            L0 = Math.Sqrt((P0.x - P1.x) * (P0.x - P1.x) + (P0.y - P1.y) * (P0.y - P1.y));
            L1 = Math.Sqrt((tempPnt.x - P1.x) * (tempPnt.x - P1.x) + (tempPnt.y - P1.y) * (tempPnt.y - P1.y));
            L2 = Math.Sqrt((P0.x - tempPnt.x) * (P0.x - tempPnt.x) + (P0.y - tempPnt.y) * (P0.y - tempPnt.y));
            double P;
            P = (L0 + L1 + L2) / 2;
            double S;
            S = Math.Sqrt(P  * (P - L0) * (P - L1) * (P - L2));
            double D;
            D = 2 * S / L0;
            return D;
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            //输出压缩后节点信息到TXT文档中
            saveFileDialog1.Filter = "压缩计算结果|*.txt";
            saveFileDialog1.FileName = "压缩计算结果输出";
            saveFileDialog1.ShowDialog();
            string strpath = saveFileDialog1.FileName;
            if (strpath != null)
            {
                System.IO.StreamWriter SW = new System.IO.StreamWriter(strpath);
                SW.Write(strResult);
                SW.Close();
            }
        }

        private void toolData_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void toolReport_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void toolHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show( "线状要素数据压缩的Douglas–Peucker算法","DP_Algorithm V1.0");
        }

    }
}
