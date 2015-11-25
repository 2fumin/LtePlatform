using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ICdmaCellRepository : IRepository<CdmaCell>
    {
        CdmaCell GetBySectorId(int btsId, byte sectorId);
    }
}
