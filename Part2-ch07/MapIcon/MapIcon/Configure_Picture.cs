using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MapIcon
{
    //配置文件
    class Configure_Picture
    {
        //比例系数
        public double scale;

        //构造函数
        public Configure_Picture(double W)
        {
            scale = W / 50;
        }

        //多房
        public Pen DfPen = new Pen(Color.Magenta, 1);

        //砼房
        public Pen TfPen = new Pen(Color.Magenta, 1);
        public Brush TfBrush = new SolidBrush(Color.Magenta);

        //小路
        public Pen XlPen = new Pen(Color.Cyan, 2);

        //点号
        public Brush StringBrush = new SolidBrush(Color.Red);
        public Font StringFont;
        public void sF()
        {
            StringFont = new Font("宋体", float.Parse((scale / 2).ToString()), FontStyle.Regular);
        }

        //路灯
        public Pen LDPen = new Pen(Color.LightPink);
        public double LdSize;
        public void LDSize()
        {
            LdSize = (int)scale / 2;
        }


        //草地
        public Pen GrassPen = new Pen(Color.LawnGreen);
        public int GrassSize;
        public void CDSize()
        {
            GrassSize = (int)scale / 2;
        }

        //公路
        public Pen RoadPen = new Pen(Color.Cyan, 1);
    }
}
