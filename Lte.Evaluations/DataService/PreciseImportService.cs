using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService
{
    public class PreciseImportService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly ITownPreciseCoverage4GStatRepository _regionRepository;

        public PreciseImportService(IPreciseCoverage4GRepository repository,
            ITownPreciseCoverage4GStatRepository regionRepository)
        {
            _repository = repository;
            _regionRepository = regionRepository;
        }
    }
}
