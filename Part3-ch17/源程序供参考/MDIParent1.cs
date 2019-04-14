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

namespace 开采沉陷计算
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "窗口 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
             
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            toolStripStatusLabel1.Text  = "准备就绪，点击导入文件";
        }

        private void btninputfile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        //用于存储多个文件夹路径的数组
        public static string[] filename;
        public static string[] safename;
        public static List<string> Lheader;  //存储点名
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                filename = openFileDialog1.FileNames;
                safename = openFileDialog1.SafeFileNames;

                int n = filename.Length;
                //生成文件夹选择项
                for (int i = 0; i < n; i++)
                {
                    CheckBox ch = new CheckBox();
                    ch.Location = new Point(30, 35 + i * 30);
                    ch.Size = new Size(105, 18);
                    ch.Text = safename[i];
                    this.groupBox1.Controls.Add(ch);
                    toolStripStatusLabel1.Text = "文件导入成功，选择数据文件后导入数据";

                }
            }
            catch
            {
                toolStripStatusLabel1.Text = "文件读取失败，请检查文件格式！";
            }
        }

        private void btninputdata_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckBox ch = new CheckBox();
                List<string> name = new List<string>();
                int chi = 0;
                foreach (CheckBox Ch in this.groupBox1.Controls)
                {
                    if (Ch.Checked == true)
                    {
                        name.Add(filename[chi]);
                    }
                    chi++;
                }
                if (name.Count != 2)
                {
                    toolStripStatusLabel1.Text = "请注意：所选数据个数为2！";
                }
                else
                {
                    //将选择的两组数据读入到表格
                    using (StreamReader sr1 = new StreamReader(name[0]))
                    {
                        Lheader = new List<string>();
                        List<string> L1x = new List<string>();
                        List<string> L1y = new List<string>();
                        List<string> L1z = new List<string>();
                        string line = sr1.ReadLine();
                        while ((line = sr1.ReadLine()) != null)
                        {
                            string[] data = line.Split(',');
                            Lheader.Add(data[0]);
                            L1y.Add(data[1]);
                            L1x.Add(data[2]);
                            L1z.Add(data[3]);
                        }
                        dataGridView1.Rows.Add(Lheader.Count);
                        for (int i = 0; i < Lheader.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells[0].Value = Lheader[i];
                            dataGridView1.Rows[i].Cells[1].Value = L1y[i];
                            dataGridView1.Rows[i].Cells[2].Value = L1x[i];
                            dataGridView1.Rows[i].Cells[3].Value = L1z[i];
                        }
                    }
                    using (StreamReader sr2 = new StreamReader(name[1]))
                    {
                        List<string> L1x = new List<string>();
                        List<string> L1y = new List<string>();
                        List<string> L1z = new List<string>();
                        string line = sr2.ReadLine();
                        while ((line = sr2.ReadLine()) != null)
                        {
                            string[] data1 = line.Split(',');
                            L1y.Add(data1[1]);
                            L1x.Add(data1[2]);
                            L1z.Add(data1[3]);
                        }
                        for (int i = 0; i < Lheader.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells[4].Value = L1y[i];
                            dataGridView1.Rows[i].Cells[5].Value = L1x[i];
                            dataGridView1.Rows[i].Cells[6].Value = L1z[i];
                        }
                    }
                }
            }
            catch
            {
                toolStripStatusLabel1.Text = "读取数据失败，请检查数据格式！";
            }

        }
        //存储点位的数组
        public static List<double> Lx1;  //前期x坐标值
        public static List<double> Ly1;  //前期y坐标值
        public static List<double> Lz1;  //前期z坐标值

        public static List<double> Lx2;  //后期x坐标值
        public static List<double> Ly2;  //后期y坐标值
        public static List<double> Lz2;  //后期z坐标值

        public static List<double> Lw;  //下沉值
        public static List<double> Li;  //倾斜值
        public static List<double> Lk;  //曲率值
        public static List<double> Lusin;  //垂直主断面水平移动值
        public static List<double> Lucos;  //沿主断面水平移动值
        public static List<double> Lesin;  //垂直主断面水平变形值
        public static List<double> Lecos;  //沿主断面水平变形值
        /// <summary>
        /// 距离计算方法
        /// </summary>
        /// <param name="x1">点1的横坐标值</param>
        /// <param name="x2">点2的横坐标值</param>
        /// <param name="y1">点1的纵坐标值</param>
        /// <param name="y2">点2的纵坐标值</param>
        /// <returns></returns>
        public static double Distance(double x1, double x2, double y1, double y2)
        {
            double dis = Math.Sqrt((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2));
            return dis;
        
        }
        /// <summary>
        /// 方位角计算方法
        /// </summary>
        /// <param name="x1">起点的横坐标值</param>
        /// <param name="y1">起点的纵坐标值</param>
        /// <param name="x2">终点的横坐标值</param>
        /// <param name="y2">终点的纵坐标值</param>
        /// <returns></returns>
        public static double Fwj(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double fwj = 0;
            double xxj = Math.Atan(Math.Abs(dy) / Math.Abs(dx));  //计算象限角
            if (dx >= 0)
            {
                if (dy >= 0)
                {
                    fwj = xxj;

                }
                else
                {
                    fwj = Math.PI * 2 - xxj;
                }
            }
            else
            {
                if (dy >= 0)
                {
                    fwj = Math.PI - xxj;
                }
                else
                {
                    fwj = Math.PI + xxj;
                }
            }
            return fwj;

        }

        private void btncaculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count >1)
                {
                    Lx1 = new List<double>();
                    Ly1 = new List<double>();
                    Lz1 = new List<double>();
                    Lx2 = new List<double>();
                    Ly2 = new List<double>();
                    Lz2 = new List<double>();

                    for (int i = 0; i < Lheader.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value != null)
                        {
                            Ly1.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value));
                            Lx1.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value));
                            Lz1.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value));

                            Ly2.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value));
                            Lx2.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value));
                            Lz2.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value));

                        }
                    }

                    //计算下沉值
                    Lw = new List<double>();
                    for (int i = 0; i < Lx1.Count; i++)
                    {
                        Lw.Add(Math.Round((Lz2[i] - Lz1[i]) * 1000, 0));
                    }

                    //计算倾斜值
                    Li = new List<double>();
                    List<double> ldt = new List<double>();  //存储两点间的水平距离
                    for (int i = 1; i < Lx1.Count; i++)
                    {
                        ldt.Add(Distance(Lx1[i], Lx1[i - 1], Ly1[i], Ly1[i - 1]));
                    }

                    for (int i = 0; i < ldt.Count; i++)
                    {
                        Li.Add(Math.Round((Lw[i] - Lw[i + 1]) / ldt[i], 1));
                    }

                    //计算曲率
                    Lk = new List<double>();
                    List<double> ldtsum = new List<double>();  //前后两点的距离和
                    for (int i = 0; i < ldt.Count - 1; i++)
                    {
                        ldtsum.Add(ldt[i] + ldt[i + 1]);
                    }

                    for (int i = 0; i < Li.Count - 1; i++)
                    {
                        Lk.Add(Math.Round((Li[i + 1] - Li[i]) / (ldtsum[i] * 0.5), 2));
                    }

                    //计算水平移动
                    Lusin = new List<double>();
                    Lucos = new List<double>();

                    List<double> dst12 = new List<double>();  //计算前后两期同一点的水平距离
                    List<double> fwj12 = new List<double>();    //存储前后两期的两点方位角值

                    for (int i = 0; i < Lx1.Count; i++)
                    {
                        dst12.Add(Distance(Lx2[i], Lx1[i], Ly2[i], Ly1[i]));
                        fwj12.Add(Fwj(Lx1[i], Ly1[i], Lx2[i], Ly2[i]));
                    }

                    double a = Fwj(Lx1[0], Ly1[0], Lx2[Ly2.Count - 1], Ly2[Ly2.Count - 1]);  //走向方位角

                    for (int i = 0; i < fwj12.Count; i++)
                    {
                        double da = a - fwj12[i];  //方位角之差
                        double usin = dst12[i] * Math.Sin(da);
                        double ucos = dst12[i] * Math.Cos(da);

                        Lusin.Add(Math.Round(usin * 1000, 0));
                        Lucos.Add(Math.Round(ucos * 1000, 0));
                    }
                    //计算水平变形
                    Lesin = new List<double>();
                    Lecos = new List<double>();

                    List<double> ldtsin = new List<double>();  //起始相邻两点垂直主断面上的位移差
                    List<double> ldtcos = new List<double>();  //起始相邻两点沿主断面上的位移差
                    List<double> Lfwj = new List<double>();    //起始相邻两点的方位角

                    for (int i = 0; i < Lx1.Count - 1; i++)
                    {
                        Lfwj.Add(Fwj(Lx1[i], Ly1[i], Lx1[i + 1], Ly1[i + 1]));
                    }

                    for (int i = 0; i < Lfwj.Count; i++)
                    {
                        double df = a - Lfwj[i];
                        ldtsin.Add(ldt[i] * Math.Sin(df));
                        ldtcos.Add(ldt[i] * Math.Cos(df));
                    }

                    List<double> dusin = new List<double>();    //sin方向水平移动之差
                    List<double> ducos = new List<double>();    //cos方向水平移动之差
                    for (int i = 1; i < Lusin.Count; i++)
                    {

                        dusin.Add(Lusin[i] - Lusin[i - 1]);
                        ducos.Add(Lucos[i] - Lucos[i - 1]);

                    }

                    for (int i = 0; i < dusin.Count; i++)
                    {
                        Lesin.Add(Math.Round(dusin[i] / ldtsin[i], 2));
                        Lecos.Add(Math.Round(ducos[i] / ldtcos[i], 2));
                    }

                    //输出结果
                    dataGridView2.Rows.Clear();
                    dataGridView2.Rows.Add(Lx1.Count);
                    for (int i = 0; i < Lx1.Count; i++)
                    {
                        dataGridView2.Rows[i].Cells[0].Value = Lheader[i];  
                        dataGridView2.Rows[i].Cells[1].Value = Lw[i]; 


                        dataGridView2.Rows[i].Cells[4].Value = Lucos[i]; 
                        dataGridView2.Rows[i].Cells[5].Value = Lusin[i]; 

                    }
                    for (int i = 0; i < Li.Count; i++)
                    {
                        dataGridView2.Rows[i].Cells[2].Value = Li[i]; 
                        dataGridView2.Rows[i].Cells[6].Value = Lecos[i]; 
                        dataGridView2.Rows[i].Cells[7].Value = Lesin[i];
                    }
                    for (int i = 0; i < Lk.Count; i++)
                    {
                        dataGridView2.Rows[i + 1].Cells[3].Value = Lk[i]; 
                    }
                    tabControl1.SelectedIndex = 1;
                    toolStripStatusLabel1.Text = "计算完成！";
                }
                else
                {
                    toolStripStatusLabel1.Text = "请确认是否导入数据！";

                }

            }
            catch
            {
                toolStripStatusLabel1.Text = "计算失败，请检查数据格式！";
                
            }        
        }

        private void btne_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //绘制下沉曲线
        private void btnw_Click(object sender, EventArgs e)
        {
            try
            {
                List<POINT> pp = new List<POINT>();
                if (Lw != null)
                {
                    for (int i = 0; i < Lw.Count; i++)
                    {
                        double x = (i + 1) * 5;
                        double y = Lw[i];
                        pp.Add(new POINT(x, y));
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "请先进行计算！";
                }

                var frm = new Draw(pp, "点号", "下沉值(mm)", "下沉曲线图");
                frm.Show(this);
                toolStripStatusLabel1.Text = "绘图完成！";
            }
            catch
            {
                toolStripStatusLabel1.Text = "绘图失败，请确认是否计算！";
            
            }
        }
        //绘制倾斜曲线
        private void btni_Click(object sender, EventArgs e)
        {
            try
            {
                List<POINT> pp = new List<POINT>();
                if (Li != null)
                {
                    pp.Add(new POINT(5, 0));
                    for (int i = 0; i < Li.Count; i++)
                    {
                        double x = (i + 2) * 5;
                        double y = Li[i];
                        pp.Add(new POINT(x, y));
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "请先进行计算！";
                }

                var frm = new Draw(pp, "点号", "倾斜值(mm/m)", "倾斜曲线图");
                frm.Show(this);
                toolStripStatusLabel1.Text = "绘图完成！";

            }
            catch
            {
                toolStripStatusLabel1.Text = "绘图失败，请确认是否计算！";
                
            }
        }
        //绘制曲率曲线图
        private void btnk_Click(object sender, EventArgs e)
        {
            try
            {
                List<POINT> pp = new List<POINT>();
                if (Li != null)
                {
                    pp.Add(new POINT(5, 0));
                    for (int i = 0; i < Lk.Count; i++)
                    {
                        double x = (i + 2) * 5;
                        double y = Lk[i];
                        pp.Add(new POINT(x, y));
                    }
                    pp.Add(new POINT((Lk.Count + 2) * 5, 0));
                }
                else
                {
                    toolStripStatusLabel1.Text = "请先进行计算！";
                }
                var frm = new Draw(pp, "点号", "曲率值(mm/m2)", "曲率曲线图");
                frm.Show(this);
                toolStripStatusLabel1.Text = "绘图完成！";

            }
            catch
            {
                toolStripStatusLabel1.Text = "绘图失败，请确认是否计算！";                
            }
        }
        //绘制沿主断面方向曲率曲线图
        private void btnux_Click(object sender, EventArgs e)
        {
            try
            {
                List<POINT> pp = new List<POINT>();
                if (Lw != null)
                {
                    for (int i = 0; i < Lucos.Count; i++)
                    {
                        double x = (i + 1) * 5;
                        double y = Lucos[i];
                        pp.Add(new POINT(x, y));
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "请先进行计算！";
                }
                var frm = new Draw(pp, "点号", "水平移动值(mm)", "沿主断面水平移动曲线图");
                frm.Show(this);
                toolStripStatusLabel1.Text = "绘图完成！";

            }
            catch
            {
                toolStripStatusLabel1.Text = "绘图失败，请确认是否计算！";                
            }
        }
        //绘制垂直主断面曲率曲线图
        private void btnuy_Click(object sender, EventArgs e)
        {
            try
            {
                List<POINT> pp = new List<POINT>();
                if (Lw != null)
                {
                    for (int i = 0; i < Lusin.Count; i++)
                    {
                        double x = (i + 1) * 5;
                        double y = Lusin[i];
                        pp.Add(new POINT(x, y));
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "请先进行计算！";
                }
                var frm = new Draw(pp, "点号", "水平移动值(mm)", "垂直主断面水平移动曲线图");
                frm.Show(this);
                toolStripStatusLabel1.Text = "绘图完成！";

            }
            catch
            {
                toolStripStatusLabel1.Text = "绘图失败，请确认是否计算！";                
            }
        }
        //绘制垂直主断面水平变形曲线图
        private void btnesin_Click(object sender, EventArgs e)
        {
            try
            {
                List<POINT> pp = new List<POINT>();
                if (Li != null)
                {
                    pp.Add(new POINT(5, 0));
                    for (int i = 0; i < Lecos.Count; i++)
                    {
                        double x = (i + 2) * 5;
                        double y = Lecos[i];
                        pp.Add(new POINT(x, y));
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "请先进行计算！";
                }

                var frm = new Draw(pp, "点号", "水平变形值(mm/m)", "沿主断面水平变形曲线图");
                frm.Show(this);
                toolStripStatusLabel1.Text = "绘图完成！";

            }
            catch
            {
                toolStripStatusLabel1.Text = "绘图失败，请确认是否计算！";                
            }
        }
        //绘制沿主断面水平变形曲线图
        private void btnecos_Click(object sender, EventArgs e)
        {
            try
            {
                List<POINT> pp = new List<POINT>();
                if (Li != null)
                {
                    pp.Add(new POINT(5, 0));
                    for (int i = 0; i < Lesin.Count; i++)
                    {
                        double x = (i + 2) * 5;
                        double y = Lesin[i];
                        pp.Add(new POINT(x, y));
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "请先进行计算！";
                }

                var frm = new Draw(pp, "点号", "水平变形值(mm/m)", "垂直主断面水平移动曲线图");
                frm.Show(this);
                toolStripStatusLabel1.Text = "绘图完成！";

            }
            catch
            {
                toolStripStatusLabel1.Text = "绘图失败，请确认是否计算！";                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件（*.txt）|*.txt|Excel文件|*.xls|所有文件|*.*";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    string s = "";
                    s += "-------------------------计算报告-------------------------" + "\r\n";
                    s += string.Format("{0,-15}", "点号") + string.Format("{0,-15}", "下沉值(mm)") + string.Format("{0,-15}", "倾斜(mm/m)") + string.Format("{0,-15}", "曲率(mm/m2)") + string.Format("{0,-20}", "沿主断面水平移动(mm)") + string.Format("{0,-20}", "垂直断面水平移动(mm)") + string.Format("{0,-20}", "沿主断面水平变形(mm/m)") + string.Format("{0,-20}", "垂直主断面水平变形(mm/m)") + "\r\n";

                    for (int i = 0; i < Lk.Count; i++)
                    {
                        if (dataGridView2.Rows[0].Cells[3].Value == null)
                            dataGridView2.Rows[0].Cells[3].Value = "      ";
                        s += string.Format("{0,-25}", dataGridView2.Rows[i].Cells[0].Value.ToString()) + string.Format("{0,-25}", dataGridView2.Rows[i].Cells[1].Value.ToString()) + string.Format("{0,-25}", dataGridView2.Rows[i].Cells[2].Value.ToString()) + string.Format("{0,-25}", dataGridView2.Rows[i].Cells[3].Value.ToString()) + string.Format("{0,-45}", dataGridView2.Rows[i].Cells[4].Value.ToString()) + string.Format("{0,-55}", dataGridView2.Rows[i].Cells[5].Value.ToString()) + string.Format("{0,-35}", dataGridView2.Rows[i].Cells[6].Value.ToString()) + string.Format("{0,-35}", dataGridView2.Rows[i].Cells[7].Value.ToString()) + "\r\n";
                    }
                    s += string.Format("{0,-25}", dataGridView2.Rows[Lk.Count].Cells[0].Value.ToString()) + string.Format("{0,-25}", dataGridView2.Rows[Lk.Count].Cells[1].Value.ToString()) + string.Format("{0,-25}", dataGridView2.Rows[Lk.Count].Cells[2].Value.ToString()) + string.Format("{0,-25}", dataGridView2.Rows[Lk.Count].Cells[3].Value.ToString()) + string.Format("{0,-45}", dataGridView2.Rows[Lk.Count].Cells[4].Value.ToString()) + string.Format("{0,-55}", dataGridView2.Rows[Lk.Count].Cells[5].Value.ToString()) + string.Format("{0,-35}", dataGridView2.Rows[Lk.Count].Cells[6].Value.ToString()) + string.Format("{0,-35}", dataGridView2.Rows[Lk.Count].Cells[7].Value.ToString()) + "\r\n";
                    s += string.Format("{0,-25}", dataGridView2.Rows[Lk.Count].Cells[0].Value.ToString()) + string.Format("{0,-25}", dataGridView2.Rows[Lk.Count].Cells[1].Value.ToString()) + string.Format("{0,-25}", "     ") + string.Format("{0,-25}", "        ") + string.Format("{0,-45}", dataGridView2.Rows[Lk.Count].Cells[4].Value.ToString()) + string.Format("{0,-55}", dataGridView2.Rows[Lk.Count].Cells[5].Value.ToString()) + string.Format("{0,-35}", "      ") + string.Format("{0,-35}", "   ") + "\r\n";
                    sw.Write(s);
                    sw.Close();
                    toolStripStatusLabel1.Text = "报告生成完成！";

                }
            }
            catch
            {
                toolStripStatusLabel1.Text = "文件保存失败，请确认数据格式！";
            }
        }

        private void viewMenu_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning );
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void fileMenu_Click(object sender, EventArgs e)
        {

        }

        private void 报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件（*.txt）|*.txt|Excel文件|*.xls|所有文件|*.*";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.ShowDialog();
        }
    }
}
 