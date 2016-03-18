using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Parameters.Entities.Basic;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Switch
{
    public class IntraFreqHoGroup : IEntity<ObjectId>, IHuaweiMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int IntraFreqHoA3Hyst { get; set; }

        public int IntraFreqHoA3TimeToTrig { get; set; }

        public int LocalCellId { get; set; }

        public int IntraFreqHoA3Offset { get; set; }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int IntraFreqHoGroupId { get; set; }
    }
}
