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
            foreach (var stat in stats)
            {
                var time = stat.StatDate.AddHours(stat.StatHour);
                var info = FirstOrDefault(x => x.BtsId == stat.BtsId && x.SectorId == stat.SectorId && x.StatTime == time);
                if (info == null)
                {
                    Insert(new TopConnection3GCell(stat));
                    count++;
                }
            }
            return count;
        }
    }
}
