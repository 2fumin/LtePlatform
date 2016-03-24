using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Neighbor;

namespace Lte.Evaluations.DataService.Mr
{
    public class NeighborCellMongoService
    {
        private readonly ICellRepository _cellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IEUtranRelationZteRepository _zteNeighborRepository;
        private readonly IEutranIntraFreqNCellRepository _huaweiNeighborRepository;
        private readonly IExternalEUtranCellFDDZteRepository _zteExternalRepository;

        public NeighborCellMongoService(ICellRepository cellRepository, IENodebRepository eNodebRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IEUtranRelationZteRepository zteNeighborRepository,
            IEutranIntraFreqNCellRepository huaweiNeighborRepository,
            IExternalEUtranCellFDDZteRepository zteExternalRepository)
        {
            _cellRepository = cellRepository;
            _eNodebRepository = eNodebRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _zteNeighborRepository = zteNeighborRepository;
            _zteExternalRepository = zteExternalRepository;
            _huaweiNeighborRepository = huaweiNeighborRepository;
        }
    }
}
