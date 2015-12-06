using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    /// <summary>
    /// 定义记录CDMA小区的信息的Excel导出数据项
    /// </summary>
    /// <remarks>需要定义与CdmaCell之间的映射关系</remarks>
    public class CdmaCellExcel
    {
        [CloneProtection]
        [ExcelColumn("扇区标识")]
        public byte SectorId { get; set; }

        [CloneProtection]
        [ExcelColumn("频点")]
        public int Frequency { get; set; }

        [ExcelColumn("覆盖类型(室内/室外/地铁)")]
        public string IsIndoor { get; set; } = "否";

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("挂高")]
        public double Height { get; set; }

        [ExcelColumn("下倾角（机械）")]
        public double MTilt { get; set; }

        [ExcelColumn("下倾角（电调）")]
        public double ETilt { get; set; }

        [ExcelColumn("方位角")]
        public double Azimuth { get; set; }

        [ExcelColumn("天线增益（dBi）", TransformEnum.DefaultZeroDouble)]
        public double AntennaGain { get; set; }

        [CloneProtection]
        [ExcelColumn("基站编号")]
        public int BtsId { get; set; }

        [CloneProtection]
        [ExcelColumn("小区标识")]
        public int CellId { get; set; }

        [CloneProtection]
        [ExcelColumn("载扇类型(1X/DO)")]
        public string CellType { get; set; }

        [CloneProtection]
        [ExcelColumn("LAC")]
        public string Lac { get; set; }

        [CloneProtection]
        [ExcelColumn("PN码")]
        public short Pn { get; set; }
    }
}
