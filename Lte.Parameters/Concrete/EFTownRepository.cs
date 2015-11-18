using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFTownRepository : LightWeightRepositroyBase<Town>, ITownRepository
    {
        protected override DbSet<Town> Entities => context.Towns;

        public IEnumerable<Town> QueryTowns(string city, string district, string town)
        {
            const string flag = "=All=";
            city = city ?? flag;
            district = district ?? flag;
            town = town ?? flag;
            return GetAllList(x =>
                (x.TownName == town || town.IndexOf(flag, StringComparison.Ordinal) >= 0)
                && (x.DistrictName == district || district.IndexOf(flag, StringComparison.Ordinal) >= 0)
                && (x.CityName == city || city.IndexOf(flag, StringComparison.Ordinal) >= 0));
        }

        public IEnumerable<Town> GetAll(string city)
        {
            return GetAll().Where(x => x.CityName == city);
        }
    }
}
