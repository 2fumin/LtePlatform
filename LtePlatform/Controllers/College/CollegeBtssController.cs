using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网CDMA基站查询控制器")]
    public class CollegeBtssController : ApiController
    {
        private readonly CollegeBtssService _service;

        public CollegeBtssController(CollegeBtssService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询单个校园的CDMA基站视图列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("CDMA基站视图列表")]
        public IEnumerable<CdmaBtsView> Get(string collegeName)
        {
            return _service.QueryCollegeBtss(collegeName);
        }

        [HttpPost]
        [ApiDoc("查询多个校园的CDMA基站视图")]
        [ApiParameterDoc("collegeNames", "校园名称列表")]
        [ApiResponse("CDMA基站视图列表")]
        public IEnumerable<CdmaBtsView> Post(IEnumerable<string> collegeNames)
        {
            return _service.QueryCollegeBtss(collegeNames);
        }
    }
}
