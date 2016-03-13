using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular.Attributes;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.ViewModels.Basic
{
    [AutoMapFrom(typeof(CdmaBts))]
    [TypeDoc("CDMA基站视图")]
    public class CdmaBtsView
    {
        [MemberDoc("基站名称")]
        public string Name { get; set; }

        [MemberDoc("所属镇区编号")]
        public int TownId { get; set; }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        [MemberDoc("区域")]
        public string DistrictName { get; set; }

        [MemberDoc("镇区")]
        public string TownName { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }
        
        [MemberDoc("地址")]
        public string Address { get; set; }

        [MemberDoc("基站编号")]
        public int BtsId { get; set; }

        [MemberDoc("BSC编号")]
        public int BscId { get; set; }
    }
}
