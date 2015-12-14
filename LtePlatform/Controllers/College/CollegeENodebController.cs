using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网LTE基站查询控制器")]
    public class CollegeENodebController : ApiController
    {
        private readonly CollegeENodebService _service;

        public CollegeENodebController(CollegeENodebService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询单个校园的LTE基站名称列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("LTE基站名称列表")]
        public IEnumerable<string> Get(string collegeName)
        {
            return _service.QueryCollegeENodebNames(collegeName);
        }

        [HttpGet]
        [ApiDoc("查询单个校园的LTE基站视图列表，指定告警统计的时间段")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiParameterDoc("begin", "查询告警的开始日期")]
        [ApiParameterDoc("end", "查询告警的结束日期")]
        [ApiResponse("LTE基站视图列表")]
        public IEnumerable<ENodebView> Get(string collegeName, DateTime begin, DateTime end)
        {
            return _service.QueryCollegeENodebs(collegeName, begin, end);
        }

        [HttpPost]
        [ApiDoc("查询多个校园的LTE基站视图")]
        [ApiParameterDoc("collegeNames", "校园名称列表")]
        [ApiResponse("LTE基站视图列表")]
        public IEnumerable<ENodebView> Post(CollegeNamesContainer collegeNames)
        {
            return _service.QueryCollegeENodebs(collegeNames.Names);
        }
    }
}
