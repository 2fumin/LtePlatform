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
    public class InterFreqHoGroupRepository : MongoDbRepositoryBase<InterFreqHoGroup, ObjectId>, IInterFreqHoGroupRepository
    {
        public InterFreqHoGroupRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "InterFreqHoGroup";
        }

        public InterFreqHoGroupRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public InterFreqHoGroup GetRecent(int eNodebId, int localCellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterFreqHoGroup>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
