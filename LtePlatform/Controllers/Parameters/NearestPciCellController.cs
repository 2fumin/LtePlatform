using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
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
    }
}
