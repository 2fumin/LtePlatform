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
        [ApiDoc("根据小区信息和时间戳信息从MongoDB数据库中获取统计信息")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("pci", "PCI")]
        [ApiParameterDoc("date", "时间戳信息")]
        [ApiResponse("统计信息")]
        public List<InterferenceMatrixMongo> Get(int eNodebId, short pci, DateTime date)
        {
            return _service.QueryMongoList(eNodebId, pci, date);
        }

        [HttpGet]
        [ApiDoc("根据小区信息从MongoDB数据库中获取统计信息")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("pci", "PCI")]
        [ApiResponse("统计信息")]
        public InterferenceMatrixMongo Get(int eNodebId, short pci)
        {
            return _service.QueryMongo(eNodebId, pci);
        }

        [HttpGet]
        public int Get(int eNodebId, byte sectorId, DateTime date, double interference)
        {
            _service.TestDumpOneStat(eNodebId, sectorId, date, interference);
            return 1;
        }
    }
}
