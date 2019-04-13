using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenParameterTransformation
{
    public class Point
    {
        public string name { get; set; }    //点名

        public double X { get; set; }   //X坐标
        public double Y { get; set; }   //Y坐标
        public double Z { get; set; }   //Z坐标

        public double X1 { get; set; }  //转换后X坐标
        public double Y1 { get; set; }  //转换后Y坐标
        public double Z1 { get; set; }  //转换后Z坐标

        public double deltaX { get; set; }  //X坐标改正数
        public double deltaY { get; set; }  //Y坐标改正数
        public double deltaZ { get; set; }  //Z坐标改正数

    }
}
