using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
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
                CollegeInfo college = _collegeRepository.Get(x.CollegeId);
                College3GTestView view = new College3GTestView
                {
                    CollegeName = college == null ? "Unknown" : college.Name
                };
                x.CloneProperties(view);
                return view;
            });
        }

        public College3GTestResults GetResult(DateTime date, int hour, string name)
        {
            CollegeInfo college = _collegeRepository.GetByName(name);
            if (college == null) return null;
            DateTime time = date.AddHours(hour);
            College3GTestResults result = _repository.GetByCollegeIdAndTime(college.Id, time);
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
            var results = _repository.GetAllList().Where(x => x.TestTime >= begin && x.TestTime < end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { Name = c.Name, Rate = r.DownloadRate };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rate));
        }

        public Task<College3GTestResults> Post(College3GTestResults result)
        {
            return _repository.InsertOrUpdateAsync(result);
        }

        public Task Delete(College3GTestResults result)
        {
            return _repository.DeleteAsync(result);
        }

        public College3GTestResults GetRecordResult(DateTime recordDate, int hour, string name)
        {
            CollegeInfo college = _collegeRepository.FirstOrDefault(x => x.Name == name);
            if (college == null) return null;
            DateTime time = recordDate.AddHours(hour);
            return _repository.FirstOrDefault(x => x.TestTime == time);
        }
    }
}
