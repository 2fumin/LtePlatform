using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using EntityFramework.Extensions;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Mr;

namespace Lte.Parameters.Concrete.Mr
{
    public class EFInterferenceMatrixRepository : EfRepositoryBase<EFParametersContext, InterferenceMatrixStat>, IInterferenceMatrixRepository
    {
        public EFInterferenceMatrixRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> UpdateItemsAsync(int eNodebId, byte sectorId, short destPci, int destENodebId, byte destSectorId)
        {
            return GetAll()
                .Where(
                    x => x.ENodebId == eNodebId && x.SectorId == sectorId && x.DestPci == destPci && x.DestENodebId == 0)
                .UpdateAsync(
                    t =>
                        new InterferenceMatrixStat
                        {
                            DestENodebId = destENodebId,
                            DestSectorId = destSectorId
                        });
        }

        public List<InterferenceMatrixStat> GetAllList(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            return
                GetAll()
                    .Where(
                        x =>
                            x.RecordTime >= begin && x.RecordTime < end && x.ENodebId == cellId &&
                            x.SectorId == sectorId)
                    .ToList();
        }

        public List<InterferenceMatrixStat> GetAllVictims(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            return
                GetAll()
                    .Where(
                        x =>
                            x.RecordTime >= begin && x.RecordTime < end && x.DestENodebId == cellId &&
                            x.DestSectorId == sectorId)
                    .ToList();
        }
    }
}
