using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Neighbor;
using Lte.Parameters.Entities.Neighbor;

namespace Lte.Parameters.Concrete.Neighbor
{
    public class EFLteNeighborCellRepository : EfRepositoryBase<EFParametersContext, LteNeighborCell>, ILteNeighborCellRepository
    {
        public List<LteNeighborCell> GetAllList(int cellId, byte sectorId)
        {
            return GetAllList(x => x.CellId == cellId && x.SectorId == sectorId);
        }

        public EFLteNeighborCellRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class EFNearestPciCellRepository : EfRepositoryBase<EFParametersContext, NearestPciCell>, INearestPciCellRepository
    {
        public List<NearestPciCell> GetAllList(int cellId, byte sectorId)
        {
            return GetAllList(x => x.CellId == cellId && x.SectorId == sectorId);
        }

        public NearestPciCell GetNearestPciCell(int cellId, byte sectorId, short pci)
        {
            return FirstOrDefault(x => x.CellId == cellId && x.SectorId == sectorId && x.Pci == pci);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public EFNearestPciCellRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
