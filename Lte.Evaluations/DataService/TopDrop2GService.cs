using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class TopDrop2GService
    {
        private readonly ITopDrop2GCellRepository _repository;
        private readonly IBtsRepository _btsRepository;
        private readonly IENodebRepository _eNodebRepository;

        public TopDrop2GService(ITopDrop2GCellRepository repository, IBtsRepository btsRepository,
            IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
            _eNodebRepository = eNodebRepository;
        }

        public TopDrop2GDateView GetDateView(DateTime statDate, string city)
        {
            var begin = statDate.AddDays(-100);
            var end = statDate.AddDays(1);
            var query = _repository.GetAllList(city, begin, end);
            begin = query.Select(x => x.StatTime).Max().Date;
            end = end.AddDays(1);
            var statContainers = GetStatContainers(city, begin, end);
            var viewContainers =
                Mapper.Map<List<TopCellContainer<TopDrop2GCell>>, IEnumerable<TopDrop2GCellViewContainer>>(statContainers);
            var views = viewContainers.Select(x =>
            {
                var view = x.TopDrop2GCellView;
                view.LteName = x.LteName;
                view.CdmaName = x.CdmaName;
                return view;
            });
            return new TopDrop2GDateView
            {
                StatDate = begin.ToShortDateString(),
                StatViews = views
            };
        }

        private List<TopCellContainer<TopDrop2GCell>> GetStatContainers(string city, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(city, begin, end)
                .QueryContainers(_btsRepository, _eNodebRepository)
                .ToList();
        }

        public IEnumerable<TopDrop2GTrendView> GetTrendViews(DateTime begin, DateTime end, string city)
        {
            var statContainers = GetTrendContainers(city, begin, end);
            var viewContainers = 
                Mapper.Map<List<TopCellContainer<TopDrop2GTrend>>, IEnumerable<TopDrop2GTrendViewContainer>>(statContainers);
            return viewContainers.Select(x =>
            {
                var view = x.TopDrop2GTrendView;
                view.CellName = x.CellName;
                view.ENodebName = x.ENodebName;
                return view;
            });
        } 

        private List<TopCellContainer<TopDrop2GTrend>> GetTrendContainers(string city, DateTime begin, DateTime end)
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
