using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IENodebRepository : IRepository<ENodeb>
    {
        ENodeb GetByENodebId(int eNodebId);

        ENodeb GetByName(string name);

        List<ENodeb> GetAllInUseList();

        int SaveChanges();
    }
}
