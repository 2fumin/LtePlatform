using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        public List<NeighborCellMongo> QueryNeighbors(int eNodebId, byte sectorId)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
            if (eNodeb == null) return null;
            if (eNodeb.Factory == "华为")
            {
                var huaweiCell = _huaweiCellRepository.GetRecent(eNodebId, sectorId);
                var localCellId = huaweiCell?.LocalCellId ?? sectorId;
                var huaweiNeighbors = _huaweiNeighborRepository.GetRecentList(eNodebId, (byte)localCellId);
                var results = Mapper.Map<List<EutranIntraFreqNCell>, List<NeighborCellMongo>>(huaweiNeighbors);
                results.ForEach(x =>
                {
                    x.SectorId = sectorId;
                    var neighborCell = _cellRepository.GetBySectorId(x.NeighborCellId, x.NeighborSectorId);
                    if (neighborCell != null) x.NeighborPci = neighborCell.Pci;
                    var neighborENodeb = _eNodebRepository.GetByENodebId(x.NeighborCellId);
                    if (neighborENodeb != null) x.NeighborCellName = neighborENodeb.Name + "-" + x.NeighborSectorId;
                });
                return results;
            }
            var relations = _zteNeighborRepository.GetRecentList(eNodebId, sectorId);
            var eNodebRelations = _zteNeighborRepository.GetRecentList(eNodebId);
            var externals = _zteExternalRepository.GetRecentList(eNodebId);
            return relations.Select(relation =>
            {
                var neighbor = Mapper.Map<EUtranRelationZte, NeighborCellMongo>(relation);
                neighbor.SectorId = sectorId;
                neighbor.NeighborCellName = relation.userLabel;
                if (relation.refExternalEUtranCellFDD == "")
                {
                    neighbor.NeighborCellId = eNodebId;
                    var innerRelation = eNodebRelations.FirstOrDefault(x => x.parentLDN == relation.refEUtranCellFDD);
                    if (innerRelation != null)
                    {
                        var fields = innerRelation.description.Split('=');
                        if (fields.Length > 1) neighbor.NeighborSectorId = byte.Parse(fields[1]);
                        neighbor.NeighborCellName = innerRelation.eNodeB_Name;
                        var neighborCell = _cellRepository.GetBySectorId(eNodebId, neighbor.NeighborSectorId);
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
                        neighbor.NeighborSectorId = (byte) external.cellLocalId;
                        neighbor.NeighborPci = (short) external.pci;
                    }
                }
                return neighbor;
                
            }).ToList();
        }

        public List<ExternalEUtranCellFDDZte> QueryExternalCells(int eNodebId)
        {
            return _zteExternalRepository.GetRecentList(eNodebId);
        }
    }
}
