using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;

namespace Lte.Parameters.Entities
{
    public class FileRecord4G
    {
        [Column(Name = "ind", DbType = "Int")]
        public int? Ind { get; set; }

        [Column(Name = "rasterNum", DbType = "SmallInt")]
        public short RasterNum { get; set; }

        [Column(Name = "rasterNum", DbType = "Char(50)")]
        public string TestTimeString { get; set; }

        [Column(Name = "lon", DbType = "Float")]
        public double? Longtitute { get; set; }

        [Column(Name = "lat", DbType = "Float")]
        public double? Lattitute { get; set; }

        [Column(Name = "eNodeBID", DbType = "Int")]
        public int? ENodebId { get; set; }

        [Column(Name = "cellID", DbType = "TinyInt")]
        public byte? SectorId { get; set; }

        [Column(Name = "freq", DbType = "Real")]
        public double? Frequency { get; set; }

        [Column(Name = "PCI", DbType = "SmallInt")]
        public short? Pci { get; set; }

        [Column(Name = "RSRP", DbType = "Real")]
        public double? Rsrp { get; set; }

        [Column(Name = "SINR", DbType = "Real")]
        public double? Sinr { get; set; }

        [Column(Name = "DLBler", DbType = "TinyInt")]
        public byte? DlBler { get; set; }

        [Column(Name = "CQIave", DbType = "Real")]
        public double? CqiAverage { get; set; }

        [Column(Name = "ULMCS", DbType = "TinyInt")]
        public byte? UlMcs { get; set; }

        [Column(Name = "DLMCS", DbType = "TinyInt")]
        public byte? DlMcs { get; set; }

        [Column(Name = "PDCPThrUL", DbType = "Real")]
        public double? PdcpThroughputUl { get; set; }

        [Column(Name = "PDCPThrDL", DbType = "Real")]
        public double? PdcpThroughputDl { get; set; }

        [Column(Name = "PHYThrDL", DbType = "Real")]
        public double? PhyThroughputDl { get; set; }

        [Column(Name = "MACThrDL", DbType = "Real")]
        public double? MacThroughputDl { get; set; }

        [Column(Name = "PUSCHRbNum", DbType = "Int")]
        public int? PuschRbNum { get; set; }

        [Column(Name = "PDSCHRbNum", DbType = "Int")]
        public int? PdschRbNum { get; set; }

        [Column(Name = "PUSCHTBSizeAve", DbType = "Int")]
        public int? PuschRbSizeAverage { get; set; }

        [Column(Name = "PDSCHTBSizeAve", DbType = "Int")]
        public int? PdschRbSizeAverage { get; set; }

        [Column(Name = "n1PCI", DbType = "SmallInt")]
        public short? N1Pci { get; set; }

        [Column(Name = "n1RSRP", DbType = "Real")]
        public double? N1Rsrp { get; set; }

        [Column(Name = "n2PCI", DbType = "SmallInt")]
        public short? N2Pci { get; set; }

        [Column(Name = "n2RSRP", DbType = "Real")]
        public double? N2Rsrp { get; set; }

        [Column(Name = "n3PCI", DbType = "SmallInt")]
        public short? N3Pci { get; set; }

        [Column(Name = "n3RSRP", DbType = "Real")]
        public double? N3Rsrp { get; set; }
    }

    public class FileRecordCoverage4G
    {
        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public double Sinr { get; set; }

        public double Rsrp { get; set; }
    }
}
