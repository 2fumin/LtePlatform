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
    public class EFTopDrop2GCellRepository : LightWeightRepositroyBase<TopDrop2GCell>, ITopDrop2GCellRepository
    {
        protected override DbSet<TopDrop2GCell> Entities => context.TopDrop2GStats;

        public int Import(IEnumerable<TopDrop2GCellExcel> stats)
        {
            var count = 0;
            foreach(var stat in stats)
            {
                var time = stat.StatDate.AddHours(stat.StatHour);
                var info = FirstOrDefault(x => x.BtsId == stat.BtsId && x.SectorId == stat.SectorId && x.StatTime == time);
                if (info == null)
                {
                    Insert(new TopDrop2GCell(stat));
                    count++;
                }
            }
            return count;
        }
    }
}
