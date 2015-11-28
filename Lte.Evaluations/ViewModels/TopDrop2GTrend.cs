using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.ViewModels
{
    public class TopDrop2GTrend : IBtsIdQuery
    {
        public int BtsId { get; set; }

        public byte SectorId { get; set; }

        public int TotalDrops { get; set; }

        public int TotalCallAttempst { get; set; }

        public int TopDates { get; set; }
    }
}
