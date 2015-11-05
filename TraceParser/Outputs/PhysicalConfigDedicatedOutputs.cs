using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceParser.Eutra;

namespace TraceParser.Outputs
{
    public static class PhysicalConfigDedicatedOutputs
    {
        public static string GetOutputs(this PhysicalConfigDedicated config)
        {
            string result = "";
            if (config.uplinkPowerControlDedicated != null)
            {
                result += "Uplink power control dedicated:" + config.uplinkPowerControlDedicated.GetOutputs();
            }
            if (config.cqi_ReportConfig != null)
            {
                result += ", CQI report config:" + config.cqi_ReportConfig.GetOutputs();
            }
            if (config.antennaInfo != null)
            {
                result += ", Antenna info:" + config.antennaInfo.explicitValue.GetOutputs();
            }
            if (config.soundingRS_UL_ConfigDedicated != null)
            {
                result += ", Sounding RS UL config dedicated:" + config.soundingRS_UL_ConfigDedicated.GetOutputs();
            }
            if (config.schedulingRequestConfig != null)
            {
                result += ", Scheduling request config:" + config.schedulingRequestConfig.setup.GetOutputs();
            }
            if (config.pdsch_ConfigDedicated != null)
            {
                result += ", PDSCH config dedicated:pA:" + config.pdsch_ConfigDedicated.p_a.GetDescrition();
            }
            if (config.pucch_ConfigDedicated != null)
            {
                result += ", PUCCH config dedicated:" + config.pucch_ConfigDedicated.GetOutputs();
            }
            if (config.pusch_ConfigDedicated != null)
            {
                result += ", PUSCH config dedicated:" + config.pusch_ConfigDedicated.GetOutputs();
            }
            if (config.tpc_PDCCH_ConfigPUCCH != null)
            {
                result += ", TPC PDCCH config PUCCH:" + config.tpc_PDCCH_ConfigPUCCH.GetOutputs();
            }
            if (config.tpc_PDCCH_ConfigPUSCH != null)
            {
                result += ", TPC PDCCH config PUSCH:" + config.tpc_PDCCH_ConfigPUSCH.GetOutputs();
            }
            return result;
        }

        public static string GetOutputs(this UplinkPowerControlDedicated control)
        {
            string result = "P0 UE PUSCH:" + control.p0_UE_PUSCH;
            result += ", Delta MCS enabled:" + control.deltaMCS_Enabled.GetDescription();
            result += ", Accumulation enabled:" + control.accumulationEnabled;
            result += ", P0 UE PUCCH:" + control.p0_UE_PUCCH;
            result += ", pSRS offset:" + control.pSRS_Offset;
            result += ", Filter coefficient:" + control.filterCoefficient.GetDescription();
            return result;
        }

