using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Concrete.Kpi
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

        public List<TopConnection3GCell> GetAllList(string city, DateTime begin, DateTime end)
        {
            return GetAll().Where(x => x.StatTime >= begin && x.StatTime < end && x.City == city).ToList();
        }
    }
}
