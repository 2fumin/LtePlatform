using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities.Switch;

namespace Lte.Parameters.Entities.Neighbor
{
    public class EUtranRelation : IMongoZte
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
    }
}
