using System.Collections.Generic;
using System.Linq;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Basic
{
    public class CellHuaweiMongoRepository : MongoDbRepositoryBase<CellHuaweiMongo, ObjectId>, ICellHuaweiMongoRepository
    {
        public CellHuaweiMongoRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "Cell";
        }

        public CellHuaweiMongoRepository() : this(new MyMongoProvider("fangww"))
        {
            
        }

        public List<CellHuaweiMongo> GetAllList(int eNodebId)
        {
            var query = MongoDB.Driver.Builders.Query<CellHuaweiMongo>.EQ(e => e.eNodeB_Id, eNodebId);
            return Collection.Find(query).AsQueryable().ToList();
        }

        public List<CellHuaweiMongo> GetRecentList(int eNodebId)
        {
            var query = MongoDB.Driver.Builders.Query<CellHuaweiMongo>.EQ(e => e.eNodeB_Id, eNodebId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }

        public CellHuaweiMongo GetRecent(int eNodebId, byte sectorId)
        {
            var query =
                MongoDB.Driver.Builders.Query<CellHuaweiMongo>.Where(
                    e => e.eNodeB_Id == eNodebId && e.CellId == sectorId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
