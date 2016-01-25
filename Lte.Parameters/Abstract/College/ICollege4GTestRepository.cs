using System;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.College;

namespace Lte.Parameters.Abstract.College
{
    public interface ICollege4GTestRepository : IRepository<College4GTestResults>
    {
        College4GTestResults GetByCollegeIdAndTime(int collegeId, DateTime time);

        int SaveChanges();
    }
}
