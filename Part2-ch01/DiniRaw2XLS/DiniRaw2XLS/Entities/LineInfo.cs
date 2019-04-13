using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiniRaw2XLS.Entities
{
    /// <summary>
    /// 线路信息类，对应一个观测文件中的所有测段信息
    /// </summary>
    public class LineInfo
    {
        public string LineName; // 线路名
        public List<PartInfo> PartList; // 测站信息

        /// <summary>
        /// 获得线路长度
        /// </summary>
        /// <returns></returns>
        public double GetLength()
        {
            double length = 0;
            for (int i = 0; i <= PartList.Count - 1; i++)
                length += PartList[i].Db + PartList[i].Df;
            return length;
        }
    }
}
