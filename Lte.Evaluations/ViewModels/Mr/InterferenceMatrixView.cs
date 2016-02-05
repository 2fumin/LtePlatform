using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels.Mr
{
    [TypeDoc("干扰菊展视图")]
    public class InterferenceMatrixView
    {
        [MemberDoc("邻小区PCI")]
        public short DestPci { get; set; }

        [MemberDoc("邻小区基站编号")]
        public int DestENodebId { get; set; }

        [MemberDoc("邻小区扇区编号")]
        public byte DestSectorId { get; set; }

        [MemberDoc("邻小区名称")]
        public string NeighborCellName { get; set; }

        [MemberDoc("模3干扰数")]
        public double Mod3Interferences { get; set; }

        [MemberDoc("模6干扰数")]
        public double Mod6Interferences { get; set; }

        [MemberDoc("6dB干扰数")]
        public double OverInterferences6Db { get; set; }

        [MemberDoc("10dB干扰数")]
        public double OverInterferences10Db { get; set; }

        [MemberDoc("总干扰水平")]
        public double InterferenceLevel { get; set; }
    }
}
