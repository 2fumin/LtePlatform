using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Geo
{
    public class GeoPoint : IGeoPoint<double>, IGeoPointReadonly<double>
    {
        public GeoPoint(double x, double y)
        {
            Longtitute = x;
            Lattitute = y;
        }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public GeoPoint() { }

        public GeoPoint(IEnumerable<IGeoPoint<double>> pointList)
        {
            var geoPoints = pointList as IGeoPoint<double>[] ?? pointList.ToArray();
            Longtitute = geoPoints.Select(x => x.Longtitute).Average();
            Lattitute = geoPoints.Select(x => x.Lattitute).Average();
        }

        public GeoPoint(IGeoPoint<double> center, double longtituteOffset, double lattituteOffset)
        {
            Longtitute = center.Longtitute + longtituteOffset;
            Lattitute = center.Lattitute + lattituteOffset;
        }
    }
}
