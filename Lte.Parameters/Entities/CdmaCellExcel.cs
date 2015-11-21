using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class CdmaCellExcel
    {
        [CloneProtection]
        [ExcelColumn("CELL_ID")]
        public byte SectorId { get; set; }

        [CloneProtection]
        [ExcelColumn("频点")]
        public int Frequency { get; set; }

        [ExcelColumn("是否接室分")]
        public string IsIndoor { get; set; } = "否";

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("天线挂高")]
        public double Height { get; set; }

        [ExcelColumn("机械下倾角")]
        public double MTilt { get; set; }

        [ExcelColumn("电下倾角")]
        public double ETilt { get; set; }

        [ExcelColumn("方位角")]
        public double Azimuth { get; set; }

        [ExcelColumn("天线增益")]
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
