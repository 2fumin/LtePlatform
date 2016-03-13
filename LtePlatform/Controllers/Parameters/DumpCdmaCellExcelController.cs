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
    [ApiControl("CDMA小区从Excel表导入控制器")]
    public class DumpCdmaCellExcelController : ApiController
    {
        private readonly CdmaCellDumpService _service;
        private readonly BasicImportService _importService;

        public DumpCdmaCellExcelController(CdmaCellDumpService service, BasicImportService importService)
        {
            _service = service;
            _importService = importService;
        }

        [HttpPost]
        [ApiDoc("从Excel表导入CDMA小区信息")]
        [ApiParameterDoc("info", "CDMA小区信息")]
        [ApiResponse("导入是否成功 ")]
        public bool Post(CdmaCellExcel info)
        {
            return _service.DumpSingleCellExcel(info);
        }

        [HttpGet]
        [ApiDoc("对比导入的CDMA小区信息和数据库现有信息，获取所有待消亡的CDMA小区编号信息")]
        [ApiResponse("所有待消亡的CDMA小区编号信息")]
        public IEnumerable<CdmaCellIdPair> Get()
        {
            return _importService.GetVanishedCdmaCellIds();
        }

        [HttpPut]
        [ApiDoc("批量修改数据库中CDMA小区的消亡状态")]
        [ApiParameterDoc("container", "待消亡的CDMA小区编号容器")]
        public void Put(CdmaCellIdsContainer container)
        {
            _service.VanishCells(container);
        }
    }
}
