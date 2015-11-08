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
    public class EFCollegeRepository : LightWeightRepositroyBase<CollegeInfo>, ICollegeRepository
    {
        protected override DbSet<CollegeInfo> Entities
        {
            get { return context.CollegeInfos; }
        }

        public CollegeRegion GetRegion(int id)
        {
            var query = Entities.Where(x => x.Id == id).Select(x => x.CollegeRegion);
            return query.Any() ? query.First() : null;
        }
    }
}
