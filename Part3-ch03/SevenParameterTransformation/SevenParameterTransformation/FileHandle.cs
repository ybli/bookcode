using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;


namespace SevenParameterTransformation
{
    class FileHandle//文件处理的类
    {

        public static double[,] A;
        public static double[,] B;

        //#region 文件导入
        //public static void FileOpen(string path, out List<Point> knownPoints,
        //    out List<Point> unknownPoints)
        //{
        //    Point p1;
        //    knownPoints = new List<Point>();
        //    unknownPoints = new List<Point>();
        //    StreamReader sr = new StreamReader(path);
        //    string s = sr.ReadLine();
        //    //string s0= sr.ReadLine();//取出分割字符串
        //    for (int i = 0; i < i0; i++)
        //    {

        //        s = sr.ReadLine();
        //        string[] item = s.Split(',');
        //        p1 = new Point();
        //        p1.name = item[0];
        //        p1.X = double.Parse(item[1]);
        //        p1.Y = double.Parse(item[2]);
        //        p1.Z = double.Parse(item[3]);
        //        p1.X1 = double.Parse(item[4]);
        //        p1.Y1 = double.Parse(item[5]);
        //        p1.Z1 = double.Parse(item[6]);
        //        knownPoints.Add(p1);
        //    }
        //    s = sr.ReadLine();
        //    while (true)
        //    {
        //        s = sr.ReadLine();

        //        string[] item = s.Split(',');

        //        p1 = new Point();
        //        p1.name = item[0];
        //        p1.X = double.Parse(item[1]);
        //        p1.Y = double.Parse(item[2]);
        //        if (0 == p1.X)
        //            break;
        //        p1.Z = double.Parse(item[3]);
        //        unknownPoints.Add(p1);
        //    }
        //    A = new double[2, 2];
        //    B = new double[2, 3];
        //    s = sr.ReadLine();
        //    for (int i = 0; i < 2; i++)
        //    {
        //        s = sr.ReadLine();

        //        string[] item = s.Split(',');
        //        A[i, 1] = double.Parse(item[0]);
        //        A[i, 2] = double.Parse(item[1]);

        //    }


        //    s = sr.ReadLine();
        //    for (int i = 0; i < 2; i++)
        //    {
        //        s = sr.ReadLine();

        //        string[] item = s.Split(',');
        //        A[i, 1] = double.Parse(item[0]);
        //        A[i, 2] = double.Parse(item[1]);
        //        A[i, 3] = double.Parse(item[2]);

        //    }
        //    sr.Close();
        //}

        //#endregion


        #region 读取数据

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="knownPoints">已知点点集</param>
        /// <param name="unknownPoints">待转换点点集</param>
        public static void ReadData(string path, out List<Point> knownPoints, out List<Point> unknownPoints)
        {
            knownPoints = new List<Point>();
            unknownPoints = new List<Point>();
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding("gb2312"));
            string line;

            while ((line = sr.ReadLine()) != "")
            {
                knownPoints.Add(GetKnownPoint(line));
            }

            while ((line = sr.ReadLine()) != "")
            {
                unknownPoints.Add(GetUnknownPoint(line));
            }

            GetTestMartix(sr, out A, out B);

            sr.Close();
        }

        /// <summary>
        /// 读取已知点信息
        /// </summary>
        private static Point GetKnownPoint(string str)
        {
            Point point = new Point();
            string[] item = str.Split(',');

            point.name = item[0];
            point.X = double.Parse(item[1]);
            point.Y = double.Parse(item[2]);
            point.Z = double.Parse(item[3]);
            point.X1 = double.Parse(item[4]);
            point.Y1 = double.Parse(item[5]);
            point.Z1 = double.Parse(item[6]);

            return point;
        }


        /// <summary>
        /// 读取待转换点信息
        /// </summary>
        private static Point GetUnknownPoint(string str)
        {
            string[] item = str.Split(',');

            Point point = new Point();
            point.name = item[0];
            point.X = double.Parse(item[1]);
            point.Y = double.Parse(item[2]);
            point.Z = double.Parse(item[3]);

            return point;
        }


