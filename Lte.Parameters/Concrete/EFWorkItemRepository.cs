using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Work;

namespace Lte.Parameters.Concrete
{
    public class EFWorkItemRepository : PagingRepositoryBase<WorkItem>, IWorkItemRepository
    {
        protected override DbSet<WorkItem> Entities => context.WorkItems;

        public void Import(WorkItemExcel itemExcel)
        {
            var stat = FirstOrDefault(x => x.SerialNumber == itemExcel.SerialNumber);
            if (stat == null) return;
            var info = Mapper.Map<WorkItemExcel, WorkItem>(itemExcel);
            stat.Comments = info.Comments;
            stat.FeedbackContents = info.FeedbackContents;
            stat.FeedbackTime = info.FeedbackTime;
            stat.FinishTime = info.FinishTime;
            stat.RejectTimes = info.RejectTimes;
            stat.RepeatTimes = info.RepeatTimes;
            stat.State = info.State;

            Update(stat);
        }
    }
}
