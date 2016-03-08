using Lte.Parameters.Abstract;

namespace Lte.Evaluations.ViewModels.Kpi
{
    public class TopDrop2GTrend : IBtsIdQuery
    {
        public int BtsId { get; set; }

        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int TotalDrops { get; set; }

        public int TotalCallAttempst { get; set; }

        public int TopDates { get; set; }

        public int MoAssignmentSuccess { get; set; }

        public int MtAssignmentSuccess { get; set; }

    }
}
