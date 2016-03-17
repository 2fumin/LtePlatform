using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete.Kpi
{
    public class EFRegionRepository : LightWeightRepositroyBase<OptimizeRegion>, IRegionRepository
    {
        protected override DbSet<OptimizeRegion> Entities => context.OptimizeRegions;

        public List<OptimizeRegion> GetAllList(string city)
        {
            return GetAllList(x => x.City == city);
        }

        public async Task<List<OptimizeRegion>> GetAllListAsync(string city)
        {
            return await GetAllListAsync(x => x.City == city);
        }
    }
}
