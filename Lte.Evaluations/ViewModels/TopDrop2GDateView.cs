using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class TopDrop2GDateView
    {
        public string StatDate { get; set; }

        public IEnumerable<TopDrop2GCellView> StatViews { get; set; }
    }
}
