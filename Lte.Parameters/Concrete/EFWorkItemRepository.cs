using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Work;

namespace Lte.Parameters.Concrete
{
    public class EFWorkItemRepository : PagingRepositoryBase<WorkItem>, IWorkItemRepository
    {
        protected override DbSet<WorkItem> Entities => context.WorkItems;

        public async Task<List<WorkItem>> GetAllListAsync(int eNodebId)
        {
            return await GetAllListAsync(x => x.ENodebId == eNodebId);
        }

        public async Task<List<WorkItem>> GetAllListAsync(DateTime begin, DateTime end)
        {
            return await GetAllListAsync(x => x.BeginTime > begin && x.BeginTime <= end);
        }

        public async Task<List<WorkItem>> GetAllListAsync(int eNodebId, byte sectorId)
        {
            return await GetAllListAsync(x => x.ENodebId == eNodebId && x.SectorId == sectorId);
        }

        public void Import(WorkItemExcel itemExcel)
        {
            var stat = FirstOrDefault(x => x.SerialNumber == itemExcel.SerialNumber);
            if (stat == null) return;
            var info = Mapper.Map<WorkItemExcel, WorkItem>(itemExcel);
            stat.Comments = info.Comments;
            
            stat.FeedbackTime = info.FeedbackTime;
            stat.FinishTime = info.FinishTime;
            stat.RejectTimes = info.RejectTimes;
            stat.RepeatTimes = info.RepeatTimes;
            stat.State = info.State;
            stat.Type = info.Type;
            stat.Subtype = info.Subtype;

            Update(stat);
        }


    }
}
