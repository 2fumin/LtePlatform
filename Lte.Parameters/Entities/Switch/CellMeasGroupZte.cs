using Abp.Domain.Entities;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Switch
{
    public class CellMeasGroupZte : IEntity<ObjectId>, IZteMongo, IZteMeasGroup
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int rsrpPeriodMeasCfgIdDl { get; set; }

        public string intraFHOMeasCfg { get; set; }

        public int lowSpeedInHighwayMeasCfg { get; set; }

        public string closedInterFMeasCfg { get; set; }

        public int tdCSFBMeasCfg { get; set; }

        public int ueRxTxTimeDiffPeriodMeasCfg { get; set; }

        public int interRatUTRANPeriodMeasCfg { get; set; }

        public int tdsLBMeasCfg { get; set; }

        public string tdVoiceMeasCfg { get; set; }

        public string openRatFMeasCfg { get; set; }

        public int modScellMeasCfg { get; set; }

        public int rsrpEventMeasCfgIdDl { get; set; }

        public string cdma2KHRPDMeasCfg { get; set; }

        public int cdmaANRMeasCfg { get; set; }

        public string geranVoiceMeasCfg { get; set; }

        public int cdma2K1xCSFBMeasCfg { get; set; }

        public int geranANRMeasCfg { get; set; }

        public string cdma2K1xMeasCfg { get; set; }

        public int pcellMeasCfg { get; set; }

        public int compMeasCfgDL { get; set; }

        public string tdMeasCfg { get; set; }

        public int cdmaSRVCCBasedVoLTEQuaMeasCfg { get; set; }

        public int addScellMeasCfg { get; set; }

        public string description { get; set; }

        public int eNodeB_Id { get; set; }

        public int eICICMeasCfg { get; set; }

        public int utranANRMeasCfg { get; set; }

        public int rptCGIMeasCfg { get; set; }

        public string openRedMeasCfg { get; set; }

        public int tdsSRVCCBasedVoLTEQuaMeasCfg { get; set; }

        public int gsmLBMeasCfg { get; set; }

        public int icicMeasCfg { get; set; }

        public int intraFPeriodMeasCfg { get; set; }

        public string eNodeB_Name { get; set; }

        public int interRatGSMPeriodMeasCfg { get; set; }

        public int highSpeedInNmlCellMeasCfg { get; set; }

        public string geranMeasCfg { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public int wcdmaLBMeasCfg { get; set; }

        public string parentLDN { get; set; }

        public int CellMeasGroup { get; set; }

        public string wcdmaMeasCfg { get; set; }

        public string userLabel { get; set; }

        public int meaGroupId { get; set; }

        public int anrA2MeasCfg { get; set; }

        public int macroSmallMeasCfg { get; set; }

        public string wcdmaVoiceMeasCfg { get; set; }

        public int intraLBMeasCfg { get; set; }

        public string reservedByEUtranCellMeasurement { get; set; }

        public int rmvScellMeasCfg { get; set; }

        public int geranSRVCCBasedVoLTEQuaMeasCfg { get; set; }

        public int rrcAccessLBMeasCfg { get; set; }

        public int gsmCSFBMeasCfg { get; set; }

        public string interFHOMeasCfg { get; set; }

        public int interFHOBasedVoLTEQuaMeasCfg { get; set; }

        public int interFPeriodMeasCfg { get; set; }

        public string opeRatVoiceMeasCfg { get; set; }

        public int wcdmaCSFBMeasCfg { get; set; }

        public int interFVoiceMeasCfg { get; set; }

        public int compMeasCfgUL { get; set; }

        public int measCfg4movUE { get; set; }

        public int anrMeasCfg { get; set; }

        public int iratANRA2MeasCfg { get; set; }

        public string openInterFMeasCfg { get; set; }

        public int intraLBMeasExtCfg { get; set; }
    }
}
