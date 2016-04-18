using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;

namespace LtePlatform.Controllers.Mr
{
    public class CellStasticController : ApiController
    {
        private readonly CellStasticService _service;

        public CellStasticController(CellStasticService service)
        {
            _service = service;
        }

        [HttpGet]
        public CellStasticView Get(int eNodebId, short pci, DateTime begin, DateTime end)
        {
            return _service.QueryDateSpanAverageStat(eNodebId, pci, begin, end);
        }
    }
}
