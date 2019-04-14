using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices; //  为了调用[DllImport("gdi32.dll")]
using System.Reflection; // 引用这个才能使用Missing字段
//using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;  // 必须在项目“引用”的“com”添加EXCEL引用

namespace Tsf_seu
{
    public partial class Main_from : Form
    {
        Boolean project_yes = false;
        
        Boolean adj_yes = false;//是否平差计算完
        Boolean xy0_yes = false;//是否近似坐标计算完
        Boolean xy0_ed_yes = false;//是否给定已知点近似坐标
        Boolean data_ok = false;//是否起算数据已经确认
        Boolean rpt_yes = false;
        string prjfile, kzdfile;//项目文件名和控制点文件名
        int dtro, dtco;
        double xp0, yp0;//P点近似坐标
        double dxd, dyd;//P点坐标改正数
        double xp, yp;//P点平差后坐标
        double pvv = 0, mmx = 0, mmy = 0,mxy=0;//精度评定
        double qxx = 0, qyy = 0, qxy = 0;//精度评定
        double axf, byf, tuv = 0;//误差椭圆长轴axf,短轴byf,长轴方位tuv

       
        double[,] feqmat=new double [3,3];//法方程矩阵
        double[,] qeqmat = new double[2, 2];//法方程矩阵
        double pal, pbl, pcl;//法方程常数项
        double sum_a = 0, sum_b = 0, sum_l = 0;//虚拟误差方程项
        int kn = 0;//虚拟误差方程个数
        double ddz;//定向角改正数

        //绘图所用变量
        int pic_wdsize = 0;//绘图大小（宽和高一样的正方形）
        int picw =0, pich ;//这个是位图的大小，一会画图的范围就是那么大
        public double maxx, maxy, minx, miny, xx0, yy0;//最多最小坐标和中心坐标
        public double pax, pay, pdx, pdy;
        double pxmt = 0;//10mm所代表的像素值
        double kall = 0,skall=0;//显示比例及全图显示比例尺
        double epkall=0;//误差椭圆显示比例

        Point p0;
        bool canDrag;
        double gdx = 0, gdy = 0;//图形移动量

        int  row1 = 0, column1 = 0;

        //========

        public Main_from()
        {
            InitializeComponent();
        }

        private void Main_from_Load(object sender, EventArgs e)
        {

            this.Width = 1000; this.Height = 660;
            tabControl1.Width =850; tabControl1.Height = 500;
           // pictureBox1.Location = new Point(0, 10); 

            dataGridView1.Location = new Point(0, 35);
            dataGridView2.Location = new Point(0, 35);

            dataGridView1.Width = 430;
            dataGridView1.Height =400;
            dataGridView2.Width = 430;
            dataGridView2.Height = 400;
            dataGridView3.Width = 600;
            dataGridView3.Height = 350;
            richTextBox1.Width = 850;
            richTextBox1.Height = 450;
            pictureBox1.Width = 600;
            pictureBox1.Height  = 420;

            this.comboBox1.Visible = false ;

            //控制点坐标表-dataGridView1表头==================================
            DataGridViewTextBoxColumn acCode1 = new DataGridViewTextBoxColumn();

            acCode1.Name = "acCode"; acCode1.DataPropertyName = "acCode";
            acCode1.HeaderText = "点名";
            dataGridView1.Columns.Add(acCode1);

            DataGridViewTextBoxColumn acCode2 = new DataGridViewTextBoxColumn();
            acCode2.HeaderText = "X坐标";
            dataGridView1.Columns.Add(acCode2);

            DataGridViewTextBoxColumn acCode3 = new DataGridViewTextBoxColumn();
            acCode3.HeaderText = "Y坐标";
            dataGridView1.Columns.Add(acCode3);

            //取消排序功能
            acCode1.SortMode = DataGridViewColumnSortMode.NotSortable;
            acCode2.SortMode = DataGridViewColumnSortMode.NotSortable;
            acCode3.SortMode = DataGridViewColumnSortMode.NotSortable;


            dataGridView1.AutoGenerateColumns = false;//自动增加列取消
            dataGridView1.AllowUserToAddRows = false;//自动增加行取消
            dataGridView1.RowsDefaultCellStyle.Font = new Font("宋体", 15, FontStyle.Regular);  //表格中字体及大小格式
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            //表格单元格内容居中
            dataGridView1.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            //表格表头内容居中
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //方向距离观测成果-dataGridView2表头==================================
            DataGridViewTextBoxColumn acCode21 = new DataGridViewTextBoxColumn();
            acCode21.Name = "acCode"; //acCode21.DataPropertyName = "acCode";
            acCode21.HeaderText = "瞄准点名";
            dataGridView2.Columns.Add(acCode21);

            DataGridViewTextBoxColumn acCode22 = new DataGridViewTextBoxColumn();
            acCode22.HeaderText = "观测方向值";
            dataGridView2.Columns.Add(acCode22);

            DataGridViewTextBoxColumn acCode23 = new DataGridViewTextBoxColumn();
            acCode23.HeaderText = "观测距离";
            dataGridView2.Columns.Add(acCode23);

            //取消排序功能
            acCode21.SortMode = DataGridViewColumnSortMode.NotSortable;
            acCode22.SortMode = DataGridViewColumnSortMode.NotSortable;
            acCode23.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView2.AutoGenerateColumns = false;//自动增加列取消
            dataGridView2.AllowUserToAddRows = false;//自动增加行取消
            dataGridView2.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          
            //dataGridView3-平差成果表======================
            DataGridViewTextBoxColumn acCode31 = new DataGridViewTextBoxColumn();
            acCode31.Name = "acCode"; //acCode21.DataPropertyName = "acCode";
            acCode31.HeaderText = "瞄准点名";
            dataGridView3.Columns.Add(acCode31);

            DataGridViewTextBoxColumn acCode32 = new DataGridViewTextBoxColumn();
            acCode32.HeaderText = "观测方向值";
            dataGridView3.Columns.Add(acCode32);

            DataGridViewTextBoxColumn acCode33 = new DataGridViewTextBoxColumn();
            acCode33.HeaderText = "观测距离";
            dataGridView3.Columns.Add(acCode33);

            DataGridViewTextBoxColumn acCode34 = new DataGridViewTextBoxColumn();
            acCode34.HeaderText = "方向改正数 (秒)";
            dataGridView3.Columns.Add(acCode34);

            DataGridViewTextBoxColumn acCode35 = new DataGridViewTextBoxColumn();
            acCode35.HeaderText = "距离改正数(mm)";
            dataGridView3.Columns.Add(acCode35);



            //取消排序功能
            acCode31.SortMode = DataGridViewColumnSortMode.NotSortable;
            acCode32.SortMode = DataGridViewColumnSortMode.NotSortable;
            acCode33.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView3.AutoGenerateColumns = false;//自动增加列取消
            dataGridView3.AllowUserToAddRows = false;//自动增加行取消
            dataGridView3.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            label17.Text = "欢迎使用自由设站测站坐标计算应用程序！！";
            label17.Location =new Point (150,250);
            label17 .Font = new Font("宋体", 20, FontStyle.Regular);
            label17.BackColor =Color.Red;

        }// private void Main_from_Load(object sender, EventArgs e)

        private void 增加一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c_temp = contextMenuStrip1.SourceControl;

