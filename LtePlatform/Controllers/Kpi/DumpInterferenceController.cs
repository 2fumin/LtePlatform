using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService;
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

        [HttpDelete]
        [ApiDoc("清除已上传干扰信息记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}
