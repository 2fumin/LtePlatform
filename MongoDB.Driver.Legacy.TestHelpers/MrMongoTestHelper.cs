using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities.Mr;
using MongoDB.Bson;

namespace MongoDB.Driver.Legacy.TestHelpers
{
    public class MrMongoTestHelper
    {
        public static MongoClient MrMongoClient { get; } = new MongoClient("mongodb://root:Abcdef9*@10.17.165.106:27017");

        public static IMongoDatabase MrMongoDatabase { get; private set; }

        public static IMongoCollection<BsonDocument> MrCollection { get; private set; }

        public static IMongoCollection<InterferenceMatrixMongo> MrMongoCollection { get; private set; } 

        public MrMongoTestHelper()
        {
            if (MrMongoDatabase == null)
                MrMongoDatabase = MrMongoClient.GetDatabase("yaoyq");
            if (MrCollection == null)
                MrCollection = MrMongoDatabase.GetCollection<BsonDocument>("CellInterfMatrix");
            if (MrMongoCollection == null)
                MrMongoCollection = MrMongoDatabase.GetCollection<InterferenceMatrixMongo>("CellInterfMatrix");
        }

        public Task<List<BsonDocument>> GetInterferenceInfos(string eNodebInfo)
        {
            return MrCollection.Find(new BsonDocument("ENODEBID_PCI_NPCI_NFREQ", eNodebInfo)).ToListAsync();
        }

        public Task<List<InterferenceMatrixMongo>> GetMongoInfos(string eNodebInfo)
        {
            return MrMongoCollection.Find(x => x.ENODEBID_PCI_NPCI_NFREQ == eNodebInfo).ToListAsync();
        }

        public Task<List<BsonDocument>> GetInterferenceInfosByTime(string timeString)
        {
            return MrCollection.Find(new BsonDocument("current_date", timeString)).ToListAsync();
        }

        public Task<List<BsonDocument>> GetInterferenceInfosByENodebAndTime(string eNodebInfo, string timeString)
        {
            return
                MrCollection.Find(
                    new BsonDocument(new List<BsonElement>
                    {
                        new BsonElement("ENODEBID_PCI_NPCI_NFREQ", eNodebInfo),
                        new BsonElement("current_date", timeString)
                    })).ToListAsync();
        }
    }
}
