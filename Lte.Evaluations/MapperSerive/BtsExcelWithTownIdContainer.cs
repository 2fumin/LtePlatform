using Lte.Parameters.Entities;
using Lte.Parameters.Entities.ExcelCsv;

namespace Lte.Evaluations.MapperSerive
{
    public class BtsExcelWithTownIdContainer
    {
        public BtsExcel BtsExcel { get; set; }

        public int TownId { get; set; }
    }

    public class BtsWithTownIdContainer
    {
        public CdmaBts CdmaBts { get; set; }

        public int TownId { get; set; }
    }
}