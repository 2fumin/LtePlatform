using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Entities;

namespace Lte.Parameters.MockOperations
{
    public static class StatMapperService
    {
        public static void MapCdmaRegionStat()
        {
            Mapper.CreateMap<CdmaRegionStatExcel, CdmaRegionStat>();
        }
    }
}
