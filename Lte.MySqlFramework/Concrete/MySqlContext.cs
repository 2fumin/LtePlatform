using System.Data.Entity;
using Abp.EntityFramework;
using Lte.MySqlFramework.Entities;
using MySql.Data.Entity;

namespace Lte.MySqlFramework.Concrete
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlContext : AbpDbContext
    {
        public MySqlContext() : base("MySqlContext")
        {
            
        }

        public DbSet<FlowHuawei> FlowHuaweises { get; set; }
    }
}
