using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ITownRepository : IRepository<Town>
    {
        IEnumerable<Town> QueryTowns(string city, string district, string town);

        Town QueryTown(string city, string district, string town);

        Town QueryTown(string district, string town);

        List<Town> GetAll(string city);
    }
}
