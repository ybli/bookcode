using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DiniRaw2XLS.Entities
{
    /// <summary>
    /// 测站信息类
    /// </summary>
    public class StationInfo
    {
        public string FPtName; // 前视点名
        public double FPtH; // 前视点高程
        public string BPtName; // 后视点名
        public double BPtH; // 后视点高程
        public List<SightInfo> SightList; // 测量信息
        public double Rb1, Rb2, Rf1, Rf2;//后视中丝读数，前视中丝读数
        public double Db1, Db2, Df1, Df2;//后视距，前视距
        public double DeltH; // 测站高差
        public double DeltD; // 测站视距差

        /// <summary>
        ///重新设置测站信息
        ///</summary>
        ///param name="BPtH">后视点高程</param>
        ///<remarks></remarks>
        public void Reset()
        {
            int rNum, fNum;//前视、后视观测次数
            rNum = fNum = 0;

            for (int i = 0; i < SightList.Count; i++)
            {
                if (SightList[i].SType == "B")//后视
                {
                    rNum += 1;
                    if (rNum == 1)//第1次
                    {
                        Rb1 = SightList[i].RD;
                        Db1 = SightList[i].HD;
                    }
                    else//第2次
                    {
                        Rb2= SightList[i].RD;
                        Db2= SightList[i].HD;
                    }//else
                    BPtName = SightList[i].ptName;
                }
                else//前视
                {
                    fNum += 1;
                    if (fNum == 1)//第1次
                    {
                        Rf1 = SightList[i].RD;
                        Df1 = SightList[i].HD;
                    }
                    else//第2次
                    {
                        Rf2 = SightList[i].RD;
                        Df2 = SightList[i].HD;
                    }//else
                    FPtName = SightList[i].ptName;
                }//endelse
            }//endfor

            DeltH = (Rb1 - Rf1 + Rb2 - Rf2) / (SightList.Count / 2);// 测站高差
            DeltD = (Db1 - Df1 + Db2 - Df2) / (SightList.Count / 2);// 测站视距差

            // 计算前视点标高
            FPtH = DeltH + BPtH;
        }
    }
}
