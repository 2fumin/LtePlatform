using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
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
                    var type = new cellAccessRelatedInfo_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 1);
                    type.plmn_IdentityList = new List<PLMN_IdentityInfo>();
                    var nBits = 3;
                    var num3 = input.readBits(nBits) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = PLMN_IdentityInfo.PerDecoder.Instance.Decode(input);
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
                    var type = new cellSelectionInfo_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 1);
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
                var type = new SystemInformationBlockType1();
                type.InitDefaults();
                var stream = new BitMaskStream(input, 3);
                type.cellAccessRelatedInfo = cellAccessRelatedInfo_Type.PerDecoder.Instance.Decode(input);
                type.cellSelectionInfo = cellSelectionInfo_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    type.p_Max = input.readBits(6) + -30;
                }
                type.freqBandIndicator = input.readBits(6) + 1;
                type.schedulingInfoList = new List<SchedulingInfo>();
                var nBits = 5;
                var num3 = input.readBits(nBits) + 1;
                for (var i = 0; i < num3; i++)
                {
                    var item = SchedulingInfo.PerDecoder.Instance.Decode(input);
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
}