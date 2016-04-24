using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels.Channel;

namespace LtePlatform.Controllers.Mongo
{
    public class CellPowerController : ApiController
    {
        private readonly CellPowerService _service;

        public CellPowerController(CellPowerService service)
        {
            _service = service;
        }

        [HttpGet]
        public CellPower Get(int eNodebId, byte sectorId)
        {
            return _service.Query(eNodebId, sectorId);
        }
    }
}
