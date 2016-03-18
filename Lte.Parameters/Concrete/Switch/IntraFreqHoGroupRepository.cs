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
    public class IntraFreqHoGroupRepository : MongoDbRepositoryBase<IntraFreqHoGroup, ObjectId>, IIntraFreqHoGroupRepository
    {
        public IntraFreqHoGroupRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "IntraFreqHoGroup";
        }

        public IntraFreqHoGroupRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public IntraFreqHoGroup GetRecent(int eNodebId, int localCellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<IntraFreqHoGroup>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
