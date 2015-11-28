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

        public TopDrop2GDateView GetViews(DateTime statDate, string city)
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
            return (from stat in
                    _repository.GetAllList(city, begin, end)
                    join bts in _btsRepository.GetAllList()
                        on stat.BtsId equals bts.BtsId into btsQuery
                    from bq in btsQuery.DefaultIfEmpty()
                    join eNodeb in _eNodebRepository.GetAllList()
                        on (bq == null ? -1 : bq.ENodebId) equals eNodeb.ENodebId into query
                    from q in query.DefaultIfEmpty()
                    select new TopCellContainer<TopDrop2GCell>
                    {
                        TopCell = stat,
                        LteName = q == null ? "无匹配LTE基站" : q.Name,
                        CdmaName = bq == null ? "无匹配CDMA基站" : bq.Name
                    }).ToList();
        }
    }
}
