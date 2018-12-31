using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 利用构建规则格网_grid_进行体积计算
{
    public struct given_point            //定义已知点的结构体（点名，X坐标，Y坐标，H高程）
        {
            public string point_name;
            public float X;
            public float Y;
            public float H;
        }
    public partial class 规则网格进行体积计算 : Form
    {
        public 规则网格进行体积计算()
        {
            InitializeComponent();
        }     
        List<given_point> g_point = new List<given_point>();  //定义已知点列表
        float height_datum=9, grid_spacing=1;//定义基准高程，网格间隔
        List<given_point> convex_hull = new List<given_point>();
        float[,] minmax;    //散点数据中最大最小X，Y值
        void  diff_grid_size_report(int grid_spacing)      //根据不同的网格大小，输出报告
        {
            体积计算 vol_cal = new 体积计算();
            float[,] out_matrix = vol_cal.out_matrix(minmax, grid_spacing);                     //外包矩形
            int get_in_gird_num = vol_cal.get_in_gird_num(convex_hull);                         //凸包内的格网数
            double r = (minmax[1, 0] - minmax[0, 0]) + (minmax[1, 1] - minmax[0, 1]) * 0.5 * 0.4;    //搜索圆半径
            double V = vol_cal.get_V(r, grid_spacing, height_datum, g_point);                        //体积

            //输出体积信息\r\n
            report.Text = report.Text + "-------------------------基本信息---------------------\r\n";
            report.Text = report.Text + "基准高程：   " + height_datum + "\r\n";
            report.Text = report.Text + "网格间隔：   " + grid_spacing + "\r\n";
            report.Text = report.Text + "网格横格：   " + Math.Ceiling((minmax[1, 0] - minmax[0, 0]) / grid_spacing) + "\r\n";
            report.Text = report.Text + "网格纵格：   " + Math.Ceiling((minmax[1, 1] - minmax[0, 1]) / grid_spacing) + "\r\n";
            report.Text = report.Text + "总网格数：   " + Math.Ceiling((minmax[1, 0] - minmax[0, 0]) / grid_spacing) * Math.Ceiling((minmax[1, 1] - minmax[0, 1]) / grid_spacing) + "\r\n";
            report.Text = report.Text + "凸包内的网格数：   " + get_in_gird_num + "\r\n";
            report.Text = report.Text + "体积：   " + V.ToString("f3") + "\r\n";
            report.Text = report.Text + "-----------点位信息---------\r\n";
            report.Text = report.Text + "外包矩形的顶点坐标：   \r\n";
            report.Text = report.Text + "X坐标       Y坐标      \r\n";
            for (int i = 0; i < 4; i++)
            { report.Text = report.Text + out_matrix[i, 0].ToString().PadRight(10) + out_matrix[i, 1].ToString().PadRight(10) + "\r\n"; }
        }

        private void openfile_Click(object sender, EventArgs e)//打开读取数据文件
        {
            try     // 进行错误处理
           {
                if (datatable.Rows.Count != 1)
                { datatable.Rows.Clear();  g_point.Clear(); convex_hull.Clear();  report.Clear();  } //如果当前数据表格中有数据，在
                                                                                       //这次导入数据前清空表格。
                OpenFileDialog opf = new OpenFileDialog();   //创建文件对话框
                opf.Filter = "文本文件(*.txt)|*.txt";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    string rpath = opf.FileName;            //获得文件地址     
                    StreamReader sr = new StreamReader(rpath);   //打开文件

                    int g_point_num = 0;   //记录有多少已知点位
                    given_point gp = new given_point();  //  临时存放点位信息

                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Trim().Split(',', '，');  //读取文件，并分割到字符串数组中
                    if (line.Length == 2)    //获得基准高程
                    {
                        height_datum = float.Parse(line[1]);
                    }
                    else if (line.Length == 4)     //获得已知点信息
                    {
                        gp.point_name = line[0];
                        gp.X = float.Parse(line[1]);
                        gp.Y = float.Parse(line[2]);
                        gp.H = float.Parse(line[3]);
                        g_point.Add(gp);          //把已知点放到列表中
                        datatable.Rows.Add();     //增加数据表格的列，并显示点位信息
                        datatable.Rows[g_point_num].Cells[0].Value = g_point[g_point_num].point_name;
                        datatable.Rows[g_point_num].Cells[1].Value = g_point[g_point_num].X;
                        datatable.Rows[g_point_num].Cells[2].Value = g_point[g_point_num].Y;
                        datatable.Rows[g_point_num].Cells[3].Value = g_point[g_point_num].H;
                        g_point_num++;
                    }
                }
                    基准高程.Text = height_datum.ToString ();
                    网格间隔.Text = grid_spacing .ToString ();
                    status.Text = "导入数据";
               }              
            }
          catch 
          {
               MessageBox.Show("输入文件有误，无法正确读取！");
          }
            datatable.BringToFront();
        }      
        private void get_V_Click(object sender, EventArgs e)            //得到体积，并输出报告
        {
            if (g_point.Count == 0) { status.Text = "没有数据，无法计算"; return; }
            if (convex_hull.Count == 0)
            {
                this.生成凸包_Click(this,e);
            }
            given_point  p0 = convex_hull[0];                        //得到p0点
            report .Text = "";
            
            //计算体积信息
            height_datum = float.Parse (基准高程.Text);     //获得最新的基准高程和网格间隔信息
            grid_spacing = float.Parse(网格间隔.Text);
            diff_grid_size_report(1);                       //不同的网格间隔大小输出不同的报告
            diff_grid_size_report(5);
            diff_grid_size_report(10);
            //输出报告：      
            report.Text = report .Text +"\r\n报告基点是:\r\n";
            report.Text = report.Text + "点号          X坐标           Y坐标           H高程\r\n";
            report.Text = report.Text + p0.point_name.PadRight(14) + p0.X.ToString().PadRight(14)+
                p0.Y.ToString().PadRight(14) + p0.H.ToString().PadRight(15)+"\r\n";
            report.Text = report.Text + "--------------------------凸包点----------------------\r\n";
            report.Text = report.Text + "点号         X坐标          Y坐标         H高程\r\n";
            for (int i = 0; i < convex_hull.Count(); i++)
            {
                report.Text = report.Text + convex_hull[i].point_name.PadRight(14) + convex_hull[i].X.ToString().PadRight(14) +
                convex_hull[i].Y.ToString().PadRight(15) + convex_hull[i].H.ToString().PadRight(15) + "\r\n";
            }        
            report.BringToFront();

            status.Text = "生成报告";
        }
        private void savereport_Click(object sender, EventArgs e)    //保存报告
        {
            if (report.Text.Length == 0) { MessageBox.Show("报告为空，无法保存。");return; }
            else
            {
                SaveFileDialog svf = new SaveFileDialog();
                svf.Filter = "文本文件(*.txt)|*.txt";
                DialogResult ok_cancel = svf.ShowDialog();
                if (ok_cancel  == DialogResult.OK)
                {
                    string spath = svf.FileName;                 
                    if (File.Exists(spath))   //若同文件名文件已经存在，则删除
                    {
                        File.Delete(spath);
                    }
                    StreamWriter sw = new StreamWriter(spath);                  
                    sw.WriteLine(report.Text);
                    sw.Close();
                    status.Text = "报告保存成功";
                }
                
            }
        }
        private void save_DXF_Click(object sender, EventArgs e)   //保存图形为.dxf文件
        {
            if (convex_hull .Count ==0) { MessageBox.Show("没有生成凸包！");return; }
            SaveFileDialog svf = new SaveFileDialog();
            svf.Filter = "(*.dxf)|*.dxf";
            DialogResult ok_cancel = svf.ShowDialog();
            if (ok_cancel == DialogResult.OK)
            {
                string dxf_path = svf.FileName;
                DRAW draw = new DRAW();
                draw.save_dxf(dxf_path, g_point, convex_hull, minmax,grid_spacing );
                status.Text = "保存图像成功";              
            }                       
        }

        private void 退出_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 数据表格_Click(object sender, EventArgs e)
        {
            datatable.BringToFront();
            status.Text = "表格显示";
        }

        private void 凸包图形_Click(object sender, EventArgs e)
        {  
            //初始化图片的大小
            pictureBox1.Width = 650;                 
            pictureBox1.Height = 400;
            panel1.BringToFront();
            status.Text = "图像显示";
        }

        private void 报告_Click(object sender, EventArgs e)
        {
            report.BringToFront();
            status.Text = "报告显示";
        }

        private void 图像放大_Click(object sender, EventArgs e)
        {
            pictureBox1.Width += pictureBox1.Width/5;
            pictureBox1.Height += pictureBox1.Height/5;
            status.Text = "图像放大";
        }

        private void 图像缩小_Click(object sender, EventArgs e)
        {
            pictureBox1.Width -= pictureBox1.Width / 5;
            pictureBox1.Height -= pictureBox1.Height / 5;
            status.Text = "图像缩小";
        }

        private void 打开文件_Click(object sender, EventArgs e)
        {
            openfile_Click(this, e);
        }

        private void 保存文件_Click(object sender, EventArgs e)
        {
            savereport_Click(this, e);
        }

        private void 退出1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 凸包点示意图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            生成凸包_Click(this, e);
        }

        private void 体积计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            get_V_Click(this, e);
        }

        private void help_Click(object sender, EventArgs e)
        {
            MessageBox.Show ("使用步骤：\r\n一：导入数据（必须是TXT文档）\r\n二：生成凸包（得到并显示凸包图），" +
                "也可以直接体积报告（得到报告，同时得到凸包图但不显示）\r\n三：可以点击软件左下角的不同的显示窗口查看不同的数据形式" +
                "\r\n四：可以选择保存报告（txt格式），也可以选择保存图片（与cad交互的dxf格式）。","使用提示");
        }

        private void 生成凸包_Click(object sender, EventArgs e)         //图形显示
        {
            if (g_point.Count == 0) { status.Text = "没有数据，无法生成凸包"; return; }
            if (convex_hull.Count == 0&&g_point .Count !=0)
            {
                生成凸包 create_c_h = new 利用构建规则格网_grid_进行体积计算.生成凸包(g_point);
                minmax = create_c_h.getminmax();                //得到数据中最大最小X，Y值


              //convex_hull = create_c_h.get_convex_hull();              //              Graham'sScan算法  得到凸包点
                convex_hull = create_c_h.conver_h1(g_point);            //               快速凸包法        得到的凸包点



            }
            grid_spacing = float.Parse(网格间隔.Text);                   //获得最新的网格间隔信息

            DRAW draw = new DRAW();                        //定义画图类
            Bitmap bmp = draw.get_picture(g_point, convex_hull, grid_spacing, minmax);   //得到画布
            pictureBox1.BackgroundImage = bmp;                                //得到图片
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;              
            panel1.Controls.Add(pictureBox1);                    //放入容器

            pictureBox1.Width = 650;                  //初始化图片的大小
            pictureBox1.Height = 400;
            panel1.BringToFront();
            status.Text = "生成凸包，并显示凸包";
        }

    }
}
