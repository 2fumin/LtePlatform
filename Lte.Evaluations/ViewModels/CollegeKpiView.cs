using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
    public class CollegeKpiView
    {
        public DateTime TestTime { get; set; }

        public string CollegeName { get; set; }

        public int OnlineUsers { get; set; }

        public double DownloadFlow { get; set; }

        public double UploadFlow { get; set; }

        public double RrcConnection { get; set; }

        public double ErabConnection { get; set; }

        public double ErabDrop { get; set; }

        public double Connection2G { get; set; }

        public double Connection3G { get; set; }

        public double Erlang3G { get; set; }

        public double Drop3G { get; set; }

        public double Flow3G { get; set; }

        public static CollegeKpiView ConstructView(CollegeKpi stat)
        {
            return Mapper.Map<CollegeKpi, CollegeKpiView>(stat);
        }
    }
}
