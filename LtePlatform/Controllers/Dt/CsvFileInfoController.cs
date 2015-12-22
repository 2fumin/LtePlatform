using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("DT测试数据文件查询控制器")]
    public class CsvFileInfoController : ApiController
    {
        private readonly CsvFileInfoService _service;

        public CsvFileInfoController(CsvFileInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得所有DT测试数据文件信息")]
        [ApiResponse("所有DT测试数据文件信息，包括测试时间、数据名称、存放目录、测试网络（2G3G4G）和测试距离等")]
        public IEnumerable<CsvFilesInfo> Get()
        {
            return _service.QueryAllList();
        }
        
        [HttpGet]
        [ApiDoc("获得指定日期范围内的DT测试数据文件信息")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("DT测试数据文件信息，包括测试时间、数据名称、存放目录、测试网络（2G3G4G）和测试距离等")]
        public IEnumerable<CsvFilesInfo> Get(DateTime begin, DateTime end)
        {
            return _service.QueryAllList();
        }
    }
}
