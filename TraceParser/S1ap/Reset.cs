using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class Reset
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Reset Decode(BitArrayInputStream input)
            {
                Reset reset = new Reset();
                reset.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                reset.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    reset.protocolIEs.Add(item);
                }
                return reset;
            }
        }
    }

    [Serializable]
    public class ResetAcknowledge
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ResetAcknowledge Decode(BitArrayInputStream input)
            {
                ResetAcknowledge acknowledge = new ResetAcknowledge();
                acknowledge.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                acknowledge.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    acknowledge.protocolIEs.Add(item);
                }
                return acknowledge;
            }
        }
    }

    [Serializable]
    public class ResetType
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> partOfS1_Interface { get; set; }

        public ResetAll s1_Interface { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ResetType Decode(BitArrayInputStream input)
            {
                int num4;
                ResetType type = new ResetType();
                type.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        num4 = 1;
                        type.s1_Interface = (ResetAll)input.readBits(num4);
                        return type;

                    case 1:
                        {
                            input.skipUnreadedBits();
                            type.partOfS1_Interface = new List<ProtocolIE_Field>();
                            num4 = 8;
                            int num6 = input.readBits(num4) + 1;
                            for (int i = 0; i < num6; i++)
                            {
                                ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                                type.partOfS1_Interface.Add(item);
                            }
                            return type;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
