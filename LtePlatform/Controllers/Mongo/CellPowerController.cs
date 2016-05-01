using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels.Channel;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("查询小区功率参数控制器")]
    public class CellPowerController : ApiController
    {
        private readonly CellPowerService _service;

        public CellPowerController(CellPowerService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定小区的功率参数")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("指定小区的功率参数，包括Pa/Pb和Rs功率参数")]
        public CellPower Get(int eNodebId, byte sectorId)
        {
            return _service.Query(eNodebId, sectorId);
        }
    }
}
