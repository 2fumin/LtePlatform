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
    public class College4GTestController : ApiController
    {
        private readonly College4GTestService _service;

        public College4GTestController(College4GTestService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<College4GTestView> GetViews(DateTime date, int hour)
        {
            return _service.GetViews(date, hour);
        }

        [HttpGet]
        public IHttpActionResult GetResult(DateTime date, int hour, string name, string eNodebName, byte sectorId)
        {
            var result = _service.GetResult(date, hour, name, eNodebName, sectorId);
            return result == null ? (IHttpActionResult)BadRequest("Illegal input arguments!") :
                Ok(result);
        }

        [HttpGet]
        public Dictionary<string, double> GetAverageRates(DateTime begin, DateTime end, byte upload)
        {
            return _service.GetAverageRates(begin, end, upload);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(College4GTestResults result)
        {
            if (ModelState.IsValid)
            {
                await _service.Post(result);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(College4GTestView view)
        {
            var result = _service.GetRecordResult(view.TestTime, view.CollegeName);
            if (result == null) return BadRequest("The test record does not exist!");
            await _service.Delete(result);
            return Ok();
        }
    }
}
