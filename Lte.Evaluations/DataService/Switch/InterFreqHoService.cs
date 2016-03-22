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

        public InterFreqHoService(IUeEUtranMeasurementRepository zteMeasurementRepository,
            ICellMeasGroupZteRepository zteGroupRepository,
            IIntraRatHoCommRepository huaweiENodebHoRepository, IInterFreqHoGroupRepository huaweiCellHoRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IENodebRepository eNodebRepository)
        {
            _zteMeasurementRepository = zteMeasurementRepository;
            _zteGroupRepository = zteGroupRepository;
            _huaweiENodebHoRepository = huaweiENodebHoRepository;
            _huaweiCellHoRepository = huaweiCellHoRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _eNodebRepository = eNodebRepository;
        }

        public ENodebInterFreqHoView QueryENodebHo(int eNodebId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            if (eNodeb.Factory == "华为")
            {
                var huaweiPara = _huaweiENodebHoRepository.GetRecent(eNodebId);
                return huaweiPara == null ? null : Mapper.Map<IntraRatHoComm, ENodebInterFreqHoView>(huaweiPara);
            }
            var zteGroup = _zteGroupRepository.GetRecent(eNodebId);
            int a1ConfigRsrp = zteGroup == null ? 20 : int.Parse(zteGroup.closedInterFMeasCfg.Split(',')[0]);
        }
    }
}
