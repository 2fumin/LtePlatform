using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CSFB_RegistrationParam1XRTT
    {
        public void InitDefaults()
        {
        }

        public bool foreignNIDReg { get; set; }

        public bool foreignSIDReg { get; set; }

        public bool homeReg { get; set; }

        public bool multipleNID { get; set; }

        public bool multipleSID { get; set; }

        public string nid { get; set; }

        public bool parameterReg { get; set; }

        public bool powerUpReg { get; set; }

        public string registrationPeriod { get; set; }

        public string registrationZone { get; set; }

        public string sid { get; set; }

        public string totalZone { get; set; }

        public string zoneTimer { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSFB_RegistrationParam1XRTT Decode(BitArrayInputStream input)
            {
                CSFB_RegistrationParam1XRTT paramxrtt = new CSFB_RegistrationParam1XRTT();
                paramxrtt.InitDefaults();
                paramxrtt.sid = input.readBitString(15);
                paramxrtt.nid = input.readBitString(0x10);
                paramxrtt.multipleSID = input.readBit() == 1;
                paramxrtt.multipleNID = input.readBit() == 1;
                paramxrtt.homeReg = input.readBit() == 1;
                paramxrtt.foreignSIDReg = input.readBit() == 1;
                paramxrtt.foreignNIDReg = input.readBit() == 1;
                paramxrtt.parameterReg = input.readBit() == 1;
                paramxrtt.powerUpReg = input.readBit() == 1;
                paramxrtt.registrationPeriod = input.readBitString(7);
                paramxrtt.registrationZone = input.readBitString(12);
                paramxrtt.totalZone = input.readBitString(3);
                paramxrtt.zoneTimer = input.readBitString(3);
                return paramxrtt;
            }
        }
    }

    [Serializable]
    public class CSFB_RegistrationParam1XRTT_v920
    {
        public void InitDefaults()
        {
        }

        public powerDownReg_r9_Enum powerDownReg_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSFB_RegistrationParam1XRTT_v920 Decode(BitArrayInputStream input)
            {
                CSFB_RegistrationParam1XRTT_v920 _v = new CSFB_RegistrationParam1XRTT_v920();
                _v.InitDefaults();
                int nBits = 1;
                _v.powerDownReg_r9 = (powerDownReg_r9_Enum)input.readBits(nBits);
                return _v;
            }
        }

        public enum powerDownReg_r9_Enum
        {
            _true
        }
    }

    [Serializable]
    public class CSFBParametersRequestCDMA2000
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

            public CSFBParametersRequestCDMA2000_r8_IEs csfbParametersRequestCDMA2000_r8 { get; set; }

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
                            type.csfbParametersRequestCDMA2000_r8 = CSFBParametersRequestCDMA2000_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public CSFBParametersRequestCDMA2000 Decode(BitArrayInputStream input)
            {
                CSFBParametersRequestCDMA2000 tcdma = new CSFBParametersRequestCDMA2000();
                tcdma.InitDefaults();
                tcdma.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return tcdma;
            }
        }
    }

    [Serializable]
    public class CSFBParametersRequestCDMA2000_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public CSFBParametersRequestCDMA2000_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSFBParametersRequestCDMA2000_r8_IEs Decode(BitArrayInputStream input)
            {
                CSFBParametersRequestCDMA2000_r8_IEs es = new CSFBParametersRequestCDMA2000_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    es.nonCriticalExtension = CSFBParametersRequestCDMA2000_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class CSFBParametersRequestCDMA2000_v8a0_IEs
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

            public CSFBParametersRequestCDMA2000_v8a0_IEs Decode(BitArrayInputStream input)
            {
                CSFBParametersRequestCDMA2000_v8a0_IEs es = new CSFBParametersRequestCDMA2000_v8a0_IEs();
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
    public class CSFBParametersResponseCDMA2000
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

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            public CSFBParametersResponseCDMA2000_r8_IEs csfbParametersResponseCDMA2000_r8 { get; set; }

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
                            type.csfbParametersResponseCDMA2000_r8 = CSFBParametersResponseCDMA2000_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public CSFBParametersResponseCDMA2000 Decode(BitArrayInputStream input)
            {
                CSFBParametersResponseCDMA2000 ecdma = new CSFBParametersResponseCDMA2000();
                ecdma.InitDefaults();
                ecdma.rrc_TransactionIdentifier = input.readBits(2);
                ecdma.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return ecdma;
            }
        }
    }

    [Serializable]
    public class CSFBParametersResponseCDMA2000_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public string mobilityParameters { get; set; }

        public CSFBParametersResponseCDMA2000_v8a0_IEs nonCriticalExtension { get; set; }

        public string rand { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSFBParametersResponseCDMA2000_r8_IEs Decode(BitArrayInputStream input)
            {
                CSFBParametersResponseCDMA2000_r8_IEs es = new CSFBParametersResponseCDMA2000_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.rand = input.readBitString(0x20);
                int nBits = input.readBits(8);
                es.mobilityParameters = input.readOctetString(nBits);
                if (stream.Read())
                {
                    es.nonCriticalExtension = CSFBParametersResponseCDMA2000_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class CSFBParametersResponseCDMA2000_v8a0_IEs
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

            public CSFBParametersResponseCDMA2000_v8a0_IEs Decode(BitArrayInputStream input)
            {
                CSFBParametersResponseCDMA2000_v8a0_IEs es = new CSFBParametersResponseCDMA2000_v8a0_IEs();
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
    public class E_CSFB_r9
    {
        public void InitDefaults()
        {
        }

        public string messageContCDMA2000_1XRTT_r9 { get; set; }

        public string messageContCDMA2000_HRPD_r9 { get; set; }

        public mobilityCDMA2000_HRPD_r9_Enum? mobilityCDMA2000_HRPD_r9 { get; set; }

        public CarrierFreqCDMA2000 redirectCarrierCDMA2000_HRPD_r9 { get; set; }

        public enum mobilityCDMA2000_HRPD_r9_Enum
        {
            handover,
            redirection
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_CSFB_r9 Decode(BitArrayInputStream input)
            {
                int nBits;
                E_CSFB_r9 _r = new E_CSFB_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _r.messageContCDMA2000_1XRTT_r9 = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    const int num2 = 1;
                    _r.mobilityCDMA2000_HRPD_r9 = (mobilityCDMA2000_HRPD_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _r.messageContCDMA2000_HRPD_r9 = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    _r.redirectCarrierCDMA2000_HRPD_r9 = CarrierFreqCDMA2000.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

}
