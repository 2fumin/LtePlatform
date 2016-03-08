using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels.Kpi;

namespace Lte.Evaluations.Policy
{
    public static class OrderTopConnection3GService
    {
        public enum OrderTopConnection3GPolicy
        {
            OrderByConnectionFailsDescending,
            OrderByConnectionRate,
            OrderByTopDatesDescending
        }

        public static Dictionary<string, OrderTopConnection3GPolicy> OrderSelectionList
            => new Dictionary<string, OrderTopConnection3GPolicy>
        {
            {"按照连接失败次数降序排列", OrderTopConnection3GPolicy.OrderByConnectionFailsDescending },
            {"按照连接成功率升序排列", OrderTopConnection3GPolicy.OrderByConnectionRate },
            {"按照出现次数降序排列", OrderTopConnection3GPolicy.OrderByTopDatesDescending }
        };

        public static IEnumerable<TopConnection3GTrendView> Order(this IEnumerable<TopConnection3GTrendView> stats,
            OrderTopConnection3GPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderTopConnection3GPolicy.OrderByConnectionFailsDescending:
                    return stats.OrderByDescending(x => x.ConnectionFails).Take(topCount);
                case OrderTopConnection3GPolicy.OrderByConnectionRate:
                    return stats.OrderBy(x => x.ConnectionRate).Take(topCount);
                default:
                    return stats.OrderByDescending(x => x.TopDates).Take(topCount);
            }
        }

        public static OrderTopConnection3GPolicy GetTopConnection3GPolicy(this string selection)
        {
            return OrderSelectionList.ContainsKey(selection)
                ? OrderSelectionList[selection]
                : OrderTopConnection3GPolicy.OrderByConnectionRate;
        }
    }
}
