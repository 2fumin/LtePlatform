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

        public IEnumerable<EutranIntraFreqNCell> GetRecent(int eNodebId, byte localCellId)
        {
            throw new NotImplementedException();
        }
    }
}
