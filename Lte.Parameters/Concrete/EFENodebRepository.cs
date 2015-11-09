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
        protected override DbSet<ENodeb> Entities
        {
            get { return context.ENodebs; }
        }
    }
}
