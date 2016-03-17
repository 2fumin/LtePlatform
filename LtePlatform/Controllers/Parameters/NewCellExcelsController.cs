using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("新LTE Excel信息查询数据库")]
    public class NewCellExcelsController : ApiController
    {
        private readonly BasicImportService _service;
        private readonly CellDumpService _dumpService;

        public NewCellExcelsController(BasicImportService service, CellDumpService dumpService)
        {
            _service = service;
            _dumpService = dumpService;
        }

        [HttpGet]
        [ApiDoc("查询待导入的小区Excel信息列表")]
        [ApiResponse("待导入的小区Excel信息列表")]
        public IEnumerable<CellExcel> Get()
        {
            return _service.GetNewCellExcels();
        }

        [HttpPost]
        [ApiDoc("导入新的小区信息")]
        [ApiParameterDoc("container", "新的小区信息列表容器")]
        [ApiResponse("成功导入条数")]
        public int Post(NewCellListContainer container)
        {
            _dumpService.UpdateENodebBtsIds(container.Infos);
            return _dumpService.DumpNewCellExcels(container.Infos);
        }
    }
}
