using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Mr;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Mr
{
    public class CellStasticRepository : MongoDbRepositoryBase<CellStastic,ObjectId>, ICellStasticRepository
    {
        public CellStasticRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellStastic";
        }

        public CellStasticRepository() : this(new MyMongoProvider("yaoyq"))
        {

        }

        public List<CellStastic> GetList(int eNodebId, short pci)
        {
            var query =
                MongoDB.Driver.Builders.Query<CellStastic>.Where(
                    e => e.ENodebId == eNodebId && e.Pci == pci);
            return Collection.Find(query).AsQueryable().ToList();
        }

        public List<CellStastic> GetList(int eNodebId, short pci, DateTime date)
        {
            var nextDate = date.AddDays(1);
            var query =
                MongoDB.Driver.Builders.Query<CellStastic>.Where(
                    e => e.ENodebId == eNodebId && e.Pci == pci && e.CurrentDate >= date && e.CurrentDate < nextDate);
            return Collection.Find(query).AsQueryable().ToList();
        }
    }
}
