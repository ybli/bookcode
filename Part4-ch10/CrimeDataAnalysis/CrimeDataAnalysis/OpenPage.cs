using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FileHelper;
using PointPattern;
using System.Diagnostics;

namespace CrimeDataAnalysis
{
    public partial class OpenPage : Form
    {
        public OpenPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            FormBorderStyle = FormBorderStyle.None;
            label1.BringToFront();
            toolStripProgressBar1.Minimum = 0;//设置ProgressBar组件最小值为0
            toolStripProgressBar1.Maximum = 10;//Maximum最大值为10
            toolStripProgressBar1.MarqueeAnimationSpeed = 50;//设定进度快在进度栏中移动的时间段
            timer1.Start();//启动定时器


            this.toolStripStatusLabel1.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.timer2.Interval = 1000;
            this.timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //定时时间到了处理事件
            this.Hide();//隐藏本窗体
            MainForm mainForm = new MainForm();//实例化一个MainForm对象
            mainForm.Show();//显示窗体
            timer1.Stop();//定制定时器
        }

      
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
