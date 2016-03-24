using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Neighbor
{
    public interface IEutranIntraFreqNCellRepository : IRepository<EutranIntraFreqNCell, ObjectId>
    {
        List<EutranIntraFreqNCell> GetRecentList(int eNodebId, byte localCellId);
    }
}
