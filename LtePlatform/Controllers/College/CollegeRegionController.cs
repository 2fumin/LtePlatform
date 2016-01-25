using System.Web.Http;
using Lte.Parameters.Abstract.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网区域查询控制器")]
    public class CollegeRegionController : ApiController
    {
        private readonly ICollegeRepository _repository;

        public CollegeRegionController(ICollegeRepository repository)
        {
            _repository = repository;
        }

        [ApiDoc("查询校园网区域信息，包括面积和边界坐标")]
        [ApiParameterDoc("id", "校园ID")]
        [ApiResponse("校园网区域信息，包括面积和边界坐标")]
        public IHttpActionResult Get(int id)
        {
            var region = _repository.GetRegion(id);
            return region == null 
                ? (IHttpActionResult)BadRequest("College Id Not Found Or without region!") 
                : Ok(region);
        }
        
        [ApiDoc("查询校园网区域范围")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网区域信息，包括东南西北的坐标点")]
        public IHttpActionResult Get(string collegeName)
        {
            var range = _repository.GetRange(collegeName);
            return range == null ? (IHttpActionResult) BadRequest("College Not Found Or without region!") : Ok(range);
        }
    }
}
