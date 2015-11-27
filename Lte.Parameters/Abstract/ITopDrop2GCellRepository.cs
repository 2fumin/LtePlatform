using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ITopDrop2GCellRepository : IRepository<TopDrop2GCell>
    {
        int Import(IEnumerable<TopDrop2GCellExcel> stats);

        List<TopDrop2GCell> GetAllList(string city, DateTime begin, DateTime end);
    }
}
