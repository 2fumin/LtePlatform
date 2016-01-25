using System.Collections.Generic;
using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFRegionRepository : LightWeightRepositroyBase<OptimizeRegion>, IRegionRepository
    {
        protected override DbSet<OptimizeRegion> Entities => context.OptimizeRegions;

        public List<OptimizeRegion> GetAllList(string city)
        {
            return GetAllList(x => x.City == city);
        }
    }
}
