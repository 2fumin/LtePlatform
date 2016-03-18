using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface ITownPreciseCoverage4GStatRepository : IRepository<TownPreciseCoverage4GStat>
    {
        List<TownPreciseCoverage4GStat> GetAllList(DateTime begin, DateTime end);

        TownPreciseCoverage4GStat GetByTown(int townId, DateTime statTime);
    }
}
