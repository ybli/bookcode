using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiniRaw2XLS.Entities
{
    /// <summary>
    /// 测段信息类
    /// </summary>
    public class PartInfo
    {
        public string partName; // 测段名
        public int partNum; // 测段号
        public string PartCode;  // 测段代码
        public string StartPtName; // 开始点名
        public string EndPtName; // 结束点名
        public List<StationInfo> StationList; // 测站信息
        public int StationCount; // 测站数
        public double StartPtH; // 开始点高程
        public double EndPtH; // 结束点高程
        public double sh; // 累计高差
        public double dz; // 线路闭合差
        public double Db; // 累计后视距
        public double Df; // 累计前视距

        /// <summary>
        ///重新设置测段信息
        ///</summary>
        public void Reset()
        {
            partName = StartPtName + "-" + EndPtName; // 赋值测段名
            StationCount = StationList.Count;
            sh = 0.0;
            Db = 0.0;
            Df = 0.0;
            for (int i = 0; i <= StationList.Count - 1; i++)
            {
                if (i > 0)StationList[i].BPtH = StationList[i - 1].FPtH;
                StationList[i].Reset();
                sh += StationList[i].DeltH;
                Db += (StationList[i].Db1 + StationList[i].Db2) / (StationList[i].SightList.Count/2);
                Df += (StationList[i].Df1 + StationList[i].Df2) / (StationList[i].SightList.Count / 2);
            }
            dz = EndPtH - (StartPtH + sh);
        }
    }
}
