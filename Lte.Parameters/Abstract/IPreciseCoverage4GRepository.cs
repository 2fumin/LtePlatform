using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Abstract
{
    public interface IPreciseCoverage4GRepository : IRepository<PreciseCoverage4G>
    {
        List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end);

        List<PreciseCoverage4G> GetAllList(DateTime begin, DateTime end);
    }
}
