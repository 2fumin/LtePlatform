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
    [ApiControl("2G测试数据查询控制器")]
    public class Record2GController : ApiController
    {
        private readonly CsvFileInfoService _service;

        public Record2GController(CsvFileInfoService service)
        {
            _service = service;
        }

        [ApiDoc("查询指定数据文件的测试数据")]
        [ApiParameterDoc("fileName", "数据文件名")]
        [ApiResponse("指定数据文件的测试数据")]
        public IEnumerable<FileRecord2G> Get(string fileName)
        {
            return _service.GetFileRecord2Gs(fileName);
        }

        [ApiDoc("查询指定数据文件和网格编号的测试数据")]
        [ApiParameterDoc("fileName", "数据文件名")]
        [ApiParameterDoc("rasterNum", "网格编号")]
        [ApiResponse("指定数据文件的测试数据")]
        public IEnumerable<FileRecord2G> Get(string fileName, int rasterNum)
        {
            return _service.GetFileRecord2Gs(fileName, rasterNum);
        }
    }
}
