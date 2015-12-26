using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
