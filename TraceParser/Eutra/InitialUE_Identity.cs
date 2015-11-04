using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class InitialUE_Identity
    {
        public void InitDefaults()
        {
        }

        public string randomValue { get; set; }

        public S_TMSI s_TMSI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InitialUE_Identity Decode(BitArrayInputStream input)
            {
                InitialUE_Identity identity = new InitialUE_Identity();
                identity.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        identity.s_TMSI = S_TMSI.PerDecoder.Instance.Decode(input);
                        return identity;

                    case 1:
                        identity.randomValue = input.readBitString(40);
                        return identity;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }
}
