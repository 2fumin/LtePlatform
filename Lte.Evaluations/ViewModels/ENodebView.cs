using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
    public class ENodebView : IGeoPointReadonly<double>
    {
        public int ENodebId { get; set; }

        public string Name { get; set; }

        public string Factory { get; set; }
        
        public double Longtitute { get; set; }
        
        public double Lattitute { get; set; }

        public double BaiduLongtitute
        { get { return Longtitute + GeoMath.BaiduLongtituteOffset; } }

        public double BaiduLattitute
        { get { return Lattitute + GeoMath.BaiduLattituteOffset; } }

        public string Address { get; set; }

        public string PlanNum { get; set; }

        public DateTime OpenDate { get; set; }

        public int AlarmTimes { get; set; }

        public ENodebView() { }

        public ENodebView(ENodeb eNodeb, IQueryable<AlarmStat> stats)
        {
            eNodeb.CloneProperties(this);
            AlarmTimes = stats.Count(x => x.ENodebId == ENodebId);
        }
    }
}
