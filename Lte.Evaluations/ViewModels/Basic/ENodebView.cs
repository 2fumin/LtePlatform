﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.ViewModels.Basic
{
    [AutoMapFrom(typeof(ENodeb))]
    public class ENodebView : IGeoPointReadonly<double>
    {
        public int ENodebId { get; set; }

        public string Name { get; set; }

        public string Factory { get; set; }

        public IpAddress GatewayIp { get; set; }

        public IpAddress Ip { get; set; }

        public bool IsInUse { get; set; }

        public double Longtitute { get; set; }
        
        public double Lattitute { get; set; }
        
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
