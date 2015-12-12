using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("LTE小区有关的控制器")]
    public class CellController : ApiController
    {
        private readonly CellService _service;

        public CellController(CellService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获取经纬度范围内的小区列表")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的小区列表")]
        public IEnumerable<Cell> Get(double west, double east, double south, double north)
        {
            return _service.GetCells(west, east, south, north);
        }

        [HttpGet]
        [ApiDoc("给定基站名对应的小区扇区编号列表")]
        [ApiParameterDoc("eNodebName", "基站名")]
        [ApiResponse("对应的小区扇区编号列表，如果找不到则会返回错误")]
        public IHttpActionResult Get(string eNodebName)
        {
            var query = _service.GetSectorIds(eNodebName);
            return query == null ? (IHttpActionResult)BadRequest("Wrong ENodeb Name!") : Ok(query);
        }

        [HttpGet]
        [ApiDoc("给定基站编号对定的扇区视图对象列表")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("对定的扇区视图对象列表")]
        public IEnumerable<SectorView> Get(int eNodebId)
        {
            return _service.QuerySectors(eNodebId);
        }

        [HttpPost]
        [ApiDoc("将Top精确覆盖率4G小区视图列表转化为便于地理化显示的Top精确覆盖率4G扇区列表")]
        [ApiParameterDoc("container", "Top精确覆盖率4G小区视图列表")]
        [ApiResponse("Top精确覆盖率4G扇区列表")]
        public IEnumerable<Precise4GSector> Post(TopPreciseViewContainer container)
        {
            return _service.QuerySectors(container);
        }
    }
}
