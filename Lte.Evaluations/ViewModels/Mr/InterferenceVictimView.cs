using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels.Mr
{
    [TypeDoc("被干扰小区视图")]
    public class InterferenceVictimView
    {
        [MemberDoc("被干扰小区基站编号")]
        public int VictimENodebId { get; set; }

        [MemberDoc("被干扰小区扇区编号")]
        public byte VictimSectorId { get; set; }

        [MemberDoc("被干扰小区名称")]
        public string VictimCellName { get; set; }

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