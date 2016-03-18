using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels.Switch
{
    public class ENodebIntraFreqHoView
    {
        public int ENodebId { get; set; }

        public int ReportInterval { get; set; }

        public int ReportAmount { get; set; }

        public int MaxReportCellNum { get; set; }

        public int TriggerQuantity { get; set; }

        public int ReportQuantity { get; set; }
    }
}
