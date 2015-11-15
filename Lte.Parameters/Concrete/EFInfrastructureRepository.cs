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
    public class EFInfrastructureRepository : LightWeightRepositroyBase<InfrastructureInfo>, IInfrastructureRepository
    {
        protected override DbSet<InfrastructureInfo> Entities => context.InfrastructureInfos;
        public IEnumerable<int> GetIds(string collegeName)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == InfrastructureType.ENodeb
                ).Select(x => x.InfrastructureId).ToList();
        }
    }
}
