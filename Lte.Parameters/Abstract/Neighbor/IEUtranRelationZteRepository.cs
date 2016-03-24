using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Neighbor
{
    public interface IEUtranRelationZteRepository : IRepository<EUtranRelationZte, ObjectId>
    {
        IEnumerable<EUtranRelationZte> GetRecent(int eNodebId, byte sectorId);
    }
}
