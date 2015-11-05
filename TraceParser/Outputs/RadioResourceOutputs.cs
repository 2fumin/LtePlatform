using System.Linq;
using TraceParser.Eutra;

namespace TraceParser.Outputs
{
    public static class RadioResourceOutputs
    {
        public static string GetOutputs(this SRB_ToAddMod config)
        {
            string result = "SRB ID:" + config.srb_Identity;
            if (config.rlc_Config != null && config.rlc_Config.explicitValue != null)
            {
                result += ", RLC config:" + config.rlc_Config.explicitValue.GetOutputs();
            }
            if (config.logicalChannelConfig != null && config.logicalChannelConfig.explicitValue != null)
            {
                result += ", Logical channel config:" + config.logicalChannelConfig.explicitValue.GetOutputs();
            }
            return result;
        }

        public static string GetOutputs(this RLC_Config config)
        {
            string result = "";
            if (config.am != null)
            {
                result += "AM:" + config.am.GetOutputs();
            }
            return result;
        }

        public static string GetOutputs(this RLC_Config.am_Type amType)
        {
            string result = "";
            if (amType.ul_AM_RLC != null)
            {
                result += "Uplink:";
                var ul = amType.ul_AM_RLC;
                result += "Poll retransmit timer:" + ul.t_PollRetransmit.GetDescription();
                result += ", Poll pdu:" + ul.pollPDU.GetDescription();
                result += ", Poll byte:" + ul.pollByte.GetDescription();
                result += ", Max retransmit threshold:" + ul.maxRetxThreshold.GetDescription();
            }
            if (amType.dl_AM_RLC != null)
            {
                result += ", Downlink:";
                var dl = amType.dl_AM_RLC;
                result += "Reordering timer:" + dl.t_Reordering.GetDescription();
                result += ", Status prohibit timer:" + dl.t_StatusProhibit.GetDescription();
            }
            return result;
        }

