using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MobilityControlInfo
    {
        public void InitDefaults()
        {
        }

        public long? additionalSpectrumEmission { get; set; }

        public CarrierBandwidthEUTRA carrierBandwidth { get; set; }

        public CarrierFreqEUTRA carrierFreq { get; set; }

        public CarrierFreqEUTRA_v9e0 carrierFreq_v9e0 { get; set; }

        public drb_ContinueROHC_r11_Enum? drb_ContinueROHC_r11 { get; set; }

        public string newUE_Identity { get; set; }

        public RACH_ConfigDedicated rach_ConfigDedicated { get; set; }

        public RadioResourceConfigCommon radioResourceConfigCommon { get; set; }

        public t304_Enum t304 { get; set; }

        public long targetPhysCellId { get; set; }

        public enum drb_ContinueROHC_r11_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityControlInfo Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                MobilityControlInfo info = new MobilityControlInfo();
                info.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 4);
                info.targetPhysCellId = input.readBits(9);
                if (stream.Read())
                {
                    info.carrierFreq = CarrierFreqEUTRA.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    info.carrierBandwidth = CarrierBandwidthEUTRA.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    info.additionalSpectrumEmission = input.readBits(5) + 1;
                }
                int nBits = 3;
                info.t304 = (t304_Enum)input.readBits(nBits);
                info.newUE_Identity = input.readBitString(0x10);
                info.radioResourceConfigCommon = RadioResourceConfigCommon.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    info.rach_ConfigDedicated = RACH_ConfigDedicated.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        info.carrierFreq_v9e0 = CarrierFreqEUTRA_v9e0.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        nBits = 1;
                        info.drb_ContinueROHC_r11 = (drb_ContinueROHC_r11_Enum)input.readBits(nBits);
                    }
                }
                return info;
            }
        }

        public enum t304_Enum
        {
            ms50,
            ms100,
            ms150,
            ms200,
            ms500,
            ms1000,
            ms2000,
            spare1
        }
    }

    [Serializable]
    public class IdleModeMobilityControlInfo
    {
        public void InitDefaults()
        {
        }

        public List<BandClassPriority1XRTT> bandClassPriorityList1XRTT { get; set; }

        public List<BandClassPriorityHRPD> bandClassPriorityListHRPD { get; set; }

        public List<FreqPriorityEUTRA> freqPriorityListEUTRA { get; set; }

        public List<FreqsPriorityGERAN> freqPriorityListGERAN { get; set; }

        public List<FreqPriorityUTRA_FDD> freqPriorityListUTRA_FDD { get; set; }

        public List<FreqPriorityUTRA_TDD> freqPriorityListUTRA_TDD { get; set; }

        public t320_Enum? t320 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IdleModeMobilityControlInfo Decode(BitArrayInputStream input)
            {
                int num2;
                IdleModeMobilityControlInfo info = new IdleModeMobilityControlInfo();
                info.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 7) : new BitMaskStream(input, 7);
                if (stream.Read())
                {
                    info.freqPriorityListEUTRA = new List<FreqPriorityEUTRA>();
                    num2 = 3;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        FreqPriorityEUTRA item = FreqPriorityEUTRA.PerDecoder.Instance.Decode(input);
                        info.freqPriorityListEUTRA.Add(item);
                    }
                }
                if (stream.Read())
                {
                    info.freqPriorityListGERAN = new List<FreqsPriorityGERAN>();
                    num2 = 4;
                    int num5 = input.readBits(num2) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        FreqsPriorityGERAN ygeran = FreqsPriorityGERAN.PerDecoder.Instance.Decode(input);
                        info.freqPriorityListGERAN.Add(ygeran);
                    }
                }
                if (stream.Read())
                {
                    info.freqPriorityListUTRA_FDD = new List<FreqPriorityUTRA_FDD>();
                    num2 = 4;
                    int num7 = input.readBits(num2) + 1;
                    for (int k = 0; k < num7; k++)
                    {
                        FreqPriorityUTRA_FDD yutra_fdd = FreqPriorityUTRA_FDD.PerDecoder.Instance.Decode(input);
                        info.freqPriorityListUTRA_FDD.Add(yutra_fdd);
                    }
                }
                if (stream.Read())
                {
                    info.freqPriorityListUTRA_TDD = new List<FreqPriorityUTRA_TDD>();
                    num2 = 4;
                    int num9 = input.readBits(num2) + 1;
                    for (int m = 0; m < num9; m++)
                    {
                        FreqPriorityUTRA_TDD yutra_tdd = FreqPriorityUTRA_TDD.PerDecoder.Instance.Decode(input);
                        info.freqPriorityListUTRA_TDD.Add(yutra_tdd);
                    }
                }
                if (stream.Read())
                {
                    info.bandClassPriorityListHRPD = new List<BandClassPriorityHRPD>();
                    num2 = 5;
                    int num11 = input.readBits(num2) + 1;
                    for (int n = 0; n < num11; n++)
                    {
                        BandClassPriorityHRPD yhrpd = BandClassPriorityHRPD.PerDecoder.Instance.Decode(input);
                        info.bandClassPriorityListHRPD.Add(yhrpd);
                    }
                }
                if (stream.Read())
                {
                    info.bandClassPriorityList1XRTT = new List<BandClassPriority1XRTT>();
                    num2 = 5;
                    int num13 = input.readBits(num2) + 1;
                    for (int num14 = 0; num14 < num13; num14++)
                    {
                        BandClassPriority1XRTT priorityxrtt = BandClassPriority1XRTT.PerDecoder.Instance.Decode(input);
                        info.bandClassPriorityList1XRTT.Add(priorityxrtt);
                    }
                }
                if (stream.Read())
                {
                    num2 = 3;
                    info.t320 = (t320_Enum)input.readBits(num2);
                }
                return info;
            }
        }

        public enum t320_Enum
        {
            min5,
            min10,
            min20,
            min30,
            min60,
            min120,
            min180,
            spare1
        }
    }

    [Serializable]
    public class IdleModeMobilityControlInfo_v9e0
    {
        public void InitDefaults()
        {
        }

        public List<FreqPriorityEUTRA_v9e0> freqPriorityListEUTRA_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IdleModeMobilityControlInfo_v9e0 Decode(BitArrayInputStream input)
            {
                IdleModeMobilityControlInfo_v9e0 _ve = new IdleModeMobilityControlInfo_v9e0();
                _ve.InitDefaults();
                _ve.freqPriorityListEUTRA_v9e0 = new List<FreqPriorityEUTRA_v9e0>();
                int nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    FreqPriorityEUTRA_v9e0 item = FreqPriorityEUTRA_v9e0.PerDecoder.Instance.Decode(input);
                    _ve.freqPriorityListEUTRA_v9e0.Add(item);
                }
                return _ve;
            }
        }
    }

    [Serializable]
    public class MobilityFromEUTRACommand
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

                public MobilityFromEUTRACommand_r8_IEs mobilityFromEUTRACommand_r8 { get; set; }

                public MobilityFromEUTRACommand_r9_IEs mobilityFromEUTRACommand_r9 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

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
                                type.mobilityFromEUTRACommand_r8 = MobilityFromEUTRACommand_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                type.mobilityFromEUTRACommand_r9 = MobilityFromEUTRACommand_r9_IEs.PerDecoder.Instance.Decode(input);
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

            public MobilityFromEUTRACommand Decode(BitArrayInputStream input)
            {
                MobilityFromEUTRACommand command = new MobilityFromEUTRACommand();
                command.InitDefaults();
                command.rrc_TransactionIdentifier = input.readBits(2);
                command.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return command;
            }
        }
    }

    [Serializable]
    public class MobilityFromEUTRACommand_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public bool cs_FallbackIndicator { get; set; }

        public MobilityFromEUTRACommand_v8a0_IEs nonCriticalExtension { get; set; }

        public purpose_Type purpose { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityFromEUTRACommand_r8_IEs Decode(BitArrayInputStream input)
            {
                MobilityFromEUTRACommand_r8_IEs es = new MobilityFromEUTRACommand_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.cs_FallbackIndicator = input.readBit() == 1;
                es.purpose = purpose_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.nonCriticalExtension = MobilityFromEUTRACommand_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        [Serializable]
        public class purpose_Type
        {
            public void InitDefaults()
            {
            }

            public CellChangeOrder cellChangeOrder { get; set; }

            public Handover handover { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public purpose_Type Decode(BitArrayInputStream input)
                {
                    purpose_Type type = new purpose_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.handover = Handover.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.cellChangeOrder = CellChangeOrder.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

    [Serializable]
    public class MobilityFromEUTRACommand_r9_IEs
    {
        public void InitDefaults()
        {
        }

        public bool cs_FallbackIndicator { get; set; }

        public MobilityFromEUTRACommand_v930_IEs nonCriticalExtension { get; set; }

        public purpose_Type purpose { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityFromEUTRACommand_r9_IEs Decode(BitArrayInputStream input)
            {
                MobilityFromEUTRACommand_r9_IEs es = new MobilityFromEUTRACommand_r9_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.cs_FallbackIndicator = input.readBit() == 1;
                es.purpose = purpose_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.nonCriticalExtension = MobilityFromEUTRACommand_v930_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        [Serializable]
        public class purpose_Type
        {
            public void InitDefaults()
            {
            }

            public CellChangeOrder cellChangeOrder { get; set; }

            public E_CSFB_r9 e_CSFB_r9 { get; set; }

            public Handover handover { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public purpose_Type Decode(BitArrayInputStream input)
                {
                    purpose_Type type = new purpose_Type();
                    type.InitDefaults();
                    input.readBit();
                    switch (input.readBits(2))
                    {
                        case 0:
                            type.handover = Handover.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.cellChangeOrder = CellChangeOrder.PerDecoder.Instance.Decode(input);
                            return type;

                        case 2:
                            type.e_CSFB_r9 = E_CSFB_r9.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

    [Serializable]
    public class MobilityFromEUTRACommand_v8a0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public MobilityFromEUTRACommand_v8d0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityFromEUTRACommand_v8a0_IEs Decode(BitArrayInputStream input)
            {
                MobilityFromEUTRACommand_v8a0_IEs es = new MobilityFromEUTRACommand_v8a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = MobilityFromEUTRACommand_v8d0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class MobilityFromEUTRACommand_v8d0_IEs
    {
        public void InitDefaults()
        {
        }

        public BandIndicatorGERAN? bandIndicator { get; set; }

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

            public MobilityFromEUTRACommand_v8d0_IEs Decode(BitArrayInputStream input)
            {
                MobilityFromEUTRACommand_v8d0_IEs es = new MobilityFromEUTRACommand_v8d0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.bandIndicator = (BandIndicatorGERAN)input.readBits(nBits);
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
    public class MobilityFromEUTRACommand_v930_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public MobilityFromEUTRACommand_v960_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityFromEUTRACommand_v930_IEs Decode(BitArrayInputStream input)
            {
                MobilityFromEUTRACommand_v930_IEs es = new MobilityFromEUTRACommand_v930_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = MobilityFromEUTRACommand_v960_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class MobilityFromEUTRACommand_v960_IEs
    {
        public void InitDefaults()
        {
        }

        public BandIndicatorGERAN? bandIndicator { get; set; }

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

            public MobilityFromEUTRACommand_v960_IEs Decode(BitArrayInputStream input)
            {
                MobilityFromEUTRACommand_v960_IEs es = new MobilityFromEUTRACommand_v960_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.bandIndicator = (BandIndicatorGERAN)input.readBits(nBits);
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
    public class MobilityStateParameters
    {
        public void InitDefaults()
        {
        }

        public long n_CellChangeHigh { get; set; }

        public long n_CellChangeMedium { get; set; }

        public t_Evaluation_Enum t_Evaluation { get; set; }

        public t_HystNormal_Enum t_HystNormal { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityStateParameters Decode(BitArrayInputStream input)
            {
                MobilityStateParameters parameters = new MobilityStateParameters();
                parameters.InitDefaults();
                int nBits = 3;
                parameters.t_Evaluation = (t_Evaluation_Enum)input.readBits(nBits);
                nBits = 3;
                parameters.t_HystNormal = (t_HystNormal_Enum)input.readBits(nBits);
                parameters.n_CellChangeMedium = input.readBits(4) + 1;
                parameters.n_CellChangeHigh = input.readBits(4) + 1;
                return parameters;
            }
        }

        public enum t_Evaluation_Enum
        {
            s30,
            s60,
            s120,
            s180,
            s240,
            spare3,
            spare2,
            spare1
        }

        public enum t_HystNormal_Enum
        {
            s30,
            s60,
            s120,
            s180,
            s240,
            spare3,
            spare2,
            spare1
        }
    }

}
