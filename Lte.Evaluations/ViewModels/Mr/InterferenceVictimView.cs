using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels.Mr
{
    [TypeDoc("������С����ͼ")]
    public class InterferenceVictimView
    {
        [MemberDoc("������С����վ���")]
        public int VictimENodebId { get; set; }

        [MemberDoc("������С���������")]
        public byte VictimSectorId { get; set; }

        [MemberDoc("������С������")]
        public string VictimCellName { get; set; }

        [MemberDoc("ģ3������")]
        public double Mod3Interferences { get; set; }

        [MemberDoc("ģ6������")]
        public double Mod6Interferences { get; set; }

        [MemberDoc("6dB������")]
        public double OverInterferences6Db { get; set; }

        [MemberDoc("10dB������")]
        public double OverInterferences10Db { get; set; }

        [MemberDoc("�ܸ���ˮƽ")]
        public double InterferenceLevel { get; set; }
    }
}