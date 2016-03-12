using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Abstract
{
    public interface IBtsRepository : IRepository<CdmaBts>
    {
        CdmaBts GetByBtsId(int btsId);

        int SaveChanges();
    }
}
