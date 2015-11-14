using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFENodebRepository : LightWeightRepositroyBase<ENodeb>, IENodebRepository
    {        
        protected override DbSet<ENodeb> Entities => context.ENodebs;

        public ENodeb GetByENodebId(int eNodebId)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId);
        }

        public ENodeb GetByName(string name)
        {
            return FirstOrDefault(x => x.Name == name);
        }
    }
}
