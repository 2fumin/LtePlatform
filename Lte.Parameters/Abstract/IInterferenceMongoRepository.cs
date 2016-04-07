using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Mr;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract
{
    public interface IInterferenceMongoRepository : IRepository<InterferenceMatrixMongo, ObjectId>
    {
        InterferenceMatrixMongo GetOne(int eNodebId, short pci);

        InterferenceMatrixMongo GetOne(int eNodebId, short pci, DateTime time);

        List<InterferenceMatrixMongo> GetList(int eNodebId, short pci);

        List<InterferenceMatrixMongo> GetList(int eNodebId, short pci, DateTime date);

    }
}
