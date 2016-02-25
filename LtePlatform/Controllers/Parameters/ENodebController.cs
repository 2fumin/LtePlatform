using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("查询LTE基站的控制器")]
    public class ENodebController : ApiController
    {
        private readonly ENodebQueryService _service;

        public ENodebController(ENodebQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据行政区域条件查询基站列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IHttpActionResult Get(string city, string district, string town)
        {
            var result = _service.GetByTownNames(city, district, town);
            return result == null ? (IHttpActionResult) BadRequest("This town has no eNodebs!") : Ok(result);
        }

        [HttpGet]
        [ApiDoc("使用名称模糊查询，可以先后匹配基站名称、基站编号、规划编号和地址")]
        [ApiParameterDoc("name", "模糊查询的名称")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IHttpActionResult Get(string name)
        {
            var result = _service.GetByGeneralName(name);
            return result == null ? (IHttpActionResult)BadRequest("No eNodebs given the query conditions!") : Ok(result);
        }

        [HttpGet]
        [ApiDoc("根据基站编号条件查询基站")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IHttpActionResult Get(int eNodebId)
        {
            var result = _service.GetByENodebId(eNodebId);
            return result == null ? (IHttpActionResult)BadRequest("No eNodebs given the query conditions!") : Ok(result);
        }
    }
}
