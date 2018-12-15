using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortPath
{
    /// <summary>
    /// 边：由开始顶点、结束顶点、边长组成
    /// </summary>
    class Edge
    {
        public string Start { get; set; }
        public string End { get; set; }
        public double Length { get; set; }

        public void Parse(string line)
        {
            var buf = line.Split(',');
            Start = buf[0];
            End = buf[1];
            Length = Convert.ToDouble(buf[2]);
        }

        public override string ToString()
        {
            string line = $"{Start},{End},{Length}";
            return line;
        }
    }
}
