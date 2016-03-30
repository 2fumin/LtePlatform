using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellHuaweiMongoService
    {
        private readonly ICellHuaweiMongoRepository _repository;
        private readonly IEUtranCellFDDZteRepository _zteCellRepository;

        public CellHuaweiMongoService(ICellHuaweiMongoRepository repository, 
            IEUtranCellFDDZteRepository zteCellRepository)
        {
            _repository = repository;
            _zteCellRepository = zteCellRepository;
        }

        public CellHuaweiMongo QueryRecentCellInfo(int eNodebId, byte sectorId)
        {
            return _repository.GetRecent(eNodebId, sectorId);
        }
    }
}
