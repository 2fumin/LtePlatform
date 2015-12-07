using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Entities;

namespace LtePlatform.Areas.TestPage.Controllers
{
    public class TestPostOriginENodebsController : ApiController
    {
        [HttpPost]
        public IEnumerable<ENodebExcel> Post(NewENodebListContainer container)
        {
            return container.Infos;
        }
    }
}
