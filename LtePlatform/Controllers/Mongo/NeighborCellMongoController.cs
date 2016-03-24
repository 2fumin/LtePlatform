using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using Lte.Evaluations.ViewModels.Mr;

namespace LtePlatform.Controllers.Mongo
{
    public class NeighborCellMongoController : ApiController
    {
        private readonly NeighborCellMongoService _service;

        public NeighborCellMongoController(NeighborCellMongoService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<NeighborCellMongo> Get(int eNodebId, byte sectorId)
        {
            return _service.QueryNeighbors(eNodebId, sectorId);
        }
    }
}
