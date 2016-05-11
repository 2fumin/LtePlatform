using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
            var flows =
                Mapper.Map<List<FlowHuaweiCsv>, IEnumerable<FlowHuawei>>(FlowHuaweiCsv.ReadFlowHuaweiCsvs(reader));
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

        public async Task<bool> DumpOneHuaweiStat()
        {
            var stat = FlowHuaweis.Pop();
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
