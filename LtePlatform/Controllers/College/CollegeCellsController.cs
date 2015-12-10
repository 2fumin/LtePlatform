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
    [ApiControl("查询校园网LTE小区的控制器")]
    public class CollegeCellsController : ApiController
    {
        private readonly CollegeCellsService _service;

        public CollegeCellsController(CollegeCellsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询校园网LTE小区")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网LTE小区列表")]
        public IEnumerable<CellView> Get(string collegeName)
        {
            return _service.GetViews(collegeName);
        }
    }
}
