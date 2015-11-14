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
    public class EFCollege4GTestRepository : LightWeightRepositroyBase<College4GTestResults>, ICollege4GTestRepository
    {
        protected override DbSet<College4GTestResults> Entities => context.College4GTestResultses;

        public College4GTestResults GetByCollegeIdAndTime(int collegeId, DateTime time)
        {
            return FirstOrDefault(x => x.CollegeId == collegeId && x.TestTime == time);
        }

        public College4GTestResults GetByTime(DateTime time)
        {
            return FirstOrDefault(x => x.TestTime == time);
        }
    }
}
