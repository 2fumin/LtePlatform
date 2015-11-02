using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFParametersContext : AbpDbContext
    {
        public EFParametersContext() : base("EFParametersContext")
        {
        }

        public DbSet<Town> Towns { get; set; }

        public DbSet<CdmaRegionStat> CdmaRegionStats { get; set; }

        public DbSet<OptimizeRegion> OptimizeRegions { get; set; }
    }
}
