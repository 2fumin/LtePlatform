using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCollegeKpiRepository : LightWeightRepositroyBase<CollegeKpi>, ICollegeKpiRepository
    {
        protected override DbSet<CollegeKpi> Entities => context.CollegeKpis;
    }
}
