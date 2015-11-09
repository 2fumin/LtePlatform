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
    public class CellController : ApiController
    {
        private readonly CellService _service;

        public CellController(CellService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Cell> Get(double west, double east, double south, double north)
        {
            return _service.GetCells(west, east, south, north);
        }

        [HttpGet]
        public IHttpActionResult GetSectorIds(string eNodebName)
        {
            var query = _service.GetSectorIds(eNodebName);
            return query == null ? (IHttpActionResult)BadRequest("Wrong ENodeb Name!") : Ok(query);
        }
    }
}
