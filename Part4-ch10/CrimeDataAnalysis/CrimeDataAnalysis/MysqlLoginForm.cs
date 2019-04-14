using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileHelper;

namespace CrimeDataAnalysis
{
    public partial class MysqlLoginForm : Form
    {
        public MysqlLoginForm()
        {
            InitializeComponent();
        }

        //定义全局变量
        public DataTable m_dataSource = new DataTable();
        public string serverName, uid, pwd, databaseName, datatableName;

        private void MysqlLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //连接按钮
            DataTable databaseTable = MysqlDataIO.DatabasesTable(textBox1.Text, textBox2.Text, textBox3.Text);
            int rowNum = databaseTable.Rows.Count;
            DataBaseList.Items.Clear();
            for (int i = 0; i < rowNum; i++)
            {
                DataBaseList.Items.Add(databaseTable.Rows[i][0]);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //确定按钮，选择数据库
            if (DataBaseList.SelectedItem==null|| DataTableList.SelectedItem==null)
            {
                return;
            }
            m_dataSource=MysqlDataIO.ReadMysqlData(textBox1.Text, textBox2.Text, textBox3.Text,
                DataBaseList.SelectedItem.ToString(), DataTableList.SelectedItem.ToString());
            //获取数据库配置，存入变量内
            serverName = textBox1.Text;
            uid = textBox2.Text;
            pwd = textBox3.Text;
            databaseName = DataBaseList.SelectedItem.ToString();
            datatableName = DataTableList.SelectedItem.ToString();
            this.Close();
        }

        private void DataBaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //选择的数据库改变时触发
            DataTable datatableTable = MysqlDataIO.DatatableTable(textBox1.Text, textBox2.Text, textBox3.Text,
                DataBaseList.SelectedItem.ToString());
            int rowNum = datatableTable.Rows.Count;
            DataTableList.Items.Clear();
            for (int i = 0; i < rowNum; i++)
            {
                DataTableList.Items.Add(datatableTable.Rows[i][0]);
            }
        }
    }
}
