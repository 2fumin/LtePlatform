using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Work;

namespace Lte.Parameters.Abstract
{
    public interface IWorkItemRepository : IRepository<WorkItem>
    {
        void Import(WorkItemExcel itemExcel);
    }
}
