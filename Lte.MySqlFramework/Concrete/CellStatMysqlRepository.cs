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
    public class CellStatMysqlRepository : EfRepositoryBase<MySqlContext, CellStatMysql>, ICellStatMysqlRepository
    {
        public CellStatMysqlRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CellStatMysql Get(int eNodebId, byte sectorId, DateTime date)
        {
            var nextDate = date.AddDays(1);
            return
                FirstOrDefault(
                    x =>
                        x.ENodebId == eNodebId && x.SectorId == sectorId && x.CurrentDate >= date &&
                        x.CurrentDate < nextDate);
        }

        public CellStatMysql Get(int eNodebId, short pci, DateTime date)
        {
            var nextDate = date.AddDays(1);
            return
                FirstOrDefault(
                    x =>
                        x.ENodebId == eNodebId && x.Pci == pci && x.CurrentDate >= date &&
                        x.CurrentDate < nextDate);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
