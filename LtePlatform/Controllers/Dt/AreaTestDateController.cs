using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("各区域测试日期信息的控制器")]
    public class AreaTestDateController : ApiController
    {
        private readonly AreaTestDateService _service;

        public AreaTestDateController(AreaTestDateService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得各区域测试日期信息，包括2G、3G、4G的最近一次测试日期")]
        [ApiResponse("各区域测试日期信息，包括2G、3G、4G的最近一次测试日期")]
        public IEnumerable<AreaTestDate> Get()
        {
            return _service.QueryAllList();
        }
    }
}
