using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class FlowService
    {
        private readonly IFlowHuaweiRepository _huaweiRepository;
        private readonly IFlowZteRepository _zteRepository;

        private static Stack<FlowHuawei> FlowHuaweis { get; set; }

        private static Stack<FlowZte> FlowZtes { get; set; }

        public int FlowHuaweiCount => FlowHuaweis.Count;

        public int FlowZteCount => FlowZtes.Count;

        public FlowService(IFlowHuaweiRepository huaweiRepositroy, IFlowZteRepository zteRepository)
        {
            _huaweiRepository = huaweiRepositroy;
            _zteRepository = zteRepository;
            if (FlowHuaweis == null)
                FlowHuaweis = new Stack<FlowHuawei>();
            if (FlowZtes == null)
                FlowZtes = new Stack<FlowZte>();
        }

        public void UploadFlowHuaweis(StreamReader reader)
        {
            var originCsvs = FlowHuaweiCsv.ReadFlowHuaweiCsvs(reader);
            var mergedCsvs = (from item in originCsvs
                group item by new
                {
                    item.StatTime.Date,
                    item.CellInfo
                }
                into g
                select new FlowHuaweiCsv
                {
                    StatTime = g.Key.Date,
                    AverageActiveUsers = g.Average(x => x.AverageActiveUsers),
                    AverageUsers = g.Average(x => x.AverageUsers),
                    ButLastDownlinkDurationInMs = g.Sum(x => x.ButLastDownlinkDurationInMs),
                    ButLastUplinkDurationInMs = g.Sum(x => x.ButLastUplinkDurationInMs),
                    CellInfo = g.Key.CellInfo,
                    DedicatedPreambles = g.Sum(x => x.DedicatedPreambles),
                    DownlinkAveragePrbs = g.Average(x => x.DownlinkAveragePrbs),
                    DownlinkAverageUsers = g.Average(x => x.DownlinkAverageUsers),
                    DownlinkDciCces = g.Sum(x => x.DownlinkDciCces),
                    DownlinkDrbPbs = g.Average(x => x.DownlinkDrbPbs),
                    DownlinkDurationInMs = g.Sum(x => x.DownlinkDurationInMs),
                    DownlinkMaxUsers = g.Max(x => x.DownlinkMaxUsers),
                    GroupAPreambles = g.Sum(x => x.GroupAPreambles),
                    GroupBPreambles = g.Sum(x => x.GroupBPreambles),
                    LastTtiDownlinkFlowInByte = g.Sum(x => x.LastTtiDownlinkFlowInByte),
                    LastTtiUplinkFlowInByte = g.Sum(x => x.LastTtiUplinkFlowInByte),
                    MaxActiveUsers = g.Max(x => x.MaxActiveUsers),
                    MaxUsers = g.Max(x => x.MaxUsers),
                    PagingUsersString = g.First().PagingUsersString,
                    PdcpDownlinkFlowInByte = g.Sum(x => x.PdcpDownlinkFlowInByte),
                    PdcpUplinkFlowInByte = g.Sum(x => x.PdcpUplinkFlowInByte),
                    PucchPrbsString = g.Sum(x => x.PucchPrbsString.ConvertToInt(0)).ToString(),
                    TotalCces = g.Sum(x => x.TotalCces),
                    UplinkAveragePrbs = g.Average(x => x.UplinkAveragePrbs),
                    UplinkAverageUsers = g.Average(x => x.UplinkAverageUsers),
                    UplinkDciCces = g.Sum(x => x.UplinkDciCces),
                    UplinkDrbPbs = g.Sum(x => x.UplinkDrbPbs),
                    UplinkDurationInMs = g.Sum(x => x.UplinkDurationInMs),
                    UplinkMaxUsers = g.Max(x => x.UplinkMaxUsers)
                }).ToList();
            var flows =
                Mapper.Map<List<FlowHuaweiCsv>, IEnumerable<FlowHuawei>>(mergedCsvs);
            foreach (var flow in flows)
            {
                FlowHuaweis.Push(flow);
            }
        }

        public void UploadFlowZtes(StreamReader reader)
        {
            var flows = Mapper.Map<IEnumerable<FlowZteCsv>, IEnumerable<FlowZte>>(FlowZteCsv.ReadFlowZteCsvs(reader));
            foreach (var flow in flows)
            {
                FlowZtes.Push(flow);
            }
        }

        public FlowHuawei GetTopHuaweiItem()
        {
            return FlowHuaweis.Pop();
        }

        public async Task<bool> DumpOneHuaweiStat()
        {
            var stat = GetTopHuaweiItem();
            if (stat == null) return false;
            var item =
                await
                    _huaweiRepository.FirstOrDefaultAsync(
                        x =>
                            x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                            x.LocalCellId == stat.LocalCellId);
            if (item == null)
            {
                var result = await _huaweiRepository.InsertAsync(stat);
                _huaweiRepository.SaveChanges();
                return result != null;
            }
            return false;
        }

        public async Task<bool> DumpOneZteStat()
        {
            var stat = FlowZtes.Pop();
            if (stat == null) return false;
            var item =
                await
                    _zteRepository.FirstOrDefaultAsync(
                        x =>
                            x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                            x.SectorId == stat.SectorId);
            if (item == null)
            {
                var result = await _zteRepository.InsertAsync(stat);
                _zteRepository.SaveChanges();
                return result != null;
            }
            return false;
        }

        public void ClearHuaweiStats()
        {
            FlowHuaweis.Clear();
        }

        public void ClearZteStats()
        {
            FlowZtes.Clear();
        }

        public FlowHuawei QueryHuaweiStat(int index)
        {
            return FlowHuaweis.ElementAt(index);
        }

        public async Task<IEnumerable<FlowHistory>> GetFlowHistories(DateTime begin, DateTime end)
        {
            var results = new List<FlowHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin;
                var endDate = begin.AddDays(1);
                var huaweiItems = await _huaweiRepository.CountAsync(beginDate, endDate);
                var zteItems = await _zteRepository.CountAsync(beginDate, endDate);
                results.Add(new FlowHistory
                {
                    DateString = begin.ToShortDateString(),
                    HuaweiItems = huaweiItems,
                    ZteItems = zteItems
                });
                begin = begin.AddDays(1);
            }
            return results;
        }
    }
}
