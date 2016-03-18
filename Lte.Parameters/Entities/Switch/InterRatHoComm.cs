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
    public class InterRatHoComm : IEntity<ObjectId>, IHuaweiMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int InterRatHoMaxRprtCell { get; set; }

        public int Cdma20001XrttMeasTimer { get; set; }

        public int Cdma2000HrpdFreqSelMode { get; set; }

        public int InterRatCdmaHrpdRprtInterval { get; set; }

        public int InterRatHoA1A2TrigQuan { get; set; }

        public int InterRatHoCdma1xRttB1MeasQuan { get; set; }

        public int InterRatHoCdmaHrpdB1MeasQuan { get; set; }

        public int InterRatHoRprtAmount { get; set; }

        public int UtranCellNumForEmcRedirect { get; set; }

        public int Cdma20001XrttMeasMode { get; set; }

        public int Cdma20001XrttJudgePnNum { get; set; }

        public int IRatBlindRedirPlmnCfgSimSw { get; set; }

        public int CdmaHrpdSectorIdSelMode { get; set; }

        public int CellInfoMaxUtranCellNum { get; set; }

        public int GeranCellNumForEmcRedirect { get; set; }

        public int InterRatCdma1xRttRprtInterval { get; set; }

        public int CdmaEcsfbPsConcurrentMode { get; set; }

        public int InterRatHoEventType { get; set; }

        public int InterRatHoGeranRprtInterval { get; set; }

        public int InterRatHoUtranB1MeasQuan { get; set; }

        public int Cdma20001XrttFreqSelMode { get; set; }

        public int CellInfoMaxGeranCellNum { get; set; }

        public int objId { get; set; }

        public int Cdma1XrttSectorIdSelMode { get; set; }

        public int InterRatHoUtranRprtInterval { get; set; }
    }
}
