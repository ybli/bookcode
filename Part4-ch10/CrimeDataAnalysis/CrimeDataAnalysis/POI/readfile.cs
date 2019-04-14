using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
namespace POI.Inquiry
{
    class ReadFile
    {
        /// <summary>
        /// 一条犯罪数据
        /// </summary>
       public struct Crime 
        {
            public string type;
            public double X;
            public double Y;
        }
        /// <summary>
        /// 兴趣点信息
        /// </summary>
       public struct POI
        {
            public string type;
            public double X;
            public double Y;
            public int number;//该类兴趣点上犯罪的总数
            public List<Search.CrimeCount> crimeinfor;
        }
      
       
        public Crime[] ReadCrimedata(string filepath)//读取犯罪数据
        {
            StreamReader file0 = new StreamReader(filepath, System.Text.Encoding.Default);
            int number = 0;
            while ((file0.ReadLine()) != null)
            {
                number++;
            }
            file0.Close();
            Crime[] data = new Crime[number];
            StreamReader file = new StreamReader(filepath, System.Text.Encoding.Default);
            for (int i = 0; i < number; i++)
            {
                string line = file.ReadLine();
                var arr = line.Split(',');
                data[i].type = arr[0];
                data[i].X = double.Parse(arr[1])/1000;
                data[i].Y = double.Parse(arr[2])/1000;
            }
            file.Close();
            return data;
        }
        public POI[] ReadPOI(string filepath)//读取兴趣点数据
        {
            StreamReader file0 = new StreamReader(filepath, System.Text.Encoding.Default);
            int number = 0;
            while ((file0.ReadLine()) != null)
            {
                number++;
            }
            file0.Close();
            POI[] data = new POI[number];
            StreamReader file = new StreamReader(filepath, System.Text.Encoding.Default);
            for (int i = 0; i < number; i++)
            {
                string line = file.ReadLine();
                var arr = line.Split(',');
                data[i].type = arr[0];
                data[i].X = double.Parse(arr[1])/1000;
                data[i].Y = double.Parse(arr[2])/1000;
            }
            file.Close();
            return data;
        }
       
    }
}
