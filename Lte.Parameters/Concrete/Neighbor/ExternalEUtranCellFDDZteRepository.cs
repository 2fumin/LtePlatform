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
    public class ExternalEUtranCellFDDZteRepository : MongoDbRepositoryBase<ExternalEUtranCellFDDZte, ObjectId>, IExternalEUtranCellFDDZteRepository
    {
        public ExternalEUtranCellFDDZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "ExternalEUtranCellFDD";
        }

        public ExternalEUtranCellFDDZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<ExternalEUtranCellFDDZte> GetRecentList(int eNodebId)
        {
            var query =
                   MongoDB.Driver.Builders.Query<ExternalEUtranCellFDDZte>.Where(
                       e => e.eNodeB_Id == eNodebId);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.Where(x => x.iDate == recentDate).ToList();
        }
    }
}
