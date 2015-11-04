using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformation
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            public SystemInformation_r8_IEs systemInformation_r8 { get; set; }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        criticalExtensionsFuture_Type type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    criticalExtensions_Type type = new criticalExtensions_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.systemInformation_r8 = SystemInformation_r8_IEs.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformation Decode(BitArrayInputStream input)
            {
                SystemInformation information = new SystemInformation();
                information.InitDefaults();
                information.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return information;
            }
        }
    }

    [Serializable]
    public class SystemInformation_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public SystemInformation_v8a0_IEs nonCriticalExtension { get; set; }

        public List<sib_TypeAndInfo_Element> sib_TypeAndInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformation_r8_IEs Decode(BitArrayInputStream input)
            {
                SystemInformation_r8_IEs es = new SystemInformation_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.sib_TypeAndInfo = new List<sib_TypeAndInfo_Element>();
                const int nBits = 5;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    sib_TypeAndInfo_Element item = sib_TypeAndInfo_Element.PerDecoder.Instance.Decode(input);
                    es.sib_TypeAndInfo.Add(item);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformation_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        [Serializable]
        public class sib_TypeAndInfo_Element
        {
            public void InitDefaults()
            {
            }

            public SystemInformationBlockType10 sib10 { get; set; }

            public SystemInformationBlockType11 sib11 { get; set; }

            public SystemInformationBlockType12_r9 sib12_v920 { get; set; }

            public SystemInformationBlockType13_r9 sib13_v920 { get; set; }

            public SystemInformationBlockType14_r11 sib14_v1130 { get; set; }

            public SystemInformationBlockType15_r11 sib15_v1130 { get; set; }

            public SystemInformationBlockType16_r11 sib16_v1130 { get; set; }

            public SystemInformationBlockType2 sib2 { get; set; }

            public SystemInformationBlockType3 sib3 { get; set; }

            public SystemInformationBlockType4 sib4 { get; set; }

            public SystemInformationBlockType5 sib5 { get; set; }

            public SystemInformationBlockType6 sib6 { get; set; }

            public SystemInformationBlockType7 sib7 { get; set; }

            public SystemInformationBlockType8 sib8 { get; set; }

            public SystemInformationBlockType9 sib9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public sib_TypeAndInfo_Element Decode(BitArrayInputStream input)
                {
                    sib_TypeAndInfo_Element element = new sib_TypeAndInfo_Element();
                    element.InitDefaults();
                    bool flag = input.readBit() != 0;
                    switch (input.readBits(4))
                    {
                        case 0:
                            element.sib2 = SystemInformationBlockType2.PerDecoder.Instance.Decode(input);
                            return element;

                        case 1:
                            element.sib3 = SystemInformationBlockType3.PerDecoder.Instance.Decode(input);
                            return element;

                        case 2:
                            element.sib4 = SystemInformationBlockType4.PerDecoder.Instance.Decode(input);
                            return element;

                        case 3:
                            element.sib5 = SystemInformationBlockType5.PerDecoder.Instance.Decode(input);
                            return element;

                        case 4:
                            element.sib6 = SystemInformationBlockType6.PerDecoder.Instance.Decode(input);
                            return element;

                        case 5:
                            element.sib7 = SystemInformationBlockType7.PerDecoder.Instance.Decode(input);
                            return element;

                        case 6:
                            element.sib8 = SystemInformationBlockType8.PerDecoder.Instance.Decode(input);
                            return element;

                        case 7:
                            element.sib9 = SystemInformationBlockType9.PerDecoder.Instance.Decode(input);
                            return element;

                        case 8:
                            element.sib10 = SystemInformationBlockType10.PerDecoder.Instance.Decode(input);
                            return element;

                        case 9:
                            element.sib11 = SystemInformationBlockType11.PerDecoder.Instance.Decode(input);
                            return element;

                        case 10:
                            if (flag)
                            {
                                element.sib12_v920 = SystemInformationBlockType12_r9.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 11:
                            if (flag)
                            {
                                element.sib13_v920 = SystemInformationBlockType13_r9.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 12:
                            if (flag)
                            {
                                element.sib14_v1130 = SystemInformationBlockType14_r11.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 13:
                            if (flag)
                            {
                                element.sib15_v1130 = SystemInformationBlockType15_r11.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 14:
                            if (flag)
                            {
                                element.sib16_v1130 = SystemInformationBlockType16_r11.PerDecoder.Instance.Decode(input);
                            }
                            return element;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

    [Serializable]
    public class SystemInformation_v8a0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformation_v8a0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformation_v8a0_IEs es = new SystemInformation_v8a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType1
    {
        public void InitDefaults()
        {
        }

        public cellAccessRelatedInfo_Type cellAccessRelatedInfo { get; set; }

        public cellSelectionInfo_Type cellSelectionInfo { get; set; }

        public long freqBandIndicator { get; set; }

        public SystemInformationBlockType1_v890_IEs nonCriticalExtension { get; set; }

        public long? p_Max { get; set; }

        public List<SchedulingInfo> schedulingInfoList { get; set; }

        public si_WindowLength_Enum si_WindowLength { get; set; }

        public long systemInfoValueTag { get; set; }

        public TDD_Config tdd_Config { get; set; }

        [Serializable]
        public class cellAccessRelatedInfo_Type
        {
            public void InitDefaults()
            {
            }

            public cellBarred_Enum cellBarred { get; set; }

            public string cellIdentity { get; set; }

            public string csg_Identity { get; set; }

            public bool csg_Indication { get; set; }

            public intraFreqReselection_Enum intraFreqReselection { get; set; }

            public List<PLMN_IdentityInfo> plmn_IdentityList { get; set; }

            public string trackingAreaCode { get; set; }

            public enum cellBarred_Enum
            {
                barred,
                notBarred
            }

            public enum intraFreqReselection_Enum
            {
                allowed,
                notAllowed
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellAccessRelatedInfo_Type Decode(BitArrayInputStream input)
                {
                    cellAccessRelatedInfo_Type type = new cellAccessRelatedInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.plmn_IdentityList = new List<PLMN_IdentityInfo>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        PLMN_IdentityInfo item = PLMN_IdentityInfo.PerDecoder.Instance.Decode(input);
                        type.plmn_IdentityList.Add(item);
                    }
                    type.trackingAreaCode = input.readBitString(0x10);
                    type.cellIdentity = input.readBitString(0x1c);
                    nBits = 1;
                    type.cellBarred = (cellBarred_Enum)input.readBits(nBits);
                    nBits = 1;
                    type.intraFreqReselection = (intraFreqReselection_Enum)input.readBits(nBits);
                    type.csg_Indication = input.readBit() == 1;
                    if (stream.Read())
                    {
                        type.csg_Identity = input.readBitString(0x1b);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class cellSelectionInfo_Type
        {
            public void InitDefaults()
            {
            }

            public long q_RxLevMin { get; set; }

            public long? q_RxLevMinOffset { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellSelectionInfo_Type Decode(BitArrayInputStream input)
                {
                    cellSelectionInfo_Type type = new cellSelectionInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.q_RxLevMin = input.readBits(6) + -70;
                    if (stream.Read())
                    {
                        type.q_RxLevMinOffset = input.readBits(3) + 1;
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType1 type = new SystemInformationBlockType1();
                type.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                type.cellAccessRelatedInfo = cellAccessRelatedInfo_Type.PerDecoder.Instance.Decode(input);
                type.cellSelectionInfo = cellSelectionInfo_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    type.p_Max = input.readBits(6) + -30;
                }
                type.freqBandIndicator = input.readBits(6) + 1;
                type.schedulingInfoList = new List<SchedulingInfo>();
                int nBits = 5;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    SchedulingInfo item = SchedulingInfo.PerDecoder.Instance.Decode(input);
                    type.schedulingInfoList.Add(item);
                }
                if (stream.Read())
                {
                    type.tdd_Config = TDD_Config.PerDecoder.Instance.Decode(input);
                }
                nBits = 3;
                type.si_WindowLength = (si_WindowLength_Enum)input.readBits(nBits);
                type.systemInfoValueTag = input.readBits(5);
                if (stream.Read())
                {
                    type.nonCriticalExtension = SystemInformationBlockType1_v890_IEs.PerDecoder.Instance.Decode(input);
                }
                return type;
            }
        }

        public enum si_WindowLength_Enum
        {
            ms1,
            ms2,
            ms5,
            ms10,
            ms15,
            ms20,
            ms40
        }
    }

    [Serializable]
    public class SystemInformationBlockType1_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public CellSelectionInfo_v1130 cellSelectionInfo_v1130 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public TDD_Config_v1130 tdd_Config_v1130 { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v1130_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType1_v1130_IEs es = new SystemInformationBlockType1_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.tdd_Config_v1130 = TDD_Config_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.cellSelectionInfo_v1130 = CellSelectionInfo_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType1_v890_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public SystemInformationBlockType1_v920_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v890_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType1_v890_IEs es = new SystemInformationBlockType1_v890_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType1_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType1_v8h0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<long> multiBandInfoList { get; set; }

        public SystemInformationBlockType1_v9e0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v8h0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType1_v8h0_IEs es = new SystemInformationBlockType1_v8h0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.multiBandInfoList = new List<long>();
                    const int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(6) + 1;
                        es.multiBandInfoList.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType1_v9e0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType1_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public CellSelectionInfo_v920 cellSelectionInfo_v920 { get; set; }

        public ims_EmergencySupport_r9_Enum? ims_EmergencySupport_r9 { get; set; }

        public SystemInformationBlockType1_v1130_IEs nonCriticalExtension { get; set; }

        public enum ims_EmergencySupport_r9_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v920_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType1_v920_IEs es = new SystemInformationBlockType1_v920_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.ims_EmergencySupport_r9 = (ims_EmergencySupport_r9_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.cellSelectionInfo_v920 = CellSelectionInfo_v920.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType1_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType1_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public long? freqBandIndicator_v9e0 { get; set; }

        public List<MultiBandInfo_v9e0> multiBandInfoList_v9e0 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v9e0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType1_v9e0_IEs es = new SystemInformationBlockType1_v9e0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.freqBandIndicator_v9e0 = input.readBits(8) + 0x41;
                }
                if (stream.Read())
                {
                    es.multiBandInfoList_v9e0 = new List<MultiBandInfo_v9e0>();
                    const int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        MultiBandInfo_v9e0 item = MultiBandInfo_v9e0.PerDecoder.Instance.Decode(input);
                        es.multiBandInfoList_v9e0.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType10
    {
        public void InitDefaults()
        {
        }

        public string dummy { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public string messageIdentifier { get; set; }

        public string serialNumber { get; set; }

        public string warningType { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType10 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType10 type = new SystemInformationBlockType10();
                type.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 1);
                type.messageIdentifier = input.readBitString(0x10);
                type.serialNumber = input.readBitString(0x10);
                type.warningType = input.readOctetString(2);
                if (stream.Read())
                {
                    type.dummy = input.readOctetString(50);
                }
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType11
    {
        public void InitDefaults()
        {
        }

        public string dataCodingScheme { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public string messageIdentifier { get; set; }

        public string serialNumber { get; set; }

        public string warningMessageSegment { get; set; }

        public long warningMessageSegmentNumber { get; set; }

        public warningMessageSegmentType_Enum warningMessageSegmentType { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType11 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType11 type = new SystemInformationBlockType11();
                type.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 1);
                type.messageIdentifier = input.readBitString(0x10);
                type.serialNumber = input.readBitString(0x10);
                const int num2 = 1;
                type.warningMessageSegmentType = (warningMessageSegmentType_Enum)input.readBits(num2);
                type.warningMessageSegmentNumber = input.readBits(6);
                int nBits = input.readBits(8);
                type.warningMessageSegment = input.readOctetString(nBits);
                if (stream.Read())
                {
                    type.dataCodingScheme = input.readOctetString(1);
                }
                if (flag && stream.Read())
                {
                    nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }

        public enum warningMessageSegmentType_Enum
        {
            notLastSegment,
            lastSegment
        }
    }

    [Serializable]
    public class SystemInformationBlockType12_r9
    {
        public void InitDefaults()
        {
        }

        public string dataCodingScheme_r9 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public string messageIdentifier_r9 { get; set; }

        public string serialNumber_r9 { get; set; }

        public string warningMessageSegment_r9 { get; set; }

        public long warningMessageSegmentNumber_r9 { get; set; }

        public warningMessageSegmentType_r9_Enum warningMessageSegmentType_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType12_r9 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType12_r9 _r = new SystemInformationBlockType12_r9();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                _r.messageIdentifier_r9 = input.readBitString(0x10);
                _r.serialNumber_r9 = input.readBitString(0x10);
                const int num2 = 1;
                _r.warningMessageSegmentType_r9 = (warningMessageSegmentType_r9_Enum)input.readBits(num2);
                _r.warningMessageSegmentNumber_r9 = input.readBits(6);
                int nBits = input.readBits(8);
                _r.warningMessageSegment_r9 = input.readOctetString(nBits);
                if (stream.Read())
                {
                    _r.dataCodingScheme_r9 = input.readOctetString(1);
                }
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }

        public enum warningMessageSegmentType_r9_Enum
        {
            notLastSegment,
            lastSegment
        }
    }

    [Serializable]
    public class SystemInformationBlockType13_r9
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public List<MBSFN_AreaInfo_r9> mbsfn_AreaInfoList_r9 { get; set; }

        public MBMS_NotificationConfig_r9 notificationConfig_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType13_r9 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType13_r9 _r = new SystemInformationBlockType13_r9();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                _r.mbsfn_AreaInfoList_r9 = new List<MBSFN_AreaInfo_r9>();
                const int num2 = 3;
                int num3 = input.readBits(num2) + 1;
                for (int i = 0; i < num3; i++)
                {
                    MBSFN_AreaInfo_r9 item = MBSFN_AreaInfo_r9.PerDecoder.Instance.Decode(input);
                    _r.mbsfn_AreaInfoList_r9.Add(item);
                }
                _r.notificationConfig_r9 = MBMS_NotificationConfig_r9.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType14_r11
    {
        public void InitDefaults()
        {
        }

        public eab_Param_r11_Type eab_Param_r11 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        [Serializable]
        public class eab_Param_r11_Type
        {
            public void InitDefaults()
            {
            }

            public EAB_Config_r11 eab_Common_r11 { get; set; }

            public List<EAB_ConfigPLMN_r11> eab_PerPLMN_List_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public eab_Param_r11_Type Decode(BitArrayInputStream input)
                {
                    eab_Param_r11_Type type = new eab_Param_r11_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.eab_Common_r11 = EAB_Config_r11.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            {
                                type.eab_PerPLMN_List_r11 = new List<EAB_ConfigPLMN_r11>();
                                int nBits = 3;
                                int num4 = input.readBits(nBits) + 1;
                                for (int i = 0; i < num4; i++)
                                {
                                    EAB_ConfigPLMN_r11 item = EAB_ConfigPLMN_r11.PerDecoder.Instance.Decode(input);
                                    type.eab_PerPLMN_List_r11.Add(item);
                                }
                                return type;
                            }
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType14_r11 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType14_r11 _r = new SystemInformationBlockType14_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.eab_Param_r11 = eab_Param_r11_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType15_r11
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public List<MBMS_SAI_InterFreq_r11> mbms_SAI_InterFreqList_r11 { get; set; }

        public List<MBMS_SAI_InterFreq_v1140> mbms_SAI_InterFreqList_v1140 { get; set; }

        public List<long> mbms_SAI_IntraFreq_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType15_r11 Decode(BitArrayInputStream input)
            {
                int num2;
                SystemInformationBlockType15_r11 _r = new SystemInformationBlockType15_r11();
                _r.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    _r.mbms_SAI_IntraFreq_r11 = new List<long>();
                    num2 = 6;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(0x10);
                        _r.mbms_SAI_IntraFreq_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    _r.mbms_SAI_InterFreqList_r11 = new List<MBMS_SAI_InterFreq_r11>();
                    num2 = 3;
                    int num6 = input.readBits(num2) + 1;
                    for (int j = 0; j < num6; j++)
                    {
                        MBMS_SAI_InterFreq_r11 _r2 = MBMS_SAI_InterFreq_r11.PerDecoder.Instance.Decode(input);
                        _r.mbms_SAI_InterFreqList_r11.Add(_r2);
                    }
                }
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (!stream2.Read())
                    {
                        return _r;
                    }
                    _r.mbms_SAI_InterFreqList_v1140 = new List<MBMS_SAI_InterFreq_v1140>();
                    num2 = 3;
                    int num8 = input.readBits(num2) + 1;
                    for (int k = 0; k < num8; k++)
                    {
                        MBMS_SAI_InterFreq_v1140 _v = MBMS_SAI_InterFreq_v1140.PerDecoder.Instance.Decode(input);
                        _r.mbms_SAI_InterFreqList_v1140.Add(_v);
                    }
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType16_r11
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public timeInfo_r11_Type timeInfo_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType16_r11 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType16_r11 _r = new SystemInformationBlockType16_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.timeInfo_r11 = timeInfo_r11_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }

        [Serializable]
        public class timeInfo_r11_Type
        {
            public void InitDefaults()
            {
            }

            public string dayLightSavingTime_r11 { get; set; }

            public long? leapSeconds_r11 { get; set; }

            public long? localTimeOffset_r11 { get; set; }

            public long timeInfoUTC_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public timeInfo_r11_Type Decode(BitArrayInputStream input)
                {
                    timeInfo_r11_Type type = new timeInfo_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 3);
                    type.timeInfoUTC_r11 = input.readBits(40);
                    if (stream.Read())
                    {
                        type.dayLightSavingTime_r11 = input.readBitString(2);
                    }
                    if (stream.Read())
                    {
                        type.leapSeconds_r11 = input.readBits(8) + -127;
                    }
                    if (stream.Read())
                    {
                        type.localTimeOffset_r11 = input.readBits(7) + -63;
                    }
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType2
    {
        public void InitDefaults()
        {
        }

        public AC_BarringConfig ac_BarringForCSFB_r10 { get; set; }

        public ac_BarringInfo_Type ac_BarringInfo { get; set; }

        public freqInfo_Type freqInfo { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public List<MBSFN_SubframeConfig> mbsfn_SubframeConfigList { get; set; }

        public RadioResourceConfigCommonSIB radioResourceConfigCommon { get; set; }

        public AC_BarringConfig ssac_BarringForMMTEL_Video_r9 { get; set; }

        public AC_BarringConfig ssac_BarringForMMTEL_Voice_r9 { get; set; }

        public TimeAlignmentTimer timeAlignmentTimerCommon { get; set; }

        public UE_TimersAndConstants ue_TimersAndConstants { get; set; }

        [Serializable]
        public class ac_BarringInfo_Type
        {
            public void InitDefaults()
            {
            }

            public bool ac_BarringForEmergency { get; set; }

            public AC_BarringConfig ac_BarringForMO_Data { get; set; }

            public AC_BarringConfig ac_BarringForMO_Signalling { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ac_BarringInfo_Type Decode(BitArrayInputStream input)
                {
                    ac_BarringInfo_Type type = new ac_BarringInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    type.ac_BarringForEmergency = input.readBit() == 1;
                    if (stream.Read())
                    {
                        type.ac_BarringForMO_Signalling = AC_BarringConfig.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.ac_BarringForMO_Data = AC_BarringConfig.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class freqInfo_Type
        {
            public void InitDefaults()
            {
            }

            public long additionalSpectrumEmission { get; set; }

            public ul_Bandwidth_Enum? ul_Bandwidth { get; set; }

            public long? ul_CarrierFreq { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public freqInfo_Type Decode(BitArrayInputStream input)
                {
                    freqInfo_Type type = new freqInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        type.ul_CarrierFreq = input.readBits(0x10);
                    }
                    if (stream.Read())
                    {
                        int nBits = 3;
                        type.ul_Bandwidth = (ul_Bandwidth_Enum)input.readBits(nBits);
                    }
                    type.additionalSpectrumEmission = input.readBits(5) + 1;
                    return type;
                }
            }

            public enum ul_Bandwidth_Enum
            {
                n6,
                n15,
                n25,
                n50,
                n75,
                n100
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType2 Decode(BitArrayInputStream input)
            {
                int num2;
                BitMaskStream stream2;
                SystemInformationBlockType2 type = new SystemInformationBlockType2();
                type.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 3) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    type.ac_BarringInfo = ac_BarringInfo_Type.PerDecoder.Instance.Decode(input);
                }
                type.radioResourceConfigCommon = RadioResourceConfigCommonSIB.PerDecoder.Instance.Decode(input);
                type.ue_TimersAndConstants = UE_TimersAndConstants.PerDecoder.Instance.Decode(input);
                type.freqInfo = freqInfo_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    type.mbsfn_SubframeConfigList = new List<MBSFN_SubframeConfig>();
                    num2 = 3;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        MBSFN_SubframeConfig item = MBSFN_SubframeConfig.PerDecoder.Instance.Decode(input);
                        type.mbsfn_SubframeConfigList.Add(item);
                    }
                }
                num2 = 3;
                type.timeAlignmentTimerCommon = (TimeAlignmentTimer)input.readBits(num2);
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 2);
                    if (stream2.Read())
                    {
                        type.ssac_BarringForMMTEL_Voice_r9 = AC_BarringConfig.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.ssac_BarringForMMTEL_Video_r9 = AC_BarringConfig.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        type.ac_BarringForCSFB_r10 = AC_BarringConfig.PerDecoder.Instance.Decode(input);
                    }
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType2_v8h0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<long> multiBandInfoList { get; set; }

        public SystemInformationBlockType2_v9e0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType2_v8h0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType2_v8h0_IEs es = new SystemInformationBlockType2_v8h0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.multiBandInfoList = new List<long>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(5) + 1;
                        es.multiBandInfoList.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType2_v9e0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType2_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public long? ul_CarrierFreq_v9e0 { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType2_v9e0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType2_v9e0_IEs es = new SystemInformationBlockType2_v9e0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.ul_CarrierFreq_v9e0 = input.readBits(0x12) + 0x10000;
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType3
    {
        public void InitDefaults()
        {
        }

        public cellReselectionInfoCommon_Type cellReselectionInfoCommon { get; set; }

        public cellReselectionServingFreqInfo_Type cellReselectionServingFreqInfo { get; set; }

        public intraFreqCellReselectionInfo_Type intraFreqCellReselectionInfo { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public long? q_QualMin_r9 { get; set; }

        public long? q_QualMinWB_r11 { get; set; }

        public s_IntraSearch_v920_Type s_IntraSearch_v920 { get; set; }

        public s_NonIntraSearch_v920_Type s_NonIntraSearch_v920 { get; set; }

        public long? threshServingLowQ_r9 { get; set; }

        [Serializable]
        public class cellReselectionInfoCommon_Type
        {
            public void InitDefaults()
            {
            }

            public q_Hyst_Enum q_Hyst { get; set; }

            public speedStateReselectionPars_Type speedStateReselectionPars { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellReselectionInfoCommon_Type Decode(BitArrayInputStream input)
                {
                    cellReselectionInfoCommon_Type type = new cellReselectionInfoCommon_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    const int nBits = 4;
                    type.q_Hyst = (q_Hyst_Enum)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.speedStateReselectionPars = speedStateReselectionPars_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }

            public enum q_Hyst_Enum
            {
                dB0,
                dB1,
                dB2,
                dB3,
                dB4,
                dB5,
                dB6,
                dB8,
                dB10,
                dB12,
                dB14,
                dB16,
                dB18,
                dB20,
                dB22,
                dB24
            }

            [Serializable]
            public class speedStateReselectionPars_Type
            {
                public void InitDefaults()
                {
                }

                public MobilityStateParameters mobilityStateParameters { get; set; }

                public q_HystSF_Type q_HystSF { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public speedStateReselectionPars_Type Decode(BitArrayInputStream input)
                    {
                        speedStateReselectionPars_Type type = new speedStateReselectionPars_Type();
                        type.InitDefaults();
                        type.mobilityStateParameters = MobilityStateParameters.PerDecoder.Instance.Decode(input);
                        type.q_HystSF = q_HystSF_Type.PerDecoder.Instance.Decode(input);
                        return type;
                    }
                }

                [Serializable]
                public class q_HystSF_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public sf_High_Enum sf_High { get; set; }

                    public sf_Medium_Enum sf_Medium { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public q_HystSF_Type Decode(BitArrayInputStream input)
                        {
                            q_HystSF_Type type = new q_HystSF_Type();
                            type.InitDefaults();
                            int nBits = 2;
                            type.sf_Medium = (sf_Medium_Enum)input.readBits(nBits);
                            nBits = 2;
                            type.sf_High = (sf_High_Enum)input.readBits(nBits);
                            return type;
                        }
                    }

                    public enum sf_High_Enum
                    {
                        dB_6,
                        dB_4,
                        dB_2,
                        dB0
                    }

                    public enum sf_Medium_Enum
                    {
                        dB_6,
                        dB_4,
                        dB_2,
                        dB0
                    }
                }
            }
        }

        [Serializable]
        public class cellReselectionServingFreqInfo_Type
        {
            public void InitDefaults()
            {
            }

            public long cellReselectionPriority { get; set; }

            public long? s_NonIntraSearch { get; set; }

            public long threshServingLow { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellReselectionServingFreqInfo_Type Decode(BitArrayInputStream input)
                {
                    cellReselectionServingFreqInfo_Type type = new cellReselectionServingFreqInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        type.s_NonIntraSearch = input.readBits(5);
                    }
                    type.threshServingLow = input.readBits(5);
                    type.cellReselectionPriority = input.readBits(3);
                    return type;
                }
            }
        }

        [Serializable]
        public class intraFreqCellReselectionInfo_Type
        {
            public void InitDefaults()
            {
            }

            public AllowedMeasBandwidth? allowedMeasBandwidth { get; set; }

            public string neighCellConfig { get; set; }

            public long? p_Max { get; set; }

            public bool presenceAntennaPort1 { get; set; }

            public long q_RxLevMin { get; set; }

            public long? s_IntraSearch { get; set; }

            public long t_ReselectionEUTRA { get; set; }

            public SpeedStateScaleFactors t_ReselectionEUTRA_SF { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public intraFreqCellReselectionInfo_Type Decode(BitArrayInputStream input)
                {
                    intraFreqCellReselectionInfo_Type type = new intraFreqCellReselectionInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 4);
                    type.q_RxLevMin = input.readBits(6) + -70;
                    if (stream.Read())
                    {
                        type.p_Max = input.readBits(6) + -30;
                    }
                    if (stream.Read())
                    {
                        type.s_IntraSearch = input.readBits(5);
                    }
                    if (stream.Read())
                    {
                        int nBits = 3;
                        type.allowedMeasBandwidth = (AllowedMeasBandwidth)input.readBits(nBits);
                    }
                    type.presenceAntennaPort1 = input.readBit() == 1;
                    type.neighCellConfig = input.readBitString(2);
                    type.t_ReselectionEUTRA = input.readBits(3);
                    if (stream.Read())
                    {
                        type.t_ReselectionEUTRA_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType3 Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                SystemInformationBlockType3 type = new SystemInformationBlockType3();
                type.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 1) : new BitMaskStream(input, 0);
                type.cellReselectionInfoCommon = cellReselectionInfoCommon_Type.PerDecoder.Instance.Decode(input);
                type.cellReselectionServingFreqInfo = cellReselectionServingFreqInfo_Type.PerDecoder.Instance.Decode(input);
                type.intraFreqCellReselectionInfo = intraFreqCellReselectionInfo_Type.PerDecoder.Instance.Decode(input);
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 4);
                    if (stream2.Read())
                    {
                        type.s_IntraSearch_v920 = s_IntraSearch_v920_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.s_NonIntraSearch_v920 = s_NonIntraSearch_v920_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.q_QualMin_r9 = input.readBits(5) + -34;
                    }
                    if (stream2.Read())
                    {
                        type.threshServingLowQ_r9 = input.readBits(5);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        type.q_QualMinWB_r11 = input.readBits(5) + -34;
                    }
                }
                return type;
            }
        }

        [Serializable]
        public class s_IntraSearch_v920_Type
        {
            public void InitDefaults()
            {
            }

            public long s_IntraSearchP_r9 { get; set; }

            public long s_IntraSearchQ_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public s_IntraSearch_v920_Type Decode(BitArrayInputStream input)
                {
                    s_IntraSearch_v920_Type type = new s_IntraSearch_v920_Type();
                    type.InitDefaults();
                    type.s_IntraSearchP_r9 = input.readBits(5);
                    type.s_IntraSearchQ_r9 = input.readBits(5);
                    return type;
                }
            }
        }

        [Serializable]
        public class s_NonIntraSearch_v920_Type
        {
            public void InitDefaults()
            {
            }

            public long s_NonIntraSearchP_r9 { get; set; }

            public long s_NonIntraSearchQ_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public s_NonIntraSearch_v920_Type Decode(BitArrayInputStream input)
                {
                    s_NonIntraSearch_v920_Type type = new s_NonIntraSearch_v920_Type();
                    type.InitDefaults();
                    type.s_NonIntraSearchP_r9 = input.readBits(5);
                    type.s_NonIntraSearchQ_r9 = input.readBits(5);
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType4
    {
        public void InitDefaults()
        {
        }

        public PhysCellIdRange csg_PhysCellIdRange { get; set; }

        public List<PhysCellIdRange> intraFreqBlackCellList { get; set; }

        public List<IntraFreqNeighCellInfo> intraFreqNeighCellList { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType4 Decode(BitArrayInputStream input)
            {
                int num2;
                SystemInformationBlockType4 type = new SystemInformationBlockType4();
                type.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 4) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    type.intraFreqNeighCellList = new List<IntraFreqNeighCellInfo>();
                    num2 = 4;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        IntraFreqNeighCellInfo item = IntraFreqNeighCellInfo.PerDecoder.Instance.Decode(input);
                        type.intraFreqNeighCellList.Add(item);
                    }
                }
                if (stream.Read())
                {
                    type.intraFreqBlackCellList = new List<PhysCellIdRange>();
                    num2 = 4;
                    int num5 = input.readBits(num2) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        PhysCellIdRange range = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                        type.intraFreqBlackCellList.Add(range);
                    }
                }
                if (stream.Read())
                {
                    type.csg_PhysCellIdRange = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                }
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType5
    {
        public void InitDefaults()
        {
        }

        public List<InterFreqCarrierFreqInfo> interFreqCarrierFreqList { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType5 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType5 type = new SystemInformationBlockType5();
                type.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 1) : new BitMaskStream(input, 0);
                type.interFreqCarrierFreqList = new List<InterFreqCarrierFreqInfo>();
                int num2 = 3;
                int num3 = input.readBits(num2) + 1;
                for (int i = 0; i < num3; i++)
                {
                    InterFreqCarrierFreqInfo item = InterFreqCarrierFreqInfo.PerDecoder.Instance.Decode(input);
                    type.interFreqCarrierFreqList.Add(item);
                }
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType5_v8h0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<InterFreqCarrierFreqInfo_v8h0> interFreqCarrierFreqList_v8h0 { get; set; }

        public SystemInformationBlockType5_v9e0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType5_v8h0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType5_v8h0_IEs es = new SystemInformationBlockType5_v8h0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.interFreqCarrierFreqList_v8h0 = new List<InterFreqCarrierFreqInfo_v8h0>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        InterFreqCarrierFreqInfo_v8h0 item = InterFreqCarrierFreqInfo_v8h0.PerDecoder.Instance.Decode(input);
                        es.interFreqCarrierFreqList_v8h0.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType5_v9e0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType5_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<InterFreqCarrierFreqInfo_v9e0> interFreqCarrierFreqList_v9e0 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType5_v9e0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType5_v9e0_IEs es = new SystemInformationBlockType5_v9e0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.interFreqCarrierFreqList_v9e0 = new List<InterFreqCarrierFreqInfo_v9e0>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        InterFreqCarrierFreqInfo_v9e0 item = InterFreqCarrierFreqInfo_v9e0.PerDecoder.Instance.Decode(input);
                        es.interFreqCarrierFreqList_v9e0.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType6
    {
        public void InitDefaults()
        {
        }

        public List<CarrierFreqUTRA_FDD> carrierFreqListUTRA_FDD { get; set; }

        public List<CarrierFreqUTRA_TDD> carrierFreqListUTRA_TDD { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public long t_ReselectionUTRA { get; set; }

        public SpeedStateScaleFactors t_ReselectionUTRA_SF { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType6 Decode(BitArrayInputStream input)
            {
                int num2;
                SystemInformationBlockType6 type = new SystemInformationBlockType6();
                type.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 4) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    type.carrierFreqListUTRA_FDD = new List<CarrierFreqUTRA_FDD>();
                    num2 = 4;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        CarrierFreqUTRA_FDD item = CarrierFreqUTRA_FDD.PerDecoder.Instance.Decode(input);
                        type.carrierFreqListUTRA_FDD.Add(item);
                    }
                }
                if (stream.Read())
                {
                    type.carrierFreqListUTRA_TDD = new List<CarrierFreqUTRA_TDD>();
                    num2 = 4;
                    int num5 = input.readBits(num2) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        CarrierFreqUTRA_TDD qutra_tdd = CarrierFreqUTRA_TDD.PerDecoder.Instance.Decode(input);
                        type.carrierFreqListUTRA_TDD.Add(qutra_tdd);
                    }
                }
                type.t_ReselectionUTRA = input.readBits(3);
                if (stream.Read())
                {
                    type.t_ReselectionUTRA_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType6_v8h0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<CarrierFreqInfoUTRA_FDD_v8h0> carrierFreqListUTRA_FDD_v8h0 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType6_v8h0_IEs Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType6_v8h0_IEs es = new SystemInformationBlockType6_v8h0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.carrierFreqListUTRA_FDD_v8h0 = new List<CarrierFreqInfoUTRA_FDD_v8h0>();
                    int nBits = 4;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        CarrierFreqInfoUTRA_FDD_v8h0 item = CarrierFreqInfoUTRA_FDD_v8h0.PerDecoder.Instance.Decode(input);
                        es.carrierFreqListUTRA_FDD_v8h0.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType7
    {
        public void InitDefaults()
        {
        }

        public List<CarrierFreqsInfoGERAN> carrierFreqsInfoList { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public long t_ReselectionGERAN { get; set; }

        public SpeedStateScaleFactors t_ReselectionGERAN_SF { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType7 Decode(BitArrayInputStream input)
            {
                SystemInformationBlockType7 type = new SystemInformationBlockType7();
                type.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 3) : new BitMaskStream(input, 2);
                type.t_ReselectionGERAN = input.readBits(3);
                if (stream.Read())
                {
                    type.t_ReselectionGERAN_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    type.carrierFreqsInfoList = new List<CarrierFreqsInfoGERAN>();
                    int num2 = 4;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        CarrierFreqsInfoGERAN item = CarrierFreqsInfoGERAN.PerDecoder.Instance.Decode(input);
                        type.carrierFreqsInfoList.Add(item);
                    }
                }
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType8
    {
        public void InitDefaults()
        {
        }

        public AC_BarringConfig1XRTT_r9 ac_BarringConfig1XRTT_r9 { get; set; }

        public CellReselectionParametersCDMA2000_v920 cellReselectionParameters1XRTT_v920 { get; set; }

        public CellReselectionParametersCDMA2000_v920 cellReselectionParametersHRPD_v920 { get; set; }

        public csfb_DualRxTxSupport_r10_Enum? csfb_DualRxTxSupport_r10 { get; set; }

        public CSFB_RegistrationParam1XRTT_v920 csfb_RegistrationParam1XRTT_v920 { get; set; }

        public bool? csfb_SupportForDualRxUEs_r9 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public parameters1XRTT_Type parameters1XRTT { get; set; }

        public parametersHRPD_Type parametersHRPD { get; set; }

        public long? searchWindowSize { get; set; }

        public List<SIB8_PerPLMN_r11> sib8_PerPLMN_List_r11 { get; set; }

        public SystemTimeInfoCDMA2000 systemTimeInfo { get; set; }

        public enum csfb_DualRxTxSupport_r10_Enum
        {
            _true
        }

        [Serializable]
        public class parameters1XRTT_Type
        {
            public void InitDefaults()
            {
            }

            public CellReselectionParametersCDMA2000 cellReselectionParameters1XRTT { get; set; }

            public CSFB_RegistrationParam1XRTT csfb_RegistrationParam1XRTT { get; set; }

            public string longCodeState1XRTT { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parameters1XRTT_Type Decode(BitArrayInputStream input)
                {
                    parameters1XRTT_Type type = new parameters1XRTT_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 3);
                    if (stream.Read())
                    {
                        type.csfb_RegistrationParam1XRTT = CSFB_RegistrationParam1XRTT.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.longCodeState1XRTT = input.readBitString(0x2a);
                    }
                    if (stream.Read())
                    {
                        type.cellReselectionParameters1XRTT = CellReselectionParametersCDMA2000.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class parametersHRPD_Type
        {
            public void InitDefaults()
            {
            }

            public CellReselectionParametersCDMA2000 cellReselectionParametersHRPD { get; set; }

            public PreRegistrationInfoHRPD preRegistrationInfoHRPD { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parametersHRPD_Type Decode(BitArrayInputStream input)
                {
                    parametersHRPD_Type type = new parametersHRPD_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.preRegistrationInfoHRPD = PreRegistrationInfoHRPD.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.cellReselectionParametersHRPD = CellReselectionParametersCDMA2000.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType8 Decode(BitArrayInputStream input)
            {
                int num2;
                BitMaskStream stream2;
                SystemInformationBlockType8 type = new SystemInformationBlockType8();
                type.InitDefaults();
                bool flag = false;
                flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 5) : new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    type.systemTimeInfo = SystemTimeInfoCDMA2000.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    type.searchWindowSize = (long)input.readBits(4);
                }
                if (stream.Read())
                {
                    type.parametersHRPD = parametersHRPD_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    type.parameters1XRTT = parameters1XRTT_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag && stream.Read())
                {
                    int nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 5);
                    if (stream2.Read())
                    {
                        type.csfb_SupportForDualRxUEs_r9 = input.readBit() == 1;
                    }
                    if (stream2.Read())
                    {
                        type.cellReselectionParametersHRPD_v920 = CellReselectionParametersCDMA2000_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.cellReselectionParameters1XRTT_v920 = CellReselectionParametersCDMA2000_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.csfb_RegistrationParam1XRTT_v920 = CSFB_RegistrationParam1XRTT_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.ac_BarringConfig1XRTT_r9 = AC_BarringConfig1XRTT_r9.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        num2 = 1;
                        type.csfb_DualRxTxSupport_r10 = (csfb_DualRxTxSupport_r10_Enum)input.readBits(num2);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (!stream2.Read())
                    {
                        return type;
                    }
                    type.sib8_PerPLMN_List_r11 = new List<SIB8_PerPLMN_r11>();
                    num2 = 3;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        SIB8_PerPLMN_r11 item = SIB8_PerPLMN_r11.PerDecoder.Instance.Decode(input);
                        type.sib8_PerPLMN_List_r11.Add(item);
                    }
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemInformationBlockType9
    {
        public void InitDefaults()
        {
        }

        public string hnb_Name { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType9 Decode(BitArrayInputStream input)
            {
                int nBits;
                SystemInformationBlockType9 type = new SystemInformationBlockType9();
                type.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    nBits = input.readBits(6);
                    type.hnb_Name = input.readOctetString(nBits + 1);
                }
                if (flag && stream.Read())
                {
                    nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SystemTimeInfoCDMA2000
    {
        public void InitDefaults()
        {
        }

        public bool cdma_EUTRA_Synchronisation { get; set; }

        public cdma_SystemTime_Type cdma_SystemTime { get; set; }

        [Serializable]
        public class cdma_SystemTime_Type
        {
            public void InitDefaults()
            {
            }

            public string asynchronousSystemTime { get; set; }

            public string synchronousSystemTime { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cdma_SystemTime_Type Decode(BitArrayInputStream input)
                {
                    cdma_SystemTime_Type type = new cdma_SystemTime_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.synchronousSystemTime = input.readBitString(0x27);
                            return type;

                        case 1:
                            type.asynchronousSystemTime = input.readBitString(0x31);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemTimeInfoCDMA2000 Decode(BitArrayInputStream input)
            {
                SystemTimeInfoCDMA2000 ocdma = new SystemTimeInfoCDMA2000();
                ocdma.InitDefaults();
                ocdma.cdma_EUTRA_Synchronisation = input.readBit() == 1;
                ocdma.cdma_SystemTime = cdma_SystemTime_Type.PerDecoder.Instance.Decode(input);
                return ocdma;
            }
        }
    }

}
