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
    public class College3GTestController : ApiController
    {
        private readonly College3GTestService _service;

        public College3GTestController(College3GTestService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<College3GTestView> GetViews(DateTime date, int hour)
        {
            return _service.GetViews(date, hour);
        }

        [HttpGet]
        public IHttpActionResult GetResult(DateTime date, int hour, string name)
        {
            var result = _service.GetResult(date, hour, name);
            return result == null ? (IHttpActionResult)BadRequest("Bad College Name!") : Ok(result);
        }

        [HttpGet]
        public Dictionary<string, double> GetAverageRates(DateTime begin, DateTime end)
        {
            return _service.GetAverageRates(begin, end);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(College3GTestResults result)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.Post(result);
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
