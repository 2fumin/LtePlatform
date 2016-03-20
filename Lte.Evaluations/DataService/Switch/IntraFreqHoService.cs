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

        public ENodebIntraFreqHoView QueryENodebHo(int eNodebId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            if (eNodeb.Factory == "华为")
            {
                var huaweiPara = _huaweiENodebHoRepository.GetRecent(eNodebId);
                return huaweiPara == null ? null : Mapper.Map<IntraRatHoComm, ENodebIntraFreqHoView>(huaweiPara);
            }
            if (UeEUtranMeasurementZte.IntraFreqHoConfigId < 0)
            {
                var zteGroup = _zteGroupRepository.GetRecent(eNodebId);
                UeEUtranMeasurementZte.IntraFreqHoConfigId = zteGroup == null ? 50 : int.Parse(zteGroup.intraFHOMeasCfg.Split(',')[0]);
            }
            
            var ztePara = _zteMeasurementRepository.GetRecent(eNodebId, UeEUtranMeasurementZte.IntraFreqHoConfigId);
            return ztePara == null ? null : Mapper.Map<UeEUtranMeasurementZte, ENodebIntraFreqHoView>(ztePara);
        }

        public CellIntraFreqHoView QueryCellHo(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            if (eNodeb.Factory == "华为")
            {
                var huaweiCell = _huaweiCellRepository.GetRecent(eNodebId, sectorId);
                var localCellId = huaweiCell?.LocalCellId ?? sectorId;
                var huaweiPara = _huaweiCellHoRepository.GetRecent(eNodebId, localCellId);
                return huaweiPara == null ? null : Mapper.Map<IntraFreqHoGroup, CellIntraFreqHoView>(huaweiPara);
            }
            var zteGroup = _zteGroupRepository.GetRecent(eNodebId);
            var configId = zteGroup == null ? 50 : int.Parse(zteGroup.intraFHOMeasCfg.Split(',')[0]);
            var ztePara = _zteMeasurementRepository.GetRecent(eNodebId, configId);
            return ztePara == null ? null : Mapper.Map<UeEUtranMeasurementZte, CellIntraFreqHoView>(ztePara);
        }
    }
}
