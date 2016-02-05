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
        List<InterferenceMatrixMongo> GetByENodebInfo(string eNodebInfo);

        InterferenceMatrixMongo GetOne(string eNodebInfo, string timeString);

        List<InterferenceMatrixMongo> GetList(int eNodebId, short pci);

        List<InterferenceMatrixMongo> GetList(int eNodebId, short pci, DateTime time);
    }
}
