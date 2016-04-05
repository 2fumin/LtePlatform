using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Parameters.Entities.Basic;

namespace LtePlatform.Controllers.Mongo
{
    public class CellHuaweiMongoController : ApiController
    {
        private readonly CellHuaweiMongoService _service;

        public CellHuaweiMongoController(CellHuaweiMongoService service)
        {
            _service = service;
        }

        [HttpGet]
        public CellHuaweiMongo Get(int eNodebId, byte sectorId)
        {
            return _service.QueryRecentCellInfo(eNodebId, sectorId);
        }

        [HttpGet]
        public HuaweiLocalCellDef Get(int eNodebId)
        {
            return _service.QueryLocalCellDef(eNodebId);
        }

    }
}
