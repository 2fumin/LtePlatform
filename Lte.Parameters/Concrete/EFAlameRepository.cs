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
    public class EFAlameRepository : LightWeightRepositroyBase<AlarmStat>, IAlarmRepository
    {
        protected override DbSet<AlarmStat> Entities => context.AlarmStats;
    }
}
