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

namespace 基于hausdorff_Frechet距离的线匹配算法
{
    public partial class 基于Hausdorff_Frechet距离的线匹配 : Form
    {
        class discriminant_result      //目视解译信息
        {
            public string model_name;
            public List<int[]> model_result = new List<int[]>();
        }

        static int readnum = 0;   //读取的是第几个图层信息
        shpfile shp1 ;
        shpfile   shp2;      //定义两个shpfile图层文件
        discriminant_result[] dis_result = new discriminant_result[5];
        List<double> all_rangeX = new List<double>();
        List<double> all_rangeY = new List<double>();
        int checked_status = 0;
        CREAT_REPORT creat_report = new 基于hausdorff_Frechet距离的线匹配算法.CREAT_REPORT();

       string Path = System.Environment.CurrentDirectory.ToString();
        public 基于Hausdorff_Frechet距离的线匹配()
        {
            InitializeComponent();          
        }
        private void open_shpfile_Click(object sender, EventArgs e)    //    打开shpfile文件
        {
            if (readnum == 0)                        //全部初始化
            {
                 shp1 = new shpfile();
                 shp2 = new shpfile();
                 dis_result[0] = null;
                 dis_result = new discriminant_result[5];
                 all_rangeX.Clear();
                 all_rangeY.Clear();
                 checkedListBox1.SetItemChecked(0, true);
                 checkedListBox1.SetItemChecked(1, true);
                 pictureBox1.BackgroundImage=new Bitmap (835, 470);
                 REPORT_show.Text="";
            };
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.InitialDirectory = Path;
               
                opf.Title = "请选择导入shp文件";
                             
                opf.Filter = "(*.shp)|*.shp";
                status_text("玩命加载中   请稍等……", Color.Black);          
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    
                    string shppath = opf.FileName;
                    if (readnum == 0)
                    {                  
                        readshp(shppath, shp1);
                        DATAshow(DATA_shpLine1, shp1);                     //显示读取的数据
                        DATA_shpLine2.Rows.Clear();
                        status_text("成功读取第一个shpfile文件", Color.Black);                       
                        readnum = 1;
                    }
                    else
                    {                  
                        readshp(shppath, shp2);
                        DATAshow(DATA_shpLine2, shp2);                          //显示读取的数据
                            status_text("成功读取第二个shpfile文件", Color.Black);
                        readnum = 0;
                    }
                        DATA_show.BringToFront();

                }
           }
            catch { MessageBox.Show("程序错误"); }
            
        }
        void readshp(string shppath ,shpfile shp)          //读取shpfile文件
        {
            
            BinaryReader br = new BinaryReader(new FileStream(shppath,FileMode.Open,
                FileAccess.Read ));         //建立读取文件的二进制文件流
            br.ReadBytes(24);               //读取头文件信息
            shp.filelength = shp.bigtolitter(br.ReadInt32());//读取文件长度及进行转换
            br.ReadInt32();
            shp.shapeType = br.ReadInt32();                 //读取图层的集合类型
            all_rangeX.Add(br.ReadDouble());             //读取该图层的坐标范围
            all_rangeY.Add(br.ReadDouble());
            all_rangeX.Add(br.ReadDouble());
            all_rangeY.Add(br.ReadDouble());
            br.ReadBytes(32);             //读取头文件信息结束

                                          //读取线实体信息
            
            while (br.PeekChar() != -1)
            {
                shpline shpl = new shpline();
                shpl.recordNumber = shp.bigtolitter(br.ReadInt32());         //该线实体序号  
                shpl.contentlength = shp.bigtolitter(br.ReadInt32());          //该线实体信息长度
                br.ReadUInt32();
                shpl.Box[0] = br.ReadDouble();                              //该线实体的范围（X，Y，最大最小）
                shpl.Box[1] = br.ReadDouble();
                shpl.Box[2] = br.ReadDouble();
                shpl.Box[3] = br.ReadDouble();
                shpl.numparts = br.ReadInt32();                              //线段的起点在总的点数组中的位置
                shpl.numpoints = br.ReadInt32();                              //该线实体的点的个数
                shpl.parts = new int[shpl.numparts];
                shpl.ponits = new PointD[shpl.numpoints];
                for (int i = 0; i < shpl.numparts; i++)
                {
                    shpl.parts[i] = br.ReadInt32();
                }

                for (int i = 0; i < shpl.numpoints; i++)                    //读取点信息
                {
                    shpl.ponits[i].name = (shp.line.Count + 1).ToString() + "-" + (i + 1); 
                   shpl.ponits[i].X = br.ReadDouble();
                   shpl.ponits[i].Y  = br.ReadDouble();                
                }
                shp.line.Add(shpl);
            }
           
        }
        void DATAshow(DataGridView DATA_shpLine ,shpfile shp)                  //在数据窗口显示数据
        {
            DATA_shpLine.Rows.Clear();                 //清空窗口
            int row_number = 0;
                DATA_shpLine.Rows.Add();
                DATA_shpLine.Rows[row_number].Cells[0].Value = "线段总数：";
                DATA_shpLine.Rows[row_number].Cells[1].Value = shp.line.Count;
                DATA_shpLine.Rows[row_number].Cells[2].Value = "图层类型：";
                DATA_shpLine.Rows[row_number++].Cells[3].Value = "线";
                for (int i = 0; i < shp.line.Count; i++)
                {
                    DATA_shpLine.Rows.Add();
                    DATA_shpLine.Rows[row_number].Cells[0].Value = "线段序号：";
                    DATA_shpLine.Rows[row_number].Cells[1].Value = shp.line[i].recordNumber;
                    DATA_shpLine.Rows[row_number].Cells[2].Value = "折线段数：";
                    DATA_shpLine.Rows[row_number++].Cells[3].Value = shp.line[i].numpoints -1;
                    for (int j = shp.line[i].parts[0]; j < shp.line[i].numpoints - 1; j++)
                    {
                        DATA_shpLine.Rows.Add();
                        DATA_shpLine.Rows[row_number].Cells[0].Value = shp.line[i]
                            .ponits[j].X.ToString("0.0000");
                        DATA_shpLine.Rows[row_number].Cells[1].Value = shp.line[i]
                            .ponits[j].Y.ToString("0.0000");
                        DATA_shpLine.Rows[row_number].Cells[2].Value = shp.line[i]
                            .ponits[j+1].X.ToString("0.0000");
                        DATA_shpLine.Rows[row_number++].Cells[3].Value = shp.line[i]
                            .ponits[j+1].Y.ToString("0.0000");
                    }
                }
           
        }
        private void 退出程序_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void 绘制图形_Click(object sender, EventArgs e)            //绘制两个.shp文件的信息 
        {
            try
            {
                if(shp1.line.Count == 0 && shp2.line.Count == 0){ status_text("请确保有图层信息，否则无法进行绘制！", Color.Red); }
                DRAW draw = new DRAW();
                Bitmap bmp;
                if (checked_status == 0)
                {
                    bmp = draw.get_picture(all_rangeX, all_rangeY, shp1, shp2);   //得到画布
                    pictureBox1.BackgroundImage = bmp;                           //得到图片                  
                }
                if (checked_status == 1)
                {
                    bmp = draw.get_picture(all_rangeX, all_rangeY, shp1, new shpfile());   //得到画布
                    pictureBox1.BackgroundImage = bmp;                           //得到图片
                }
                if (checked_status == 2)
                {
                    bmp = draw.get_picture(all_rangeX, all_rangeY, new shpfile(), shp2);   //得到画布
                    pictureBox1.BackgroundImage = bmp;                           //得到图片
                }
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                panel1.Controls.Add(pictureBox1);
                image_show.BringToFront();
                status_text("图形绘制成功并显示", Color.Black);
            }
            catch { status_text("请确保有图层信息，否则无法进行绘制！",Color.Red); }
        }
        private void image_big_Click(object sender, EventArgs e)//           图形放大
        {
            pictureBox1.Width += pictureBox1.Width/5;
            pictureBox1.Height += pictureBox1.Height / 5;
        }
        private void image_small_Click(object sender, EventArgs e)          //图形缩小
        {
            pictureBox1.Width -= pictureBox1.Width / 5;
            pictureBox1.Height -= pictureBox1.Height / 5;
        }
        private void image_unchang_Click(object sender, EventArgs e)          //图形复原  
        {
            pictureBox1.Width = 835;
            pictureBox1.Height = 464;
        }
        private void IMAGE_window_Click(object sender, EventArgs e)           //打开图形窗口
        {
            image_show.BringToFront();
            status_text("图形窗口显示", Color.Black);
        }
        private void DATA_window_Click(object sender, EventArgs e)             //打开数据窗口
        {
            DATA_show.BringToFront();
            status_text("数据窗口显示",Color.Black);
        }
        private void image_redraw_Click(object sender, EventArgs e)            //图形重绘
        {
            if (checkedListBox1.GetItemChecked(0) && checkedListBox1.GetItemChecked(1))   //两个选项均被选
            {
                checked_status = 0;
            }
            else if (checkedListBox1.GetItemChecked(0) && !checkedListBox1.GetItemChecked(1))   //第一个选项被选，第二个未被选
            { checked_status = 1; }
            else if (!checkedListBox1.GetItemChecked(0) && checkedListBox1.GetItemChecked(1))     //第一个选项未被选，第二个被选
            { checked_status = 2; }
            else
            { checked_status = 1; checkedListBox1.SetItemChecked(0, true); }
            绘制图形_Click(this, e);
            status_text("图形重绘并显示", Color.Red);

        }
        private void save_image_Click(object sender, EventArgs e)               //保存图形
        {
            if (pictureBox1.BackgroundImage == null) { status_text("图形窗口无图形，保存（.dxf）失败", Color.Red);return; }     
            SaveFileDialog svf = new SaveFileDialog();
            svf.Filter = "(*.dxf)|*.dxf";
            DialogResult ok_cancel = svf.ShowDialog();
            if (ok_cancel == DialogResult.OK)
            {
                string dxf_path = svf.FileName;
                if (File.Exists(dxf_path))   //若同文件名文件已经存在，则删除
                {
                    File.Delete(dxf_path);
                }
                DRAW draw = new DRAW();
                if (checked_status == 0)
                {
                draw.save_dxf(dxf_path, all_rangeX, all_rangeY,shp1, shp2);
                }
                if (checked_status == 1)
                {
                    draw.save_dxf(dxf_path, all_rangeX, all_rangeY, shp1, new shpfile());
                }
                  if (checked_status == 2)
                 {
                    draw.save_dxf(dxf_path, all_rangeX, all_rangeY, new shpfile(), shp2);
                }
                status_text("图形保存（.dxf）成功", Color.Black);
            }
        }
        private void report_Click(object sender, EventArgs e)             // 生成报告
        {
            try
            {
                if (shp1.line.Count == 0 || shp2.line.Count == 0) { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red);return; }
                status_text("努力计算中……，请稍等……", Color.Red);
                一键生成全部ToolStripMenuItem_Click(this, e);
                status_text("生成报告成功，报告显示", Color.Black);
            }
            catch { status_text( "请确保有两个图层信息，否则无法进行匹配！", Color.Red); }
        }
        private void data_show(List<List<double>> hausdorff_D,string str)         //报告数据显示
        {
            discriminant_result dis_re = new discriminant_result();
            dis_re.model_name = str;

            REPORT_show.Text += "----------------------------------"+str+"线匹配---------------------------------\r\n";          
            string match_info="--------------------------------------------------------\r\n"+
                "阈值："+ threshold_value.Text + "\r\n"+"图层1     ---匹配-->    图层2         距离值\r\n";            
            int m = hausdorff_D.Count / 10;                                               //用来处理图层有多与10个的情况,让他们分段显示
            for (int n = 0; n <= m; n++)
            {
                if (n == m)
                {
                    REPORT_show.Text += str.PadRight(22);
                    for (int i = n*10; i < hausdorff_D[0].Count; i++)
                    {
                        REPORT_show.Text += "  图层2线实体" + (i + 1);
                    }
                    REPORT_show.Text += "\r\n";
                    for (int j = m * 10; j < hausdorff_D.Count; j++)
                    {
                        REPORT_show.Text += "图层1线实体" + (j + 1).ToString().PadRight(20);
                        for (int jj = 0; jj < hausdorff_D[j].Count; jj++)
                        {
                            REPORT_show.Text += hausdorff_D[j][jj].ToString("0.0000").PadRight(14);
                            if (hausdorff_D[j][jj] < double.Parse(threshold_value.Text))
                            {
                                match_info += "线实体" + (j + 1).ToString().PadRight(5) + " ---->".ToString().PadRight(13)
                                      + "线实体" + (jj + 1).ToString().PadRight(8) + hausdorff_D[j][jj].ToString("0.0000") + "\r\n";
                                int[] a = new int[] { j + 1, jj + 1 };
                                dis_re.model_result.Add(a);
                            }
                        }
                        REPORT_show.Text += "\r\n";
                    }
                }
                else
                {
                    REPORT_show.Text += str.PadRight(22);
                    for (int i = n * 10; i < (n+1)*10; i++)
                    {
                        REPORT_show.Text += "  图层2线实体" + (i + 1);
                    }
                    REPORT_show.Text += "\r\n";

                    for (int j = n * 10; j < 10 * (n + 1); j++)
                    {
                        REPORT_show.Text += "图层1线实体" + (j + 1).ToString().PadRight(10);
                        for (int jj = 0; jj < hausdorff_D[j].Count; jj++)
                        {
                            REPORT_show.Text += hausdorff_D[j][jj].ToString("0.0000").PadRight(14);
                            if (hausdorff_D[j][jj] < double.Parse(threshold_value.Text))
                            {
                                match_info += "线实体" + (j + 1).ToString().PadRight(5) + " ---->".ToString().PadRight(13)
                                      + "线实体" + (jj + 1).ToString().PadRight(8) + hausdorff_D[j][jj].ToString("0.0000") + "\r\n";
                                int[] a = new int[] { j + 1, jj + 1 };
                                dis_re.model_result.Add(a);
                            }
                        }
                        REPORT_show.Text += "\r\n";
                    }
                }
            }
            if ("基于传统Hausdorff距离".Equals(str))                      //根据不同的model，把不同的结果放入不同的位置
            { dis_result[1] = dis_re; }
            else if ("基于改进的Hausdorff_SMHD距离".Equals(str))
            { dis_result[2] = dis_re; }
            else if ("基于离散Frechet距离".Equals(str))
            { dis_result[3] = dis_re; }
            else if ("基于平均Frechet距离".Equals(str))
            { dis_result[4] = dis_re; }
            else { }
            REPORT_show.Text += match_info+"\r\n  \r\n";
        }
        private void REPORT_window_Click(object sender, EventArgs e)
        {
            REPORT_show.BringToFront();
            status_text("报告窗口显示", Color.Black);
        }
        private void 绘制图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            绘制图形_Click(this, e);
        }
        private void 保存图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_image_Click(this, e);
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void 保存报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_report_Click(this, e);
        }
        private void 读取文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open_shpfile_Click(this, e);
        }
        private void save_report_Click(object sender, EventArgs e)                  //保存报告
        {
            if (REPORT_show.Text.Length==0) { status_bar.ForeColor = Color.Red; status_bar.Text = "报告中无内容，无法保存！"; }
            else
            {
                SaveFileDialog svf = new SaveFileDialog();
                svf.Filter = "文本文件(*.txt)|*.txt";
                DialogResult ok_cancel = svf.ShowDialog();
                if (ok_cancel == DialogResult.OK)
                {
                    string spath = svf.FileName;
                    if (File.Exists(spath))   //若同文件名文件已经存在，则删除
                    {
                        File.Delete(spath);
                    }
                    StreamWriter sw = new StreamWriter(spath);
                    sw.WriteLine(REPORT_show.Text);
                    sw.Close();
                    status_text("保存报告成功", Color.Black);
                }

            }
        }
        private void status_text(string str, Color c)
        {
            status_bar.ForeColor = c;
            status_bar.Text = str;
        }
        private void Help_Click(object sender, EventArgs e)                //帮助
        {
            MessageBox.Show("1.打开软件，点击打开文件，出现文件对话框，把.shp文件导入到软件中  <要进行计算要导入两个.shp文件，导入一个无法计算，但是可以绘制图形>。\r\n" +
                              " 2.点击绘图，可以形成图形，也可以先选择生成报告，直接形成报告。\r\n" +
                                "3.在软件的左下角有切换窗口的快捷按钮，可通过这里进行切换数据窗口，图形窗口，报告窗口。\r\n" +
                                 " 4.点击保存按钮可以保存报告到.txt文件。\r\n" +
                                    " 5.点图形保存可以把图形输出保存到.dxf文件中。\r\n","使用提示");
                                         
        }
        private void 基于传统Hausdorff距离_Click(object sender, EventArgs e)
        {
            try
            {
                if (shp1.line.Count == 0 || shp2.line.Count == 0) { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); return; }
                status_text("努力计算中……，请稍等……", Color.Red);
                status_text("生成报告成功，报告显示", Color.Black);

                List<List<double>> hausdorff_D = new List<List<double>>();
                hausdorff_D = creat_report.Hausdorff(shp1, shp2);
                REPORT_show.Text = "";
                data_show(hausdorff_D, "基于传统Hausdorff距离");
                REPORT_show.BringToFront();
            }
            catch { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); }
        }
        private void 基于离散Frechet距离_Click(object sender, EventArgs e)
        {
            try
            {
                if (shp1.line.Count == 0 || shp2.line.Count == 0) { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); return; }
                status_text("努力计算中……，请稍等……", Color.Red);
                status_text("生成报告成功，报告显示", Color.Black);

                List<List<double>> Frechet_D = new List<List<double>>();
                Frechet_D = creat_report.Feachet(shp1, shp2);
                REPORT_show.Text = "";
                data_show(Frechet_D, "基于离散Frechet距离");
                REPORT_show.BringToFront();
            }
            catch { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); }
        }
        private void 基于改进的Hausdorff_SMHD距离_Click(object sender, EventArgs e)
        {
            try
            {
                if (shp1.line.Count == 0 || shp2.line.Count == 0) { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); return; }
                status_text("努力计算中……，请稍等……", Color.Red);
                status_text("生成报告成功，报告显示", Color.Black);

                List<List<double>> im_hausdorff_D = new List<List<double>>();
                im_hausdorff_D = creat_report.improve_Hausdorff(shp1, shp2);
                REPORT_show.Text = "";
                data_show(im_hausdorff_D, "基于改进的Hausdorff_SMHD距离");
                REPORT_show.BringToFront();
            }
            catch { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); }
        }
        private void 基于平均Frechet距离ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (shp1.line.Count == 0 || shp2.line.Count == 0) { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); return; }
                status_text("努力计算中……，请稍等……", Color.Red);
                status_text("生成报告成功，报告显示", Color.Black);

                List<List<double>> aver_Frechet_D = new List<List<double>>();
                aver_Frechet_D = creat_report.aver_Feachet(shp1, shp2);
                REPORT_show.Text = "";
                data_show(aver_Frechet_D, "基于平均Frechet距离");
                REPORT_show.BringToFront();
            }
            catch { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); }
        }
        private void 一键生成全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (shp1.line.Count == 0 || shp2.line.Count == 0) { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); return; }
                status_text("努力计算中……，请稍等……", Color.Red);
                status_text("生成报告成功，报告显示", Color.Black);

                基于传统Hausdorff距离_Click(this, e);

                List<List<double>> im_hausdorff_D = new List<List<double>>();
                im_hausdorff_D = creat_report.improve_Hausdorff(shp1, shp2);
                data_show(im_hausdorff_D, "基于改进的Hausdorff_SMHD距离");

                List<List<double>> Frechet_D = new List<List<double>>();
                Frechet_D = creat_report.Feachet(shp1, shp2);
                data_show(Frechet_D, "基于离散Frechet距离");

                List<List<double>> aver_Frechet_D = new List<List<double>>();
                aver_Frechet_D = creat_report.aver_Feachet(shp1, shp2);
                data_show(aver_Frechet_D, "基于平均Frechet距离");
            }
            catch { status_text("请确保有两个图层信息，否则无法进行匹配！", Color.Red); }
        }
        private void 读入参考数据_Click(object sender, EventArgs e)
        {                 
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "(*.txt)|*.txt";
                status_text("玩命加载中   请稍等……", Color.Black);
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    dis_result[0] = new discriminant_result();
                    string rpath = opf.FileName;            //获得文件地址     
                    StreamReader sr = new StreamReader(rpath);   //打开文件
                    dis_result[0].model_name = sr.ReadLine().Trim();
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Trim().Split(',', '，');
                        int[] a = new int[] { int.Parse(line[0]), int.Parse(line[1]) };
                        dis_result[0].model_result.Add(a);
                    }
                    sr.Close();
                }
                REPORT_show.Text = "----------------------------------目视解译 线匹配---------------------------------\r\n";
                REPORT_show.Text += "图层1    -->  图层2" + "\r\n";
                for (int i = 0; i < dis_result[0].model_result.Count; i++)
                {
                    REPORT_show.Text += "线实体" + dis_result[0].model_result[i][0] + "  -->  " + "线实体" + dis_result[0].model_result[i][1]+"\r\n";
                }
                REPORT_show.BringToFront();
                status_text("读取参考数据成功", Color.Black);
              }
              catch { status_text("读取参考数据失败", Color.Red); }
        }
        private void 评价_Click(object sender, EventArgs e)      //生成匹配评价精度结果    
        {
            if (dis_result[0] == null) { status_text("请先读取参考数据！", Color.Red);  return; }
            try
            {
                一键生成全部ToolStripMenuItem_Click(this, e);
                REPORT_show.Text = "";
                REPORT_show.Text += "\r\n";
                REPORT_show.Text += "----------------------------------目视解译 线匹配---------------------------------\r\n";
                REPORT_show.Text += "图层1    -->  图层2" + "\r\n";
                for (int i = 0; i < dis_result[0].model_result.Count; i++)
                {
                    REPORT_show.Text += "线实体" + dis_result[0].model_result[i][0] + "  -->  " + "线实体" + dis_result[0].model_result[i][1] + "\r\n";
                }
                int real_ture = dis_result[0].model_result.Count;
                REPORT_show.Text += "\r\n";
                REPORT_show.Text += "----------------------------------线匹配结果评价---------------------------------\r\n";
                for (int i = 1; i < 5; i++)
                {
                    int model_true = 0;
                    REPORT_show.Text += "匹配模型：" + dis_result[i].model_name + "\r\n";
                    REPORT_show.Text += "图层1    -->  图层2" + "\r\n";
                    for (int ii = 0; ii < dis_result[i].model_result.Count; ii++)
                    {
                        REPORT_show.Text += "线实体" + dis_result[i].model_result[ii][0] + "  -->  " + "线实体" + dis_result[i].model_result[ii][1] + "\r\n";
                        for (int j = 0; j < dis_result[0].model_result.Count; j++)
                        {
                            if (dis_result[0].model_result[j][0] == dis_result[i].model_result[ii][0] && dis_result[0].model_result[j][1] == dis_result[i].model_result[ii][1])
                            { model_true++; break; }
                        }
                    }
                    if (dis_result[i].model_result.Count != 0)
                    {
                        REPORT_show.Text += "匹配精度（或叫匹配查准率）Matching precision（P）" + ((double)model_true / dis_result[i].model_result.Count * 100).ToString("0.000") + "%\r\n";
                    }
                    else { REPORT_show.Text += "匹配精度（或叫匹配查准率）Matching precision（P）" + 0.ToString("0.000") + "%\r\n"; }
                    REPORT_show.Text += "匹配查全率 Matching recall（R）:" + ((double)model_true / real_ture * 100).ToString("0.000") + "%\r\n";
                    REPORT_show.Text += "-------------------------------------------------------------------------------\r\n";
                }
                REPORT_show.BringToFront();

            }
            catch { status_text("请先确保图层信息完整！", Color.Red); return; }
        }
        private void 结果评价_Click(object sender, EventArgs e)
        {
            if (dis_result[0] == null)
            {
                读入参考数据_Click(this, e);
            }
            评价_Click(this, e);
        }
    }
}
