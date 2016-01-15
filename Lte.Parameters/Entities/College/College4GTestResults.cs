using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Parameters.Entities
{
    [TypeDoc("校园网4G测试结果")]
    public class College4GTestResults : Entity
    {
        [MemberDoc("校园编号")]
        public int CollegeId { get; set; }

        [MemberDoc("测试时间")]
        public DateTime TestTime { get; set; }

        [MemberDoc("下载速率")]
        public double DownloadRate { get; set; }

        [MemberDoc("上传速率")]
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
