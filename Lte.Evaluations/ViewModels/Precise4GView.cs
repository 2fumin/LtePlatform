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
    public class Precise4GView
    {
        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int TotalMrs { get; set; }

        public int SecondNeighbors { get; set; }

        public double FirstRate { get; set; }

        public double SecondRate { get; set; }

        public double ThirdRate { get; set; }

        public string ENodebName { get; set; }

        public Precise4GView()
        {
        }

        public Precise4GView(PreciseCoverage4G stat, IENodebRepository repository)
        {
            stat.CloneProperties(this);
            ENodeb eNodeb = repository.FirstOrDefault(x => x.ENodebId == stat.CellId);
            ENodebName = eNodeb != null ? eNodeb.Name : "未导入基站";
        }
    }
}
