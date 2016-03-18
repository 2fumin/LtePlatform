using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface ICdmaRegionStatRepository : IRepository<CdmaRegionStat>
    {
        int Import(IEnumerable<CdmaRegionStatExcel> stats);

        List<CdmaRegionStat> GetAllList(DateTime begin, DateTime end);

        Task<List<CdmaRegionStat>> GetAllListAsync(DateTime begin, DateTime end);
    }
}
