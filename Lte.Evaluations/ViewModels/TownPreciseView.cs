using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
    public class TownPreciseView
    {
        public string City { get; set; } = "-";

        public string District { get; set; } = "-";

        public string Town { get; set; } = "-";

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double) FirstNeighbors*100/TotalMrs;

        public double ThirdRate => 100 - (double) ThirdNeighbors*100/TotalMrs;

        public TownPreciseView() { }

        public TownPreciseView(TownPreciseCoverage4GStat stat, ITownRepository repository)
        {
            stat.CloneProperties(this);
            var town = repository.Get(stat.TownId);
            if (town == null) return;
            City = town.CityName;
            District = town.DistrictName;
            Town = town.TownName;
        }
    }
}
