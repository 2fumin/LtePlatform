using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("TOP连接失败记录查询的控制器")]
    public class TopConnection3GController : ApiController
    {
        private readonly TopConnection3GService _service;

        public TopConnection3GController(TopConnection3GService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("指定日期和城市，查询单个日期的TOP连接失败记录，如果指定日期没有记录，则会匹配之前最近一天的记录")]
        [ApiParameterDoc("statDate", "日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("单个日期的TOP连接失败记录，记录的日期可能比指定的日期要前")]
        public TopConnection3GDateView Get(DateTime statDate, string city)
        {
            return _service.GetDateView(statDate, city);
        }

        [HttpGet]
        [ApiDoc("指定日期区间和城市，查询该日期区间的TOP连接失败记录集合")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiResponse("日期区间的TOP连接失败记录集合")]
        public IEnumerable<TopConnection3GTrendView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.GetTrendViews(begin, end, city);
        }

        [HttpGet]
        [ApiDoc("指定日期区间、城市、排序标准以及TOP个数，查询该日期区间的TOP连接失败记录集合")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("policy", "排序标准")]
        [ApiParameterDoc("topCount", "TOP个数")]
        [ApiResponse("日期区间的TOP连接失败记录集合")]
        public IEnumerable<TopConnection3GTrendView> Get(DateTime begin, DateTime end, string city,
            string policy, int topCount)
        {
            return _service.GetTrendViews(begin, end, city).Order(policy.GetTopConnection3GPolicy(), topCount);
        }

        [HttpGet]
        [ApiDoc("获取TOP连接失败排序标准的所有选项列表")]
        [ApiResponse("TOP连接失败排序标准的所有选项列表")]
        public IEnumerable<string> Get()
        {
            return OrderTopConnection3GService.OrderSelectionList.Select(x => x.Key);
        }
    }
}
