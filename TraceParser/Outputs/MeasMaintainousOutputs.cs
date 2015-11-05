using System.Linq;
using TraceParser.Eutra;

namespace TraceParser.Outputs
{
    public static class MeasMaintainousOutputs
    {
        public static string GetMaintainousOutputs(this MeasConfig config)
        {
            string result = config.measObjectToRemoveList.Aggregate(
                "Measure object to remove list:", (current, id) => current + (id + ","));
            result += config.reportConfigToRemoveList.Aggregate(
                "Report config to remove list:", (current, id) => current + (id + ","));
            result += "Quantity config:" + config.quantityConfig.quantityConfigEUTRA.GetOutputs();
            result += ", S-measure:" + config.s_Measure;
            return result;
        }

        public static string GetOutputs(this QuantityConfigEUTRA config)
        {
            string result = "Filter coefficient RSRP:" + config.filterCoefficientRSRP.GetDescription();
            result += ", Filter coefficient RSRQ:" + config.filterCoefficientRSRQ.GetDescription();
            return result;
        }

        public static string GetOutputs(this MobilityControlInfo info)
        {
            string result = "Target PCI:" + info.targetPhysCellId;
            result += ", Carrier frequency:" + info.carrierFreq.GetDescription();
            result += ", Carrier bandwidth:" + info.carrierBandwidth.GetDescription();
            result += ", Additional spectrum emission:" + info.additionalSpectrumEmission;
            result += ", T304:" + info.t304.GetDescription();
            result += ", New UE-ID:" + info.newUE_Identity;
            result += ", Radio resource common:" + info.radioResourceConfigCommon.GetOutputs();
            result += ", RACH config dedicated:" + info.rach_ConfigDedicated.GetOutputs();
            return result;
        }

        public static string GetDescription(this CarrierFreqEUTRA freq)
        {
            string result = "Downlink:" + freq.dl_CarrierFreq;
            result += ", Uplink:" + freq.ul_CarrierFreq;
            return result;
        }

        public static string GetDescription(this CarrierBandwidthEUTRA bandwidth)
        {
            string result = "Downlink:" + bandwidth.dl_Bandwidth.GetDescription();
            result += ", Uplink:" + bandwidth.ul_Bandwidth.GetDescription();
            return result;
        }

