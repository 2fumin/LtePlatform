using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Mr
{
    public class InterferenceMatrixMongo
    {
        public ObjectId _id { get; set; }

        public double? INTERF_ONLY_COFREQ { get; set; }

        public string current_date { get; set; }

        public int? MOD3_COUNT { get; set; }

        public int? MOD6_COUNT { get; set; }

        public int? OVERCOVER_COFREQ_6DB { get; set; }

        public string ENODEBID_PCI_NPCI_NFREQ { get; set; }

        public double? OVERCOVER_COFREQ_10DB { get; set; }
    }
}
