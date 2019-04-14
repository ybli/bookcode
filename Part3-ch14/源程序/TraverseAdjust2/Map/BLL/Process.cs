using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Map.Entities;
using Map.UI;

namespace Map.BLL
{
    /*
  * 功能概要：绘图业务处理类
  * 编号：Map_Process_001
  * 作者：廖振修
  *  创建日期:2016-06-09
  */
    public  class Process
    {
        /// <summary>
        /// 显示绘图窗口
        /// </summary>
        /// <param name="gList">图元列表</param>
        public void ShowMap(List<GElement> gList)
        {
            frmMap frmObj = new frmMap();
            frmObj.gList = gList;
            frmObj.ShowDialog();
        }
    }//endclass
}//endspace
