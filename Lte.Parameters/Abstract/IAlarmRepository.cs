using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface IAlarmRepository : IRepository<AlarmStat>
    {
    }
}
