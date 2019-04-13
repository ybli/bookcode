using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapIcon
{
    //配置文件——DXF
    class Configure_DXF
    {

        public double scale;

        //构造函数
        public Configure_DXF(double Scale)
        {
            scale = Scale;
        }

        //多点房屋
        public int DFColor = 6;

        //砼房屋
        public int TFColor = 6;
        public double TFSize_Text;
        public void TFSize()
        {
            TFSize_Text = scale;
        }

        //小路
        public int XLColor = 4;

        //公路
        public int GLColor = 4;

        //点号
        public int TextColor = 1;
        public double TextSize;
        public void Text()
        {
            TextSize = scale;
        }

        //路灯
        public int LDColor = 21;
        public double LdSize;
        public void Ldsize()
        {
            LdSize = scale / 2;
        }

        //草地
        public int CDColor = 3;
    }
}
