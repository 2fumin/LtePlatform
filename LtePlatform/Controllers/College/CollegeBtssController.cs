using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels;

namespace LtePlatform.Controllers.College
{
    public class CollegeBtssController : ApiController
    {
        private readonly CollegeBtssService _service;

        public CollegeBtssController(CollegeBtssService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<CdmaBtsView> Get(string collegeName)
        {
            return _service.QueryCollegeBtss(collegeName);
        } 
    }
}
