using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Concrete.Kpi
{
    public class EFPreciseCoverage4GRepository : LightWeightRepositroyBase<PreciseCoverage4G>,
        IPreciseCoverage4GRepository
    {
        protected override DbSet<PreciseCoverage4G> Entities => context.PrecisCoverage4Gs;

        public List<PreciseCoverage4G> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end);
        }

        public List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return GetAllList(x =>
                x.StatTime >= begin && x.StatTime < end && x.CellId == cellId && x.SectorId == sectorId);
        }
    }
}
