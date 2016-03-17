using System.Linq;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Switch
{
    public class CellMeasGroupZteRepository : MongoDbRepositoryBase<CellMeasGroupZte, ObjectId>, ICellMeasGroupZteRepository
    {
        public CellMeasGroupZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellMeasGroup";
        }

        public CellMeasGroupZteRepository() : this(new MyMongoProvider("fangww"))
        {
            
        }

        public CellMeasGroupZte GetRecent(int eNodebId)
        {
            var query = MongoDB.Driver.Builders.Query<CellMeasGroupZte>.EQ(e => e.eNodeB_Id, eNodebId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
