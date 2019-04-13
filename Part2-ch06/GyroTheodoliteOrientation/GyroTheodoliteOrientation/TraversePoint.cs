using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>
    /// 导线点
    /// </summary>
    class TraversePoint
    {
        private string _pointName;   // 点名
        private int _pointindex;   //点序号
        private double _xCoor;   // x坐标
        private double _yCoor;   // y坐标
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="rowText"></param>
        public TraversePoint(string[] rowText)
        {
            PointName = rowText[0];
            XCoor = Convert.ToDouble(rowText[1]);
            YCoor = Convert.ToDouble(rowText[2]);
        }

        public int Pointindex { get { return _pointindex;} set { _pointindex = value;} }
        public string PointName { get { return _pointName;} set { _pointName = value;} }
        public double XCoor { get { return _xCoor;} set { _xCoor = value;} }
        public double YCoor { get { return _yCoor; } set { _yCoor = value; } }
    }
}
