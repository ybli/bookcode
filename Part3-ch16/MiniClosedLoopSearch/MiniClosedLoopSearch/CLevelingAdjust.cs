using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MyMatrix;//引用自定义的矩阵计算类。

namespace MiniClosedLoopSearch
{
    /// <summary>
    /// 水准网平差计算静态类
    /// </summary>
    static class CLevelingAdjust
    {
        public static List<LevelingPoint> levelingPoints = new List<LevelingPoint>();//水准点的集合，包括已知点和未知点。
        public static List<LevelingLine> levelingLines = new List<LevelingLine>();//观测的路线的集合。
        public static List<string> strLoopClosure = new List<string>();//返回闭合差计算后的闭合差计算结果的字符信息。
        public static int m_Pnumber;//水准网总点数，包括已知点和未知点。
        public static int m_kownPnumber;//已知点个数。
        public static double Alpha;//验前单位权中误差。
        public static int m_Lnumber;//高差观测值个数。
        public static int unknowPointNum;//已经计算出的未知点高程的点数。不能定义成局部变量。

        /// <summary>
        /// 搜索最短路径
        /// </summary>
        /// <param name="p">目标点的点号</param>
        /// <param name="exclude">编号等于exclude的观测值不得参加最短路线的链接。</param>
        /// <param name="neighbor">邻接点点号数组，数组长度等于总点数</param>
        /// <param name="diff">高差累加值数组，存储目标点沿最短路线到每点的高差之和，数组长度等于总点数,数组内容待计算。</param>
        /// <param name="S">路线长度累加值数组，存储目标点沿最短路线到每点的路线长度，数组长度等于总点数，数组内容待计算。</param>
        public static void FindShortPath(int p, int exclude, int[] neighbor, double[] diff, double[] S)
        {
            for (int i = 0; i < m_Pnumber; i++)
            {
                neighbor[i] = -1;//给每个点的邻接点号数组赋值-1，表示还没有邻接点。
                S[i] = Math.Pow(10, 30);//每点到目标点的初始路线长度等于无穷大。
            }

            S[p] = 0.0;
            diff[p] = 0.0;
            neighbor[p] = p;//目标点的邻接点点号就是自己的点号。

            for (int j = 0; ; j++)
            {
                bool unchanged = true;//表示所有点都找到其邻接点的点号了。
                for (int k = 0; k < m_Lnumber; k++)
                {
                    if (k == exclude)
                    {
                        continue;//表示跳出当前这一次循环。
                    }
                    int p1 = levelingLines[k].StarPoint.LevelingPointNum;//该观测路线起点的编号。
                    int p2 = levelingLines[k].EndPoint.LevelingPointNum;//该观测路线的终点的编号。
                    double S12 = levelingLines[k].LeveingRoadLength;//该观测路线的长度。
                    if (neighbor[p1] < 0 && neighbor[p2] < 0)//若起点和终点邻接点点号都没有找到，则跳出本次循环。
                    {
                        continue;//表示跳出当前这一次循环。
                    }
                    if (S[p2] > (S[p1] + S12))
                    {
                        neighbor[p2] = p1;
                        S[p2] = S[p1] + S12;
                        diff[p2] = diff[p1] + levelingLines[k].LeveingHeightDifferent;
                        unchanged = false;//不能少啊！否则就不在循环第二遍啦。
                    }

                    if (S[p1] > (S[p2] + S12))
                    {
                        neighbor[p1] = p2;
                        S[p1] = S[p2] + S12;
                        diff[p1] = diff[p2] - levelingLines[k].LeveingHeightDifferent;
                        unchanged = false;//不能少啊！否则就不在循环第二遍啦。
                    }

                }
                if (unchanged)
                {
                    break;
                }

            }
            return;
        }
        /// <summary>
        /// 环闭合差计算
        /// </summary>
        public static void LoopClosure()
        {

            string str1 = "\t=========环闭合差计算=============";
            strLoopClosure.Add(str1+"\r\n");
            int num = m_Lnumber - m_Pnumber + 1;//独立闭合环的个数。
            if (num < 1)
            {
                MessageBox.Show("该水准网无闭合环！");
                return;
            }
            int[] neighbor = new int[m_Pnumber];//邻接点号数组。
            int[] used = new int[m_Lnumber];//观测值是否已经用于闭合差计算。
            double[] diff = new double[m_Pnumber];//高差累加值数组。
            double[] S = new double[m_Pnumber];//每点到目标点的路线长数组。

            for (int i = 0; i < m_Lnumber; i++)
            {
                used[i] = 0;//初始化所有观测值都没有参加闭合差计算。
            }
            for (int j = 0; j < m_Lnumber; j++)
            {
                int k1 = 0;
                int k2 = 0;
                if (levelingLines.Count != 0)
                {
                    k1 = levelingLines[j].StarPoint.LevelingPointNum;//得到该观测值的起点编号。
                    k2 = levelingLines[j].EndPoint.LevelingPointNum;//得到该观测值的终点编号。
                    if (used[j] == 1)
                    {
                        continue;//若该观测值已经参加过闭合差计算，则跳出本次循环。
                    }
                }
                else
                {
                    MessageBox.Show("请先输入或导入数据，再进行闭合差计算！");
                    return;
                }
                FindShortPath(k2, j, neighbor, diff, S);
                if (neighbor[k1] < 0)
                {
                    //显示结果的字符串。
                    string str2 = string.Format("观测值{0}-{1}与任何观测边不构成闭合环" + "\r\n", levelingLines[j].StarPoint.StrLevelingPointName, levelingLines[j].EndPoint.StrLevelingPointName);
                    strLoopClosure.Add(str2);
                }
                else
                {
                    used[j] = 1;//该观测值已参加闭合差计算。
                    string str3 = "闭合环：" + "\r\n";
                    strLoopClosure.Add(str3);
                    int p1 = k1;
                    while (true)
                    {
                        int p2 = neighbor[p1];
                        string str4 = string.Format("{0}--", levelingPoints[p1].StrLevelingPointName);
                        strLoopClosure.Add(str4);
                        //将用过的边标定。
                        for (int r = 0; r < m_Lnumber; r++)
                        {
                            if (levelingLines[r].StarPoint.LevelingPointNum == p1 && levelingLines[r].EndPoint.LevelingPointNum == p2)
                            {
                                used[r] = 1;
                                break;
                            }
                            if (levelingLines[r].StarPoint.LevelingPointNum == p2 && levelingLines[r].EndPoint.LevelingPointNum == p1)
                            {
                                used[r] = 1;
                                break;
                            }
                        }
                        if (p2 == k2)
                        {
                            break;
                        }
                        else
                        {
                            p1 = p2;//继续寻找邻接点，直到邻接点点号是K2。
                        }
                    }
                    string str5 = string.Format("{0}--{1}" + "\r\n", levelingPoints[k2].StrLevelingPointName, levelingPoints[k1].StrLevelingPointName);
                    strLoopClosure.Add(str5);
                    double W = levelingLines[j].LeveingHeightDifferent + diff[k1];//闭合差。
                    double SS = S[k1] + levelingLines[j].LeveingRoadLength;//环的长度。
                    string str6 = string.Format("闭合差:W ={0} " + "\r\n" + "\r\n", -Math.Round(W, 5));
                    strLoopClosure.Add(str6);

                }

            }
        }
        /// <summary>
        /// 附合路线闭合差计算。
        /// </summary>
        public static void LineClosure()
        {
            string str1 = "\t=====附合路线闭合差计算=========";
            strLoopClosure.Add(str1+"\r\n");
            if (m_kownPnumber < 2)//已知点数小于2，则表明无符合路线。
            {
                return;
            }
            int[] neighbor = new int[m_Pnumber];//l邻接点号数组。
            double[] diff = new double[m_Pnumber];//高差累加数组。
            double[] S = new double[m_Pnumber];//路线长累加数组。

            for (int i = 0; i < m_Pnumber; i++)
            {
                if (levelingPoints[i].PointNature == PointNature.known)//如果该点是已知点
                {
                    //该方法的第二个参数-1表示每个观测值都参加最短路径的搜索。
                    FindShortPath(i, -1, neighbor, diff, S);//搜索到i号点的最短路线，用所有观测值。
                    for (int j = 0; j < m_Pnumber; j++)
                    {
                        if ((levelingPoints[j].PointNature == PointNature.known) && (levelingPoints[j].LevelingPointNum != levelingPoints[i].LevelingPointNum))//如果该点是已知点同时要求该点和上一个已知点不是同一个点。
                        {
                            if (neighbor[j] < 0)
                            {
                                string str2 = string.Format("{0}--{1}之间找不到最短路径。"+"\r\n", levelingPoints[i].StrLevelingPointName, levelingPoints[j].StrLevelingPointName);
                                strLoopClosure.Add(str2);
                                continue;
                            }
                            string str3 = "附合路线：" + "\r\n";
                            strLoopClosure.Add(str3);
                            int k = j;
                            while (true)
                            {
                                string str4 = string.Format("{0}--", levelingPoints[k].StrLevelingPointName);
                                strLoopClosure.Add(str4);
                                k = neighbor[k];
                                if (k == i)
                                {
                                    break;
                                }
                            }
                            string str5 = string.Format("{0}" + "\r\n", levelingPoints[i].StrLevelingPointName);
                            strLoopClosure.Add(str5);
                            double W = levelingPoints[i].LevelingHeight + diff[j] - levelingPoints[j].LevelingHeight;
                            string str6 = string.Format("闭合差W ={0} " + "\r\n" + "\r\n", -Math.Round(W, 5));
                            strLoopClosure.Add(str6);
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 高程近似值计算。
        /// </summary>
        public static void Cal_H0()
        {
            //计算每个点的近似高程
            for (int i = 0; ; i++)
            {
                for (int j = 0; j < m_Lnumber; j++)
                {
                    //如果某观测边起点高程已知，同时终点高程未知的情况。
                    if ((levelingLines[j].StarPoint.LevelingHeight != 0) && (levelingLines[j].EndPoint.LevelingHeight == 0))
                    {
                        levelingLines[j].EndPoint.LevelingHeight = levelingLines[j].StarPoint.LevelingHeight + levelingLines[j].LeveingHeightDifferent;
                        unknowPointNum++;
                    }
                    //如果某观测边终点高程已知，同时起点高程未知的情况。
                    if ((levelingLines[j].StarPoint.LevelingHeight == 0) && (levelingLines[j].EndPoint.LevelingHeight != 0))
                    {
                        levelingLines[j].StarPoint.LevelingHeight = levelingLines[j].EndPoint.LevelingHeight - levelingLines[j].LeveingHeightDifferent;
                        unknowPointNum++;
                    }

                    if (unknowPointNum == (m_Pnumber - m_kownPnumber))
                    {
                        MessageBox.Show("近似高程计算成功！");
                        return;
                    }
                    if (i > (m_Pnumber - m_kownPnumber))
                    {
                      //  string str1 = "下列点无法计算出概略高程：   ";
                        for (int k = 0; k < m_Pnumber; k++)
                        {
                            if (levelingPoints[k].LevelingHeight == 0)
                            {
                                string str2 = string.Format("{0}", levelingPoints[k].StrLevelingPointName);
                            }
                            MessageBox.Show("近似高程计算失败！");
                            return;
                        }
                    }
                }
            }
         
        }
        


    }
}
