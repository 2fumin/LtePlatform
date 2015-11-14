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
    public class EFCollege3GTestRepository : LightWeightRepositroyBase<College3GTestResults>, ICollege3GTestRepository
    {
        protected override DbSet<College3GTestResults> Entities => context.College3GTestResultses;
        public College3GTestResults GetByCollegeIdAndTime(int collegeId, DateTime time)
        {
            return FirstOrDefault(x => x.CollegeId == collegeId && x.TestTime == time);
        }

        public College3GTestResults GetByTime(DateTime time)
        {
            return FirstOrDefault(x => x.TestTime == time);
        }

        public IEnumerable<College3GTestResults> GetByTimeSpan(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.TestTime >= begin && x.TestTime < end);
        }
    }
}
