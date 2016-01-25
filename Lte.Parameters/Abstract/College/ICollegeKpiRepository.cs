using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract.College
{
    public interface ICollegeKpiRepository : IRepository<CollegeKpi>
    {
        List<CollegeKpi> GetAllList(DateTime time);

        CollegeKpi GetByCollegeIdAndTime(int collegeId, DateTime time);

        List<CollegeKpi> GetAllList(DateTime begin, DateTime end);
    }
}
