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
    public class EUtranRelationZteRepository : MongoDbRepositoryBase<EUtranRelationZte, ObjectId>, IEUtranRelationZteRepository
    {
        public EUtranRelationZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EUtranRelation";
        }

        public EUtranRelationZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public IEnumerable<EUtranRelationZte> GetRecent(int eNodebId, byte sectorId)
        {
            throw new NotImplementedException();
        }
    }
}
