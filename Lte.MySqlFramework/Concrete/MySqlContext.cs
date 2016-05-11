using System.Data.Entity;
using Abp.EntityFramework;
using Lte.MySqlFramework.Entities;
using MySql.Data.Entity;

namespace Lte.MySqlFramework.Concrete
{
    //实施数据库迁移前，请解除注释；迁移完成后，请再次注释，否则程序会报错
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlContext : AbpDbContext
    {
        public MySqlContext() : base("MySqlContext")
        {
            
        }

        public DbSet<FlowHuawei> FlowHuaweises { get; set; }

        public DbSet<FlowZte> FlowZtes { get; set; }

        public DbSet<CellStatMysql> CellStatMysqls { get; set; }
    }
}
