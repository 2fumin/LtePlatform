using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface IFlowHuaweiRepository : IRepository<FlowHuawei>
    {
        List<FlowHuawei> GetAllList(DateTime begin, DateTime end);

        Task<int> CountAsync(DateTime begin, DateTime end);

        List<FlowHuawei> GetAllList(DateTime begin, DateTime end, int eNodebId);

        List<FlowHuawei> GetAllList(DateTime begin, DateTime end, int eNodebId, byte localCellId);

        int SaveChanges();
    }

    public interface IFlowZteRepository : IRepository<FlowZte>
    {
        List<FlowZte> GetAllList(DateTime begin, DateTime end);

        Task<int> CountAsync(DateTime begin, DateTime end);

        List<FlowZte> GetAllList(DateTime begin, DateTime end, int eNodebId);

        List<FlowZte> GetAllList(DateTime begin, DateTime end, int eNodebId, byte sectorId);

        int SaveChanges();
    }
}
