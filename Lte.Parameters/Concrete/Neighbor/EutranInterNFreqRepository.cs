using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Neighbor
{
    public class EutranInterNFreqRepository : MongoDbRepositoryBase<EutranInterNFreq, ObjectId>, IEutranInterNFreqRepository
    {
        public EutranInterNFreqRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EutranInterNFreq";
        }

        public EutranInterNFreqRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<EutranInterNFreq> GetRecentList(int eNodebId)
        {
            var query = MongoDB.Driver.Builders.Query<EutranInterNFreq>.EQ(e => e.eNodeB_Id, eNodebId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }

        public List<EutranInterNFreq> GetRecentList(int eNodebId, int localCellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<EutranInterNFreq>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }
    }
}
