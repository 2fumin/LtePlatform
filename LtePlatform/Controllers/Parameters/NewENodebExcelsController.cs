using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Parameters
{
    public class NewENodebExcelsController : ApiController
    {
        private readonly BasicImportService _service;
        private readonly ENodebDumpService _dumpService;

        public NewENodebExcelsController(BasicImportService service, ENodebDumpService dumpService)
        {
            _service = service;
            _dumpService = dumpService;
        }

        [HttpGet]
        public IEnumerable<ENodebExcel> Get()
        {
            return _service.GetNewENodebExcels();
        }

        [HttpPost]
        public int Post(NewENodebListContainer container)
        {
            return _dumpService.DumpNewEnodebExcels(container.Infos);
        }
    }
}
