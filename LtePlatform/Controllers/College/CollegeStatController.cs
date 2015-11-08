using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.College
{
    public class CollegeStatController : ApiController
    {
        private readonly CollegeStatService _service;

        public CollegeStatController(CollegeStatService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            CollegeStat stat = _service.QueryStat(id);
            return stat == null ? (IHttpActionResult)BadRequest("ID Not Found!") : Ok(stat);
        }

        [HttpGet]
        public IEnumerable<CollegeStat> Get()
        {
            return _service.QueryStats();
        }
    }
}
