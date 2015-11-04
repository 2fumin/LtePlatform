using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRCConnectionReconfiguration
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

                public RRCConnectionReconfiguration_r8_IEs rrcConnectionReconfiguration_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public object spare4 { get; set; }

                public object spare5 { get; set; }

                public object spare6 { get; set; }

                public object spare7 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(3))
                        {
                            case 0:
                                type.rrcConnectionReconfiguration_r8 
                                    = RRCConnectionReconfiguration_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;

                            case 4:
                                return type;

                            case 5:
                                return type;

                            case 6:
                                return type;

                            case 7:
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

            public RRCConnectionReconfiguration Decode(BitArrayInputStream input)
            {
                RRCConnectionReconfiguration reconfiguration = new RRCConnectionReconfiguration();
                reconfiguration.InitDefaults();
                reconfiguration.rrc_TransactionIdentifier = input.readBits(2);
                reconfiguration.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return reconfiguration;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReconfiguration_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public List<string> dedicatedInfoNASList { get; set; }

        public MeasConfig measConfig { get; set; }

        public MobilityControlInfo mobilityControlInfo { get; set; }

        public RRCConnectionReconfiguration_v890_IEs nonCriticalExtension { get; set; }

        public RadioResourceConfigDedicated radioResourceConfigDedicated { get; set; }

        public SecurityConfigHO securityConfigHO { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReconfiguration_r8_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReconfiguration_r8_IEs es = new RRCConnectionReconfiguration_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 6);
                if (stream.Read())//measConfigPresent=True
                {
                    es.measConfig = MeasConfig.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())//mobilityControlInfoPresent=True
                {
                    es.mobilityControlInfo = MobilityControlInfo.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())//dedicatedInfoNASListPresent=True
                {
                    es.dedicatedInfoNASList = new List<string>();
                    const int num2 = 4;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        int nBits = input.readBits(8);
                        string item = input.readOctetString(nBits);
                        es.dedicatedInfoNASList.Add(item);
                    }
                }
                if (stream.Read())//radioResourceConfigDedicatedPresent=True
                {
                    es.radioResourceConfigDedicated = RadioResourceConfigDedicated.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())//securityConfigHOPresent=True
                {
                    es.securityConfigHO = SecurityConfigHO.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())//nonCriticalExtensionPresent=True
                {
                    es.nonCriticalExtension = RRCConnectionReconfiguration_v890_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReconfiguration_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public RRCConnectionReconfiguration_v1130_IEs nonCriticalExtension { get; set; }

        public List<SCellToAddMod_r10> sCellToAddModList_r10 { get; set; }

        public List<long> sCellToReleaseList_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReconfiguration_v1020_IEs Decode(BitArrayInputStream input)
            {
                int num2;
                RRCConnectionReconfiguration_v1020_IEs es = new RRCConnectionReconfiguration_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.sCellToReleaseList_r10 = new List<long>();
                    num2 = 2;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(3) + 1;
                        es.sCellToReleaseList_r10.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.sCellToAddModList_r10 = new List<SCellToAddMod_r10>();
                    num2 = 2;
                    int num6 = input.readBits(num2) + 1;
                    for (int j = 0; j < num6; j++)
                    {
                        SCellToAddMod_r10 _r = SCellToAddMod_r10.PerDecoder.Instance.Decode(input);
                        es.sCellToAddModList_r10.Add(_r);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionReconfiguration_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReconfiguration_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public string systemInfomationBlockType1Dedicated_r11 { get; set; }

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

            public RRCConnectionReconfiguration_v1130_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReconfiguration_v1130_IEs es = new RRCConnectionReconfiguration_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.systemInfomationBlockType1Dedicated_r11 = input.readOctetString(nBits);
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
    public class RRCConnectionReconfiguration_v890_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public RRCConnectionReconfiguration_v920_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReconfiguration_v890_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReconfiguration_v890_IEs es = new RRCConnectionReconfiguration_v890_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionReconfiguration_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReconfiguration_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public fullConfig_r9_Enum? fullConfig_r9 { get; set; }

        public RRCConnectionReconfiguration_v1020_IEs nonCriticalExtension { get; set; }

        public OtherConfig_r9 otherConfig_r9 { get; set; }

        public enum fullConfig_r9_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReconfiguration_v920_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReconfiguration_v920_IEs es = new RRCConnectionReconfiguration_v920_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.otherConfig_r9 = OtherConfig_r9.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.fullConfig_r9 = (fullConfig_r9_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionReconfiguration_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

}
