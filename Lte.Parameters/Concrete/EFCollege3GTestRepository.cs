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
    }
}
