namespace Lte.Evaluations.ViewModels
{
    public class TopDrop2GTrendView
    {
        public string CellName { get; set; }

        public string ENodebName { get; set; }

        public int TotalDrops { get; set; }

        public int TotalCallAttempst { get; set; }

        public double DropRate => TotalCallAttempst == 0 ? 0 : (double) TotalDrops/TotalCallAttempst;

        public int TopDates { get; set; }
    }
}
