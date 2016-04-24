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
    public class CellDlpcPdschPaRepository : MongoDbRepositoryBase<CellDlpcPdschPa, ObjectId>, ICellDlpcPdschPaRepository
    {
        public CellDlpcPdschPaRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellDlpcPdschPa";
        }

        public CellDlpcPdschPaRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public CellDlpcPdschPa GetRecent(int eNodebId, int localCellId)
        {
            var query =
                MongoDB.Driver.Builders.Query<CellDlpcPdschPa>.Where(
                    e => e.eNodeB_Id == eNodebId && e.LocalCellId == localCellId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
