using System;
using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class Precise4GView
    {
        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int TotalMrs { get; set; }

        public int SecondNeighbors { get; set; }

        public double FirstRate { get; set; }

        public double SecondRate { get; set; }

        public double ThirdRate { get; set; }

        public string ENodebName { get; set; } = "未导入基站";

        public int TopDates { get; set; }
        
        public static Precise4GView ConstructView(PreciseCoverage4G stat, IENodebRepository repository)
        {
            var view = Mapper.Map<PreciseCoverage4G, Precise4GView>(stat);
            var eNodeb = repository.GetByENodebId(stat.CellId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }

    public class PreciseImportView
    {
        public Precise4GView View { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }
    }
}
