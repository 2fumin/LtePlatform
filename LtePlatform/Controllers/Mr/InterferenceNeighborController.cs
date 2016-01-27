using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;

namespace LtePlatform.Controllers.Mr
{
    public class InterferenceNeighborController : ApiController
    {
        private readonly InterferenceNeighborService _service;

        public InterferenceNeighborController(InterferenceNeighborService service)
        {
            _service = service;
        }

        [HttpGet]
        public int Get(int cellId, byte sectorId)
        {
            return _service.UpdateNeighbors(cellId, sectorId);
        }
    }
}
