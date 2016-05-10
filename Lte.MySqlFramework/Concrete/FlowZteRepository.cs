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
    public class FlowZteRepository : EfRepositoryBase<MySqlContext, FlowZte>, IFlowZteRepository
    {
        public FlowZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<FlowZte> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end);
        }

        public List<FlowZte> GetAllList(DateTime begin, DateTime end, int eNodebId)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId);
        }

        public List<FlowZte> GetAllList(DateTime begin, DateTime end, int eNodebId, byte sectorId)
        {
            return
                GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId &&
                        x.SectorId == sectorId);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
