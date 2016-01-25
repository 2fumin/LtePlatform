using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFInfrastructureRepository : EfRepositoryBase<EFParametersContext, InfrastructureInfo>, IInfrastructureRepository
    {
        public IEnumerable<int> GetENodebIds(string collegeName)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.ENodeb
                ).Select(x => x.InfrastructureId).ToList();
        }


        public IEnumerable<int> GetCellIds(string collegeName)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.Cell
                ).Select(x => x.InfrastructureId).ToList();
        }

        public IEnumerable<int> GetBtsIds(string collegeName)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.CdmaBts
                ).Select(x => x.InfrastructureId).ToList();
        }

        public IEnumerable<int> GetCdmaCellIds(string collegeName)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.CdmaCell
                ).Select(x => x.InfrastructureId).ToList();
        }

        public IEnumerable<int> GetLteDistributionIds(string collegeName)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.LteIndoor
                ).Select(x => x.InfrastructureId).ToList();
        }

        public IEnumerable<int> GetCdmaDistributionIds(string collegeName)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.CdmaIndoor
                ).Select(x => x.InfrastructureId).ToList();
        }

        public InfrastructureInfo GetTopPreciseMonitor(int id)
        {
            return FirstOrDefault(x => x.InfrastructureId == id && x.HotspotType == HotspotType.TopPrecise);
        }

        public List<InfrastructureInfo> GetAllPreciseMonitor()
        {
            return GetAll().Where(x => x.HotspotType == HotspotType.TopPrecise).ToList();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public EFInfrastructureRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
