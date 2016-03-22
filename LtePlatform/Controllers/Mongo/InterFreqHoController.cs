using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Switch;
using Lte.Evaluations.ViewModels.Switch;

namespace LtePlatform.Controllers.Mongo
{
    public class InterFreqHoController : ApiController
    {
        private readonly InterFreqHoService _service;

        public InterFreqHoController(InterFreqHoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ENodebInterFreqHoView Get(int eNodebId)
        {
            return _service.QueryENodebHo(eNodebId);
        }

        [HttpGet]
        public List<CellInterFreqHoView> Get(int eNodebId, byte sectorId)
        {
            return _service.QueryCellHo(eNodebId, sectorId);
        }
    }
}
