using System;
using System.Collections.Generic;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MBSFN_AreaInfo_r9
    {
        public void InitDefaults()
        {
        }

        public long mbsfn_AreaId_r9 { get; set; }

        public mcch_Config_r9_Type mcch_Config_r9 { get; set; }

        public non_MBSFNregionLength_Enum non_MBSFNregionLength { get; set; }

        public long notificationIndicator_r9 { get; set; }

        [Serializable]
        public class mcch_Config_r9_Type
        {
            public void InitDefaults()
            {
            }

            public mcch_ModificationPeriod_r9_Enum mcch_ModificationPeriod_r9 { get; set; }

            public long mcch_Offset_r9 { get; set; }

            public mcch_RepetitionPeriod_r9_Enum mcch_RepetitionPeriod_r9 { get; set; }

            public string sf_AllocInfo_r9 { get; set; }

            public signallingMCS_r9_Enum signallingMCS_r9 { get; set; }

            public enum mcch_ModificationPeriod_r9_Enum
            {
                rf512,
                rf1024
            }

            public enum mcch_RepetitionPeriod_r9_Enum
            {
                rf32,
                rf64,
                rf128,
                rf256
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public mcch_Config_r9_Type Decode(BitArrayInputStream input)
                {
                    mcch_Config_r9_Type type = new mcch_Config_r9_Type();
                    type.InitDefaults();
                    int nBits = 2;
                    type.mcch_RepetitionPeriod_r9 = (mcch_RepetitionPeriod_r9_Enum)input.readBits(nBits);
                    type.mcch_Offset_r9 = input.readBits(4);
                    nBits = 1;
                    type.mcch_ModificationPeriod_r9 = (mcch_ModificationPeriod_r9_Enum)input.readBits(nBits);
                    type.sf_AllocInfo_r9 = input.readBitString(6);
                    nBits = 2;
                    type.signallingMCS_r9 = (signallingMCS_r9_Enum)input.readBits(nBits);
                    return type;
                }
            }

            public enum signallingMCS_r9_Enum
            {
                n2,
                n7,
                n13,
                n19
            }
        }

        public enum non_MBSFNregionLength_Enum
        {
            s1,
            s2
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBSFN_AreaInfo_r9 Decode(BitArrayInputStream input)
            {
                MBSFN_AreaInfo_r9 _r = new MBSFN_AreaInfo_r9();
                _r.InitDefaults();
                input.readBit();
                _r.mbsfn_AreaId_r9 = input.readBits(8);
                const int nBits = 1;
                _r.non_MBSFNregionLength = (non_MBSFNregionLength_Enum)input.readBits(nBits);
                _r.notificationIndicator_r9 = input.readBits(3);
                _r.mcch_Config_r9 = mcch_Config_r9_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class MBSFN_SubframeConfig
    {
        public void InitDefaults()
        {
        }

        public long radioframeAllocationOffset { get; set; }

        public radioframeAllocationPeriod_Enum radioframeAllocationPeriod { get; set; }

        public subframeAllocation_Type subframeAllocation { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBSFN_SubframeConfig Decode(BitArrayInputStream input)
            {
                MBSFN_SubframeConfig config = new MBSFN_SubframeConfig();
                config.InitDefaults();
                int nBits = 3;
                config.radioframeAllocationPeriod = (radioframeAllocationPeriod_Enum)input.readBits(nBits);
                config.radioframeAllocationOffset = input.readBits(3);
                config.subframeAllocation = subframeAllocation_Type.PerDecoder.Instance.Decode(input);
                return config;
            }
        }

        public enum radioframeAllocationPeriod_Enum
        {
            n1,
            n2,
            n4,
            n8,
            n16,
            n32
        }

        [Serializable]
        public class subframeAllocation_Type
        {
            public void InitDefaults()
            {
            }

            public string fourFrames { get; set; }

            public string oneFrame { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public subframeAllocation_Type Decode(BitArrayInputStream input)
                {
                    subframeAllocation_Type type = new subframeAllocation_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.oneFrame = input.readBitString(6);
                            return type;

                        case 1:
                            type.fourFrames = input.readBitString(0x18);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

    [Serializable]
    public class MBSFNAreaConfiguration_r9
    {
        public void InitDefaults()
        {
        }

        public List<MBSFN_SubframeConfig> commonSF_Alloc_r9 { get; set; }

        public commonSF_AllocPeriod_r9_Enum commonSF_AllocPeriod_r9 { get; set; }

        public MBSFNAreaConfiguration_v930_IEs nonCriticalExtension { get; set; }

        public List<PMCH_Info_r9> pmch_InfoList_r9 { get; set; }

        public enum commonSF_AllocPeriod_r9_Enum
        {
            rf4,
            rf8,
            rf16,
            rf32,
            rf64,
            rf128,
            rf256
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBSFNAreaConfiguration_r9 Decode(BitArrayInputStream input)
            {
                MBSFNAreaConfiguration_r9 _r = new MBSFNAreaConfiguration_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                _r.commonSF_Alloc_r9 = new List<MBSFN_SubframeConfig>();
                int nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    MBSFN_SubframeConfig item = MBSFN_SubframeConfig.PerDecoder.Instance.Decode(input);
                    _r.commonSF_Alloc_r9.Add(item);
                }
                nBits = 3;
                _r.commonSF_AllocPeriod_r9 = (commonSF_AllocPeriod_r9_Enum)input.readBits(nBits);
                _r.pmch_InfoList_r9 = new List<PMCH_Info_r9>();
                nBits = 4;
                int num5 = input.readBits(nBits);
                for (int j = 0; j < num5; j++)
                {
                    PMCH_Info_r9 _r2 = PMCH_Info_r9.PerDecoder.Instance.Decode(input);
                    _r.pmch_InfoList_r9.Add(_r2);
                }
                if (stream.Read())
                {
                    _r.nonCriticalExtension = MBSFNAreaConfiguration_v930_IEs.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class MBSFNAreaConfiguration_v930_IEs
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

            public MBSFNAreaConfiguration_v930_IEs Decode(BitArrayInputStream input)
            {
                MBSFNAreaConfiguration_v930_IEs es = new MBSFNAreaConfiguration_v930_IEs();
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
    public class MCCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public MCCH_MessageType message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MCCH_Message Decode(BitArrayInputStream input)
            {
                MCCH_Message message = new MCCH_Message();
                message.InitDefaults();
                message.message = MCCH_MessageType.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }

    [Serializable]
    public class MCCH_MessageType
    {
        public void InitDefaults()
        {
        }

        public c1_Type c1 { get; set; }

        public later_Type later { get; set; }

        [Serializable]
        public class c1_Type
        {
            public void InitDefaults()
            {
            }

            public MBSFNAreaConfiguration_r9 mbsfnAreaConfiguration_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public c1_Type Decode(BitArrayInputStream input)
                {
                    c1_Type type = new c1_Type();
                    type.InitDefaults();
                    if (input.readBits(1) != 0)
                    {
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                    type.mbsfnAreaConfiguration_r9 = MBSFNAreaConfiguration_r9.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }

        [Serializable]
        public class later_Type
        {
            public void InitDefaults()
            {
            }

            public c2_Type c2 { get; set; }

            public messageClassExtension_Type messageClassExtension { get; set; }

            [Serializable]
            public class c2_Type
            {
                public void InitDefaults()
                {
                }

                public MBMSCountingRequest_r10 mbmsCountingRequest_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c2_Type Decode(BitArrayInputStream input)
                    {
                        c2_Type type = new c2_Type();
                        type.InitDefaults();
                        if (input.readBits(1) != 0)
                        {
                            throw new Exception(GetType().Name + ":NoChoice had been choose");
                        }
                        type.mbmsCountingRequest_r10 = MBMSCountingRequest_r10.PerDecoder.Instance.Decode(input);
                        return type;
                    }
                }
            }

            [Serializable]
            public class messageClassExtension_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public messageClassExtension_Type Decode(BitArrayInputStream input)
                    {
                        messageClassExtension_Type type = new messageClassExtension_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public later_Type Decode(BitArrayInputStream input)
                {
                    later_Type type = new later_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.c2 = c2_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.messageClassExtension = messageClassExtension_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MCCH_MessageType Decode(BitArrayInputStream input)
            {
                MCCH_MessageType type = new MCCH_MessageType();
                type.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                        return type;

                    case 1:
                        type.later = later_Type.PerDecoder.Instance.Decode(input);
                        return type;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
