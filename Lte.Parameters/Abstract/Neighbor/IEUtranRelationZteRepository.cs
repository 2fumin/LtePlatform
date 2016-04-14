using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Concrete.Neighbor;
using Lte.Parameters.Entities.Channel;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Neighbor
{
    public interface IEUtranRelationZteRepository : IRepository<EUtranRelationZte, ObjectId>
    {
        List<EUtranRelationZte> GetRecentList(int eNodebId, byte sectorId);

        List<EUtranRelationZte> GetRecentList(int eNodebId);

        EUtranRelationZte GetRecent(int eNodebId, int externalId);
    }

    public interface IExternalEUtranCellFDDZteRepository : IRepository<ExternalEUtranCellFDDZte, ObjectId>
    {
        List<ExternalEUtranCellFDDZte> GetRecentList(int eNodebId);

        List<ExternalEUtranCellFDDZte> GetReverseList(int destENodebId, byte destSectorId);
    }

    public interface IPowerControlDLZteRepository : IRepository<PowerControlDLZte, ObjectId>
    {
        List<PowerControlDLZte> GetRecentList(int eNodebId);

        PowerControlDLZte GetRecent(int eNodebId, byte sectorId);
    }
}
