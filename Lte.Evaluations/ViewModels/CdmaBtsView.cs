using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
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

        public static CdmaBtsView ConstructView(CdmaBts bts)
        {
            return Mapper.Map<CdmaBts, CdmaBtsView>(bts);
        }
    }
}
