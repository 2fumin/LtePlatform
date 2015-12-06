using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LtePlatform.Areas.TestPage.Models;

namespace LtePlatform.Areas.TestPage.Controllers
{
    public class SimpleTypeTestSumController : ApiController
    {
        [HttpPost]
        public int Post(SimpleTypeClass item)
        {
            return item.A + item.B;
        }
    }

    public class SimpleTypeContainerController : ApiController
    {
        [HttpPost]
        public int Post(SimpleTypeContainer container)
        {
            return container.SimpleTypeClass.A + container.SimpleTypeClass.B;
        }
    }

    public class ListTypeContainerController : ApiController
    {
        [HttpPost]
        public int Post(ListTypeContainer container)
        {
            return container.List.Sum(x => x.A) + container.List.Sum(x => x.B);
        }
    }
}
