using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService.Mr
{
    public class InterferenceNeighborService
    {
        private readonly IInterferenceMatrixRepository _repository;
        private readonly INearestPciCellRepository _neighborRepository;
        private readonly IENodebRepository _eNodebRepository;

        public InterferenceNeighborService(IInterferenceMatrixRepository repository,
            INearestPciCellRepository neighboRepository, IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _neighborRepository = neighboRepository;
            _eNodebRepository = eNodebRepository;
        }

        public int UpdateNeighbors(int cellId, byte sectorId)
        {
            var neighborList = _neighborRepository.GetAllList(cellId, sectorId);
            foreach (var cell in neighborList)
            {
                _repository.UpdateItems(cellId, sectorId, cell.Pci, cell.NearestCellId, cell.NearestSectorId);
            }
            return _repository.SaveChanges();
        }

        public IEnumerable<InterferenceMatrixView> QueryViews(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            var statList = _repository.GetAllList(begin, end, cellId, sectorId);
            var results = from stat in statList
                group stat by new {stat.ENodebId, stat.SectorId, stat.DestPci, stat.DestENodebId, stat.DestSectorId}
                into g
                select new InterferenceMatrixView
                {
                    DestPci = g.Key.DestPci,
                    DestENodebId = g.Key.DestENodebId,
                    DestSectorId = g.Key.DestSectorId,
                    Mod3Interferences = g.Average(x => x.Mod3Interferences),
                    Mod6Interferences = g.Average(x => x.Mod6Interferences),
                    OverInterferences10Db = g.Average(x => x.OverInterferences10Db),
                    OverInterferences6Db = g.Average(x => x.OverInterferences6Db),
                    InterferenceLevel = g.Average(x => x.InterferenceLevel),
                    NeighborCellName = "未匹配小区"
                };
            var views = results as InterferenceMatrixView[] ?? results.ToArray();
            foreach (var result in views.Where(x=>x.DestENodebId>0))
            {
                var eNodeb = _eNodebRepository.GetByENodebId(result.DestENodebId);
                result.NeighborCellName = eNodeb?.Name + "-" + result.DestSectorId;
            }
            return views;
        } 
    }
}
