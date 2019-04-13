using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelLib
{
    public class DataCenter
    {
        public PointInfo KnownPoint1;
        public PointInfo KnownPoint2;
        public List<Station> Stations;
        public List<Station> NewStations;
        public List<PointInfo> Points;
        public Matrix L;
        public Matrix invBTPB;
        public Matrix x;
        public Matrix A;
        public Matrix B;
        public double fh;
    }
}
