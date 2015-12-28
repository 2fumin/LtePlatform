using System;
using AutoMapper;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class TownPreciseView
    {
        public DateTime StatTime { get; set; }

        public string City { get; set; } = "-";

        public string District { get; set; } = "-";

        public string Town { get; set; } = "-";

        public int TownId { get; set; }

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double) FirstNeighbors*100/TotalMrs;

        public double ThirdRate => 100 - (double) ThirdNeighbors*100/TotalMrs;

        public static TownPreciseView ConstructView(TownPreciseCoverage4GStat stat, ITownRepository repository)
        {
            var view = Mapper.Map<TownPreciseCoverage4GStat, TownPreciseView>(stat);
            var town = repository.Get(stat.TownId);
            view.City = town?.CityName;
            view.District = town?.DistrictName;
            view.Town = town?.TownName;
            return view;
        }
    }
}
