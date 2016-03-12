using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Parameters.Entities.College;

namespace Lte.Evaluations.ViewModels.College
{
    [AutoMapFrom(typeof(College3GTestResults))]
    public class College3GTestView
    {
        public DateTime TestTime { get; set; }

        public string CollegeName { get; set; }

        public double DownloadRate { get; set; }

        public int AccessUsers { get; set; }

        public double MinRssi { get; set; }

        public double MaxRssi { get; set; }

        public double Vswr { get; set; }
    }
}
