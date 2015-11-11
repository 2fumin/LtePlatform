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
    public class College4GTestService
    {
        private readonly ICollege4GTestRepository _repository;
        private readonly ICollegeRepository _collegeRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellRepository _cellRepository;

        public College4GTestService(ICollege4GTestRepository repository,
            ICollegeRepository collegeRepository, IENodebRepository eNodebRepository, ICellRepository cellRepository)
        {
            _repository = repository;
            _collegeRepository = collegeRepository;
            _eNodebRepository = eNodebRepository;
            _cellRepository = cellRepository;
        }

        public IEnumerable<College4GTestView> GetViews(DateTime date, int hour)
        {
            var statTime = date.AddHours(hour);
            var results = _repository.GetAllList().Where(x => x.TestTime == statTime).ToList();
            if (!results.Any()) return new List<College4GTestView>();
            return results.Select(x =>
            {
                CollegeInfo college = _collegeRepository.Get(x.CollegeId);
                ENodeb eNodeb = _eNodebRepository.FirstOrDefault(e => e.ENodebId == x.ENodebId);
                Cell cell = eNodeb == null
                    ? null
                    : _cellRepository.FirstOrDefault(ce =>
                        ce.ENodebId == x.ENodebId && ce.SectorId == x.SectorId);
                College4GTestView view = new College4GTestView
                {
                    CollegeName = college == null ? "Unknown" : college.Name,
                    CellName = eNodeb == null ? "Undefined" : eNodeb.Name + "-" + x.SectorId,
                    Pci = cell?.Pci ?? -1
                };
                x.CloneProperties(view);
                return view;
            });
        }

        public College4GTestResults GetResult(DateTime date, int hour, string name, string eNodebName, byte sectorId)
        {
            CollegeInfo college = _collegeRepository.FirstOrDefault(x => x.Name == name);
            if (college == null) return null;
            ENodeb eNodeb = _eNodebRepository.FirstOrDefault(x => x.Name == eNodebName);
            if (eNodeb == null) return null;
            DateTime time = date.AddHours(hour);
            College4GTestResults result = _repository.FirstOrDefault(
                x => x.TestTime == time && x.CollegeId == college.Id);
            if (result == null)
                return new College4GTestResults
                {
                    TestTime = date.AddHours(hour),
                    CollegeId = college.Id,
                    ENodebId = eNodeb.ENodebId,
                    AccessUsers = 20,
                    DownloadRate = 8000,
                    UploadRate = 3000,
                    SectorId = sectorId,
                    Rsrp = -90,
                    Sinr = 14
                };
            result.ENodebId = eNodeb.ENodebId;
            result.SectorId = sectorId;
            return result;
        }

        public Dictionary<string, double> GetAverageRates(DateTime begin, DateTime end, byte upload)
        {
            var results = _repository.GetAllList().Where(x => x.TestTime >= begin && x.TestTime < end);
            var query = from r in results
                        join c in _collegeRepository.GetAllList() on r.CollegeId equals c.Id
                        select new { Name = c.Name, Rate = (upload == 0) ? r.DownloadRate : r.UploadRate };
            return query.GroupBy(x => x.Name).ToDictionary(s => s.Key, t => t.Average(x => x.Rate));
        }

        public Task<College4GTestResults> Post(College4GTestResults result)
        {
            return _repository.InsertOrUpdateAsync(result);
        }

        public Task Delete(College4GTestResults result)
        {
            return _repository.DeleteAsync(result);
        }

        public College4GTestResults GetRecordResult(DateTime recordDate, int hour, string name)
        {
            CollegeInfo college = _collegeRepository.FirstOrDefault(x => x.Name == name);
            if (college == null) return null;
            DateTime time = recordDate.AddHours(hour);
            return _repository.FirstOrDefault(x => x.TestTime == time);
        }
    }
}
