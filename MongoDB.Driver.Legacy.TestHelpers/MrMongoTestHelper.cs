using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Driver.Legacy.TestHelpers
{
    public class MrMongoTestHelper
    {
        public static MongoClient MrMongoClient { get; } = new MongoClient("mongodb://root:Abcdef9*@10.17.165.106:27017");

        public static MongoDatabase MrMongoDatabase { get; }

        public static MongoCollection
    }
}
