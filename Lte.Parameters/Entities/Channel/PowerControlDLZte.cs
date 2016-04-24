using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Channel
{
    public class PowerControlDLZte : IEntity<ObjectId>, IZteMongo
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

        public int paForPCCH { get; set; }

        public int paForBCCH { get; set; }

        public int paForDTCH { get; set; }

        public string pdcchF1DPwrOfst { get; set; }

        public string pdcchF2PwrOfst { get; set; }

        public string pdcchF1PwrOfst { get; set; }

        public string pdcchF1CPwrOfst { get; set; }

        public string pdcchF3PwrOfst { get; set; }

        public string pdcchF3APwrOfst { get; set; }

        public string pdcchF0PwrOfst { get; set; }

        public int pdschCLPCSwchDl { get; set; }

        public int paForMSG2 { get; set; }

        public string pdcchF1BPwrOfst { get; set; }

        public int pcfichPwrOfst { get; set; }

        public int paForCCCH { get; set; }

        public int PowerControlDL { get; set; }

        public int phichPwrOfst { get; set; }

        public string pdcchF1APwrOfst { get; set; }

        public string pdcchF2APwrOfst { get; set; }

        public int paForDCCH { get; set; }

        public int csiRSPwrOfst { get; set; }
    }
}
