using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Neighbor;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Channel
{
    public class PowerControlDLZteRepository : MongoDbRepositoryBase<PowerControlDLZte, ObjectId>, IPowerControlDLZteRepository
    {
        public PowerControlDLZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PowerControlDL";
        }

        public PowerControlDLZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<PowerControlDLZte> GetRecentList(int eNodebId)
        {
            var query =
                MongoDB.Driver.Builders.Query<PowerControlDLZte>.Where(e => e.eNodeB_Id == eNodebId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }

        public PowerControlDLZte GetRecent(int eNodebId, byte sectorId)
        {
            var query =
                MongoDB.Driver.Builders.Query<PowerControlDLZte>.Where(
                    e => e.eNodeB_Id == eNodebId && e.description == "cellLocalId=" + sectorId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
