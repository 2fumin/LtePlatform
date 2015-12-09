using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Abstract;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("获取行政区域信息的控制器")]
    public class CityListController : ApiController
    {
        private readonly TownQueryService _service;

        public CityListController(TownQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得所有城市列表")]
        [ApiResponse("所有城市列表")]
        public IHttpActionResult Get()
        {
            var query = _service.GetCities();
            return query.Count == 0 ? (IHttpActionResult) BadRequest("Empty City List!") : Ok(query);
        }

        [HttpGet]
        [ApiDoc("获得指定城市下属的区域列表")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiResponse("区域列表")]
        public IHttpActionResult Get(string city)
        {
            var query = _service.GetDistricts(city);
            return query.Count == 0 ? (IHttpActionResult)BadRequest("Empty District List!") : Ok(query);
        }

        [HttpGet]
        [ApiDoc("获得指定城市和区域下属的镇区列表")]
        [ApiParameterDoc("city", "指定城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("镇区列表")]
        public IHttpActionResult Get(string city, string district)
        {
            var query = _service.GetTowns(city, district);
            return query.Count == 0 ? (IHttpActionResult)BadRequest("Empty Town List!") : Ok(query);
        }
    }
}
