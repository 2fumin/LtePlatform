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
    public class IntraFreqHoController : ApiController
    {
        private readonly IntraFreqHoService _service;

        public IntraFreqHoController(IntraFreqHoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ENodebIntraFreqHoView Get(int eNodebId)
        {
            return _service.QueryENodebHo(eNodebId);
        }

        [HttpGet]
        public CellIntraFreqHoView Get(int eNodebId, byte sectorId)
        {
            return _service.QueryCellHo(eNodebId, sectorId);
        }
    }
}
