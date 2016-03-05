using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels
{
    [TypeDoc("单日TOP掉话小区视图")]
    public class TopDrop2GDateView
    {
        [MemberDoc("统计日期")]
        public DateTime StatDate { get; set; }

        [MemberDoc("TOP掉话小区视图列表")]
        public IEnumerable<TopDrop2GCellView> StatViews { get; set; }
    }
}
