using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 双像空间前方交会
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double x1, y1, x2, y2, f;
        double o1, o2, p1, p2, q1, q2;
        double Xs1,Ys1,Zs1,Xs2,Ys2,Zs2;
        double a11, a12, a13, b11, b12, b13, c11, c12, c13, a21, a22, a23, b21, b22, b23, c21, c22, c23;
        double pI = Math.PI;
        double Bu, Bv, Bw;
        double N1, N2;
        double u1, v1, w1, u2, v2, w2;
        double X, Y, Z;

        private void button1_Click(object sender, EventArgs e)
        {
             //读取文件，并显示到文本框          
         
            StreamReader sr  =  new  StreamReader(@"D:bhl1.txt",System.Text.Encoding.Default);  
            Xs1 = double.Parse(sr.ReadLine());             
            Ys1 = double.Parse(sr.ReadLine());             
            Zs1 = double.Parse(sr.ReadLine());             
            o1 = double.Parse(sr.ReadLine());             
            p1 = double.Parse(sr.ReadLine());             
            q1 = double.Parse(sr.ReadLine());             
            x1 = double.Parse(sr.ReadLine());             
            y1 = double.Parse(sr.ReadLine());             
            f = double.Parse(sr.ReadLine());             
            Xs2 = double.Parse(sr.ReadLine());             
            Ys2 = double.Parse(sr.ReadLine());             
            Zs2 = double.Parse(sr.ReadLine()); 
            o2 = double.Parse(sr.ReadLine());             
            p2 = double.Parse(sr.ReadLine());             
            q2 = double.Parse(sr.ReadLine());             
            x2 = double.Parse(sr.ReadLine());             
            y2 = double.Parse(sr.ReadLine());             
            f = double.Parse(sr.ReadLine());
            textBox12.Text = Xs1.ToString("0.00000"); 
            textBox13.Text = Ys1.ToString("0.00000");             
            textBox14.Text = Zs1.ToString("0.00000");            
            textBox15.Text = o1.ToString("0.00000");             
            textBox16.Text = p1.ToString("0.00000");             
            textBox17.Text = q1.ToString("0.00000");             
            textBox18.Text = x1.ToString("0.00000");             
            textBox19.Text = y1.ToString("0.00000");             
            textBox20.Text = f.ToString("0.00000");             
            textBox21.Text = Xs2.ToString("0.00000");             
            textBox22.Text = Ys2.ToString("0.00000");             
            textBox23.Text = Zs2.ToString("0.00000");             
            textBox24.Text = o2.ToString("0.00000");            
            textBox25.Text = p2.ToString("0.00000");             
            textBox26.Text = q2.ToString("0.00000");             
            textBox27.Text = x2.ToString("0.00000");             
            textBox28.Text = y2.ToString("0.00000");
            textBox29.Text = f.ToString("0.00000");

            //求旋转矩阵中的元素 

            a11 = Math.Cos(o1 / 360 * 2 * pI) * Math.Cos(q1 / 360 * 2 * pI) - Math.Sin(o1 / 360 * 2 * pI) * Math.Sin(p1 / 360 * 2 * pI) * Math.Sin(q1 / 360 * 2 * pI);              
            a12 = -Math.Cos((o1 / 360) * 2 * pI) * Math.Sin((q1 / 360) * 2 * pI) - Math.Sin((o1 / 360) * 2 * pI) * Math.Sin((p1 / 360) * 2 * pI) * Math.Sin((q1 / 360) * 2 * pI); 
            a13 = -Math.Sin((o1 / 360) * 2 * pI) * Math.Cos((p1 / 360) * 2 * pI); 
            b11 = Math.Cos((p1 / 360) * 2 * pI) * Math.Sin(q1 / 360 * 2 * pI); 
            b12 = Math.Cos((p1 / 360) * 2 * pI) * Math.Cos(q1 / 360 * 2 * pI); 
            b13 = -Math.Sin(p1 / 360 * 2 * pI); 
            c11 = Math.Sin(o1 / 360 * 2 * pI) * Math.Cos(q1 / 360 * 2 * pI) + Math.Cos(o1 / 360 * 2 * pI) * Math.Sin(p1 / 360 * 2 * pI) * Math.Sin(q1 / 360 * 2 * pI); 
            c12 = -Math.Sin(p1 / 360 * 2 * pI) * Math.Sin(q1 / 360 * 2 * pI) + Math.Cos(o1 / 360 * 2 * pI) * Math.Sin(p1 / 360 * 2 * pI) * Math.Cos(q1 / 360 * 2 * pI); 
            c13 = Math.Cos(o1 / 360 * 2 * pI) * Math.Cos(p1 / 360 * 2 * pI); 
            a21 = Math.Cos(o2 / 360 * 2 * pI) * Math.Cos(q2 / 360 * 2 * pI) - Math.Sin(o2 / 360 * 2 * pI) * Math.Sin(p2 / 360 * 2 * pI) * Math.Sin(q2 / 360 * 2 * pI); 
            a22 = -Math.Cos(o2 / 360 * 2 * pI) * Math.Sin(q2 / 360 * 2 * pI) - Math.Sin(o2 / 360 * 2 * pI) * Math.Sin(p2 / 360 * 2 * pI) * Math.Sin(q2 / 360 * 2 * pI); 
            a23 = -Math.Sin(o2 / 360 * 2 * pI) * Math.Cos(p2 / 360 * 2 * pI); 
            b21 = Math.Cos(p2 / 360 * 2 * pI) * Math.Sin(q2 / 360 * 2 * pI); 
            b22 = Math.Cos(p2 / 360 * 2 * pI) * Math.Cos(q2 / 360 * 2 * pI); 
            b23 = -Math.Sin(p2 / 360 * 2 * pI); 
            c21 = Math.Sin(o2 / 360 * 2 * pI) * Math.Cos(q2 / 360 * 2 * pI) + Math.Cos(o2 / 360 * 2 * pI) * Math.Sin(p2 / 360 * 2 * pI) * Math.Sin(q2 / 360 * 2 * pI); 
            c22 = -Math.Sin(p2 / 360 * 2 * pI) * Math.Sin(q2 / 360 * 2 * pI) + Math.Cos(o2 / 360 * 2 * pI) * Math.Sin(p2 / 360 * 2 * pI) * Math.Cos(q2 / 360 * 2 * pI); 
            c23 = Math.Cos(o2 / 360 * 2 * pI) * Math.Cos(p2 / 360 * 2 * pI);

            //摄影基线的三个分量
            Bu = Xs2 - Xs1;
            Bv = Ys2 - Ys1;
            Bw = Zs2 - Zs1; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
             //地面点坐标                     
            X = Xs1 + N1 * u1;              
            Y = 0.5 * ((Ys1 + N1 * v1) + (Ys2 + N2 * v2));             
            Z = Zs1 + N1 * w1;              
            textBox1.Text = X.ToString("0.00000");             
            textBox2.Text = Y.ToString("0.00000");          
            textBox3.Text = Z.ToString("0.00000"); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //投影系数
            N1 = (Bu * w2 - Bw * u2) / (u1 * w2 - u2 * w1);             
            N2 = (Bu * w1 - Bw * u1) / (u1 * w2 - u2 * w1);
            textBox4.Text = N1.ToString("0.00000");
            textBox5.Text = N2.ToString("0.00000");            
        }

        private void button4_Click(object sender, EventArgs e)
        {
        
            //像空间辅助坐标 
            u1 = a11 * x1 + a12 * y1 + a13 * (f);             
            v1 = b11 * x1 + b12 * y1 + b13 * (f);             
            w1 = c11 * x1 + c12 * y1 + c13 * (f);               
            u2 = a21 * x2 + a22 * y2 + a23 * (f);             
            v2 = b21 * x2 + b22 * y2 + b23 * (f);             
            w2 = c21 * x2 + c22 * y2 + c23 * (f);
            textBox6.Text = u1.ToString("0.00000");
            textBox7.Text = v1.ToString("0.00000");
            textBox8.Text = w1.ToString("0.00000");
            textBox9.Text = u2.ToString("0.00000");
            textBox10.Text = v2.ToString("0.00000");
            textBox11.Text = w2.ToString("0.00000"); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
