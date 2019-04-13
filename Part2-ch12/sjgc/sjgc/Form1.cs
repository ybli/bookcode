using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace sjgc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private int flag = 0;    //用于标志是否为正确有效的数据标志
        private int n;            //用于标志当前已被选中的记录  
        private double bihecha;  //用来记录闭合差
        private double[] a;   //全局数组
        private Font printFont;
        private int lineNo = 0;


        private void tianjia_Click(object sender, EventArgs e)
        {

            shujujiaoyan();
            if (flag == 1)
            {
                DataGridViewRow hang1 = new DataGridViewRow();
                DataGridViewTextBoxCell text1 = new DataGridViewTextBoxCell();
                text1.Value = dataGridView1.RowCount;    //
                hang1.Cells.Add(text1);
                DataGridViewTextBoxCell text2 = new DataGridViewTextBoxCell();
                text2.Value = ceduan.Text;            //测段
                hang1.Cells.Add(text2);

                DataGridViewTextBoxCell text3 = new DataGridViewTextBoxCell();
                text3.Value = wanghan.Text;        //往返
                hang1.Cells.Add(text3);
           
                DataGridViewTextBoxCell text4 = new DataGridViewTextBoxCell();
                text4.Value = xieju.Text;     //斜距
                hang1.Cells.Add(text4);
             
                DataGridViewTextBoxCell text5 = new DataGridViewTextBoxCell();
                text5.Value = yiqigao.Text;     //仪器高
                hang1.Cells.Add(text5);
          
                DataGridViewTextBoxCell text6 = new DataGridViewTextBoxCell();
                text6.Value = mubiaogao.Text;    //目标高
                hang1.Cells.Add(text6);
              
                DataGridViewTextBoxCell text7 = new DataGridViewTextBoxCell();
                text7.Value = chuizhijiao.Text;     //垂直角
                hang1.Cells.Add(text7);


                //DataGridViewTextBoxCell text8 = new DataGridViewTextBoxCell();
                //text8.Value = gaocheng.Text;         //高程
                //hang1.Cells.Add(text8);
                dataGridView1.Rows.Add(hang1);
                qingchu();
                ceduan.Focus();
            }
        }

        private void tihuan_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("确实要替换吗？", "消息框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                shujujiaoyan();
                
                if (flag == 1)
                {
                    dataGridView1[1, n].Value = ceduan.Text;          //测段
                    dataGridView1[2, n].Value = wanghan.Text;        //往返
                    dataGridView1[3, n].Value = xieju.Text;         //斜距
                    dataGridView1[4, n].Value = yiqigao.Text;          //仪器高
                    dataGridView1[5, n].Value = mubiaogao.Text;     //目标高
                    dataGridView1[6, n].Value = chuizhijiao.Text; //垂直角              

                    MessageBox.Show("替换成功！");
                    qingchu();
                }
            }

        }

        private void shujujiaoyan()   //数据校验
        {
            if (ceduan.Text == "" & wanghan.Text == "" & xieju.Text == "" & yiqigao.Text == "" & mubiaogao.Text == "" & chuizhijiao.Text == "" & gaocheng.Text == "")
                MessageBox.Show("数据为空，不能添加");
             else if (gongchengmingcheng.Text == "")
            {
                MessageBox.Show("工程名称为空！");
                gongchengmingcheng.Focus();
                flag = 0;
            }
            else if (gongchenglixing.Text == "")
            {
                MessageBox.Show("工程类型不能为空！");
                gaocheng.Focus();
                flag = 0;
            }
            else if (dengji.Text == "")
            {
                MessageBox.Show("等级不能为空！");
                dengji.Focus();
                flag = 0;
            }
            else if (wanghan.Text == "")
            {
                MessageBox.Show("往返不能为空");
                wanghan.Focus();
                flag = 0;
            }
            else if (xieju.Text == "")
            {
                MessageBox.Show("斜距不能为空");
                xieju.Focus();
                flag = 0;
            }
         
            else if (yiqigao.Text == "")
            {
                MessageBox.Show("仪器高不能为空");
                yiqigao.Focus();
                flag = 0;
            }
         
            else if (mubiaogao.Text == "")
            {
                MessageBox.Show("目标高不能为空");
                mubiaogao.Focus();
                flag = 0;
            }
           
            else if (chuizhijiao.Text == "   °  ′  ″")
            {
                MessageBox.Show("垂直角不能为空");
                chuizhijiao.Focus();
                flag = 0;
            }
               
             else if (jiaodupanduan(chuizhijiao.Text) == 1)                
            {     
                MessageBox.Show("垂直角输入非法字符，请重新输入！");
                chuizhijiao.Text = "   °  ′  ″";
                chuizhijiao.Focus();               
                flag = 0;
            }      

            else
                flag = 1;
        }
        private void qingchu()  //数据清除
        {
            ceduan.Text = "";
            wanghan.Text = "";
            xieju.Text = "";
            yiqigao.Text = "";
            mubiaogao.Text = "";
            chuizhijiao.Text = "";
            gaocheng.Text = "";
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            n = dataGridView1.CurrentCell.RowIndex;
            ceduan.Text = dataGridView1[1, n].Value.ToString();          //测段
            wanghan.Text = dataGridView1[2, n].Value.ToString();         //往返
            xieju.Text = dataGridView1[3, n].Value.ToString();         //斜距
            yiqigao.Text = dataGridView1[4, n].Value.ToString();          //仪器高
            mubiaogao.Text = dataGridView1[5, n].Value.ToString();     //目标高
           chuizhijiao.Text = dataGridView1[6, n].Value.ToString();  //垂直角              
         //  gaocheng.Text = dataGridView1[7, n].Value.ToString();      //高程

            // MessageBox.Show(Convert.ToString(n+1));
        }

        private void chanchu_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("确实要删除吗？", "消息框", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(n);
            }

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            n = dataGridView1.CurrentCell.RowIndex;
            ceduan.Text = dataGridView1[1, n].Value.ToString();          //测段
            wanghan.Text = dataGridView1[2, n].Value.ToString();         //往返
            xieju.Text = dataGridView1[3, n].Value.ToString();         //斜距
            yiqigao.Text = dataGridView1[4, n].Value.ToString();          //仪器高
            mubiaogao.Text = dataGridView1[5, n].Value.ToString();     //目标高
            chuizhijiao.Text = dataGridView1[6, n].Value.ToString();  //垂直角              
       //     gaocheng.Text = dataGridView1[7, n].Value.ToString();      //高程
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("确认要删除该行数据吗？", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {// 如果不是 OK，则取消。
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            qingchu();

        }

         private double jiaoduzhuanhuan(string jiaodu)   //角度转换
        {
            double jiaodu1;
            int du1, fen1, miao1;//度，分，秒中间转换量
            string du, fen, miao;   //度，分，秒            
            du1 = jiaodu.IndexOf("°");
            fen1 = jiaodu.IndexOf("′");
            miao1 = jiaodu.IndexOf("″");
            du = jiaodu.Substring(0, du1);
            fen = jiaodu.Substring(du1 + 1, 2);
            miao = jiaodu.Substring(fen1 + 1, 2);
            jiaodu1 = double.Parse(du) + double.Parse(fen) / 60 + double.Parse(miao) / 3600;    //角度轮换成功  
            return jiaodu1;

        }
        private int  jiaodupanduan( string jiaodu)  //角度判断
        {
            int jiaodupanduanflag = 0;
            int du1, fen1, miao1;//度，分，秒中间转换量
            string du, fen, miao;   //度，分，秒        
            du1 = jiaodu.IndexOf("°");    //取出°的位置
            fen1 = jiaodu.IndexOf("′");   //取出′的位置
            miao1 = jiaodu.IndexOf("″");  //取出″的位置
            du = jiaodu.Substring(0, du1);
            fen = jiaodu.Substring(du1 + 1, 2);
            miao = jiaodu.Substring(fen1 + 1, 2);
            if (float.Parse(du) > 360)
                jiaodupanduanflag = 1;
            if (float.Parse(fen) > 60)
                jiaodupanduanflag = 1;
            if (float.Parse(miao) > 60)
                jiaodupanduanflag = 1;    
            return jiaodupanduanflag;
        }

        private void 计算高差ToolStripMenuItem_Click(object sender, EventArgs e)    //高差
        {
            double   yiqi, mubiaogao,xieju,guancejiaodu1,qiqiucha;
            string guancejiaodu;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                xieju= double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());   //斜距        
                yiqi = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());   //仪器高    
                mubiaogao  = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());   //目标高           
                guancejiaodu =dataGridView1.Rows[i].Cells[6].Value.ToString();       //垂直角            
                guancejiaodu1 = Math.Tan(jiaoduzhuanhuan(guancejiaodu));          
                          
               
                if (i % 2 == 0)
                {
                    qiqiucha = double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());  //气球差
                    dataGridView1.Rows[i].Cells[9].Value = Math.Round(xieju * guancejiaodu1 + yiqi - mubiaogao + qiqiucha, 2); //高差
                }
                else
                {
                    qiqiucha = double.Parse(dataGridView1.Rows[i-1].Cells[8].Value.ToString());  //气球差
                    dataGridView1.Rows[i].Cells[9].Value = Math.Round(xieju * guancejiaodu1 + yiqi - mubiaogao + qiqiucha, 2); //高差
                
                }
            }
            限差检验.Enabled = true;

        }  

        private void gancejuli_KeyPress(object sender, KeyPressEventArgs e)
        {
            string ch = xieju.Text.Trim();
            for (int i = 0; i < ch.Length; i++)
                if (ch[i] < 48 || ch[i] > 57)
                {
                    if (ch[i] != 46)
                    {
                        MessageBox.Show("输入非法字符，请重新输入！");
                        xieju.Clear();
                        xieju.Focus();
                    }
                }

        }

        private void yiqigao_KeyPress(object sender, KeyPressEventArgs e)
        {
            string ch = yiqigao.Text.Trim();
            for (int i = 0; i < ch.Length; i++)
                if (ch[i] < 48 || ch[i] > 57)
                {
                    if (ch[i] != 46)
                    {
                        MessageBox.Show("输入非法字符，请重新输入！");
                        yiqigao.Clear();
                        yiqigao.Focus();
                    }
                }
        }

        private void mubiaogao_KeyPress(object sender, KeyPressEventArgs e)
        {
            string ch = mubiaogao.Text.Trim();
            for (int i = 0; i < ch.Length; i++)
                if (ch[i] < 48 || ch[i] > 57)
                {
                    if (ch[i] != 46)
                    {
                        MessageBox.Show("输入非法字符，请重新输入！");
                        mubiaogao.Clear();
                        mubiaogao.Focus();
                    }
                }
        }

        private void gaocheng_KeyPress(object sender, KeyPressEventArgs e)
        {
            string ch =gaocheng.Text.Trim();
            for (int i = 0; i < ch.Length; i++)
                if (ch[i] < 48 || ch[i] > 57)
                {
                    if (ch[i] != 46)
                    {
                        MessageBox.Show("输入非法字符，请重新输入！");
                        gaocheng.Clear();
                        gaocheng.Focus();
                    }
                }
        }

        private void 限差检验ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double sum = 0.0;//sum用于记录高差总合
            double  behexiancha = 0.0;


            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                sum = sum + double.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString());    //平均值
            }
            if (gongchenglixing.Text == "闭合路线")
            {
                bihecha = Math.Round(sum - (double.Parse(zhongdiangaocheng.Text) - double.Parse(gaocheng.Text)));
            }
            if (dengji.Text == "四等")
            {
                behexiancha = Math.Round(20 * Math.Sqrt(sum));
                if (bihecha < behexiancha)
                {
                    MessageBox.Show("符合限差要求！"+"闭合差为："+bihecha.ToString());
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i = i + 1)
                    {
                        if (i%2==0)
                            dataGridView1.Rows[i].Cells[10].Value = "T";
                        else
                            dataGridView1.Rows[i].Cells[10].Value = "     ";
                    }

                }
                else
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i = i + 1)
                    {
                        if (i%2==0)
                           dataGridView1.Rows[i].Cells[10].Value = "F";
                        else
                           dataGridView1.Rows[i].Cells[10].Value = "    ";

                    }
                    MessageBox.Show("限差为：  " + bihecha.ToString() + " ;   " + "超出限差范围!");
                }
            }
            else
            {
                behexiancha = Math.Round(30 * Math.Sqrt(sum), 2);
                if (bihecha < behexiancha)
                    MessageBox.Show("符合限差要求！");
                else
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i = i + 1)
                    {
                        if(i%2==0)
                        dataGridView1.Rows[i].Cells[11].Value = "F";
                        else
                            dataGridView1.Rows[i].Cells[11].Value = "     ";
                    }
               
                    MessageBox.Show("限差为：" + bihecha.ToString() + ";" + "超出限差范围!");
                }
            }
            平均值.Enabled = true;

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gongchenglixing.Text == "附合线路")
            {
                zhongdiangaocheng.Visible = true;
                label17.Visible = true;
                label18.Visible = true;

                label7.Visible = true;
                gaocheng.Visible = true;
                label12.Visible = true;
            
            }
            else
            {

                label7.Visible = true;
                gaocheng.Visible = true ;
                label12.Visible = true ;

                zhongdiangaocheng.Visible = false ;
                label17.Visible = false ;
                label18.Visible = false ;
            
            }
                

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            zhongdiangaocheng.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label7.Visible = false;
            gaocheng.Visible = false;
            label12.Visible = false;
            改正后高差.Enabled = false ;
            气球差.Enabled = false;
            高差.Enabled = false;
            限差检验.Enabled = false;
            平均值.Enabled = false;
            改正数.Enabled = false;
            改正后高差.Enabled = false;



          



        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("闭合差为：" + bihecha.ToString());
                
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)   //球气差
        {
            double D=0.0,p=0.0,f;
            double xieju,chuzhijiao1;
            int m = 0;  //用于记录数组容量
            string chuzhijiao;
            m = dataGridView1.Rows.Count - 1;
            a = new double[m];    //动态数组 a用来存取平距中间值


            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                xieju = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());   //斜距      
                chuzhijiao = dataGridView1.Rows[i].Cells[6].Value.ToString();//垂直角
           
                chuzhijiao1 = Math.Cos(jiaoduzhuanhuan(chuzhijiao));  
                D = xieju * chuzhijiao1;
                p = D * D / 12756274;
                f = Math.Round (0.85 * p,8);
                a[i] = f;
                
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i = i + 1)
            {
                if (i % 2 == 0)
                   dataGridView1.Rows[i].Cells[8].Value =Math.Round ((a[i] + a[i + 1]) / 2,8);//球气差
                else
                   dataGridView1.Rows[i].Cells[8].Value ="     ";
                
            }

            
               
            高差.Enabled = true;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)     //平距
        {
            double D = 0.0;
            int m = 0;   //用于记录数组容量
            double xieju, chuzhijiao1;
            string chuzhijiao;
            m = dataGridView1.Rows.Count - 1;
            a = new double[m];    //动态数组 a用来存取平距中间值
            
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                xieju = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());   //斜距      
                chuzhijiao = dataGridView1.Rows[i].Cells[6].Value.ToString();//垂直角
                chuzhijiao1 = Math.Cos(jiaoduzhuanhuan(chuzhijiao));
                D = Math.Round (xieju * chuzhijiao1,2);               
                a[i] = D;                          
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i = i + 1)    //斜距显示控制
            {
                if(i % 2 ==0)
                    dataGridView1.Rows[i].Cells[7].Value = (a[i] + a[i + 1]) / 2;
                else
                    dataGridView1.Rows[i].Cells[7].Value = "    ";

                
            }
            气球差.Enabled = true;
        }

        private void 平均值_Click(object sender, EventArgs e)    //平均高差值
        {
            double gaocha1,gaocha2;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i=i+1)          
            {
                if (i%2==0)
                { 
                    gaocha1=double.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString());
                    gaocha2=double.Parse(dataGridView1.Rows[i+1].Cells[9].Value.ToString());
                    dataGridView1.Rows[i].Cells[11].Value = (gaocha1 + gaocha2) / 2;
                }
                else
                    dataGridView1.Rows[i].Cells[11].Value = "          ";
            }
            改正数.Enabled = true;
        }

        private void 改正数_Click(object sender, EventArgs e)    //高差改正数
        {
            double sum = 0.0;
            int m = 0;  //用于记录数组容量           
            m = dataGridView1.Rows.Count - 1;
            a = new double[m];    //动态数组 a用来存取平距中间值


            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)     //求出平距总和
            { 
                if ( i%2==0 )
                sum = sum + double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
            
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (i % 2 == 0)
                    a[i] = -bihecha * double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()) / sum;
                else
                    a[i] = -bihecha * double.Parse(dataGridView1.Rows[i-1].Cells[7].Value.ToString()) / sum;
              
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (i%2==0)
                   dataGridView1.Rows[i].Cells[12].Value = a[i].ToString();   //改正数
                else
                    dataGridView1.Rows[i].Cells[12].Value = "      "; 
                 
            }
            改正后高差.Enabled = true;

        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)  //高程计算
        {
           double  j=0;
            if (gongchenglixing.Text == "闭合路线")
            {
                if (gaocheng.Text == "")
                {
                    MessageBox.Show("起点高程为空！");
                    gaocheng.Focus();
                }
                else
                {
                    dataGridView1.Rows[0].Cells[13].Value = double.Parse(gaocheng.Text.Trim()) + double.Parse(dataGridView1.Rows[0].Cells[12].Value.ToString());

                    for (int i = 2; i < dataGridView1.Rows.Count - 1; i = i + 1)
                    {
                        if (i%2==0)
                           dataGridView1.Rows[i].Cells[13].Value = double.Parse(dataGridView1.Rows[i - 2].Cells[13].Value.ToString()) + double.Parse(dataGridView1.Rows[i].Cells[12].Value.ToString());
                        else 
                            dataGridView1.Rows[i].Cells[13].Value="     ";
                    }
                }
            }
            else
            {
                if (gaocheng.Text == "")
                {
                    MessageBox.Show("起点高程为空！");
                    gaocheng.Focus();
                }
                else if (zhongdiangaocheng.Text == "")
                {
                    MessageBox.Show("终点高程为空！");
                    zhongdiangaocheng.Focus();
                }
                else 
                {
                    dataGridView1.Rows[0].Cells[13].Value = double.Parse(gaocheng.Text.Trim()) + double.Parse(dataGridView1.Rows[0].Cells[12].Value.ToString());

                    for (int i = 2; i < dataGridView1.Rows.Count - 1; i = i + 2)
                    {
                        if (i % 2 == 0)
                        {
                            dataGridView1.Rows[i].Cells[13].Value = double.Parse(dataGridView1.Rows[i - 2].Cells[13].Value.ToString()) + double.Parse(dataGridView1.Rows[i].Cells[12].Value.ToString());
                            j = double.Parse(dataGridView1.Rows[i].Cells[13].Value.ToString());
                        }
                        else
                            dataGridView1.Rows[i].Cells[13].Value = "    ";

                     }
                    if (j==double.Parse((zhongdiangaocheng.Text.Trim())))
                    {
                        MessageBox.Show("已闭合！");
                    }
                    else
                        MessageBox.Show("未闭合！");
                }

              
            
            }
        }

        private void 精度评定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)    //打开文件
        {
            dataGridView1.Rows.Clear();
            OpenFileDialog openFile = new OpenFileDialog();//创建打开文件对话框
            openFile.Filter = "文本文件|*.txt";//定义打开文件的类型
            openFile.ShowDialog();//打开文件对话框
            string path = openFile.FileName;
            if (path == "")
            {
                return;
            }
            string[] str = File.ReadAllLines(path, Encoding.Default);
          //  MessageBox.Show(str.Length.ToString());
            for (int i = 0; i < str.Length-1; i++)
            {
                int j = 0;
                string[] s = str[i].Split(new char[] { ',' });   //以行为单位进行读取
                if (i == 0)                                    //读取基本内容
                {
                    gongchengmingcheng.Text = s[j++];
                    gongchenglixing.Text = s[j++];
                    dengji.Text = s[j++];
                
                }
                else
                {
                    //       MessageBox.Show(i.ToString());

                    dataGridView1.Rows.Add(s[j++], s[j++], s[j++], s[j++], s[j++], s[j++], s[j++]);                //读取表格内容
                }
            }


        }
        private void duqujiaodu(string jiaodu)   //读取角度
        {
            //double jiaodu1;
            //int du1, fen1, miao1;//度，分，秒中间转换量
            //string du, fen, miao;              //度，分，秒
            //du1 = jiaodu.IndexOf(".");

            //MessageBox.Show(du1.ToString());
            //fen1 = jiaodu.IndexOf("′");
            //miao1 = jiaodu.IndexOf("″");
            //du = jiaodu.Substring(0, du1);
            //fen = jiaodu.Substring(du1 + 1, 2);
            //miao = jiaodu.Substring(fen1 + 1, 2);
            //jiaodu1 = double.Parse(du) + double.Parse(fen) / 60 + double.Parse(miao) / 3600;
            //return jiaodu1;

        
        }


        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();//创建文件保存对象
            saveFile.Filter = "文本文件|*.txt";//定义保存文件格式
            saveFile.ShowDialog();
            string path = saveFile.FileName;
            if (path == "")
            {
                return;
            }
            int row = dataGridView1.Rows.Count;
            int j;
            string[] str = new string[row];

          //  MessageBox.Show(row.ToString());
            for (int i = 0; i < row-1; i++)
            {
                //if (i == 0)
                //{
                //    str[i] = "序号    " + "测段    " + "往返   " + "斜距    " + "仪器高   " + "目标高    " + "垂直角    " + "平距    " + "气球差   " + "高差   " + "超限标志   " + "高差平均值   " + "改正数    " + "高程改正数  " + "\r\n";
                //}
                //else
                {
                  
                    j = 0;
                    str[i] = str[i] + dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + "   ," +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " ;
               //   MessageBox.Show(i.ToString());
                
                
                }
                         
              
            }
            File.WriteAllLines(path, str, Encoding.Default);//写入文件
            MessageBox.Show("保存成功");//提示保存成功
        }

        private void gongchenglixing_TextUpdate(object sender, EventArgs e)
        {
            if (gongchenglixing.Text == "附合线路")
            {
                zhongdiangaocheng.Visible = true;
                label17.Visible = true;
                label18.Visible = true;

                label7.Visible = true;
                gaocheng.Visible = true;
                label12.Visible = true;

            }
            else
            {

                label7.Visible = true;
                gaocheng.Visible = true;
                label12.Visible = true;

                zhongdiangaocheng.Visible = false;
                label17.Visible = false;
                label18.Visible = false;

            }
        }

        private void gongchenglixing_TextChanged(object sender, EventArgs e)
        {
            if (gongchenglixing.Text == "附合线路")
            {
                zhongdiangaocheng.Visible = true;
                label17.Visible = true;
                label18.Visible = true;

                label7.Visible = true;
                gaocheng.Visible = true;
                label12.Visible = true;

            }
            else
            {

                label7.Visible = true;
                gaocheng.Visible = true;
                label12.Visible = true;

                zhongdiangaocheng.Visible = false;
                label17.Visible = false;
                label18.Visible = false;

            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OpenFileDialog openFile = new OpenFileDialog();//创建打开文件对话框
            openFile.Filter = "文本文件|*.txt";//定义打开文件的类型
            openFile.ShowDialog();//打开文件对话框
            string path = openFile.FileName;
            if (path == "")
            {
                return;
            }
            string[] str = File.ReadAllLines(path, Encoding.Default);
            for (int i = 0; i < str.Length; i++)
            {
                int j = 0;
                string[] s = str[i].Split(new char[] { ',' });   //以行为单位进行读取
                if (i == str.Length - 1)                                    //读取基本内容
                {
                    gongchengmingcheng.Text = s[j++];
                    gongchenglixing.Text = s[j++];
                    dengji.Text = s[j++];
                }
                else
                {
                    dataGridView1.Rows.Add(s[j++], s[j++], s[j++], s[j++], s[j++], s[j++], s[j++]);                //读取表格内容
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();//创建文件保存对象
            saveFile.Filter = "文本文件|*.txt";//定义保存文件格式
            saveFile.ShowDialog();
            string path = saveFile.FileName;
            if (path == "")
            {
                return;
            }
            int row = dataGridView1.Rows.Count;
            int j;
            string[] str = new string[row];

            //  MessageBox.Show(row.ToString());
            for (int i = 0; i < row - 1; i++)
            {
                //if (i == 0)
                //{
                //    str[i] = "序号    " + "测段    " + "往返   " + "斜距    " + "仪器高   " + "目标高    " + "垂直角    " + "平距    " + "气球差   " + "高差   " + "超限标志   " + "高差平均值   " + "改正数    " + "高程改正数  " + "\r\n";
                //}
                //else
                {

                    j = 0;
                    str[i] = str[i] + dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + "   ," +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   " +
                                    dataGridView1.Rows[i].Cells[j++].Value.ToString() + ",   ";
                    //   MessageBox.Show(i.ToString());


                }


            }
            File.WriteAllLines(path, str, Encoding.Default);//写入文件
            MessageBox.Show("保存成功");//提示保存成功
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            printDialog1.Document = printDocument1;
            printDialog1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();//创建打开文件对话框
            openFile.Filter = "文本文件|*.txt";//定义打开文件的类型
            openFile.ShowDialog();//打开文件对话框
            string path = openFile.FileName;
            if (path == "")
            {
                return;
            }
            string[] str = File.ReadAllLines(path, Encoding.Default);
          //  MessageBox.Show(str.Length.ToString());
            textBox1.Text = "";
            
 
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0)
                {
                    textBox1.Text = str[i] + "," + "\r\n";
                }
                else
                {
                    textBox1.Text += str[i] + "," + "\r\n";
                
                
                }
                    
                    
                    
                   

                
                
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            linesPerPage = e.MarginBounds.Height; printFont.GetHeight(e.Graphics);
            while (count < linesPerPage && lineNo < textBox1.Lines.Length)
            {
                line = textBox1.Lines[lineNo++];
                yPos = topMargin + (count * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }
            if (line != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lineNo = 0;
            printFont = new Font("宋体", 20);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;

        }

        private void button7_Click(object sender, EventArgs e)    //字体对话框
        {
            FontDialog FD = new FontDialog();
            FD.FontMustExist = true;
            FD.AllowVectorFonts = true;
            FD.ShowEffects = true;
            FD.ShowColor = true;
            FD.Color = this.textBox1.ForeColor ;
            FD.MaxSize = 100;
            FD.MinSize = 9;
            FD.Font = this.textBox1.Font;
            if (FD.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Font = FD.Font;
                this.textBox1.ForeColor = FD.Color;
            
            }
        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            平距.Enabled = true;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

     
       

      
    }
}

