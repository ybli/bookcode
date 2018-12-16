using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraverseAdjust.Entities
{
    /*
    * 功能概要：原始观测值实体类
    * 编号：TA_Entity_003
    * 作者：廖振修
    *  创建日期:2016-06-09
    */
    public class KnowedObsData
    {
        public int NetType; // 路线类型（1—附和，2－闭合）；闭合导线作为附和导线的特例，后面2已知点与前已知点重合)
        public int AngleType; // 角度观测类型（1－左角，2－右角）；导线测量顺序默认按点名数组角标顺序；闭合导线除连接角有左角右角之分外，输入的是内角，都是右角
        public List<string> Pnames; // 点名列表
        public List<double> X0; // 已知点X坐标列表
        public List<double> Y0;// 已知点Y坐标列表
        public List<double> bb;// 水平角观测值列表
        public List<double> SS;// 水平距离观测值列表
    }//endclass
}//endspace
