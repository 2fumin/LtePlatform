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
    public class EFTopDrop2GCellRepository : LightWeightRepositroyBase<TopDrop2GCell>, ITopDrop2GCellRepository
    {
        protected override DbSet<TopDrop2GCell> Entities => context.TopDrop2GStats;
    }
}
