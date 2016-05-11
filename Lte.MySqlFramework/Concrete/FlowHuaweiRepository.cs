using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Concrete
{
    public class FlowHuaweiRepository :  EfRepositoryBase<MySqlContext, FlowHuawei>, IFlowHuaweiRepository
    {
        public FlowHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<FlowHuawei> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end);
        }

        public async Task<int> CountAsync(DateTime begin, DateTime end)
        {
            return await CountAsync(x => x.StatTime >= begin && x.StatTime < end);
        }

        public List<FlowHuawei> GetAllList(DateTime begin, DateTime end, int eNodebId)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId);
        }

        public List<FlowHuawei> GetAllList(DateTime begin, DateTime end, int eNodebId, byte localCellId)
        {
            return
                GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId &&
                        x.LocalCellId == localCellId);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
