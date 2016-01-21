using System.Collections.Generic;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.MapperSerive
{
    public class TopPrecise4GContainer
    {
        public PreciseCoverage4G PreciseCoverage4G { get; set; }

        public int TopDates { get; set; }
    }

    public class TopPreciseViewContainer
    {
        public IEnumerable<Precise4GView> Views { get; set; } 
    }
}
