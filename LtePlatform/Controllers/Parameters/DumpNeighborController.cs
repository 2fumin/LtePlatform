using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("导入邻区信息处理器")]
    public class DumpNeighborController : ApiController
    {
        private readonly NearestPciCellService _service;

        public DumpNeighborController(NearestPciCellService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条邻区信息")]
        [ApiResponse("导入结果")]
        public Task<bool> Put()
        {
            return _service.DumpOneStat();
        }

        [HttpGet]
        [ApiDoc("获取当前等待导入邻区数")]
        [ApiResponse("当前等待导入邻区数")]
        public int Get()
        {
            return _service.NearestCellCount;
        }
        
        [HttpDelete]
        [ApiDoc("清除已上传邻区记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearNeighbors();
        }
    }
}
