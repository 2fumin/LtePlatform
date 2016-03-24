using Abp.Domain.Entities;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Neighbor
{
    public class ExternalEUtranCellFDDZte : IEntity<ObjectId>, IZteMongo
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

        public double earfcnDl { get; set; }

        public int cellType { get; set; }

        public int pci { get; set; }

        public string userLabel { get; set; }

        public int antPort1 { get; set; }

        public int cellLocalId { get; set; }

        public int plmnIdList_mcc { get; set; }

        public int switchSurportTrunking { get; set; }

        public int tac { get; set; }

        public int plmnIdList_mnc { get; set; }

        public string reservedByEUtranRelation { get; set; }

        public int bandWidthUl { get; set; }

        public int mcc { get; set; }

        public int eNBId { get; set; }

        public double earfcnUl { get; set; }

        public int freqBandInd { get; set; }

        public int voLTESwch { get; set; }

        public int coMPFlagUl { get; set; }

        public int bandWidthDl { get; set; }

        public int mnc { get; set; }

        public string addiFreqBand { get; set; }

        public int ExternalEUtranCellFDD { get; set; }
    }
}
