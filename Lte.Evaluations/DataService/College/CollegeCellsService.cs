using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;

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

        public IEnumerable<SectorView> QuerySectors(IEnumerable<string> collegeNames)
        {
            var ids = collegeNames.Select(x => _repository.GetCellIds(x)).Aggregate((x, y) => x.Concat(y)).Distinct();
            var query = ids.Select(_cellRepository.Get).Where(cell => cell != null).ToList();
            return query.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    query.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : null;
        } 
    }
}
