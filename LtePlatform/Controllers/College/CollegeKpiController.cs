using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.College
{
    public class CollegeKpiController : ApiController
    {
        private readonly CollegeKpiService _service;

        public CollegeKpiController(CollegeKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<CollegeKpiView> GetViews(DateTime date, int hour)
        {
            return _service.GetViews(date, hour);
        }

        [HttpGet]
        public IHttpActionResult GetResult(DateTime date, int hour, string name)
        {
            var result = _service.GetResult(date, hour, name);
            return result == null ? (IHttpActionResult)BadRequest("Bad College Name!") : Ok(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(CollegeKpi stat)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.Post(stat);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Delete(DateTime recordDate, int hour, string name)
        {
            var result = _service.GetRecordResult(recordDate, hour, name);
            if (result == null) return BadRequest("The test record does not exist!");
            await _service.Delete(result);
            return Ok();
        }
    }
}
