using Lte.Evaluations.DataService.Switch;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellHuaweiMongoService
    {
        private readonly ICellHuaweiMongoRepository _repository;
        private readonly IEUtranCellFDDZteRepository _zteCellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteMeasRepository;

        public CellHuaweiMongoService(ICellHuaweiMongoRepository repository, 
            IEUtranCellFDDZteRepository zteCellRepository, IENodebRepository eNodebRepository,
            IEUtranCellMeasurementZteRepository zteMeasRepository)
        {
            _repository = repository;
            _zteCellRepository = zteCellRepository;
            _eNodebRepository = eNodebRepository;
            _zteMeasRepository = zteMeasRepository;
        }

        private IMongoQuery<CellHuaweiMongo> ConstructQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<CellHuaweiMongo>) new HuaweiCellQuery(_repository, eNodebId, sectorId)
                : new ZteCellQuery(_zteCellRepository, _zteMeasRepository, eNodebId, sectorId);
        }

        public CellHuaweiMongo QueryRecentCellInfo(int eNodebId, byte sectorId)
        {
            var query = ConstructQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }

    internal class HuaweiCellQuery : IMongoQuery<CellHuaweiMongo>
    {
        private readonly ICellHuaweiMongoRepository _repository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public HuaweiCellQuery(ICellHuaweiMongoRepository repository, int eNodebId, byte sectorId)
        {
            _repository = repository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellHuaweiMongo Query()
        {
            return _repository.GetRecent(_eNodebId, _sectorId);
        }
    }

    internal class ZteCellQuery : IMongoQuery<CellHuaweiMongo>
    {
        private readonly IEUtranCellFDDZteRepository _zteCellRepository;
        private readonly IEUtranCellMeasurementZteRepository _zteMeasRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteCellQuery(IEUtranCellFDDZteRepository zteCellRepository,
            IEUtranCellMeasurementZteRepository zteMeasRepository, int eNodebId, byte sectorId)
        {
            _zteCellRepository = zteCellRepository;
            _zteMeasRepository = zteMeasRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellHuaweiMongo Query()
        {
            var zteCell = _zteCellRepository.GetRecent(_eNodebId, _sectorId);
            var zteMeas = _zteMeasRepository.GetRecent(_eNodebId, _sectorId);
            return new CellHuaweiMongo
            {
                PhyCellId = zteCell?.pci ?? 0,
                CellSpecificOffset = zteCell?.ocs ?? 15,
                QoffsetFreq = zteMeas?.eutranMeasParas_offsetFreq ?? 15
            };
        }
    }
}
