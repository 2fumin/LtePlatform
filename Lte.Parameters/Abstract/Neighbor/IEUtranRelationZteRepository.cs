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
    }

    public interface IExternalEUtranCellFDDZteRepository : IRepository<ExternalEUtranCellFDDZte, ObjectId>
    {
        List<ExternalEUtranCellFDDZte> GetRecentList(int eNodebId);
    }

    public interface IPowerControlDLZteRepository : IRepository<PowerControlDLZte, ObjectId>
    {
        List<PowerControlDLZte> GetRecentList(int eNodebId);

        List<PowerControlDLZte> GetRecentList(int eNodebId, byte sectorId);
    }
}
