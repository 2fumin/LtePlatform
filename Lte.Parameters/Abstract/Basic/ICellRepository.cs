using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Abstract.Basic
{
    public interface ICellRepository : IRepository<Cell>
    {
        void AddCells(IEnumerable<Cell> cells);

        Cell GetBySectorId(int eNodebId, byte sectorId);

        Cell GetByFrequency(int eNodebId, int frequency);

        List<Cell> GetAllList(int eNodebId);

        List<Cell> GetAllList(double west, double east, double south, double north);

        List<Cell> GetAllInUseList();

        int SaveChanges();
    }
}
