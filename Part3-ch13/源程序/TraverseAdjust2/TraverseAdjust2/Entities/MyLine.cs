using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraverseAdjust.Entities
{
    /*
     * 功能概要：测线实体类
     * 编号：TA_Entity_002
     * 作者：廖振修
     *  创建日期:2016-06-09
     */
    public class MyLine
    {
        public string Name;
        public byte Type; // 线类型：1-已知点连线，2-未知点与已知点，或未知点与未知点连线；
        public MyPoint StartPt;
        public MyPoint EndPt;
        public double Distance; // 距离
        public double Direction;
    }//endspace
}//endspace
