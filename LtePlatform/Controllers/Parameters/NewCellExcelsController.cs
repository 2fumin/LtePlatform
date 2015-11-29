using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Parameters
{
    public class NewCellExcelsController : ApiController
    {
        private readonly BasicImportService _service;

        public NewCellExcelsController(BasicImportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<CellExcel> Get()
        {
            return _service.GetNewCellExcels();
        } 
    }
}
