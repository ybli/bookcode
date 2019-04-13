using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GauCoor_Trans
{
    public struct BL
    {
        public double B;
        public double L;
    }
    public struct XY
    {
        public double X;
        public double Y;//假定坐标
    }
    public struct Data
    {
        public XY XY;
        public double Yi;
        public string Name;
    }
    class Read_in
    {
        Caculate ca = new Caculate();
        List<BL> BL = new List<BL>();
        List<XY> XY = new List<XY>();
        /// <summary>
        /// 获取高斯坐标
        /// </summary>
        /// <param name="all_line"></param>
        /// <returns></returns>
        public List<Data> get_XY(string []all_line)
        {
            List<Data> Data = new List<Data>();
            for (int i = 0; i < all_line.Length; i++)
            {
                string[] temp = null;
                Data data = new Data();
                temp = all_line[i].Split(new char[] { ' ' });
                int m = 0;
                for (int j = 0; j < temp.Length; j++)
                {
                    if (m == 0 && temp[j] != "")
                    {
                        data.Name = temp[j].Trim();
                        m++;
                        continue;
                    }
                    if (m == 1 && temp[j] != "")
                    {
                        data.XY.X = double.Parse(temp[j].Trim());
                        m++;
                        continue;
                    }
                    if (m == 2 && temp[j] != "")
                    {
                        data.XY.Y = double.Parse(temp[j].Trim());
                        m++;
                        continue;
                    }
                    if (m == 3 && temp[j] != "")
                    {
                        data.Yi = double.Parse(temp[j].Trim());
                    }
                }
                if (m != 0)
                {
                    Data.Add(data);
                }
            }
            return Data;
        }
    }
}
