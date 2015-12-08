﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网4G测试控制器")]
    public class College4GTestController : ApiController
    {
        private readonly College4GTestService _service;

        public College4GTestController(College4GTestService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取4G测试记录集合
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <param name="hour">指定时段</param>
        /// <returns></returns>
        [HttpGet]
        [ApiDoc("获取所有校园的4G测试记录集合")]
        [ApiParameterDoc("date", "指定日期")]
        [ApiParameterDoc("hour", "指定时段")]
        [ApiResponse("4G测试记录集合")]
        public IEnumerable<College4GTestView> GetViews(DateTime date, int hour)
        {
            return _service.GetViews(date, hour);
        }

        [HttpGet]
        [ApiDoc("获取指定校园、指定小区的4G测试记录集合")]
        [ApiParameterDoc("date", "指定日期")]
        [ApiParameterDoc("hour", "指定时段")]
        [ApiParameterDoc("name", "校园名称")]
        [ApiParameterDoc("eNodebName", "基站名称")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("4G测试记录集合查询结果，或错误输出信息")]
        public IHttpActionResult GetResult(DateTime date, int hour, string name, string eNodebName, byte sectorId)
        {
            var result = _service.GetResult(date, hour, name, eNodebName, sectorId);
            return result == null ? (IHttpActionResult)BadRequest("Illegal input arguments!") :
                Ok(result);
        }

        [HttpGet]
        [ApiDoc("获取各个校园对应的速率等指标")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("各个校园对应的速率等指标，以字典格式表示")]
        public Dictionary<string, double> GetAverageRates(DateTime begin, DateTime end, byte upload)
        {
            return _service.GetAverageRates(begin, end, upload);
        }

        [HttpPost]
        [ApiDoc("保存4G测试结果信息")]
        [ApiParameterDoc("result", "测试记录信息")]
        [ApiResponse("保存结果")]
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
        [ApiDoc("删除4G测试记录")]
        [ApiParameterDoc("view", "待删除的测试记录")]
        [ApiResponse("删除结果")]
        public async Task<IHttpActionResult> Delete(College4GTestView view)
        {
            var result = _service.GetRecordResult(view.TestTime, view.CollegeName);
            if (result == null) return BadRequest("The test record does not exist!");
            await _service.Delete(result);
            return Ok();
        }
    }
}
