using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Channel
{
    public class PDSCHCfgRepository : MongoDbRepositoryBase<PDSCHCfg, ObjectId>, IPDSCHCfgRepository
    {
        public PDSCHCfgRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PDSCHCfg";
        }

        public PDSCHCfgRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public PDSCHCfg GetRecent(int eNodebId, int localCellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<PDSCHCfg>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
