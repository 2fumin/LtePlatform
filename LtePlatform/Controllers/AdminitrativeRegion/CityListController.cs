using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Abstract;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [Authorize]
    public class CityListController : ApiController
    {
        private readonly TownQueryService _service;

        public CityListController(TownQueryService service)
        {
            _service = service;
        }

        public IHttpActionResult Get()
        {
            var query = _service.GetCities();
            return query.Count == 0 ? (IHttpActionResult) BadRequest("Empty City List!") : Ok(query);
        }
    }
}
