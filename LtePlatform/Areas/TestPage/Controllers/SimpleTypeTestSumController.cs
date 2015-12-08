using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LtePlatform.Areas.TestPage.Models;
using LtePlatform.Models;

namespace LtePlatform.Areas.TestPage.Controllers
{
    [ApiControl("简单类型控制器，仅供测试用")]
    public class SimpleTypeTestSumController : ApiController
    {
        [HttpPost]
        [ApiDoc("上传简单类型")]
        [ApiParameterDoc("item", "类型对象")]
        [ApiResponse("对象内所有元素总和")]
        public int Post(SimpleTypeClass item)
        {
            return item.A + item.B;
        }
    }

    [ApiControl("简单类型容器控制器，仅供测试用")]
    public class SimpleTypeContainerController : ApiController
    {
        [HttpPost]
        [ApiDoc("上传简单类型容器")]
        [ApiParameterDoc("container", "类型容器")]
        [ApiResponse("容器内所有元素总和")]
        public int Post(SimpleTypeContainer container)
        {
            return container.SimpleTypeClass.A + container.SimpleTypeClass.B;
        }
    }

    [ApiControl("列表类型容器控制器，仅供测试用")]
    public class ListTypeContainerController : ApiController
    {
        [HttpPost]
        [ApiDoc("上传列表容器")]
        [ApiParameterDoc("container", "列表容器")]
        [ApiResponse("容器内所有元素总和")]
        public int Post(ListTypeContainer container)
        {
            return container.List.Sum(x => x.A) + container.List.Sum(x => x.B);
        }
    }
}
