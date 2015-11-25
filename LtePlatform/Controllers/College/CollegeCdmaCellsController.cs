using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.College
{
    public class CollegeCdmaCellsController : ApiController
    {
        private readonly CollegeCdmaCellsService _service;

        public CollegeCdmaCellsController(CollegeCdmaCellsService service)
        {
            _service = service;
        }

        public IEnumerable<CdmaCellView> Get(string collegeName)
        {
            return _service.GetViews(collegeName);
        }
    }
}
