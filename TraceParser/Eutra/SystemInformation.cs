using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformation
    {
        public static void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public static void InitDefaults()
            {
            }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            public SystemInformation_r8_IEs systemInformation_r8 { get; set; }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public static void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public static criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        var type = new criticalExtensionsFuture_Type();
                        InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    var type = new criticalExtensions_Type();
                    InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.systemInformation_r8 = SystemInformation_r8_IEs.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public static SystemInformation Decode(BitArrayInputStream input)
            {
                var information = new SystemInformation();
                InitDefaults();
                information.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return information;
            }
        }
    }
}
