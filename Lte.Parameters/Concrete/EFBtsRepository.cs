using System.Data.Entity;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Concrete
{
    public class EFBtsRepository : EfRepositoryBase<EFParametersContext, CdmaBts>, IBtsRepository
    {
        public CdmaBts GetByBtsId(int btsId)
        {
            return FirstOrDefault(x => x.BtsId == btsId);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public EFBtsRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
