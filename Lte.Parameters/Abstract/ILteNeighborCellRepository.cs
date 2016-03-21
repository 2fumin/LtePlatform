using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Neighbor;

namespace Lte.Parameters.Abstract
{
    public interface ILteNeighborCellRepository : IRepository<LteNeighborCell>
    {
        List<LteNeighborCell> GetAllList(int cellId, byte sectorId);
    }

    public interface INearestPciCellRepository : IRepository<NearestPciCell>
    {
        List<NearestPciCell> GetAllList(int cellId, byte sectorId);

        NearestPciCell GetNearestPciCell(int cellId, byte sectorId, short pci);

        int SaveChanges();
    }
}
