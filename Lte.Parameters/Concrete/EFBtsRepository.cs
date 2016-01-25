using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Concrete
{
    public class EFBtsRepository : LightWeightRepositroyBase<CdmaBts>, IBtsRepository
    {
        protected override DbSet<CdmaBts> Entities => context.Btss;

        public CdmaBts GetByBtsId(int btsId)
        {
            return FirstOrDefault(x => x.BtsId == btsId);
        }
    }
}
