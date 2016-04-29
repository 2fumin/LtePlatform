using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.LinqToExcel;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Work;

namespace Lte.Evaluations.DataService
{
    public class WorkItemService
    {
        private readonly IWorkItemRepository _repository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly ITownRepository _townRepository;
        private readonly ICellRepository _cellRepository;

        public WorkItemService(IWorkItemRepository repository, IENodebRepository eNodebRepository,
            IBtsRepository btsRepository, ITownRepository townRepository, ICellRepository cellRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _btsRepository = btsRepository;
            _townRepository = townRepository;
            _cellRepository = cellRepository;
        }

        public WorkItemView Query(string serialNumber)
        {
            var item = _repository.GetAll().FirstOrDefault(x => x.SerialNumber == serialNumber);
            var result = Mapper.Map<WorkItem, WorkItemView>(item);
            result.UpdateTown(_eNodebRepository, _btsRepository, _townRepository);
            return result;
        }

        public int UpdateLteSectorIds()
        {
            var items = _repository.GetAllList(x => x.ENodebId > 10000);
            int count = 0;
            foreach (var item in items)
            {
                var cell = _cellRepository.GetBySectorId(item.ENodebId, item.SectorId);
                if (cell != null) continue;
                cell = _cellRepository.GetBySectorId(item.ENodebId, (byte) (item.SectorId + 48));
                if (cell != null)
                {
                    item.SectorId += 48;
                    _repository.Update(item);
                    count++;
                }
            }
            return count;
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

        public int QueryTotalItems(string statCondition, string typeCondition)
        {
            var predict = (statCondition + '_' + typeCondition).GetWorkItemFilter();
            var counts = predict == null ? _repository.Count() : _repository.Count(predict);
            return counts;
        }

        public async Task<Tuple<int, int, int>> QueryTotalItemsThisMonth()
        {
            var lastMonthDate = DateTime.Today.Day < 26 ? DateTime.Today.AddMonths(-1) : DateTime.Today;
            var begin = new DateTime(lastMonthDate.Year, lastMonthDate.Month, 26);
            var end = begin.AddMonths(1);
            var items = await _repository.GetAllKpiListAsync(begin, end);
            return new Tuple<int, int, int>(items.Count, items.Count(x => x.FinishTime != null),
                items.Count(x => (x.Deadline < DateTime.Today && x.FinishTime == null) ||
                (x.FinishTime != null && x.FinishTime > x.Deadline)));
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

        public async Task<IEnumerable<WorkItemView>> QueryViews(int eNodebId)
        {
            var statList = await _repository.GetAllListAsync(eNodebId);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(statList);
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views;
        }

        public async Task<IEnumerable<WorkItemView>> QueryViews(int eNodebId, byte sectorId)
        {
            var statList = await _repository.GetAllListAsync(eNodebId, sectorId);
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(statList);
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views;
        }

        public IEnumerable<WorkItemChartView> QueryViews()
        {
            var views = Mapper.Map<List<WorkItem>, List<WorkItemView>>(_repository.GetAllList());
            views.ForEach(x => x.UpdateTown(_eNodebRepository, _btsRepository, _townRepository));
            return views.MapTo<IEnumerable<WorkItemChartView>>();
        }

        public void FeedBack(string userName, string message, string serialNumber)
        {
            var item = _repository.GetAll().FirstOrDefault(x => x.SerialNumber == serialNumber);
            if (item != null)
            {
                var now = DateTime.Now;
                item.FeedbackContents += "[" + now + "]" + userName + ":" + message;
                item.FeedbackTime = now;
                _repository.Update(item);
            }
        }
    }
}
