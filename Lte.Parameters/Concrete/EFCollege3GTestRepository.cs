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
    }
}
