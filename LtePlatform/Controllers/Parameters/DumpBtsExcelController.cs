using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Dump;
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
    }
}
