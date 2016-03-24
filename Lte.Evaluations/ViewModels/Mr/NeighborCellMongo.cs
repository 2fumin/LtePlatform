using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels.Mr
{
    [TypeDoc("来自MongoDB的LTE邻区关系视图")]
    public class NeighborCellMongo
    {
        [MemberDoc("小区编号（对于LTE来说就是基站编号）")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("邻区小区编号")]
        public int NeighborCellId { get; set; }

        [MemberDoc("邻区扇区编号")]
        public byte NeighborSectorId { get; set; }

        [MemberDoc("PCI，便于查询邻区")]
        public short NeighborPci { get; set; }

        [MemberDoc("是否为ANR创建")]
        public bool IsAnrCreated { get; set; }

        [MemberDoc("是否允许切换")]
        public bool HandoffAllowed { get; set; }

        [MemberDoc("是否可以被ANR删除")]
        public bool RemovedAllowed { get; set; }

        [MemberDoc("小区测量优先级是否为高")]
        public int CellPriority { get; set; }
    }
}
