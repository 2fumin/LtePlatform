using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface IIntraFreqHoGroupRepository : IRepository<IntraFreqHoGroup, ObjectId>
    {
        IntraFreqHoGroup GetRecent(int eNodebId, int localCellId);
    }
}
