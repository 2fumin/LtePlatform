using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.ViewModels.College;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities.College;

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
                var view = x.MapTo<CollegeKpiView>();
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

        public Dictionary<string, double> GetAverageOnlineUsers(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, Users = (double)r.OnlineUsers };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Users));
        }

        public Dictionary<string, double> GetAverageDownloadFlow(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.DownloadFlow };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.DownloadFlow));
        }

        public Dictionary<string, double> GetAverageUploadFlow(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.UploadFlow };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.UploadFlow));
        }

        public Dictionary<string, double> GetAverageRrcConnection(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.RrcConnection };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.RrcConnection));
        }

        public Dictionary<string, double> GetAverageErabConnection(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.ErabConnection };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.ErabConnection));
        }

        public Dictionary<string, double> GetAverageErabDrop(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.ErabDrop };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.ErabDrop));
        }


        public Dictionary<string, double> GetAverageConnection2G(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.Connection2G };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Connection2G));
        }

        public Dictionary<string, double> GetAverageConnection3G(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.Connection3G };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Connection3G));
        }

        public Dictionary<string, double> GetAverageErlang3G(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.Erlang3G };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Erlang3G));
        }

        public Dictionary<string, double> GetAverageFlow3G(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.Flow3G };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Flow3G));
        }

        public Dictionary<string, double> GetAverageDrop3G(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.Drop3G };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Drop3G));
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
