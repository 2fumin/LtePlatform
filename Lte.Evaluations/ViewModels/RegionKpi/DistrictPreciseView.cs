using AutoMapper;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class DistrictPreciseView
    {
        public string City { get; set; } = "-";

        public string District { get; set; } = "-";

        public int TotalMrs { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double)FirstNeighbors * 100 / TotalMrs;

        public static DistrictPreciseView ConstructView(TownPreciseView townView)
        {
            return Mapper.Map<TownPreciseView, DistrictPreciseView>(townView);
        }
    }
}
