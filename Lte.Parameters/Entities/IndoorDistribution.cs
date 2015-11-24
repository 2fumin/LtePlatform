using Abp.Domain.Entities;
using AutoMapper;
using Lte.Domain.Common.Geo;

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

        public static IndoorDistribution ConstructItem(IndoorDistributionExcel info)
        {
            return Mapper.Map<IndoorDistributionExcel, IndoorDistribution>(info);
        }
    }
}
