using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace FileHelper
{
    public class MysqlDataIO
    {
        public static DataTable ConnectMysql(string server,string uid,string pwd,string mysqlCmd)
        {
            MySqlConnection conn;
            MySqlDataAdapter myadp;
            DataSet myds;
            DataTable databasesTable = new DataTable();//存储数据库的表格
            string MyConnectionString;
            MyConnectionString = String.Format("server = {0}; uid = {1}; pwd = {2}; database = mysql",
                server, uid, pwd);
            try

            {
                //打开菜单配置数据库连接
                conn = new MySqlConnection();   // 实例化数据库连接（instanced）
                conn.ConnectionString = MyConnectionString;   // 配置连接（configured）
                conn.Open();   // 打开连接（opened）
                //MessageBox.Show("连接成功！");
                myadp = new MySqlDataAdapter(mysqlCmd, conn);//输入命令
                myds = new DataSet();
                // 填充和绑定数据
                myadp.Fill(myds);
                databasesTable = myds.Tables[0];
                conn.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("不能连接到数据库服务器，请联系数据库管理员！"); break;
                    case 1045:
                        MessageBox.Show("无效的用户名/密码,请重试！"); break;
                    case 1049:
                        MessageBox.Show("数据库不存在，或数据库名错误"); break;
                    default:
                        MessageBox.Show(ex.Message); break;
                }
            }
            return databasesTable;
        }

        public static DataTable DatabasesTable(string server, string uid, string pwd)
        {
            return ConnectMysql(server, uid, pwd, "show databases;");
        }

        public static DataTable DatatableTable(string server, string uid, string pwd,string databaseName)
        {
            string useDB = "use " + databaseName + ";";
            return ConnectMysql(server, uid, pwd, useDB + "show tables;");
        }

        public static DataTable ReadMysqlData(string server, string uid, string pwd,
            string databaseName,string datatableName)
        {
            DataTable table = new DataTable();
            string useDB = "use " + databaseName + ";";
            string useTable = "select * from " + datatableName + ";";
            string mysqlCmd = useDB + useTable;
            table = ConnectMysql(server, uid, pwd, mysqlCmd);
            return table;
        }
    }
}
