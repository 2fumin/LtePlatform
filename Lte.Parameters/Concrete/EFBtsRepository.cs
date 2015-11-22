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
    public class EFBtsRepository : LightWeightRepositroyBase<CdmaBts>, IBtsRepository
    {
        protected override DbSet<CdmaBts> Entities => context.Btss;
    }
}
