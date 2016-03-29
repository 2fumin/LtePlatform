using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.ViewModels.Switch;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
    public class InterFreqHoService
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly IIntraRatHoCommRepository _huaweiENodebHoRepository;
        private readonly IInterFreqHoGroupRepository _huaweiCellHoRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IEutranInterNFreqRepository _huaweiNFreqRepository;
        private readonly IIntraFreqHoGroupRepository _intraFreqHoGroupRepository;

        public InterFreqHoService(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository,
            IIntraRatHoCommRepository huaweiENodebHoRepository, IInterFreqHoGroupRepository huaweiCellHoRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IENodebRepository eNodebRepository,
            IEutranInterNFreqRepository huaweiNFreqRepository, IIntraFreqHoGroupRepository intraFreqHoGroupRepository)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _huaweiENodebHoRepository = huaweiENodebHoRepository;
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
            _huaweiNFreqRepository = huaweiNFreqRepository;
            _intraFreqHoGroupRepository = intraFreqHoGroupRepository;
        }

        private IMongoQuery<ENodebInterFreqHoView> ConstructENodebQuery(int eNodebId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<ENodebInterFreqHoView>)new HuaweiInterFreqENodebQuery(_huaweiENodebHoRepository, eNodebId)
                : new ZteInterFreqENodebQuery(_zteMeasurementRepository, _zteGroupRepository, eNodebId);
        }

        public ENodebInterFreqHoView QueryENodebHo(int eNodebId)
        {
            var query = ConstructENodebQuery(eNodebId);
            return query?.Query();
        }

        private IMongoQuery<List<CellInterFreqHoView>> ConstructCellQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<List<CellInterFreqHoView>>)
                    new HuaweiInterFreqCellQuery(_huaweiCellHoRepository, _huaweiCellRepository, _huaweiNFreqRepository,
                        _intraFreqHoGroupRepository, eNodebId, sectorId)
                : new ZteInterFreqCellQuery(_zteMeasurementRepository, _zteGroupRepository, eNodebId, sectorId);
        }

        public List<CellInterFreqHoView> QueryCellHo(int eNodebId, byte sectorId)
        {
            var query = ConstructCellQuery(eNodebId, sectorId);
            return query?.Query();
        }
    }

    internal class HuaweiInterFreqENodebQuery : IMongoQuery<ENodebInterFreqHoView>
    {
        private readonly IIntraRatHoCommRepository _huaweiENodebHoRepository;
        private readonly int _eNodebId;

        public HuaweiInterFreqENodebQuery(IIntraRatHoCommRepository huaweiENodebHoRepository, int eNodebId)
        {
            _huaweiENodebHoRepository = huaweiENodebHoRepository;
            _eNodebId = eNodebId;
        }

        public ENodebInterFreqHoView Query()
        {
            var huaweiPara = _huaweiENodebHoRepository.GetRecent(_eNodebId);
            return huaweiPara == null ? null : Mapper.Map<IntraRatHoComm, ENodebInterFreqHoView>(huaweiPara);
        }
    }

    internal class ZteInterFreqENodebQuery : IMongoQuery<ENodebInterFreqHoView>
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly int _eNodebId;

        public ZteInterFreqENodebQuery(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, int eNodebId)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _eNodebId = eNodebId;
        }

        public ENodebInterFreqHoView Query()
        {
            var view = new ENodebInterFreqHoView
            {
                ENodebId = _eNodebId
            };
            var zteGroup = _zteGroupRepository.GetRecent(_eNodebId);
            var a1ConfigId = zteGroup == null ? 20 : int.Parse(zteGroup.closedInterFMeasCfg.Split(',')[0]);
            var a1Congig = _zteMeasurementRepository.GetRecent(_eNodebId, a1ConfigId);
            view.InterFreqHoA1TrigQuan = view.A3InterFreqHoA1TrigQuan = a1Congig?.triggerQuantity ?? 0;
            var a2ConfigId = zteGroup == null ? 30 : int.Parse(zteGroup.openInterFMeasCfg.Split(',')[0]);
            var a2Config = _zteMeasurementRepository.GetRecent(_eNodebId, a2ConfigId);
            view.InterFreqHoA1TrigQuan = view.A3InterFreqHoA2TrigQuan = a2Config?.triggerQuantity ?? 0;
            var hoEventId = zteGroup == null ? 70 : int.Parse(zteGroup.interFHOMeasCfg.Split(',')[0]);
            var hoEvent = _zteMeasurementRepository.GetRecent(_eNodebId, hoEventId);
            view.InterFreqHoA4TrigQuan = hoEvent?.triggerQuantity ?? 0;
            view.InterFreqHoA4RprtQuan = hoEvent?.reportQuantity ?? 0;
            view.InterFreqHoRprtInterval = hoEvent?.reportInterval ?? 0;
            return view;
        }
    }

    internal class HuaweiInterFreqCellQuery : IMongoQuery<List<CellInterFreqHoView>>
    {
        private readonly IInterFreqHoGroupRepository _huaweiCellHoRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IEutranInterNFreqRepository _huaweiNFreqRepository;
        private readonly IIntraFreqHoGroupRepository _intraFreqHoGroupRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public HuaweiInterFreqCellQuery(IInterFreqHoGroupRepository huaweiCellHoRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IEutranInterNFreqRepository huaweiNFreqRepository,
            IIntraFreqHoGroupRepository intraFreqHoGroupRepository, int eNodebId, byte sectorId)
        {
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _huaweiNFreqRepository = huaweiNFreqRepository;
            _intraFreqHoGroupRepository = intraFreqHoGroupRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public List<CellInterFreqHoView> Query()
        {
            var results = new List<CellInterFreqHoView>();
            var huaweiCell = _huaweiCellRepository.GetRecent(_eNodebId, _sectorId);
            var localCellId = huaweiCell?.LocalCellId ?? _sectorId;
            var nFreqs = _huaweiNFreqRepository.GetRecentList(_eNodebId, localCellId);
            var hoGroup = _huaweiCellHoRepository.GetRecent(_eNodebId, localCellId);
            if (hoGroup == null) return null;
            var intraFreqConfig = _intraFreqHoGroupRepository.GetRecent(_eNodebId, localCellId);
            foreach (var config in nFreqs.Select(freq => new CellInterFreqHoView
            {
                Earfcn = freq.DlEarfcn,
                InterFreqHoEventType = freq.InterFreqHoEventType,
                InterFreqEventA1 = Mapper.Map<InterFreqHoGroup, InterFreqEventA1>(hoGroup),
                InterFreqEventA2 = Mapper.Map<InterFreqHoGroup, InterFreqEventA2>(hoGroup),
                InterFreqEventA3 = Mapper.Map<InterFreqHoGroup, InterFreqEventA3>(hoGroup),
                InterFreqEventA4 = Mapper.Map<InterFreqHoGroup, InterFreqEventA4>(hoGroup),
                InterFreqEventA5 = Mapper.Map<InterFreqHoGroup, InterFreqEventA5>(hoGroup)
            }))
            {
                if (intraFreqConfig != null)
                {
                    config.InterFreqEventA3.Hysteresis = intraFreqConfig.IntraFreqHoA3Hyst;
                    config.InterFreqEventA3.TimeToTrigger = intraFreqConfig.IntraFreqHoA3TimeToTrig;
                }
                switch (config.InterFreqHoEventType)
                {
                    case 0:
                        config.InterFreqEventA1.ThresholdOfRsrp = hoGroup.A3InterFreqHoA1ThdRsrp;
                        config.InterFreqEventA2.ThresholdOfRsrp = hoGroup.A3InterFreqHoA2ThdRsrp;
                        config.InterFreqEventA1.ThresholdOfRsrq = hoGroup.A3InterFreqHoA1ThdRsrq;
                        config.InterFreqEventA2.ThresholdOfRsrq = hoGroup.A3InterFreqHoA2ThdRsrq;
                        break;
                    default:
                        config.InterFreqEventA1.ThresholdOfRsrp = hoGroup.InterFreqHoA1ThdRsrp;
                        config.InterFreqEventA2.ThresholdOfRsrp = hoGroup.InterFreqHoA2ThdRsrp;
                        config.InterFreqEventA1.ThresholdOfRsrq = hoGroup.InterFreqHoA1ThdRsrq;
                        config.InterFreqEventA2.ThresholdOfRsrq = hoGroup.InterFreqHoA2ThdRsrq;
                        break;
                }
                results.Add(config);
            }
            return results;
        }
    }

    internal class ZteInterFreqCellQuery : IMongoQuery<List<CellInterFreqHoView>>
    {
        private readonly IUeEUtranMeasurementRepository _zteMeasurementRepository;
        private readonly ICellMeasGroupZteRepository _zteGroupRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteInterFreqCellQuery(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository, int eNodebId, byte sectorId)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public List<CellInterFreqHoView> Query()
        {
            var zteGroup = _zteGroupRepository.GetRecent(_eNodebId);
            var view = new CellInterFreqHoView
            {
                Earfcn = 0
            };
            var configId = zteGroup == null ? 70 : int.Parse(zteGroup.interFHOMeasCfg.Split(',')[0]);
            var measurement = _zteMeasurementRepository.GetRecent(_eNodebId, configId);
            if (measurement != null)
            {
                view.InterFreqHoEventType = measurement.eventId;
                view.InterFreqEventA3 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA3>(measurement);
                view.InterFreqEventA4 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA4>(measurement);
                view.InterFreqEventA5 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA5>(measurement);
            }
            configId = zteGroup == null ? 60 : int.Parse(zteGroup.closedInterFMeasCfg.Split(',')[0]);
            measurement = _zteMeasurementRepository.GetRecent(_eNodebId, configId);
            if (measurement != null)
            {
                view.InterFreqEventA1 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA1>(measurement);
            }
            configId = zteGroup == null ? 0 : int.Parse(zteGroup.openInterFMeasCfg.Split(',')[0]);
            measurement = _zteMeasurementRepository.GetRecent(_eNodebId, configId);
            if (measurement != null)
            {
                view.InterFreqEventA2 = Mapper.Map<UeEUtranMeasurementZte, InterFreqEventA2>(measurement);
            }

            return new List<CellInterFreqHoView>
            {
                view
            };
        }
    }
}
