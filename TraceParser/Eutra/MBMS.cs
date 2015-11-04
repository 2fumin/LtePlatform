using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MBMS_NotificationConfig_r9
    {
        public void InitDefaults()
        {
        }

        public long notificationOffset_r9 { get; set; }

        public notificationRepetitionCoeff_r9_Enum notificationRepetitionCoeff_r9 { get; set; }

        public long notificationSF_Index_r9 { get; set; }

        public enum notificationRepetitionCoeff_r9_Enum
        {
            n2,
            n4
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBMS_NotificationConfig_r9 Decode(BitArrayInputStream input)
            {
                MBMS_NotificationConfig_r9 _r = new MBMS_NotificationConfig_r9();
                _r.InitDefaults();
                const int nBits = 1;
                _r.notificationRepetitionCoeff_r9 = (notificationRepetitionCoeff_r9_Enum)input.readBits(nBits);
                _r.notificationOffset_r9 = input.readBits(4);
                _r.notificationSF_Index_r9 = input.readBits(3) + 1;
                return _r;
            }
        }
    }

    [Serializable]
    public class MBMS_SAI_InterFreq_r11
    {
        public void InitDefaults()
        {
        }

        public long dl_CarrierFreq_r11 { get; set; }

        public List<long> mbms_SAI_List_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBMS_SAI_InterFreq_r11 Decode(BitArrayInputStream input)
            {
                MBMS_SAI_InterFreq_r11 _r = new MBMS_SAI_InterFreq_r11();
                _r.InitDefaults();
                _r.dl_CarrierFreq_r11 = input.readBits(0x12);
                _r.mbms_SAI_List_r11 = new List<long>();
                const int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    long item = input.readBits(0x10);
                    _r.mbms_SAI_List_r11.Add(item);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class MBMS_SAI_InterFreq_v1140
    {
        public void InitDefaults()
        {
        }

        public List<long> multiBandInfoList_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBMS_SAI_InterFreq_v1140 Decode(BitArrayInputStream input)
            {
                MBMS_SAI_InterFreq_v1140 _v = new MBMS_SAI_InterFreq_v1140();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _v.multiBandInfoList_r11 = new List<long>();
                    const int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(8) + 1;
                        _v.multiBandInfoList_r11.Add(item);
                    }
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class MBMS_SessionInfo_r9
    {
        public void InitDefaults()
        {
        }

        public long logicalChannelIdentity_r9 { get; set; }

        public string sessionId_r9 { get; set; }

        public TMGI_r9 tmgi_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBMS_SessionInfo_r9 Decode(BitArrayInputStream input)
            {
                MBMS_SessionInfo_r9 _r = new MBMS_SessionInfo_r9();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                _r.tmgi_r9 = TMGI_r9.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    _r.sessionId_r9 = input.readOctetString(1);
                }
                _r.logicalChannelIdentity_r9 = input.readBits(5);
                return _r;
            }
        }
    }

    [Serializable]
    public class MBMSCountingRequest_r10
    {
        public void InitDefaults()
        {
        }

        public List<CountingRequestInfo_r10> countingRequestList_r10 { get; set; }

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

            public MBMSCountingRequest_r10 Decode(BitArrayInputStream input)
            {
                MBMSCountingRequest_r10 _r = new MBMSCountingRequest_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                _r.countingRequestList_r10 = new List<CountingRequestInfo_r10>();
                const int num2 = 4;
                int num3 = input.readBits(num2) + 1;
                for (int i = 0; i < num3; i++)
                {
                    CountingRequestInfo_r10 item = CountingRequestInfo_r10.PerDecoder.Instance.Decode(input);
                    _r.countingRequestList_r10.Add(item);
                }
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    _r.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class MBMSCountingResponse_r10
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

                public MBMSCountingResponse_r10_IEs countingResponse_r10 { get; set; }

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
                                type.countingResponse_r10 = MBMSCountingResponse_r10_IEs.PerDecoder.Instance.Decode(input);
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

            public MBMSCountingResponse_r10 Decode(BitArrayInputStream input)
            {
                MBMSCountingResponse_r10 _r = new MBMSCountingResponse_r10();
                _r.InitDefaults();
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class MBMSCountingResponse_r10_IEs
    {
        public void InitDefaults()
        {
        }

        public List<CountingResponseInfo_r10> countingResponseList_r10 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public long? mbsfn_AreaIndex_r10 { get; set; }

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

            public MBMSCountingResponse_r10_IEs Decode(BitArrayInputStream input)
            {
                MBMSCountingResponse_r10_IEs es = new MBMSCountingResponse_r10_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    es.mbsfn_AreaIndex_r10 = input.readBits(3);
                }
                if (stream.Read())
                {
                    es.countingResponseList_r10 = new List<CountingResponseInfo_r10>();
                    const int num2 = 4;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        CountingResponseInfo_r10 item = CountingResponseInfo_r10.PerDecoder.Instance.Decode(input);
                        es.countingResponseList_r10.Add(item);
                    }
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
    public class MBMSInterestIndication_r11
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

                public MBMSInterestIndication_r11_IEs interestIndication_r11 { get; set; }

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
                                type.interestIndication_r11 = MBMSInterestIndication_r11_IEs.PerDecoder.Instance.Decode(input);
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

            public MBMSInterestIndication_r11 Decode(BitArrayInputStream input)
            {
                MBMSInterestIndication_r11 _r = new MBMSInterestIndication_r11();
                _r.InitDefaults();
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class MBMSInterestIndication_r11_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public List<long> mbms_FreqList_r11 { get; set; }

        public mbms_Priority_r11_Enum? mbms_Priority_r11 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public enum mbms_Priority_r11_Enum
        {
            _true
        }

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

            public MBMSInterestIndication_r11_IEs Decode(BitArrayInputStream input)
            {
                int num2;
                MBMSInterestIndication_r11_IEs es = new MBMSInterestIndication_r11_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    es.mbms_FreqList_r11 = new List<long>();
                    num2 = 3;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(0x12);
                        es.mbms_FreqList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    num2 = 1;
                    es.mbms_Priority_r11 = (mbms_Priority_r11_Enum)input.readBits(num2);
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

}
