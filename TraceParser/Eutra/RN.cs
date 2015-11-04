using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RN_SubframeConfig_r10
    {
        public void InitDefaults()
        {
        }

        public rpdcch_Config_r10_Type rpdcch_Config_r10 { get; set; }

        public subframeConfigPattern_r10_Type subframeConfigPattern_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RN_SubframeConfig_r10 Decode(BitArrayInputStream input)
            {
                RN_SubframeConfig_r10 _r = new RN_SubframeConfig_r10();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.subframeConfigPattern_r10 = subframeConfigPattern_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.rpdcch_Config_r10 = rpdcch_Config_r10_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }

        [Serializable]
        public class rpdcch_Config_r10_Type
        {
            public void InitDefaults()
            {
            }

            public demodulationRS_r10_Type demodulationRS_r10 { get; set; }

            public long pdsch_Start_r10 { get; set; }

            public pucch_Config_r10_Type pucch_Config_r10 { get; set; }

            public resourceAllocationType_r10_Enum resourceAllocationType_r10 { get; set; }

            public resourceBlockAssignment_r10_Type resourceBlockAssignment_r10 { get; set; }

            [Serializable]
            public class demodulationRS_r10_Type
            {
                public void InitDefaults()
                {
                }

                public interleaving_r10_Enum interleaving_r10 { get; set; }

                public noInterleaving_r10_Enum noInterleaving_r10 { get; set; }

                public enum interleaving_r10_Enum
                {
                    crs
                }

                public enum noInterleaving_r10_Enum
                {
                    crs,
                    dmrs
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public demodulationRS_r10_Type Decode(BitArrayInputStream input)
                    {
                        int num2;
                        demodulationRS_r10_Type type = new demodulationRS_r10_Type();
                        type.InitDefaults();
                        switch (input.readBits(1))
                        {
                            case 0:
                                num2 = 1;
                                type.interleaving_r10 = (interleaving_r10_Enum)input.readBits(num2);
                                return type;

                            case 1:
                                num2 = 1;
                                type.noInterleaving_r10 = (noInterleaving_r10_Enum)input.readBits(num2);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public rpdcch_Config_r10_Type Decode(BitArrayInputStream input)
                {
                    rpdcch_Config_r10_Type type = new rpdcch_Config_r10_Type();
                    type.InitDefaults();
                    input.readBit();
                    const int nBits = 3;
                    type.resourceAllocationType_r10 = (resourceAllocationType_r10_Enum)input.readBits(nBits);
                    type.resourceBlockAssignment_r10 = resourceBlockAssignment_r10_Type.PerDecoder.Instance.Decode(input);
                    type.demodulationRS_r10 = demodulationRS_r10_Type.PerDecoder.Instance.Decode(input);
                    type.pdsch_Start_r10 = input.readBits(2) + 1;
                    type.pucch_Config_r10 = pucch_Config_r10_Type.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }

            [Serializable]
            public class pucch_Config_r10_Type
            {
                public void InitDefaults()
                {
                }

                public fdd_Type fdd { get; set; }

                public tdd_Type tdd { get; set; }

                [Serializable]
                public class fdd_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public long n1PUCCH_AN_P0_r10 { get; set; }

                    public long? n1PUCCH_AN_P1_r10 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public fdd_Type Decode(BitArrayInputStream input)
                        {
                            fdd_Type type = new fdd_Type();
                            type.InitDefaults();
                            BitMaskStream stream = new BitMaskStream(input, 1);
                            type.n1PUCCH_AN_P0_r10 = input.readBits(11);
                            if (stream.Read())
                            {
                                type.n1PUCCH_AN_P1_r10 = input.readBits(11);
                            }
                            return type;
                        }
                    }
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public pucch_Config_r10_Type Decode(BitArrayInputStream input)
                    {
                        pucch_Config_r10_Type type = new pucch_Config_r10_Type();
                        type.InitDefaults();
                        switch (input.readBits(1))
                        {
                            case 0:
                                type.tdd = tdd_Type.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                type.fdd = fdd_Type.PerDecoder.Instance.Decode(input);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class tdd_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public channelSelectionMultiplexingBundling_Type channelSelectionMultiplexingBundling { get; set; }

                    public fallbackForFormat3_Type fallbackForFormat3 { get; set; }

                    [Serializable]
                    public class channelSelectionMultiplexingBundling_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public List<long> n1PUCCH_AN_List_r10 { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public channelSelectionMultiplexingBundling_Type Decode(BitArrayInputStream input)
                            {
                                channelSelectionMultiplexingBundling_Type type = new channelSelectionMultiplexingBundling_Type();
                                type.InitDefaults();
                                type.n1PUCCH_AN_List_r10 = new List<long>();
                                int nBits = 2;
                                int num3 = input.readBits(nBits) + 1;
                                for (int i = 0; i < num3; i++)
                                {
                                    long item = input.readBits(11);
                                    type.n1PUCCH_AN_List_r10.Add(item);
                                }
                                return type;
                            }
                        }
                    }

                    [Serializable]
                    public class fallbackForFormat3_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public long n1PUCCH_AN_P0_r10 { get; set; }

                        public long? n1PUCCH_AN_P1_r10 { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public fallbackForFormat3_Type Decode(BitArrayInputStream input)
                            {
                                fallbackForFormat3_Type type = new fallbackForFormat3_Type();
                                type.InitDefaults();
                                BitMaskStream stream = new BitMaskStream(input, 1);
                                type.n1PUCCH_AN_P0_r10 = input.readBits(11);
                                if (stream.Read())
                                {
                                    type.n1PUCCH_AN_P1_r10 = input.readBits(11);
                                }
                                return type;
                            }
                        }
                    }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public tdd_Type Decode(BitArrayInputStream input)
                        {
                            tdd_Type type = new tdd_Type();
                            type.InitDefaults();
                            switch (input.readBits(1))
                            {
                                case 0:
                                    type.channelSelectionMultiplexingBundling = channelSelectionMultiplexingBundling_Type.PerDecoder.Instance.Decode(input);
                                    return type;

                                case 1:
                                    type.fallbackForFormat3 = fallbackForFormat3_Type.PerDecoder.Instance.Decode(input);
                                    return type;
                            }
                            throw new Exception(GetType().Name + ":NoChoice had been choose");
                        }
                    }
                }
            }

            public enum resourceAllocationType_r10_Enum
            {
                type0,
                type1,
                type2Localized,
                type2Distributed,
                spare4,
                spare3,
                spare2,
                spare1
            }

            [Serializable]
            public class resourceBlockAssignment_r10_Type
            {
                public void InitDefaults()
                {
                }

                public type01_r10_Type type01_r10 { get; set; }

                public type2_r10_Type type2_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public resourceBlockAssignment_r10_Type Decode(BitArrayInputStream input)
                    {
                        resourceBlockAssignment_r10_Type type = new resourceBlockAssignment_r10_Type();
                        type.InitDefaults();
                        input.readBit();
                        switch (input.readBits(1))
                        {
                            case 0:
                                type.type01_r10 = type01_r10_Type.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                type.type2_r10 = type2_r10_Type.PerDecoder.Instance.Decode(input);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class type01_r10_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public string nrb100_r10 { get; set; }

                    public string nrb15_r10 { get; set; }

                    public string nrb25_r10 { get; set; }

                    public string nrb50_r10 { get; set; }

                    public string nrb6_r10 { get; set; }

                    public string nrb75_r10 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public type01_r10_Type Decode(BitArrayInputStream input)
                        {
                            type01_r10_Type type = new type01_r10_Type();
                            type.InitDefaults();
                            switch (input.readBits(3))
                            {
                                case 0:
                                    type.nrb6_r10 = input.readBitString(6);
                                    return type;

                                case 1:
                                    type.nrb15_r10 = input.readBitString(8);
                                    return type;

                                case 2:
                                    type.nrb25_r10 = input.readBitString(13);
                                    return type;

                                case 3:
                                    type.nrb50_r10 = input.readBitString(0x11);
                                    return type;

                                case 4:
                                    type.nrb75_r10 = input.readBitString(0x13);
                                    return type;

                                case 5:
                                    type.nrb100_r10 = input.readBitString(0x19);
                                    return type;
                            }
                            throw new Exception(GetType().Name + ":NoChoice had been choose");
                        }
                    }
                }

                [Serializable]
                public class type2_r10_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public string nrb100_r10 { get; set; }

                    public string nrb15_r10 { get; set; }

                    public string nrb25_r10 { get; set; }

                    public string nrb50_r10 { get; set; }

                    public string nrb6_r10 { get; set; }

                    public string nrb75_r10 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public type2_r10_Type Decode(BitArrayInputStream input)
                        {
                            type2_r10_Type type = new type2_r10_Type();
                            type.InitDefaults();
                            switch (input.readBits(3))
                            {
                                case 0:
                                    type.nrb6_r10 = input.readBitString(5);
                                    return type;

                                case 1:
                                    type.nrb15_r10 = input.readBitString(7);
                                    return type;

                                case 2:
                                    type.nrb25_r10 = input.readBitString(9);
                                    return type;

                                case 3:
                                    type.nrb50_r10 = input.readBitString(11);
                                    return type;

                                case 4:
                                    type.nrb75_r10 = input.readBitString(12);
                                    return type;

                                case 5:
                                    type.nrb100_r10 = input.readBitString(13);
                                    return type;
                            }
                            throw new Exception(GetType().Name + ":NoChoice had been choose");
                        }
                    }
                }
            }
        }

        [Serializable]
        public class subframeConfigPattern_r10_Type
        {
            public void InitDefaults()
            {
            }

            public string subframeConfigPatternFDD_r10 { get; set; }

            public long subframeConfigPatternTDD_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public subframeConfigPattern_r10_Type Decode(BitArrayInputStream input)
                {
                    subframeConfigPattern_r10_Type type = new subframeConfigPattern_r10_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.subframeConfigPatternFDD_r10 = input.readBitString(8);
                            return type;

                        case 1:
                            type.subframeConfigPatternTDD_r10 = input.readBits(5);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

    [Serializable]
    public class RN_SystemInfo_r10
    {
        public void InitDefaults()
        {
        }

        public string systemInformationBlockType1_r10 { get; set; }

        public SystemInformationBlockType2 systemInformationBlockType2_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RN_SystemInfo_r10 Decode(BitArrayInputStream input)
            {
                RN_SystemInfo_r10 _r = new RN_SystemInfo_r10();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    _r.systemInformationBlockType1_r10 = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    _r.systemInformationBlockType2_r10 = SystemInformationBlockType2.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class RNReconfiguration_r10
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        public long rrc_TransactionIdentifier { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public RNReconfiguration_r10_IEs rnReconfiguration_r10 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(2))
                        {
                            case 0:
                                type.rnReconfiguration_r10 = RNReconfiguration_r10_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

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
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
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

            public RNReconfiguration_r10 Decode(BitArrayInputStream input)
            {
                RNReconfiguration_r10 _r = new RNReconfiguration_r10();
                _r.InitDefaults();
                _r.rrc_TransactionIdentifier = input.readBits(2);
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class RNReconfiguration_r10_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public RN_SubframeConfig_r10 rn_SubframeConfig_r10 { get; set; }

        public RN_SystemInfo_r10 rn_SystemInfo_r10 { get; set; }

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

            public RNReconfiguration_r10_IEs Decode(BitArrayInputStream input)
            {
                RNReconfiguration_r10_IEs es = new RNReconfiguration_r10_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    es.rn_SystemInfo_r10 = RN_SystemInfo_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.rn_SubframeConfig_r10 = RN_SubframeConfig_r10.PerDecoder.Instance.Decode(input);
                }
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
    public class RNReconfigurationComplete_r10
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        public long rrc_TransactionIdentifier { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public RNReconfigurationComplete_r10_IEs rnReconfigurationComplete_r10 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(2))
                        {
                            case 0:
                                type.rnReconfigurationComplete_r10 = RNReconfigurationComplete_r10_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

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
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
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

            public RNReconfigurationComplete_r10 Decode(BitArrayInputStream input)
            {
                RNReconfigurationComplete_r10 _r = new RNReconfigurationComplete_r10();
                _r.InitDefaults();
                _r.rrc_TransactionIdentifier = input.readBits(2);
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class RNReconfigurationComplete_r10_IEs
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

            public RNReconfigurationComplete_r10_IEs Decode(BitArrayInputStream input)
            {
                RNReconfigurationComplete_r10_IEs es = new RNReconfigurationComplete_r10_IEs();
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

}
