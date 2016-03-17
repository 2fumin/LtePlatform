using System;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities.College;

namespace Lte.Parameters.Concrete.College
{
    public class EFCollege4GTestRepository : EfRepositoryBase<EFParametersContext, College4GTestResults>, ICollege4GTestRepository
    {
        public College4GTestResults GetByCollegeIdAndTime(int collegeId, DateTime time)
        {
            return FirstOrDefault(x => x.CollegeId == collegeId && x.TestTime == time);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public EFCollege4GTestRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
