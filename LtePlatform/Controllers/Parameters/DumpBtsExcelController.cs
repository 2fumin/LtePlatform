using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Entities.ExcelCsv;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("CDMA基站从Excel表导入控制器")]
    public class DumpBtsExcelController : ApiController
    {
        private readonly BtsDumpService _service;
        private readonly BasicImportService _importService;

        public DumpBtsExcelController(BtsDumpService service, BasicImportService importService)
        {
            _service = service;
            _importService = importService;
        }

        [HttpPost]
        [ApiDoc("从Excel表导入CDMA基站信息")]
        [ApiParameterDoc("info", "CDMA基站信息")]
        [ApiResponse("导入是否成功 ")]
        public bool Post(BtsExcel info)
        {
            return _service.DumpSingleBtsExcel(info);
        }

        [HttpGet]
        [ApiDoc("对比导入的CDMA基站信息和数据库现有信息，获取所有待消亡的CDMA基站编号信息")]
        [ApiResponse("所有待消亡的CDMA基站编号信息")]
        public IEnumerable<int> Get()
        {
            return _importService.GetVanishedBtsIds();
        }

        [HttpPut]
        [ApiDoc("批量修改数据库中CDMA基站的消亡状态")]
        [ApiParameterDoc("container", "待消亡的CDMA基站编号容器")]
        public void Put(ENodebIdsContainer container)
        {
            _service.VanishBtss(container);
        }
    }
}
