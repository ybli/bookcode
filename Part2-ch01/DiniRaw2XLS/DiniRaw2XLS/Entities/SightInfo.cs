using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiniRaw2XLS.Entities
{
    /// <summary>
    /// 单次观测信息类
    /// </summary>
    public class SightInfo
    {
        public int LineID; // 所在原始数据行号
        public string ptName;
        public double RD;
        public double HD;
        public int Adr;
        public string TimeStr;
        public string SType; // 测量类型：后视B，前视F
    }
}
