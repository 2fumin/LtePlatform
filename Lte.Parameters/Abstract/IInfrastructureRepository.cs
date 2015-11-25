using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface IInfrastructureRepository : IRepository<InfrastructureInfo>
    {
        IEnumerable<int> GetENodebIds(string collegeName);

        IEnumerable<int> GetCellIds(string collegeName);

        IEnumerable<int> GetBtsIds(string collegeName);

        IEnumerable<int> GetCdmaCellIds(string collegeName);
    }
}