        /// <summary>
        /// 读取两个测试矩阵
        /// </summary>
        private static void GetTestMartix(StreamReader sr, out double[,] A, out double[,] B)
        {
            A = new double[2, 2];
            B = new double[2, 3];
            string s;

            for (int i = 0; i < 2; i++)
            {
                s = sr.ReadLine();
                string[] item = s.Split(',');
                A[i, 0] = double.Parse(item[0]);
                A[i, 1] = double.Parse(item[1]);
            }

            s = sr.ReadLine();//跳过中间的一个空行

            for (int i = 0; i < 2; i++)
            {
                s = sr.ReadLine();
                string[] item = s.Split(',');
                B[i, 0] = double.Parse(item[0]);
                B[i, 1] = double.Parse(item[1]);
                B[i, 2] = double.Parse(item[2]);
            }
        }

        public static DataTable ToDataTable(List<Point> knownPoints, List<Point> unknownPoints)
        {
            List<Point> points = new List<Point>();
            points.AddRange(knownPoints);
            points.AddRange(unknownPoints);

            DataTable table = InitTable();
            for (int i = 0; i < points.Count; i++)
            {
                DataRow row = table.NewRow();
                row["点名"] = points[i].name;
                row["X"] = points[i].X;
                row["Y"] = points[i].Y;
                row["Z"] = points[i].Z;

                if (points[i].X1 == 0)
                {
                    row["X1"] = null;
                    row["Y1"] = null;
                    row["Z1"] = null;
                }
                else
                {
                    row["X1"] = string.Format("{0:f4}", points[i].X1);
                    row["Y1"] = string.Format("{0:f4}", points[i].Y1);
                    row["Z1"] = string.Format("{0:f4}", points[i].Z1);
                }

                row["转换后坐标："] = null;
                table.Rows.Add(row);
            }
            return table;
        }

