using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;

namespace Lte.Parameters.Entities.ExcelCsv
{
    public class NeighborCellZteCsv
    {
        [CsvColumn(Name = "网元")]
        public int ENodebId { get; set; }

        [CsvColumn(Name = "小区")]
        public byte SectorId { get; set; }

        [CsvColumn(Name = "邻区关系")]
        public string NeighborRelation { get; set; }

        [CsvColumn(Name = "[FDD]系统内同频切换出请求次数（小区对）")]
        public int IntraSystemTimes { get; set; }

        [CsvColumn(Name = "[FDD]系统内异频切换出请求次数(小区对)")]
        public int InterSystemTimes { get; set; }

        public static List<NeighborCellZteCsv> ReadNeighborCellZteCsvs(StreamReader reader)
        {
            var infos = CsvContext.Read<NeighborCellZteCsv>(reader, CsvFileDescription.CommaDescription);
            var groupInfos = (from info in infos
                              group info by new { info.ENodebId, info.SectorId, info.NeighborRelation }
                into g
                              select new NeighborCellZteCsv
                              {
                                  ENodebId = g.Key.ENodebId,
                                  SectorId = g.Key.SectorId,
                                  NeighborRelation = g.Key.NeighborRelation,
                                  IntraSystemTimes = g.Sum(x => x.IntraSystemTimes),
                                  InterSystemTimes = g.Sum(x => x.InterSystemTimes)
                              }).ToList();
            return groupInfos;
        }

    }

    public class NeighborCellHwCsv
    {
        [CsvColumn(Name = "邻小区")]
        public string CellRelation { get; set; }

        [CsvColumn(Name = "特定两小区间切换出尝试次数 (无)")]
        public int TotalTimes { get; set; }

        public static List<NeighborCellHwCsv> ReadNeighborCellHwCsvs(StreamReader reader)
        {
            var infos = CsvContext.Read<NeighborCellHwCsv>(reader, CsvFileDescription.CommaDescription);
            var groupInfos = (from info in infos
                group info by info.CellRelation
                into g
                select new NeighborCellHwCsv
                {
                    CellRelation = g.Key,
                    TotalTimes = g.Sum(x => x.TotalTimes)
                }).ToList();
            return groupInfos;
        }

    }
}
