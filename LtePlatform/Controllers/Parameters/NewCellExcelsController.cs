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
    public class NewCellExcelsController : ApiController
    {
        private readonly BasicImportService _service;
        private readonly CellDumpService _dumpService;

        public NewCellExcelsController(BasicImportService service, CellDumpService dumpService)
        {
            _service = service;
            _dumpService = dumpService;
        }

        [HttpGet]
        public IEnumerable<CellExcel> Get()
        {
            return _service.GetNewCellExcels();
        }

        [HttpPost]
        public async Task Post(IEnumerable<CellExcel> infos)
        {
            await Task.Run(() =>
            {
                _dumpService.DumpNewCellExcels(infos);
                _dumpService.UpdateENodebBtsIds(infos);
            });
        }
    }
}
