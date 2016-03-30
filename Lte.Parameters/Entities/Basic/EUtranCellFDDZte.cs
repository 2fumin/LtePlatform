using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Basic
{
    public class EUtranCellFDDZte : IEntity<ObjectId>, IZteMongo
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

        public int flagSwiMode { get; set; }

        public double latitude { get; set; }

        public int cpMerger { get; set; }

        public int ratTarCarriFre_ratTarCarriFreTDD { get; set; }

        public int siWindowLength { get; set; }

        public int minMCSDl { get; set; }

        public int pullCardJudgeSwitch { get; set; }

        public int radioMode { get; set; }

        public int rlfDelayTime { get; set; }

        public int phsNIInd { get; set; }

        public int tm34T4RSwch { get; set; }

        public int ocs { get; set; }

        public int switchUlPRBRandom { get; set; }

        public int oldCellId { get; set; }

        public int switchForNGbrDrx { get; set; }

        public string pciList { get; set; }

        public string rbInterferenceBitMapUl { get; set; }

        public int bandWidthDl { get; set; }

        public int maxUeRbNumUl { get; set; }

        public int matrixType { get; set; }

        public int cqiExpiredTimer { get; set; }

        public string masterECellEqFun { get; set; }

        public int coMPFlagUl { get; set; }

        public int voIPRLFDelayTime { get; set; }

        public int bfmumimoEnableDl { get; set; }

        public int ratTarCarriFre_ratTarCarriFreUTRATDD { get; set; }

        public int pci { get; set; }

        public string cellReservedForOptUse { get; set; }

        public double cellReferenceSignalPower { get; set; }

        public int maxMCSUl { get; set; }

        public int ratTarCarriFre_ratTarCarriFreUTRAFDD { get; set; }

        public int simuLoadSwchDl { get; set; }

        public int ratTarCarriFre_ratTarCarriFreGERAN { get; set; }

        public int pb { get; set; }

        public int sceneCfg { get; set; }

        public double maximumTransmissionPower { get; set; }

        public string addiFreqBand { get; set; }

        public int maxUeRbNumDl { get; set; }

        public double earfcnDl { get; set; }

        public string reservedByEUtranRelation { get; set; }

        public int eai { get; set; }

        public int ratioShared { get; set; }

        public int phyChCPSel { get; set; }

        public string refECellEquipmentFunction { get; set; }

        public int freqBandInd { get; set; }

        public int csfbMethodofGSM { get; set; }

        public string userLabel { get; set; }

        public string alias { get; set; }

        public int allowedAccessClasses { get; set; }

        public int mumimoEnableUl { get; set; }

        public int csfbMethdofCDMA { get; set; }

        public int sfAssignment { get; set; }

        public int cFI { get; set; }

        public int cceAdaptMod { get; set; }

        public int voLTESwch { get; set; }

        public int cutRbNumDl { get; set; }

        public int rd4ForCoverage { get; set; }

        public int minMCSUl { get; set; }

        public int sampleRateCfg { get; set; }

        public int testState { get; set; }

        public int ceuccuSwitch { get; set; }

        public int loadtestSwitch { get; set; }

        public int cutRbNumUl { get; set; }

        public int cellLocalId { get; set; }

        public string ratioOperatorn { get; set; }

        public int commCCENumDl { get; set; }

        public int qosAdpSwchUL { get; set; }

        public int specialSfPatterns { get; set; }

        public int tac { get; set; }

        public int cellSize { get; set; }

        public string refPlmn { get; set; }

        public int addiSpecEmiss { get; set; }

        public int aggregationUl { get; set; }

        public int switchForGbrDrx { get; set; }

        public int ratTarCarriFre_ratTarCarriFreCMA1xRTT { get; set; }

        public double earfcnUl { get; set; }

        public int avoidFreqOffsetNISwch { get; set; }

        public int ueTransMode { get; set; }

        public int mimoModeSwitch { get; set; }

        public int energySavControl { get; set; }

        public int srsRLFSwitch { get; set; }

        public int cellResvInfo { get; set; }

        public int wimaxCoexistSwitch { get; set; }

        public double longitude { get; set; }

        public int ratTarCarriFre_ratTarCarriFreFDD { get; set; }

        public int maxMCSDl { get; set; }

        public int isCompressed { get; set; }

        public string rbInterferenceBitMapDl { get; set; }

        public double offsetAngle { get; set; }

        public int cellCapaLeveInd { get; set; }

        public int cellRadius { get; set; }

        public int timeAlignTimer { get; set; }

        public int ratTarCarriFre_ratTarCarriFreCMAHRPD { get; set; }

        public int antPort1 { get; set; }

        public int EUtranCellFDD { get; set; }

        public double transmissionPower { get; set; }

        public int glCSSSwch { get; set; }

        public int rbSharMode { get; set; }

        public int rlfSwitch { get; set; }

        public int fullConfigSwch { get; set; }

        public int bandIndicator { get; set; }

        public int ueTransModeTDD { get; set; }

        public int bandWidthUl { get; set; }

        public int antDecRankSwch { get; set; }

        public int adminState { get; set; }

        public int upInterfFreqEffThr { get; set; }

        public int nbrBlackListExist { get; set; }

        public int preScheUEAccessSwitchUl { get; set; }

        public int csfbMethdofUMTS { get; set; }

        public int mimoScenarios { get; set; }

        public int flagSwiModeUl { get; set; }

        public string buildPhyLocation { get; set; }

        public int aggregationDl { get; set; }

        public string addiSpecEmissForAddiFreq { get; set; }

        public int cellRSPortNum { get; set; }

        public int qosAdpSwchDL { get; set; }

        public string supercellFlag { get; set; }

        public int magicRadioSwch { get; set; }

        public int qam64DemSpIndUl { get; set; }
    }
}
