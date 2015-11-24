using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
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

        public static Precise4GView ConstructView(PreciseCoverage4G stat, IENodebRepository repository)
        {
            var view = Mapper.Map<PreciseCoverage4G, Precise4GView>(stat);
            var eNodeb = repository.GetByENodebId(stat.CellId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }
}
