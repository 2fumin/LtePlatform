using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Work;

namespace Lte.Parameters.Concrete
{
    public class EFWorkItemRepository : LightWeightRepositroyBase<WorkItem>, IWorkItemRepository
    {
        private readonly EFParametersContext _context = new EFParametersContext();

        protected override DbSet<WorkItem> Entities => _context.WorkItems;

        public int Import(IEnumerable<WorkItemExcel> itemExcels)
        {
            foreach (var item in itemExcels.Select(Mapper.Map<WorkItemExcel, WorkItem>))
            {
                InsertOrUpdateAsync(item);
            }
            return itemExcels.Count();
        }
    }
}
