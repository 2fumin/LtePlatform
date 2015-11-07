using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    public class College4GTestResults : Entity
    {
        public int CollegeId { get; set; }

        public DateTime TestTime { get; set; }

        public double DownloadRate { get; set; }

        public double UploadRate { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int AccessUsers { get; set; }

        public double Rsrp { get; set; }

        public double Sinr { get; set; }
    }
}
