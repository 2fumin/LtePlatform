using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeCdmaCellsService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly ICdmaCellRepository _cellRepository;
        private readonly IBtsRepository _btsRepository;

        public CollegeCdmaCellsService(IInfrastructureRepository repository, ICdmaCellRepository cellRepository,
            IBtsRepository btsRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            _btsRepository = btsRepository;
        }

        public IEnumerable<CdmaCellView> GetViews(string collegeName)
        {
            var ids = _repository.GetCdmaCellIds(collegeName);
            var query = ids.Select(_cellRepository.Get).Where(cell => cell != null).ToList();
            return query.Any()
                ? query.Select(x => CdmaCellView.ConstructView(x, _btsRepository))
                : null;
        }

        public IEnumerable<SectorView> QuerySectors(string collegeName)
        {
            var ids = _repository.GetCdmaCellIds(collegeName);
            var query = ids.Select(_cellRepository.Get).Where(cell => cell != null).ToList();
            return query.Any()
                ? Mapper.Map<IEnumerable<CdmaCellView>, IEnumerable<SectorView>>(
                    query.Select(x => CdmaCellView.ConstructView(x, _btsRepository)))
                : null;
        }

        public IEnumerable<SectorView> QuerySectors(IEnumerable<string> collegeNames)
        {
            var ids =
                collegeNames.Select(x => _repository.GetCdmaCellIds(x)).Aggregate((x, y) => x.Concat(y)).Distinct();
            var query = ids.Select(_cellRepository.Get).Where(cell => cell != null).ToList();
            return query.Any()
                ? Mapper.Map<IEnumerable<CdmaCellView>, IEnumerable<SectorView>>(
                    query.Select(x => CdmaCellView.ConstructView(x, _btsRepository)))
                : null;
        }
    }
}
