using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.Policy
{
    public static class OrderPreciseStatService
    {
        public enum OrderPreciseStatPolicy : byte
        {
            OrderBySecondRate,
            OrderBySecondNeighborsDescending,
            OrderByFirstRate,
            OrderByFirstNeighborsDescending,
            OrderByTotalMrsDescending
        }

        public static Dictionary<string, OrderPreciseStatPolicy> OrderSelectionList => new Dictionary<string, OrderPreciseStatPolicy>
        {
            { "按照精确覆盖率升序", OrderPreciseStatPolicy.OrderBySecondNeighborsDescending},
            { "按照第二邻区数量降序", OrderPreciseStatPolicy.OrderBySecondRate},
            { "按照第一邻区精确覆盖率升序", OrderPreciseStatPolicy.OrderByFirstNeighborsDescending},
            { "按照第一邻区数量降序", OrderPreciseStatPolicy.OrderByFirstRate},
            { "按照总测量报告数降序", OrderPreciseStatPolicy.OrderByTotalMrsDescending}
        };

        public static List<PreciseCoverage4G> Order(this IEnumerable<PreciseCoverage4G> result, OrderPreciseStatPolicy policy,
            int topCount)
        {
            switch (policy)
            {
                case OrderPreciseStatPolicy.OrderBySecondRate:
                    return result.OrderBy(x => x.SecondRate)
                        .Take(topCount).ToList();
                case OrderPreciseStatPolicy.OrderBySecondNeighborsDescending:
                    return result.OrderByDescending(x => x.SecondNeighbors)
                        .Take(topCount).ToList();
                case OrderPreciseStatPolicy.OrderByFirstRate:
                    return result.OrderBy(x => x.FirstRate)
                        .Take(topCount).ToList();
                case OrderPreciseStatPolicy.OrderByFirstNeighborsDescending:
                    return result.OrderByDescending(x => x.FirstNeighbors)
                        .Take(topCount).ToList();
                default:
                    return result.OrderByDescending(x => x.TotalMrs)
                        .Take(topCount).ToList();
            }
        }

        public static OrderPreciseStatPolicy GetPrecisePolicy(this string selection)
        {
            return OrderSelectionList.ContainsKey(selection)
                ? OrderSelectionList[selection]
                : OrderPreciseStatPolicy.OrderBySecondNeighborsDescending;
        }
    }
}
