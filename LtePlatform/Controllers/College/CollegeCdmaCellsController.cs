using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询校园网CDMA小区的控制器")]
    public class CollegeCdmaCellsController : ApiController
    {
        private readonly CollegeCdmaCellsService _service;

        public CollegeCdmaCellsController(CollegeCdmaCellsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询校园网CDMA小区列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网CDMA小区列表")]
        public IEnumerable<CdmaCellView> Get(string collegeName)
        {
            return _service.GetViews(collegeName);
        }
    }
}
