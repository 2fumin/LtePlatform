using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFLteNeighborCellRepository : LightWeightRepositroyBase<LteNeighborCell>, ILteNeighborCellRepository
    {
        protected override DbSet<LteNeighborCell> Entities => context.LteNeighborCells;

        public List<LteNeighborCell> GetAllList(int cellId, byte sectorId)
        {
            return GetAllList(x => x.CellId == cellId && x.SectorId == sectorId);
        }
    }

    public class EFNearestPciCellRepository : LightWeightRepositroyBase<NearestPciCell>, INearestPciCellRepository
    {
        protected override DbSet<NearestPciCell> Entities => context.NearestPciCells;

        public List<NearestPciCell> GetAllList(int cellId, byte sectorId)
        {
            return GetAllList(x => x.CellId == cellId && x.SectorId == sectorId);
        }
    }
}
