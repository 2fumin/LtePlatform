using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
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

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public EFNearestPciCellRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
