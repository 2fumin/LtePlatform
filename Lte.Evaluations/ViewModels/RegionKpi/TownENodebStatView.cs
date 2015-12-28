namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class TownENodebStatView
    {
        public int TotalENodebs { get; set; }
        
        public string CityName { get; set; }
        
        public string DistrictName { get; set; }
        
        public string TownName { get; set; }

        public int TownId { get; set; }
    }

    public class TownBtsStatView
    {
        public int TotalBtss { get; set; }

        public string CityName { get; set; }

        public string DistrictName { get; set; }

        public string TownName { get; set; }

        public int TownId { get; set; }
    }
}
