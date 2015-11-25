using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;

namespace LtePlatform.Controllers.College
{
    public class CollegeCellsController : ApiController
    {
        private readonly CollegeCellsService _service;

        public CollegeCellsController(CollegeCellsService service)
        {
            _service = service;
        }

        public IEnumerable<CellView> Get(string collegeName)
        {
            return _service.GetViews(collegeName);
        }
    }
}
