using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Mr
{
    public class InterferenceMatrixMongo : IEntity<ObjectId>
    {
        public ObjectId Id { get; set; }
        
        public bool IsTransient()
        {
            return false;
        }
        
        public int ENodebId { get; set; }

        public int Pci { get; set; }
        
        public int? Over10db { get; set; }

        public int? Mod3Count { get; set; }

        public int? Over6db { get; set; }

        public int? Mod6Count { get; set; }

        public DateTime CurrentDate { get; set; }

        public double? InterfLevel { get; set; }

        public int NeighborFreq { get; set; }

        public int NeighborPci { get; set; }
    }
}
