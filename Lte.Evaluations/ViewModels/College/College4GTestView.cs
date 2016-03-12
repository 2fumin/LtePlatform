using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;
using Lte.Parameters.Entities.College;

namespace Lte.Evaluations.ViewModels.College
{
    [AutoMapFrom(typeof(College4GTestResults))]
    [TypeDoc("记录校园网4G测试记录视图的类")]
    public class College4GTestView
    {
        [MemberDoc("测试时间")]
        public DateTime TestTime { get; set; }

        [MemberDoc("校园名称")]
        public string CollegeName { get; set; }

        [MemberDoc("小区名称")]
        public string CellName { get; set; }

        [MemberDoc("小区PCI")]
        public short Pci { get; set; }

        [MemberDoc("下载速率（kByte/s）")]
        public double DownloadRate { get; set; }

        [MemberDoc("上传速率（kByte/s）")]
        public double UploadRate { get; set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("接入用户数")]
        public int AccessUsers { get; set; }

        [MemberDoc("RSRP")]
        public double Rsrp { get; set; }

        [MemberDoc("SINR")]
        public double Sinr { get; set; }
    }
}
