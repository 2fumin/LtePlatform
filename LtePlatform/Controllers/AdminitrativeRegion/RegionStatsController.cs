using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("区域、镇区在用基站扇区数量统计控制器")]
    public class RegionStatsController : ApiController
    {
        private readonly TownQueryService _service;

        public RegionStatsController(TownQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询分区域在用基站扇区数量")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("分区域在用基站扇区数量")]
        public List<DistrictStat> Get(string city)
        {
            return _service.QueryDistrictStats(city);
        }

        [HttpGet]
        [ApiDoc("查询分镇区在用基站扇区数量")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("分镇区在用基站扇区数量")]
        public List<TownStat> Get(string city, string district)
        {
            return _service.QueryTownStats(city, district);
        }
    }
}
