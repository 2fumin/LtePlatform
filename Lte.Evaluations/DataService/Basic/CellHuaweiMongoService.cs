using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellHuaweiMongoService
    {
        private readonly ICellHuaweiMongoRepository _repository;

        public CellHuaweiMongoService(ICellHuaweiMongoRepository repository)
        {
            _repository = repository;
        }

        public CellHuaweiMongo QueryRecentCellInfo(int eNodebId, byte sectorId)
        {
            return _repository.GetRecent(eNodebId, sectorId);
        }
    }
}
