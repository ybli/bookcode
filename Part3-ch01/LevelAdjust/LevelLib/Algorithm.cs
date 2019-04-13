using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelLib
{
    public class Algorithm
    {
        public static void PreProcess(ref DataCenter dataCenter)
        {
            for (int i = 0; i < dataCenter.Stations.Count; i++)
            {
                double[] a = new double[18];
                for (int j = 0; j < 8; j++)
                {
                    a[j] = dataCenter.Stations[i].list[j];
                }
                a[8] = a[3] - a[5];
                a[9] = a[1] - a[7];
                a[10] = a[9] - a[8];

                a[11] = a[0] - a[2];
                a[12] = a[6] - a[4];
                a[13] = (a[11] + a[12]) / 2;
                if (dataCenter.Stations[i].Point1.Name!="-1")
                {
                    a[14] = a[13];
                }
                else
                {
                    a[14] = a[13] + dataCenter.Stations[i - 1].list[14];
                }
                a[15] = a[1] - a[3];
                a[16] = a[7] - a[5];
                a[17] = (a[15] + a[16]) / 2;
                if(dataCenter.Stations[i].Point1.Name != "-1")
                {
                    dataCenter.Stations[i].deltaH = a[17];
                }
                else
                {
                    dataCenter.Stations[i].deltaH = dataCenter.Stations[i-1].deltaH + a[17];
                }
                for (int j = 8; j < 18; j++)
                {
                    dataCenter.Stations[i].list.Add(a[j]);
                }
            }
        }
        public static void ApproximateProcess(ref DataCenter dataCenter)
        {
            dataCenter.fh = 0;
            double totalL = 0;
            dataCenter.Stations[0].Point1.H = dataCenter.KnownPoint1.H;
            for (int i = 0; i < dataCenter.Stations.Count; i++)
            {
                dataCenter.fh += dataCenter.Stations[i].list[17];
                totalL += dataCenter.Stations[i].list[14];
            }
            double totalH = dataCenter.KnownPoint1.H;
            dataCenter.fh = dataCenter.fh - (dataCenter.KnownPoint2.H - dataCenter.KnownPoint1.H);
            //Console.WriteLine(dataCenter.fh);
            for(int i = 0; i < dataCenter.Stations.Count; i++)
            {
                dataCenter.Stations[i].v = -dataCenter.fh * dataCenter.Stations[i].list[14] / totalL;
                totalH += dataCenter.Stations[i].list[17]+dataCenter.Stations[i].v;
                dataCenter.Stations[i].Point2.H = dataCenter.Stations[i].v+ totalH;
                //Console.WriteLine(dataCenter.Stations[i].v);
                if(i< dataCenter.Stations.Count - 1)
                {
                    dataCenter.Stations[i+1].Point1.H = dataCenter.Stations[i].Point2.H;
                }
            }
        }

        public static void FinalProcess(ref DataCenter dataCenter)
        {
            for (int i = 0; i < dataCenter.Stations.Count; i++)
            {
                if (dataCenter.Stations[i].Point1.Name != "-1")
                {
                    Station station1 = new Station();
                    station1.Point1 = dataCenter.Stations[i].Point1;
                    dataCenter.NewStations.Add(station1);
                }
                if (dataCenter.Stations[i].Point2.Name != "-1")
                {
                    dataCenter.NewStations[dataCenter.NewStations.Count - 1].Point2 = dataCenter.Stations[i].Point2;
                    dataCenter.NewStations[dataCenter.NewStations.Count - 1].D = dataCenter.Stations[i].list[14];
                    dataCenter.NewStations[dataCenter.NewStations.Count - 1].deltaH = dataCenter.Stations[i].deltaH;
                }
            }
            Matrix B = new Matrix();
            B.N = dataCenter.NewStations.Count - 1;
            B.M = dataCenter.NewStations.Count;
            for(int i = 0; i < B.M; i++)
            {
                for(int j = 0; j < B.N; j++)
                {
                    if (i == j)
                    {
                        B.array.Add(1);
                    }
                    else if (j + 1 == i)
                    {
                        B.array.Add(-1);
                    }
                    else
                    {
                        B.array.Add(0);
                    }
                }
            }
            Matrix L = new Matrix();
            L.M = dataCenter.NewStations.Count;
            L.N = 1;
            for (int i=0;i< dataCenter.NewStations.Count; i++)
            {
                double l = -(dataCenter.NewStations[i].deltaH + dataCenter.NewStations[i].Point1.H - dataCenter.NewStations[i].Point2.H);
                L.array.Add(l);
            }
            dataCenter.L = L;
            Matrix P = new Matrix();
            P.M = dataCenter.NewStations.Count;
            P.N = dataCenter.NewStations.Count;
            for(int i = 0; i < P.M; i++)
            {
                for(int j = 0; j < P.N; j++)
                {
                    if (i == j)
                    {
                        P.array.Add(10 / dataCenter.NewStations[i].D);
                    }
                    else
                    {
                        P.array.Add(0);
                    }
                }
            }
            Matrix BT = Matrix.getT(B);
            Matrix BTPB = Matrix.multiply(Matrix.multiply(BT, P), B);
            Matrix invBTPB = Matrix.getInv(BTPB);
            dataCenter.invBTPB = invBTPB;
            Matrix BTPL = Matrix.multiply(Matrix.multiply(BT, P), L);
            Matrix x = Matrix.multiply(invBTPB, BTPL);
            dataCenter.x = x;
            for(int i = 0; i < dataCenter.NewStations.Count-1; i++)
            {
                dataCenter.NewStations[i].Point2.H = dataCenter.NewStations[i].Point2.H;
                dataCenter.NewStations[i].Point2.realH = dataCenter.NewStations[i].Point2.H + x.array[i];
                dataCenter.Points.Add(dataCenter.NewStations[i].Point2);
            }
        }
    }
}
