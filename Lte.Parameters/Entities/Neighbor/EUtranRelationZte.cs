using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Neighbor
{
    public class EUtranRelationZte : IEntity<ObjectId>, IZteMongo
    {
        public int eNodeB_Id { get; set; }

        public string eNodeB_Name { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public string parentLDN { get; set; }

        public string description { get; set; }

        public int resPRBDown { get; set; }

        public int resPRBUp { get; set; }

        public int overlapCoverage { get; set; }

        public int shareCover { get; set; }

        public int numRRCCntNumCov { get; set; }

        public int lbIntraMeasureOffset { get; set; }

        public int isX2HOAllowed { get; set; }

        public string userLabel { get; set; }

        public int coperType { get; set; }

        public int isAnrCreated { get; set; }

        public int s1DataFwdFlag { get; set; }

        public int isHOAllowed { get; set; }

        public int switchonTimeWindow { get; set; }

        public int nCelPriority { get; set; }

        public int EUtranRelation { get; set; }

        public int isESCoveredBy { get; set; }

        public int stateInd { get; set; }

        public string refEUtranCellFDD { get; set; }

        public int cellIndivOffset { get; set; }

        public int isRemoveAllowed { get; set; }

        public int qofStCell { get; set; }

        public int esSwitch { get; set; }

        public int coverESCell { get; set; }

        public string refExternalEUtranCellFDD { get; set; }

        public string supercellFlag { get; set; }

        public string refExternalEUtranCellTDD { get; set; }
        
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }
    }
}
