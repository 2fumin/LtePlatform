using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCollegeKpiRepository : LightWeightRepositroyBase<CollegeKpi>, ICollegeKpiRepository
    {
        protected override DbSet<CollegeKpi> Entities => context.CollegeKpis;

        public IEnumerable<CollegeKpi> GetList(DateTime time)
        {
            return GetAllList(x => x.TestTime == time);
        }

        public CollegeKpi GetByCollegeIdAndTime(int collegeId, DateTime time)
        {
            return FirstOrDefault(x => x.CollegeId == collegeId && x.TestTime == time);
        }
    }
}
