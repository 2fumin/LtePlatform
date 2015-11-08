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
    public class CollegeStatController : ApiController
    {
        private readonly ICollegeRepository _repository;
        private readonly IInfrastructureRepository _infrastructureRepository;

        public CollegeStatController(ICollegeRepository repository, 
            IInfrastructureRepository infrastructureRepository)
        {
            _repository = repository;
            _infrastructureRepository = infrastructureRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            CollegeInfo info = _repository.Get(id);
            return info == null
                ? (IHttpActionResult)BadRequest("ID Not Found!")
                : Ok(new CollegeStat(_repository, info, _infrastructureRepository));
        }

        [HttpGet]
        public IEnumerable<CollegeStat> Get()
        {
            IEnumerable<CollegeInfo> infos = _repository.GetAllList();
            return !infos.Any()
                ? new List<CollegeStat>()
                : infos.Select(x => new CollegeStat(_repository, x, _infrastructureRepository));
        }
    }
}
