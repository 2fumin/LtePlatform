using System;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ICollege3GTestRepository : IRepository<College3GTestResults>
    {
        College3GTestResults GetByCollegeIdAndTime(int collegeId, DateTime time);
    }
}
