using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.DataService.Switch;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Neighbor;
using Lte.Parameters.Entities.Neighbor;

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

        private IMongoQuery<List<NeighborCellMongo>> ConstructNeighborQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? (IMongoQuery<List<NeighborCellMongo>>)
                    new HuaweiNeighborQuery(_cellRepository, _eNodebRepository, _huaweiCellRepository,
                        _huaweiNeighborRepository, eNodebId, sectorId)
                : new ZteNeighborQuery(_cellRepository, _zteNeighborRepository, _zteExternalRepository, eNodebId,
                    sectorId);
        }

        public List<NeighborCellMongo> QueryNeighbors(int eNodebId, byte sectorId)
        {
            var query = ConstructNeighborQuery(eNodebId, sectorId);
            return query?.Query();
        }

        public List<ExternalEUtranCellFDDZte> QueryExternalCells(int eNodebId)
        {
            return _zteExternalRepository.GetRecentList(eNodebId);
        }
    }

    internal class HuaweiNeighborQuery : IMongoQuery<List<NeighborCellMongo>>
    {
        private readonly ICellRepository _cellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellHuaweiMongoRepository _huaweiCellRepository;
        private readonly IEutranIntraFreqNCellRepository _huaweiNeighborRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public HuaweiNeighborQuery(ICellRepository cellRepository, IENodebRepository eNodebRepository,
            ICellHuaweiMongoRepository huaweiCellRepository, IEutranIntraFreqNCellRepository huaweiNeighborRepository,
            int eNodebId, byte sectorId)
        {
            _cellRepository = cellRepository;
            _eNodebRepository = eNodebRepository;
            _huaweiCellRepository = huaweiCellRepository;
            _huaweiNeighborRepository = huaweiNeighborRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public List<NeighborCellMongo> Query()
        {
            var huaweiCell = _huaweiCellRepository.GetRecent(_eNodebId, _sectorId);
            var localCellId = huaweiCell?.LocalCellId ?? _sectorId;
            var huaweiNeighbors = _huaweiNeighborRepository.GetRecentList(_eNodebId, (byte)localCellId);
            var results = Mapper.Map<List<EutranIntraFreqNCell>, List<NeighborCellMongo>>(huaweiNeighbors);
            results.ForEach(x =>
            {
                x.SectorId = _sectorId;
                var neighborCell = _cellRepository.GetBySectorId(x.NeighborCellId, x.NeighborSectorId);
                if (neighborCell != null) x.NeighborPci = neighborCell.Pci;
                var neighborENodeb = _eNodebRepository.GetByENodebId(x.NeighborCellId);
                if (neighborENodeb != null) x.NeighborCellName = neighborENodeb.Name + "-" + x.NeighborSectorId;
            });
            return results;
        }
    }

    internal class ZteNeighborQuery : IMongoQuery<List<NeighborCellMongo>>
    {
        private readonly ICellRepository _cellRepository;
        private readonly IEUtranRelationZteRepository _zteNeighborRepository;
        private readonly IExternalEUtranCellFDDZteRepository _zteExternalRepository;
        private readonly int _eNodebId;
        private readonly byte _sectorId;

        public ZteNeighborQuery(ICellRepository cellRepository, IEUtranRelationZteRepository zteNeighborRepository,
            IExternalEUtranCellFDDZteRepository zteExternalRepository, int eNodebId, byte sectorId)
        {
            _cellRepository = cellRepository;
            _zteExternalRepository = zteExternalRepository;
            _zteNeighborRepository = zteNeighborRepository;
            _eNodebId = eNodebId;
            _sectorId = sectorId;
        }

        public List<NeighborCellMongo> Query()
        {
            var relations = _zteNeighborRepository.GetRecentList(_eNodebId, _sectorId);
            var eNodebRelations = _zteNeighborRepository.GetRecentList(_eNodebId);
            var externals = _zteExternalRepository.GetRecentList(_eNodebId);
            return relations.Select(relation =>
            {
                var neighbor = Mapper.Map<EUtranRelationZte, NeighborCellMongo>(relation);
                neighbor.SectorId = _sectorId;
                neighbor.NeighborCellName = relation.userLabel;
                if (relation.refExternalEUtranCellFDD == "")
                {
                    neighbor.NeighborCellId = _eNodebId;
                    var innerRelation = eNodebRelations.FirstOrDefault(x => x.parentLDN == relation.refEUtranCellFDD);
                    if (innerRelation != null)
                    {
                        var fields = innerRelation.description.Split('=');
                        if (fields.Length > 1) neighbor.NeighborSectorId = byte.Parse(fields[1]);
                        neighbor.NeighborCellName = innerRelation.eNodeB_Name;
                        var neighborCell = _cellRepository.GetBySectorId(_eNodebId, neighbor.NeighborSectorId);
                        if (neighborCell != null) neighbor.NeighborPci = neighborCell.Pci;
                    }
                }
                else
                {
                    var external =
                        externals.FirstOrDefault(
                            x =>
                                x.description != null &&
                                x.description.Contains(relation.refExternalEUtranCellFDD.Split(',')[2]));
                    if (external != null)
                    {
                        neighbor.NeighborCellId = external.eNBId;
                        neighbor.NeighborSectorId = (byte)external.cellLocalId;
                        neighbor.NeighborPci = (short)external.pci;
                    }
                }
                return neighbor;

            }).ToList();
        }
    }
}
