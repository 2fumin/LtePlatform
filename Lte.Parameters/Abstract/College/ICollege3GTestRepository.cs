using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract.College
{
    public interface ICollege3GTestRepository : IRepository<College3GTestResults>
    {
        College3GTestResults GetByCollegeIdAndTime(int collegeId, DateTime time);

        List<College3GTestResults> GetAllList(DateTime begin, DateTime end);

        int SaveChanges();
    }
}
