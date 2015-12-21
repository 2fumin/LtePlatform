using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Dt
{
    public class Record4GController : ApiController
    {
        private readonly CsvFileInfoService _service;

        public Record4GController(CsvFileInfoService service)
        {
            _service = service;
        }

        public IEnumerable<FileRecord4G> Get(string fileName)
        {
            return _service.GetFileRecord4Gs(fileName);
        }

        public IEnumerable<FileRecord4G> Get(string fileName, int rasterNum)
        {
            return _service.GetFileRecord4Gs(fileName, rasterNum);
        } 
    }
}
