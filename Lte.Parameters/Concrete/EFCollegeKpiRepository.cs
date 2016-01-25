using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCollegeKpiRepository : LightWeightRepositroyBase<CollegeKpi>, ICollegeKpiRepository
    {
        protected override DbSet<CollegeKpi> Entities => context.CollegeKpis;

        public List<CollegeKpi> GetAllList(DateTime time)
        {
            return GetAllList(x => x.TestTime == time);
        }

        public CollegeKpi GetByCollegeIdAndTime(int collegeId, DateTime time)
        {
            return FirstOrDefault(x => x.CollegeId == collegeId && x.TestTime == time);
        }

        public List<CollegeKpi> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.TestTime >= begin && x.TestTime < end);
        }
    }
}
