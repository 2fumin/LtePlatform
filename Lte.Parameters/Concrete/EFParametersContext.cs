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

        public DbSet<CollegeInfo> CollegeInfos { get; set; }

        public DbSet<InfrastructureInfo> InfrastructureInfos { get; set; }

        public DbSet<AlarmStat> AlarmStats { get; set; }

        public DbSet<College3GTestResults> College3GTestResultses { get; set; }

        public DbSet<College4GTestResults> College4GTestResultses { get; set; }

        public DbSet<CollegeKpi> CollegeKpis { get; set; }

        public DbSet<ENodeb> ENodebs { get; set; }

        public DbSet<Cell> Cells { get; set; }

        public DbSet<IndoorDistribution> IndoorDistributions { get; set; }
    }
}
