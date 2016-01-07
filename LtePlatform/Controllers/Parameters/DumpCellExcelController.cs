using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("LTE小区从Excel表导入控制器")]
    public class DumpCellExcelController : ApiController
    {
        private readonly CellDumpService _service;
        private readonly BasicImportService _importService;

        public DumpCellExcelController(CellDumpService service, BasicImportService importService)
        {
            _service = service;
            _importService = importService;
        }

        [HttpPost]
        [ApiDoc("从Excel表导入LTE小区信息")]
        [ApiParameterDoc("info", "LTE小区信息")]
        [ApiResponse("导入是否成功 ")]
        public bool Post(CellExcel info)
        {
            return _service.DumpSingleCellExcel(info);
        }

        [HttpGet]
        [ApiDoc("对比导入的LTE小区信息和数据库现有信息，获取所有待消亡的LTE小区编号信息")]
        [ApiResponse("所有待消亡的LTE小区编号信息")]
        public IEnumerable<CellIdPair> Get()
        {
            return _importService.GetVanishedCellIds();
        }

        [HttpPut]
        [ApiDoc("批量修改数据库中LTE小区的消亡状态")]
        [ApiParameterDoc("container", "待消亡的LTE小区编号容器")]
        public void Put(CellIdsContainer container)
        {
            _service.VanishCells(container);
        }
    }
}
