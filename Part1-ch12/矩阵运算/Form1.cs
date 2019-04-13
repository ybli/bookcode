using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 矩阵运算
{
    public partial class MartixProcess : Form
    {
        public MartixProcess()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 从界面获取输入矩阵
        /// </summary>
        class input
        {
            /// <summary>
            /// 输入界面上存储两个矩阵的控件内容
            /// </summary>
            /// <param name="LeftMartix"></param>
            /// <param name="RightMartix"></param>
            //public input(string LeftMartix, string RightMartix)
            //{
            //    double[,] Left;
            //    double[,] Right;
            //    string[] LeftLine;
            //    string[] RightLine;
            //    string[] Leftelement;
            //    string[] Rightelement;
            //    int leftrow, rightrow;
            //    int leftcolumm, rightcolumn;
            //    LeftLine = LeftMartix.Split('\n');
            //    RightLine = RightMartix.Split('\n');
            //    if (LeftMartix != "")
            //    {
            //        leftrow = LeftMartix.Split('\n').Length;
            //        leftcolumm = LeftLine[0].Split(',').Length;
            //    }
            //    else
            //    {
            //        leftrow = 2;
            //        leftcolumm = 2;
            //    }
            //    if (RightMartix != "")
            //    {
            //        rightrow = RightMartix.Split('\n').Length;
            //        rightcolumn = RightLine[0].Split(',').Length;
            //    }
            //    else
            //    {
            //        rightrow = 2;
            //        rightcolumn = 2;
            //    }
            //    Left = new double[leftrow, leftcolumm];

            //    Right = new double[rightrow, rightcolumn];
            //    if (LeftMartix != "")
            //    {
            //        for (int i = 0; i < LeftLine.Length; i++)
            //        {
            //            Leftelement = LeftLine[i].Split(',');
            //            for (int j = 0; j < Leftelement.Length; j++)
            //                Left[i, j] = double.Parse(Leftelement[j]);
            //        }
            //    }
            //    if (RightMartix != "")
            //    {
            //        for (int i = 0; i < RightLine.Length; i++)
            //        {
            //            Rightelement = RightLine[i].Split(',');
            //            for (int j = 0; j < Rightelement.Length; j++)
            //                Right[i, j] = double.Parse(Rightelement[j]);
            //        }
            //    }
            //    Leftmartix = new Martix(Left);
            //    Rightmartix = new Martix(Right);
            //}
            public input(string InputMartix)
            {
                double[,] martix;
                string[] Line;
                string[] element;
                int row=0;
                int column=0;
                NULL = false;
                while (InputMartix.Contains("\n\n"))//去除多余的回车
                    InputMartix = InputMartix.Replace("\n\n", "\n");
                Line = InputMartix.Split('\n');
                if (InputMartix != " ")
                {
                    for (int i = 0; i < Line.Length; i++)
                    {
                        if (Line[i] != "")
                        {
                            row = InputMartix.Split('\n').Length;
                            Line[i] = Line[i].Replace("，", ",");
                            column = Line[i].Split(',').Length;
                            break;
                        }
                    }
                }
                else
                {
                    row = 2;
                    column = 2;
                }
                martix = new double[row, column];

                if (InputMartix != " ")
                {
                    for (int i = 0; i < Line.Length; i++)
                    {
                        Line[i] = Line[i].Replace("，", ",");
                        if (Line[i]=="")
                        {
                            MessageBox.Show("输入含空字符!");
                            NULL = true;
                        }
                        else
                        {
                            element = Line[i].Split(',');
                            for (int j = 0; j < element.Length; j++)
                                martix[i, j] = double.Parse(element[j]);
                        }
                    }
                }
                Martix = new Martix(martix);
            }
            /// <summary>
            /// 左矩阵
            /// </summary>
            public Martix Martix { get; set; }
            /// <summary>
            /// 是否为空
            /// </summary>
           public bool NULL { get; set; }
        }
        /// <summary>
        /// 转置操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Transpose_Click(object sender, EventArgs e)
        {
            ResultMartix2.Text = null;
            input input = new input(LeftMartix2.Text);
            Martix Result = input.Martix;
            Result = input.Martix.Transpose();
            for (int i = 0; i < Result.Rows; i++)
            {
                for (int j = 0; j < Result.Columns; j++)
                {
                    ResultMartix2.Text += Result[i, j].ToString();
                    if (j != Result.Columns - 1)
                        ResultMartix2.Text += ",";
                }
                ResultMartix2.Text += "\n";
            }
        }
        /// <summary>
        /// 求逆操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Inverse_Click(object sender, EventArgs e)
        {
            ResultMartix2.Text = null;
            input input = new input(LeftMartix2.Text);
            Martix Result = input.Martix;
            Result = input.Martix.Inverse();
            for (int i = 0; i < Result.Rows; i++)
            {
                for (int j = 0; j < Result.Columns; j++)
                {
                    ResultMartix2.Text += Result[i, j].ToString() ;
                    if(j!= Result.Columns-1)
                        ResultMartix2.Text += ",";
                }
                ResultMartix2.Text += "\n";
            }
        }
        /// <summary>
        /// 求行列式操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Determinant_Click(object sender, EventArgs e)
        {
            ResultMartix2.Text = null;
            input input = new input(LeftMartix2.Text);
            double Result = input.Martix.Determinant(input.Martix);
            ResultMartix2.Text = Result.ToString();
        }
        /// <summary>
        /// 双目运算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calculate_Click(object sender, EventArgs e)
        {
            if (radioButton_Add.Checked)//加法运算
            {
                ResultMartix.Text = null;
                input Leftinput = new input(LeftMartix.Text);
                input Rightinput = new input(RightMartix.Text);
                if (!Leftinput.NULL)
                {
                    Martix Result = Leftinput.Martix;
                    Result = Leftinput.Martix + Rightinput.Martix;
                    for (int i = 0; i < Result.Rows; i++)
                    {
                        for (int j = 0; j < Result.Columns; j++)
                        {
                            ResultMartix.Text += Result[i, j].ToString();
                            if (j != Result.Columns - 1)
                                ResultMartix.Text += ",";
                        }
                        ResultMartix.Text += "\n";
                    }
                }
            }
            if (radioButton_Sub.Checked)//减法运算
            {
                ResultMartix.Text = null;
                input Leftinput = new input(LeftMartix.Text);
                input Rightinput = new input(RightMartix.Text);
                if (!Leftinput.NULL)
                {
                    Martix Result = Leftinput.Martix;
                    Result = Leftinput.Martix - Rightinput.Martix;
                    for (int i = 0; i < Result.Rows; i++)
                    {
                        for (int j = 0; j < Result.Columns; j++)
                        {
                            ResultMartix.Text += Result[i, j].ToString();
                            if (j != Result.Columns - 1)
                                ResultMartix.Text += ",";
                        }
                        ResultMartix.Text += "\n";
                    }
                }
            }
            if (radioButton_Mul.Checked)//乘法运算
            {
                ResultMartix.Text = null;
                input Leftinput = new input(LeftMartix.Text);
                input Rightinput = new input(RightMartix.Text);
                if (!Leftinput.NULL)
                {
                    Martix Result = Leftinput.Martix;
                    Result = Leftinput.Martix * Rightinput.Martix;
                    for (int i = 0; i < Result.Rows; i++)
                    {
                        for (int j = 0; j < Result.Columns; j++)
                        {
                            ResultMartix.Text += Result[i, j].ToString();
                            if (j != Result.Columns - 1)
                                ResultMartix.Text += ",";
                        }
                        ResultMartix.Text += "\n";
                    }
                }
            }
            if (radioButton_Converse.Checked)//除法运算1,2,3; 2,1,2; 1,3,4
            {
                ResultMartix.Text = null;
                input Leftinput = new input(LeftMartix.Text);
                input Rightinput = new input(RightMartix.Text);
                if (!Leftinput.NULL)
                {
                    Martix Result = Leftinput.Martix;
                    Result = Leftinput.Martix / Rightinput.Martix;
                    for (int i = 0; i < Result.Rows; i++)
                    {
                        for (int j = 0; j < Result.Columns; j++)
                        {
                            ResultMartix.Text += Result[i, j].ToString();
                            if (j != Result.Columns - 1)
                                ResultMartix.Text += ",";
                        }
                        ResultMartix.Text += "\n";
                    }
                }
            }
        }
    }
}
