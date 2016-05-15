using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Mr
{
    public class CellStastic : IEntity<ObjectId>, ICellStastic
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int ENodebId { get; set; }

        public int Pci { get; set; }

        public int Mod3Count { get; set; }

        public int WeakCoverCount { get; set; }

        public int Mod6Count { get; set; }

        public DateTime CurrentDate { get; set; }

        public int OverCoverCount { get; set; }

        public int PreciseCount { get; set; }

        public int MrCount { get; set; }
    }

}
