using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.College
{
    public class CollegeRegionController : ApiController
    {
        private readonly ICollegeRepository _repository;

        public CollegeRegionController(ICollegeRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get(int id)
        {
            var region = _repository.GetRegion(id);
            return region == null 
                ? (IHttpActionResult)BadRequest("College Id Not Found Or without region!") 
                : Ok(region);
        }
    }
}
