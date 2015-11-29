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
    public class NewBtsExcelsController : ApiController
    {
        private readonly BasicImportService _service;

        public NewBtsExcelsController(BasicImportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<BtsExcel> Get()
        {
            return _service.GetNewBtsExcels();
        } 
    }
}
