using System;
using System.Collections.Generic;
using Lte.Parameters.Entities.ExcelCsv;

namespace Lte.Parameters.MockOperations
{
    public class InterferenceMatrixCsvContainer
    {
        public DateTime RecordTime { get; set; }

        public List<InterferenceMatrixCsv> InterferenceMatrixCsvs { get; set; } 
    }
}
