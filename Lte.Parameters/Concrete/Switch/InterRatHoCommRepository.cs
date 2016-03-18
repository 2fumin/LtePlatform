using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Switch
{
    public class InterRatHoCommRepository : MongoDbRepositoryBase<InterRatHoComm, ObjectId>, IInterRatHoCommRepository
    {
        public InterRatHoCommRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "InterRatHoComm";
        }

        public InterRatHoCommRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public InterRatHoComm GetRecent(int eNodebId)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterRatHoComm>.Where(e => e.eNodeB_Id == eNodebId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
