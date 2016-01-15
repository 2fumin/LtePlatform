using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class InterferenceMatrixService
    {
        private readonly IInterferenceMatrixRepository _repository;
        private readonly ICellRepository _cellRepository;

        private static Stack<InterferenceMatrixStat> InterferenceMatrixStats { get; set; }

        public InterferenceMatrixService(IInterferenceMatrixRepository repository, ICellRepository cellRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            if (InterferenceMatrixStats == null)
                InterferenceMatrixStats = new Stack<InterferenceMatrixStat>();
        }
    }
}
