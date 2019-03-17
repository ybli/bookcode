using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortPath
{
    /// <summary>
    /// 顶点
    /// </summary>
    class Vertex
    {
        public string Name { get; set; }
        public double Weight { get; set; }

        internal double Inf = 1000000000; //一个很大的值
        public Vertex()
        {
            Name = "";
            Weight = Inf;
        }

        public Vertex(string name)
        {
            Name = name;
            Weight = Inf;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vertex))
                return false;

            Vertex v = obj as Vertex;
            return v != null && this.Name == v.Name;
        }

        public override string ToString()
        {
            double TOLERANCE = 0.1;
            string line = $"{Name}";
            if (Math.Abs(Weight - Inf) > TOLERANCE)
            {
                line += $", {Weight}";
            }
            return line;
        }
    }
}
