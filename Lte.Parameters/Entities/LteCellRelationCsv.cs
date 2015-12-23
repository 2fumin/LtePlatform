using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv;

namespace Lte.Parameters.Entities
{
    public class LteCellRelationCsv
    {
        [CsvColumn(Name = "网元")]
        public int ENodebId { get; set; }

        [CsvColumn(Name = "小区")]
        public byte SectorId { get; set; }

        [CsvColumn(Name = "邻区关系")]
        public string NeighborRelation { get; set; }
    }
}
