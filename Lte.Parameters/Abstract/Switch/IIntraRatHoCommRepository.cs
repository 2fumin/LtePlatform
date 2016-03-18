using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface IIntraRatHoCommRepository : IRepository<IntraRatHoComm, ObjectId>
    {
        IntraRatHoComm GetRecent(int eNodebId);
    }
}
