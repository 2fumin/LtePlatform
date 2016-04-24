using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.DataService.Switch;
using Lte.Evaluations.ViewModels.Channel;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Neighbor;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellPowerService
    {
        private readonly IEUtranCellFDDZteRepository _ztePbRepository;
        private readonly IPowerControlDLZteRepository _ztePaRepository;
        private readonly IPDSCHCfgRepository _huaweiPbRepository;
        private readonly ICellDlpcPdschPaRepository _huaweiPaRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IENodebRepository _eNodebRepository;

        public CellPowerService(IEUtranCellFDDZteRepository ztePbRepository,
            IPowerControlDLZteRepository ztePaRepository, IPDSCHCfgRepository huaweiPbRepository,
            ICellDlpcPdschPaRepository huaweiPaRepository, ICellHuaweiMongoRepository huaweiCellRepository,
            IENodebRepository eNodebRepository)
        {
            _ztePbRepository = ztePbRepository;
            _ztePaRepository = ztePaRepository;
            _huaweiPbRepository = huaweiPbRepository;
            _huaweiPaRepository = huaweiPaRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
        }

        private IMongoQuery<CellPower> ConstructQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<CellPower>)
                    new HuaweiCellPowerQuery(_huaweiCellRepository, _huaweiPbRepository, _huaweiPaRepository, eNodebId,
                        sectorId)
                : new ZteCellPowerQuery(_ztePbRepository, _ztePaRepository, eNodebId, sectorId);
        }

        public CellPower Query(int eNodebId, byte sectorId)
        {
            var query = ConstructQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }

    internal class HuaweiCellPowerQuery : IMongoQuery<CellPower>
    {
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IPDSCHCfgRepository _huaweiPbRepository;
        private readonly ICellDlpcPdschPaRepository _huaweiPaRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public HuaweiCellPowerQuery(ICellHuaweiMongoRepository huaweiCellRepository,
            IPDSCHCfgRepository huaweiPbRepository, ICellDlpcPdschPaRepository huaweiPaRepository, int eNodebId,
            byte sectorId)
        {
            _huaweiCellRepository = huaweiCellRepository;
            _huaweiPbRepository = huaweiPbRepository;
            _huaweiPaRepository = huaweiPaRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellPower Query()
        {
            var huaweiCell = _huaweiCellRepository.GetRecent(_eNodebId, _sectorId);
            var localCellId = huaweiCell?.LocalCellId ?? _sectorId;
            var pbCfg = _huaweiPbRepository.GetRecent(_eNodebId, localCellId);
            var paCfg = _huaweiPaRepository.GetRecent(_eNodebId, localCellId);
            return pbCfg == null || paCfg == null ? null : new CellPower(pbCfg, paCfg) {SectorId = _sectorId};
        }
    }

    internal class ZteCellPowerQuery : IMongoQuery<CellPower>
    {
        private readonly IEUtranCellFDDZteRepository _ztePbRepository;
        private readonly IPowerControlDLZteRepository _ztePaRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteCellPowerQuery(IEUtranCellFDDZteRepository ztePbRepository,
            IPowerControlDLZteRepository ztePaRepository, int eNodebId, byte sectorId)
        {
            _ztePbRepository = ztePbRepository;
            _ztePaRepository = ztePaRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellPower Query()
        {
            var pbCfg = _ztePbRepository.GetRecent(_eNodebId, _sectorId);
            var paCfg = _ztePaRepository.GetRecent(_eNodebId, _sectorId);
            return pbCfg == null || paCfg == null ? null : new CellPower(pbCfg, paCfg) { SectorId = _sectorId };
        }
    }
}
