using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Entities.ExcelCsv;

namespace LtePlatform.Controllers.Parameters
{
    public class NewBtsExcelsController : ApiController
    {
        private readonly BasicImportService _service;
        private readonly BtsDumpService _dumpService;

        public NewBtsExcelsController(BasicImportService service, BtsDumpService dumpService)
        {
            _service = service;
            _dumpService = dumpService;
        }

        [HttpGet]
        public IEnumerable<BtsExcel> Get()
        {
            return _service.GetNewBtsExcels();
        }

        [HttpPost]
        public int Post(NewBtsListContainer container)
        {
            return _dumpService.DumpBtsExcels(container.Infos);
        }
    }
}
