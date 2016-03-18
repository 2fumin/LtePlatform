using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface ITopDrop2GCellRepository : IRepository<TopDrop2GCell>
    {
        int Import(IEnumerable<TopDrop2GCellExcel> stats);

        List<TopDrop2GCell> GetAllList(string city, DateTime begin, DateTime end);
    }
}
