using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.Precise;
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

        [HttpGet]
        [ApiDoc("获得指定日期范围内的已导入精确覆盖率记录统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("指定日期范围内的已导入精确覆盖率记录统计")]
        public IEnumerable<PreciseHistory> Get(DateTime begin, DateTime end)
        {
            return _service.GetPreciseHistories(begin, end);
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
