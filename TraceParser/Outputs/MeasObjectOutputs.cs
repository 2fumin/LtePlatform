using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using TraceParser.Eutra;

namespace TraceParser.Outputs
{
    public static class MeasObjectOutputs
    {
        public static string GetOutputs(this MeasObjectToAddMod measObject)
        {
            string result = measObject.measObjectId.ToString();
            if (measObject.measObject.measObjectEUTRA != null)
            {
                var content = measObject.measObject.measObjectEUTRA;
                result += ", Carrier:" + content.carrierFreq;
                result += ", Bandwidth:" + content.allowedMeasBandwidth.GetDescription();
                result += ", Presence Antenna Port 1:" + content.presenceAntennaPort1;
                result += ", Neighbor Cell Config:" + content.neighCellConfig.GetNeighborCellConfigDescription();
                result += ", Offset Freq:" + content.offsetFreq.GetDescription();
                result += ", Measure Cycle Scell:" + content.measCycleSCell_r10.GetDescription();
            }
            return result;
        }

        public static string GetOutputs(this ReportConfigToAddMod reportConfig)
        {
            string result = reportConfig.reportConfigId.ToString();
            if (reportConfig.reportConfig.reportConfigEUTRA != null)
            {
                var content = reportConfig.reportConfig.reportConfigEUTRA;
                if (content.triggerType != null)
                    result += content.triggerType.GetOutputs();
                result += ", Trigger quantity:" + content.triggerQuantity.GetDescription();
                result += ", Report quantity:" + content.reportQuantity.GetDescription();
                result += ", Max report cells:" + content.maxReportCells;
                result += ", Report interval:" + content.reportInterval.GetDescription();
                result += ", Report amount:" + content.reportAmount.GetDescription();
                if (content.si_RequestForHO_r9 != null)
                    result += ", SI request for HO:" + content.si_RequestForHO_r9.GetDescription();
                if (content.ue_RxTxTimeDiffPeriodical_r9 != null)
                    result += ", UE Rx/Tx time diferent periodical:" 
                        + content.ue_RxTxTimeDiffPeriodical_r9.GetDescription();
                if (content.includeLocationInfo_r10 != null)
                    result += ", Include location info:" + content.includeLocationInfo_r10.GetDescription();
                if (content.reportAddNeighMeas_r10 != null)
                    result += ", Report add neighbors:" + content.reportAddNeighMeas_r10.GetDescription();
            }
            return result;
        }

        public static string GetOutputs(this MeasIdToAddMod measId)
        {
            return "measId: " + measId.measId +
                   ", measObjectId: " + measId.measObjectId +
                   ", reportConfigId: " + measId.reportConfigId;
        }

        public static string GetOutputs(this ReportConfigEUTRA.triggerType_Type type)
        {
            string result = "";
            if (type.event_ != null)
            {
                var eventId = type.event_.eventId;
                if (eventId.eventA3 != null)
                {
                    result += ", A3 Event: offset: " + eventId.eventA3.a3_Offset 
                        + ", report on leave:" + eventId.eventA3.reportOnLeave;
                }
                if (eventId.eventA2 != null)
                {
                    result += ", A2 Event: threshold-RSRP: " + eventId.eventA2.a2_Threshold.threshold_RSRP;
                }
                result += ", Hysteresis:" + type.event_.hysteresis;
                result += ", Time to trigger:" + type.event_.timeToTrigger.GetDescription();
            }
            if (type.periodical != null)
            {
                result += ", Periodical: purpose: " + type.periodical.purpose.GetDescription();
            }
            return result;
        }

        public static string GetDescription(this ReportConfigEUTRA.triggerQuantity_Enum quantity)
        {
            switch (quantity)
            {
                case ReportConfigEUTRA.triggerQuantity_Enum.rsrp:
                    return "RSRP";
                case ReportConfigEUTRA.triggerQuantity_Enum.rsrq:
                    return "RSRQ";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportConfigEUTRA.reportQuantity_Enum quantity)
        {
            switch (quantity)
            {
                case ReportConfigEUTRA.reportQuantity_Enum.both:
                    return "both";
                case ReportConfigEUTRA.reportQuantity_Enum.sameAsTriggerQuantity:
                    return "same as trigger quantity";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportConfigEUTRA.reportAmount_Enum amount)
        {
            switch (amount)
            {
                case ReportConfigEUTRA.reportAmount_Enum.r1:
                    return "once";
                case ReportConfigEUTRA.reportAmount_Enum.r2:
                    return "twice";
                case ReportConfigEUTRA.reportAmount_Enum.r4:
                    return "4times";
                case ReportConfigEUTRA.reportAmount_Enum.r8:
                    return "8times";
                case ReportConfigEUTRA.reportAmount_Enum.r16:
                    return "16times";
                case ReportConfigEUTRA.reportAmount_Enum.r32:
                    return "32times";
                case ReportConfigEUTRA.reportAmount_Enum.r64:
                    return "64times";
                case ReportConfigEUTRA.reportAmount_Enum.infinity:
                    return "infinity";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportConfigEUTRA.triggerType_Type.periodical_Type.purpose_Enum purpose)
        {
            switch (purpose)
            {
                case ReportConfigEUTRA.triggerType_Type.periodical_Type.purpose_Enum.reportCGI:
                    return "report CGI";
                case ReportConfigEUTRA.triggerType_Type.periodical_Type.purpose_Enum.reportStrongestCells:
                    return "report strongest cells";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportConfigEUTRA.si_RequestForHO_r9_Enum? siRequest)
        {
            if (siRequest == null) return "undefined";
            switch (siRequest)
            {
                case ReportConfigEUTRA.si_RequestForHO_r9_Enum.setup:
                    return "setup";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportConfigEUTRA.ue_RxTxTimeDiffPeriodical_r9_Enum? periodical)
        {
            if (periodical == null) return "undefined";
            switch (periodical)
            {
                case ReportConfigEUTRA.ue_RxTxTimeDiffPeriodical_r9_Enum.setup:
                    return "setup";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportConfigEUTRA.includeLocationInfo_r10_Enum? info)
        {
            if (info == null) return "undefined";
            switch (info)
            {
                case ReportConfigEUTRA.includeLocationInfo_r10_Enum._true:
                    return "true";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportConfigEUTRA.reportAddNeighMeas_r10_Enum? report)
        {
            if (report == null) return "undefined";
            switch (report)
            {
                case ReportConfigEUTRA.reportAddNeighMeas_r10_Enum.setup:
                    return "setup";
                default:
                    return "undefined";
            }
        }
    }
}
