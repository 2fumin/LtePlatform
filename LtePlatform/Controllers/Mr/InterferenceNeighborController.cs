using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("干扰邻区查询控制器")]
    [Authorize]
    public class InterferenceNeighborController : ApiController
    {
        private readonly InterferenceNeighborService _service;

        public InterferenceNeighborController(InterferenceNeighborService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("更新指定小区的邻区干扰记录")]
        [ApiParameterDoc("cellId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("更新结果")]
        public int Get(int cellId, byte sectorId)
        {
            return _service.UpdateNeighbors(cellId, sectorId);
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
}
