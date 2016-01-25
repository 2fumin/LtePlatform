using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ICollegeRepository : IRepository<CollegeInfo>
    {
        CollegeRegion GetRegion(int id);

        CollegeInfo GetByName(string name);

        RectangleRange GetRange(string name);
    }
}
