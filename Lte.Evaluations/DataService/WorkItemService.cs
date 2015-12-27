using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.LinqToExcel;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Work;

namespace Lte.Evaluations.DataService
{
    public class WorkItemService
    {
        private readonly IWorkItemRepository _repository;

        public WorkItemService(IWorkItemRepository repository)
        {
            _repository = repository;
        }

        public List<WorkItem> QueryAllList()
        {
            return _repository.GetAllList();
        }

        public string ImportExcelFiles(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            const string sheetName = "工单查询结果";
            var infos = (from c in factory.Worksheet<WorkItemExcel>(sheetName)
                        select c).ToList();
            var oldInfos = from info in infos
                join item in _repository.GetAllList() on info.SerialNumber equals item.SerialNumber
                select info;
            var newInfos = infos.Except(oldInfos).ToList();
            var newItems = Mapper.Map<List<WorkItemExcel>, IEnumerable<WorkItem>>(newInfos);
            foreach (var oldInfo in oldInfos)
            {
                _repository.Import(oldInfo);
            }
            var count = 0;
            foreach (var item in newItems)
            {
                if (_repository.Insert(item) != null) count++;
            }
            return "完成工单导入：" + count + "条";
        }
    }
}
