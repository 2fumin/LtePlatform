using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Abstract;

namespace LtePlatform.Controllers.College
{
    public class CollegeENodebController : ApiController
    {
        private readonly CollegeENodebService _service;

        public CollegeENodebController(CollegeENodebService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<string> Get(string collegeName)
        {
            return _service.QueryCollegeENodebNames(collegeName);
        }
    }
}
