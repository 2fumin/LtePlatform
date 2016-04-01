using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class InterferenceMongoRepository : MongoDbRepositoryBase<InterferenceMatrixMongo, ObjectId>, IInterferenceMongoRepository
    {
        public InterferenceMongoRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellInterfMatrix";
        }

        public InterferenceMongoRepository() : this(new MyMongoProvider("yaoyq"))
        {
            
        }

        public List<InterferenceMatrixMongo> GetByENodebInfo(string eNodebInfo)
        {
            var query = MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.EQ(e => e.ENODEBID_PCI_NPCI_NFREQ, eNodebInfo);
            return Collection.Find(query).AsQueryable().ToList();
        }
        
        public InterferenceMatrixMongo GetOne(string eNodebInfo, string timeString)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(
                    e => e.ENODEBID_PCI_NPCI_NFREQ == eNodebInfo && e.current_date == timeString);
            return Collection.FindOne(query);
        }

        public InterferenceMatrixMongo GetOne(int eNodebId, short pci)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(
                    e => e.ENODEBID_PCI_NPCI_NFREQ.StartsWith(eNodebId + "_" + pci));
            return Collection.FindOne(query);
        }

        public InterferenceMatrixMongo GetOne(int eNodebId, short pci, string dateString)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(
                    e => e.ENODEBID_PCI_NPCI_NFREQ.StartsWith(eNodebId + "_" + pci)
                    && e.current_date.StartsWith(dateString));
            return Collection.FindOne(query);
        }

        public List<InterferenceMatrixMongo> GetList(int eNodebId, short pci)
        {
            var query =
                MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Where(
                    e => e.ENODEBID_PCI_NPCI_NFREQ.StartsWith(eNodebId + "_" + pci));
            return Collection.Find(query).AsQueryable().ToList();
        }

        public List<InterferenceMatrixMongo> GetList(int eNodebId, short pci, DateTime time)
        {
            var timeString = time.ToString("yyyyMMdd");
            return GetList(eNodebId, pci, timeString);
        }

        public List<InterferenceMatrixMongo> GetList(int eNodebId, short pci, string dateString)
        {
            var query1 = MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Matches(e => e.ENODEBID_PCI_NPCI_NFREQ,
                new BsonRegularExpression("^" + eNodebId + "_" + pci + "_"));
            var query2 = MongoDB.Driver.Builders.Query<InterferenceMatrixMongo>.Matches(e => e.current_date,
                new BsonRegularExpression("^" + dateString));
            var query = MongoDB.Driver.Builders.Query.And(query1, query2);
            var cursor = Collection.Find(query);
            return cursor.ToList();
        }
    }
}
