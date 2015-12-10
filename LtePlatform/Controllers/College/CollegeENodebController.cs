using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;
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

        [HttpGet]
        public IEnumerable<ENodebView> Get(string collegeName, DateTime begin, DateTime end)
        {
            return _service.QueryCollegeENodebs(collegeName, begin, end);
        }
    }
}
