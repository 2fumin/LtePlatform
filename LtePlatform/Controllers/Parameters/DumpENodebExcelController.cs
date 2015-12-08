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
    public class DumpENodebExcelController : ApiController
    {
        private readonly ENodebDumpService _service;

        public DumpENodebExcelController(ENodebDumpService service)
        {
            _service = service;
        }

        [HttpPost]
        public bool Post(ENodebExcel info)
        {
            return _service.DumpSingleENodebExcel(info);
        }
    }
}
