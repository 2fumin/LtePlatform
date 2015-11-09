using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Geo
{
    public interface ITown
    {
        string CityName { get; set; }

        string DistrictName { get; set; }

        string TownName { get; set; }
    }

    public static class ITownQueries
    {
        public static bool IsAddConditionsValid(this ITown addConditions)
        {
            return !(string.IsNullOrEmpty(addConditions.CityName) || string.IsNullOrEmpty(addConditions.DistrictName)
                || string.IsNullOrEmpty(addConditions.TownName));
        }

        public static string GetAddConditionsInfo(this ITown addConditions)
        {
            return addConditions.CityName + "-" + addConditions.DistrictName + "-" + addConditions.TownName;
        }
    }
}
