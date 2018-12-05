using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiData
{
    class SessionList
    {
        public List<Session> Data=new List<Session>();

        public double TotalLength, DirctLength;
        public SessionList(List<Epoch> epoches)
        {
            for (int i = 0; i < epoches.Count-1; i++)
            {
                Session s=new Session(epoches[i],epoches[i+1]);
                s.Sn = i;
                Data.Add(s);
            }
            GetTotalLength();
            GetDirctLength(epoches);
        }

        private void GetDirctLength(List<Epoch> epoches)
        {
            int n = epoches.Count;
            Session s = new Session(epoches[0], epoches[n-1]);
            DirctLength = s.Length;
        }

        private void GetTotalLength()
        {
            TotalLength = 0;
            foreach (var d in Data)
            {
                TotalLength += d.Length;
            }
        }

        public override string ToString()
        {
           string line= "------------速度和方位角计算结果----------\r\n";
            foreach (var d in Data)
            {
                line += d.ToString()+"\r\n";
            }
            line += "------------距离计算结果-----------------\r\n";
            line += $"累积距离：{TotalLength:f3} (km)\r\n";
            line += $"首尾直线距离： {DirctLength:f3} (km)";

            return line;
        }
    }
}
