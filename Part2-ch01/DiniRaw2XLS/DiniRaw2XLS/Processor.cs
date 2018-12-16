using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DiniRaw2XLS.Entities;

namespace DiniRaw2XLS
{
    public static class Processor
    {
        /// <summary>
        /// 获取线路测量信息
        /// </summary>
        /// <param name="lines">线路数据行数组</param>
        /// <returns></returns>
        public static LineInfo GetLineInfo(string[] lines,ref string errInfo )
        {
            try
            {
                LineInfo theLine = new LineInfo();
                // 获取线路名
                string tempLine;
                for (int i = 0; i < lines.Length; i++)
                {
                    tempLine = lines[i];
                    if (tempLine.ToUpper().Contains(".DAT"))
                    {
                        theLine.LineName = tempLine.Substring(21, 27).Trim();
                        break;
                    }//endif
                }//endfor

                // 获取线路信息
                List<string> partLineList = new List<string>();
                List<PartInfo> partList = new List<PartInfo>();
                for (int i = 0; i < lines.Length; i++)
                {
                    tempLine = lines[i];
                    if (tempLine.Contains("Start")) partLineList = new List<string>();//测段开始
                    if (tempLine.Contains("End"))//测段结束
                    {
                        if (lines[i + 1] != null && lines[i + 1].Contains("Cont-Line"))//测段中断标识，测段未结束
                        {
                            i++;//跳一行
                            continue;
                        }//endif

                        partLineList.Add(tempLine);
                        partList.Add(GetPartInfo(partLineList)); // 获得一测段信息
                        continue;
                    }//endif
                    partLineList.Add(tempLine);
                }
                theLine.PartList = partList;
                return theLine;
            }
            catch(Exception ex)
            {
                errInfo = ex.Message;
                return null;
            }//endcatch
        }

        /// <summary>
        /// 获取一测段的测量信息
        /// </summary>
        /// <param name="partLineList"></param>
        /// <returns></returns>
        private static PartInfo GetPartInfo(List<string> partLineList)
        {
            int sightNum = 2; // 1测站测量次数
            string tempLine = "";
            PartInfo thePart = new PartInfo();
            List<string> CLDataLineList = new List<string>(); // 正常测量数据行列表
            List<int> CLDataLineID = new List<int>(); // 正常测量数据行序号
            for (int i = 0; i < partLineList.Count ; i++)
            {
                tempLine = partLineList[i];
                if (tempLine.Contains("Start-Line"))//测段开始标记行
                {
                    if (tempLine.Substring(37, 5).Trim().Length > 3) sightNum = 4; // 如aBFFB，BFFB等
                    thePart.partNum = Convert.ToInt32(tempLine.Substring(44, 4).Trim()); // 测段号
                    i +=1;//前进一行，获取线路起点信息
                    tempLine = partLineList[i];
                    thePart.StartPtName = tempLine.Substring(20, 9).Trim(); // 开始点名
                    thePart.PartCode = tempLine.Substring(29, 6).Trim(); // 测段代码
                    thePart.StartPtH = Convert.ToDouble(tempLine.Substring(96, 16).Trim()); // 起点高程
                }
                else if (tempLine.Contains("#####") || tempLine.Contains("Reading") ||
                        tempLine.Contains("repeated") || tempLine.Contains("Cont-Line"))//无效测量信息行
                {
                    continue;
                }
                else if (tempLine.Substring(49, 2) == "Sh")//测段累计高差行
                {
                    if (tempLine.Substring(72, 2) != "dz") continue;//线路中断测量后继续测量导致的

                    thePart.EndPtName = tempLine.Substring(20, 9).Trim(); // 结束点点名
                    thePart.EndPtH = Convert.ToDouble(tempLine.Substring(96, 16).Trim()); // 结束点高程
                }
                else if (tempLine.Substring(49, 2) == "Db")//测段累计视距行
                {
                    thePart.Db = Convert.ToDouble(tempLine.Substring(51, 15).Trim()); // 测段累计后视视距
                    thePart.Df = Convert.ToDouble(tempLine.Substring(74, 15).Trim()); // 测段累计前视视距
                }
                else if ( tempLine.Contains("End-Line"))//测段结束标记行
                    break;
                else//正常数据行
                {
                    CLDataLineList.Add(tempLine);
                    CLDataLineID.Add(i);
                }
            }

            // 获取测站数据
            double bsPtH; // 后视点高程
            bsPtH = thePart.StartPtH;
            List<StationInfo> STList = new List<StationInfo>(); // 测站列表
            for (int i = 0; i <= CLDataLineList.Count - 1; i += sightNum + 1)
            {
                StationInfo ST = new StationInfo();
                List<SightInfo> CLDataList = new List<SightInfo>(); // 测量数据列表
                ST.BPtH = bsPtH; // 后视点高程
                for (int j = 0; j <= sightNum; j++)
                {
                    if (j != sightNum) CLDataList.Add(GetCLData(CLDataLineList[i + j], CLDataLineID[i + j]));
                }
                ST.SightList = CLDataList;
                ST.Reset(); // 重新设置测站信息
                bsPtH = ST.FPtH;
                STList.Add(ST);
            }
            thePart.StationList = STList;
            thePart.Reset(); // 重新赋值测段信息
            return thePart;
        }

        /// <summary>
        /// 从一行测量数据中提取信息
        /// </summary>
        /// <param name="strLine">数据行</param>
        /// <param name="LineID">线路ID号</param>
        /// <returns></returns>
        private static SightInfo GetCLData(string strLine, int LineID)
        {
            SightInfo CLData = new SightInfo();
            CLData.LineID = LineID;
            CLData.Adr = System.Convert.ToInt32(strLine.Substring(10, 6).Trim());
            CLData.ptName = strLine.Substring(20, 9).Trim();
            CLData.TimeStr = strLine.Substring(35, 8).Trim();
            if (strLine.Substring(49, 2) == "Rb")
                CLData.SType = "B";
            else
                CLData.SType = "F";
            CLData.RD = System.Convert.ToDouble(strLine.Substring(51, 15).Trim()); // 中丝读数
            CLData.HD = System.Convert.ToDouble(strLine.Substring(74, 15).Trim()); // 视距

            return CLData;
        }
    }//endclass
}//endspace
