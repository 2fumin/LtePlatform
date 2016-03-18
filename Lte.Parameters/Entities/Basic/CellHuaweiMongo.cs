using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Basic
{
    public class CellHuaweiMongo : IEntity<ObjectId>, IHuaweiMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int CellId { get; set; }

        public int LocalCellId { get; set; }

        public int FddTddInd { get; set; }

        public int PreambleFmt { get; set; }

        public int MultiRruCellFlag { get; set; }

        public int UlEarfcnCfgInd { get; set; }

        public int DlCyclicPrefix { get; set; }

        public int DlBandWidth { get; set; }

        public int UePowerMaxCfgInd { get; set; }

        public int UlCyclicPrefix { get; set; }

        public int CellScaleInd { get; set; }

        public int EuCellStandbyMode { get; set; }

        public int CellRadiusStartLocation { get; set; }

        public int HighSpeedFlag { get; set; }

        public string UserLabel { get; set; }

        public int AirCellFlag { get; set; }

        public int UlBandWidth { get; set; }

        public int CrsPortNum { get; set; }

        public int CellRadius { get; set; }

        public int CsgInd { get; set; }

        public int CellSpecificOffset { get; set; }

        public int RootSequenceIdx { get; set; }

        public int PhyCellId { get; set; }

        public int CustomizedBandWidthCfgInd { get; set; }

        public int EmergencyAreaIdCfgInd { get; set; }

        public int CnOpSharingGroupId { get; set; }

        public int AdditionalSpectrumEmission { get; set; }

        public int FreqPriorityForAnr { get; set; }

        public int IntraFreqAnrInd { get; set; }

        public int TxRxMode { get; set; }

        public int DlEarfcn { get; set; }

        public int WorkMode { get; set; }

        public int CellAdminState { get; set; }

        public int CellActiveState { get; set; }

        public int IntraFreqRanSharingInd { get; set; }

        public BsonDocument[] CellSlaveBand { get; set; }

        public int CPRICompression { get; set; }

        public string CellName { get; set; }

        public int objId { get; set; }

        public int FreqBand { get; set; }

        public int QoffsetFreq { get; set; }
    }
}
