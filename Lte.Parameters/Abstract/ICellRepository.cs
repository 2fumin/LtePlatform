using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ICellRepository : IRepository<Cell>
    {
        void AddCells(IEnumerable<Cell> cells);

        Cell GetBySectorId(int eNodebId, byte sectorId);
    }
}
