using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Abstract
{
    public interface ICdmaRegionStatRepository : IRepository<CdmaRegionStat>
    {
        int Import(IEnumerable<CdmaRegionStatExcel> stats);

        List<CdmaRegionStat> GetAllList(DateTime begin, DateTime end);
    }
}
