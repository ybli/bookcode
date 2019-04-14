using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringPointsForDesignedPoints
{
    public class CalDevData
    {
        public string Name; // 点名

        public double X; // m

        public double Y; // m

        public double Z; // m

        public double Length; // 曲线长

        public double PMil; // 里程

        public double VDevVal; // 偏差

        public double DX; // m

        public double DY; // m

        public double DZ; // m

        public double TAzi; // 方位角

        public double LDevVal;

        public double LDevIteVal;

        public double TCorx; // 坐标x

        public double TCory; // 坐标y

        // 新建标识符，判断区段
        public string Poslabel; // 位置标签
    }
}
