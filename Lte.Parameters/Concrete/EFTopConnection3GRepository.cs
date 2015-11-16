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
    public class EFTopConnection3GRepository : LightWeightRepositroyBase<TopConnection3GCell>,
        ITopConnection3GRepository
    {
        protected override DbSet<TopConnection3GCell> Entities => context.TopConnection3GStats;
    }
}
