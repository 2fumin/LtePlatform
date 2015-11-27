using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.Policy
{
    public static class OrderTopDrop2GService
    {
        public enum OrderTopDrop2GPolicy
        {
            OrderByDropsDescending,
            OrderByDropRateDescending
        }

        public static Dictionary<string,OrderTopDrop2GPolicy> OrderSelectionList=>new Dictionary<string,OrderTopDrop2GPolicy>
        {
            {"按照掉话次数降序排列", OrderTopDrop2GPolicy.OrderByDropsDescending },
            {"按照掉话率降序排列", OrderTopDrop2GPolicy.OrderByDropRateDescending }
        };

        public static IEnumerable<TopDrop2GCellView> Order(this IEnumerable<TopDrop2GCellView> stats, OrderTopDrop2GPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderTopDrop2GPolicy.OrderByDropRateDescending:
                    return stats.OrderByDescending(x => x.DropRate).Take(topCount);
                default:
                    return stats.OrderByDescending(x => x.Drops).Take(topCount);
            }
        }

        public static OrderTopDrop2GPolicy GetTopDrop2GPolicy(this string selection)
        {
            return OrderSelectionList.ContainsKey(selection)
                ? OrderSelectionList[selection]
                : OrderTopDrop2GPolicy.OrderByDropRateDescending;
        }
    }
}
