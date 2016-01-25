using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Concrete
{
    public class EFENodebRepository : EfRepositoryBase<EFParametersContext, ENodeb>, IENodebRepository
    {   
        public ENodeb GetByENodebId(int eNodebId)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId);
        }

        public ENodeb GetByName(string name)
        {
            return FirstOrDefault(x => x.Name == name);
        }

        public List<ENodeb> GetAllInUseList()
        {
            return GetAll().Where(x => x.IsInUse).ToList();
        }

        public EFENodebRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

    }
}
