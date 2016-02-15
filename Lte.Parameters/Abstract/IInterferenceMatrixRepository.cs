using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Mr;

namespace Lte.Parameters.Abstract
{
    public interface IInterferenceMatrixRepository : IRepository<InterferenceMatrixStat>
    {
        int SaveChanges();

        void UpdateItems(int eNodebId, byte sectorId, short destPci, int destENodebId, byte destSectorId);

        List<InterferenceMatrixStat> GetAllList(DateTime begin, DateTime end, int cellId, byte sectorId);

        List<InterferenceMatrixStat> GetAllVictims(DateTime begin, DateTime end, int cellId, byte sectorId);
    }
}
