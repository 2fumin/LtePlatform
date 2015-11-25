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
    public class EFCdmaCellRepository : LightWeightRepositroyBase<CdmaCell>, ICdmaCellRepository
    {
        protected override DbSet<CdmaCell> Entities => context.CdmaCells;
        public CdmaCell GetBySectorId(int btsId, byte sectorId)
        {
            return FirstOrDefault(x => x.BtsId == btsId && x.SectorId == sectorId);
        }
    }
}
