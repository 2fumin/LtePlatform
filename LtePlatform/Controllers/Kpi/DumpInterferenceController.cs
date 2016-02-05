using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Parameters.Entities.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入干扰矩阵信息处理器")]
    public class DumpInterferenceController : ApiController
    {
        private readonly InterferenceMatrixService _service;

        public DumpInterferenceController(InterferenceMatrixService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条干扰信息")]
        [ApiResponse("导入结果")]
        public Task<bool> Put()
        {
            return _service.DumpOneStat();
        }

        [HttpGet]
        [ApiDoc("获取当前等待导入干扰信息数")]
        [ApiResponse("当前等待导入干扰信息数")]
        public int Get()
        {
            return _service.GetStatsToBeDump();
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
        [ApiParameterDoc("time", "时间戳信息")]
        [ApiResponse("统计信息")]
        public List<InterferenceMatrixStat> Get(int eNodebId, short pci, DateTime time)
        {
            return _service.QueryStats(eNodebId, pci, time);
        }
            
        [HttpDelete]
        [ApiDoc("清除已上传干扰信息记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}
