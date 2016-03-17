using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.College;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeAlarmService
    {
        private readonly ICollegeRepository _repository;
        private readonly CollegeENodebService _service;

        public CollegeAlarmService(ICollegeRepository repository, IInfrastructureRepository infrastructureRepository,
            IENodebRepository eNodebRepository, IAlarmRepository alarmRepository)
        {
            _repository = repository;
            _service = new CollegeENodebService(infrastructureRepository, eNodebRepository, alarmRepository);
        }

        public Dictionary<string, IEnumerable<Tuple<string, int>>> GetAlarmCounts(DateTime begin, DateTime end)
        {
            var colleges = _repository.GetAllList().Select(x => x.Name);
            var results = new Dictionary<string, IEnumerable<Tuple<string, int>>>();
            foreach (var college in colleges)
            {
                var alarms = _service.QueryCollegeENodebs(college, begin, end).Where(x => x.AlarmTimes > 0).ToArray();
                if (alarms.Any())
                {
                    results.Add(college, alarms.Select(x => new Tuple<string, int>(x.Name, x.AlarmTimes)));
                }
            }
            return results;
        }
    }
}
