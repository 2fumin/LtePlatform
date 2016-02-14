using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("查询PCI邻区控制器")]
    public class NearestPciCellController : ApiController
    {
        private readonly NearestPciCellService _service;

        public NearestPciCellController(NearestPciCellService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据小区编号和扇区编号查询PCI邻区列表")]
        [ApiParameterDoc("cellId", "小区编号(eNodebId)")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("PCI邻区列表")]
        public List<NearestPciCellView> Get(int cellId, byte sectorId)
        {
            return _service.QueryCells(cellId, sectorId);
        }

        [HttpPost]
        [ApiDoc("更新精确覆盖率统计项内邻区列表的PCI定义")]
        [ApiParameterDoc("view", "精确覆盖率统计项")]
        [ApiResponse("更新结果")]
        public int Post(Precise4GView view)
        {
            return _service.UpdateNeighborPcis(view.CellId, view.SectorId);
        }

        [HttpPut]
        [ApiDoc("更新单条邻区定义")]
        [ApiParameterDoc("cell", "待更新的邻区信息")]
        public void Put(NearestPciCell cell)
        {
            _service.UpdateNeighborCell(cell);
        }
    }
}
