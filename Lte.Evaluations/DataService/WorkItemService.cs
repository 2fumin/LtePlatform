using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.LinqToExcel;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Work;

namespace Lte.Evaluations.DataService
{
    public class WorkItemService
    {
        private readonly IWorkItemRepository _repository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly ITownRepository _townRepository;

        public WorkItemService(IWorkItemRepository repository, IENodebRepository eNodebRepository,
            IBtsRepository btsRepository, ITownRepository townRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _btsRepository = btsRepository;
            _townRepository = townRepository;
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

        public int QueryTotalPages(string statCondition, string typeCondition, int itemsPerPage)
        {
            var predict = (statCondition + '_' + typeCondition).GetWorkItemFilter();
            var counts = predict == null ? _repository.Count() : _repository.Count(predict);
            return (int) Math.Ceiling((double) counts/itemsPerPage);
        }

        public IEnumerable<WorkItemView> QueryViews(string statCondition, string typeCondition, int itemsPerPage,
            int page)
        {
            var predict = (statCondition + '_' + typeCondition).GetWorkItemFilter();
            var stats = predict == null
                ? _repository.GetAll(page, itemsPerPage, x => x.Deadline)
                : _repository.Get(predict, page, itemsPerPage, x => x.Deadline);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(stats.ToList());
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views;
        } 
    }
}
