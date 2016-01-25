using System;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities.College
{
    public class College3GTestResults : Entity
    {
        public int CollegeId { get; set; }

        public DateTime TestTime { get; set; }

        public double DownloadRate { get; set; }

        public int AccessUsers { get; set; }

        public double MinRssi { get; set; }

        public double MaxRssi { get; set; }

        public double Vswr { get; set; }
    }
}
