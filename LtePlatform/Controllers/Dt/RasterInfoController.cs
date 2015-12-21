using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("网格测试信息查询控制器")]
    public class RasterInfoController : ApiController
    {
        private readonly RasterInfoService _service;

        public RasterInfoController(RasterInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有网格的测试信息")]
        [ApiResponse("所有网格的测试信息视图，包括网格坐标、所属镇区和包含的测试数据表列表")]
        public IEnumerable<RasterInfoView> Get()
        {
            return _service.QueryAllList();
        } 
    }
}
