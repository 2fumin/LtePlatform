using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ICdmaRegionStatRepository : IRepository<CdmaRegionStat>
    {
        int Import(IEnumerable<CdmaRegionStatExcel> stats);

        List<CdmaRegionStat> GetByDateSpan(DateTime begin, DateTime end);
    }
}
