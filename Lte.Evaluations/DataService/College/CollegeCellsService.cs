using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeCellsService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly ICellRepository _cellRepository;
        private readonly IENodebRepository _eNodebRepository;

        public CollegeCellsService(IInfrastructureRepository repository, ICellRepository cellRepository,
            IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            _eNodebRepository = eNodebRepository;
        }

        public IEnumerable<CellView> GetViews(string collegeName)
        {
            var ids = _repository.GetCellIds(collegeName);
            var query = ids.Select(_cellRepository.Get).Where(cell => cell != null).ToList();
            return query.Any()
                ? query.Select(x => CellView.ConstructView(x, _eNodebRepository))
                : null;
        }

        public IEnumerable<SectorView> QuerySectors(string collegeName)
        {
            var ids = _repository.GetCellIds(collegeName);
            var query = ids.Select(_cellRepository.Get).Where(cell => cell != null).ToList();
            return query.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    query.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : null;
        }
    }
}
