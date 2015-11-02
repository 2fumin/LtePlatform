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
    public class EFCdmaRegionStatRepository : LightWeightRepositroyBase<CdmaRegionStat>, ICdmaRegionStatRepository
    {
        protected override DbSet<CdmaRegionStat> Entities => context.CdmaRegionStats;
    }
}
