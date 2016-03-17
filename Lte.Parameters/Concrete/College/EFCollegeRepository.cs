using System.Data.Entity;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.College;

namespace Lte.Parameters.Concrete.College
{
    public class EFCollegeRepository : LightWeightRepositroyBase<CollegeInfo>, ICollegeRepository
    {
        protected override DbSet<CollegeInfo> Entities => context.CollegeInfos;

        public CollegeRegion GetRegion(int id)
        {
            var query = Entities.Where(x => x.Id == id).Select(x => x.CollegeRegion);
            return query.Any() ? query.First() : null;
        }

        public CollegeInfo GetByName(string name)
        {
            return FirstOrDefault(x => x.Name == name);
        }

        public RectangleRange GetRange(string name)
        {
            var college = GetByName(name);
            return college == null ? null : GetRegion(college.Id)?.RectangleRange;
        }
    }
}
