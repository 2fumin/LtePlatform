using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.ViewModels.Basic
{
    [AutoMapFrom(typeof(CdmaBts))]
    public class CdmaBtsView
    {
        public string Name { get; set; }

        public int TownId { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public double BaiduLongtitute => Longtitute + GeoMath.BaiduLongtituteOffset;

        public double BaiduLattitute => Lattitute + GeoMath.BaiduLattituteOffset;

        public string Address { get; set; }

        public int BtsId { get; set; }

        public int BscId { get; set; }

        public static CdmaBtsView ConstructView(CdmaBts bts)
        {
            return bts.MapTo<CdmaBtsView>();
        }
    }
}
