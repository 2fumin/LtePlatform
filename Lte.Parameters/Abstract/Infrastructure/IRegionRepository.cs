using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface IRegionRepository : IRepository<OptimizeRegion>
    {
        List<OptimizeRegion> GetAllList(string city);

        Task<List<OptimizeRegion>> GetAllListAsync(string city);
    }
}
