using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Concrete.Basic
{
    public class EFBtsRepository : EfRepositoryBase<EFParametersContext, CdmaBts>, IBtsRepository
    {
        public CdmaBts GetByBtsId(int btsId)
        {
            return FirstOrDefault(x => x.BtsId == btsId);
        }

        public CdmaBts GetByName(string name)
        {
            return FirstOrDefault(x => x.Name == name);
        }

        public List<CdmaBts> GetAllInUseList()
        {
            return GetAll().Where(x => x.IsInUse).ToList();
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
