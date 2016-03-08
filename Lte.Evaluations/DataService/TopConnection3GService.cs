using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.MapperSerive.Kpi;
using Lte.Evaluations.ViewModels.Kpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class TopConnection3GService
    {
        private readonly ITopConnection3GRepository _repository;
        private readonly IBtsRepository _btsRepository;
        private readonly IENodebRepository _eNodebRepository;

        public TopConnection3GService(ITopConnection3GRepository repository, IBtsRepository btsRepository,
            IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
            _eNodebRepository = eNodebRepository;
        }

        public TopConnection3GDateView GetDateView(DateTime statDate, string city)
        {
            var begin = statDate.AddDays(-100);
            var end = statDate.AddDays(1);
            var query = _repository.GetAllList(city, begin, end);
            begin = query.Select(x => x.StatTime).Max().Date;
            end = end.AddDays(1);
            var statContainers = GetStatContainers(city, begin, end);
            var viewContainers =
                Mapper.Map<List<TopCellContainer<TopConnection3GCell>>, IEnumerable<TopConnection3GCellViewContainer>>(statContainers);
            var views = viewContainers.Select(x =>
            {
                var view = x.TopConnection3GCellView;
                view.LteName = x.LteName;
                view.CdmaName = x.CdmaName;
                return view;
            });
            return new TopConnection3GDateView
            {
                StatDate = begin,
                StatViews = views
            };
        }

        private List<TopCellContainer<TopConnection3GCell>> GetStatContainers(string city, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(city, begin, end)
                .QueryContainers(_btsRepository, _eNodebRepository)
                .ToList();
        }

        public IEnumerable<TopConnection3GTrendView> GetTrendViews(DateTime begin, DateTime end, string city)
        {
            var statContainers = GetTrendContainers(city, begin, end);
            var viewContainers =
                Mapper.Map<List<TopCellContainer<TopConnection3GTrend>>, IEnumerable<TopConnection3GTrendViewContainer>>(statContainers);
            return viewContainers.Select(x =>
            {
                var view = x.TopConnection3GTrendView;
                view.CellName = x.CellName;
                view.ENodebName = x.ENodebName;
                return view;
            });
        }

        private List<TopCellContainer<TopConnection3GTrend>> GetTrendContainers(string city, DateTime begin, DateTime end)
        {
            return
                _repository.GetAllList(city, begin, end)
                    .QueryTrends()
                    .ToList()
                    .QueryContainers(_btsRepository, _eNodebRepository)
                    .ToList();
        }
    }
}