        private static DataTable InitTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("点名");
            table.Columns.Add("X");
            table.Columns.Add("Y");
            table.Columns.Add("Z");
            table.Columns.Add("转换后坐标：");
            table.Columns.Add("X1");
            table.Columns.Add("Y1");
            table.Columns.Add("Z1");
            return table;
        }
        #endregion


        #region 写报告
        /// <summary>
        /// 报告
        /// </summary>
        /// <param name="points">待转换点的点的集合</param>
        /// <param name="aInverseMatrix">A矩阵的的逆矩阵</param>
        /// <param name="B0">B矩阵</param>
        /// <param name="x">x矩阵</param>
        /// <param name="NBB">Nbb矩阵</param>
        /// <param name="AT">A的转置矩阵</param>
        /// <param name="AB">A矩阵与B矩阵的乘积</param>
        /// <param name="V">改正数矩阵</param>
        /// <returns>报告</returns>
        public static string WriteReport(List<Point> points, double[,] B0, double[,] V,
            double[,] x, double[,] NBB, double[,] AT, double[,] AB, double[,] aInverseMatrix)
        {
            string report = "******************坐标转换报告*******************\r\nB矩阵为：\r\n";

            for (int i = 0; i < B0.GetLength(0); i++)//写B矩阵
            {
                for (int j = 0; j < B0.GetLength(1); j++)
                {
                    report += string.Format("{0,20:f6}", B0[i, j]);
                }
                report += "\r\n";

            }

            report += "x矩阵为：\r\n";


            for (int i = 0; i < x.GetLength(0); i++)//写x矩阵
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    report += string.Format("{0,20:f10}", x[i, j]);
                }
                report += "\r\n";

            }

            report += "NBB矩阵为：\r\n";


            for (int i = 0; i < NBB.GetLength(0); i++)//写NBB矩阵
            {
                for (int j = 0; j < NBB.GetLength(1); j++)
                {
                    report += string.Format("{0,25:f6}", NBB[i, j]);
                }
                report += "\r\n";

            }

            report += "改正数为：\r\n";

            for (int i = 0; i < V.GetLength(0); i++)//写改正数矩阵
            {
                for (int j = 0; j < V.GetLength(1); j++)
                {
                    report += string.Format("{0,20:f6}", V[i, j]);
                }
                report += "\r\n";

            }

            report += "转换点在新坐标下的坐标值：\r\n";
            report += string.Format("{0,-5}{1,15:f4}{2,15:f4}{3,15:f4}{4,15:f4}{5,15:f4}{6,15:f4}\r\n",
                     "点名", "旧X坐标", "旧Y坐标", "旧Z坐标", "新X坐标", "新Y坐标", "新Z坐标");
            for (int i = 0; i < points.Count; i++)
            {
                report += string.Format("{0,-5}{1,20:f4}{2,20:f4}{3,20:f4}{4,20:f4}{5,20:f4}{6,20:f4}\r\n",
                    points[i].name, points[i].X, points[i].Y, points[i].Z, points[i].X1, points[i].Y1, points[i].Z1);
            }


            report += "A矩阵的逆矩阵：\r\n";

            for (int i = 0; i < aInverseMatrix.GetLength(0); i++)//写改正数矩阵
            {
                for (int j = 0; j < aInverseMatrix.GetLength(1); j++)
                {
                    report += string.Format("{0,15:f3}", aInverseMatrix[i, j]);
                }
                report += "\r\n";

            }

            report += "A矩阵与B矩阵的乘积：\r\n";

            for (int i = 0; i < AB.GetLength(0); i++)//写改正数矩阵
            {
                for (int j = 0; j < AB.GetLength(1); j++)
                {
                    report += string.Format("{0,15:f3}", AB[i, j]);
                }
                report += "\r\n";

            }

            report += "A矩阵的转置：\r\n";

            for (int i = 0; i < AT.GetLength(0); i++)//写改正数矩阵
            {
                for (int j = 0; j < AT.GetLength(1); j++)
                {
                    report += string.Format("{0,15:f3}", AT[i, j]);
                }
                report += "\r\n";

            }
            return report;
        }
        #endregion


        public static string ArrayToStr(double[,] array)
        {
            string str = null;
            for (int i = 0; i < array.GetLength(0); i++)//写B矩阵
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    str += string.Format("{0,25:f6},", array[i, j]);
                    //str += string.Format("{0:f6},", array[i, j]);
                }
                str += "\r\n";
            }

            return str;
        }

        #region 保存报告
        public static void SaveReport(string path, string report)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
            sw.Write(report);
            sw.Close();
        }
        #endregion


        #region 保存DXF
        public static void SaveDxf(string path, List<Point> knownPoints, List<Point> unknownPoints)
        {
            List<Point> points = knownPoints;
            points.AddRange(unknownPoints);

            int h = 1;//字体高度
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
            sw.WriteLine("0");
            sw.WriteLine("SECTION");
            sw.WriteLine("2");
            sw.WriteLine("ENTITIES");
            for (int i = 0; i < points.Count; i++)
            {
                sw.WriteLine("0");
                sw.WriteLine("POINT");
                sw.WriteLine("8");
                sw.WriteLine("点层");
                sw.WriteLine("10");
                sw.WriteLine(points[i].X1);
                sw.WriteLine("20");
                sw.WriteLine(points[i].Y1);

                sw.WriteLine("0");
                sw.WriteLine("TEXT");
                sw.WriteLine("8");
                sw.WriteLine("注记");
                sw.WriteLine("10");
                sw.WriteLine(points[i].X1);
                sw.WriteLine("20");
                sw.WriteLine(points[i].Y1);
                sw.WriteLine("40");
                sw.WriteLine(h);
                sw.WriteLine("1");
                sw.WriteLine(points[i].name);
            }
            sw.WriteLine("0");
            sw.WriteLine("ENDSEC");
            sw.WriteLine("0");
            sw.WriteLine("EOF");
            sw.Close();
        }
        #endregion
    }
}

