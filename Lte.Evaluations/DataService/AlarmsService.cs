using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class AlarmsService
    {
        private readonly IAlarmRepository _repository;

        public AlarmsService(IAlarmRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AlarmView> Get(int eNodebId, DateTime begin, DateTime end)
        {
            var stats = _repository.GetAllList(begin, end, eNodebId);
            return Mapper.Map<IEnumerable<AlarmStat>, IEnumerable<AlarmView>>(stats);
        }

        public int GetCounts(int eNodebId, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(begin, end, eNodebId).Count;
        }

        private static Stack<AlarmStat> AlarmStats { get; } = new Stack<AlarmStat>();

        public void UploadZteAlarms(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<AlarmStatCsv>(reader, CsvFileDescription.CommaDescription).ToList();
                foreach (var stat in stats)
                {
                    AlarmStats.Push(AlarmStat.ConstructStat(stat));
                }
            }
            catch
            {
                // ignored
            }
        }

        public void UploadHwAlarms(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<AlarmStatHuawei>(reader, CsvFileDescription.CommaDescription).ToList();
                foreach (var stat in stats)
                {
                    AlarmStats.Push(AlarmStat.ConstructStat(stat));
                }
            }
            catch
            {
                // ignored
            }
        }

        public bool DumpOneStat()
        {
            var stat = AlarmStats.Pop();
            if (stat == null) return false;
            var item =
                _repository.FirstOrDefault(
                    x =>
                        x.HappenTime == stat.HappenTime && x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId &&
                        x.AlarmId == stat.AlarmId);
            if (item == null)
            {
                _repository.Insert(stat);
            }
            return true;
        }

        public int GetAlarmsToBeDump()
        {
            return AlarmStats.Count;
        }

        public IEnumerable<AlarmHistory> GetAlarmHistories(DateTime begin, DateTime end)
        {
            var results = new List<AlarmHistory>();
            while (begin < end)
            {
                var items = _repository.GetAllList(x => x.HappenTime >= begin && x.HappenTime < begin.AddDays(1));
                results.Add(new AlarmHistory
                {
                    DateString = begin.ToShortDateString(),
                    Alarms = items.Count
                });
                begin = begin.AddDays(1);
            }
            return results;
        } 
    } 
}
