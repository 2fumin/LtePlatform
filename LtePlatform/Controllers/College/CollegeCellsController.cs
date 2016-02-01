using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询校园网LTE小区的控制器")]
    public class CollegeCellsController : ApiController
    {
        private readonly CollegeCellsService _service;

        public CollegeCellsController(CollegeCellsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询校园网LTE小区")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网LTE小区列表")]
        public IEnumerable<CellView> Get(string collegeName)
        {
            return _service.GetViews(collegeName);
        }

        [HttpPost]
        [ApiDoc("查询多个校园对应的LTE小区扇区列表（可用于地理化显示）")]
        [ApiParameterDoc("collegeNames", "校园名称列表")]
        [ApiResponse("LTE小区扇区列表（可用于地理化显示）")]
        public IEnumerable<SectorView> Post(CollegeNamesContainer collegeNames)
        {
            return _service.QuerySectors(collegeNames.Names);
        }
    }
}
