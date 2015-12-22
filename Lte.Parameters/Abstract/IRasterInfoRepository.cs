using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface IRasterInfoRepository
    {
        IQueryable<RasterInfo> RasterInfos { get; }

        List<RasterInfo> GetAllList();

        List<RasterInfo> GetAllList(string dataType);

        List<RasterInfo> GetAllList(string dataType, double west, double east, double south, double north);
    }
}
