using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.Parameters.Entities.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("从MongoDB数据库中查询小区基本信息（不限于华为）")]
    public class CellHuaweiMongoController : ApiController
    {
        private readonly CellHuaweiMongoService _service;

        public CellHuaweiMongoController(CellHuaweiMongoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定小区的基本信息")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("指定小区的基本信息")]
        public CellHuaweiMongo Get(int eNodebId, byte sectorId)
        {
            return _service.QueryRecentCellInfo(eNodebId, sectorId);
        }

        [HttpGet]
        [ApiDoc("查询指定基站下带的华为本地小区标识定义")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("指定基站下带的华为本地小区标识定义")]
        public HuaweiLocalCellDef Get(int eNodebId)
        {
            return _service.QueryLocalCellDef(eNodebId);
        }

    }
}
