using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.College
{
    public class College3GTestService
    {
        private readonly ICollege3GTestRepository _repository;
        private readonly ICollegeRepository _collegeRepository;

        public College3GTestService(ICollege3GTestRepository repository, ICollegeRepository collegeRepository)
        {
            _repository = repository;
            _collegeRepository = collegeRepository;
        }

        public IEnumerable<College3GTestView> GetViews(DateTime date, int hour)
        {
            var statTime = date.AddHours(hour);
            var results = _repository.GetAll().Where(x => x.TestTime == statTime).ToList();
            if (!results.Any()) return new List<College3GTestView>();
            return results.Select(x =>
            {
                var college = _collegeRepository.Get(x.CollegeId);
                var view = x.MapTo<College3GTestView>();
                view.CollegeName = college?.Name;
                return view;
            });
        }

        public College3GTestResults GetResult(DateTime date, int hour, string name)
        {
            var college = _collegeRepository.GetByName(name);
            if (college == null) return null;
            var time = date.AddHours(hour);
            var result = _repository.GetByCollegeIdAndTime(college.Id, time);
            return result ?? new College3GTestResults
            {
                TestTime = date.AddHours(hour),
                CollegeId = college.Id,
                AccessUsers = 10,
                DownloadRate = 500,
                MinRssi = -110,
                MaxRssi = -100,
                Vswr = 1.4
            };
        }

        public Dictionary<string, double> GetAverageRates(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new {c.Name, Rate = r.DownloadRate };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rate));
        }

        public Dictionary<string, double> GetAverageUsers(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                select new {c.Name, Users = (double) r.AccessUsers};
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Users));
        }

        public Dictionary<string, double> GetAverageMinRssi(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, Rssi = r.MinRssi };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rssi));
        }

        public Dictionary<string, double> GetAverageMaxRssi(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, Rssi = r.MaxRssi };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rssi));
        }

        public Dictionary<string, double> GetAverageVswr(DateTime begin, DateTime end)
        {
            var results = _repository.GetAllList(begin, end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { c.Name, r.Vswr };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Vswr));
        }

        public async Task<int> Post(College3GTestResults result)
        {
            await _repository.InsertOrUpdateAsync(result);
            return _repository.SaveChanges();
        }

        public async Task<int> Delete(College3GTestResults result)
        {
            await _repository.DeleteAsync(result);
            return _repository.SaveChanges();
        }

        public College3GTestResults GetRecordResult(DateTime time, string name)
        {
            var college = _collegeRepository.GetByName(name);
            return college == null ? null : _repository.GetByCollegeIdAndTime(college.Id, time);
        }
    }
}
