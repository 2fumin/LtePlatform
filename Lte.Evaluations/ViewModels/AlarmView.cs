using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class AlarmView
    {
        public int ENodebId { get; set; }

        public string Position { get; set; }

        public DateTime HappenTime { get; set; }

        public string HappenTimeString => HappenTime.ToShortDateString();

        public double Duration { get; set; }

        public string AlarmLevelDescription { get; set; }

        public string AlarmCategoryDescription { get; set; }

        public string AlarmTypeDescription { get; set; }

        public string Details { get; set; }
    }

    public class AlarmHistory
    {
        public string DateString { get; set; }

        public int Alarms { get; set; }
    }
}
