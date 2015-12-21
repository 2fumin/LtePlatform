using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class MasterRasterInfoRepository : IRasterInfoRepository
    {
        private readonly MasterTestContext _context = new MasterTestContext();

        public IQueryable<RasterInfo> RasterInfos => _context.RasterInfos;
    }
}
