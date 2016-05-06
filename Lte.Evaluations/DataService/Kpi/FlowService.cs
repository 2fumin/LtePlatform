using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class FlowService
    {
        private readonly IFlowHuaweiRepository _huaweiRepository;

        private static Stack<FlowHuawei> FlowHuaweis { get; set; }

        public int FlowHuaweiCount => FlowHuaweis.Count;

        public FlowService(IFlowHuaweiRepository huaweiRepositroy)
        {
            _huaweiRepository = huaweiRepositroy;
            if (FlowHuaweis == null)
                FlowHuaweis = new Stack<FlowHuawei>();
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

        public async Task<FlowHuawei> DumpOneHuaweiStat()
        {
            var stat = FlowHuaweis.Pop();
            if (stat == null) return null;
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
            }
            return stat;
        }

        public void ClearHuaweiStats()
        {
            FlowHuaweis.Clear();
        }

        public FlowHuawei QueryHuaweiStat(int index)
        {
            return FlowHuaweis.ElementAt(index);
        }
    }
}
