namespace TraceParser.X2ap
{
    public enum AdditionalSpecialSubframePatterns
    {
        ssp0,
        ssp1,
        ssp2,
        ssp3,
        ssp4,
        ssp5,
        ssp6,
        ssp7,
        ssp8,
        ssp9
    }

    public enum CauseMisc
    {
        control_processing_overload,
        hardware_failure,
        om_intervention,
        not_enough_user_plane_processing_resources,
        unspecified
    }

    public enum CauseProtocol
    {
        transfer_syntax_error,
        abstract_syntax_error_reject,
        abstract_syntax_error_ignore_and_notify,
        message_not_compatible_with_receiver_state,
        semantic_error,
        unspecified,
        abstract_syntax_error_falsely_constructed_message
    }

    public enum CauseRadioNetwork
    {
        handover_desirable_for_radio_reasons,
        time_critical_handover,
        resource_optimisation_handover,
        reduce_load_in_serving_cell,
        partial_handover,
        unknown_new_eNB_UE_X2AP_ID,
        unknown_old_eNB_UE_X2AP_ID,
        unknown_pair_of_UE_X2AP_ID,
        ho_target_not_allowed,
        tx2relocoverall_expiry,
        trelocprep_expiry,
        cell_not_available,
        no_radio_resources_available_in_target_cell,
        invalid_MME_GroupID,
        unknown_MME_Code,
        encryption_and_or_integrity_protection_algorithms_not_supported,
        reportCharacteristicsEmpty,
        noReportPeriodicity,
        existingMeasurementID,
        unknown_eNB_Measurement_ID,
        measurement_temporarily_not_available,
        unspecified,
        load_balancing,
        handover_optimisation,
        value_out_of_allowed_range,
        multiple_E_RAB_ID_instances,
        switch_off_ongoing,
        not_supported_QCI_value
    }

    public enum CauseTransport
    {
        transport_resource_unavailable,
        unspecified
    }

    public enum Cell_Size
    {
        verysmall,
        small,
        medium,
        large
    }

    public enum Criticality
    {
        reject,
        ignore,
        notify
    }

    public enum CSGMembershipStatus
    {
        member,
        not_member
    }

    public enum CyclicPrefixDL
    {
        normal,
        extended
    }

    public enum CyclicPrefixUL
    {
        normal,
        extended
    }

    public enum DeactivationIndication
    {
        deactivated
    }

    public enum DL_Forwarding
    {
        dL_forwardingProposed
    }

    public enum EventType
    {
        change_of_serving_cell
    }

    public enum ExpectedHOInterval
    {
        sec15,
        sec30,
        sec60,
        sec90,
        sec120,
        sec180,
        long_time
    }

    public enum ForbiddenInterRATs
    {
        all,
        geran,
        utran,
        cdma2000,
        geranandutran,
        cdma2000andutran
    }

    public enum HandoverReportType
    {
        hoTooEarly,
        hoToWrongCell
    }

    public enum InvokeIndication
    {
        abs_information
    }

    public enum Links_to_log
    {
        uplink,
        downlink,
        both_uplink_and_downlink
    }

    public enum LoadIndicator
    {
        lowLoad,
        mediumLoad,
        highLoad,
        overLoad
    }

    public enum M1ReportingTrigger
    {
        periodic,
        a2eventtriggered,
        a2eventtriggered_periodic
    }

    public enum M3period
    {
        ms100,
        ms1000,
        ms10000
    }

    public enum M4period
    {
        ms1024,
        ms2048,
        ms5120,
        ms10240,
        min1
    }

    public enum M5period
    {
        ms1024,
        ms2048,
        ms5120,
        ms10240,
        min1
    }

    public enum ManagementBasedMDTallowed
    {
        allowed
    }

    public enum MDT_Activation
    {
        immediate_MDT_only,
        immediate_MDT_and_Trace
    }

    public enum Number_of_Antennaports
    {
        an1,
        an2,
        an4
    }

    public enum PartialSuccessIndicator
    {
        partial_success_allowed
    }

    public enum Pre_emptionCapability
    {
        shall_not_trigger_pre_emption,
        may_trigger_pre_emption
    }

    public enum Pre_emptionVulnerability
    {
        not_pre_emptable,
        pre_emptable
    }

    public enum Presence
    {
        optional,
        conditional,
        mandatory
    }

    public enum RadioframeAllocationPeriod
    {
        n1,
        n2,
        n4,
        n8,
        n16,
        n32
    }

    public enum Registration_Request
    {
        start,
        stop
    }

    public enum ReportAmountMDT
    {
        r1,
        r2,
        r4,
        r8,
        r16,
        r32,
        r64,
        rinfinity
    }

    public enum ReportArea
    {
        ecgi
    }

    public enum ReportingPeriodicity
    {
        one_thousand_ms,
        two_thousand_ms,
        five_thousand_ms,
        ten_thousand_ms
    }

    public enum ReportIntervalMDT
    {
        ms120,
        ms240,
        ms480,
        ms640,
        ms1024,
        ms2048,
        ms5120,
        ms10240,
        min1,
        min6,
        min12,
        min30,
        min60
    }

    public enum RNTP_Threshold
    {
        minusInfinity,
        minusEleven,
        minusTen,
        minusNine,
        minusEight,
        minusSeven,
        minusSix,
        minusFive,
        minusFour,
        minusThree,
        minusTwo,
        minusOne,
        zero,
        one,
        two,
        three
    }

    public enum RRCConnReestabIndicator
    {
        reconfigurationFailure,
        handoverFailure,
        otherFailure
    }

    public enum RRCConnSetupIndicator
    {
        rrcConnSetup
    }

    public enum SourceOfUEActivityBehaviourInformation
    {
        subscription_information,
        statistics
    }

    public enum SpecialSubframePatterns
    {
        ssp0,
        ssp1,
        ssp2,
        ssp3,
        ssp4,
        ssp5,
        ssp6,
        ssp7,
        ssp8
    }

    public enum SRVCCOperationPossible
    {
        possible
    }

    public enum SubframeAssignment
    {
        sa0,
        sa1,
        sa2,
        sa3,
        sa4,
        sa5,
        sa6
    }

    public enum TimeToWait
    {
        v1s,
        v2s,
        v5s,
        v10s,
        v20s,
        v60s
    }

    public enum TraceDepth
    {
        minimum,
        medium,
        maximum,
        minimumWithoutVendorSpecificExtension,
        mediumWithoutVendorSpecificExtension,
        maximumWithoutVendorSpecificExtension
    }

    public enum Transmission_Bandwidth
    {
        bw6,
        bw15,
        bw25,
        bw50,
        bw75,
        bw100
    }

    public enum TriggeringMessage
    {
        initiating_message,
        successful_outcome,
        unsuccessful_outcome
    }

    public enum TypeOfError
    {
        not_understood,
        missing
    }

    public enum UL_InterferenceOverloadIndication_Item
    {
        high_interference,
        medium_interference,
        low_interference
    }

}
