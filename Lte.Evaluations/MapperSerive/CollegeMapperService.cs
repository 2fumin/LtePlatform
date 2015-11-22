using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.MapperSerive
{
    public static class CollegeMapperService
    {
        public static void MapCollege3GTest()
        {
            Mapper.CreateMap<College3GTestResults, College3GTestView>();
        }

        public static void MapCollege4GTest()
        {
            Mapper.CreateMap<College4GTestResults, College4GTestView>();
        }

        public static void MapCollegeKpi()
        {
            Mapper.CreateMap<CollegeKpi, CollegeKpiView>();
        }
    }
}
