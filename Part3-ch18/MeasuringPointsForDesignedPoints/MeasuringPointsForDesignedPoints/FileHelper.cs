using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MeasuringPointsForDesignedPoints
{
    public class FileHelper
    {
        public List<InfoData> ReadInfoData(string infoPath)
        {
            try
			{
                List<InfoData> ListInfoData = new List<InfoData>();
                StreamReader readinfofile = new StreamReader(infoPath, Encoding.Default);
				string text = "";
                while ((text = readinfofile.ReadLine()) != null)
				{
                    string[] einfoda = text.Split(',');

                    InfoData infodata = new InfoData();

                    infodata.Name = einfoda[0];
                    infodata.X = double.Parse(einfoda[1]);
                    infodata.Y = double.Parse(einfoda[2]);
                    infodata.Mil = double.Parse(einfoda[3]);
                    infodata.erfa = double.Parse(einfoda[4]);
                    infodata.K = double.Parse(einfoda[5]);
                    infodata.Rad = double.Parse(einfoda[6]);
                    infodata.l0 = double.Parse(einfoda[7]);

                    ListInfoData.Add(infodata);
				}
                readinfofile.Close();
                return ListInfoData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

        public List<CzxData> ReadCzxData(string czxPath)
        {
            try
            {
                List<CzxData> ListCzxData = new List<CzxData>();
                StreamReader readczxfile = new StreamReader(czxPath, Encoding.Default);
                string text = "";
                while ((text = readczxfile.ReadLine()) != null)
                {
                    string[] eczxda = text.Split(',');

                    CzxData czxdata = new CzxData();

                    czxdata.Name = eczxda[0]; // 点号
                    czxdata.X = double.Parse(eczxda[1]); // X(m)
                    czxdata.Y = double.Parse(eczxda[2]); // Y(m)

                    ListCzxData.Add(czxdata);
                }
                readczxfile.Close();
                return ListCzxData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LiPointsDxf(string filename, List<CzxData> ListLiPoints)
        {
            FileStream fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("SECTION");
                streamWriter.WriteLine("2");
                streamWriter.WriteLine("ENTITIES");
                // 画点
                for (int i = 0; i < ListLiPoints.Count; i++)
                {
                    streamWriter.WriteLine("0"); // 图元类型
                    streamWriter.WriteLine("POINT");
                    streamWriter.WriteLine("8"); // 图层名
                    streamWriter.WriteLine("PointLayer"); // 点图层
                    streamWriter.WriteLine("10");
                    streamWriter.WriteLine(ListLiPoints[i].Y);
                    streamWriter.WriteLine("20");
                    streamWriter.WriteLine(ListLiPoints[i].X);

                    streamWriter.WriteLine("0");
                    streamWriter.WriteLine("TEXT");
                    streamWriter.WriteLine("8");
                    streamWriter.WriteLine("TextLayer");
                    streamWriter.WriteLine("10"); // 直线或文字图元的起点
                    streamWriter.WriteLine(ListLiPoints[i].Y + 10);
                    streamWriter.WriteLine("20"); // 直线或文字图元的起点
                    streamWriter.WriteLine(ListLiPoints[i].X + 10);
                    streamWriter.WriteLine("50"); // 文字高度
                    streamWriter.WriteLine(0.1);
                    streamWriter.WriteLine("1");
                    streamWriter.WriteLine(ListLiPoints[i].Name);
                }

                streamWriter.WriteLine("0");
                streamWriter.WriteLine("ENDSEC");
                streamWriter.WriteLine("0");
                streamWriter.WriteLine("EOF");

                streamWriter.Flush();
                streamWriter.Close();
            }
            fileStream.Close();
        }

        public void SaveResultReport(string filename, string TextContext)
        {
            FileStream fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(TextContext);
            }
            fileStream.Close();
        }

        public StringBuilder OutReport(List<InfoData> ListInfoData, List<CalDevData> ListCalDevDa)
        {
            Mathematics_lb Math_lb = new Mathematics_lb();

            CharacteristicTrack_lb Chatra_lb = new CharacteristicTrack_lb();

            LateralDeviationSet_lb Latdev_lb = new LateralDeviationSet_lb();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("\r\n");

            stringBuilder.Append("***********************************************************计算报告***********************************************************\r\n");

            stringBuilder.Append("\r\n");

            stringBuilder.Append("********************************曲线基本信息*********************************\r\n");

            stringBuilder.Append("\r\n");

            Chatra_lb.l0 = ListInfoData[1].l0; // 缓和曲线
            Chatra_lb.Rad = ListInfoData[1].Rad; // 半径
            Chatra_lb.erfa = Chatra_lb.ERFAFun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.beta0 = Chatra_lb.BRTA0Fun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.m = Chatra_lb.MFun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.p = Chatra_lb.PFun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.T = Chatra_lb.TFun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.L = Chatra_lb.LFun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.E0 = Chatra_lb.E0Fun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.q = Chatra_lb.QFun(ListInfoData[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);

            // 曲线参数显示
            string[] obj1 = new string[33];
            obj1[0] = "交点名称：";
            obj1[1] = ListInfoData[1].Name;
            obj1[2] = "\n";
            obj1[3] = "线路偏向角α(°.′ ″)：";
            obj1[4] = ListInfoData[1].erfa.ToString("0.000");
            obj1[5] = "\n";
            obj1[6] = "圆曲线半径R：";
            obj1[7] = Chatra_lb.Rad.ToString("0.000");
            obj1[8] = "\n";
            obj1[9] = "缓和曲线长l0：";
            obj1[10] = Chatra_lb.l0.ToString("0.000");
            obj1[11] = "\n";
            obj1[12] = "缓和曲线切线角β0：";
            obj1[13] = Chatra_lb.beta0.ToString("0.000");
            obj1[14] = "\n";
            obj1[15] = "切垂距m：";
            obj1[16] = Chatra_lb.m.ToString("0.000");
            obj1[17] = "\n";
            obj1[18] = "圆曲线内移量p：";
            obj1[19] = Chatra_lb.p.ToString("0.000");
            obj1[20] = "\n";
            obj1[21] = "切线长T：";
            obj1[22] = Chatra_lb.T.ToString("0.000");
            obj1[23] = "\n";
            obj1[24] = "曲线长L：";
            obj1[25] = Chatra_lb.L.ToString("0.000");
            obj1[26] = "\n";
            obj1[27] = "外矢距E0：";
            obj1[28] = Chatra_lb.E0.ToString("0.000");
            obj1[29] = "\n";
            obj1[30] = "切曲差q：";
            obj1[31] = Chatra_lb.q.ToString("0.000");
            obj1[32] = "\n";
            stringBuilder.Append(string.Concat(obj1));

            stringBuilder.Append("\r\n");

            stringBuilder.Append("********************************************实测点设计位置及对应横向偏差计算结果*********************************************\r\n");
            stringBuilder.Append("\r\n");
            stringBuilder.Append("点名\t\tX(设计)\t\t\tY(设计)\t\t\t里程(m)\t\t横向偏差(mm)\t\t标签\r\n");

            for (int i = 0; i < ListCalDevDa.Count; i++)
            {
                StringBuilder stringBuilder2 = stringBuilder;
                string[] obj2 = new string[12];
                obj2[0] = ListCalDevDa[i].Name;
                obj2[1] = ",\t\t";
                obj2[2] = ListCalDevDa[i].DX.ToString("0.0000");
                obj2[3] = ",\t\t";
                obj2[4] = ListCalDevDa[i].DY.ToString("0.0000");
                obj2[5] = ",\t\t";
                obj2[6] = ListCalDevDa[i].PMil.ToString("0.0000");
                obj2[7] = ",\t";
                obj2[8] = ListCalDevDa[i].VDevVal.ToString("0.00");
                obj2[9] = ",\t\t\t";
                obj2[10] = ListCalDevDa[i].Poslabel;
                obj2[11] = "\n";
                stringBuilder2.Append(string.Concat(obj2));
            }

            stringBuilder.Append("\r\n");

            stringBuilder.Append("**************************************************************END**************************************************************\r\n");
            return stringBuilder;
        }

    }
}
