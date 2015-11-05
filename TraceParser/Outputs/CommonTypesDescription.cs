using TraceParser.Eutra;

namespace TraceParser.Outputs
{
    public static class CommonTypesDescription
    {
        public static string GetDescription(this AllowedMeasBandwidth bandwidth)
        {
            switch (bandwidth)
            {
                case AllowedMeasBandwidth.mbw6:
                    return "mbw6";
                case AllowedMeasBandwidth.mbw15:
                    return "mbw15";
                case AllowedMeasBandwidth.mbw25:
                    return "mbw25";
                case AllowedMeasBandwidth.mbw50:
                    return "mbw50";
                case AllowedMeasBandwidth.mbw75:
                    return "mbw75";
                case AllowedMeasBandwidth.mbw100:
                    return "mbw100";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this Q_OffsetRange range)
        {
            switch (range)
            {
                case Q_OffsetRange.dB_24:
                    return "-24dB";
                case Q_OffsetRange.dB_22:
                    return "-22dB";
                case Q_OffsetRange.dB_20:
                    return "-20dB";
                case Q_OffsetRange.dB_18:
                    return "-18dB";
                case Q_OffsetRange.dB_16:
                    return "-16dB";
                case Q_OffsetRange.dB_14:
                    return "-14dB";
                case Q_OffsetRange.dB_12:
                    return "-12dB";
                case Q_OffsetRange.dB_10:
                    return "-10dB";
                case Q_OffsetRange.dB_8:
                    return "-8dB";
                case Q_OffsetRange.dB_6:
                    return "-6dB";
                case Q_OffsetRange.dB_5:
                    return "-5dB";
                case Q_OffsetRange.dB_4:
                    return "-4dB";
                case Q_OffsetRange.dB_3:
                    return "-3dB";
                case Q_OffsetRange.dB_2:
                    return "-2dB";
                case Q_OffsetRange.dB_1:
                    return "-1dB";
                case Q_OffsetRange.dB0:
                    return "0dB";
                case Q_OffsetRange.dB1:
                    return "1dB";
                case Q_OffsetRange.dB2:
                    return "2dB";
                case Q_OffsetRange.dB3:
                    return "3dB";
                case Q_OffsetRange.dB4:
                    return "4dB";
                case Q_OffsetRange.dB5:
                    return "5dB";
                case Q_OffsetRange.dB6:
                    return "6dB";
                case Q_OffsetRange.dB8:
                    return "8dB";
                case Q_OffsetRange.dB10:
                    return "10dB";
                case Q_OffsetRange.dB12:
                    return "12dB";
                case Q_OffsetRange.dB14:
                    return "14dB";
                case Q_OffsetRange.dB16:
                    return "16dB";
                case Q_OffsetRange.dB18:
                    return "18dB";
                case Q_OffsetRange.dB20:
                    return "20dB";
                case Q_OffsetRange.dB22:
                    return "22dB";
                case Q_OffsetRange.dB24:
                    return "24dB";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this MeasCycleSCell_r10? config)
        {
            switch (config)
            {
                case null:
                    return "No such field for early version";
                case MeasCycleSCell_r10.sf160:
                    return "sf160";
                case MeasCycleSCell_r10.sf256:
                    return "sf256";
                case MeasCycleSCell_r10.sf320:
                    return "sf320";
                case MeasCycleSCell_r10.sf512:
                    return "sf512";
                case MeasCycleSCell_r10.sf640:
                    return "sf640";
                case MeasCycleSCell_r10.sf1024:
                    return "sf1024";
                case MeasCycleSCell_r10.sf1280:
                    return "sf1280";
                case MeasCycleSCell_r10.spare1:
                    return "spare1";
                default:
                    return "undefiend";
            }
        }

        public static string GetDescription(this TimeToTrigger trigger)
        {
            switch (trigger)
            {
                case TimeToTrigger.ms0:
                    return "0ms";
                case TimeToTrigger.ms40:
                    return "40ms";
                case TimeToTrigger.ms64:
                    return "64ms";
                case TimeToTrigger.ms80:
                    return "80ms";
                case TimeToTrigger.ms100:
                    return "100ms";
                case TimeToTrigger.ms128:
                    return "128ms";
                case TimeToTrigger.ms160:
                    return "160ms";
                case TimeToTrigger.ms256:
                    return "256ms";
                case TimeToTrigger.ms320:
                    return "320ms";
                case TimeToTrigger.ms480:
                    return "480ms";
                case TimeToTrigger.ms512:
                    return "512ms";
                case TimeToTrigger.ms640:
                    return "640ms";
                case TimeToTrigger.ms1024:
                    return "1024ms";
                case TimeToTrigger.ms1280:
                    return "1280ms";
                case TimeToTrigger.ms2560:
                    return "2560ms";
                case TimeToTrigger.ms5120:
                    return "5120ms";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this ReportInterval interval)
        {
            switch (interval)
            {
                case ReportInterval.ms120:
                    return "120ms";
                case ReportInterval.ms240:
                    return "240ms";
                case ReportInterval.ms480:
                    return "480ms";
                case ReportInterval.ms640:
                    return "640ms";
                case ReportInterval.ms1024:
                    return "1024ms";
                case ReportInterval.ms2048:
                    return "2048ms";
                case ReportInterval.ms5120:
                    return "5120ms";
                case ReportInterval.ms10240:
                    return "10240ms";
                case ReportInterval.min1:
                    return "1min";
                case ReportInterval.min6:
                    return "6min";
                case ReportInterval.min12:
                    return "12min";
                case ReportInterval.min30:
                    return "30min";
                case ReportInterval.min60:
                    return "60min";
                default:
                    return "undefined";
            }
        }

        public static string GetNeighborCellConfigDescription(this string config)
        {
            switch (config)
            {
                case "00'B":
                    return "Different MBSFN";
                case "01'B":
                    return "No MBSFN";
                case "10'B":
                    return "Identical MBSFN";
                case "11'B":
                    return "TDD config";
                default:
                    return "Undefined";
            }
        }

        public static string GetDescription(this T_PollRetransmit retransmit)
        {
            switch (retransmit)
            {
                case T_PollRetransmit.ms5:
                    return "5ms";
                case T_PollRetransmit.ms10:
                    return "10ms";
                case T_PollRetransmit.ms15:
                    return "15ms";
                case T_PollRetransmit.ms20:
                    return "20ms";
                case T_PollRetransmit.ms25:
                    return "25ms";
                case T_PollRetransmit.ms30:
                    return "30ms";
                case T_PollRetransmit.ms35:
                    return "35ms";
                case T_PollRetransmit.ms40:
                    return "40ms";
                case T_PollRetransmit.ms45:
                    return "45ms";
                case T_PollRetransmit.ms50:
                    return "50ms";
                case T_PollRetransmit.ms55:
                    return "55ms";
                case T_PollRetransmit.ms60:
                    return "60ms";
                case T_PollRetransmit.ms65:
                    return "65ms";
                case T_PollRetransmit.ms70:
                    return "70ms";
                case T_PollRetransmit.ms75:
                    return "75ms";
                case T_PollRetransmit.ms80:
                    return "80ms";
                case T_PollRetransmit.ms85:
                    return "85ms";
                case T_PollRetransmit.ms90:
                    return "90ms";
                case T_PollRetransmit.ms95:
                    return "95ms";
                case T_PollRetransmit.ms100:
                    return "100ms";
                case T_PollRetransmit.ms105:
                    return "105ms";
                case T_PollRetransmit.ms110:
                    return "110ms";
                case T_PollRetransmit.ms115:
                    return "115ms";
                case T_PollRetransmit.ms120:
                    return "120ms";
                case T_PollRetransmit.ms125:
                    return "125ms";
                case T_PollRetransmit.ms130:
                    return "130ms";
                case T_PollRetransmit.ms135:
                    return "135ms";
                case T_PollRetransmit.ms140:
                    return "140ms";
                case T_PollRetransmit.ms145:
                    return "145ms";
                case T_PollRetransmit.ms150:
                    return "150ms";
                case T_PollRetransmit.ms155:
                    return "155ms";
                case T_PollRetransmit.ms160:
                    return "160ms";
                case T_PollRetransmit.ms165:
                    return "165ms";
                case T_PollRetransmit.ms170:
                    return "170ms";
                case T_PollRetransmit.ms175:
                    return "175ms";
                case T_PollRetransmit.ms180:
                    return "180ms";
                case T_PollRetransmit.ms185:
                    return "185ms";
                case T_PollRetransmit.ms190:
                    return "195ms";
                case T_PollRetransmit.ms200:
                    return "200ms";
                case T_PollRetransmit.ms205:
                    return "205ms";
                case T_PollRetransmit.ms210:
                    return "210ms";
                case T_PollRetransmit.ms215:
                    return "215ms";
                case T_PollRetransmit.ms220:
                    return "220ms";
                case T_PollRetransmit.ms225:
                    return "225ms";
                case T_PollRetransmit.ms230:
                    return "230ms";
                case T_PollRetransmit.ms235:
                    return "235ms";
                case T_PollRetransmit.ms240:
                    return "240ms";
                case T_PollRetransmit.ms245:
                    return "245ms";
                case T_PollRetransmit.ms250:
                    return "250ms";
                case T_PollRetransmit.ms300:
                    return "300ms";
                case T_PollRetransmit.ms350:
                    return "350ms";
                case T_PollRetransmit.ms400:
                    return "400ms";
                case T_PollRetransmit.ms450:
                    return "450ms";
                case T_PollRetransmit.ms500:
                    return "500ms";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this PollPDU pdu)
        {
            switch (pdu)
            {
                case PollPDU.p4:
                    return "4";
                case PollPDU.p8:
                    return "8";
                case PollPDU.p16:
                    return "16";
                case PollPDU.p32:
                    return "32";
                case PollPDU.p64:
                    return "64";
                case PollPDU.p128:
                    return "128";
                case PollPDU.p256:
                    return "256";
                case PollPDU.pInfinity:
                    return "infinity";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this PollByte poll)
        {
            switch (poll)
            {
                case PollByte.kB25:
                    return "25kB";
                case PollByte.kB50:
                    return "50kB";
                case PollByte.kB75:
                    return "75kB";
                case PollByte.kB100:
                    return "100kB";
                case PollByte.kB125:
                    return "125kB";
                case PollByte.kB250:
                    return "250kB";
                case PollByte.kB375:
                    return "375kB";
                case PollByte.kB500:
                    return "500kB";
                case PollByte.kB750:
                    return "750kB";
                case PollByte.kB1000:
                    return "1000kB";
                case PollByte.kB1250:
                    return "1250kB";
                case PollByte.kB1500:
                    return "1500kB";
                case PollByte.kB2000:
                    return "2000kB";
                case PollByte.kB3000:
                    return "3000kB";
                case PollByte.kBinfinity:
                    return "infinity";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this T_Reordering reordering)
        {
            switch (reordering)
            {
                case T_Reordering.ms0:
                    return "0ms";
                case T_Reordering.ms5:
                    return "5ms";
                case T_Reordering.ms10:
                    return "10ms";
                case T_Reordering.ms15:
                    return "15ms";
                case T_Reordering.ms20:
                    return "20ms";
                case T_Reordering.ms25:
                    return "25ms";
                case T_Reordering.ms30:
                    return "30ms";
                case T_Reordering.ms35:
                    return "35ms";
                case T_Reordering.ms40:
                    return "40ms";
                case T_Reordering.ms45:
                    return "45ms";
                case T_Reordering.ms50:
                    return "50ms";
                case T_Reordering.ms55:
                    return "55ms";
                case T_Reordering.ms60:
                    return "60ms";
                case T_Reordering.ms65:
                    return "65ms";
                case T_Reordering.ms70:
                    return "70ms";
                case T_Reordering.ms75:
                    return "75ms";
                case T_Reordering.ms80:
                    return "80ms";
                case T_Reordering.ms85:
                    return "85ms";
                case T_Reordering.ms90:
                    return "90ms";
                case T_Reordering.ms95:
                    return "95ms";
                case T_Reordering.ms100:
                    return "100ms";
                case T_Reordering.ms110:
                    return "110ms";
                case T_Reordering.ms120:
                    return "120ms";
                case T_Reordering.ms130:
                    return "130ms";
                case T_Reordering.ms140:
                    return "140ms";
                case T_Reordering.ms150:
                    return "150ms";
                case T_Reordering.ms160:
                    return "160ms";
                case T_Reordering.ms170:
                    return "170ms";
                case T_Reordering.ms180:
                    return "180ms";
                case T_Reordering.ms190:
                    return "190ms";
                case T_Reordering.ms200:
                    return "200ms";
                case T_Reordering.spare1:
                    return "spare1";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this T_StatusProhibit status)
        {
            switch (status)
            {
                case T_StatusProhibit.ms0:
                    return "0ms";
                case T_StatusProhibit.ms5:
                    return "5ms";
                case T_StatusProhibit.ms10:
                    return "10ms";
                case T_StatusProhibit.ms15:
                    return "15ms";
                case T_StatusProhibit.ms20:
                    return "20ms";
                case T_StatusProhibit.ms25:
                    return "25ms";
                case T_StatusProhibit.ms30:
                    return "30ms";
                case T_StatusProhibit.ms35:
                    return "35ms";
                case T_StatusProhibit.ms40:
                    return "40ms";
                case T_StatusProhibit.ms45:
                    return "45ms";
                case T_StatusProhibit.ms50:
                    return "50ms";
                case T_StatusProhibit.ms55:
                    return "55ms";
                case T_StatusProhibit.ms60:
                    return "60ms";
                case T_StatusProhibit.ms65:
                    return "65ms";
                case T_StatusProhibit.ms70:
                    return "70ms";
                case T_StatusProhibit.ms75:
                    return "75ms";
                case T_StatusProhibit.ms80:
                    return "80ms";
                case T_StatusProhibit.ms85:
                    return "85ms";
                case T_StatusProhibit.ms90:
                    return "90ms";
                case T_StatusProhibit.ms95:
                    return "95ms";
                case T_StatusProhibit.ms100:
                    return "100ms";
                case T_StatusProhibit.ms105:
                    return "105ms";
                case T_StatusProhibit.ms110:
                    return "110ms";
                case T_StatusProhibit.ms115:
                    return "115ms";
                case T_StatusProhibit.ms120:
                    return "120ms";
                case T_StatusProhibit.ms125:
                    return "125ms";
                case T_StatusProhibit.ms130:
                    return "130ms";
                case T_StatusProhibit.ms135:
                    return "135ms";
                case T_StatusProhibit.ms140:
                    return "140ms";
                case T_StatusProhibit.ms145:
                    return "145ms";
                case T_StatusProhibit.ms150:
                    return "150ms";
                case T_StatusProhibit.ms155:
                    return "155ms";
                case T_StatusProhibit.ms160:
                    return "160ms";
                case T_StatusProhibit.ms165:
                    return "165ms";
                case T_StatusProhibit.ms170:
                    return "170ms";
                case T_StatusProhibit.ms175:
                    return "175ms";
                case T_StatusProhibit.ms180:
                    return "180ms";
                case T_StatusProhibit.ms185:
                    return "185ms";
                case T_StatusProhibit.ms190:
                    return "190ms";
                case T_StatusProhibit.ms195:
                    return "195ms";
                case T_StatusProhibit.ms200:
                    return "200ms";
                case T_StatusProhibit.ms205:
                    return "205ms";
                case T_StatusProhibit.ms210:
                    return "210ms";
                case T_StatusProhibit.ms215:
                    return "215ms";
                case T_StatusProhibit.ms220:
                    return "220ms";
                case T_StatusProhibit.ms225:
                    return "225ms";
                case T_StatusProhibit.ms230:
                    return "230ms";
                case T_StatusProhibit.ms235:
                    return "235ms";
                case T_StatusProhibit.ms240:
                    return "240ms";
                case T_StatusProhibit.ms245:
                    return "245ms";
                case T_StatusProhibit.ms250:
                    return "250ms";
                case T_StatusProhibit.ms300:
                    return "300ms";
                case T_StatusProhibit.ms350:
                    return "350ms";
                case T_StatusProhibit.ms400:
                    return "400ms";
                case T_StatusProhibit.ms450:
                    return "450ms";
                case T_StatusProhibit.ms500:
                    return "500ms";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this FilterCoefficient coefficient)
        {
            switch (coefficient)
            {
                case FilterCoefficient.spare1:
                    return "spare1";
                case FilterCoefficient.fc1:
                    return "1";
                case FilterCoefficient.fc2:
                    return "2";
                case FilterCoefficient.fc3:
                    return "3";
                case FilterCoefficient.fc4:
                    return "4";
                case FilterCoefficient.fc5:
                    return "5";
                case FilterCoefficient.fc6:
                    return "6";
                case FilterCoefficient.fc7:
                    return "7";
                case FilterCoefficient.fc8:
                    return "8";
                case FilterCoefficient.fc9:
                    return "9";
                case FilterCoefficient.fc11:
                    return "11";
                case FilterCoefficient.fc13:
                    return "13";
                case FilterCoefficient.fc15:
                    return "15";
                case FilterCoefficient.fc17:
                    return "17";
                case FilterCoefficient.fc19:
                    return "19";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this CQI_ReportModeAperiodic? mode)
        {
            if (mode == null) return "undefined";
            switch (mode)
            {
                case CQI_ReportModeAperiodic.spare1:
                    return "spare1";
                case CQI_ReportModeAperiodic.spare2:
                    return "spare2";
                case CQI_ReportModeAperiodic.rm12:
                    return "rm12";
                case CQI_ReportModeAperiodic.rm20:
                    return "rm20";
                case CQI_ReportModeAperiodic.rm22:
                    return "rm22";
                case CQI_ReportModeAperiodic.rm30:
                    return "rm30";
                case CQI_ReportModeAperiodic.rm31:
                    return "rm31";
                case CQI_ReportModeAperiodic.rm32_v12xx:
                    return "rm32";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this PreambleTransMax max)
        {
            switch (max)
            {
                case PreambleTransMax.n3:
                    return "3";
                case PreambleTransMax.n4:
                    return "4";
                case PreambleTransMax.n5:
                    return "5";
                case PreambleTransMax.n6:
                    return "6";
                case PreambleTransMax.n7:
                    return "7";
                case PreambleTransMax.n8:
                    return "8";
                case PreambleTransMax.n10:
                    return "10";
                case PreambleTransMax.n20:
                    return "20";
                case PreambleTransMax.n50:
                    return "50";
                case PreambleTransMax.n100:
                    return "100";
                case PreambleTransMax.n200:
                    return "200";
                default:
                    return "undefined";
            }
        }

        public static string GetDescription(this UL_CyclicPrefixLength length)
        {
            switch (length)
            {
                case UL_CyclicPrefixLength.len1:
                    return "1";
                case UL_CyclicPrefixLength.len2:
                    return "2";
                default:
                    return "undefined";
            }
        }
    }
}
