using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class CsvFileInfoService
    {
        private static ICsvFileInfoRepository _repository;

        public CsvFileInfoService(ICsvFileInfoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CsvFilesInfo> QueryAllList()
        {
            return _repository.CsvFilesInfos.ToList();
        } 
    }
}
