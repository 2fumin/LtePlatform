using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SecurityAlgorithmConfig
    {
        public void InitDefaults()
        {
        }

        public cipheringAlgorithm_Enum cipheringAlgorithm { get; set; }

        public integrityProtAlgorithm_Enum integrityProtAlgorithm { get; set; }

        public enum cipheringAlgorithm_Enum
        {
            eea0,
            eea1,
            eea2,
            eea3_v1130,
            spare4,
            spare3,
            spare2,
            spare1
        }

        public enum integrityProtAlgorithm_Enum
        {
            eia0_v920,
            eia1,
            eia2,
            eia3_v1130,
            spare4,
            spare3,
            spare2,
            spare1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SecurityAlgorithmConfig Decode(BitArrayInputStream input)
            {
                SecurityAlgorithmConfig config = new SecurityAlgorithmConfig();
                config.InitDefaults();
                int nBits = (input.readBit() == 0) ? 3 : 3;
                config.cipheringAlgorithm = (cipheringAlgorithm_Enum)input.readBits(nBits);
                nBits = (input.readBit() == 0) ? 3 : 3;
                config.integrityProtAlgorithm = (integrityProtAlgorithm_Enum)input.readBits(nBits);
                return config;
            }
        }
    }

    [Serializable]
    public class SecurityConfigHO
    {
        public void InitDefaults()
        {
        }

        public handoverType_Type handoverType { get; set; }

        [Serializable]
        public class handoverType_Type
        {
            public void InitDefaults()
            {
            }

            public interRAT_Type interRAT { get; set; }

            public intraLTE_Type intraLTE { get; set; }

            [Serializable]
            public class interRAT_Type
            {
                public void InitDefaults()
                {
                }

                public string nas_SecurityParamToEUTRA { get; set; }

                public SecurityAlgorithmConfig securityAlgorithmConfig { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public interRAT_Type Decode(BitArrayInputStream input)
                    {
                        interRAT_Type type = new interRAT_Type();
                        type.InitDefaults();
                        type.securityAlgorithmConfig = SecurityAlgorithmConfig.PerDecoder.Instance.Decode(input);
                        type.nas_SecurityParamToEUTRA = input.readOctetString(6);
                        return type;
                    }
                }
            }

            [Serializable]
            public class intraLTE_Type
            {
                public void InitDefaults()
                {
                }

                public bool keyChangeIndicator { get; set; }

                public long nextHopChainingCount { get; set; }

                public SecurityAlgorithmConfig securityAlgorithmConfig { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public intraLTE_Type Decode(BitArrayInputStream input)
                    {
                        intraLTE_Type type = new intraLTE_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 1);
                        if (stream.Read())
                        {
                            type.securityAlgorithmConfig = SecurityAlgorithmConfig.PerDecoder.Instance.Decode(input);
                        }
                        type.keyChangeIndicator = input.readBit() == 1;
                        type.nextHopChainingCount = input.readBits(3);
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public handoverType_Type Decode(BitArrayInputStream input)
                {
                    handoverType_Type type = new handoverType_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.intraLTE = intraLTE_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.interRAT = interRAT_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SecurityConfigHO Decode(BitArrayInputStream input)
            {
                SecurityConfigHO gho = new SecurityConfigHO();
                gho.InitDefaults();
                input.readBit();
                gho.handoverType = handoverType_Type.PerDecoder.Instance.Decode(input);
                return gho;
            }
        }
    }

    [Serializable]
    public class SecurityConfigSMC
    {
        public void InitDefaults()
        {
        }

        public SecurityAlgorithmConfig securityAlgorithmConfig { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SecurityConfigSMC Decode(BitArrayInputStream input)
            {
                SecurityConfigSMC gsmc = new SecurityConfigSMC();
                gsmc.InitDefaults();
                input.readBit();
                gsmc.securityAlgorithmConfig = SecurityAlgorithmConfig.PerDecoder.Instance.Decode(input);
                return gsmc;
            }
        }
    }

    [Serializable]
    public class SecurityModeCommand
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

                public SecurityModeCommand_r8_IEs securityModeCommand_r8 { get; set; }

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
                                type.securityModeCommand_r8 = SecurityModeCommand_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public SecurityModeCommand Decode(BitArrayInputStream input)
            {
                SecurityModeCommand command = new SecurityModeCommand();
                command.InitDefaults();
                command.rrc_TransactionIdentifier = input.readBits(2);
                command.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return command;
            }
        }
    }

    [Serializable]
    public class SecurityModeCommand_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public SecurityModeCommand_v8a0_IEs nonCriticalExtension { get; set; }

        public SecurityConfigSMC securityConfigSMC { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SecurityModeCommand_r8_IEs Decode(BitArrayInputStream input)
            {
                SecurityModeCommand_r8_IEs es = new SecurityModeCommand_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.securityConfigSMC = SecurityConfigSMC.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.nonCriticalExtension = SecurityModeCommand_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SecurityModeCommand_v8a0_IEs
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

            public SecurityModeCommand_v8a0_IEs Decode(BitArrayInputStream input)
            {
                SecurityModeCommand_v8a0_IEs es = new SecurityModeCommand_v8a0_IEs();
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
    public class SecurityModeComplete
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

            public SecurityModeComplete_r8_IEs securityModeComplete_r8 { get; set; }

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
                            type.securityModeComplete_r8 = SecurityModeComplete_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public SecurityModeComplete Decode(BitArrayInputStream input)
            {
                SecurityModeComplete complete = new SecurityModeComplete();
                complete.InitDefaults();
                complete.rrc_TransactionIdentifier = input.readBits(2);
                complete.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return complete;
            }
        }
    }

    [Serializable]
    public class SecurityModeComplete_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public SecurityModeComplete_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SecurityModeComplete_r8_IEs Decode(BitArrayInputStream input)
            {
                SecurityModeComplete_r8_IEs es = new SecurityModeComplete_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    es.nonCriticalExtension = SecurityModeComplete_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SecurityModeComplete_v8a0_IEs
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

            public SecurityModeComplete_v8a0_IEs Decode(BitArrayInputStream input)
            {
                SecurityModeComplete_v8a0_IEs es = new SecurityModeComplete_v8a0_IEs();
                es.InitDefaults();
                bool flag = false;
                BitMaskStream stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
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
    public class SecurityModeFailure
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

            public SecurityModeFailure_r8_IEs securityModeFailure_r8 { get; set; }

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
                            type.securityModeFailure_r8 = SecurityModeFailure_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public SecurityModeFailure Decode(BitArrayInputStream input)
            {
                SecurityModeFailure failure = new SecurityModeFailure();
                failure.InitDefaults();
                failure.rrc_TransactionIdentifier = input.readBits(2);
                failure.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return failure;
            }
        }
    }

    [Serializable]
    public class SecurityModeFailure_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public SecurityModeFailure_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SecurityModeFailure_r8_IEs Decode(BitArrayInputStream input)
            {
                SecurityModeFailure_r8_IEs es = new SecurityModeFailure_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    es.nonCriticalExtension = SecurityModeFailure_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class SecurityModeFailure_v8a0_IEs
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

            public SecurityModeFailure_v8a0_IEs Decode(BitArrayInputStream input)
            {
                SecurityModeFailure_v8a0_IEs es = new SecurityModeFailure_v8a0_IEs();
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
