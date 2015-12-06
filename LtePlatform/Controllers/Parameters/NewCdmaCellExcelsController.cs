using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Parameters
{
    public class NewCdmaCellExcelsController : ApiController
    {
        private readonly BasicImportService _service;
        private readonly CdmaCellDumpService _dumpService;

        public NewCdmaCellExcelsController(BasicImportService service, CdmaCellDumpService dumpService)
        {
            _service = service;
            _dumpService = dumpService;
        }

        [HttpGet]
        public IEnumerable<CdmaCellExcel> Get()
        {
            return _service.GetNewCdmaCellExcels();
        }

        [HttpPost]
        public async Task Post(NewCdmaCellListContainer container)
        {
            await Task.Run(() => { _dumpService.DumpNewCellExcels(container.Infos); });
        }
    }
}