        public static string GetDescription(this UplinkPowerControlDedicated.deltaMCS_Enabled_Enum config)
        {
            switch (config)
            {
                case UplinkPowerControlDedicated.deltaMCS_Enabled_Enum.en0:
                    return "en0";
                case UplinkPowerControlDedicated.deltaMCS_Enabled_Enum.en1:
                    return "en1";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this CQI_ReportConfig config)
        {
            string result = "CQI report mode aperiodic:" + config.cqi_ReportModeAperiodic.GetDescription();
            result += ", Nominal PDSCH RS EPRE offset:" + config.nomPDSCH_RS_EPRE_Offset;
            result += ", CQI report periodic:" + config.cqi_ReportPeriodic.setup.GetOutputs();
            return result;
        }

        public static string GetOutputs(this CQI_ReportPeriodic.setup_Type type)
        {
            string result = "CQI PUCCH resource index:" + type.cqi_PUCCH_ResourceIndex;
            result += ", CQI PMI  config index:" + type.cqi_pmi_ConfigIndex;
            result += ", CQI format indicator periodic:" + type.cqi_FormatIndicatorPeriodic.GetDescription();
            result += ", RI config index:" + type.ri_ConfigIndex;
            result += ", Simultaneous ack nack and CQI:" + type.simultaneousAckNackAndCQI;
            return result;
        }

        public static string GetDescription(
            this CQI_ReportPeriodic.setup_Type.cqi_FormatIndicatorPeriodic_Type type)
        {
            if (type.widebandCQI != null)
            {
                return "wideband CQI";
            }
            if (type.subbandCQI != null)
            {
                return type.subbandCQI.GetDescription();
            }
            return "wideband CQI";
        }

        public static string GetDescription(
            this CQI_ReportPeriodic.setup_Type.cqi_FormatIndicatorPeriodic_Type.subbandCQI_Type type)
        {
            return "Subband CQI coefficient:" + type.k;
        }

        public static string GetOutputs(this AntennaInfoDedicated info)
        {
            string result = "Transmission mode:" + info.transmissionMode.GetDescription();
            if (info.codebookSubsetRestriction!=null)
                result += ", Code book subset restriction:" + info.codebookSubsetRestriction.GetOutputs();
            if (info.ue_TransmitAntennaSelection!=null)
                result += ", UE transmit antenna selection:" + info.ue_TransmitAntennaSelection.GetOutputs();
            return result;
        }

        public static string GetDescription(this AntennaInfoDedicated.transmissionMode_Enum mode)
        {
            switch (mode)
            {
                case AntennaInfoDedicated.transmissionMode_Enum.tm1:
                    return "TM1";
                case AntennaInfoDedicated.transmissionMode_Enum.tm2:
                    return "TM2";
                case AntennaInfoDedicated.transmissionMode_Enum.tm3:
                    return "TM3";
                case AntennaInfoDedicated.transmissionMode_Enum.tm4:
                    return "TM4";
                case AntennaInfoDedicated.transmissionMode_Enum.tm5:
                    return "TM5";
                case AntennaInfoDedicated.transmissionMode_Enum.tm6:
                    return "TM6";
                case AntennaInfoDedicated.transmissionMode_Enum.tm7:
                    return "TM7";
                case AntennaInfoDedicated.transmissionMode_Enum.tm8_v920:
                    return "TM8";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this AntennaInfoDedicated.codebookSubsetRestriction_Type type)
        {
            if (!string.IsNullOrEmpty(type.n2TxAntenna_tm3))
                return "n2TxAntenna_tm3";
            if (!string.IsNullOrEmpty(type.n2TxAntenna_tm4))
                return "n2TxAntenna_tm4";
            if (!string.IsNullOrEmpty(type.n2TxAntenna_tm5))
                return "n2TxAntenna_tm5";
            if (!string.IsNullOrEmpty(type.n2TxAntenna_tm6))
                return "n2TxAntenna_tm6";
            if (!string.IsNullOrEmpty(type.n4TxAntenna_tm3))
                return "n4TxAntenna_tm3";
            if (!string.IsNullOrEmpty(type.n4TxAntenna_tm4))
                return "n4TxAntenna_tm4";
            if (!string.IsNullOrEmpty(type.n4TxAntenna_tm5))
                return "n4TxAntenna_tm5";
            if (!string.IsNullOrEmpty(type.n4TxAntenna_tm6))
                return "n4TxAntenna_tm6";
            return "undefined";
        }

        public static string GetOutputs(this AntennaInfoDedicated.ue_TransmitAntennaSelection_Type type)
        {
            if (type.release != null) return "release";
            switch (type.setup)
            {
                case AntennaInfoDedicated.ue_TransmitAntennaSelection_Type.setup_Enum.closedLoop:
                    return "closed-loop";
                case AntennaInfoDedicated.ue_TransmitAntennaSelection_Type.setup_Enum.openLoop:
                    return "open-loop";
                default:
                    return "release";
            }
        }

        public static string GetOutputs(this SoundingRS_UL_ConfigDedicated config)
        {
            if (config.release != null) return "release";
            return config.setup.GetOutputs();
        }

        public static string GetOutputs(this SoundingRS_UL_ConfigDedicated.setup_Type type)
        {
            string result = "Cyclic shift:" + type.cyclicShift.GetDescription();
            result += ", Duration:" + type.duration;
            result += ", Frequency domain position:" + type.freqDomainPosition;
            result += "SRS bandwidth:" + type.srs_Bandwidth.GetDescription();
            result += "SRS config index:" + type.srs_ConfigIndex;
            result += "SRS hopping bandwidth:" + type.srs_HoppingBandwidth.GetDescription();
            result += "Transmission comb:" + type.transmissionComb;
            return result;
        }

        public static string GetDescription(this SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum shift)
        {
            switch (shift)
            {
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs0:
                    return "cs0";
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs1:
                    return "cs1";
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs2:
                    return "cs2";
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs3:
                    return "cs3";
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs4:
                    return "cs4";
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs5:
                    return "cs5";
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs6:
                    return "cs6";
                case SoundingRS_UL_ConfigDedicated.setup_Type.cyclicShift_Enum.cs7:
                    return "cs7";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this SoundingRS_UL_ConfigDedicated.setup_Type.srs_Bandwidth_Enum bandwidth)
        {
            switch (bandwidth)
            {
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_Bandwidth_Enum.bw0:
                    return "bw0";
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_Bandwidth_Enum.bw1:
                    return "bw1";
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_Bandwidth_Enum.bw2:
                    return "bw2";
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_Bandwidth_Enum.bw3:
                    return "bw3";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this SoundingRS_UL_ConfigDedicated.setup_Type.srs_HoppingBandwidth_Enum bandwidth)
        {
            switch (bandwidth)
            {
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_HoppingBandwidth_Enum.hbw0:
                    return "hbw0";
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_HoppingBandwidth_Enum.hbw1:
                    return "hbw1";
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_HoppingBandwidth_Enum.hbw2:
                    return "hbw2";
                case SoundingRS_UL_ConfigDedicated.setup_Type.srs_HoppingBandwidth_Enum.hbw3:
                    return "hbw3";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this SchedulingRequestConfig.setup_Type config)
        {
            string result = "SR PUCCH resource index:" + config.sr_PUCCH_ResourceIndex;
            result += ", SR config index:" + config.sr_ConfigIndex;
            result += ", DSR transmisstion max:" + config.dsr_TransMax.GetDescription();
            return result;
        }

        public static string GetDescription(this SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum trans)
        {
            switch (trans)
            {
                    case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.spare1:
                    return "spare1";
                    case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.spare2:
                    return "spare2";
                    case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.spare3:
                    return "spare3";
                    case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.n4:
                    return "n4";
                    case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.n8:
                    return "n8";
                    case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.n16:
                    return "n16";
                    case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.n32:
                    return "n32";
                case SchedulingRequestConfig.setup_Type.dsr_TransMax_Enum.n64:
                    return "n64";
                default:
                    return "undefined";
            }
        }

        public static string GetDescrition(this PDSCH_ConfigDedicated.p_a_Enum pa)
        {
            switch (pa)
            {
                case PDSCH_ConfigDedicated.p_a_Enum.dB_6:
                    return "-6dB";
                case PDSCH_ConfigDedicated.p_a_Enum.dB_4dot77:
                    return "-4.77dB";
                case PDSCH_ConfigDedicated.p_a_Enum.dB_3:
                    return "-3dB";
                case PDSCH_ConfigDedicated.p_a_Enum.dB_1dot77:
                    return "-1.77dB";
                case PDSCH_ConfigDedicated.p_a_Enum.dB0:
                    return "0dB";
                case PDSCH_ConfigDedicated.p_a_Enum.dB1:
                    return "1dB";
                case PDSCH_ConfigDedicated.p_a_Enum.dB2:
                    return "2dB";
                case PDSCH_ConfigDedicated.p_a_Enum.dB3:
                    return "3dB";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this PUCCH_ConfigDedicated config)
        {
            string result = "Ack/Nack repetition:" + config.ackNackRepetition.GetOutputs();
            result += ", TDD ack/nack feedbackmode:" + config.tdd_AckNackFeedbackMode.GetDescription();
            return result;
        }

        public static string GetOutputs(this PUCCH_ConfigDedicated.ackNackRepetition_Type type)
        {
            if (type.release != null)
                return "release";
            if (type.setup != null)
            {
                return type.setup.GetOutputs();
            }
            return "undefined";
        }

        public static string GetOutputs(this PUCCH_ConfigDedicated.ackNackRepetition_Type.setup_Type type)
        {
            string result = "N1 PUCCH AN repetition:" + type.n1PUCCH_AN_Rep;
            result += "Repetition factor:" + type.repetitionFactor.GetDescription();
            return result;
        }

        public static string GetDescription(
            this PUCCH_ConfigDedicated.ackNackRepetition_Type.setup_Type.repetitionFactor_Enum factor)
        {
            switch (factor)
            {
                case PUCCH_ConfigDedicated.ackNackRepetition_Type.setup_Type.repetitionFactor_Enum.spare1:
                    return "spare1";
                case PUCCH_ConfigDedicated.ackNackRepetition_Type.setup_Type.repetitionFactor_Enum.n2:
                    return "2";
                case PUCCH_ConfigDedicated.ackNackRepetition_Type.setup_Type.repetitionFactor_Enum.n4:
                    return "4";
                case PUCCH_ConfigDedicated.ackNackRepetition_Type.setup_Type.repetitionFactor_Enum.n6:
                    return "6";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this PUCCH_ConfigDedicated.tdd_AckNackFeedbackMode_Enum? mode)
        {
            if (mode == null) return "undefined";
            switch (mode)
            {
                case PUCCH_ConfigDedicated.tdd_AckNackFeedbackMode_Enum.bundling:
                    return "bundling";
                case PUCCH_ConfigDedicated.tdd_AckNackFeedbackMode_Enum.multiplexing:
                    return "multiplexing";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this PUSCH_ConfigDedicated config)
        {
            string result = "Beta offset ack index:" + config.betaOffset_ACK_Index;
            result += ", Beta offset CQI index:" + config.betaOffset_CQI_Index;
            result += ", Beta offset RI index:" + config.betaOffset_RI_Index;
            return result;
        }

        public static string GetOutputs(this TPC_PDCCH_Config config)
        {
            if (config.release != null)
                return "release";
            if (config.setup != null)
            {
                return config.setup.GetOutputs();
            }
            return "undefined";
        }

        public static string GetOutputs(this TPC_PDCCH_Config.setup_Type type)
        {
            string result = "TPC index:" + type.tpc_Index.GetOutputs();
            result += ", TPC RNTI:" + type.tpc_RNTI;
            return result;
        }

        public static string GetOutputs(this TPC_Index index)
        {
            string result = "Index of format3:" + index.indexOfFormat3;
            result += ", Index of format3A:" + index.indexOfFormat3A;
            return result;
        }
    }
}
