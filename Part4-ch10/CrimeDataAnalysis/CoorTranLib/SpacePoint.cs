using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorTran
{
    /// <summary>
    /// 平面点
    /// </summary>
    public class SpacePoint
    {
        public string pointName;
        public double B, L;//经纬度，弧度形式;
        public double x, y;//高斯平面坐标
        public int L0;//高斯平面坐标的带号

        public SpacePoint()
        {
            pointName = string.Empty;
            B = -1;L = -1;
            x = -1;y = -1;
            L0 = -1;
        }
    }
}
