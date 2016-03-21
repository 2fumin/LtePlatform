using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IEutranInterNFreqRepository : IRepository<EutranInterNFreq, ObjectId>
    {
        List<EutranInterNFreq> GetRecentList(int eNodebId);

        List<EutranInterNFreq> GetRecentList(int eNodebId, int localCellId);
    }
}