        public static string GetDescription(this UL_AM_RLC.maxRetxThreshold_Enum threshold)
        {
            switch (threshold)
            {
                case UL_AM_RLC.maxRetxThreshold_Enum.t1:
                    return "1";
                case UL_AM_RLC.maxRetxThreshold_Enum.t2:
                    return "2";
                case UL_AM_RLC.maxRetxThreshold_Enum.t3:
                    return "3";
                case UL_AM_RLC.maxRetxThreshold_Enum.t4:
                    return "4";
                case UL_AM_RLC.maxRetxThreshold_Enum.t6:
                    return "6";
                case UL_AM_RLC.maxRetxThreshold_Enum.t8:
                    return "8";
                case UL_AM_RLC.maxRetxThreshold_Enum.t16:
                    return "16";
                case UL_AM_RLC.maxRetxThreshold_Enum.t32:
                    return "32";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this LogicalChannelConfig config)
        {
            string result = "";
            if (config.ul_SpecificParameters != null)
            {
                var parameters = config.ul_SpecificParameters;
                result += "Priority:" + parameters.priority;
                result += ", Prioritised bit rate:" + parameters.prioritisedBitRate.GetDescription();
                result += ", Bucket size duration:" + parameters.bucketSizeDuration.GetDescription();
                result += ", Logical channel group:" + parameters.logicalChannelGroup;
            }
            return result;
        }

        public static string GetDescription(
            this LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum bitRate)
        {
            switch (bitRate)
            {
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.spare1:
                    return "spare1";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.spare2:
                    return "spare2";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.spare3:
                    return "spare3";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.spare4:
                    return "spare4";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.spare5:
                    return "spare5";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps0:
                    return "0kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps8:
                    return "8kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps16:
                    return "16kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps32:
                    return "32kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps64:
                    return "64kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps128:
                    return "128kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps256:
                    return "256kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps512_v1020:
                    return "512kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps1024_v1020:
                    return "1024kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.kBps2048_v1020:
                    return "2048kB";
                case LogicalChannelConfig.ul_SpecificParameters_Type.prioritisedBitRate_Enum.infinity:
                    return "infinity";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum bucketSize)
        {
            switch (bucketSize)
            {
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.spare1:
                    return "spare1";
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.spare2:
                    return "spare2";
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.ms50:
                    return "50ms";
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.ms100:
                    return "100ms";
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.ms150:
                    return "150ms";
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.ms300:
                    return "300ms";
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.ms500:
                    return "500ms";
                case LogicalChannelConfig.ul_SpecificParameters_Type.bucketSizeDuration_Enum.ms1000:
                    return "1000ms";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this DRB_ToAddMod drb)
        {
            string result = "EpsId:" + drb.eps_BearerIdentity + ", DrbId:" + drb.drb_Identity;
            var pdcp = drb.pdcp_Config;
            if (pdcp != null)
            {
                result += ", PDCP config:" + pdcp.GetOutputs();
            }
            var rlc = drb.rlc_Config;
            if (rlc != null)
            {
                result += ", RLC config:" + rlc.GetOutputs();
            }
            result += ", Logical channel Id:" + drb.logicalChannelIdentity;
            var logical = drb.logicalChannelConfig;
            if (logical != null)
            {
                result += ", Logical channel config:" + logical.GetOutputs();
            }
            return result;
        }

        public static string GetOutputs(this PDCP_Config config)
        {
            string result = " Discard Timer:" + config.discardTimer.GetDescription();
            result += ", RLC AM Status Report Required:" + config.rlc_AM.statusReportRequired;
            result += ", Header Compression:" + config.headerCompression.GetDescription();
            return result;
        }

        public static string GetDescription(this PDCP_Config.discardTimer_Enum? timer)
        {
            if (timer == null) return "undefined";
            switch (timer)
            {
                case PDCP_Config.discardTimer_Enum.infinity:
                    return "infinity";
                case PDCP_Config.discardTimer_Enum.ms50:
                    return "50ms";
                case PDCP_Config.discardTimer_Enum.ms100:
                    return "100ms";
                case PDCP_Config.discardTimer_Enum.ms150:
                    return "150ms";
                case PDCP_Config.discardTimer_Enum.ms300:
                    return "300ms";
                case PDCP_Config.discardTimer_Enum.ms500:
                    return "500ms";
                case PDCP_Config.discardTimer_Enum.ms750:
                    return "750ms";
                case PDCP_Config.discardTimer_Enum.ms1500:
                    return "1500ms";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this PDCP_Config.headerCompression_Type type)
        {
            if (type.rohc != null)
            {
                return type.rohc.GetDescription();
            }
            return "not used";
        }

        public static string GetDescription(this PDCP_Config.headerCompression_Type.rohc_Type type)
        {
            string result = "Max CID:" + type.maxCID;
            if (type.profiles != null)
            {
                result += ", Profiles:" + type.profiles.GetDescription();
            }
            return result;
        }

        public static string GetDescription(
            this PDCP_Config.headerCompression_Type.rohc_Type.profiles_Type type)
        {
            return "0001:" + type.profile0x0001 + ",0002:" + type.profile0x0002
                   + ",0003:" + type.profile0x0003 + ",0004:" + type.profile0x0004
                   + ",0006:" + type.profile0x0006 + ",0101:" + type.profile0x0101
                   + ",0102:" + type.profile0x0102 + ",0103:" + type.profile0x0103
                   + ",0104:" + type.profile0x0104;
        }

        public static string GetOutputs(this MAC_MainConfig config)
        {
            string result = "";
            if (config.ul_SCH_Config != null)
            {
                result += "UL SCH config:" + config.ul_SCH_Config.GetOutputs();
            }
            if (config.drx_Config != null)
            {
                result += ", DRX config:" + config.drx_Config.GetOutputs();
            }
            result += ", Time alignment timer dedicate:" + config.timeAlignmentTimerDedicated;
            if (config.phr_Config != null)
            {
                result += ", PHR config:" + config.phr_Config.setup.GetOutputs();
            }
            return result;
        }

        public static string GetOutputs(this MAC_MainConfig.ul_SCH_Config_Type type)
        {
            string result = "Max HARQ Transmission:" + type.maxHARQ_Tx.GetDescription();
            result += ", Period BSR timer:" + type.periodicBSR_Timer.GetDescription();
            result += ", Retransmission BSR timer:" + type.retxBSR_Timer.GetDescription();
            result += ", TTI bundling:" + type.ttiBundling;
            return result;
        }

        public static string GetDescription(this MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum? times)
        {
            if (times == null) return "undefined";
            switch (times)
            {
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.spare1:
                    return "spare1";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.spare2:
                    return "spare2";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n1:
                    return "1";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n2:
                    return "2";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n3:
                    return "3";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n4:
                    return "4";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n5:
                    return "5";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n6:
                    return "6";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n7:
                    return "7";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n8:
                    return "8";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n10:
                    return "10";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n12:
                    return "12";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n16:
                    return "16";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n20:
                    return "20";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n24:
                    return "24";
                case MAC_MainConfig.ul_SCH_Config_Type.maxHARQ_Tx_Enum.n28:
                    return "28";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum? timer)
        {
            if (timer == null)
                return "undefined";
            switch (timer)
            {
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.infinity:
                    return "infinity";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.spare1:
                    return "spare1";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf5:
                    return "5 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf10:
                    return "10 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf16:
                    return "16 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf20:
                    return "20 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf32:
                    return "32 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf40:
                    return "40 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf64:
                    return "64 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf80:
                    return "80 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf128:
                    return "128 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf160:
                    return "160 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf320:
                    return "320 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf640:
                    return "640 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf1280:
                    return "1280 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.periodicBSR_Timer_Enum.sf2560:
                    return "2560 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum timer)
        {
            switch (timer)
            {
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.spare1:
                    return "spare1";
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.spare2:
                    return "spare2";
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.sf320:
                    return "320 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.sf640:
                    return "640 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.sf1280:
                    return "1280 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.sf2560:
                    return "2560 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.sf5120:
                    return "5120 subframes";
                case MAC_MainConfig.ul_SCH_Config_Type.retxBSR_Timer_Enum.sf10240:
                    return "10240 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this DRX_Config config)
        {
            string result = "";
            if (config.setup != null)
            {
                var setup = config.setup;
                result += "On duration timer:" + setup.onDurationTimer.GetDescription();
                result += ", DRX inactive timer:" + setup.drx_InactivityTimer.GetDescription();
                result += ", DRX retransmission timer:" + setup.drx_RetransmissionTimer.GetDescription();
                result += ", Long DRX-Cycle start offset:" + setup.longDRX_CycleStartOffset.GetOutputs();
                result += ", Short DRX:" + setup.shortDRX.GetOutputs();
            }
            return result;
        }

        public static string GetDescription(this DRX_Config.setup_Type.onDurationTimer_Enum timer)
        {
            switch (timer)
            {
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf1:
                    return "1psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf2:
                    return "2psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf3:
                    return "3psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf4:
                    return "4psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf5:
                    return "5psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf6:
                    return "6psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf8:
                    return "8psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf10:
                    return "10psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf20:
                    return "20psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf30:
                    return "30psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf40:
                    return "40psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf50:
                    return "50psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf60:
                    return "60psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf80:
                    return "80psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf100:
                    return "100psf";
                    case DRX_Config.setup_Type.onDurationTimer_Enum.psf200:
                    return "200psf";
                    default:
                    return "undefined";
            }
        }

        public static string GetDescription(this DRX_Config.setup_Type.drx_InactivityTimer_Enum timer)
        {
            switch (timer)
            {
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare1:
                    return "spare1";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare2:
                    return "spare2";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare3:
                    return "spare3";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare4:
                    return "spare4";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare5:
                    return "spare5";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare6:
                    return "spare6";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare7:
                    return "spare7";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare8:
                    return "spare8";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.spare9:
                    return "spare9";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf0_v1020:
                    return "0psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf1:
                    return "1psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf2:
                    return "2psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf3:
                    return "3psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf4:
                    return "4psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf5:
                    return "5psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf6:
                    return "6psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf8:
                    return "8psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf10:
                    return "10psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf20:
                    return "20psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf40:
                    return "40psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf60:
                    return "60psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf80:
                    return "80psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf100:
                    return "100psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf200:
                    return "200psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf300:
                    return "300psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf500:
                    return "500psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf750:
                    return "750psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf1280:
                    return "1250psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf1920:
                    return "1920psf";
                case DRX_Config.setup_Type.drx_InactivityTimer_Enum.psf2560:
                    return "2560psf";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this DRX_Config.setup_Type.drx_RetransmissionTimer_Enum timer)
        {
            switch (timer)
            {
                case DRX_Config.setup_Type.drx_RetransmissionTimer_Enum.psf1:
                    return "1psf";
                case DRX_Config.setup_Type.drx_RetransmissionTimer_Enum.psf2:
                    return "2psf";
                case DRX_Config.setup_Type.drx_RetransmissionTimer_Enum.psf6:
                    return "6psf";
                case DRX_Config.setup_Type.drx_RetransmissionTimer_Enum.psf8:
                    return "8psf";
                case DRX_Config.setup_Type.drx_RetransmissionTimer_Enum.psf16:
                    return "16psf";
                case DRX_Config.setup_Type.drx_RetransmissionTimer_Enum.psf24:
                    return "24psf";
                case DRX_Config.setup_Type.drx_RetransmissionTimer_Enum.psf33:
                    return "33psf";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this DRX_Config.setup_Type.longDRX_CycleStartOffset_Type type)
        {
            if (type.sf10 > 0) return "sf10:" + type.sf10;
            if (type.sf20 > 0) return "sf20:" + type.sf20;
            if (type.sf32 > 0) return "sf32:" + type.sf32;
            if (type.sf40 > 0) return "sf40:" + type.sf40;
            if (type.sf64 > 0) return "sf64:" + type.sf64;
            if (type.sf80 > 0) return "sf80:" + type.sf80;
            if (type.sf128 > 0) return "sf128:" + type.sf128;
            if (type.sf160 > 0) return "sf160:" + type.sf160;
            if (type.sf256 > 0) return "sf256:" + type.sf256;
            if (type.sf320 > 0) return "sf320:" + type.sf320;
            if (type.sf512 > 0) return "sf512:" + type.sf512;
            if (type.sf640 > 0) return "sf640:" + type.sf640;
            if (type.sf1024 > 0) return "sf1024:" + type.sf1024;
            if (type.sf1280 > 0) return "sf1280:" + type.sf1280;
            if (type.sf2048 > 0) return "sf2048:" + type.sf2048;
            if (type.sf2560 > 0) return "sf2560:" + type.sf2560;
            return "undefined";
        }

        public static string GetOutputs(this DRX_Config.setup_Type.shortDRX_Type type)
        {
            string result = "Short DRX cycle:" + type.shortDRX_Cycle.GetDescription();
            result += ", DRX short cycle timer:" + type.drxShortCycleTimer;
            return result;
        }

        public static string GetDescription(this DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum cycle)
        {
            switch (cycle)
            {
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf2:
                    return "2 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf5:
                    return "5 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf8:
                    return "8 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf10:
                    return "10 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf16:
                    return "16 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf20:
                    return "20 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf32:
                    return "32 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf40:
                    return "40 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf64:
                    return "64 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf80:
                    return "80 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf128:
                    return "120 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf160:
                    return "160 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf256:
                    return "256 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf320:
                    return "320 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf512:
                    return "512 subframes";
                    case DRX_Config.setup_Type.shortDRX_Type.shortDRX_Cycle_Enum.sf640:
                    return "640 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this MAC_MainConfig.phr_Config_Type.setup_Type type)
        {
            string result = "Periodic PHR timer:" + type.periodicPHR_Timer.GetDescription();
            result += ", Prohibit PHR timer:" + type.prohibitPHR_Timer.GetDescription();
            result += ", Downlink pathloss change:" + type.dl_PathlossChange.GetDescription();
            return result;
        }

        public static string GetDescription(
            this MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum timer)
        {
            switch (timer)
            {
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.sf10:
                    return "10 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.sf20:
                    return "20 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.sf50:
                    return "50 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.sf100:
                    return "100 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.sf200:
                    return "200 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.sf500:
                    return "500 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.sf1000:
                    return "1000 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.periodicPHR_Timer_Enum.infinity:
                    return "infinity";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum timer)
        {
            switch (timer)
            {
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf0:
                    return "0";
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf10:
                    return "10 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf20:
                    return "20 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf50:
                    return "50 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf100:
                    return "100 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf200:
                    return "200 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf500:
                    return "500 subframes";
                case MAC_MainConfig.phr_Config_Type.setup_Type.prohibitPHR_Timer_Enum.sf1000:
                    return "1000 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this MAC_MainConfig.phr_Config_Type.setup_Type.dl_PathlossChange_Enum pathloss)
        {
            switch (pathloss)
            {
                case MAC_MainConfig.phr_Config_Type.setup_Type.dl_PathlossChange_Enum.dB1:
                    return "1dB";
                case MAC_MainConfig.phr_Config_Type.setup_Type.dl_PathlossChange_Enum.dB3:
                    return "3dB";
                case MAC_MainConfig.phr_Config_Type.setup_Type.dl_PathlossChange_Enum.dB6:
                    return "6dB";
                case MAC_MainConfig.phr_Config_Type.setup_Type.dl_PathlossChange_Enum.infinity:
                    return "infinity";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this SPS_Config config)
        {
            string result = "Downlink config:" + config.sps_ConfigDL.GetOutputs();
            result += ", Uplink config:" + config.sps_ConfigUL.GetOutputs();
            return result;
        }

        public static string GetOutputs(this SPS_ConfigDL config)
        {
            if (config.release != null) return "release";
            if (config.setup != null)
            {
                return config.setup.GetDescription();
            }
            return "undefined";
        }

        public static string GetDescription(this SPS_ConfigDL.setup_Type type)
        {
            string result 
                = type.n1PUCCH_AN_PersistentList.Aggregate("N1 PUCCH AN persistent list:", 
                (current, persistent) => current + (persistent + ", "));
            result += "Number of conf SPS processes:" + type.numberOfConfSPS_Processes;
            result += ", Semi-persist scheduled interval downlink:" + type.semiPersistSchedIntervalDL.GetDescription();
            return result;
        }

        public static string GetDescription(this SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum interval)
        {
            switch (interval)
            {
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.spare1:
                    return "spare1";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.spare2:
                    return "spare2";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.spare3:
                    return "spare3";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.spare4:
                    return "spare4";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.spare5:
                    return "spare5";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.spare6:
                    return "spare6";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf10:
                    return "10 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf20:
                    return "20 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf32:
                    return "32 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf40:
                    return "40 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf64:
                    return "64 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf80:
                    return "80 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf128:
                    return "128 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf160:
                    return "160 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf320:
                    return "320 subframes";
                case SPS_ConfigDL.setup_Type.semiPersistSchedIntervalDL_Enum.sf640:
                    return "640 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetOutputs(this SPS_ConfigUL config)
        {
            if (config.release != null)
                return "release";
            if (config.setup != null)
                return config.setup.GetOutputs();
            return "undefined";
        }

        public static string GetOutputs(this SPS_ConfigUL.setup_Type type)
        {
            string result = "Implicit release after:" + type.implicitReleaseAfter.GetDescription();
            result += ", P0 persistent:" + type.p0_Persistent.GetDescription();
            result += ", Semi-persist scheduled interval uplink:" 
                + type.semiPersistSchedIntervalUL.GetDescription();
            result += ", " + type.twoIntervalsConfig.GetDescription();
            return result;
        }

        public static string GetDescription(
            this SPS_ConfigUL.setup_Type.implicitReleaseAfter_Enum release)
        {
            switch (release)
            {
                case SPS_ConfigUL.setup_Type.implicitReleaseAfter_Enum.e2:
                    return "e2";
                case SPS_ConfigUL.setup_Type.implicitReleaseAfter_Enum.e3:
                    return "e3";
                case SPS_ConfigUL.setup_Type.implicitReleaseAfter_Enum.e4:
                    return "e4";
                case SPS_ConfigUL.setup_Type.implicitReleaseAfter_Enum.e8:
                    return "e8";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(
            this SPS_ConfigUL.setup_Type.p0_Persistent_Type type)
        {
            string result = "";
            if (type.p0_NominalPUSCH_Persistent > 0)
                result += "Nominal PUSCH:" + type.p0_NominalPUSCH_Persistent;
            if (type.p0_UE_PUSCH_Persistent > 0)
                result += ", UE PUSCH:" + type.p0_UE_PUSCH_Persistent;
            return result;
        }

        public static string GetDescription(
            this SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum interval)
        {
            switch (interval)
            {
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.spare1:
                    return "spare1";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.spare2:
                    return "spare2";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.spare3:
                    return "spare3";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.spare4:
                    return "spare4";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.spare5:
                    return "spare5";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.spare6:
                    return "spare6";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf10:
                    return "10 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf20:
                    return "20 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf32:
                    return "32 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf40:
                    return "40 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf64:
                    return "64 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf80:
                    return "80 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf128:
                    return "128 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf160:
                    return "160 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf320:
                    return "320 subframes";
                case SPS_ConfigUL.setup_Type.semiPersistSchedIntervalUL_Enum.sf640:
                    return "640 subframes";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this SPS_ConfigUL.setup_Type.twoIntervalsConfig_Enum? config)
        {
            if (config == null) return "undefined";
            switch (config)
            {
                case SPS_ConfigUL.setup_Type.twoIntervalsConfig_Enum._true:
                    return "true";
                default:
                    return "false";
            }
        }
    }
}
