using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("干扰邻区查询控制器")]
    public class InterferenceNeighborController : ApiController
    {
        private readonly InterferenceNeighborService _service;
        private readonly NearestPciCellService _neighborService;

        public InterferenceNeighborController(InterferenceNeighborService service, 
            NearestPciCellService neighborService)
        {
            _service = service;
            _neighborService = neighborService;
        }

        [HttpGet]
        [ApiDoc("更新指定小区的邻区干扰记录（匹配小区编号和扇区编号）")]
        [ApiParameterDoc("cellId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("更新结果")]
        public async Task<int> Get(int cellId, byte sectorId)
        {
            return await _service.UpdateNeighbors(cellId, sectorId);
        }

        [HttpGet]
        [ApiDoc("更新指定小区的邻区的邻区干扰记录（匹配小区编号和扇区编号）")]
        [ApiParameterDoc("cellId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("更新结果")]
        public async Task<int> GetNeighbor(int neighborCellId, byte neighborSectorId)
        {
            var count = 0;
            var neighbors = _neighborService.QueryNeighbors(neighborCellId, neighborSectorId);
            foreach (var neighbor in neighbors)
            {
                count+= await _service.UpdateNeighbors(neighbor.NearestCellId, neighbor.SectorId);
            }
            return count;
        }

        [HttpGet]
        [ApiDoc("查询指定时间段和小区的干扰记录")]
        [ApiParameterDoc("cellId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("邻区干扰记录列表")]
        public IEnumerable<InterferenceMatrixView> Get(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            return _service.QueryViews(begin, end, cellId, sectorId);
        }
    }

    [ApiControl("被干扰小区查询控制器")]
    public class InterferenceVictimController : ApiController
    {
        private readonly InterferenceNeighborService _service;

        public InterferenceVictimController(InterferenceNeighborService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定时间段和小区的被干扰小区记录")]
        [ApiParameterDoc("cellId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("被干扰小区记录列表")]
        public IEnumerable<InterferenceVictimView> Get(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            return _service.QueryVictimViews(begin, end, cellId, sectorId);
        }
    }
}
