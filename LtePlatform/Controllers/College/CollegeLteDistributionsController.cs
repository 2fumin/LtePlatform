using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.College
{
    public class CollegeLteDistributionsController : ApiController
    {
        private readonly CollegeDistributionService _service;

        public CollegeLteDistributionsController(CollegeDistributionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<IndoorDistribution> Get(string collegeName)
        {
            return _service.QueryLteDistributions(collegeName);
        }
    }
}
