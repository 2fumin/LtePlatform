using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;
using Lte.Parameters.Abstract;
using Lte.Evaluations.ViewModels;

namespace Lte.Evaluations.DataService
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
            var stats = _alarmRepository.GetAllList(begin, end);
            var ids = _repository.GetENodebIds(collegeName);
            return ids.Select(_eNodebRepository.Get
                ).Where(eNodeb => eNodeb != null).ToList().Select(x => ENodebView.ConstructView(x, stats));
        }

        public IEnumerable<string> QueryCollegeENodebNames(string collegeName)
        {
            var ids = _repository.GetENodebIds(collegeName);
            return ids.Select(_eNodebRepository.Get
                ).Where(eNodeb => eNodeb != null).ToList().Select(x => x.Name);
        } 
    }
}
