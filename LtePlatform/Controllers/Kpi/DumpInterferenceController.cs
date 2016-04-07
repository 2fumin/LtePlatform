using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.MapperSerive;
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
        [ApiDoc("验证输入日期段是否合法，如合法则返回待导入的小区信息列表（即受监控的信息列表）")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("待导入的小区信息列表")]
        public List<PciCell> Get(DateTime begin, DateTime end)
        {
            return (end < begin) ? new List<PciCell>() : InterferenceMatrixService.PciCellList;
        }

        [HttpGet]
        public List<InterferenceMatrixStat> Get(int eNodebId, short pci, DateTime date)
        {
            return _service.QueryStats(eNodebId, pci, date);
        }

        [HttpGet]
        public int GetExisted(int eNodebId, byte sectorId, DateTime date)
        {
            return _service.QueryExistedStatsCount(eNodebId, sectorId, date);
        }

        [HttpPost]
        [ApiDoc("导入指定小区信息、开始日期和结束日期的干扰矩阵信息")]
        [ApiParameterDoc("dumpInfo", "指定小区信息、开始日期和结束日期")]
        [ApiResponse("一共导入的数量")]
        public int Post(InterferenceMatrixStat dumpInfo)
        {
            return _service.DumpMongoStats(dumpInfo);
        }
   
        [HttpDelete]
        [ApiDoc("清除已上传干扰信息记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}
