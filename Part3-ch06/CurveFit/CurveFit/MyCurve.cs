using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurveFit
{
    public class MyCurve
    {
        #region
        //曲线表示方法说明
        //x=p0+p1*z+p2*z*z+p3*z*z*z
        //y=q0+q1*z+q2*z*z+q3*z*z*z
        //其中z为两点之间的弦长变量[0,1]
        #endregion

        public double p0, p1, p2, p3;
        public double q0, q1, q2, q3;
        public MyPoint mypoint_start;
        public MyPoint mypoint_end;
    }
}
