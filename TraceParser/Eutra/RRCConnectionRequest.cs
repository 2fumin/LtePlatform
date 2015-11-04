using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRCConnectionRequest
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

            public RRCConnectionRequest_r8_IEs rrcConnectionRequest_r8 { get; set; }

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
                            type.rrcConnectionRequest_r8 = RRCConnectionRequest_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public RRCConnectionRequest Decode(BitArrayInputStream input)
            {
                RRCConnectionRequest request = new RRCConnectionRequest();
                request.InitDefaults();
                request.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return request;
            }
        }
    }

    [Serializable]
    public class RRCConnectionRequest_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public EstablishmentCause establishmentCause { get; set; }

        public string spare { get; set; }

        public InitialUE_Identity ue_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionRequest_r8_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionRequest_r8_IEs es = new RRCConnectionRequest_r8_IEs();
                es.InitDefaults();
                es.ue_Identity = InitialUE_Identity.PerDecoder.Instance.Decode(input);
                int nBits = 3;
                es.establishmentCause = (EstablishmentCause)input.readBits(nBits);
                es.spare = input.readBitString(1);
                return es;
            }
        }
    }

}
