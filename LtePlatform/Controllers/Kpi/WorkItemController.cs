using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities.Work;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("工单查询控制器")]
    [Authorize]
    public class WorkItemController : ApiController
    {
        private readonly WorkItemService _service;

        public WorkItemController(WorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有工单列表")]
        [ApiResponse("所有工单列表")]
        public IEnumerable<WorkItemView> Get()
        {
            return _service.QueryViews();
        }

        [HttpGet]
        [ApiDoc("查询指定条件下的工单列表数量")]
        [ApiParameterDoc("statCondition", "工单状态条件")]
        [ApiParameterDoc("typeCondition", "工单类型条件")]
        [ApiResponse("总的数量")]
        [AllowAnonymous]
        public int Get(string statCondition, string typeCondition)
        {
            return _service.QueryTotalItems(statCondition, typeCondition);
        }

        [HttpGet]
        [ApiDoc("查询指定条件下某一页的工单视图列表")]
        [ApiParameterDoc("statCondition", "工单状态条件")]
        [ApiParameterDoc("typeCondition", "工单类型条件")]
        [ApiParameterDoc("itemsPerPage", "每页显示的工单数")]
        [ApiParameterDoc("page", "指定页码")]
        [ApiResponse("某一页的工单视图列表")]
        public IEnumerable<WorkItemView> Get(string statCondition, string typeCondition, int itemsPerPage,
            int page)
        {
            return _service.QueryViews(statCondition, typeCondition, itemsPerPage, page);
        }

        [HttpGet]
        [ApiDoc("查询对应基站编号的所有工单")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("对应的所有工单")]
        public async Task<IEnumerable<WorkItemView>> Get(int eNodebId)
        {
            return await _service.QueryViews(eNodebId);
        }

        [HttpGet]
        [ApiDoc("查询对应基站编号和扇区编号的所有工单")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("对应的所有工单")]
        public async Task<IEnumerable<WorkItemView>> Get(int eNodebId, byte sectorId)
        {
            return await _service.QueryViews(eNodebId, sectorId);
        }

        [HttpPut]
        [ApiDoc("更新LTE扇区编号")]
        [ApiResponse("更新扇区编号数")]
        public int Put()
        {
            return _service.UpdateLteSectorIds();
        }

        [HttpPost]
        public void Post(WorkItemFeedbackView view)
        {
            _service.FeedBack(User.Identity.Name, view.Message, view.SerialNumber);
        }
    }

    public class WorkItemCurrentMonthController : ApiController
    {
        private readonly WorkItemService _service;

        public WorkItemCurrentMonthController(WorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<Tuple<int, int, int>> Get()
        {
            return await _service.QueryTotalItemsThisMonth();
        }
    }
}
