using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract.College
{
    public interface ICollegeRepository : IRepository<CollegeInfo>
    {
        CollegeRegion GetRegion(int id);

        CollegeInfo GetByName(string name);

        RectangleRange GetRange(string name);
    }
}
