using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("镇区信息查询控制器")]
    public class TownController : ApiController
    {
        private readonly ENodebQueryService _service;

        public TownController(ENodebQueryService service)
        {
            _service = service;
        }

        [ApiDoc("根据LTE基站编号查询基站镇区信息")]
        [ApiParameterDoc("eNodebId", "LTE基站编号")]
        [ApiResponse("基站镇区信息，包括城市、区域、镇")]
        public Tuple<string, string, string> Get(int eNodebId)
        {
            return _service.GetTownNamesByENodebId(eNodebId);
        }
    }
}
