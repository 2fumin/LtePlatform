using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入精确覆盖率的控制器")]
    public class PreciseImportController : ApiController
    {
        private readonly PreciseImportService _service;

        public PreciseImportController(PreciseImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得等待导入数据库的记录总数")]
        [ApiResponse("等待导入数据库的记录总数")]
        public int Get()
        {
            return _service.GetStatsToBeDump();
        }

        [HttpPut]
        [ApiDoc("导入一条精确覆盖率记录")]
        [ApiResponse("导入是否成功")]
        public bool Put()
        {
            return _service.DumpOneStat();
        }

        [HttpDelete]
        [ApiDoc("清除等待导入数据库的记录")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}
