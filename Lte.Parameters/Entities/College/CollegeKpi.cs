using System;
using Abp.Domain.Entities.Auditing;

namespace Lte.Parameters.Entities.College
{
    public class CollegeKpi : AuditedEntity
    {
        public int CollegeId { get; set; }

        public DateTime TestTime { get; set; }

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
    }
}
