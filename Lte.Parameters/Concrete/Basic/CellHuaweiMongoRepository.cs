using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract;
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
    }
}
