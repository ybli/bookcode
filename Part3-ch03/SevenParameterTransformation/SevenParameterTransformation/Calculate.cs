using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenParameterTransformation
{
    /// <summary>
    /// 求解七参数 并进行坐标转换
    /// </summary>
    class Calculate
    {

        /// <summary>
        /// 求解七参数
        /// </summary>
        /// <param name="knownPoints">已知点构成的点集</param>
        /// <param name="B"></param>
        /// <param name="N"></param>
        /// <returns>七参数构成的数组</returns>
        public static double[,] ComputeSevenParameter(List<Point> knownPoints, out double[,] B, out double[,] N)
        {
            Martix tempB = new Martix(GetB(knownPoints));
            Martix l = new Martix(Getl(knownPoints));

            Martix tempN = tempB.Transpose() * tempB;
            Martix W = tempB.Transpose() * l;
            Martix x = tempN.Inverse(tempN.Element) * W;
            Martix V = tempB * x - l;

            string str = FileHandle.ArrayToStr(tempN.Element);

            B = tempB.Element;
            N = tempN.Element;

            return x.Element;
        }

        /// <summary>
        /// 坐标转换
        /// </summary>
        /// <param name="knownPoints">已知点构成的点集</param>
        /// <param name="unknownPoints">待转换点构成的点集</param>
        /// <param name="sevenParameter">七参数构成的数组</param>
        public static void Transform(List<Point> knownPoints, List<Point> unknownPoints, double[,] sevenParameter)
        {
            double m = sevenParameter[6, 0];
            double epsilonZ = sevenParameter[5, 0];
            double epsilonY = sevenParameter[4, 0];
            double epsilonX = sevenParameter[3, 0];
            double deltaZ = sevenParameter[2, 0];
            double deltaY = sevenParameter[1, 0];
            double deltaX = sevenParameter[0, 0];
            //计算已知点的改正数
            for (int i = 0; i < knownPoints.Count; i++)
            {
                double tempX = (1 + m) * knownPoints[i].X + knownPoints[i].Y * epsilonZ - knownPoints[i].Z * epsilonY + deltaX; //已知点X坐标的转换值
                double tempY = (1 + m) * knownPoints[i].Y - knownPoints[i].X * epsilonZ + knownPoints[i].Z * epsilonX + deltaY; //已知点Y坐标的转换值
                double tempZ = (1 + m) * knownPoints[i].Z + knownPoints[i].X * epsilonY - knownPoints[i].Y * epsilonX + deltaZ; //已知点Z坐标的转换值

                knownPoints[i].deltaX = knownPoints[i].X1 - tempX;
                knownPoints[i].deltaY = knownPoints[i].Y1 - tempY;
                knownPoints[i].deltaZ = knownPoints[i].Z1 - tempZ;
            }
            //计算待转换点的转换值
            for (int i = 0; i < unknownPoints.Count; i++)
            {
                unknownPoints[i].X1 = (1 + m) * unknownPoints[i].X + unknownPoints[i].Y * epsilonZ - unknownPoints[i].Z * epsilonY + deltaX;
                unknownPoints[i].Y1 = (1 + m) * unknownPoints[i].Y - unknownPoints[i].X * epsilonZ + unknownPoints[i].Z * epsilonX + deltaY;
                unknownPoints[i].Z1 = (1 + m) * unknownPoints[i].Z + unknownPoints[i].X * epsilonY - unknownPoints[i].Y * epsilonX + deltaZ;
            }
            //计算待转换点在各方向的改正数
            for (int i = 0; i < unknownPoints.Count; i++)
            {
                double sumPV = 0;
                double sumP = 0;
                for (int j = 0; j < knownPoints.Count; j++)
                {
                    double distance = Distance(unknownPoints[i], knownPoints[j]);
                    sumPV += knownPoints[j].deltaX / distance / distance;
                    sumP += 1 / distance / distance;
                }
                unknownPoints[i].deltaX = sumPV / sumP;
            }
            for (int i = 0; i < unknownPoints.Count; i++)
            {
                double sumPV = 0;
                double sumP = 0;
                for (int j = 0; j < knownPoints.Count; j++)
                {
                    double distance = Distance(unknownPoints[i], knownPoints[j]);
                    sumPV += knownPoints[j].deltaY / distance / distance;
                    sumP += 1 / distance / distance;
                }
                unknownPoints[i].deltaY = sumPV / sumP;
            }
            for (int i = 0; i < unknownPoints.Count; i++)
            {
                double sumPV = 0;
                double sumP = 0;
                for (int j = 0; j < knownPoints.Count; j++)
                {
                    double distance = Distance(unknownPoints[i], knownPoints[j]);
                    sumPV += knownPoints[j].deltaZ / distance / distance;
                    sumP += 1 / distance / distance;
                }
                unknownPoints[i].deltaZ = sumPV / sumP;
            }
            //计算待转换点的改正数
            for (int i = 0; i < unknownPoints.Count; i++)
            {
                unknownPoints[i].X1 += unknownPoints[i].deltaX;
                unknownPoints[i].Y1 += unknownPoints[i].deltaY;
                unknownPoints[i].Z1 += unknownPoints[i].deltaZ;
            }

        }


        /// <summary>
        /// 获取系数矩阵B
        /// </summary>
        /// <param name="points">已知点构成的点集</param>
        /// <returns>系数矩阵B</returns>
        private static double[,] GetB(List<Point> points)
        {
            double[,] B = new double[3 * points.Count, 7];

            int temp = 0;
            for (int i = 0; i < B.GetLength(0); i += 3)
            {
                //B[i, 0] = 1; B[i, 4] = -points[temp].Z; B[i, 5] = points[temp].Y; B[i, 6] = points[temp].X;
                //B[i + 1, 1] = 1; B[i + 1, 3] = points[temp].Z; B[i + 1, 5] = -points[temp].X; B[i + 1, 6] = points[temp].Y;
                //B[i + 2, 2] = 1; B[i + 2, 3] = -points[temp].Y; B[i + 2, 4] = points[temp].X; B[i + 2, 6] = points[temp].Z;

                B[i, 0] = -1; B[i, 4] = points[temp].Z; B[i, 5] = -points[temp].Y; B[i, 6] = -points[temp].X;
                B[i + 1, 1] = -1; B[i + 1, 3] = -points[temp].Z; B[i + 1, 5] = points[temp].X; B[i + 1, 6] = -points[temp].Y;
                B[i + 2, 2] = -1; B[i + 2, 3] = points[temp].Y; B[i + 2, 4] = -points[temp].X; B[i + 2, 6] = -points[temp].Z;
                temp += 1;
            }

            return B;
        }

        /// <summary>
        /// 获取l
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static double[,] Getl(List<Point> points)
        {
            double[,] l = new double[points.Count * 3, 1];

            for (int i = 0; i < points.Count; i++)
            {
                l[3 * i + 0, 0] = points[i].X - points[i].X1;
                l[3 * i + 1, 0] = points[i].Y - points[i].Y1;
                l[3 * i + 2, 0] = points[i].Z - points[i].Z1;
            }

            return l;
        }

        /// <summary>
        /// 获取改正数矩阵V
        /// </summary>
        /// <param name="knownPoints">已知点点集</param>
        /// <param name="unknownPoints">待转换点点集</param>
        /// <returns>改正数矩阵V</returns>
        public static double[,] GetV(List<Point> knownPoints)
        {
            double[,] V = new double[3*knownPoints.Count, 1];
            int j = 0;
            for (int i = 0; i < V.GetLength(0); i+=3)
            {
                V[i, 0] = knownPoints[j].deltaX;
                V[i + 1, 0] = knownPoints[j].deltaY;
                V[i + 2, 0] = knownPoints[j].deltaZ;
                j += 1;
            }

            return V;
        }

        /// <summary>
        /// 两点间的距离
        /// </summary>
        private static double Distance(Point p1, Point p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

    }
}
