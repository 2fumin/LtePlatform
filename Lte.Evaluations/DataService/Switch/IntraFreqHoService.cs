using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels.Switch;
using Lte.Parameters.Entities.Switch;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Abstract.Basic;
using AutoMapper;

namespace Lte.Evaluations.DataService.Switch
{
    public class IntraFreqHoService
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IIntraFreqHoGroupRepository _huaweiCellHoRepository;
        private readonly IIntraRatHoCommRepository _huaweiENodebHoRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IENodebRepository _eNodebRepository;

        public IntraFreqHoService(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository,
            IIntraFreqHoGroupRepository huaweiCellHoRepository, IIntraRatHoCommRepository huaweiENodebHoRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IENodebRepository eNodebRepository)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiENodebHoRepository = huaweiENodebHoRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
        }

        private IMongoQuery<ENodebIntraFreqHoView> ConstructENodebQuery(int eNodebId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<ENodebIntraFreqHoView>) new HuaweiENodebQuery(_huaweiENodebHoRepository, eNodebId)
                : new ZteENodebQuery(_zteGroupRepository, _zteMeasurementRepository, eNodebId);
        }

        public ENodebIntraFreqHoView QueryENodebHo(int eNodebId)
        {
            var query = ConstructENodebQuery(eNodebId);
            return query?.Query();
        }

        private IMongoQuery<CellIntraFreqHoView> ConstructCellQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<CellIntraFreqHoView>)
                    new HuaweiCellQuery(_huaweiCellRepository, _huaweiCellHoRepository, eNodebId, sectorId)
                : new ZteCellQuery(_zteMeasurementRepository, _zteGroupRepository, eNodebId, sectorId);
        }

        public CellIntraFreqHoView QueryCellHo(int eNodebId, byte sectorId)
        {
            var query = ConstructCellQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }

    internal class HuaweiENodebQuery : IMongoQuery<ENodebIntraFreqHoView>
    {
        private readonly IIntraRatHoCommRepository _huaweiENodebHoRepository;
        private readonly int _eNodebId;

        public HuaweiENodebQuery(IIntraRatHoCommRepository huaweiENodebHoRepository, int eNodebId)
        {
            _huaweiENodebHoRepository = huaweiENodebHoRepository;
            _eNodebId = eNodebId;
        }

        public ENodebIntraFreqHoView Query()
        {
            var huaweiPara = _huaweiENodebHoRepository.GetRecent(_eNodebId);
            return huaweiPara == null ? null : Mapper.Map<IntraRatHoComm, ENodebIntraFreqHoView>(huaweiPara);
        }
    }

    internal class ZteENodebQuery : IMongoQuery<ENodebIntraFreqHoView>
    {
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly int _eNodebId;

        public ZteENodebQuery(ICellMeasGroupZteRepository zteGroupRepository,
            IUeEUtranMeasurementRepository zteMeasurementRepository, int eNodebId)
        {
            _zteGroupRepository = zteGroupRepository;
            _zteMeasurementRepository = zteMeasurementRepository;
            _eNodebId = eNodebId;
        }

        public ENodebIntraFreqHoView Query()
        {
            if (UeEUtranMeasurementZte.IntraFreqHoConfigId < 0)
            {
                var zteGroup = _zteGroupRepository.GetRecent(_eNodebId);
                UeEUtranMeasurementZte.IntraFreqHoConfigId = zteGroup == null
                    ? 50
                    : int.Parse(zteGroup.intraFHOMeasCfg.Split(',')[0]);
            }

            var ztePara = _zteMeasurementRepository.GetRecent(_eNodebId, UeEUtranMeasurementZte.IntraFreqHoConfigId);
            return ztePara == null ? null : Mapper.Map<UeEUtranMeasurementZte, ENodebIntraFreqHoView>(ztePara);
        }
    }

    internal class HuaweiCellQuery : IMongoQuery<CellIntraFreqHoView>
    {
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IIntraFreqHoGroupRepository _huaweiCellHoRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public HuaweiCellQuery(ICellHuaweiMongoRepository huaweiCellRepository,
            IIntraFreqHoGroupRepository huaweiCellHoRepository, int eNodebId, byte sectorId)
        {
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellIntraFreqHoView Query()
        {
            var huaweiCell = _huaweiCellRepository.GetRecent(_eNodebId, _sectorId);
            var localCellId = huaweiCell?.LocalCellId ?? _sectorId;
            var huaweiPara = _huaweiCellHoRepository.GetRecent(_eNodebId, localCellId);
            return huaweiPara == null ? null : Mapper.Map<IntraFreqHoGroup, CellIntraFreqHoView>(huaweiPara);
        }
    }

    internal class ZteCellQuery : IMongoQuery<CellIntraFreqHoView>
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteCellQuery(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, int eNodebId, byte sectorId)
        {
            _zteGroupRepository = zteGroupRepository;
            _zteMeasurementRepository = zteMeasurementRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public CellIntraFreqHoView Query()
        {
            var zteGroup = _zteGroupRepository.GetRecent(_eNodebId);
            var configId = zteGroup == null ? 50 : int.Parse(zteGroup.intraFHOMeasCfg.Split(',')[0]);
            var ztePara = _zteMeasurementRepository.GetRecent(_eNodebId, configId);
            return ztePara == null ? null : Mapper.Map<UeEUtranMeasurementZte, CellIntraFreqHoView>(ztePara);
        }
    }

}
