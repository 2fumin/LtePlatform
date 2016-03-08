using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels
{
    [TypeDoc("单日TOP连接成功率小区视图")]
    public class TopConnection3GDateView
    {
        [MemberDoc("统计日期")]
        public DateTime StatDate { get; set; }

        [MemberDoc("TOP连接成功率小区视图列表")]
        public IEnumerable<TopConnection3GCellView> StatViews { get; set; }
    }
}
