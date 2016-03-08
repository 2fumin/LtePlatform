using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels.Kpi
{
    public class TopConnection3GTrendView
    {
        [MemberDoc("小区名称")]
        public string CellName { get; set; }

        [MemberDoc("LTE基站名称")]
        public string ENodebName { get; set; }

        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("CDMA基站名称")]
        public string CdmaName { get; set; }

        [MemberDoc("LTE基站名称")]
        public string LteName { get; set; }

        public int BtsId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("无线掉线次数")]
        public int WirelessDrop { get; set; }

        [MemberDoc("连接尝试次数")]
        public int ConnectionAttempts { get; set; }

        [MemberDoc("连接失败次数")]
        public int ConnectionFails { get; set; }

        [MemberDoc("链路繁忙率")]
        public double LinkBusyRate { get; set; }

        [MemberDoc("连接成功率")]
        public double ConnectionRate => (double)(ConnectionAttempts - ConnectionFails) / ConnectionAttempts;

        [MemberDoc("掉线率")]
        public double DropRate => (double)WirelessDrop / (ConnectionAttempts - ConnectionFails);

        [MemberDoc("进入TOP的日期数")]
        public int TopDates { get; set; }
    }
}
