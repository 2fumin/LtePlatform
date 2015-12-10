using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("操作校园网指标的控制器")]
    public class CollegeKpiController : ApiController
    {
        private readonly CollegeKpiService _service;

        public CollegeKpiController(CollegeKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定时段的所有校园的指标")]
        [ApiParameterDoc("date", "指定日期")]
        [ApiParameterDoc("hour", "指定时间")]
        [ApiResponse("指定时段的所有校园的指标")]
        public IEnumerable<CollegeKpiView> GetViews(DateTime date, int hour)
        {
            return _service.GetViews(date, hour);
        }

        [HttpGet]
        [ApiDoc("查询指定时段的指定校园的指标，若查不到则会报错")]
        [ApiParameterDoc("date", "指定日期")]
        [ApiParameterDoc("hour", "指定时间")]
        [ApiParameterDoc("name", "指定校园")]
        [ApiResponse("指定时段的指定校园的指标，若查不到则会报错")]
        public IHttpActionResult GetResult(DateTime date, int hour, string name)
        {
            var result = _service.GetResult(date, hour, name);
            return result == null ? (IHttpActionResult)BadRequest("Bad College Name!") : Ok(result);
        }

        [HttpPost]
        [ApiDoc("新增或保存校园网指标信息")]
        [ApiParameterDoc("stat", "待新增或保存校园网指标信息")]
        [ApiResponse("操作结果")]
        public async Task<IHttpActionResult> Post(CollegeKpi stat)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.Post(stat);
            return Ok();
        }

        [HttpDelete]
        [ApiDoc("删除校园网指标信息")]
        [ApiParameterDoc("stat", "待删除校园网指标信息")]
        [ApiResponse("操作结果")]
        public async Task<IHttpActionResult> Delete(CollegeKpiView view)
        {
            var result = _service.GetRecordResult(view);
            if (result == null) return BadRequest("The test record does not exist!");
            await _service.Delete(result);
            return Ok();
        }
    }
}
