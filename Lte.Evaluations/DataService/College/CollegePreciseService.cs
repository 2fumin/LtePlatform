using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService.College
{
    public class CollegePreciseService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly ICellRepository _cellRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IPreciseCoverage4GRepository _kpiRepository;

        public CollegePreciseService(IInfrastructureRepository repository, ICellRepository cellRepository,
            IENodebRepository eNodebRepository, IPreciseCoverage4GRepository kpiRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            _eNodebRepository = eNodebRepository;
            _kpiRepository = kpiRepository;
        }

        public IEnumerable<CellPreciseKpiView> GetViews(string collegeName, DateTime begin, DateTime end)
        {
            var ids = _repository.GetCellIds(collegeName);
            var query =
                ids.Select(_cellRepository.Get).Where(cell => cell != null)
                    .Select(x => CellPreciseKpiView.ConstructView(x, _eNodebRepository)).ToList();
            foreach (var view in query)
            {
                view.UpdateKpi(_kpiRepository, begin, end);
            }
            return query;
        }
    }
}
