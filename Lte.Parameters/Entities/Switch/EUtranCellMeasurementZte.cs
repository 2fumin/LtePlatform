using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Switch
{
    public class EUtranCellMeasurementZte : IEntity<ObjectId>, IZteMongo, IZteMeasGroup
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

        public int multiPLMNLocSt4PSSwch { get; set; }

        public string intraFHOMeasCfg { get; set; }

        public string openRedMeasCfg { get; set; }

        public string closedInterFMeasCfg { get; set; }

        public int filterCoeffRsrq { get; set; }

        public int measBaseVoiceSwch { get; set; }

        public int uePosiMeasSwch { get; set; }

        public int multiPLMNLocSt4ReestabSwch { get; set; }

        public int interRatGsmPeriodMeasSwitch { get; set; }

        public int eutranMeasParas_freqBandInd { get; set; }

        public int ratPriorityPara_ratPriority6 { get; set; }

        public string interFHOMeasCfg { get; set; }

        public int timeOfShortHO { get; set; }

        public int CDMA2000MeasParas_searchWindowPresent { get; set; }

        public string cdma2K1xMeasCfg { get; set; }

        public int lbMaxHOCell { get; set; }

        public int ratPriCnPara_ratPriCnCSFB1 { get; set; }

        public int ratPriorityPara_ratPriority5 { get; set; }

        public int ocs { get; set; }

        public int CDMA2000MeasParas_cdmaType { get; set; }

        public int interRatGSMPeriodMeasCfg { get; set; }

        public int qci1RedSwch { get; set; }

        public string tdMeasCfg { get; set; }

        public int utranANRMeasCfg { get; set; }

        public int CDMA2000MeasParas_cdmaFreqCsfbPriority { get; set; }

        public int cSFBbaseLAI { get; set; }

        public int offFreqPriorityPara_offFreqPriority5 { get; set; }

        public int eutranMeasParas_eutranFreqRdPriority { get; set; }

        public int pingPongHoSwitch { get; set; }

        public int dualMeasSwitch { get; set; }

        public int offFreqPriorityPara_offFreqPriority3 { get; set; }

        public int eutranMeasParas_interFVoiceAbility { get; set; }

        public int gsmLBMeasCfg { get; set; }

        public int cdmaCarriFreqNum { get; set; }

        public int CDMA2000MeasParas_cdmaARFCN { get; set; }

        public int intraFPeriodMeasCfg { get; set; }

        public int ratPriIdPara_ratPriIdleCSFB4 { get; set; }

        public string geranMeasCfg { get; set; }

        public int CDMA2000MeasParas_cdmaFreqRdPriority { get; set; }

        public int interRatUtranPeriodMeasSwitch { get; set; }

        public string openRatFMeasCfg { get; set; }

        public int gapDelay { get; set; }

        public int hoTarCellPRBThrd { get; set; }

        public int ratPriCnPara_ratPriCnCSFB3 { get; set; }

        public int measureThresh { get; set; }

        public int offFreqPriorityPara_offFreqPriority2 { get; set; }

        public int hoCandCelNum { get; set; }

        public int CDMA2000MeasParas_cdmaOffFreq { get; set; }

        public int ratPriorityPara_ratPriority4 { get; set; }

        public int ratSrvccPara_ratSrvcc4 { get; set; }

        public int caPcellMeasure { get; set; }

        public int offFreqPriorityPara_offFreqPriority7 { get; set; }

        public int intraLBMeasCfg { get; set; }

        public int ratPriIdPara_ratPriIdleCSFB3 { get; set; }

        public string refCellMeasGroup { get; set; }

        public int minStayTimeInSC { get; set; }

        public int eutranMeasParas_interFMeasBW { get; set; }

        public int intraFPeriodMeasSwitch { get; set; }

        public int ratPriCnPara_ratPriCnCSFB4 { get; set; }

        public int quantityFddUtra { get; set; }

        public int gsmCSFBMeasCfg { get; set; }

        public int macrotSmallForbidTime { get; set; }

        public int csfbMeasure { get; set; }

        public int csfbMethodofGSM { get; set; }

        public int interFPeriodMeasCfg { get; set; }

        public int cdmaANRMeasCfg { get; set; }

        public int ratPriorityPara_ratPriority3 { get; set; }

        public int geranANRMeasCfg { get; set; }

        public int neighCellConfig { get; set; }

        public int anrMeasCfg { get; set; }

        public int methodLTEtoUTRAN { get; set; }

        public int ratPriorityPara_ratPriority7 { get; set; }

        public int macroSmallIntraFreqMeasSwch { get; set; }

        public int filterCoeffUtra { get; set; }

        public int sceneOfRanShare { get; set; }

        public int blindA2Strategy { get; set; }

        public int interRatUTRANPeriodMeasCfg { get; set; }

        public int geranCarriFreqNum { get; set; }

        public double eutranMeasParas_interCarriFreq { get; set; }

        public int tarNeighCellRsrqThr { get; set; }

        public int rsrpEventMeasCfgIdDl { get; set; }

        public string cdma2KHRPDMeasCfg { get; set; }

        public int CDMA2000MeasParas_searchWinSize { get; set; }

        public int servCellRsrqThr { get; set; }

        public int tdsLBMeasCfg { get; set; }

        public int cdma2K1xCSFBMeasCfg { get; set; }

        public int mediumSpeedUserInd { get; set; }

        public int multiPLMNLocStCSFB { get; set; }

        public int interFHOBasedVoLTEQualSwch { get; set; }

        public int CDMA2000MeasParas_cdmaBandClass { get; set; }

        public int ratPriorityPara_ratPriority2 { get; set; }

        public int eutranMeasParas_offsetFreq { get; set; }

        public int EUtranCellMeasurement { get; set; }

        public int meas4HOBasedVoLTEQual { get; set; }

        public int lbIntraFreqPriority { get; set; }

        public int tdCSFBMeasCfg { get; set; }

        public int utranCarriFreqNum { get; set; }

        public int rsrpPeriodMeasCfgIdDl { get; set; }

        public string openInterFMeasCfg { get; set; }

        public int icicMeasCfg { get; set; }

        public int srvccBasedVoLTEQualitySwch { get; set; }

        public int eICICMeasCfg { get; set; }

        public int filterCoeffGera { get; set; }

        public int hoBasedSpeedSwch { get; set; }

        public string voiceMeasReCfgThrd { get; set; }

        public string explicitARFCN { get; set; }

        public int netWorkControlOrder { get; set; }

        public int CDMA2000MeasParas_cdmaFreqANRInd { get; set; }

        public int wcdmaLBMeasCfg { get; set; }

        public int ratPriCnPara_ratPriCnCSFB2 { get; set; }

        public int ratPriIdPara_ratPriIdleCSFB2 { get; set; }

        public string wcdmaMeasCfg { get; set; }

        public int voiceAdmtSwch { get; set; }

        public int multiPLMNLocSt4RdSwch { get; set; }

        public int methodLTEtoGSM { get; set; }

        public int filterCoeffRsrp { get; set; }

        public int offFreqPriorityPara_offFreqPriority1 { get; set; }

        public int csfbMethdofCDMA { get; set; }

        public int pingPongThres4Macro2Small { get; set; }

        public int ratSrvccPara_ratSrvcc3 { get; set; }

        public int trigQuaofTarCellOrder { get; set; }

        public int interFreqNum { get; set; }

        public int ratPriorityPara_ratPriority1 { get; set; }

        public string filCoeUtran { get; set; }

        public int eutranMeasParas_interFreqANRInd { get; set; }

        public int intraRATHObasedLoadSwch { get; set; }

        public int eutranMeasParas_lbInterFreqOfn { get; set; }

        public int eutranMeasParas_lbInterFreqPriority { get; set; }

        public int intraFMeasBW { get; set; }

        public int interFandInterR { get; set; }

        public int interFPeriodMeasSwitch { get; set; }

        public int offFreqPriorityPara_offFreqPriority6 { get; set; }

        public int csfbMethdofUMTS { get; set; }

        public int offFreqPriorityPara_offFreqPriority4 { get; set; }

        public int rptCGIMeasCfg { get; set; }

        public int wcdmaCSFBMeasCfg { get; set; }

        public int ratPriIdPara_ratPriIdleCSFB1 { get; set; }

        public int ratSrvccPara_ratSrvcc1 { get; set; }

        public int ratSrvccPara_ratSrvcc2 { get; set; }

        public int redA2Switch { get; set; }
    }
}