            if (c_temp.Name == "dataGridView1")
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Height = 30;
            }
            else if (c_temp.Name == "dataGridView2")
            {
                dataGridView2.Rows.Add(1);
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Height = 30;
            }
        }

        private void 删除一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control c_temp = contextMenuStrip1.SourceControl;
            if (c_temp.Name == "dataGridView1")
            {

                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(r);
                    }
                }
            }// if (c_temp.Name == "dataGridView1")
            else if (c_temp.Name == "dataGridView2")
            {
                foreach (DataGridViewRow r in dataGridView2.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        dataGridView2.Rows.Remove(r);
                    }
                }
            }// else if (c_temp.Name == "dataGridView2")
        }

        private void 插入一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Insert(1, new DataGridViewRow());//在当前行前插入一行
            Control c_temp = contextMenuStrip1.SourceControl;

            if (c_temp.Name == "dataGridView1")
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    int pos = this.dataGridView1.CurrentRow.Index;
                    dataGridView1.Rows.Insert(pos, new DataGridViewRow());
                    dataGridView1.Rows[pos].Height = 30;
                }

            }
            else if (c_temp.Name == "dataGridView2")
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    int pos = this.dataGridView2.CurrentRow.Index;
                    dataGridView2.Rows.Insert(pos, new DataGridViewRow());
                    dataGridView2.Rows[pos].Height = 30;
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr0 = MessageBox.Show("确实需要退出程序吗？", "检查对话框",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Warning
                                      );
            if (dr0 == DialogResult.Yes)
            {
                if (Cpub.n_dir > 0)
                {
                    DialogResult dr1 = MessageBox.Show("数据要保存吗？", "数据保存检查对话框",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning
                                          );
                    if (dr1 == DialogResult.Yes)
                    {
                        filesave();
                    }
                }
                System.Environment.Exit(0);
            }
             
                
        }

        private void 数据确认ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataok() == true)
            {
                MessageBox.Show("\n计算数据输入检查结束，可以进行其它各项工作！！！ ");
            }
        } // private void 数据确认ToolStripMenuItem_Click(object sender, EventArgs e
        private Boolean  dataok()//输入数据确认并读入保存
        {
            //读入读入控制点表坐标数据
            string tempstr1 = null, tempstr2 = null,str1=null ;
            double tempv1 = 0, tempv2 = 0;
            Cpub.n_kzd = 0; Cpub.n_dir = 0;
            Cpub.mListkzd.Clear();
            Cpub.mListvdir.Clear();

            try
            {
                for (int i = 1; i <= dataGridView1.Rows.Count; i++)
                {

                    if ((dataGridView1.Rows[i - 1].Cells[0].Value == "" || dataGridView1.Rows[i - 1].Cells[0].Value.ToString() == null)
                         || (dataGridView1.Rows[i - 1].Cells[1].Value == "" || dataGridView1.Rows[i - 1].Cells[1].Value.ToString() == null)
                         || (dataGridView1.Rows[i - 1].Cells[2].Value == "" || dataGridView1.Rows[i - 1].Cells[2].Value.ToString() == null)
                    )
                    {
                        MessageBox.Show("\n出错啦,有空数据或空行，请检查重新输入 ");
                        break;
                    }
                }
                for (int i = 1; i <= dataGridView1.Rows.Count; i++)
                {                  
                        kzdxy temkzd = new kzdxy();
                        temkzd.name = dataGridView1.Rows[i - 1].Cells[0].Value.ToString();
                        temkzd.x = Double.Parse(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                        temkzd.y = Double.Parse(dataGridView1.Rows[i - 1].Cells[2].Value.ToString());
                        Cpub.n_kzd = Cpub.n_kzd + 1;
                        Cpub.mListkzd.Add(temkzd);                  

                }// for (int i = 1; i <= dataGridView1.Rows.Count; i++)              


                //读入方向观测值表数据

                Cpub.n_ang = 0; Cpub.n_dis = 0;
                for (int i = 1; i <= dataGridView2.Rows.Count; i++)
                {
                    tempstr1 = null; tempstr2 = null;
                    tempv1 = 0; tempv2 = 0;
                    if (dataGridView2.Rows[i - 1].Cells[0].Value.ToString() == "" || dataGridView2.Rows[i - 1].Cells[0].Value == null)
                    {
                        MessageBox.Show("\n出错啦,瞄准点名不能为空，请检查重新输入 ");
                        break;
                    }
                    vdir temdir = new vdir();
                    temdir.name = dataGridView2.Rows[i - 1].Cells[0].Value.ToString();

                    if (dataGridView2.Rows[i - 1].Cells[1].Value == null || dataGridView2.Rows[i - 1].Cells[1].Value.ToString() == "")  //没有方向观测值
                        temdir.v_ang = 720;
                    else
                    {
                        tempstr1 = dataGridView2.Rows[i - 1].Cells[1].Value.ToString();
                        
                        tempv1 = Double.Parse(tempstr1);
                        temdir.v_ang = Cpub.ddeg(tempv1);
                        Cpub.n_ang = Cpub.n_ang + 1;
                    }
                    if (dataGridView2.Rows[i - 1].Cells[2].Value == null || dataGridView2.Rows[i - 1].Cells[2].Value == "")//没有距离观测值

                        temdir.v_dis = -50000.000;
                    else
                    {
                        tempstr2 = dataGridView2.Rows[i - 1].Cells[2].Value.ToString();
                        temdir.v_dis = Double.Parse(tempstr2);
                        Cpub.n_dis = Cpub.n_dis + 1;
                    }
                    Cpub.mListvdir.Add(temdir);
                    Cpub.n_dir = Cpub.n_dir + 1;

                }// for (int i = 1; i <= dataGridView2.Rows.Count; i++)

                Cpub.ma = Double.Parse(textBox1.Text.ToString());
                Cpub.mda = Double.Parse(textBox2.Text.ToString());
                Cpub.mdb = Double.Parse(textBox3.Text.ToString());

                //找出瞄准点在控制点存储线性表mlistkzd[]中的序号
                for (int i = 0; i <= Cpub.n_dir - 1; i++) 
                {
                    str1 = Cpub.mListvdir[i].name;
                    vdir temdir = new vdir();
                    temdir = Cpub.mListvdir[i];
                    for (int j = 0; j <= Cpub.n_kzd - 1; j++)
                    {
                        if (Cpub.mListkzd[j].name == str1)
                        {
                            temdir.no = j;
                            Cpub.mListvdir[i] = temdir;
                            break;
                        }
                    }
                }// //找出瞄准点在控制点存储线性表mlistkzd[]中的序号
                              

                data_ok = true;
                return true ;
            }//try异常处理语句
            catch
            {
                MessageBox.Show(" 表格中输入数据有问题，请重新输入！！！");
                return false;
            }

        }
      

        private void comfirmComboBoxValue(ComboBox com, String cellValue)//让表格中元素与组合框中元素相同
        {
            com.SelectedIndex = -1;
            if (cellValue == null)
            {
                com.Text = "";
                return;
            }
            com.Text = cellValue;
            foreach (Object item in com.Items)
            {
                if ((String)item == cellValue)
                {
                    com.SelectedItem = item;
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str1 = null;
            this.comboBox1.Visible = true;
            if (e.ColumnIndex == 0 )
            {

                comboBox1.Items.Clear();
                for (int i = 1; i <= dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i - 1].Cells[0].Value != null)
                    {
                        str1 = dataGridView1.Rows[i - 1].Cells[0].Value.ToString();
                        comboBox1.Items.Add(str1);
                    }
                }


                this.comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest; //输入提示
                this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;



                //此处cell即CurrentCell
                this.comboBox1.Visible = true;
                 dtro = e.RowIndex;
                 dtco = e.ColumnIndex;
                DataGridViewCell cell = this.dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Rectangle rect = this.dataGridView2.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                int cellX = dataGridView2.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, false).X;

                int cellY = dataGridView2.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, false).Y;
                cellX = cellX + dataGridView2.Location.X;
                cellY = cellY + dataGridView2.Location.Y;
                this.comboBox1.Location = new Point(cellX, cellY);
                // this.comboBox1.Location = rect.Location;
                //  this.comboBox1.Size = rect.Size;
                comboBox1.Width = rect.Width;
                comboBox1.Height = rect.Height;
                //   comboBox1.ItemHeight = rect.Height;
                comfirmComboBoxValue(this.comboBox1, (String)cell.Value);


            }
            else
                this.comboBox1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = this.dataGridView2.Rows[dtro].Cells[dtco];
            cell.Value = this.comboBox1.Text;
        }

        private void approximate_xy0()//近似坐标计算模块
        {
          double xa,ya,xb,yb,xc,yc;
          double dxab,dyab,sab,sap,sbp,fab,fbc,fap,fbp; 
          double ang_a ,ang_b,ang_c;
          double tempv = 0, tempu = 0;
            double spc1=0,spc2=0;
            //===================== 
          if (data_ok == false)
          {
              MessageBox.Show("\n出错啦,原始起算数据没有确认，请检查！！！ ");
              return;
          }
            if (checkBox1.Checked == true)
            {
                if (!double.TryParse(textBox6.Text, out xp0) ) //与
                {
                    MessageBox.Show("XP0输入框中可能为空或有非法字符，请正确输入数值！！！");
                    return;
                }
                if (!double.TryParse(textBox7.Text, out yp0))
                {
                    MessageBox.Show("YP0输入框中可能为空或有非法字符，请正确输入数值！！！");
                    return;
                }
                
                xy0_yes = true;
                return;
            }            
            

            //====================
            int kai=-100,kaj=-100;//标准后方交会，距离、角度均有测量
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                if (kai==-100 && Cpub.mListvdir[i].v_ang <360 && Cpub.mListvdir[i].v_dis >=0)
                    kai=i;
                else if((kaj==-100 && Cpub.mListvdir[i].v_ang <360 && Cpub.mListvdir[i].v_dis >=0))
                {
                    tempv = Math.Abs(Cpub.mListvdir[i].v_ang - Cpub.mListvdir[kai].v_ang);
                    if((tempv >=0.35 && tempv<=2.79)||(tempv<=5.93&&tempv>=3.49))//判断交会不能太小，>20度,<160度,或>200°，<340°这里是弧度
                    {
                         kaj=i;
                         break;
                    }

                }
            }    

            if (kai>=0 && kaj>=0){
                xa=Cpub .mListkzd [Cpub .mListvdir [kai].no].x;
                ya=Cpub .mListkzd [Cpub .mListvdir [kai].no].y;
                xb=Cpub .mListkzd [Cpub .mListvdir [kaj].no].x;
                yb=Cpub .mListkzd [Cpub .mListvdir [kaj].no].y;

                dxab =xb-xa;dyab =yb-ya;
                sab =Math.Sqrt (dxab*dxab+dyab*dyab);
                fab=Cpub .fwj (dxab,dyab);
                sap=Cpub .mListvdir [kai].v_dis ;sbp=Cpub .mListvdir [kaj].v_dis ;
                ang_a=(sab*sab+sap*sap-sbp*sbp)/(2*sap*sab);
                if (Math.Abs(Math.Abs(ang_a) - 1.0) < 0.00001)//三点在一条直线上
                    ang_a =0.0;
                else 
                    ang_a=Math .Acos (ang_a);
                ang_c =Cpub .mListvdir [kaj].v_ang -Cpub .mListvdir [kai].v_ang ;
                if (ang_c <Cpub .pii )
                    fap=fab+ang_a ;
                else
                    fap=fab-ang_a ;
                xp0=xa+sap*Math.Cos(fap);yp0=ya+sap*Math.Sin(fap);
                xy0_yes = true;
                return;

            }

            //========================================
            int kbi=-100,kbj=-100,kbk=-100;//角度后方交会，仅测量角度
            //利用利用原武测91年出版《测量学》P207余切公式计算
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                if (kbi==-100 && Cpub.mListvdir[i].v_ang <360)
                    kbi=i;
                else if(kbj==-100 && Cpub.mListvdir[i].v_ang <360)
                {
                    tempv = Math.Abs(Cpub.mListvdir[i].v_ang - Cpub.mListvdir[kbi].v_ang);
                    if ((tempv >= 0.35  && tempv <= 5.93))//判断交会不能太小，>20度,<340度
                    kbj=i;                    
                }
                else if (kbk==-100 && Cpub.mListvdir[i].v_ang <360)
                {
                    tempv = Math.Abs(Cpub.mListvdir[i].v_ang - Cpub.mListvdir[kbj].v_ang);
                    tempu = Math.Abs(Cpub.mListvdir[i].v_ang - Cpub.mListvdir[kbi].v_ang);
                    if ((tempv >= 0.35 && tempv <= 5.93) && (tempv <= 5.93 && tempv >= 0.35))//判断交会不能太小，>20度,<340度，这里是弧度
                    {
                        kbk = i;
                        break;
                    }
                }
            }
            double v1, v2, vq,vn;

            if (kbi >= 0 && kbj >= 0 && kbk >= 0)
            {
               //防止某个角接近180度，如接近重新确定A、B、C三点顺序，满足公式计算
                int ik = kbi, jk =kbj, kk =kbk;
                tempv = 0; tempu = 0;
                tempv = Math.Abs(Cpub.mListvdir[kbi].v_ang - Cpub.mListvdir[kbj].v_ang);
                tempu = Math.Abs(Cpub.mListvdir[kbk].v_ang - Cpub.mListvdir[kbj].v_ang);
                if (tempv >= Math.PI)
                {
                    kbi = jk; kbj = kk; kbk = ik;
                }
                else if (tempu >= Math.PI)
                {
                    kbi = kk; kbj = ik; kbk = jk;
                } 

               //
               
                
                xa = Cpub.mListkzd[Cpub.mListvdir[kbi].no].x;
                ya = Cpub.mListkzd[Cpub.mListvdir[kbi].no].y;
                xc = Cpub.mListkzd[Cpub.mListvdir[kbj].no].x;
                yc = Cpub.mListkzd[Cpub.mListvdir[kbj].no].y;
                xb = Cpub.mListkzd[Cpub.mListvdir[kbk].no].x;
                yb = Cpub.mListkzd[Cpub.mListvdir[kbk].no].y;
                ang_a =Cpub .mListvdir [kbj].v_ang-Cpub .mListvdir [kbi].v_ang;
                ang_b =Cpub .mListvdir [kbk].v_ang-Cpub .mListvdir [kbj].v_ang;
                v1 = (yc - yb) / Math.Tan(ang_b) - (ya - yc) / Math.Tan(ang_a) - (xa - xb);
                v2 = (xc - xb) / Math.Tan(ang_b) - (xa - xc) / Math.Tan(ang_a) + (ya - yb);
                vq = v1 / v2;
                vn = (yc - yb) *(1/ Math.Tan(ang_b)-vq) - (xc - xb)*(1+vq / Math.Tan(ang_b));
                xp0 = xc + vn / (1 + vq * vq); yp0 = yc + vq * vn / (1 + vq * vq);
                xy0_yes = true;
                return ;
            }//=====角度后方交会，仅测量角度

            
            //=============================================================
  
            int kci=-100,kcj=-100,kck=-100,kfi=-100,kfj=-100;//能否找出三个方向距离观测值和两个方向方向角值
            double xp01=0, yp01=0, xp02=0, yp02=0, xp03=0, yp03=0, xp04=0, yp04=0;
            double fpa = 0, fpb=0,tempvang=0;
            double temps1 = 0, temps2=0;
            string tsstring;
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                if (kci == -100 && Cpub.mListvdir[i].v_dis > 0)//找出三个方向观测值
                    kci=i;
                else if(kcj==-100 && Cpub.mListvdir[i].v_dis>0)
                {
                   //判断三点是否在一直线上
                    tempv = 0;
                    xa = Cpub.mListkzd[Cpub.mListvdir[kci].no].x;
                    ya = Cpub.mListkzd[Cpub.mListvdir[kci].no].y;
                    xb = Cpub.mListkzd[Cpub.mListvdir[i].no].x;
                    yb = Cpub.mListkzd[Cpub.mListvdir[i].no].y;
                    dxab = xb - xa; dyab = yb - ya;
                    sab = Math.Sqrt(dxab * dxab + dyab * dyab);
                    temps1 = Cpub.mListvdir[kci].v_dis;
                    temps2 = Cpub.mListvdir[i].v_dis;
                    tempv = Math.Abs ((temps1  *temps1  + temps2  * temps2  - sab*sab) / (2 * temps1  * temps2 ));
                    if (tempv <= 0.93)   //这里是cos20°=0.9396
                        kcj = i;
                    else
                        kck = i;
                }
                else if (kck==-100 && Cpub.mListvdir[i].v_dis>0)
                {                   
                    kck=i;
                }

                if (kfi == -100 && Cpub.mListvdir[i].v_ang < 360)//找两个方向角观测值
                {
                    kfi = i;
                }
                else if (kfj == -100 && Cpub.mListvdir[i].v_ang < 360)
                {
                    kfj = i;
                   
                }


            }////找出三个方向距离观测值

            if (kci >= 0 && kcj >= 0 && kck >= 0)
            //能够找到三个方向距离观测值,有一个距离多余观测值能判定距离交会计算点的位置
            //当A、B、C三个控制点在一条直线上时不能计算出正确结果
            {
                xa = Cpub.mListkzd[Cpub.mListvdir[kci].no].x;
                ya = Cpub.mListkzd[Cpub.mListvdir[kci].no].y;
                xb = Cpub.mListkzd[Cpub.mListvdir[kcj].no].x;
                yb = Cpub.mListkzd[Cpub.mListvdir[kcj].no].y;
                xc = Cpub.mListkzd[Cpub.mListvdir[kck].no].x;
                yc = Cpub.mListkzd[Cpub.mListvdir[kck].no].y;
                //由A和B点计算
                dxab = xb - xa; dyab = yb - ya;
                sab = Math.Sqrt(dxab * dxab + dyab * dyab);
                fab = Cpub.fwj(dxab, dyab);
                sap = Cpub.mListvdir[kci].v_dis; sbp = Cpub.mListvdir[kcj].v_dis;
                ang_a = (sab * sab + sap * sap - sbp * sbp) / (2 * sap * sab);
                if (Math.Abs(Math.Abs(ang_a) - 1.0) < 0.00001)//P、A、B三点在一条直线上
                    ang_a = 0.0;
                else
                    ang_a = Math.Acos(ang_a);                
                fap = fab + ang_a;
                xp01 = xa + sap * Math.Cos(fap); yp01 = ya + sap * Math.Sin(fap);
                fap = fab - ang_a;
                xp02 = xa + sap * Math.Cos(fap); yp02 = ya + sap * Math.Sin(fap);  


                //计算两个点到C点距离，根据距离判断哪个点符合要求
                spc1 =Math.Abs( Math.Sqrt((xp01 - xc) * (xp01 - xc) + (yp01 - yc) * (yp01 - yc))- Cpub.mListvdir[kck].v_dis);
                spc2 = Math.Abs(Math.Sqrt((xp02 - xc) * (xp02 - xc) + (yp02 - yc) * (yp02 - yc)) - Cpub.mListvdir[kck].v_dis);
                if(spc1<=1.0)
                {              
                    xp0 = xp01; yp0 = yp01;
                }
                else if (spc2 <= 1.0)
                {
                    xp0 = xp01; yp0 = yp01;
                }

                xy0_yes = true;
                return;
                //A、B、C三点不在一直线上结束

            }////能够找到三个方向距离结束

            else if (kci >= 0 && kcj >= 0 && kfi >= 0 && kfj > 0)
            //能够找到二个方向距离观测值,有一个角度多余观测值能判定距离交会计算点的位置
            {
              //先计算距离交会得到的两个点坐标值
                xa = Cpub.mListkzd[Cpub.mListvdir[kci].no].x;
                ya = Cpub.mListkzd[Cpub.mListvdir[kci].no].y;
                xb = Cpub.mListkzd[Cpub.mListvdir[kcj].no].x;
                yb = Cpub.mListkzd[Cpub.mListvdir[kcj].no].y;               
                //由A和B点计算
                dxab = xb - xa; dyab = yb - ya;
                sab = Math.Sqrt(dxab * dxab + dyab * dyab);
                fab = Cpub.fwj(dxab, dyab);
                sap = Cpub.mListvdir[kci].v_dis; sbp = Cpub.mListvdir[kcj].v_dis;
                ang_a = (sab * sab + sap * sap - sbp * sbp) / (2 * sap * sab);
                if (Math.Abs(Math.Abs(ang_a) - 1.0) < 0.00001)//三点在一条直线上
                    ang_a = 0.0;
                else
                    ang_a = Math.Acos(ang_a);
                fap = fab + ang_a;
                xp01 = xa + sap * Math.Cos(fap); yp01 = ya + sap * Math.Sin(fap);
                fap = fab - ang_a;
                xp02 = xa + sap * Math.Cos(fap); yp02 = ya + sap * Math.Sin(fap);
               // 根据角值判定取1号点还是2号点
                ang_c = Cpub.mListvdir[kfj].v_ang - Cpub.mListvdir[kfi].v_ang;

                xa = Cpub.mListkzd[Cpub.mListvdir[kfi].no].x;
                ya = Cpub.mListkzd[Cpub.mListvdir[kfi].no].y;                
               
                dxab = xa - xp01; dyab = ya - yp01;              
                fpa = Cpub.fwj(dxab, dyab);
                xb = Cpub.mListkzd[Cpub.mListvdir[kfj].no].x;
                yb = Cpub.mListkzd[Cpub.mListvdir[kfj].no].y;
                dxab = xb - xp01; dyab = yb - yp01;
                fpb = Cpub.fwj(dxab, dyab);
                tempvang = fpb - fpa;
                if (tempvang < 0)
                    tempvang = tempvang + 2 * Cpub.pii;

                double tta, ttb;
                tta = Cpub.ddms(tempvang); ttb = Cpub.ddms(ang_c);
                if (Math.Abs(tempvang - ang_c) < 0.003)//如反算出的角值与测量的角值小于3′，就认为此点为正确点
                {
                    xp0 = xp01; yp0 = yp01;
                }
                else
                {
                    xp0 = xp02; yp0 = yp02;
                }

                xy0_yes = true;
                return;

            }//二个方向距离观测值和有一个角度多余观测值计算点的位置结束
                
            else if (kai >= 0  && (kbj>=0 && kbi>=0))
            //一个方向既有距离又有方向角值，另一个方向有方向角值
            { 
                double ang_p;
                if (kai == kbi)
                {
                    xa = Cpub.mListkzd[Cpub.mListvdir[kai].no].x;
                    ya = Cpub.mListkzd[Cpub.mListvdir[kai].no].y;
                    xb = Cpub.mListkzd[Cpub.mListvdir[kbj].no].x;
                    yb = Cpub.mListkzd[Cpub.mListvdir[kbj].no].y;

                    dxab = xb - xa; dyab = yb - ya;
                    sab = Math.Sqrt(dxab * dxab + dyab * dyab);
                    fab = Cpub.fwj(dxab, dyab);
                    sap = Cpub.mListvdir[kai].v_dis;

                    ang_c = Cpub.mListvdir[kbj].v_ang - Cpub.mListvdir[kai].v_ang;
                    if (ang_c > Cpub.pii)
                        ang_p = ang_c - Math.PI;
                    else
                        ang_p = ang_c;
                    // 正弦定理
                    ang_a = Math.PI - Math.Asin(sap * Math.Sin(ang_p) / sab) - ang_p;
                    if (ang_c < Cpub.pii)
                        fap = fab + ang_a;
                    else
                        fap = fab - ang_a;
                    xp0 = xa + sap * Math.Cos(fap); yp0 = ya + sap * Math.Sin(fap);
                    xy0_yes = true;
                    return;
                }
                else
                {
                    xa = Cpub.mListkzd[Cpub.mListvdir[kai].no].x;
                    ya = Cpub.mListkzd[Cpub.mListvdir[kai].no].y;
                    xb = Cpub.mListkzd[Cpub.mListvdir[kbi].no].x;
                    yb = Cpub.mListkzd[Cpub.mListvdir[kbi].no].y;

                    dxab = xb - xa; dyab = yb - ya;
                    sab = Math.Sqrt(dxab * dxab + dyab * dyab);
                    fab = Cpub.fwj(dxab, dyab);
                    sap = Cpub.mListvdir[kai].v_dis;

                    ang_c = Cpub.mListvdir[kai].v_ang - Cpub.mListvdir[kbi].v_ang;
                    if (ang_c > Cpub.pii)
                        ang_p = ang_c - Math.PI;
                    else
                        ang_p = ang_c;
                    // 正弦定理
                    ang_a = Math.PI - Math.Asin(sap * Math.Sin(ang_p) / sab) - ang_p;
                    if (ang_c < Cpub.pii)
                        fap = fab - ang_a;
                    else
                        fap = fab + ang_a;
                    xp0 = xa + sap * Math.Cos(fap); yp0 = ya + sap * Math.Sin(fap);
                    xy0_yes = true;
                    return;
                }


            }



            else if (kci >= 0 && kcj >= 0)
            //仅能够找到两个方向距离观测值,无多余观测，距离交会计算点的位置需根据待定点与两控制点编号顺序判定（顺时针还是逆时针）
            {
                xa = Cpub.mListkzd[Cpub.mListvdir[kci].no].x;
                ya = Cpub.mListkzd[Cpub.mListvdir[kci].no].y;
                xb = Cpub.mListkzd[Cpub.mListvdir[kcj].no].x;
                yb = Cpub.mListkzd[Cpub.mListvdir[kcj].no].y;

                //由A和B点计算
                dxab = xb - xa; dyab = yb - ya;
                sab = Math.Sqrt(dxab * dxab + dyab * dyab);
                fab = Cpub.fwj(dxab, dyab);
                sap = Cpub.mListvdir[kci].v_dis; sbp = Cpub.mListvdir[kcj].v_dis;
                ang_a = (sab * sab + sap * sap - sbp * sbp) / (2 * sap * sab);
                if (Math.Abs(Math.Abs(ang_a) - 1.0) < 0.00001)//三点在一条直线上
                    ang_a = 0.0;
                else
                    ang_a = Math.Acos(ang_a);
                fap = fab + ang_a;
                xp01 = xa + sap * Math.Cos(fap); yp01 = ya + sap * Math.Sin(fap);
                fap = fab - ang_a;
                xp02 = xa + sap * Math.Cos(fap); yp02 = ya + sap * Math.Sin(fap);

                tsstring = "三角形" + Cpub.mListvdir[kci].name + "-" + Cpub.mListvdir[kcj].name + "-" + "P" + "是顺时针编号嘛？";

                DialogResult dr1 = MessageBox.Show(tsstring, "测边交会点位判断对话框",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning
                               );
                if (dr1 == DialogResult.Yes)//顺时针编号
                {
                    xp0 = xp01; yp = yp01;
                    xy0_yes = true;
                    return;
                }
                else//逆时针编号
                {
                    xp0 = xp02; yp = yp02;
                    xy0_yes = true;
                    return;
                }
                ////只有两个距离计算结束


            }

            

        }
      

        private void error_equation1()//误差方程和法方程组成（采用史赖佰消元消去定向角，二阶矩阵求逆简单）
        {
            //计算误差方程a,b系数
            Boolean fdir = false;//标识第一个方向
            double dfwj = 0;// 定向角近似值
            double cpl0=0;//方向角值常数项
            double dx0, dy0, ds0, fwj0,ang_v0;
            double mms;//测距误差及权
            feqmat[1, 1] = 0.0; feqmat[1, 2] = 0.0;feqmat [2,1]=0.0 ; feqmat[2, 2] = 0.0;
            pbl = 0.0; pcl = 0.0;
           
           //计算误差方程系数a,b及常数项l
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                vdir temdir = new vdir();
                temdir = Cpub.mListvdir[i];
                dx0 = Cpub.mListkzd[temdir.no].x - xp0; dy0 = Cpub.mListkzd[temdir.no].y - yp0;
                ds0 = Math.Sqrt(dx0 * dx0 + dy0 * dy0);
                fwj0 = Cpub.fwj(dx0, dy0);
                temdir.dis0 = ds0;
                temdir.fwj = fwj0;

               
                //方向观测值
                if (temdir.v_ang >= 0 && temdir.v_ang < 360)
                {
                    temdir.ang_a = Cpub.po * dy0 / (ds0 * ds0);
                    temdir.ang_b = -1 * Cpub.po * dx0 / (ds0 * ds0);
                    if (fdir == false)
                    {
                        fdir = true;
                        dfwj = fwj0;
                    }
                    ang_v0 = fwj0 - dfwj;
                     

                    if (ang_v0 < 0  )
                        ang_v0 = ang_v0 + 2 * Math.PI ; 
 
                     cpl0=ang_v0- temdir.v_ang;
                     if (Math.Abs(cpl0) > Math.PI)
                         cpl0 =Math.Sign ( cpl0) *(Math.Abs(cpl0)- 2 * Math.PI);// 防止与定向角夹角很小的角值
     
                    temdir.ang_l =cpl0 *206265.0;
                    double tx = 0;
                    tx = temdir.ang_l;
                }
             
                
                // 距离观测值
                if (temdir.v_dis > 0)
                {
                    temdir.dis_a = -1 * dx0 / (ds0);
                    temdir.dis_b = -1 * dy0 / (ds0);
                    temdir.dis_l = (ds0 - temdir.v_dis) * 1000.0;
                    mms = Cpub.mda + Cpub.mdb * temdir.v_dis / 1000.0;
                    temdir.wp = Cpub.ma * Cpub.ma / (mms * mms);
                }
                Cpub.mListvdir[i] = temdir;

            }

            sum_a = 0; sum_b = 0; sum_l = 0;//虚拟误差方程项
             kn = 0;//虚拟误差方程个数
            // 组成法方程
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                if (Cpub.mListvdir[i].v_ang < 360 && Cpub .n_ang >1)
                {                   
                    feqmat[1, 1] = feqmat[1, 1] + Cpub.mListvdir[i].ang_a * Cpub.mListvdir[i].ang_a;
                    feqmat[1, 2] = feqmat[1, 2] + Cpub.mListvdir[i].ang_a * Cpub.mListvdir[i].ang_b;
                    pbl = pbl + Cpub.mListvdir[i].ang_a * Cpub.mListvdir[i].ang_l;
                    feqmat[2, 2] = feqmat[2, 2] + Cpub.mListvdir[i].ang_b * Cpub.mListvdir[i].ang_b;
                    pcl = pcl + Cpub.mListvdir[i].ang_b * Cpub.mListvdir[i].ang_l;
                    sum_a = sum_a + Cpub.mListvdir[i].ang_a; 
                    sum_b = sum_b + Cpub.mListvdir[i].ang_b;
                    sum_l = sum_l + Cpub.mListvdir[i].ang_l;
                    kn = kn + 1;
                }
               
                if (Cpub.mListvdir[i].v_dis > 0)
                {
                    feqmat[1, 1] = feqmat[1, 1] + Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_a * Cpub.mListvdir[i].dis_a;
                    feqmat[1, 2] = feqmat[1, 2] + Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_a * Cpub.mListvdir[i].dis_b;
                    pbl = pbl + Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_a * Cpub.mListvdir[i].dis_l;
                    feqmat[2, 2] = feqmat[2, 2] + Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_b * Cpub.mListvdir[i].dis_b;
                    pcl = pcl + Cpub.mListvdir[i].dis_b * Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_l;

                }

            }//for (int i = 0; i <= Cpub.n_dir - 1; i++)
            if (kn > 1)
            {
                feqmat[1, 1] = feqmat[1, 1] - sum_a * sum_a / kn;
                feqmat[1, 2] = feqmat[1, 2] - sum_a * sum_b / kn;
                pbl = pbl - sum_a * sum_l / kn;

                feqmat[2, 2] = feqmat[2, 2] - sum_b * sum_b / kn;
                pcl = pcl - sum_b * sum_l / kn;
            }
        }


        private void error_equation0()//误差方程和法方程组成（不采用史赖佰法则，但要求3阶矩阵逆阵输入数据确认并读入保存本软件没有使用)

        {
          //计算误差方程a,b系数
            Boolean  fdir=false;//标识第一个方向
            double dfwj=0;// 定向角近似值
            double dx0, dy0, ds0, fwj0, ang_v0; ;
            double mms;//测距误差及权
            feqmat[1, 1] = 0.0; feqmat[1, 2] = 0.0; feqmat[2, 1] = 0.0; feqmat[2, 2] = 0.0;
            feqmat[0, 0] = 0.0; feqmat[0, 1] = 0.0; feqmat[1, 0] = 0.0; feqmat[0, 2] = 0.0; feqmat[2, 0] = 0.0;
            pal = 0; pbl = 0.0; pcl = 0.0;

            //计算误差方程系数a,b及常数项l
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                vdir temdir = new vdir();
                temdir = Cpub.mListvdir[i];
                dx0 = Cpub.mListkzd[temdir.no].x - xp0; dy0 = Cpub.mListkzd[temdir.no].y - yp0;
                ds0 = Math.Sqrt(dx0 * dx0 + dy0 * dy0);
                fwj0 = Cpub.fwj(dx0, dy0);
                temdir.dis0 = ds0;
                temdir.fwj = fwj0;


                //方向观测值
                if (temdir.v_ang >= 0 && temdir.v_ang < 360)
                {
                    temdir.ang_a = Cpub.po * dy0 / (ds0 * ds0);
                    temdir.ang_b = -1 * Cpub.po * dx0 / (ds0 * ds0);
                    if (fdir == false)
                    {
                        fdir = true;
                        dfwj = fwj0;
                    }
                    ang_v0 = fwj0 - dfwj;
                    if (ang_v0 < 0)
                        ang_v0 = ang_v0 + 2 * Cpub.pii;
                    temdir.ang_l = (ang_v0 - temdir.v_ang) * 206265.0;

                }
                // 距离观测值
                if (temdir.v_dis > 0)
                {
                    temdir.dis_a = -1 * dx0 / (ds0);
                    temdir.dis_b = -1 * dy0 / (ds0);
                    temdir.dis_l = (ds0 - temdir.v_dis) * 1000.0;
                    mms = Cpub.mda + Cpub.mdb * temdir.v_dis / 1000.0;
                    temdir.wp = Cpub.ma * Cpub.ma / (mms * mms);
                }
                Cpub.mListvdir[i] = temdir;

            }


          // 组成法方程
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                if (Cpub.mListvdir[i].v_ang < 360)
                {
                    feqmat [0,0]=feqmat [0,0]+1;
                    feqmat[0, 1] = feqmat[0, 1] - Cpub.mListvdir[i].ang_a;
                    feqmat[0, 2] = feqmat[0, 2] - Cpub.mListvdir[i].ang_b;
                    pal = pal - Cpub.mListvdir[i].ang_l;
                    feqmat[1, 1] = feqmat[1, 1] + Cpub.mListvdir[i].ang_a *Cpub.mListvdir[i].ang_a;
                    feqmat[1, 2] = feqmat[1, 2] + Cpub.mListvdir[i].ang_a* Cpub.mListvdir[i].ang_b;
                    pbl = pbl + Cpub.mListvdir[i].ang_a * Cpub.mListvdir[i].ang_l;
                    feqmat[2, 2] = feqmat[2, 2] + Cpub.mListvdir[i].ang_b * Cpub.mListvdir[i].ang_b;
                    pcl = pcl + Cpub.mListvdir[i].ang_b * Cpub.mListvdir[i].ang_l;

                }
                if (Cpub.mListvdir[i].v_dis > 0)
                {


                    feqmat[1, 1] = feqmat[1, 1] +Cpub .mListvdir [i].wp *Cpub.mListvdir[i].dis_a * Cpub.mListvdir[i].dis_a;
                    feqmat[1, 2] = feqmat[1, 2] + Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_a * Cpub.mListvdir[i].dis_b;
                    pbl = pbl + Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_a * Cpub.mListvdir[i].dis_l;
                    feqmat[2, 2] = feqmat[2, 2] + Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_b * Cpub.mListvdir[i].dis_b;
                    pcl = pcl + Cpub.mListvdir[i].dis_b * Cpub.mListvdir[i].wp * Cpub.mListvdir[i].dis_l;

                }

            }

        }
        private void button1_Click(object sender, EventArgs e)//从外部导入控制点坐标文件，如GPS观测平差后坐标文件
        {
            String tempString = null;
            String[] temstr = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();  //也可以在界面设计时的工具箱中点击一个          
            openFileDialog1.Filter = "txt文件|*.txt|dat文件|*.dat|所有文件|*.*";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {

                try
                {
                    kzdfile  = openFileDialog1.FileName;//获取文件路径和文件名                   

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n出错啦: " + ex.Message);
                }
                finally
                {

                }
            }  // if (DialogResult .OK==openFileDialog1 .ShowDialog ())
            //读入文件数据============================


            FileStream fs = new FileStream(kzdfile , FileMode.Open);
            StreamReader reader = new StreamReader(fs);
            // StreamReader reader = new StreamReader(fs, Encoding.Default);
            //c#对汉字字符读取方式有几种，在Encoding中选择，如Encoding.Default，Encoding.UTF8等

            //读水准点信息
            temstr = null;
            if (reader !=null)
            {

                do
                {
                    tempString = reader.ReadLine();
                    if (tempString != null && tempString != "")// 如遇空行或文件结束就停止读入
                    {
                        temstr = tempString.Split(',');
                        dataGridView1.Rows.Add(1);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Height = 30;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = temstr[0];
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = temstr[1];
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = temstr[2];
                    }
                    else
                        break;

                } while (true);
                reader.Close();
          }
       }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)//打开项目文件
        {

            fileopen();
            tabControl1.Visible = true;
            label17.Visible = false;
        }

        private void 保存文件ToolStripMenuItem_Click(object sender, EventArgs e)//保存项目文件
        {
            filesave();
        }
        private void fileopen()//打开项目文件模块
        {
            String tempString = null;
            String[] temstr = null;
            OpenFileDialog openFileDialog2 = new OpenFileDialog();  //也可以在界面设计时的工具箱中点击一个          
            openFileDialog2.Filter = "txt文件|*.txt|dat文件|*.dat|所有文件|*.*";
            if (DialogResult.OK == openFileDialog2.ShowDialog())
            {

                try
                {
                    prjfile = openFileDialog2.FileName;//获取文件路径和文件名                   
                    tabControl1.Visible = true;
                    label17.Visible = false;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("\n出错啦: " + ex.Message);
                    return;
                }
                finally
                {

                }
            }  // if (DialogResult .OK==openFileDialog1 .ShowDialog ())
            else
            {
                return;
            }
            //读入文件数据============================

            dataGridView1.RowCount = 0;
            dataGridView2.RowCount = 0;
            dataGridView3.RowCount = 0;
            Cpub.mListkzd.Clear();
            Cpub.mListvdir.Clear();


            FileStream fs = new FileStream(prjfile, FileMode.Open);
            StreamReader reader = new StreamReader(fs);

            temstr = null;
            tempString = reader.ReadLine();
            if (tempString == "begin_kzdxy")
            {
                do
                {
                    tempString = reader.ReadLine();
                    temstr = tempString.Split(',');
                    if (temstr[0] == "end_kzdxy")
                        break;
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Height = 30;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = temstr[0];
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = temstr[1];
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = temstr[2];
                } while (true);
            }

            //读测段观测值信息
            temstr = null;
            tempString = reader.ReadLine();
            if (tempString == "begin_gcdata")
            {
                do
                {
                    tempString = reader.ReadLine();
                    temstr = tempString.Split(',');
                    if (temstr[0] == "end_gcdata")
                        break;
                    dataGridView2.Rows.Add(1);
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Height = 30;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = temstr[0];
                    if (temstr[1] == "720")
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = "";
                    else
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = temstr[1];
                    if (temstr[2] == "-50000.000")
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = "";
                    else
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = temstr[2];

                } while (true);
            }
            tempString = reader.ReadLine();
            if (tempString == "begin_accuracy")
            {

                tempString = reader.ReadLine();
                temstr = tempString.Split(',');
                textBox1.Text = temstr[0];
                textBox2.Text = temstr[1];
                textBox3.Text = temstr[2];
            }


            reader.Close();
            project_yes =true;

            //=====================
        }

        private void filesave()//保存项目文件模块
        {

            string dline = null;
            int kj1 = 0, kj2 = 0;
            string strr = null;
                       
            if (prjfile  == "" || prjfile  == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();//用语句实现的一个保存对话框，也可以在界面设计时的工具箱中点击一个
                sfd.Title = "";
                sfd.Filter = "文本文件(*.txt)|*.txt|data文件(*.dat)|*.dat";//一般在保存文件时使用，选择一种格式文件保存               
                sfd.ShowDialog();
                prjfile  = sfd.FileName;
            }
            StreamWriter fwr = null;//需要using System.IO;
            fwr = new StreamWriter(prjfile );
            fwr.WriteLine("begin_kzdxy");
            kj1 = dataGridView1.Rows.Count;           
            strr = dataGridView1.Rows[0].Cells[0].Value.ToString();
            kj2 = kj1;
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "" || dataGridView1.Rows[i].Cells[0].Value.ToString() == null)
                    break;
                dline = null;
                dline = dataGridView1.Rows[i].Cells[0].Value + "," + dataGridView1.Rows[i].Cells[1].Value + ",";                
                dline = dline + dataGridView1.Rows[i].Cells[2].Value;   
                fwr.WriteLine(dline);
            }
            fwr.WriteLine("end_kzdxy");

            fwr.WriteLine("begin_gcdata");
            for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value.ToString() == "" || dataGridView2.Rows[i].Cells[0].Value.ToString() == null)
                    break;
                dline = null;
                dline = dataGridView2.Rows[i].Cells[0].Value + "," + dataGridView2.Rows[i].Cells[1].Value + ",";
                dline = dline + dataGridView2.Rows[i].Cells[2].Value ;

                fwr.WriteLine(dline);
            }
            fwr.WriteLine("end_gcdata");
            fwr.WriteLine("begin_accuracy");
            dline = null;
            dline = textBox1.Text  + "," + textBox2 .Text  + ","+textBox3.Text ;
            fwr.WriteLine(dline);
            fwr.WriteLine("end_accuracy");
            fwr.Close();
        }

        private void 平差计算ToolStripMenuItem_Click(object sender, EventArgs e)//平差计算
        {
            if (data_ok == true && xy0_yes == true)
            {
                adjust();
                return;
            }
            {
                MessageBox.Show("\n出错啦,原始起算数据没有确认或近似坐标没有计算，请检查！！！ ");               
            }


        }
        private void adjust()//平差计算模块
        {
            double tempv;
            
            double vb=0, vs=0;
            int kj = 0;
            dataGridView3.RowCount = 0;



                error_equation1();
                //逆矩阵qeqmat
                tempv = feqmat[1, 1] * feqmat[2, 2] - feqmat[1, 2] * feqmat[1, 2];
                qeqmat[0, 0] = 1 * feqmat[2, 2] / tempv;
                qeqmat[1, 1] = 1 * feqmat[1, 1] / tempv;
                qeqmat[0, 1] = -1 * feqmat[1, 2] / tempv;
                qeqmat[1, 0] = qeqmat[0, 1];
               //求改正数
                dxd = -1 * (pbl * qeqmat[0, 0] + pcl * qeqmat[0, 1]);
                dyd = -1 * (pbl * qeqmat[1, 0] + pcl * qeqmat[1, 1]);
                xp = xp0 + dxd / 1000.0; yp = yp0 + dyd / 1000.0;

                label8.Text ="X="+ Math.Round((xp), 4).ToString();
                label9.Text ="Y="+ Math.Round((yp), 4).ToString();


               // xp0 = xp; yp0 = yp;
                //计算改正数和中误差
                ddz = dxd * sum_a / kn + dyd * sum_b / kn + sum_l / kn;

                for (int i = 0; i <= Cpub.n_dir - 1; i++)
                {
                    vdir temdir = new vdir();
                    temdir = Cpub.mListvdir[i];
                    kj = kj + 1;
                    dataGridView3.Rows.Add(1);
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Height = 30;
                    dataGridView3.Rows[kj-1].Cells[0].Value = temdir.name;
                    //方向观测值
                    if (temdir.v_ang >= 0 && temdir.v_ang < 360)
                    {
                        vb = -ddz + temdir.ang_a * dxd + temdir.ang_b  * dyd + temdir.ang_l;
                        pvv = pvv + vb * vb;                      
                        dataGridView3.Rows[kj - 1].Cells[1].Value =Cpub .rad_dms_str( temdir.v_ang) ;
                        dataGridView3.Rows[kj - 1].Cells[3].Value = Math.Round((vb), 1).ToString();
                        temdir.v_a = vb;
                    }
                    // 距离观测值
                    if (temdir.v_dis > 0)
                    {
                        vs = temdir.dis_a * dxd + temdir.dis_b * dyd + temdir.dis_l;
                        pvv = pvv + temdir.wp * vs * vs;

                        dataGridView3.Rows[kj - 1].Cells[2].Value = temdir.v_dis;
                        dataGridView3.Rows[kj - 1].Cells[4].Value = Math.Round((vs), 1).ToString();
                        temdir.v_d = vs;
                    }
                   Cpub.mListvdir[i]= temdir;

                }// for (int i = 0; i <= Cpub.n_dir - 1; i++)
                if (Cpub.n_dir > 1)
                    pvv = pvv / (Cpub.n_dir + Cpub.n_dis - 3);
                else if (Cpub.n_dir == 0 && Cpub.n_dis > 2)
                    pvv = pvv / (Cpub.n_dis - 2);
                else
                    pvv = 0;

                if (pvv > 0.001)
                {
                    mmx = Math.Sqrt(pvv * qeqmat[0, 0]); mmy = Math.Sqrt(pvv * qeqmat[1, 1]);
                    mxy = Math.Sqrt(pvv * qeqmat[0, 1]);
                    qxx =pvv * qeqmat[0, 0]; qyy = pvv * qeqmat[1, 1];
                    qxy =pvv * qeqmat[0, 1];

                    label11.Text = "mx=" + Math.Round((mmx), 1).ToString();
                    label12.Text = "my=" + Math.Round((mmy), 1).ToString();
                }
                else
                    label11.Text = "没有多余观测！";

                //计算误差椭圆长轴axf,短轴byf,长轴方位tuv
                double abq = 0;
                abq = (qxx - qyy) * (qxx - qyy) + 4 * qxy * qxy;
                abq = Math.Sqrt(abq);
                axf = qxx + qyy + abq;
                byf = qxx + qyy - abq;
                axf = axf / 2; axf = Math.Sqrt(axf);
                byf = byf / 2; byf = Math.Sqrt(byf);
                tuv = 2 * qxy / (qxx - qyy);
                tuv = Math.Atan(tuv);
                if (tuv < 0)
                    tuv = tuv + 2 * Math.PI;
                tuv = tuv / 2;
                if (mxy > 0 && tuv < Math.PI / 2)
                    tuv = tuv;
                else if (mxy > 0 && tuv > Math.PI / 2)
                    tuv = tuv - Math.PI / 2;
                else if (mxy < 0 && tuv < Math.PI / 2)
                    tuv = tuv + Math.PI / 2;
                else if (mxy < 0 && tuv > Math.PI / 2)
                    tuv = tuv;
              //计算误差椭圆参数结束
            
               tabControl1.SelectedTab = tabPage3;//显示某页
               adj_yes = true;
               rpt_yes = true;
        }


        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)//项目文件另存为另一文件
        {
            SaveFileDialog sfd = new SaveFileDialog();//用语句实现的一个保存对话框，也可以在界面设计时的工具箱中点击一个
            sfd.Title = "";
            sfd.Filter = "文本文件(*.txt)|*.txt|data文件(*.dat)|*.dat";//一般在保存文件时使用，选择一种格式文件保存               
            sfd.ShowDialog();
            prjfile  = sfd.FileName;
            filesave();
        }

        private void 近似坐标计算ToolStripMenuItem_Click(object sender, EventArgs e)//近似坐标计算
        {           
            string strxy0 = null;
            xy0_yes = false;
            approximate_xy0();
            if (xy0_yes == true)
            {
                strxy0 = "\n 近似坐标计算完成，请检查是否正确！！！ ";
                strxy0 += "\n" + "测站点近似坐标:   XP0=" + xp0.ToString("f3") + "；   YP0=" + yp0.ToString("f3");
                MessageBox.Show(strxy0);
            }
            else
                MessageBox.Show("近似坐标无法求出，可能数据有问题，请检查输入数值！！！");

        }
         private void graph_0()//绘制图形初始化
        {
            float wx = 0, wy = 0;
            double wwx = 0, wwy = 0;//图形的宽度和高度,与pic_wdsize的区别是图片中能够绘图的最大范围


           
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
           
           // pictureBox1.Size = new Size(490,290);//宽和高
            pictureBox1.Size = new Size(600, 420);//宽和高
            picw = pictureBox1.Width; pich = pictureBox1.Height;
            if (picw <pich)
                pic_wdsize =picw;
            else
                pic_wdsize =pich;
            pic_wdsize = pic_wdsize -10;
            //ab方向坐标，cd方向坐标
            maxx =xp; minx = xp;
            maxy =yp; miny = yp;
            xx0 = xp; yy0 = yp;
      
            int knj=0;
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
               
                knj = Cpub.mListvdir[i].no;             
                if (maxx < Cpub.mListkzd[knj].x)
                    maxx = Cpub.mListkzd[knj].x;
                if (minx > Cpub.mListkzd[knj].x)
                    minx = Cpub.mListkzd[knj].x;
                if (maxy < Cpub.mListkzd[knj].y)
                    maxy = Cpub.mListkzd[knj].y;
                if (miny > Cpub.mListkzd[knj].y)
                    miny = Cpub.mListkzd[knj].y;
                xx0 = xx0 + Cpub.mListkzd[knj].x;
                yy0 = yy0 + Cpub.mListkzd[knj].y;
            }
            wwx = maxx - minx;//测量X与计算机相反
            wwy = maxy - miny;
            xx0 = xx0 / (Cpub.n_dir  + 1); yy0 = yy0 / (Cpub.n_dir  + 1);

            pxmt = MillimetersToPixelsWidth(10.0);
            kall = wwx / 10.0 * pxmt;
            if (kall < wwy / 10.0 * pxmt)//以最大方向的算比例尺
                kall = wwy / 10.0 * pxmt;


            kall = pic_wdsize  / kall;
            kall = pic_wdsize / wwx;
            if (kall < pic_wdsize / wwy)//以最大方向的算比例尺
                kall = pic_wdsize / wwy;    

            skall = kall;
            textBox4.Text = (Math.Round (1000/kall,0)).ToString(); 
         }
         private void drawgraph(double skk, double ddx, double ddy)//绘制图形，本软件没有试用
        {
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0, x0 = 0, y0 = 0, xx1 = 0, yy1 = 0, xx2 = 0, yy2 = 0;
            Graphics gra = pictureBox1.CreateGraphics();

            Brush bush = new SolidBrush(Color.Green);//填充的颜色
            Font myFont = new Font("宋体", 5, FontStyle.Bold);

            for (int i = 0; i <= Cpub .n_kzd -1; i++)
            {
                xx1 = Cpub.mListkzd[i].x; yy1 = Cpub.mListkzd[i].y;
                x1 = (yy1 - miny) * skk + ddx; y1 = 300 - (xx1 - minx) * skk + ddy;

                
                x2 = (yp - miny) * skk + ddx; y2 = 300 - (xp - minx) * skk + ddy;

                //画点
                gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                bush = new SolidBrush(Color.Green);//填充的颜色
                gra.FillEllipse(bush, (int)x1, (int)y1, 3, 3);//画填充椭圆的方法，x坐标、y坐标、宽、高，半径3个像素约1mm
                //写点名
                bush = new SolidBrush(Color.Red);//填充的颜色
                gra.DrawString(Cpub.mListkzd[i].name , myFont, bush, (int)x1, (int)y1);
                //画导线边
                gra.DrawLine(new Pen(Color.Blue), (int)x1, (int)y1, (int)x2, (int)y2);

            }
            //测站点
            
            x1 = (yp - miny) * skk + ddx; y1 = 300 - (xp - minx) * skk + ddy;
            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bush = new SolidBrush(Color.Green);//填充的颜色
            gra.FillEllipse(bush, (int)x1, (int)y1, 3, 3);
            bush = new SolidBrush(Color.Red);//填充的颜色
            gra.DrawString("测站P", myFont, bush, (int)x1, (int)y1);


        }

        public double MillimetersToPixelsWidth(double length) //像素与实际长度换算，length是毫米，1厘米=10毫米
        {
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(p.Handle);
            IntPtr hdc = g.GetHdc();
            int width = GetDeviceCaps(hdc, 4);     // HORZRES 宽度（毫米）
            int pixels = GetDeviceCaps(hdc, 8);     // BITSPIXEL宽度像素
           // textBox4.Text = width.ToString() + " " + pixels.ToString();
            g.ReleaseHdc(hdc);
            return (((double)pixels / (double)width) * (double)length);
        }
       
        [DllImport("gdi32.dll")]   //C#调用winAPI的方法（以gdi32.dll为例）//C#中的DllImport使用方法
        private static extern int GetDeviceCaps(IntPtr hdc, int Index);

        private void button2_Click(object sender, EventArgs e)//全图
        {
           
            pictureBox1.Refresh();
            gdx = 0; gdy = 0;
            pictureBox1.Image = getBitMapFile(skall, gdx, gdy);//此方法绘制的图形窗体最小化不会消失，也可以采用重新
            textBox4.Text = (Math.Round(1000 / skall, 0)).ToString(); 
            // drawgraph(kall, 50, 50);//此方法绘制的图形窗体最小化后消失
        }

        private void button3_Click(object sender, EventArgs e)//放大
        {
            kall = 1.2 * kall;
            pictureBox1.Refresh();
            pictureBox1.Image = getBitMapFile(kall, gdx, gdy);
            textBox4.Text = (Math.Round(1000 / kall, 0)).ToString(); 
        }

        private void button4_Click(object sender, EventArgs e)//缩小
        {
            kall = 0.8 * kall;
            pictureBox1.Refresh();
            pictureBox1.Image = getBitMapFile(kall, gdx, gdy);
            textBox4.Text = (Math.Round(1000 / kall, 0)).ToString(); 
        }

        //解决图片最小化后出现图形消失问题，下面采用的是位图方法解决，也可以采用OnPaint事件解决
        public Bitmap getBitMapFile(double skk, double ddx, double ddy) //绘制图形
        {        

            Bitmap img = new Bitmap(picw, pich);
            Graphics gra = Graphics.FromImage(img);
            double fawj = 0;
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0, x0 = 0, y0 = 0, xx1 = 0, yy1 = 0, xx2 = 0, yy2 = 0;
            Brush bush = new SolidBrush(Color.Green);//填充的颜色
            Font myFont = new Font("宋体", 10, FontStyle.Bold);

            int knj=0;

            epkall= Double.Parse(textBox5.Text.ToString());

            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {               
                knj = Cpub.mListvdir[i].no ;               
                xx1 = Cpub.mListkzd[knj].x; yy1 = Cpub.mListkzd[knj].y;
                x1 = (yy1 - miny) * skk + ddx; y1 = pic_wdsize - (xx1 - minx) * skk + ddy;                
                x2 = (yp - miny) * skk + ddx; y2 = pic_wdsize - (xp - minx) * skk + ddy;

                //画点
                gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                bush = new SolidBrush(Color.Green);//填充的颜色
                gra.FillEllipse(bush, (int)x1, (int)y1, 3, 3);//画填充椭圆的方法，x坐标、y坐标、宽、高，半径3个像素约1mm
                //写点名
                bush = new SolidBrush(Color.Red);//填充的颜色

                gra.DrawString(Cpub.mListkzd[knj].name, myFont, bush, (int)x1, (int)y1);
                //画测站至控制 点线
                gra.DrawLine(new Pen(Color.Blue), (int)x1, (int)y1, (int)x2, (int)y2);

            }
            //测站P点            
            x1 = (yp - miny) * skk + ddx; y1 = pic_wdsize - (xp - minx) * skk + ddy;
            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bush = new SolidBrush(Color.Green);//填充的颜色
            gra.FillEllipse(bush, (int)x1, (int)y1, 3, 3);
            bush = new SolidBrush(Color.Red);//填充的颜色
            gra.DrawString("测站_P", myFont, bush, (int)x1, (int)y1);
            textBox4.Text = (Math.Round(1000 / kall, 0)).ToString(); 
            
            
            //绘制误差椭圆开始
           
            double tepx,tepy,c_tepx=0,c_tepy=0;


            for (int k=0;k<=361;k++)
            {
                fawj = k / 180.0 *Math.PI ;
                tepx =epkall * axf *Math .Cos (fawj); tepy =epkall * byf *Math .Sin (fawj);//计算不倾斜的椭圆坐标
                //epkall为绘制误差椭圆比例尺，与图形不一致
                //计算倾斜方位角tuv椭圆坐标，坐标系旋转
                c_tepx=tepx *Math.Cos (tuv)-tepy*Math .Sin (tuv);c_tepy =tepx*Math.Sin(tuv)+tepy*Math.Cos (tuv);
          
                 x1 = (yp - miny) * skk + ddx-c_tepy*pxmt/10 ; y1 = pic_wdsize - (xp - minx) * skk + ddy+c_tepx*pxmt/10  ; 
            
                 if (k>0)
                   gra.DrawLine(new Pen(Color.Blue), (int)x1, (int)y1, (int)x2, (int)y2);
                  x2=x1;y2=y1;
            }

            //绘制误差椭圆结束
            Pen arrowPen = new Pen(Color.Blue);//绘制坐标轴及箭头
            arrowPen.Width = 4;
            arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;            
            gra.DrawLine(arrowPen,4, 410,4, 320);
            gra.DrawLine(arrowPen, 4, 410, 102, 410);
            gra.DrawString("X", myFont, bush, 4, 315);
            gra.DrawString("Y", myFont, bush, 106, 405);


            return img;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e) //选择底部toolStrip1按钮操作
        {
            if (e.TabPage == tabPage4)
            {
                graph_0();
                pictureBox1.Refresh();
                gdx = 0; gdy = 0;
                pictureBox1.Image = getBitMapFile(kall, gdx, gdy);//此方法绘制的图形窗体最小化不会消失，也可以采用重新
                textBox4.Text = (Math.Round(1000 / kall, 0)).ToString(); 
                // drawgraph(kall, 50, 50);//此方法绘制的图形窗体最小化后消失
            }
            else if (e.TabPage == tabPage5)
            {
                richTextBox1.Text = WriteReport();
            }
            else if (e.TabPage == tabPage6)
            {
                richTextBox2.Text ="欢迎使用本软件！！"+"\r\n";
                richTextBox2.Text =richTextBox2.Text +"本软件能进行单点自由设站坐标计算，具有以下功能："+"\r\n";
                richTextBox2.Text = richTextBox2.Text + "一、全站仪测角测距坐标计算，" + "\r\n";
                richTextBox2.Text = richTextBox2.Text + "二、全站仪仅测角测站坐标计算，" + "\r\n";
                richTextBox2.Text = richTextBox2.Text + "三、全站仪仅测距测站坐标计算，" + "\r\n";
                richTextBox2.Text = richTextBox2.Text + "四、全站仪部分测角、测距测站坐标计算，" + "\r\n";
            }



        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)//选择底部toolStrip1按钮操作选择
        {
           // if (e.TabPageIndex == 1) e.Cancel = true;

            if (e.TabPage == tabPage4 && adj_yes == false)
            {
                MessageBox.Show("坐标没有计算，不能绘图！！");
                e.Cancel = true;
            }
            if (e.TabPage == tabPage5 && rpt_yes == false)
            {
                MessageBox.Show("没有平差计算，不能显示报告！！");
                e.Cancel = true;
            }

        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//鼠标移动操作，在左下角显示坐标
        {
            int px = 0, py = 0;
            double ppx=0,ppy=0;
            px = e.X; py = e.Y;

            //x2 = (yp - miny) * skk + ddx; y2 = pic_wdsize - (xp - minx) * skk + ddy;
            ppx=(pic_wdsize-py+gdy)/kall+minx ; ppy = (px - gdx) / kall + miny;
           
            label15.Text ="x="+Math.Round((ppx), 3).ToString ();
            label16.Text = "y=" + Math.Round((ppy), 3).ToString();
        }

       

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)//鼠标右键按下拖动图形操作
        {
            if (e.Button == MouseButtons.Left)
            {
                // 按下鼠标中键，记录鼠标按下的位置
                p0 = e.Location;
                // 设置标志：准备拖动图片
                canDrag = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)//鼠标右键抬起拖动图形操作
        {
            if (e.Button == MouseButtons.Left)
            {
                // 松开鼠标中键，清除拖动标志，禁止鼠标拖动图标操作

                canDrag = false;
                //左键起来就移动图像
                gdx = gdx + e.X - p0.X; gdy = gdy + e.Y - p0.Y;
                pictureBox1.Refresh();
                pictureBox1.Image = getBitMapFile(kall, gdx, gdy);
                textBox4.Text = (Math.Round(1000 / kall, 0)).ToString(); 
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//打开项目文件
        {
            fileopen();
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//保存项目文件
        {
            filesave();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//顶部工具条按钮操作，平差按钮
        {
            if (data_ok == true && xy0_yes == true)
            {
                adjust();
                return;
            }
            {
                MessageBox.Show("\n出错啦,原始起算数据没有确认或近似坐标没有计算，请检查！！！ ");
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)//顶部工具条按钮操作，数据确认按钮
        {
           
            if (dataok() == true)
            {
                MessageBox.Show("\n计算数据输入检查结束，可以进行其它各项工作！！！ ");
            };

        }

        private void toolStripButton6_Click(object sender, EventArgs e)//顶部工具条按钮操作，近似坐标按钮
        {
            string strxy0 = null;
            xy0_yes = false;
            approximate_xy0();
            if (xy0_yes == true)
            {
                strxy0 = "\n 近似坐标计算完成，请检查是否正确！！！ ";
                strxy0 +="\n"+"测站点近似坐标:   XP0="+xp0.ToString("f3")+"；   YP0="+yp0.ToString("f3");
                MessageBox.Show(strxy0 );
            }
            else
                MessageBox.Show("近似坐标无法求出，可能数据有问题，请检查输入数值！！！");

        }

        private void toolStripButton5_Click(object sender, EventArgs e)//顶部工具条按钮操作，绘图按钮
        {
            if (adj_yes == false)
            {
                MessageBox.Show("坐标没有计算，不能绘图！！");
                return;
            }

                tabControl1.SelectedTab = tabPage4;//显示某页
                graph_0();
                pictureBox1.Refresh();
                gdx = 0; gdy = 0;
                pictureBox1.Image = getBitMapFile(kall, gdx, gdy);//此方法绘制的图形窗体最小化不会消失，也可以采用重新
                textBox4.Text = (Math.Round(1000 / kall, 0)).ToString(); 
            // drawgraph(kall, 50, 50);//此方法绘制的图形窗体最小化后消失
            
        }

        private void 图形显示ToolStripMenuItem_Click(object sender, EventArgs e)//图形显示菜单操作
        {
            if (adj_yes == false)
            {
                MessageBox.Show("坐标没有计算，不能绘图！！");
                return;
            }

            tabControl1.SelectedTab = tabPage4;//显示某页
            graph_0();
            pictureBox1.Refresh();
            gdx = 0; gdy = 0;
            pictureBox1.Image = getBitMapFile(kall, gdx, gdy);//此方法绘制的图形窗体最小化不会消失，也可以采用重新
            textBox4.Text = (Math.Round(1000 / kall, 0)).ToString(); 
            // drawgraph(kall, 50, 50);//此方法绘制的图形窗体最小化后消失
        }
        private string WriteReport()//形成报告模块
        {
            string report = null;
            string temstr = null;
            report = report + "======================计算成果报告=====================" + "\r\n";
            report = report + "\r\n";
            report = report + "      观测者：_______    计算者:__________     日期：________" + "\r\n";
            report = report + "=====================控制点坐标数据===================="+" \r\n";
            report += string.Format("{0,15} {1,10:f4}{2,15:f4}\r\n", "点名", "X", "Y");
           

            int knj=0;
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                
                knj = Cpub.mListvdir[i].no;
                report += string.Format("{0,15} {1,15:f4}{2,15:f4}\r\n", Cpub.mListkzd[knj].name, Cpub.mListkzd[knj].x, Cpub.mListkzd[knj].y);
               
            }// for (int i = 0; i <= Cpub.n_dir - 1; i++)
            report = report + "======================================================" + " \r\n";
            report = report + "\r\n";

            report = report + "=====================观测值平差数据===================" + " \r\n";
            report = report + string.Format("{0,5} {1,8:f5}{2,12:f1}{3,8:f5}{4,12:f4}{5,13:f1}{6,9:f4}\r\n", 
                "瞄准点名", "方向值", "vβ(秒)", "平差值", "距离值", "vs(mm)", "平差值");
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {

                if (Cpub.mListvdir[i].v_ang < 360 && Cpub.mListvdir[i].v_dis > 0)
                {
                   // report += string.Format("{0,15} {1,15:f5}{2,10:f1}{3,15:f5}{4,15:f4}{5,10:f1}{6,15:f4}\r\n",
                  //      Cpub.mListvdir[i].name, Cpub.ddms(Cpub.mListvdir[i].v_ang), Cpub.mListvdir[i].v_a,
                  //      Cpub.ddms(Cpub.mListvdir[i].v_ang + Cpub.mListvdir[i].v_a / 206265),
                  //      Cpub.mListvdir[i].v_dis, Cpub.mListvdir[i].v_d,
                    //      Cpub.mListvdir[i].v_dis + Cpub.mListvdir[i].v_d / 1000);//角度以十进制形式显示
                    report += string.Format("{0,5} {1,15:f5}{2,10:f1}{3,15:f5}{4,15:f4}{5,10:f1}{6,15:f4}\r\n",
                       Cpub.mListvdir[i].name, Cpub.rad_dms_str (Cpub.mListvdir[i].v_ang), Cpub.mListvdir[i].v_a,
                       Cpub.rad_dms_str (Cpub.mListvdir[i].v_ang + Cpub.mListvdir[i].v_a / 206265),
                       Cpub.mListvdir[i].v_dis, Cpub.mListvdir[i].v_d,
                       Cpub.mListvdir[i].v_dis + Cpub.mListvdir[i].v_d / 1000);//角度以°′″形式显示

                }
                else if (Cpub.mListvdir[i].v_ang > 360)
                {
                    report += string.Format("{0,5} {1,15:f5}{2,10:f1}{3,15:f5}{4,15:f4}{5,10:f1}{6,15:f4}\r\n",
                                           Cpub.mListvdir[i].name, " ", " ",
                                           " ",
                                           Cpub.mListvdir[i].v_dis, Cpub.mListvdir[i].v_d,
                                           Cpub.mListvdir[i].v_dis + Cpub.mListvdir[i].v_d / 1000);
                }
                else if (Cpub.mListvdir[i].v_dis <0 )
                {
                   report += string.Format("{0,5} {1,15:f5}{2,10:f1}{3,15:f5}{4,15:f4}{5,10:f1}{6,15:f4}\r\n",
                        Cpub.mListvdir[i].name, Cpub.rad_dms_str (Cpub.mListvdir[i].v_ang), Cpub.mListvdir[i].v_a,
                        Cpub.rad_dms_str (Cpub.mListvdir[i].v_ang + Cpub.mListvdir[i].v_a / 206265),
                        " ", " ",
                        " ");
                }


            }
            report = report + "======================================================" + " \r\n";
            report = report + "\r\n";
            report = report + "=====================测站点坐标===================" + "\r\n";
            report += string.Format("{0,15} {1,10}{2,2:f4}{3,10}{4,2:f4}\r\n", "坐标值:","X=", xp,"Y=", yp);
            report += string.Format("{0,10} {1,10}{2,2:f1}{3,10}{4,2:f1}{5,10}{6,2:f1}\r\n", "点位精度(mm):", "mx=", mmx, "my=", mmy,
                "mp=",Math.Sqrt(mmx*mmx+mmy*mmy));
            report = report + "======================================================" + " \r\n";
            report = report + "" + " \r\n";
            report = report + "" + " \r\n";
            report = report + "======================评分使用报告=======================" + " \r\n";
            report += string.Format("{0,15} {1,10}{2,2:f4}{3,10}{4,2:f4}\r\n", "近似坐标值:", "XP0=", xp0, "YP0=", yp0);
           
            
            report = report + "======================误差方程=======================" + " \r\n";
            report = report + "======================方向值误差方程=======================" + " \r\n";

            report = report + string.Format("{0,5} {1,8:f5}{2,15:f1}{3,10:f5}{4,10:f4}{5,15:f1}{6,15:f4}{7,12:f5}\r\n",
              "瞄准点名", "方向观测值", "近似方位角", "系数a", "系数b", "常数项l(秒)", "v(秒)", "平差值");

            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
            
                if (Cpub.mListvdir[i].v_ang < 360)
                {
                    report += string.Format("{0,5} {1,15:f5}{2,15:f5}{3,10:f3}{4,15:f3}{5,15:f3}{6,15:f1}{7,18:f5}\r\n",
                      Cpub.mListvdir[i].name, Cpub.rad_dms_str(Cpub.mListvdir[i].v_ang), Cpub.rad_dms_str(Cpub.mListvdir[i].fwj) ,
                      Cpub.mListvdir[i].ang_a , Cpub.mListvdir[i].ang_b ,Cpub.mListvdir[i].ang_l ,
                       Cpub.mListvdir[i].v_a,
                      Cpub.rad_dms_str(Cpub.mListvdir[i].v_ang + Cpub.mListvdir[i].v_a / 206265));
                }
            }

            report = report + "======================距离误差方程=======================" + " \r\n";

            report = report + string.Format("{0,5} {1,8:f5}{2,12:f1}{3,12:f5}{4,12:f4}{5,13:f1}{6,12:f4}{7,13:f5}\r\n",
              "瞄准点名", "距离观测值", "近似距离", "系数a", "系数b", "常数项l(mm)", "vs(mm)", "平差值");

            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {

                if (Cpub.mListvdir[i].v_dis >0)
                {
                    report += string.Format("{0,5} {1,15:f4}{2,15:f4}{3,15:f3}{4,15:f3}{5,13:f3}{6,15:f1}{7,15:f4}\r\n",
                      Cpub.mListvdir[i].name, Cpub.mListvdir[i].v_dis, Cpub.mListvdir[i].dis0,
                      Cpub.mListvdir[i].dis_a, Cpub.mListvdir[i].dis_b, Cpub.mListvdir[i].dis_l,
                       Cpub.mListvdir[i].v_d ,
                       Cpub.mListvdir[i].v_dis + Cpub.mListvdir[i].v_d / 1000);
                }
            }
            report = report + "======================法方程=======================" + " \r\n";
            report = report + "             ===1、采用史赖佰法则===          " + " \r\n";
            report = report + string.Format("{0,5} {1,12:f5}{2,15:f5}\r\n", "[paa]", "[pbb]", "常数项C");
            report += string.Format("{0,5:f5} {1,15:f5}{2,15:f5}\r\n", feqmat[1,1], feqmat[1, 2], pbl);
            report += string.Format("{0,5:f5} {1,15:f5}{2,15:f5}\r\n", feqmat[1, 2], feqmat[2, 2], pcl);
            report = report + "            ===2、没有采用史赖佰法则===   " + " \r\n";
            error_equation0();

            report = report + string.Format("{0,5}{1,15}{2,15:f5}{3,15:f5}\r\n", "[paa]", "[pbb]", "[pcc]", "常数项C");
            report += string.Format("{0,5:f5}{1,15:f5} {2,15:f5}{3,15:f5}\r\n", feqmat[0, 0], feqmat[0, 1], feqmat[0, 2], pal);
            report += string.Format("{0,5:f5}{1,15:f5} {2,15:f5}{3,15:f5}\r\n", feqmat[0,1], feqmat[1, 1], feqmat[1, 2], pbl);
            report += string.Format("{0,5:f5}{1,15:f5} {2,15:f5}{3,15:f5}\r\n", feqmat[0,2], feqmat[1, 2], feqmat[2, 2], pcl);


            report = report + "======================逆矩阵(未知数协因数阵)=======================" + " \r\n";
            report = report + string.Format("{0,5} {1,8:f5}\r\n", "Qaa", "Qbb");
            report = report + string.Format("{0,5:f5} {1,8:f5}\r\n", qeqmat[0, 0], qeqmat[0, 1] );
            report += string.Format("{0,5:f5} {1,8:f5}\r\n", qeqmat[0, 1], qeqmat[1, 1]);

            report = report + "======================未知数解=======================" + " \r\n";
            report += string.Format("{0,5}{1,2:f3}{2,1}{3,10}{4,2:f3}{5,1}{6,10}{7,2:f3}{8,1}\r\n", "δzp=", ddz, "″", "δxp=", dxd, "mm ", "δyp=", dyd, "mm");
         
            report = report + "=====================测站点坐标===================" + "\r\n";
            report += string.Format("{0,15} {1,10}{2,2:f4}{3,10}{4,2:f4}\r\n", "坐标值:","X=", xp,"Y=", yp);
            report += string.Format("{0,10} {1,10}{2,2:f1}{3,10}{4,2:f1}{5,10}{6,2:f1}\r\n", "点位精度(mm):", "mx=", mmx, "my=", mmy,
                "mp=",Math.Sqrt(mmx*mmx+mmy*mmy));
            report = report + "======================误差椭圆参数=======================" + " \r\n";
            report += string.Format("{0,5}{1,2:f3}{2,1}{3,10}{4,2:f3}{5,1}{6,10}{7,2:f3}\r\n", "长轴 E=", axf, "mm", "短轴 F=", byf , "mm ", "长轴方向", Cpub.rad_dms_str (tuv ));



            return report;
        }
        private void 帮助说明ToolStripMenuItem_Click(object sender, EventArgs e)//帮助说明
        {
            MessageBox.Show("《测绘程序设计试题集（竞赛篇 试题07 自由设站测站坐标计算）》配套程序\n作者：闻道秋\n东南大学交通学院\r\nEMAIL: 740336073@qq.com\r\n2019.01.08");
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//顶部工具条按钮操作，一键计算按钮
        {
            Boolean ddkk = false;
            xy0_yes = false;
            ddkk=dataok();
            if (ddkk == true)
            {
                approximate_xy0();

                if (xy0_yes == false)
                {
                    MessageBox.Show("近似坐标无法求出，可能数据有问题，请检查输入数值！！！");
                    return;
                }

                adjust();
            }
            else
            {              
                MessageBox.Show("\n请仔细检查控制点数据和外业观测数据输入的正确性！！！ ");
             }
        }

        private void tXT文本文件报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text  = WriteReport();
            tabControl1.SelectedTab = tabPage5;//显示某页
            string fndxf = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "计算报告文本保存";            
            sfd.Filter = "  文本文件 txt文件(*.txt)|*.txt|所有文件(*.*)|*.*";//一般在保存文件时使用，选择一种格式文件保存
            sfd.ShowDialog();
            fndxf = sfd.FileName;
            StreamWriter fwr = null;//需要using System.IO;
            fwr = new StreamWriter(fndxf);
            fwr.WriteLine(WriteReport ());
            fwr.Close();
        }

        private void 图形DXFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fndxf = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "图形保存DXF文件";
            // sfd.InitialDirectory = @"C:\";
            // sfd.Filter = "文本文件| *.txt";
            //  sfd.Filter = "导线计算数据文件|*.dat;*.txt|所有文件|*.*";//一般在打开文件时使用，把多种格式的文件显示出来
            sfd.Filter = " cad dxf文件(*.dxf)|*.dxf|所有文件(*.*)|*.*";//一般在保存文件时使用，选择一种格式文件保存
            sfd.ShowDialog();
            fndxf = sfd.FileName;


            StreamWriter fwr = null;
            fwr = new StreamWriter(fndxf);
            //================================DXF文件开始
            //标题段可省略
            //下面是表段（TABLE），一般含4个表，按顺序分别为线型表-LTYE，图层表-LAYER，字样表-STYLE，视图表-VIEW
            // 每个表又包含可变数量的表项
            fwr.WriteLine("0");//"0"表示一个事物开始，如一个块、表、图层、实体
            fwr.WriteLine("SECTION");
            fwr.WriteLine("2");//"2"表示一个事物的名字，如段、表、块、线型、字体、视图
            fwr.WriteLine("TABLES");
            //===================建立一个新图层，图层名为mylay
            fwr.WriteLine("0");
            fwr.WriteLine("TABLE");
            fwr.WriteLine("2");
            fwr.WriteLine("LAYER");

            fwr.WriteLine("0");
            fwr.WriteLine("LAYER");
            fwr.WriteLine("2");
            fwr.WriteLine("MYLAY");

            fwr.WriteLine("70");//
            fwr.WriteLine("0");

            fwr.WriteLine("62");//颜色
            fwr.WriteLine("3");

            fwr.WriteLine("6");//线型
            fwr.WriteLine("CONTINUOUS");

            fwr.WriteLine("0");
            fwr.WriteLine("ENDTAB");//表项结束

            fwr.WriteLine("0");
            fwr.WriteLine("ENDSEC");


            //=======================实体段开始
            //可建立各种实体
            //===============================建立实体entities
            fwr.WriteLine("0");
            fwr.WriteLine("SECTION");
            fwr.WriteLine("2");
            fwr.WriteLine("ENTITIES");
            int knj=0;
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {
                
                knj = Cpub.mListvdir[i].no;

                //直线段实体
                fwr.WriteLine("0");
                fwr.WriteLine("LINE");
                fwr.WriteLine("8");//图层名
                fwr.WriteLine("MYLAY");

                fwr.WriteLine("10");
                fwr.WriteLine(Cpub.mListkzd[knj].y.ToString());
                fwr.WriteLine("20");
                fwr.WriteLine(Cpub.mListkzd[knj].x.ToString());
                fwr.WriteLine("30");
                fwr.WriteLine("0");
                fwr.WriteLine("11");
                fwr.WriteLine(yp.ToString());
                fwr.WriteLine("21");
                fwr.WriteLine(xp.ToString());
                fwr.WriteLine("31");
                fwr.WriteLine("0");
                fwr.WriteLine("62");
                fwr.WriteLine("1");
                //圆实体===========================CIRCLE
                fwr.WriteLine("0");
                fwr.WriteLine("CIRCLE");
                fwr.WriteLine("8");
                fwr.WriteLine("0");

                fwr.WriteLine("10");
                fwr.WriteLine(Cpub.mListkzd[knj].y.ToString());
                fwr.WriteLine("20");
                fwr.WriteLine(Cpub.mListkzd[knj].x.ToString());
                fwr.WriteLine("30");
                fwr.WriteLine("0");
                fwr.WriteLine("40");//半径
                fwr.WriteLine("10");


                //文字实体==============================TEXT
                fwr.WriteLine("0");
                fwr.WriteLine("TEXT");

                fwr.WriteLine("8");//图层
                fwr.WriteLine("0");

                fwr.WriteLine("10");
                fwr.WriteLine(Cpub.mListkzd[knj].y.ToString());
                fwr.WriteLine("20");
                fwr.WriteLine(Cpub.mListkzd[knj].x.ToString());
                fwr.WriteLine("30");
                fwr.WriteLine("0");

                fwr.WriteLine("40");//注记文本高30
                fwr.WriteLine("30");

                fwr.WriteLine("1");//注记内容
                fwr.WriteLine(Cpub.mListkzd[knj].name);

                fwr.WriteLine("50");//字体旋转角度
                fwr.WriteLine("0");// fwr.WriteLine("0")为字体旋转180度

            }

            //==================================================实体结束标志
            fwr.WriteLine("0");
            fwr.WriteLine("ENDSEC");//实体结束

            //====================================================end eof文件结束标志
            fwr.WriteLine("0");
            fwr.WriteLine("EOF");

            fwr.Close();
            



        }

        private void eXCEL报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //成果转换成EXCEL文件
            int knn = 0;
            int icol = 0;
            string fnxls = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "导线成果保存在EXCEL格式";
            // sfd.InitialDirectory = @"C:\";
            // sfd.Filter = "文本文件| *.txt";
            //  sfd.Filter = "导线计算数据文件|*.dat;*.txt|所有文件|*.*";//一般在打开文件时使用，把多种格式的文件显示出来
            sfd.Filter = " excel文件(*.xls)|*.xls|xlsx文件(*.xlsx)|*.xlsx";//一般在保存文件时使用，选择一种格式文件保存
            sfd.ShowDialog();
            fnxls = sfd.FileName;


            Excel.Application myExcel = new Excel.Application();
            //  在创建excel workbook之前，检查系统是否安装excel

            if (myExcel == null)
            {
                // if equal null means EXCEL is not installed.  
                MessageBox.Show("Excel is not properly installed!");
                return;
            }


            Excel.Workbook myworkBook;
            //判断文件是否存在，如果存在就打开workbook，如果不存在就新建一个
            if (File.Exists(fnxls))
            {
                myworkBook = myExcel.Workbooks.Open(fnxls, 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            }
            else
            {
                myworkBook = myExcel.Workbooks.Add(true);
            }


            Excel.Worksheet myworkSheet = myworkBook.ActiveSheet as Excel.Worksheet;
            //write data  
            myworkSheet = (Excel.Worksheet)myworkBook.Worksheets.get_Item(1);//获得第i个sheet，准备写入  


            //单元格合并


            //制作表头

            Excel.Range myrange = myExcel.Range[myExcel.Cells[1, 1], myExcel.Cells[2, 1]];

            myrange = myworkSheet.get_Range("A1", "G2"); //获取Excel多个单元格区域：本例做为Excel表头 

            //.get_Range 方法用于用字母表示的区域,.Range 用于用cell 单元格表示的范围
            myrange.Merge(0); //单元格合并动作 
            myworkSheet.Cells[1, 1] = "自由设站测量计算成果表格";

            Excel.Range kzdexcelRange = myworkSheet.Range[myworkSheet.Cells[3, 1], myworkSheet.Cells[3, 7]];
            kzdexcelRange.Merge(kzdexcelRange.MergeCells);
            myworkSheet.Cells[3, 1] = "控制点坐标表";
           
            myworkSheet.Cells[4, 1] = "控制点名";
           
            myworkSheet.Cells[4, 2] = "X坐标";
            
            myworkSheet.Cells[4,3] = "Y坐标";

            int knj = 0;
            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {

                knj = Cpub.mListvdir[i].no;
                myworkSheet.Cells[5 + i, 1] = Cpub.mListkzd[knj].name;
                myworkSheet.Cells[5 + i, 2] = Cpub.mListkzd[knj].x;
                myworkSheet.Cells[5 + i, 3] = Cpub.mListkzd[knj].y;
              
            }// for (int i = 0; i <= Cpub.n_dir - 1; i++)
            knn = 5 + Cpub.n_dir;

            Excel.Range direxcelRange = myworkSheet.Range[myworkSheet.Cells[knn, 1], myworkSheet.Cells[knn, 7]];
            direxcelRange.Merge(direxcelRange.MergeCells);
            myworkSheet.Cells[knn, 1] = "观测结果平差表";


            
            myworkSheet.Cells[knn+1, 1] = "瞄准点名";
            myworkSheet.Cells[knn+1, 2] = "方向观测值";
            myworkSheet.Cells[knn+1, 3] = "方向改正数";
            myworkSheet.Cells[knn+1, 4] = "方向平差值";
            myworkSheet.Cells[knn+1, 5] = "距离观测值";
            myworkSheet.Cells[knn+1, 6] = "距离改正数";
            myworkSheet.Cells[knn+1, 7] = "距离平差值";
            knn = knn +2;
            double tempv0 = 0;

            for (int i = 0; i <= Cpub.n_dir - 1; i++)
            {

                myworkSheet.Cells[knn+ i, 1] =Cpub.mListvdir[i].name;
                if (Cpub.mListvdir[i].v_ang < 360)
                {
                    //myworkSheet.Cells[knn + i, 2] = Math.Round(Cpub.ddms(Cpub.mListvdir[i].v_ang), 5);
                    myworkSheet.Cells[knn + i, 2] =Cpub.rad_dms_str (Cpub.mListvdir[i].v_ang); //以°′″形式显示角度
                    myworkSheet.Cells[knn + i, 3] = Math.Round(Cpub.mListvdir[i].v_a, 1);
                   // myworkSheet.Cells[knn + i, 4] = Math.Round(Cpub.ddms(Cpub.mListvdir[i].v_ang + Cpub.mListvdir[i].v_a / 206265), 5);
                    myworkSheet.Cells[knn + i, 4] =Cpub.rad_dms_str (Cpub.mListvdir[i].v_ang + Cpub.mListvdir[i].v_a / 206265);//以°′″形式显示角度
                }
                if (Cpub.mListvdir[i].v_dis > 0)
                {
                    myworkSheet.Cells[knn + i, 5] = Math.Round(Cpub.mListvdir[i].v_dis, 4);
                    myworkSheet.Cells[knn + i, 6] = Math.Round(Cpub.mListvdir[i].v_d, 1);
                    myworkSheet.Cells[knn + i, 7] = Math.Round(Cpub.mListvdir[i].v_dis + Cpub.mListvdir[i].v_d / 1000, 4);
                }
            }


          
            knn = knn + Cpub.n_dir+1;

            Excel.Range myexcelRange = myworkSheet.Range[myworkSheet.Cells[knn, 1], myworkSheet.Cells[knn+1, 7]];
            myexcelRange.Merge(myexcelRange.MergeCells);
            myworkSheet.Cells[knn, 1] = "测站点坐标结果";

            myexcelRange = myworkSheet.Range[myworkSheet.Cells[knn+2, 1], myworkSheet.Cells[knn+3, 2]];
            myexcelRange.Merge(myexcelRange.MergeCells);
            myworkSheet.Cells[knn+2, 1] = "XP=" + Math .Round (xp,4);
            myexcelRange = myworkSheet.Range[myworkSheet.Cells[knn + 2, 3], myworkSheet.Cells[knn+3,4]];
            myexcelRange.Merge(myexcelRange.MergeCells);
            myworkSheet.Cells[knn + 2, 3] = "YP=" +Math .Round ( yp,4);
            myexcelRange = myworkSheet.Range[myworkSheet.Cells[knn+ 2 ,5], myworkSheet.Cells[knn +3, 5]];
            myexcelRange.Merge(myexcelRange.MergeCells);
            myworkSheet.Cells[knn + 2, 5] = "mx=" + Math .Round (mmx,1)+"mm";
            myexcelRange = myworkSheet.Range[myworkSheet.Cells[knn + 2, 6], myworkSheet.Cells[knn + 3, 6]];
            myexcelRange.Merge(myexcelRange.MergeCells);
            myworkSheet.Cells[knn + 2, 6] = "my=" + Math .Round (mmy,1)+"mm";
            myexcelRange = myworkSheet.Range[myworkSheet.Cells[knn + 2, 7], myworkSheet.Cells[knn + 3, 7]];
            myexcelRange.Merge(myexcelRange.MergeCells);
            myworkSheet.Cells[knn +2, 7] = "my=" + Math.Round(Math.Sqrt (mmx*mmx+mmy*mmy), 1) + "mm"; 

            // 数据填充完=======================

            //对表格进行行高、行宽、字体及大小、居中、表格线进行处理
            //ColumnWidth "A:B"表示第一列和第二列, "A:A"表示第一列
            ((Excel.Range)myworkSheet.Columns["A:F", System.Type.Missing]).ColumnWidth = 15;     //列宽
            //RowHeight   "1:1"表示第一行, "1:2"表示,第一行和第二行 
            ((Excel.Range)myworkSheet.Rows["1:1", System.Type.Missing]).RowHeight = 20;



            myexcelRange = myworkSheet.Range[myworkSheet.Cells[1, 1], myworkSheet.Cells[knn+3, 7]];
            myexcelRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//指定对齐
            myexcelRange.Font.Size = 8;
            myexcelRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //  myworkSheet.get_Range("A1", "S1").Merge(myworkSheet.get_Range("A1", "O1").MergeCells);
            //  myworkSheet.get_Range("A2", "S2").Merge(myworkSheet.get_Range("A2", "O2").MergeCells);
            //   myworkSheet.get_Range("A3", "S3").Merge(myworkSheet.get_Range("A3", "O3").MergeCells);

            // CellRowID表示要修改的行，size表示要改成的高度，单位是mm  
            /* 
             excelRange = (Excel.Range)excelWorkSheet.Rows[CellRowID, Missing.Value];                     
             excelRange.RowHeight = size;                  
             excelRange = null;

             ((Excel.Range)ThisSheet.Rows["1:1 ", System.Type.Missing]).RowHeight = 28.5;   //行高
             ((Excel.Range)ThisSheet.Columns["A:A ", System.Type.Missing]).ColumnWidth = 0.85;     //列宽
             */
            //====================================
            //set visible the Excel will run in background 
            //两个选项可以设置，如下，visable属性设置为true的话，excel程序会启动；false的话，excel只在后台运行。
            //displayalert设置为true将会显示excel中的提示信息。


            myExcel.Visible = true;
            //set false the alerts will not display  
            myExcel.DisplayAlerts = false;


            //workBook.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);  
            //保存文件，关闭workbook 
            myworkBook.SaveAs(fnxls);
            myworkBook.Close(false, Missing.Value, Missing.Value);

            //quit and clean up objects  
            //退出并清理objects，回收内存
            myExcel.Quit();
            myworkSheet = null;
            myworkBook = null;
            myExcel = null;
            GC.Collect();




            //============
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (project_yes == true)
            {
                DialogResult dr1 = MessageBox.Show("原有项目数据是否保存？", "数据是否保存对话框",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning
                                   );
                if (dr1 == DialogResult.Yes)//顺时针编号
                {
                    filesave();
                }


            }
            
         Cpub.n_kzd = 0; Cpub.n_dir = 0;
         Cpub.mListkzd.Clear();
         Cpub.mListvdir.Clear();

         adj_yes = false;//是否平差计算完
         xy0_yes = false;//是否近似坐标计算完
         data_ok = false;//是否起算数据已经确认
         rpt_yes = false;
         prjfile=null ; kzdfile=null ;//项目文件名和控制点文件名
         dtro=0; dtco=0;
         xp0=0; yp0=0;//P点近似坐标
         dxd=0; dyd=0;//P点坐标改正数
         xp=0; yp=0;//P点平差后坐标
         pvv = 0;mmx = 0; mmy = 0;mxy=0;//精度评定
       
        
         pal=0; pbl=0; pcl=0;//法方程常数项
         sum_a = 0; sum_b = 0; sum_l = 0;//虚拟误差方程项
         kn = 0;//虚拟误差方程个数
         ddz=0;//定向角改正数
        //绘图所用变量


         pic_wdsize = 0;//绘图大小（宽和高一样的正方形）
        


         maxx=0; maxy=0; minx=0; miny=0; xx0=0; yy0=0;//最多最小坐标和中心坐标
         pax=0 ;pay=0;pdx=0; pdy=0;
         pxmt = 0;//10mm所代表的像素值
         kall = 0;skall=0;//显示比例及全图显示比例尺

        
         canDrag=false;
         gdx = 0; gdy = 0;//图形移动量
         tabControl1.Visible = true;
         label17.Visible = false;
         dataGridView1.RowCount = 0;
         dataGridView1.RowCount = 0;
        }

        private void Main_from_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr0 = MessageBox.Show("确实需要退出程序吗？", "检查对话框",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning
                                                 );
            if (dr0 == DialogResult.Yes)
            {
                if (Cpub.n_dir > 0)
                {
                    DialogResult dr1 = MessageBox.Show("数据要保存吗？", "数据保存检查对话框",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning
                                          );
                    if (dr1 == DialogResult.Yes)
                    {
                        filesave();
                    }
                }
                Environment.Exit(0);
                //System.Environment.Exit(0);
            }
             else 
                e.Cancel =true ;
            }

        private void textBox5_TextChanged(object sender, EventArgs e)//绘制误差椭圆比例尺发生变化事件
        {
                       
            pictureBox1.Refresh();
            pictureBox1.Image = getBitMapFile(kall, gdx, gdy);
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                panel1.Visible = true;
                xy0_ed_yes = true;

            }
            else
            {
                panel1.Visible = false;
                xy0_ed_yes = false;

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row0 = 0, column0 = 0;
            double tempv;
            string cellstr = null;
            
           
               // row = dataGridView2.SelectedCells[0].RowIndex;
                //column = dataGridView2.SelectedCells[0].ColumnIndex;
                row0 = dataGridView2.CurrentRow.Index;
                column0 = dataGridView2.CurrentCell.ColumnIndex;
               // if (dataGridView2.Rows[row1].Cells[column1].Value == null)
                   // MessageBox.Show("输入框中为NULL！！！");
            if (dataGridView2.Rows[row1].Cells[column1].Value != null) 
                //与if (dataGridView2.Rows[row1].Cells[column1].Value.ToString ()!= null)区别
            
                {
                    cellstr = dataGridView2.Rows[row1].Cells[column1].Value.ToString();
                    
                        if (column1 > 0)
                        {
                            if (!double.TryParse(cellstr, out tempv)) //与
                            {
                                MessageBox.Show("刚才输入的框中可能为空或有非法字符，请正确输入数值！！！");
                               
                            }

                        }

                }
                row1 = row0; 
                column1 = column0;
        }

     
           

    }//  public partial class Main_from : Form
}
