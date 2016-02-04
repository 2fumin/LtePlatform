using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MongoDb;
using MongoDB.Driver;

namespace Lte.Parameters.Concrete
{
    public class MyMongoProvider : IMongoDatabaseProvider
    {
        private static readonly MongoClient Client = new MongoClient("mongodb://root:Abcdef9*@10.17.165.106:27017");
        private static MongoDatabase _database;

        public MyMongoProvider(string databaseString)
        {
            if (_database != null) return;
#pragma warning disable 618
            var server = Client.GetServer();
#pragma warning restore 618
            _database = server.GetDatabase(databaseString);
        }

        public MongoDatabase Database => _database;
    }
}
