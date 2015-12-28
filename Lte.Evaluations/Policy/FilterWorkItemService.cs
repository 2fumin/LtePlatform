using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities.Work;

namespace Lte.Evaluations.Policy
{
    public static class FilterWorkItemService
    {
        public static Expression<Func<WorkItem, bool>> GetWorkItemFilter(this string filerDescription)
        {
            switch (filerDescription)
            {
                case "未完成_全部":
                    return x => x.State != WorkItemState.Finished;
                case "全部_2/3G":
                    return x => x.Type == WorkItemType.Kpi2G || x.Type == WorkItemType.NetworkProblem;
                case "全部_4G":
                    return
                        x =>
                            x.Type == WorkItemType.Infrastructure4G || x.Type == WorkItemType.Interference4G ||
                            x.Type == WorkItemType.Kpi4G || x.Type == WorkItemType.RrcConnection;
                case "未完成_2/3G":
                    return
                        x =>
                            x.State != WorkItemState.Finished &&
                            (x.Type == WorkItemType.Kpi2G || x.Type == WorkItemType.NetworkProblem);
                case "未完成_4G":
                    return
                        x =>
                            x.State != WorkItemState.Finished &&
                            (x.Type == WorkItemType.Infrastructure4G || x.Type == WorkItemType.Interference4G ||
                             x.Type == WorkItemType.Kpi4G || x.Type == WorkItemType.RrcConnection);
                default:
                    return null;
            }
        } 
    }
}
