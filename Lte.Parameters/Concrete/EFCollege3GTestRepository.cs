using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCollege3GTestRepository : EfRepositoryBase<EFParametersContext, College3GTestResults>, ICollege3GTestRepository
    {
        public College3GTestResults GetByCollegeIdAndTime(int collegeId, DateTime time)
        {
            return FirstOrDefault(x => x.CollegeId == collegeId && x.TestTime == time);
        }
        
        public List<College3GTestResults> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.TestTime >= begin && x.TestTime < end);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public EFCollege3GTestRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public EFCollege3GTestRepository()
            : base(new SimpleDbContextProvider<EFParametersContext>(new EFParametersContext()))
        {
            
        }
    }
}
