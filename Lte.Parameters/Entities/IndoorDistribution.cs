using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class IndoorDistribution : Entity, IGeoPoint<double>
    { 
        public string Name { get; set; }
        
        public string Range { get; set; }
        
        public string SourceName { get; set; }
        
        public string SourceType { get; set; }
        
        public double Longtitute { get; set; }
        
        public double Lattitute { get; set; }

        public double BaiduLongtitute => Longtitute + GeoMath.BaiduLongtituteOffset;

        public double BaiduLattitute => Lattitute + GeoMath.BaiduLattituteOffset;

        public IndoorDistribution() { }

        public IndoorDistribution(IndoorDistributionExcel info)
        {
            info.CloneProperties(this);
        }
    }
}
