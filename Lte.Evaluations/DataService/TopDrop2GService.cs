﻿using System;
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

        public IEnumerable<TopDrop2GCellView> GetViews(DateTime statDate, string city)
        {
            var end = statDate.AddDays(1);
            var statContainers =
                (from stat in
                    _repository.GetAll().Where(x => x.StatTime >= statDate && x.StatTime < end && x.City == city)
                    join bts in _btsRepository.GetAll()
                        on stat.BtsId equals bts.BtsId into btsQuery
                    from bq in btsQuery.DefaultIfEmpty()
                    join eNodeb in _eNodebRepository.GetAll()
                        on (bq == null ? -1 : bq.ENodebId) equals eNodeb.ENodebId into query
                    from q in query.DefaultIfEmpty()
                    select new TopDrop2GCellContainer
                    {
                        TopDrop2GCell = stat,
                        LteName = q == null ? "无匹配LTE基站" : q.Name,
                        CdmaName = bq == null ? "无匹配CDMA基站" : bq.Name
                    }).ToList();
            var viewContainers =
                Mapper.Map<List<TopDrop2GCellContainer>, IEnumerable<TopDrop2GCellViewContainer>>(statContainers);
            return viewContainers.Select(x =>
            {
                var view = x.TopDrop2GCellView;
                view.LteName = x.LteName;
                view.CdmaName = x.CdmaName;
                return view;
            });
        } 
    }
}
