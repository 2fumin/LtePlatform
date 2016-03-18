using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Switch
{
    public class UeEUtranMeasurementRepository : MongoDbRepositoryBase<UeEUtranMeasurementZte, ObjectId>, IUeEUtranMeasurementRepository
    {
        public UeEUtranMeasurementRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "UeEUtranMeasurement";
        }

        public UeEUtranMeasurementRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public UeEUtranMeasurementZte GetRecent(int eNodebId, int measIndex)
        {
            var query =
                MongoDB.Driver.Builders.Query<UeEUtranMeasurementZte>.Where(
                    e => e.eNodeB_Id == eNodebId && e.measCfgIdx == measIndex);
            var list = Collection.Find(query).AsQueryable();
            var recentDate = list.Max(x => x.iDate);
            return list.FirstOrDefault(x => x.iDate == recentDate);
        }
    }
}
