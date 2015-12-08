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
    public class DumpBtsExcelController : ApiController
    {
        private readonly BtsDumpService _service;

        public DumpBtsExcelController(BtsDumpService service)
        {
            _service = service;
        }

        public bool Post(BtsExcel info)
        {
            return _service.DumpSingleBtsExcel(info);
        }
    }
}
