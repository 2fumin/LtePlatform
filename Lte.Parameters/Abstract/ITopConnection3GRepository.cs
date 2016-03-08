using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ITopConnection3GRepository : IRepository<TopConnection3GCell>
    {
        int Import(IEnumerable<TopConnection3GCellExcel> stats);

        List<TopConnection3GCell> GetAllList(string city, DateTime begin, DateTime end);
    }
}
