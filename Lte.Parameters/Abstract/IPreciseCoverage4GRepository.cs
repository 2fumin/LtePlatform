using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface IPreciseCoverage4GRepository : IRepository<PreciseCoverage4G>
    {
        IEnumerable<PreciseCoverage4G> GetTopCountStats(DateTime begin, DateTime end, int topCount);

        List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end);
    }
}
