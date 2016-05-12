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
        List<InterferenceMatrixMongo> GetList(int eNodebId, short pci);

        Task<List<InterferenceMatrixMongo>> GetListAsync(int eNodebId, short pci);

        Task<List<InterferenceMatrixMongo>> GetListAsync(int eNodebId, short pci, DateTime date);

        Task<List<InterferenceMatrixMongo>> GetListAsync(int eNodebId, short pci, short neighborPci, DateTime date);

        InterferenceMatrixMongo GetOne(int eNodebId, short pci);
    }

    public interface ICellStasticRepository : IRepository<CellStastic, ObjectId>
    {
        List<CellStastic> GetList(int eNodebId, short pci);

        List<CellStastic> GetList(int eNodebId, short pci, DateTime date);
    }
}
