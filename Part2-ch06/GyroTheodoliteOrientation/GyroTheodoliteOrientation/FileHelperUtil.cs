using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class FileHelperUtil
    {
        /// <summary>
        /// 将数据保存为txt格式文本
        /// </summary>
        /// <param name="datatablegrid">DataGridView实例，数据框</param>
        /// <returns>txt格式文本</returns>
        public static String DataToTxt(DataGridView datatablegrid)
        {
            String datatable = "";
            for(int i=0;i < datatablegrid.RowCount;i++)
            {
                for(int j =0; j < datatablegrid.ColumnCount; j++)
                {
                    if(j == datatablegrid.ColumnCount - 1)
                    {
                        if( datatablegrid.Rows[i].Cells[j].Value == null)
                        {
                            datatable += "" +  '\n';
                        }
                        else
                        {
                            datatable += datatablegrid.Rows[i].Cells[j].Value.ToString() + '\n'; 
                        }                     
                    }
                    else
                    {
                        if(datatablegrid.Rows[i].Cells[j].Value == null)
                        {
                            datatable += "" +",";
                        }
                        else
                        {
                            datatable += datatablegrid.Rows[i].Cells[j].Value.ToString() + ",";
                        }
                    }
                }
            }
            return datatable;
        }


    }
}
