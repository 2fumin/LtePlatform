using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeENodebService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IAlarmRepository _alarmRepository;

        public CollegeENodebService(IInfrastructureRepository repository,
            IENodebRepository eNodebRepository, IAlarmRepository alarmRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _alarmRepository = alarmRepository;
        }

        public IEnumerable<ENodebView> QueryCollegeENodebs(string collegeName,
            DateTime begin, DateTime end)
        {
            var ids = _repository.GetENodebIds(collegeName);
            return (from id in ids
                select _eNodebRepository.Get(id)
                into eNodeb
                where eNodeb != null
                let stats = _alarmRepository.GetAllList(begin, end, eNodeb.ENodebId)
                select ENodebView.ConstructView(eNodeb, stats)).ToList();
        }

        public IEnumerable<string> QueryCollegeENodebNames(string collegeName)
        {
            var ids = _repository.GetENodebIds(collegeName);
            return ids.Select(_eNodebRepository.Get
                ).Where(eNodeb => eNodeb != null).ToList().Select(x => x.Name);
        } 

        public IEnumerable<ENodebView> QueryCollegeENodebs(IEnumerable<string> collegeNames)
        {
            var concateIds = collegeNames.Select(x => _repository.GetENodebIds(x));
            var distictIds = concateIds.Aggregate((x, y) => x.Concat(y)).Distinct();
            var items = distictIds.Select(_eNodebRepository.Get).Where(eNodeb => eNodeb != null).ToList();
            return Mapper.Map<List<ENodeb>, IEnumerable<ENodebView>>(items);
        }
    }
}
