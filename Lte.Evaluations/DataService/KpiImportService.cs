using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToExcel;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class KpiImportService
    {
        private readonly ICdmaRegionStatRepository _regionStatRepository;
        private readonly ITopDrop2GCellRepository _top2GRepository;
        private readonly ITopConnection3GRepository _top3GRepository;
        const string topDropTag = "掉话TOP30小区";
        const string topConnectionTag = "连接TOP30小区";

        public KpiImportService(ICdmaRegionStatRepository regionStatRepository,
            ITopDrop2GCellRepository top2GRepository, ITopConnection3GRepository top3GRepository)
        {
            _regionStatRepository = regionStatRepository;
            _top2GRepository = top2GRepository;
            _top3GRepository = top3GRepository;
        }

        public string Import(string path, IEnumerable<string> regions)
        {
            var factory = new ExcelQueryFactory {FileName = path};
            var message = "";
            foreach (var region in regions)
            {
                var stats = (from c in factory.Worksheet<CdmaRegionStatExcel>(region)
                    where c.StatDate > DateTime.Today.AddDays(-30) && c.StatDate <= DateTime.Today
                    select c).ToList();
                var count = _regionStatRepository.Import(stats);
                message += "完成导入区域：'" + region + "'的日常指标导入" + count + "条</br>";
            }
            var topDrops = (from c in factory.Worksheet<TopDrop2GCellExcel>(topDropTag)
                            select c).ToList();
            var drops = _top2GRepository.Import(topDrops);
            message += "完成TOP掉话小区导入" + drops + "个</br>";
            var topConnections = (from c in factory.Worksheet<TopConnection3GCellExcel>(topConnectionTag)
                                  select c).ToList();
            var connections = _top3GRepository.Import(topConnections);
            message += "完成TOP连接小区导入" + connections + "个";
            return message;
        }
    }
}
