using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFIndoorDistributionRepository
        : LightWeightRepositroyBase<IndoorDistribution>, IIndoorDistributioinRepository
    {
        protected override DbSet<IndoorDistribution> Entities
        {
            get
            {
                return context.IndoorDistributions;
            }
        }
    }
}
