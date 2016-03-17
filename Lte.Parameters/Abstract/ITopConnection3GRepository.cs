using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Abstract
{
    public interface ITopConnection3GRepository : IRepository<TopConnection3GCell>
    {
        int Import(IEnumerable<TopConnection3GCellExcel> stats);

        List<TopConnection3GCell> GetAllList(string city, DateTime begin, DateTime end);
    }
}
