using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class AreaTestDateService
    {
        private readonly IAreaTestDateRepository _repository;

        public AreaTestDateService(IAreaTestDateRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AreaTestDate> QueryAllList()
        {
            return _repository.AreaTestDates.ToList();
        }
    }
}
