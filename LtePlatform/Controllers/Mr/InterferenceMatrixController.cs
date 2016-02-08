using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Parameters.Entities.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("干扰矩阵信息查询控制器")]
    public class InterferenceMatrixController : ApiController
    {
        private readonly InterferenceMatrixService _service;

        public InterferenceMatrixController(InterferenceMatrixService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据小区信息和时间戳信息从MongoDB数据库中获取一条统计信息")]
        [ApiParameterDoc("eNodebInfo", "小区信息")]
        [ApiParameterDoc("timeString", "时间戳信息")]
        [ApiResponse("一条统计信息（没有小区信息")]
        public InterferenceMatrixStat Get(string eNodebInfo, string timeString)
        {
            return _service.QueryStat(eNodebInfo, timeString);
        }

        [HttpGet]
        [ApiDoc("根据小区信息和时间戳信息从MongoDB数据库中获取统计信息")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("pci", "PCI")]
        [ApiParameterDoc("date", "时间戳信息")]
        [ApiResponse("统计信息")]
        public InterferenceMatrixMongo Get(int eNodebId, short pci, DateTime date)
        {
            return _service.QueryMongo(eNodebId, pci, date);
        }

        [HttpGet]
        public InterferenceMatrixMongo Get(int eNodebId, short pci)
        {
            return _service.QueryMongo(eNodebId, pci);
        }
    }
}
