using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface IAlarmRepository : IRepository<AlarmStat>
    {
        List<AlarmStat> GetAllList(DateTime begin, DateTime end);

        List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId);

        List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId, byte sectorId);

        int Count(DateTime begin, DateTime end, int eNodebId);

        int SaveChanges();
    }
}
