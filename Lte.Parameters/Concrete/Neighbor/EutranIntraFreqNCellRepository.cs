using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Neighbor;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Neighbor
{
    public class EutranIntraFreqNCellRepository : MongoDbRepositoryBase<EutranIntraFreqNCell, ObjectId>, IEutranIntraFreqNCellRepository
    {
        public EutranIntraFreqNCellRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EutranIntraFreqNCell";
        }

        public EutranIntraFreqNCellRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<EutranIntraFreqNCell> GetRecentList(int eNodebId, byte localCellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<EutranIntraFreqNCell>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }

        public List<EutranIntraFreqNCell> GetReverseList(int destENodebId, byte destSectorId)
        {
            var query =
                MongoDB.Driver.Builders.Query<EutranIntraFreqNCell>.Where(
                    e => e.eNodeBId == destENodebId && e.CellId == destSectorId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }
    }
}
