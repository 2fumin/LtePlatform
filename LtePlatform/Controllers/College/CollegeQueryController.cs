using System.Collections.Generic;
using System.Web.Http;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.College;

namespace LtePlatform.Controllers.College
{
    public class CollegeQueryController : ApiController
    {
        private readonly ICollegeRepository _repository;

        public CollegeQueryController(ICollegeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            CollegeInfo info = _repository.Get(id);
            return info == null ? (IHttpActionResult)BadRequest("College Id Not Found!") : Ok(info);
        }

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            CollegeInfo info = _repository.FirstOrDefault(x => x.Name == name);
            return info == null ? (IHttpActionResult)BadRequest("College Name Not Found!") : Ok(info);
        }

        [HttpGet]
        public IEnumerable<CollegeInfo> Get()
        {
            return _repository.GetAllList();
        }
    }
}
