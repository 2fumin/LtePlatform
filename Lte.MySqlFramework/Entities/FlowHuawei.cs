using System;
using Abp.Domain.Entities;

namespace Lte.MySqlFramework.Entities
{
    public class FlowHuawei : Entity
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte LocalCellId { get; set; }

        public double PdcpDownlinkFlow { get; set; }

        public double PdcpUplinkFlow { get; set; }

        public double AverageUsers { get; set; }

        public int MaxUsers { get; set; }

        public double AverageActiveUsers { get; set; }

        public int MaxActiveUsers { get; set; }

        public double UplinkAverageUsers { get; set; }

        public int UplinkMaxUsers { get; set; }

        public double DownlinkAverageUsers { get; set; }

        public int DownlinkMaxUsers { get; set; }

        public double DownlinkDuration { get; set; }

        public double UplinkDuration { get; set; }

        public int PagingUsers { get; set; }

        public double DownlinkAveragePrbs { get; set; }

        public double DownlinkDrbPbs { get; set; }

        public double UplinkAveragePrbs { get; set; }

        public double UplinkDrbPbs { get; set; }

        public int GroupAPreambles { get; set; }

        public int GroupBPreambles { get; set; }

        public int DedicatedPreambles { get; set; }

        public double UplinkDciCceRate { get; set; }

        public double DownlinkDciCceRate { get; set; }

        public double PucchPrbs { get; set; }

        public double LastTtiUplinkFlow { get; set; }

        public double ButLastUplinkDuration { get; set; }

        public double LastTtiDownlinkFlow { get; set; }

        public double ButLastDownlinkDuration { get; set; }
    }
}
