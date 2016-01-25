using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.MapperSerive
{
    public class ENodebExcelWithTownIdContainer
    {
        public ENodebExcel ENodebExcel { get; set; }

        public int TownId { get; set; }
    }

    public class ENodebWithTownIdContainer
    {
        public ENodeb ENodeb { get; set; }

        public int TownId { get; set; }
    }
}
