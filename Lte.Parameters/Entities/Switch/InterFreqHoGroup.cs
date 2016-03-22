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
    public class InterFreqHoGroup : IEntity<ObjectId>, IHuaweiMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int InterFreqHoA1ThdRsrq { get; set; }

        public int FreqPriInterFreqHoA1ThdRsrp { get; set; }

        public int LocalCellId { get; set; }

        public int FreqPriInterFreqHoA1ThdRsrq { get; set; }

        public int InterFreqMlbA1A2ThdRsrp { get; set; }

        public int InterFreqHoA5Thd1Rsrq { get; set; }

        public int InterFreqHoA4Hyst { get; set; }

        public int SrvReqHoA4ThdRsrp { get; set; }

        public int InterFreqHoA1ThdRsrp { get; set; }

        public int InterFreqHoA1A2Hyst { get; set; }

        public int InterFreqHoGroupId { get; set; }

        public int UlHeavyTrafficMlbA4ThdRsrp { get; set; }

        public int InterFreqHoA4TimeToTrig { get; set; }

        public int FreqPriInterFreqHoA2ThdRsrp { get; set; }

        public int A3InterFreqHoA2ThdRsrp { get; set; }

        public int InterFreqLoadBasedHoA4ThdRsrq { get; set; }

        public int FreqPriInterFreqHoA2ThdRsrq { get; set; }

        public int UlBadQualHoA4Offset { get; set; }

        public int UlHeavyTrafficMlbA4ThdRsrq { get; set; }

        public int A3InterFreqHoA2ThdRsrq { get; set; }

        public int A3InterFreqHoA1ThdRsrp { get; set; }

        public int InterFreqHoA3Offset { get; set; }

        public int InterFreqHoA1A2TimeToTrig { get; set; }

        public int InterFreqHoA4ThdRsrp { get; set; }

        public int MlbInterFreqHoA5Thd1Rsrp { get; set; }

        public int MlbInterFreqHoA5Thd1Rsrq { get; set; }

        public int InterFreqHoA2ThdRsrq { get; set; }

        public int A3InterFreqHoA1ThdRsrq { get; set; }

        public int InterFreqHoA4ThdRsrq { get; set; }

        public int InterFreqLoadBasedHoA4ThdRsrp { get; set; }

        public int InterFreqHoA5Thd1Rsrp { get; set; }

        public int SrvReqHoA4ThdRsrq { get; set; }

        public int InterFreqHoA2ThdRsrp { get; set; }
    }
}
