using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService
{
    public class NeighborMonitorService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly ICellRepository _cellRepository;
        private readonly INearestPciCellRepository _nearestPciCellRepository;

        public NeighborMonitorService(IInfrastructureRepository repository, ICellRepository cellRepository,
            INearestPciCellRepository nearestPciCellRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            _nearestPciCellRepository = nearestPciCellRepository;
        }
    }
}
