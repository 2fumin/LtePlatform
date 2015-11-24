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
    public class EFTopConnection3GRepository : LightWeightRepositroyBase<TopConnection3GCell>,
        ITopConnection3GRepository
    {
        protected override DbSet<TopConnection3GCell> Entities => context.TopConnection3GStats;

        public int Import(IEnumerable<TopConnection3GCellExcel> stats)
        {
            var count = 0;
            foreach (var stat in from stat in stats
                let time = stat.StatDate.AddHours(stat.StatHour)
                let info =
                    FirstOrDefault(x => x.BtsId == stat.BtsId && x.SectorId == stat.SectorId && x.StatTime == time)
                where info == null
                select stat)
            {
                Insert(TopConnection3GCell.ConstructStat(stat));
                count++;
            }
            return count;
        }
    }
}
