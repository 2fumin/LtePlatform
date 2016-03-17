using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IBtsRepository : IRepository<CdmaBts>
    {
        CdmaBts GetByBtsId(int btsId);

        CdmaBts GetByName(string name);

        List<CdmaBts> GetAllInUseList();

        int SaveChanges();
    }
}
