using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ILteNeighborCellRepository : IRepository<LteNeighborCell>
    {
        List<LteNeighborCell> GetAllList(int cellId, byte sectorId);
    }

    public interface INearestPciCellRepository : IRepository<NearestPciCell>
    {
        List<NearestPciCell> GetAllList(int cellId, byte sectorId);
    }
}
