using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Parameters.Entities.ExcelCsv
{
    [AutoMapTo(typeof(CdmaBts))]
    [TypeDoc("定义记录CDMA基站信息的Excel导出数据项，需要定义与CdmaBts之间的映射关系。")]
    public class BtsExcel
    {
        [ExcelColumn("基站名称")]
        public string Name { get; set; }

        [ExcelColumn("行政区域")]
        public string DistrictName { get; set; }

        [ExcelColumn("所属镇区")]
        public string TownName { get; set; }

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("地址", TransformEnum.AntiNullAddress)]
        public string Address { get; set; }

        [ExcelColumn("基站编号")]
        public int BtsId { get; set; }

        [ExcelColumn("BSC编号")]
        public short BscId { get; set; }
    }
}
