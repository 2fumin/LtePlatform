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
    public class DumpCellExcelController : ApiController
    {
        private readonly CellDumpService _service;

        public DumpCellExcelController(CellDumpService service)
        {
            _service = service;
        }

        public bool Post(CellExcel info)
        {
            return _service.DumpSingleCellExcel(info);
        }
    }
}
