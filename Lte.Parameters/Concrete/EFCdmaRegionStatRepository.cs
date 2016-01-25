using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Concrete
{
    public class EFCdmaRegionStatRepository : LightWeightRepositroyBase<CdmaRegionStat>, ICdmaRegionStatRepository
    {
        protected override DbSet<CdmaRegionStat> Entities => context.CdmaRegionStats;

        public int Import(IEnumerable<CdmaRegionStatExcel> stats)
        {
            int count = 0;
            foreach (var stat in from stat in stats
                let info = FirstOrDefault(x => x.Region == stat.Region && x.StatDate == stat.StatDate)
                where info == null
                select stat)
            {
                Insert(CdmaRegionStat.ConstructStat(stat));
                count++;
            }
            return count;
        }

        public List<CdmaRegionStat> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatDate >= begin && x.StatDate < end);
        }
    }
}
