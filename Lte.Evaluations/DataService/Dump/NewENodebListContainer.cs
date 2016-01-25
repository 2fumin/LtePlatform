using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;
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
