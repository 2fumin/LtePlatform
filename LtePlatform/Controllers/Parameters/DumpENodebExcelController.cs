using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("LTE基站从Excel表导入控制器")]
    public class DumpENodebExcelController : ApiController
    {
        private readonly ENodebDumpService _service;
        private readonly BasicImportService _importService;

        public DumpENodebExcelController(ENodebDumpService service, BasicImportService importService)
        {
            _service = service;
            _importService = importService;
        }

        [HttpPost]
        [ApiDoc("从Excel表导入LTE基站信息")]
        [ApiParameterDoc("info", "LTE基站信息")]
        [ApiResponse("导入是否成功 ")]
        public bool Post(ENodebExcel info)
        {
            return _service.DumpSingleENodebExcel(info);
        }

        [HttpGet]
        [ApiDoc("对比导入的LTE基站信息和数据库现有信息，获取所有待消亡的LTE基站编号信息")]
        [ApiResponse("所有待消亡的LTE基站编号信息")]
        public IEnumerable<int> Get()
        {
            return _importService.GetVanishedENodebIds();
        }

        [HttpPut]
        [ApiDoc("批量修改数据库中LTE基站的消亡状态")]
        [ApiParameterDoc("container", "待消亡的LTE基站编号容器")]
        public void Put(ENodebIdsContainer container)
        {
            _service.VanishENodebs(container);
        }
    }
}
