using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraverseAdjust.Entities
{
    /*
    * 功能概要：导线实体类
    * 编号：TA_Entity_003
    * 作者：廖振修
    *  创建日期:2016-06-09
    */
    public class TraverseLine
    {
        public int NetType; // 1-附和导线；2-闭合导线
        public int AngleType; // 1-左角；2-右角
        public List<MyLine> Lines; // 直线列表
        public KnowedObsData KnowedObsData; // 已知信息和观测值数据对象
        public bool HasAdjust;//是否已经平差标识
    }//endclass
}//endspace
