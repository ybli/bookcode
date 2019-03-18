using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortPath
{
    /// <summary>
    /// 由边和顶点组成的图
    /// </summary>
    class Graph
    {
        public List<Edge> Edges;
        public List<Vertex> Vertexes;

        public Graph(List<Edge> edges)
        {
            Edges = edges;
            Parse(edges);
        }

        private void Parse(List<Edge> edges)
        {
            Vertexes = new List<Vertex>();
            foreach (var d in edges)
            {
                var v = new Vertex(d.Start);
                if (!Vertexes.Contains(v))
                {
                    Vertexes.Add(v);
                }
                v = new Vertex(d.End);
                if (!Vertexes.Contains(v))
                {
                    Vertexes.Add(v);
                }
            }
        }

        public override string ToString()
        {
            string line = "-------顶点------\n";
            foreach (var d in Vertexes)
            {
                line += d.ToString()+"\n";
            }
            line += "--------边-------\n";
            foreach (var d in Edges)
            {
                line += d.ToString()+"\n";
            }
            return line;
        }
    }
}
