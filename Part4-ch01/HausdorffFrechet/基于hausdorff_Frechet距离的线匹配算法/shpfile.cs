using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 基于hausdorff_Frechet距离的线匹配算法
{
    class shpfile         //定义.shp文件的信息格式
    {
       public  int filelength;
       public int shapeType;
        public List<shpline> line=new List<shpline> ();
       public int bigtolitter(int num)         //对数据进行大端小端的转化（数据存储时的内存地址顺序的不同）
        {
            int reverse;//返回结果
            int bit0, bit1, bit2, bit3;           //进行位变换          
            bit0 = (num & 0X000000ff);
            bit1 = (num & 0X0000ff00)>>8;
            bit2 = (num & 0x00ff0000)>>16;
            bit3 = (int)(num & 0xff000000)>>24;
            ///reverse =(uint)(((num & 0X000000ff) << 24) | ((num & 0X0000ff00) << 16) | ((num & 0x00ff0000) << 8)
           //     | ((num & 0xff000000)));
            reverse = ((bit0<< 24) | (bit1 << 16) | (bit2 << 8)
                | (bit3));
            return reverse;
        }
    }
    class shpline                       //.shp文件中的实体信息
    {
        public int recordNumber;
        public int contentlength;
        public double[] Box=new double[4];
        public int numparts;
        public int numpoints;
        public int[] parts;
        public PointD[] ponits;
    }
    public struct PointD              //定义文件中的点（点序号，坐标X，坐标Y）
    {
        public string name;
        public double X;
        public double Y;
    }
}
