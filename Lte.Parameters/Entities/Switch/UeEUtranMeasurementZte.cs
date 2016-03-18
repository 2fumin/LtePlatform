using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Switch
{
    public class UeEUtranMeasurementZte : IEntity<ObjectId>, IMongoZte
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int eNodeB_Id { get; set; }

        public string eNodeB_Name { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public string parentLDN { get; set; }

        public string description { get; set; }

        public int prdReportInterval { get; set; }

        public int reportAmount { get; set; }

        public int triggerQuantity { get; set; }

        public int a5Threshold2OfRSRP { get; set; }

        public int a6Offset { get; set; }

        public int reportQuantity { get; set; }

        public int maxReportCellNum { get; set; }

        public int timeToTrigger { get; set; }

        public int a5Threshold2OfRSRQ { get; set; }

        public int thresholdOfRSRQ { get; set; }

        public int prdReportAmount { get; set; }

        public int measCfgIdx { get; set; }

        public int reportInterval { get; set; }

        public int reportCriteria { get; set; }

        public int a3Offset { get; set; }

        public int measCfgFunc { get; set; }

        public int hysteresis { get; set; }

        public int prdRptRurpose { get; set; }

        public int eventId { get; set; }

        public int thresholdOfRSRP { get; set; }

        public int reportOnLeave { get; set; }

        public int UeEUtranMeasurement { get; set; }
    }
}
