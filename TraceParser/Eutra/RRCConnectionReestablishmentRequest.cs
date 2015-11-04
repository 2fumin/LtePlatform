using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRCConnectionReestablishmentRequest
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

            public RRCConnectionReestablishmentRequest_r8_IEs rrcConnectionReestablishmentRequest_r8 { get; set; }

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
                            type.rrcConnectionReestablishmentRequest_r8 = RRCConnectionReestablishmentRequest_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public RRCConnectionReestablishmentRequest Decode(BitArrayInputStream input)
            {
                RRCConnectionReestablishmentRequest request = new RRCConnectionReestablishmentRequest();
                request.InitDefaults();
                request.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return request;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReestablishmentRequest_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public ReestablishmentCause reestablishmentCause { get; set; }

        public string spare { get; set; }

        public ReestabUE_Identity ue_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReestablishmentRequest_r8_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReestablishmentRequest_r8_IEs es = new RRCConnectionReestablishmentRequest_r8_IEs();
                es.InitDefaults();
                es.ue_Identity = ReestabUE_Identity.PerDecoder.Instance.Decode(input);
                int nBits = 2;
                es.reestablishmentCause = (ReestablishmentCause)input.readBits(nBits);
                es.spare = input.readBitString(2);
                return es;
            }
        }
    }
}
