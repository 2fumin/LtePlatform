using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFPreciseCoverage4GRepository : LightWeightRepositroyBase<PreciseCoverage4G>,
        IPreciseCoverage4GRepository
    {
        protected override DbSet<PreciseCoverage4G> Entities => context.PrecisCoverage4Gs;

        public IEnumerable<PreciseCoverage4G> GetTopCountStats(DateTime begin, DateTime end, int topCount,
            byte fieldSelector)
        {
            if (topCount <= 0)
                return new List<PreciseCoverage4G>();
            var query =
                GetAll()
                    .Where(x => x.StatTime >= begin && x.StatTime < end)
                    .OrderByDescending(x => x.TotalMrs)
                    .Take(topCount * (end - begin).Days);
            var result =
                from q in query.AsEnumerable()
                group q by new
                {
                    q.CellId,
                    q.SectorId
                }
                    into g
                select new PreciseCoverage4G
                {
                    CellId = g.Key.CellId,
                    SectorId = g.Key.SectorId,
                    FirstNeighbors = g.Sum(q => q.FirstNeighbors),
                    SecondNeighbors = g.Sum(q => q.SecondNeighbors),
                    ThirdNeighbors = g.Sum(q => q.ThirdNeighbors),
                    TotalMrs = g.Sum(q => q.TotalMrs)
                };
            switch (fieldSelector)
            {
                case 0:
                    return result.OrderBy(x => x.SecondRate)
                        .Take(topCount).ToList();
                case 1:
                    return result.OrderByDescending(x => x.SecondNeighbors)
                        .Take(topCount).ToList();
                case 2:
                    return result.OrderBy(x=>x.FirstRate)
                        .Take(topCount).ToList();
                case 3:
                    return result.OrderByDescending(x => x.FirstNeighbors)
                        .Take(topCount).ToList();
                default:
                    return result.OrderByDescending(x => x.TotalMrs)
                        .Take(topCount).ToList();
            }
            
        }

        public List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.CellId == cellId && x.SectorId == sectorId);
        }
    }
}
