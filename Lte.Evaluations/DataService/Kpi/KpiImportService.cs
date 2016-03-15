using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.LinqToExcel;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class KpiImportService
    {
        private readonly ICdmaRegionStatRepository _regionStatRepository;
        private readonly ITopDrop2GCellRepository _top2GRepository;
        private readonly ITopConnection3GRepository _top3GRepository;

        public KpiImportService(ICdmaRegionStatRepository regionStatRepository,
            ITopDrop2GCellRepository top2GRepository, ITopConnection3GRepository top3GRepository)
        {
            _regionStatRepository = regionStatRepository;
            _top2GRepository = top2GRepository;
            _top3GRepository = top3GRepository;
        }

        public List<string> Import(string path, IEnumerable<string> regions)
        {
            var factory = new ExcelQueryFactory {FileName = path};
            var message = (from region in regions
                let stats = (from c in factory.Worksheet<CdmaRegionStatExcel>(region)
                    where c.StatDate > DateTime.Today.AddDays(-30) && c.StatDate <= DateTime.Today
                    select c).ToList()
                let count = _regionStatRepository.Import(stats)
                select "完成导入区域：'" + region + "'的日常指标导入" + count + "条").ToList();
            var topDrops = (from c in factory.Worksheet<TopDrop2GCellExcel>(TopDrop2GCellExcel.SheetName)
                            select c).ToList();
            var drops = _top2GRepository.Import(topDrops);
            message.Add("完成TOP掉话小区导入" + drops + "个");
            var topConnections = (from c in factory.Worksheet<TopConnection3GCellExcel>(TopConnection3GCellExcel.SheetName)
                                  select c).ToList();
            var connections = _top3GRepository.Import(topConnections);
            message.Add("完成TOP连接小区导入" + connections + "个");
            return message;
        }
    }
}
