using System.Data.Entity;
using Abp.EntityFramework;
using Lte.MySqlFramework.Entities;
using MySql.Data.Entity;

namespace Lte.MySqlFramework.Concrete
{
    public class MySqlContext : AbpDbContext
    {
        public MySqlContext() : base("MySqlContext")
        {
            
        }

        public DbSet<FlowHuawei> FlowHuaweises { get; set; }
    }
}
