using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class OverloadResponse
    {
        public void InitDefaults()
        {
        }

        public OverloadAction overloadAction { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public OverloadResponse Decode(BitArrayInputStream input)
            {
                OverloadResponse response = new OverloadResponse();
                response.InitDefaults();
                input.readBit();
                if (input.readBits(1) != 0)
                {
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
                int nBits = (input.readBit() == 0) ? 2 : 2;
                response.overloadAction = (OverloadAction)input.readBits(nBits);
                return response;
            }
        }
    }

    [Serializable]
    public class OverloadStart
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public OverloadStart Decode(BitArrayInputStream input)
            {
                OverloadStart start = new OverloadStart();
                start.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                start.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    start.protocolIEs.Add(item);
                }
                return start;
            }
        }
    }

    [Serializable]
    public class OverloadStop
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public OverloadStop Decode(BitArrayInputStream input)
            {
                OverloadStop stop = new OverloadStop();
                stop.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                stop.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    stop.protocolIEs.Add(item);
                }
                return stop;
            }
        }
    }

}
