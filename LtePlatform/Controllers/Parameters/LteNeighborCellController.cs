using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("查询LTE邻区控制器")]
    public class LteNeighborCellController : ApiController
    {
        private readonly LteNeighborCellService _service;

        public LteNeighborCellController(LteNeighborCellService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据小区编号和扇区编号查询LTE邻区列表")]
        [ApiParameterDoc("cellId", "小区编号(eNodebId)")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("LTE邻区列表（PCI可能为空）")]
        public List<LteNeighborCell> Get(int cellId, byte sectorId)
        {
            return _service.QueryCells(cellId, sectorId);
        } 
    }
}
