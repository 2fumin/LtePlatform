using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFTownRepository : EfRepositoryBase<EFParametersContext, Town>, ITownRepository
    {
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

        public Town QueryTown(string city, string district, string town)
        {
            return FirstOrDefault(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
        }

        public Town QueryTown(string district, string town)
        {
            return FirstOrDefault(x => x.DistrictName == district && x.TownName == town);
        }

        public List<Town> GetAll(string city)
        {
            return GetAllList(x => x.CityName == city);
        }

        public EFTownRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
