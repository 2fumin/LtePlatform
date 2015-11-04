using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceParser.S1ap
{
    public enum CauseMisc
    {
        control_processing_overload,
        not_enough_user_plane_processing_resources,
        hardware_failure,
        om_intervention,
        unspecified,
        unknown_PLMN
    }

    public enum CauseNas
    {
        normal_release,
        authentication_failure,
        detach,
        unspecified,
        csg_subscription_expiry
    }

    public enum CauseProtocol
    {
        transfer_syntax_error,
        abstract_syntax_error_reject,
        abstract_syntax_error_ignore_and_notify,
        message_not_compatible_with_receiver_state,
        semantic_error,
        abstract_syntax_error_falsely_constructed_message,
        unspecified
    }

    public enum CauseRadioNetwork
    {
        unspecified,
        tx2relocoverall_expiry,
        successful_handover,
        release_due_to_eutran_generated_reason,
        handover_cancelled,
        partial_handover,
        ho_failure_in_target_EPC_eNB_or_target_system,
        ho_target_not_allowed,
        tS1relocoverall_expiry,
        tS1relocprep_expiry,
        cell_not_available,
        unknown_targetID,
        no_radio_resources_available_in_target_cell,
        unknown_mme_ue_s1ap_id,
        unknown_enb_ue_s1ap_id,
        unknown_pair_ue_s1ap_id,
        handover_desirable_for_radio_reason,
        time_critical_handover,
        resource_optimisation_handover,
        reduce_load_in_serving_cell,
        user_inactivity,
        radio_connection_with_ue_lost,
        load_balancing_tau_required,
        cs_fallback_triggered,
        ue_not_available_for_ps_service,
        radio_resources_not_available,
        failure_in_radio_interface_procedure,
        invalid_qos_combination,
        interrat_redirection,
        interaction_with_other_procedure,
        unknown_E_RAB_ID,
        multiple_E_RAB_ID_instances,
        encryption_and_or_integrity_protection_algorithms_not_supported,
        s1_intra_system_handover_triggered,
        s1_inter_system_handover_triggered,
        x2_handover_triggered,
        redirection_towards_1xRTT,
        not_supported_QCI_value,
        invalid_CSG_Id
    }

    public enum CauseTransport
    {
        transport_resource_unavailable,
        unspecified
    }

    public enum Cdma2000HORequiredIndication
    {
        True
    }

    public enum Cdma2000HOStatus
    {
        hOSuccess,
        hOFailure
    }

    public enum Cdma2000RATType
    {
        hRPD,
        onexRTT
    }

    public enum Cell_Size
    {
        verysmall,
        small,
        medium,
        large
    }

    public enum CellAccessMode
    {
        hybrid
    }

    public enum CNDomain
    {
        ps,
        cs
    }

    public enum ConcurrentWarningMessageIndicator
    {
        True
    }

    public enum Criticality
    {
        reject,
        ignore,
        notify
    }

    public enum CSFallbackIndicator
    {
        cs_fallback_required,
        cs_fallback_high_priority
    }

    public enum CSGMembershipStatus
    {
        member,
        not_member
    }

    public enum Data_Forwarding_Not_Possible
    {
        data_Forwarding_not_Possible
    }

    public enum Direct_Forwarding_Path_Availability
    {
        directPathAvailable
    }

    public enum DL_Forwarding
    {
        dL_Forwarding_proposed
    }

    public enum EventType
    {
        direct,
        change_of_serve_cell,
        stop_change_of_serve_cell
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

    public enum HandoverType
    {
        intralte,
        ltetoutran,
        ltetogeran,
        utrantolte,
        gerantolte
    }

    public enum OverloadAction
    {
        reject_non_emergency_mo_dt,
        reject_all_rrc_cr_signalling,
        permit_emergency_sessions_and_mobile_terminated_services_only
    }

    public enum PagingDRX
    {
        v32,
        v64,
        v128,
        v256
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

    public enum PS_ServiceNotAvailable
    {
        ps_service_not_available
    }

    public enum ReportArea
    {
        ecgi
    }

    public enum ResetAll
    {
        reset_all
    }

    public enum RRC_Establishment_Cause
    {
        emergency,
        highPriorityAccess,
        mt_Access,
        mo_Signalling,
        mo_Data
    }

    public enum SONInformationRequest
    {
        x2TNL_Configuration_Info,
        time_Synchronization_Info
    }

    public enum SRVCCHOIndication
    {
        pSandCS,
        cSonly
    }

    public enum SRVCCOperationPossible
    {
        possible
    }

    public enum SynchronizationStatus
    {
        synchronous,
        asynchronous
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

    public enum TriggeringMessage
    {
        initiating_message,
        successful_outcome,
        unsuccessfull_outcome
    }

    public enum TypeOfError
    {
        not_understood,
        missing
    }

}
