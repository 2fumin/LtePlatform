using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网3G测试记录查询控制器")]
    public class College3GTestController : ApiController
    {
        private readonly College3GTestService _service;

        public College3GTestController(College3GTestService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期和时段3G测试记录列表")]
        [ApiParameterDoc("date", "日期")]
        [ApiParameterDoc("hour", "时段")]
        [ApiResponse("3G测试记录视图列表")]
        public IEnumerable<College3GTestView> Get(DateTime date, int hour)
        {
            return _service.GetViews(date, hour);
        }

        [HttpGet]
        [ApiDoc("查询指定日期、时段和校园名称的3G测试记录")]
        [ApiParameterDoc("date", "日期")]
        [ApiParameterDoc("hour", "时段")]
        [ApiParameterDoc("name", "校园名称")]
        [ApiResponse("3G测试记录视图")]
        public IHttpActionResult Get(DateTime date, int hour, string name)
        {
            var result = _service.GetResult(date, hour, name);
            return result == null ? (IHttpActionResult)BadRequest("Bad College Name!") : Ok(result);
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的各校园的平均速率")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("各校园的平均速率")]
        public Dictionary<string, double> Get(DateTime begin, DateTime end)
        {
            return _service.GetAverageRates(begin, end);
        }
        
        [HttpGet]
        [ApiDoc("查询指定日期范围内的各校园的单项指标")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("kpiName", "指标名称")]
        [ApiResponse("各校园的单项指标")]
        public Dictionary<string, double> Get(DateTime begin, DateTime end, string kpiName)
        {
            switch (kpiName)
            {
                case "users":
                    return _service.GetAverageUsers(begin, end);
                case "minRssi":
                    return _service.GetAverageMinRssi(begin, end);
                case "maxRssi":
                    return _service.GetAverageMaxRssi(begin, end);
                default:
                    return _service.GetAverageVswr(begin, end);
            }
        }

        [HttpPost]
        [ApiDoc("保存校园网3G测试记录")]
        [ApiParameterDoc("result", "校园网3G测试记录")]
        [ApiResponse("保存结果")]
        public async Task<IHttpActionResult> Post(College3GTestResults result)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.Post(result);
            return Ok();
        }

        [HttpDelete]
        [ApiDoc("删除校园网3G测试记录")]
        [ApiParameterDoc("view", "校园网3G测试记录视图")]
        [ApiResponse("删除结果")]
        public async Task<IHttpActionResult> Delete(College3GTestView view)
        {
            var result = _service.GetRecordResult(view.TestTime, view.CollegeName);
            if (result == null) return BadRequest("The test record does not exist!");
            await _service.Delete(result);
            return Ok();
        }
    }
}
