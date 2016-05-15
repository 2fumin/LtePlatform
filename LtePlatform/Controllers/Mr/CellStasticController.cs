using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("从MongoDB数据库中查询小区干扰信息统计")]
    public class CellStasticController : ApiController
    {
        private readonly CellStasticService _service;

        public CellStasticController(CellStasticService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的单个小区聚合小区干扰信息统计")]
        [ApiParameterDoc("eNodebId", "小区基站编号")]
        [ApiParameterDoc("pci", "小区PCI（由于MR中不记录扇区编号，必须用PCI进行查询）")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("单个小区聚合小区干扰信息统计")]
        public CellStasticView Get(int eNodebId, short pci, DateTime begin, DateTime end)
        {
            return _service.QueryDateSpanAverageStat(eNodebId, pci, begin, end);
        }

        [HttpGet]
        public List<CellStatMysql> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.QueryDateSpanStatList(eNodebId, sectorId, begin, end);
        }
    }
}
