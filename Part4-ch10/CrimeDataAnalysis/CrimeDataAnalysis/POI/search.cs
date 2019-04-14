using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;


namespace POI.Inquiry
{
    class Search
    {
        /// <summary>
        /// 某类犯罪数量的统计
        /// </summary>
        public struct CrimeCount
        {
            public string type;
            public int number;
        }
        public ReadFile.POI[] SearchQuiry(ReadFile.Crime[] crimedata,
            ReadFile.POI[] pointinterest, double threshold)
        {

            Parallel.For(0, pointinterest.Length, i =>
            //  for (int i = 0; i < pointinterest.Length; i++)
            {
                string[] array = new string[crimedata.Length];
                List<string> array1 = new List<string>();
                pointinterest[i].crimeinfor = new List<CrimeCount>();
                pointinterest[i].number = 0;

                Parallel.For(0, crimedata.Length, j =>
                {
                    double distance = calculatedistance(pointinterest[i].X, pointinterest[i].Y, crimedata[j].X, crimedata[j].Y);
                    if (distance < threshold)
                    {
                        if (contain(array1, crimedata[j].type) == 0)
                        {
                            array1.Add(crimedata[j].type);
                        }
                        array[j] = crimedata[j].type;
                        pointinterest[i].number++;
                    }
                });
                //Parallel.For(0, array1.Count, k =>
                for (int k = 0; k < array1.Count; k++)
                {
                    CrimeCount T = new CrimeCount();
                    T.type = array1[k];
                    T.number = calculate(array1[k], array);
                    pointinterest[i].crimeinfor.Add(T);
                }
                //);
                
            }
            );
            return pointinterest;
        }
        public ReadFile.POI[] Countpoicrime(ReadFile.POI[] pointinterest)
        {
            List<string> type_poi = new List<string>();
            for (int i = 0; i < pointinterest.Length; i++)
            {
                if (contain(type_poi, pointinterest[i].type) == 0)
                {
                    type_poi.Add(pointinterest[i].type);
                }
            }
            ReadFile.POI[] countpoicrime=new ReadFile.POI[type_poi.Count];
             int[] num_poi=new int[type_poi.Count];
            for (int j = 0; j < countpoicrime.Length; j++)
            {
                countpoicrime[j].type = type_poi[j];
                countpoicrime[j].crimeinfor = new List<CrimeCount>();
                num_poi[j] = 0;
            }
                for (int i = 0; i < pointinterest.Length; i++)
                {
                  /*  for(int )
                    for(int k=0;k<pointinterest[i].crimeinfor.Count;k++)
                    {
                        for(int j=0;)
                    if(((crimecount)pointinterest[i].crimeinfor[j]).type==countpoicrime[])
                    }*/
                    for (int j = 0; j < countpoicrime.Length; j++)
                    {
                        // countpoicrime[j].type=(string)type_poi[j];
                        if (pointinterest[i].type == countpoicrime[j].type)
                        {
                            num_poi[j] = num_poi[j] + 1;
                            countpoicrime[j].number = countpoicrime[j].number + pointinterest[i].number;
                            if (countpoicrime[j].crimeinfor.Count == 0)
                            {
                                // crimecount P = new crimecount();
                                // P.type = ((crimecount)pointinterest[i].crimeinfor[k]).type;
                                // P.number = ((crimecount)pointinterest[i].crimeinfor[k]).number;
                                countpoicrime[j].crimeinfor.Add(pointinterest[i].crimeinfor[0]);
                            }
                            for (int k = 1; k < pointinterest[i].crimeinfor.Count; k++)
                            {
                                int ss = 0;
                                for (int m = 0; m < countpoicrime[j].crimeinfor.Count; m++)
                                {
                                    if (pointinterest[i].crimeinfor[k].type == countpoicrime[j].crimeinfor[m].type)
                                    {
                                        ss = 1;
                                        CrimeCount T = new CrimeCount();
                                        T.type = countpoicrime[j].crimeinfor[m].type;
                                        T.number = countpoicrime[j].crimeinfor[m].number + pointinterest[i].crimeinfor[k].number;
                                        countpoicrime[j].crimeinfor[m] = T;
                                        break;
                                    }
                                }
                                if(ss==0)
                                countpoicrime[j].crimeinfor.Add(pointinterest[i].crimeinfor[k]);
                            }
                        }
                    }
                }
             for (int i = 0; i < type_poi.Count; i++)
                {
                    countpoicrime[i].number = countpoicrime[i].number/num_poi[i];
                    for (int j = 0; j < countpoicrime[i].crimeinfor.Count; j++)
                    {
                        CrimeCount N = new CrimeCount();
                        N.type = countpoicrime[i].crimeinfor[j].type;
                        N.number= countpoicrime[i].crimeinfor[j].number / num_poi[i];
                        countpoicrime[i].crimeinfor[j] = N;
                    }
                }
             
                return countpoicrime;
            
        }
        public double calculatedistance(double X1, double Y1, double X2, double Y2)
        {
            double distance = Math.Sqrt(Math.Pow((X2 - X1), 2) + Math.Pow((Y2 - Y1), 2));
            return distance;    
        }
        public int contain(List<string> data, string type)//检测类型是否已包含在数组中
        {
            // int s=0;
            // Parallel.For(0, data.Count, i =>
            //// for (int i = 0; i < data.Count; i++)
            // {
            //     if (data[i] == type)
            //     {
            //         s = -1;
            //     }
            // }
            // );
            // return s;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == type)
                {
                    return -1;
                }
            }
            return 0;

            
        }
        public int calculate(object s1, string[] arr)//统计每种犯罪类型的数量
        {
            int num=0;
           // Parallel.For(0, arr.Count, i =>
             for (int i = 0; i < arr.Length; i++)
            {
                if ((string)s1 == arr[i])
                    num++;
            }
            //);
            return num;
        }
    }
}
