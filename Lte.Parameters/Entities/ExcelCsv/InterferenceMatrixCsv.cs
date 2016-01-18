using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Parameters.MockOperations;

namespace Lte.Parameters.Entities.ExcelCsv
{
    public class InterferenceMatrixCsv
    {
        [CsvColumn(Name = "ENODEBID_源PCI_邻PCI_邻频点")]
        public string CellRelation { get; set; }

        [CsvColumn(Name = "MOD3干扰数")]
        public int Mod3Interferences { get; set; }

        [CsvColumn(Name = "MOD6干扰数")]
        public int Mod6Interferences { get; set; }

        [CsvColumn(Name = "过覆盖数同频6db")]
        public int OverInterferences6Db { get; set; }

        [CsvColumn(Name = "过覆盖数同频10db")]
        public int OverInterferences10Db { get; set; }

        [CsvColumn(Name = "干扰值只有同频")]
        public double InterferenceLevel { get; set; }

        public static InterferenceMatrixCsvContainer ReadInterferenceMatrixCsvs(StreamReader reader, string path)
        {
            var time = path.GetDateTimeFromFileName();
            if (time != null)
            {
                return new InterferenceMatrixCsvContainer
                {
                    InterferenceMatrixCsvs =
                        CsvContext.Read<InterferenceMatrixCsv>(reader, CsvFileDescription.CommaDescription).ToList(),
                    RecordTime = (DateTime) time
                };
            }
            return null;
        } 
    }
}
