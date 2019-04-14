using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARModel
{
    public partial class Export : Form
    {
        int i_MutStep; //用户选择的步数
        private Transmit transmit;//接受AR窗体传过来的函数
        public Export(Transmit translate, int Mul)
        {
            this.i_MutStep = Mul;
            this.transmit = translate;

            InitializeComponent();
            for (int i = 0; i < i_MutStep; i++)
            {
                comboBox1.Items.Add(i + 1);
            }
            this.FormBorderStyle = FormBorderStyle.Fixed3D;//不允许放大缩小对话框
            this.MaximizeBox = false;//不设置最大化图标
            this.MinimizeBox = false;//不设置最小化图标

        }

     

      

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择报表的预测步数！");
                return;
            }
            transmit(Convert.ToInt32(comboBox1.Items[comboBox1.SelectedIndex]));
            this.Close();
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            transmit(0);
            this.Close();
        }

       
    }
}
