using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Mr
{
    public class NeighborMonitorService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly ICellRepository _cellRepository;

        public NeighborMonitorService(IInfrastructureRepository repository, ICellRepository cellRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
        }

        public int AddOneMonitor(int cellId, byte sectorId)
        {
            var cell = _cellRepository.GetBySectorId(cellId, sectorId);
            if (cell == null) return 0;
            var item = _repository.GetTopPreciseMonitor(cell.Id);
            if (item != null) return 0;
            _repository.Insert(new InfrastructureInfo
            {
                InfrastructureId = cell.Id,
                HotspotType = HotspotType.TopPrecise
            });
            return _repository.SaveChanges();
        }
    }
}
