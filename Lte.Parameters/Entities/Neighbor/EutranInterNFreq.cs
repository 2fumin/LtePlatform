using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Entities.Neighbor
{
    public class EutranInterNFreq : IHuaweiMongo
    {
        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int ThreshXlow { get; set; }

        public int MeasFreqPriority { get; set; }

        public int EutranReselTime { get; set; }

        public int LocalCellId { get; set; }

        public int FreqPriBasedHoMeasFlag { get; set; }

        public int MasterBandFlag { get; set; }

        public int CtrlMode { get; set; }

        public int PmaxCfgInd { get; set; }

        public int UlTrafficMlbTargetInd { get; set; }

        public int FreqPriorityForAnr { get; set; }

        public int MlbTargetInd { get; set; }

        public int InterFreqHighSpeedFlag { get; set; }

        public int ConnFreqPriority { get; set; }

        public int CellReselPriorityCfgInd { get; set; }

        public int VoipPriority { get; set; }

        public int SpeedDependSPCfgInd { get; set; }

        public int QqualMinCfgInd { get; set; }

        public int MlbFreqPriority { get; set; }

        public int DlEarfcn { get; set; }

        public int UlTrafficMlbPriority { get; set; }

        public int AnrInd { get; set; }

        public int ThreshXlowQ { get; set; }

        public int InterFreqHoEventType { get; set; }

        public int IdleMlbUEReleaseRatio { get; set; }

        public int QRxLevMin { get; set; }

        public int UlEarfcnCfgInd { get; set; }

        public int NeighCellConfig { get; set; }

        public int QoffsetFreqConn { get; set; }

        public int MeasBandWidth { get; set; }

        public int PresenceAntennaPort1 { get; set; }

        public int BackoffTargetInd { get; set; }

        public int ThreshXhigh { get; set; }

        public int MlbInterFreqHoEventType { get; set; }

        public int ThreshXhighQ { get; set; }

        public int IfHoThdRsrpOffset { get; set; }

        public int InterFreqRanSharingInd { get; set; }

        public int IfMlbThdRsrpOffset { get; set; }

        public int QoffsetFreq { get; set; }
    }
}
