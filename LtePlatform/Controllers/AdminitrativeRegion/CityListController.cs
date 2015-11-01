using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Parameters.Abstract;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [Authorize]
    public class CityListController : ApiController
    {
        private readonly ITownRepository _repository;

        public CityListController(ITownRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {
            var query = _repository.GetAll().Select(x => x.CityName).Distinct().ToList();
            return query.Count == 0 ? (IHttpActionResult) BadRequest("Empty City List!") : Ok(query);
        }
    }
}
