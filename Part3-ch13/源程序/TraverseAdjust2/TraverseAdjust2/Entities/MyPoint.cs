using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraverseAdjust.Entities
{
    /*
     * 功能概要：测点实体类
     * 编号：TA_Entity_001
     * 作者：廖振修
     *  创建日期:2016-06-09
     */
    public class MyPoint
    {
        public string Name;
        public byte Type; // 点类型:1-已知点,2-未知点；
        public double X;
        public double Y;
        public double H;
    }//endclass
}//endspace
