using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class InDeviceCoexIndication_r11
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

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public InDeviceCoexIndication_r11_IEs inDeviceCoexIndication_r11 { get; set; }

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
                                type.inDeviceCoexIndication_r11 = InDeviceCoexIndication_r11_IEs.PerDecoder.Instance.Decode(input);
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

            public InDeviceCoexIndication_r11 Decode(BitArrayInputStream input)
            {
                InDeviceCoexIndication_r11 _r = new InDeviceCoexIndication_r11();
                _r.InitDefaults();
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class InDeviceCoexIndication_r11_IEs
    {
        public void InitDefaults()
        {
        }

        public List<AffectedCarrierFreq_r11> affectedCarrierFreqList_r11 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public TDM_AssistanceInfo_r11 tdm_AssistanceInfo_r11 { get; set; }

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

            public InDeviceCoexIndication_r11_IEs Decode(BitArrayInputStream input)
            {
                InDeviceCoexIndication_r11_IEs es = new InDeviceCoexIndication_r11_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    es.affectedCarrierFreqList_r11 = new List<AffectedCarrierFreq_r11>();
                    int num2 = 5;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        AffectedCarrierFreq_r11 item = AffectedCarrierFreq_r11.PerDecoder.Instance.Decode(input);
                        es.affectedCarrierFreqList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.tdm_AssistanceInfo_r11 = TDM_AssistanceInfo_r11.PerDecoder.Instance.Decode(input);
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
    public class TDM_AssistanceInfo_r11
    {
        public void InitDefaults()
        {
        }

        public drx_AssistanceInfo_r11_Type drx_AssistanceInfo_r11 { get; set; }

        public List<IDC_SubframePattern_r11> idc_SubframePatternList_r11 { get; set; }

        [Serializable]
        public class drx_AssistanceInfo_r11_Type
        {
            public void InitDefaults()
            {
            }

            public drx_ActiveTime_r11_Enum drx_ActiveTime_r11 { get; set; }

            public drx_CycleLength_r11_Enum drx_CycleLength_r11 { get; set; }

            public long? drx_Offset_r11 { get; set; }

            public enum drx_ActiveTime_r11_Enum
            {
                sf20,
                sf30,
                sf40,
                sf60,
                sf80,
                sf100,
                spare2,
                spare1
            }

            public enum drx_CycleLength_r11_Enum
            {
                sf40,
                sf64,
                sf80,
                sf128,
                sf160,
                sf256,
                spare2,
                spare1
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public drx_AssistanceInfo_r11_Type Decode(BitArrayInputStream input)
                {
                    drx_AssistanceInfo_r11_Type type = new drx_AssistanceInfo_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    int nBits = 3;
                    type.drx_CycleLength_r11 = (drx_CycleLength_r11_Enum)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.drx_Offset_r11 = input.readBits(8);
                    }
                    nBits = 3;
                    type.drx_ActiveTime_r11 = (drx_ActiveTime_r11_Enum)input.readBits(nBits);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TDM_AssistanceInfo_r11 Decode(BitArrayInputStream input)
            {
                TDM_AssistanceInfo_r11 _r = new TDM_AssistanceInfo_r11();
                _r.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        _r.drx_AssistanceInfo_r11 = drx_AssistanceInfo_r11_Type.PerDecoder.Instance.Decode(input);
                        return _r;

                    case 1:
                        {
                            _r.idc_SubframePatternList_r11 = new List<IDC_SubframePattern_r11>();
                            const int nBits = 3;
                            int num4 = input.readBits(nBits) + 1;
                            for (int i = 0; i < num4; i++)
                            {
                                IDC_SubframePattern_r11 item = IDC_SubframePattern_r11.PerDecoder.Instance.Decode(input);
                                _r.idc_SubframePatternList_r11.Add(item);
                            }
                            return _r;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
