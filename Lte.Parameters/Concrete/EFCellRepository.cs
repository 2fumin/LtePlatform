using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCellRepository : LightWeightRepositroyBase<Cell>, ICellRepository
    {
        protected override DbSet<Cell> Entities => context.Cells;

        public void AddCells(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                Insert(cell);
            }
        }

        public Cell GetBySectorId(int eNodebId, byte sectorId)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId);
        }

        public List<Cell> GetAllList(int eNodebId)
        {
            return GetAll().Where(x => x.ENodebId == eNodebId).ToList();
        }

        public List<Cell> GetAllList(double west, double east, double south, double north)
        {
            return GetAllList(x =>
                x.Longtitute + GeoMath.BaiduLongtituteOffset >= west 
                && x.Longtitute + GeoMath.BaiduLongtituteOffset <= east 
                && x.Lattitute + GeoMath.BaiduLattituteOffset >= south 
                && x.Lattitute + GeoMath.BaiduLattituteOffset <= north);
        }
    }
}
