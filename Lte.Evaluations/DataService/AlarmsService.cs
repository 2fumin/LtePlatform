using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Evaluations.DataService.Basic;
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
            if (AlarmStats == null)
                AlarmStats = new Stack<AlarmStat>();
        }

        public IEnumerable<AlarmView> Get(int eNodebId, DateTime begin, DateTime end)
        {
            var stats = _repository.GetAllList(begin, end, eNodebId);
            return Mapper.Map<IEnumerable<AlarmStat>, IEnumerable<AlarmView>>(stats);
        }

        public IEnumerable<AlarmView> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var stats = _repository.GetAllList(begin, end, eNodebId, sectorId);
            return Mapper.Map<IEnumerable<AlarmStat>, IEnumerable<AlarmView>>(stats);
        }

        public int GetCounts(int eNodebId, DateTime begin, DateTime end)
        {
            return _repository.Count(begin, end, eNodebId);
        }

        private static Stack<AlarmStat> AlarmStats { get; set; }

        public void UploadZteAlarms(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<AlarmStatCsv>(reader, CsvFileDescription.CommaDescription).ToList();
                foreach (var stat in stats)
                {
                    AlarmStats.Push(Mapper.Map<AlarmStatCsv, AlarmStat>(stat));
                }
            }
            catch
            {
                //ignore.
            }
        }

        public void UploadHwAlarms(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<AlarmStatHuawei>(reader, CsvFileDescription.CommaDescription).ToList();
                foreach (var stat in stats)
                {
                    AlarmStats.Push(Mapper.Map<AlarmStatHuawei, AlarmStat>(stat));
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
            if (stat == null) throw new NullReferenceException("alarm stat is null!");
            var item =
                _repository.FirstOrDefault(
                    x =>
                        x.HappenTime == stat.HappenTime && x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId &&
                        x.AlarmId == stat.AlarmId);
            if (item == null)
            {
                _repository.Insert(stat);
            }
            else
            {
                item.RecoverTime = stat.RecoverTime;
            }
            _repository.SaveChanges();
            return true;
        }

        public int GetAlarmsToBeDump()
        {
            return AlarmStats.Count;
        }

        public IEnumerable<AlarmStat> QueryAlarmStats()
        {
            return AlarmStats;
        }

        public void ClearAlarmStats()
        {
            AlarmStats.Clear();
        }

        public IEnumerable<AlarmHistory> GetAlarmHistories(DateTime begin, DateTime end)
        {
            var results = new List<AlarmHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin;
                var endDate = begin.AddDays(1);
                var items = _repository.GetAllList(beginDate, endDate);
                results.Add(new AlarmHistory
                {
                    DateString = begin.ToShortDateString(),
                    Alarms = items.Count
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

        public int DumpHuaweiAlarmInfo(HuaweiLocalCellDef cellDef)
        {
            var items = _repository.GetAllList(DateTime.Now.AddDays(-100), DateTime.Now, cellDef.ENodebId);
            foreach (var item in items.Where(x => x.SectorId == 0))
            {
                if (item.AlarmCategory == AlarmCategory.Huawei)
                {
                    switch (item.AlarmType)
                    {
                        //eNodeB名称=大良南区电信LBBU6, 本地小区标识=3, 小区双工模式=FDD, 小区名称=大良美的广场擎峰_3, eNodeB标识=501157, 小区标识=51, 具体问题=射频单元异常
                        case AlarmType.CellDown://AlarmType=4
                            item.AlarmCategory = AlarmCategory.Qos;
                            item.SectorId =
                                byte.Parse(
                                    item.Details.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)[5].Split('=')
                                        [1]);
                            break;
                        //eNodeB名称=杏坛电信LBBU13, 本地小区标识=2, PCI值=212, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=杏坛万亩员工村R_2, eNodeB标识=500578, 小区标识=50
                        case AlarmType.PciCrack://AlarmType=46
                            item.AlarmCategory = AlarmCategory.Qos;
                            item.SectorId =
                                byte.Parse(
                                    item.Details.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)[8].Split('=')
                                        [1]);
                            break;
                        //eNodeB名称=乐从奥运林, 本地小区标识=0, 小区双工模式=FDD, 小区当前使用发射通道数=2, 小区当前使用接收通道数=4, 小区名称=乐从奥运林_0, eNodeB标识=499773, 小区标识=0, 具体问题=基带降额
                        //eNodeB名称=乐从东平桥脚, 本地小区标识=4, 小区双工模式=FDD, 小区当前使用发射通道数=1, 小区当前使用接收通道数=2, 小区名称=乐从东平桥脚_4, eNodeB标识=500264, 小区标识=4, 具体问题=通道异常
                        //eNodeB名称=容桂华口接入机房LBBU2, 本地小区标识=1, 小区双工模式=FDD, 小区当前使用发射通道数=2, 小区当前使用接收通道数=4, 小区名称=容桂骏业路R_1, eNodeB标识=552563, 小区标识=49, 具体问题=基带降额
                        case AlarmType.BadPerformance://AlarmType=43
                            item.SectorId =
                               byte.Parse(
                                   item.Details.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)[7].Split('=')
                                       [1]);
                            item.AlarmCategory = AlarmCategory.Apparatus;
                            break;
                        default:
                            item.SectorId = 255;
                            break;
                    }
                }
                else
                    item.SectorId = 255;
                _repository.Update(item);
            }
            return _repository.SaveChanges();
        }
    } 
}
