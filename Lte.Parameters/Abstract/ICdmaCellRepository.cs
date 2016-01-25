using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Abstract
{
    public interface ICdmaCellRepository : IRepository<CdmaCell>
    {
        CdmaCell GetBySectorId(int btsId, byte sectorId);

        CdmaCell GetBySectorIdAndCellType(int btsId, byte sectorId, string cellType);

        List<CdmaCell> GetAllList(int btsId);

        List<CdmaCell> GetAllInUseList();
    }
}
