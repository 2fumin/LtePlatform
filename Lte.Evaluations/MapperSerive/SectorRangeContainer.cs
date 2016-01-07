using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Evaluations.MapperSerive
{
    [TypeDoc("指定扇区查询范围条件")]
    public class SectorRangeContainer
    {
        [MemberDoc("西边经度")]
        public double West { get; set; }

        [MemberDoc("东边经度")]
        public double East { get; set; }

        [MemberDoc("南边纬度")]
        public double South { get; set; }

        [MemberDoc(("北边纬度"))]
        public double North { get; set; }

        [MemberDoc("需要排除的小区列表")]
        public IEnumerable<CellIdPair> ExcludedCells { get; set; } 
    }

    [TypeDoc("小区编号和扇区编号定义 ")]
    public class CellIdPair
    {
        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }
    }

    [TypeDoc("基站编号容器")]
    public class ENodebIdsContainer
    {
        [MemberDoc("基站编号列表")]
        public IEnumerable<int> ENodebIds { get; set; } 
    }

    [TypeDoc("小区编号容器")]
    public class CellIdsContainer
    {
        [MemberDoc("小区编号列表")]
        public IEnumerable<CellIdPair> CellIdPairs { get; set; } 
    }
}
