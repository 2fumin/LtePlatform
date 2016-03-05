using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.MapperSerive
{
    public static class TopCellQueriesService
    {
        public static IEnumerable<TopCellContainer<TTopCell>> QueryContainers<TTopCell>(this List<TTopCell> topCells,
            IBtsRepository btsRepository, IENodebRepository eNodebRepository)
            where TTopCell : IBtsIdQuery
        {
            return from stat in
                topCells
                join bts in btsRepository.GetAllList()
                    on stat.BtsId equals bts.BtsId into btsQuery
                from bq in btsQuery.DefaultIfEmpty()
                join eNodeb in eNodebRepository.GetAllList()
                    on bq?.ENodebId ?? -1 equals eNodeb.ENodebId into query
                from q in query.DefaultIfEmpty()
                select new TopCellContainer<TTopCell>
                {
                    TopCell = stat,
                    LteName = q == null ? "无匹配LTE基站" : q.Name,
                    CdmaName = bq == null ? "无匹配CDMA基站" : bq.Name
                };
        }

        public static IEnumerable<TopDrop2GTrend> QueryTrends(this IEnumerable<TopDrop2GCell> stats)
        {
            return from stat in stats
                group stat by new {stat.BtsId, stat.SectorId}
                into g
                select new TopDrop2GTrend
                {
                    BtsId = g.Key.BtsId,
                    SectorId = g.Key.SectorId,
                    TopDates = g.Count(),
                    TotalDrops = g.Sum(x => x.Drops),
                    TotalCallAttempst = g.Sum(x => x.TrafficAssignmentSuccess),
                    MoAssignmentSuccess = g.Sum(x => x.MoAssignmentSuccess),
                    MtAssignmentSuccess = g.Sum(x => x.MtAssignmentSuccess)
                };
        }
    }
}
