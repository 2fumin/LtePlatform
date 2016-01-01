using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    [TypeDoc("新基站信息容器")]
    public class NewENodebListContainer
    {
        [MemberDoc("基站Excel信息列表")]
        public IEnumerable<ENodebExcel> Infos { get; set; }
    }
}
