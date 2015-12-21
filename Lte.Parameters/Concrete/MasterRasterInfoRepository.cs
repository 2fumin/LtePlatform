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

        private static List<RasterInfo> _list;  

        public IQueryable<RasterInfo> RasterInfos => _context.RasterInfos;

        public List<RasterInfo> GetAllList()
        {
            if (_list == null || !_list.Any())
                _list = RasterInfos.ToList();
            return _list;
        }

        public List<RasterInfo> GetAllList(string dataType)
        {
            switch (dataType)
            {
                case "2G":
                    return GetAllList().Where(x => x.CsvFilesName2G != "").ToList();
                case "3G":
                    return GetAllList().Where(x => x.CsvFilesName3G != "").ToList();
                default:
                    return GetAllList().Where(x => x.CsvFilesName4G != "").ToList();
            }
        }
    }
}
