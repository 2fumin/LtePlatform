using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService
{
    public class InterferenceNeighborService
    {
        private readonly IInterferenceMatrixRepository _repository;
        private readonly INearestPciCellRepository _neighborRepository;

        public InterferenceNeighborService(IInterferenceMatrixRepository repository,
            INearestPciCellRepository neighboRepository)
        {
            _repository = repository;
            _neighborRepository = neighboRepository;
        }

        public int UpdateNeighbors(int cellId, byte sectorId)
        {
            var neighborList = _neighborRepository.GetAllList(cellId, sectorId);
            foreach (var cell in neighborList)
            {
                _repository.UpdateItems(cellId, sectorId, cell.Pci, cell.NearestCellId, cell.NearestSectorId);
            }
            return _repository.SaveChanges();
        }
    }
}
