using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeKpiService
    {
        private readonly ICollegeKpiRepository _repository;
        private readonly ICollegeRepository _collegeRepository;

        public CollegeKpiService(ICollegeKpiRepository repository, ICollegeRepository collegeRepository)
        {
            _repository = repository;
            _collegeRepository = collegeRepository;
        }

        public IEnumerable<CollegeKpiView> GetViews(DateTime date, int hour)
        {
            var time = date.AddHours(hour);
            var results = _repository.GetAllList(time);

            if (!results.Any()) return new List<CollegeKpiView>();
            return results.Select(x =>
            {
                var college = _collegeRepository.Get(x.CollegeId);
                var view = Mapper.Map<CollegeKpi, CollegeKpiView>(x);
                view.CollegeName = college?.Name;
                return view;
            });
        }

        public CollegeKpi GetResult(DateTime date, int hour, string name)
        {
            var college = _collegeRepository.GetByName(name);
            if (college == null) return null;
            var time = date.AddHours(hour);
            var result = _repository.GetByCollegeIdAndTime(college.Id, time);
            return result ?? new CollegeKpi
            {
                TestTime = date.AddHours(hour),
                CollegeId = college.Id,
                OnlineUsers = 100,
                DownloadFlow = 10,
                UploadFlow = 1,
                RrcConnection = 99.9,
                ErabConnection = 99.9,
                ErabDrop = 0.1,
                Connection2G = 99.8,
                Connection3G = 99.9,
                Erlang3G = 32,
                Flow3G = 1,
                Drop3G = 0.1
            };
        }

        public Task<CollegeKpi> Post(CollegeKpi stat)
        {
            return _repository.InsertOrUpdateAsync(stat);
        }

        public Task Delete(CollegeKpi stat)
        {
            return _repository.DeleteAsync(stat);
        }

        public CollegeKpi GetRecordResult(CollegeKpiView view)
        {
            var college = _collegeRepository.GetByName(view.CollegeName);
            if (college == null) return null;
            var time = view.TestTime;
            return _repository.GetByCollegeIdAndTime(college.Id, time);
        }
    }
}