        public static string GetDescription(this CarrierBandwidthEUTRA.dl_Bandwidth_Enum bandwidth)
        {
            switch (bandwidth)
            {
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare1:
                    return "spare1";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare2:
                    return "spare2";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare3:
                    return "spare3";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare4:
                    return "spare4";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare5:
                    return "spare5";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare6:
                    return "spare6";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare7:
                    return "spare7";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare8:
                    return "spare8";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare9:
                    return "spare9";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.spare10:
                    return "spare10";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.n6:
                    return "1.4M";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.n15:
                    return "3M";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.n25:
                    return "5M";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.n50:
                    return "10M";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.n75:
                    return "15M";
                case CarrierBandwidthEUTRA.dl_Bandwidth_Enum.n100:
                    return "20M";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this CarrierBandwidthEUTRA.ul_Bandwidth_Enum? bandwidth)
        {
            if (bandwidth == null) return "undefined";
            switch (bandwidth)
            {
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare1:
                    return "spare1";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare2:
                    return "spare2";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare3:
                    return "spare3";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare4:
                    return "spare4";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare5:
                    return "spare5";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare6:
                    return "spare6";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare7:
                    return "spare7";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare8:
                    return "spare8";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare9:
                    return "spare9";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.spare10:
                    return "spare10";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.n6:
                    return "1.4M";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.n15:
                    return "3M";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.n25:
                    return "5M";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.n50:
                    return "10M";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.n75:
                    return "15M";
                case CarrierBandwidthEUTRA.ul_Bandwidth_Enum.n100:
                    return "20M";
                default:
                    return "undefined"; 
            }
        }

        public static string GetDescription(this MobilityControlInfo.t304_Enum t304)
        {
            switch (t304)
            {
                case MobilityControlInfo.t304_Enum.spare1:
                    return "spare1";
                case MobilityControlInfo.t304_Enum.ms50:
                    return "50ms";
                case MobilityControlInfo.t304_Enum.ms100:
                    return "100ms";
                case MobilityControlInfo.t304_Enum.ms150:
                    return "150ms";
                case MobilityControlInfo.t304_Enum.ms200:
                    return "200ms";
                case MobilityControlInfo.t304_Enum.ms500:
                    return "500ms";
                case MobilityControlInfo.t304_Enum.ms1000:
                    return "1000ms";
                case MobilityControlInfo.t304_Enum.ms2000:
                    return "2000ms";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this RadioResourceConfigCommon config)
        {
            string result = "RACH config common:" + config.rach_ConfigCommon.GetOutputs();
            result += ", PRACH config:" + config.prach_Config.GetOutputs();
            result += ", PDSCH config common:" + config.pdsch_ConfigCommon.GetOutputs();
            result += ", PUSCH config common:" + config.pusch_ConfigCommon.GetOutputs();
            result += ", PHICH config:" + config.phich_Config.GetOutputs();
            result += ", PUCCH config common:" + config.pucch_ConfigCommon.GetOutputs();
            result += ", Sounding RS UL config common:" + config.soundingRS_UL_ConfigCommon.GetOutputs();
            result += ", Uplink power control common:" + config.uplinkPowerControlCommon.GetOutputs();
            result += ", Antenna Info common:" + config.antennaInfoCommon.GetOutputs();
            result += ", p-Max:" + config.p_Max;
            result += ", UL cyclic prfix length:" + config.ul_CyclicPrefixLength.GetDescription();
            return result;
        }

        public static string GetOutputs(this RACH_ConfigCommon config)
        {
            string result = "Preamble info:" + config.preambleInfo.GetOutputs();
            result += ", Power ramping parameters:" + config.powerRampingParameters.GetOutputs();
            result += ", RA supervision info:" + config.ra_SupervisionInfo.GetOutputs();
            result += ", Max HRAQ msg3 Transmissions:" + config.maxHARQ_Msg3Tx;
            return result;
        }

        public static string GetOutputs(this RACH_ConfigCommon.preambleInfo_Type type)
        {
            string result = "Number of RA preambles:" + type.numberOfRA_Preambles.GetDescription();
            result += ", Preambles group-A config:" + type.preamblesGroupAConfig.GetOutputs();
            return result;
        }

        public static string GetDescription(this RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum preambles)
        {
            switch (preambles)
            {
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n4:
                    return "4";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n8:
                    return "8";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n12:
                    return "12";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n16:
                    return "16";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n20:
                    return "20";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n24:
                    return "24";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n28:
                    return "28";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n32:
                    return "32";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n36:
                    return "36";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n40:
                    return "40";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n44:
                    return "44";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n48:
                    return "48";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n52:
                    return "52";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n56:
                    return "56";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n60:
                    return "60";
                case RACH_ConfigCommon.preambleInfo_Type.numberOfRA_Preambles_Enum.n64:
                    return "64";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type type)
        {
            string result = "Size of RA preamble group-A:" + type.sizeOfRA_PreamblesGroupA.GetDescription();
            result += ", Message size group-A:" + type.messageSizeGroupA.GetDescription();
            result += ", Message power offset group-B:" + type.messagePowerOffsetGroupB.GetDescription();
            return result;
        }

        public static string GetDescription(
            this RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum size)
        {
            switch (size)
            {
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n4:
                    return "4";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n8:
                    return "8";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n12:
                    return "12";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n16:
                    return "16";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n20:
                    return "20";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n24:
                    return "24";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n28:
                    return "28";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n32:
                    return "32";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n36:
                    return "36";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n40:
                    return "40";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n44:
                    return "44";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n48:
                    return "48";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n52:
                    return "52";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n56:
                    return "56";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.sizeOfRA_PreamblesGroupA_Enum.n60:
                    return "60";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messageSizeGroupA_Enum message)
        {
            switch (message)
            {
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messageSizeGroupA_Enum.b56:
                    return "56bits";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messageSizeGroupA_Enum.b144:
                    return "144bits";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messageSizeGroupA_Enum.b208:
                    return "208bits";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messageSizeGroupA_Enum.b256:
                    return "256bits";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum message)
        {
            switch (message)
            {
                case
                    RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum
                        .minusinfinity:
                    return "-infinity";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum.dB0:
                    return "0dB";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum.dB5:
                    return "5dB";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum.dB8:
                    return "8dB";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum.dB10:
                    return "10dB";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum.dB12:
                    return "12dB";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum.dB15:
                    return "15dB";
                case RACH_ConfigCommon.preambleInfo_Type.preamblesGroupAConfig_Type.messagePowerOffsetGroupB_Enum.dB18:
                    return "18dB";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this PowerRampingParameters parameters)
        {
            string result = "Power ramping step:" + parameters.powerRampingStep.GetDescription();
            result += ", Preamble initial received target power:" + parameters.preambleInitialReceivedTargetPower.GetDescription();
            return result;
        }

        public static string GetDescription(this PowerRampingParameters.powerRampingStep_Enum step)
        {
            switch (step)
            {
                case PowerRampingParameters.powerRampingStep_Enum.dB0:
                    return "0dB";
                case PowerRampingParameters.powerRampingStep_Enum.dB2:
                    return "2dB";
                case PowerRampingParameters.powerRampingStep_Enum.dB4:
                    return "4dB";
                case PowerRampingParameters.powerRampingStep_Enum.dB6:
                    return "6dB";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this PowerRampingParameters.preambleInitialReceivedTargetPower_Enum power)
        {
            switch (power)
            {
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_120:
                    return "-120dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_118:
                    return "-118dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_116:
                    return "-116dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_114:
                    return "-114dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_112:
                    return "-112dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_110:
                    return "-110dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_108:
                    return "-108dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_106:
                    return "-106dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_104:
                    return "-104dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_102:
                    return "-102dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_100:
                    return "-100dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_98:
                    return "-98dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_96:
                    return "-96dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_94:
                    return "-94dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_92:
                    return "-92dBm";
                case PowerRampingParameters.preambleInitialReceivedTargetPower_Enum.dBm_90:
                    return "-90dBm";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this RACH_ConfigCommon.ra_SupervisionInfo_Type type)
        {
            string result = "Preamble Transmission max:" + type.preambleTransMax.GetDescription();
            result += ", RA response window size:" + type.ra_ResponseWindowSize.GetDescription();
            result += ", MAC contention resolution timer:" + type.mac_ContentionResolutionTimer.GetDescription();
            return result;
        }

        public static string GetDescription(
            this RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum size)
        {
            switch (size)
            {
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf2:
                    return "2 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf3:
                    return "3 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf4:
                    return "4 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf5:
                    return "5 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf6:
                    return "6 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf7:
                    return "7 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf8:
                    return "8 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.ra_ResponseWindowSize_Enum.sf10:
                    return "10 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum timer)
        {
            switch (timer)
            {
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf8:
                    return "8 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf16:
                    return "16 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf24:
                    return "24 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf32:
                    return "32 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf40:
                    return "40 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf48:
                    return "48 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf56:
                    return "56 subframes";
                case RACH_ConfigCommon.ra_SupervisionInfo_Type.mac_ContentionResolutionTimer_Enum.sf64:
                    return "64 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this PRACH_Config config)
        {
            string result = "Root sequence index:" + config.rootSequenceIndex;
            result += ", Prach config info:" + config.prach_ConfigInfo.GetOutputs();
            return result;
        }

        public static string GetOutputs(this PRACH_ConfigInfo info)
        {
            string result = "PRACH config index:" + info.prach_ConfigIndex;
            result += ", High-speed flag:" + info.highSpeedFlag;
            result += ", Zero-correlation zone config:" + info.zeroCorrelationZoneConfig;
            result += ", PRACH frequency offset:" + info.prach_FreqOffset;
            return result;
        }

        public static string GetOutputs(this PDSCH_ConfigCommon config)
        {
            string result = "Reference signal power:" + config.referenceSignalPower;
            result += ", Pb:" + config.p_b;
            return result;
        }

        public static string GetOutputs(this PUSCH_ConfigCommon config)
        {
            string result = "PUSCH config basic:" + config.pusch_ConfigBasic.GetOutputs();
            result += ", UL RS PUSCH:" + config.ul_ReferenceSignalsPUSCH.GetOutputs();
            return result;
        }

        public static string GetOutputs(this PUSCH_ConfigCommon.pusch_ConfigBasic_Type type)
        {
            string result = "nSB:" + type.n_SB;
            result += ", Hopping mode:" + type.hoppingMode.GetDescription();
            result += ", PUSCH hopping offset:" + type.pusch_HoppingOffset;
            result += ", Enabled 64QAM:" + type.enable64QAM;
            return result;
        }

        public static string GetDescription(this PUSCH_ConfigCommon.pusch_ConfigBasic_Type.hoppingMode_Enum mode)
        {
            switch (mode)
            {
                case PUSCH_ConfigCommon.pusch_ConfigBasic_Type.hoppingMode_Enum.interSubFrame:
                    return "inter subframes";
                case PUSCH_ConfigCommon.pusch_ConfigBasic_Type.hoppingMode_Enum.intraAndInterSubFrame:
                    return "intra and inter subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this UL_ReferenceSignalsPUSCH type)
        {
            string result = "Group hopping enabled:" + type.groupHoppingEnabled;
            result += ", Group assignment PUSCH:" + type.groupAssignmentPUSCH;
            result += ", Sequence hopping enabled:" + type.sequenceHoppingEnabled;
            result += ", Cyclic shift:" + type.cyclicShift;
            return result;
        }

        public static string GetOutputs(this PHICH_Config config)
        {
            string result = "PHICH duration:" + config.phich_Duration.GetDescription();
            result += ", PHICH resource:" + config.phich_Resource.GetDescription();
            return result;
        }

        public static string GetDescription(this PHICH_Config.phich_Duration_Enum duration)
        {
            switch (duration)
            {
                case PHICH_Config.phich_Duration_Enum.normal:
                    return "normal";
                case PHICH_Config.phich_Duration_Enum.extended:
                    return "extended";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this PHICH_Config.phich_Resource_Enum resource)
        {
            switch (resource)
            {
                case PHICH_Config.phich_Resource_Enum.half:
                    return "half";
                case PHICH_Config.phich_Resource_Enum.oneSixth:
                    return "one sixth";
                case PHICH_Config.phich_Resource_Enum.one:
                    return "one";
                case PHICH_Config.phich_Resource_Enum.two:
                    return "two";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this PUCCH_ConfigCommon config)
        {
            string result = "Delta PUCCH shift:" + config.deltaPUCCH_Shift.GetDescription();
            result += ", nRB CQI:" + config.nRB_CQI;
            result += ", nCS AN:" + config.nCS_AN;
            result += ", n1 PUCCH AN:" + config.n1PUCCH_AN;
            return result;
        }

        public static string GetDescription(this PUCCH_ConfigCommon.deltaPUCCH_Shift_Enum shift)
        {
            switch (shift)
            {
                case PUCCH_ConfigCommon.deltaPUCCH_Shift_Enum.ds1:
                    return "ds1";
                case PUCCH_ConfigCommon.deltaPUCCH_Shift_Enum.ds2:
                    return "ds2";
                case PUCCH_ConfigCommon.deltaPUCCH_Shift_Enum.ds3:
                    return "ds3";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this SoundingRS_UL_ConfigCommon config)
        {
            if (config.release != null) return "release";
            return config.setup != null ? config.setup.GetOutputs() : "undefined";
        }

        public static string GetOutputs(this SoundingRS_UL_ConfigCommon.setup_Type type)
        {
            string result = "Ack/Nack SRS simultaneous transmission:" + type.ackNackSRS_SimultaneousTransmission;
            result += ", SRS bandwidth config:" + type.srs_BandwidthConfig.GetDescription();
            result += ", SRS max upPts:" + type.srs_MaxUpPts.GetDescription();
            result += ", SRS subframe config:" + type.srs_SubframeConfig.GetDescription();
            return result;
        }

        public static string GetDescription(this SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum config)
        {
            switch (config)
            {
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw0:
                    return "bw0";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw1:
                    return "bw1";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw2:
                    return "bw2";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw3:
                    return "bw3";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw4:
                    return "bw4";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw5:
                    return "bw5";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw6:
                    return "bw6";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_BandwidthConfig_Enum.bw7:
                    return "bw7";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this SoundingRS_UL_ConfigCommon.setup_Type.srs_MaxUpPts_Enum? max)
        {
            if (max == null) return "undefined";
            switch (max)
            {
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_MaxUpPts_Enum._true:
                    return "true";
                default:
                    return "false";
            }
        }

        public static string GetDescription(this SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum config)
        {
            switch (config)
            {
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc0:
                    return "sc0";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc1:
                    return "sc1";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc2:
                    return "sc2";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc3:
                    return "sc3";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc4:
                    return "sc4";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc5:
                    return "sc5";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc6:
                    return "sc6";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc7:
                    return "sc7";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc8:
                    return "sc8";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc9:
                    return "sc9";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc10:
                    return "sc10";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc11:
                    return "sc11";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc12:
                    return "sc12";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc13:
                    return "sc13";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc14:
                    return "sc14";
                case SoundingRS_UL_ConfigCommon.setup_Type.srs_SubframeConfig_Enum.sc15:
                    return "sc15";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this UplinkPowerControlCommon control)
        {
            string result = "P0 norminal PUSCH:" + control.p0_NominalPUSCH;
            result += ", Alpha:" + control.alpha.GetDescription();
            result += ", P0 nominal PUCCH:" + control.p0_NominalPUCCH;
            result += ", Delta F list PUCCH:" + control.deltaFList_PUCCH.GetOutputs();
            result += ", Delta preamble msg3:" + control.deltaPreambleMsg3;
            return result;
        }

        public static string GetDescription(this UplinkPowerControlCommon.alpha_Enum alpha)
        {
            switch (alpha)
            {
                case UplinkPowerControlCommon.alpha_Enum.al0:
                    return "0";
                case UplinkPowerControlCommon.alpha_Enum.al1:
                    return "1";
                case UplinkPowerControlCommon.alpha_Enum.al04:
                    return "4";
                case UplinkPowerControlCommon.alpha_Enum.al05:
                    return "5";
                case UplinkPowerControlCommon.alpha_Enum.al06:
                    return "6";
                case UplinkPowerControlCommon.alpha_Enum.al07:
                    return "7";
                case UplinkPowerControlCommon.alpha_Enum.al08:
                    return "8";
                case UplinkPowerControlCommon.alpha_Enum.al09:
                    return "9";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this DeltaFList_PUCCH list)
        {
            string result = "Format1:" + list.deltaF_PUCCH_Format1.GetDescription();
            result += ", Format1b:" + list.deltaF_PUCCH_Format1b.GetDescription();
            result += ", Format2:" + list.deltaF_PUCCH_Format2.GetDescription();
            result += ", Format2a:" + list.deltaF_PUCCH_Format2a.GetDescription();
            result += ", Format2b:" + list.deltaF_PUCCH_Format2b.GetDescription();
            return result;
        }

        public static string GetDescription(this DeltaFList_PUCCH.deltaF_PUCCH_Format1_Enum format1)
        {
            switch (format1)
            {
                case DeltaFList_PUCCH.deltaF_PUCCH_Format1_Enum.deltaF0:
                    return "0";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format1_Enum.deltaF2:
                    return "2";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format1_Enum.deltaF_2:
                    return "-2";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this DeltaFList_PUCCH.deltaF_PUCCH_Format1b_Enum format1B)
        {
            switch (format1B)
            {
                case DeltaFList_PUCCH.deltaF_PUCCH_Format1b_Enum.deltaF1:
                    return "1";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format1b_Enum.deltaF3:
                    return "3";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format1b_Enum.deltaF5:
                    return "5";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this DeltaFList_PUCCH.deltaF_PUCCH_Format2_Enum format2)
        {
            switch (format2)
            {
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2_Enum.deltaF_2:
                    return "-2";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2_Enum.deltaF0:
                    return "0";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2_Enum.deltaF1:
                    return "1";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2_Enum.deltaF2:
                    return "2";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this DeltaFList_PUCCH.deltaF_PUCCH_Format2a_Enum format2A)
        {
            switch (format2A)
            {
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2a_Enum.deltaF0:
                    return "0";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2a_Enum.deltaF2:
                    return "2";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2a_Enum.deltaF_2:
                    return "-2";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this DeltaFList_PUCCH.deltaF_PUCCH_Format2b_Enum format2B)
        {
            switch (format2B)
            {
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2b_Enum.deltaF0:
                    return "0";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2b_Enum.deltaF2:
                    return "2";
                case DeltaFList_PUCCH.deltaF_PUCCH_Format2b_Enum.deltaF_2:
                    return "-2";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this AntennaInfoCommon info)
        {
            return "Antenna ports count:" + info.antennaPortsCount.GetDescription();
        }

        public static string GetDescription(this AntennaInfoCommon.antennaPortsCount_Enum count)
        {
            switch (count)
            {
                case AntennaInfoCommon.antennaPortsCount_Enum.spare1:
                    return "spare1";
                case AntennaInfoCommon.antennaPortsCount_Enum.an1:
                    return "1";
                case AntennaInfoCommon.antennaPortsCount_Enum.an2:
                    return "2";
                case AntennaInfoCommon.antennaPortsCount_Enum.an4:
                    return "4";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this RACH_ConfigDedicated config)
        {
            string result = "RA preamble index:" + config.ra_PreambleIndex;
            result += ", RA PRACH mask index:" + config.ra_PRACH_MaskIndex;
            return result;
        }

        public static string GetOutputs(this SecurityConfigHO config)
        {
            string result = "";
            if (config.handoverType != null)
            {
                result += "Handover type:";
                if (config.handoverType.intraLTE != null)
                    result += "Intra LTE:" + config.handoverType.intraLTE.GetOutputs();
            }
            return result;
        }

        public static string GetOutputs(this SecurityConfigHO.handoverType_Type.intraLTE_Type type)
        {
            string result = "Security algorithm config:" + type.securityAlgorithmConfig.GetOutputs();
            result += ", Key change indicator:" + type.keyChangeIndicator;
            result += ", Next hop chaining count:" + type.nextHopChainingCount;
            return result;
        }

        public static string GetOutputs(this SecurityAlgorithmConfig config)
        {
            string result = "Ciphering algorithm:" + config.cipheringAlgorithm.GetDescription();
            result += ", Integrity protection algorithm:" + config.integrityProtAlgorithm.GetDescription();
            return result;
        }

        public static string GetDescription(this SecurityAlgorithmConfig.cipheringAlgorithm_Enum algorithm)
        {
            switch (algorithm)
            {
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.eea0:
                    return "EEA0";
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.eea1:
                    return "EEA1";
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.eea2:
                    return "EEA2";
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.eea3_v1130:
                    return "EEA3";
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.spare1:
                    return "spare1";
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.spare2:
                    return "spare2";
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.spare3:
                    return "spare3";
                case SecurityAlgorithmConfig.cipheringAlgorithm_Enum.spare4:
                    return "spare4";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this SecurityAlgorithmConfig.integrityProtAlgorithm_Enum algorithm)
        {
            switch (algorithm)
            {
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.eia0_v920:
                    return "EIA0";
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.eia1:
                    return "EIA1";
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.eia2:
                    return "EIA2";
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.eia3_v1130:
                    return "EIA3";
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.spare1:
                    return "spare1";
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.spare2:
                    return "spare2";
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.spare3:
                    return "spare3";
                case SecurityAlgorithmConfig.integrityProtAlgorithm_Enum.spare4:
                    return "spare4";
                default:
                    return "undefined";
            }
        }
    }
}
