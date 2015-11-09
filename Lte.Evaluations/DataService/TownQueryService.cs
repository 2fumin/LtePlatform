using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService
{
    public static class TownQueryService
    {
        public static IEnumerable<Town> QueryTowns(this ITownRepository townRepository,
            string city, string district, string town)
        {
            const string flag = "=All=";
            city = city ?? flag;
            district = district ?? flag;
            town = town ?? flag;
            return townRepository.GetAllList().Where(x =>
                (x.TownName == town || town.IndexOf(flag, StringComparison.Ordinal) >= 0)
                && (x.DistrictName == district || district.IndexOf(flag, StringComparison.Ordinal) >= 0)
                && (x.CityName == city || city.IndexOf(flag, StringComparison.Ordinal) >= 0));
        }

    }
}
