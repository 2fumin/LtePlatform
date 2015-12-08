using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Parameters
{
    public class DumpCdmaCellExcelController : ApiController
    {
        private readonly CdmaCellDumpService _service;

        public DumpCdmaCellExcelController(CdmaCellDumpService service)
        {
            _service = service;
        }

        [HttpPost]
        public bool Post(CdmaCellExcel info)
        {
            return _service.DumpSingleCellExcel(info);
        }
    }
}
