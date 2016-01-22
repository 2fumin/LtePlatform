using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels.Basic
{
    [AutoMapFrom(typeof(ENodeb))]
    public class ENodebView : IGeoPointReadonly<double>
    {
        public int ENodebId { get; set; }

        public string Name { get; set; }

        public string Factory { get; set; }
        
        public double Longtitute { get; set; }
        
        public double Lattitute { get; set; }

        public double BaiduLongtitute => Longtitute + GeoMath.BaiduLongtituteOffset;

        public double BaiduLattitute => Lattitute + GeoMath.BaiduLattituteOffset;

        public string Address { get; set; }

        public string PlanNum { get; set; }

        public DateTime OpenDate { get; set; }

        public string OpenDateString => OpenDate.ToShortDateString();

        public int AlarmTimes { get; set; }

        public static ENodebView ConstructView(ENodeb eNodeb, IEnumerable<AlarmStat> stats)
        {
            var view = Mapper.Map<ENodeb, ENodebView>(eNodeb);
            view.AlarmTimes = stats.Count(x => x.ENodebId == eNodeb.ENodebId);
            return view;
        }
    }
}
